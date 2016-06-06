using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MiscUtil.IO;

namespace NetML
{
    public static class SourceGenerator
    {
        public static void GenerateSource(string OutputPath, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams, IEnumerable<Domain> Domains)
        {
            using (var fs = File.Open(OutputPath, FileMode.Create))
            {
                GenerateHeader(fs, Parameters);
                GenerateNet(fs, Parameters, Nodes, Links, Streams, Domains);
                GenerateTraces(fs, Parameters);
                GenerateStreamParameters(fs, Parameters, Streams);
                GenerateMain(fs, Parameters, Nodes, Links, Streams, Domains);
            }
        }

        private static void GenerateHeader(System.IO.Stream OutputStream, SimulationParameters Parameters)
        {
            var headerContent = File.ReadAllBytes(@"Source Fragments/header.txt");
            OutputStream.Write(headerContent, 0, headerContent.Length);
        }

        // <%%nodes%%> - The network nodes.
        // <%%links%%> - The network links.
        // <%%streams%%> - The network streams.
        // <%%domains%%> - The network domains.
        // <%%nodeAtts%%> - Attribute display of the network nodes.
        // <%%linkAtts%%> - Attribute display of the network links.
        // <%%streamAtts%%> - Attribute display of the network streams.
        // <%%domainAtts%%> - Attribute display of the network domains.
        private static void GenerateNet(System.IO.Stream OutputStream, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams, IEnumerable<Domain> Domains)
        {
            using (var sw = new NonClosingStreamWrapper(OutputStream))
            {
                using (var write = new StreamWriter(sw))
                {
                    var template = File.ReadAllText(@"Source Fragments/net.txt");

                    var sb = new StringBuilder();
                    foreach (var node in Nodes)
                    {
                        sb.Append($"    Ptr<Node> N{node.Name};\n");
                    }
                    var nodes = sb.ToString();

                    sb.Clear();
                    foreach (var link in Links)
                    {
                        sb.Append($"    Ptr<NetDevice> L{link.Name}");
                        if (link.Duplex)
                        {
                            sb.Append($", L{link.Name}_rev;\n");
                        }
                        else
                        {
                            sb.Append(";\n");
                        }
                    }
                    var links = sb.ToString();

                    sb.Clear();
                    foreach (var stream in Streams)
                    {
                        sb.Append($"    Ptr<Object> S{stream.Name};\n");
                        sb.Append($"    Ptr<Object> C{stream.Name};\n");
                    }
                    var streams = sb.ToString();

                    sb.Clear();
                    foreach (var domain in Domains)
                    {
                        foreach (var node in domain.Nodes)
                        {
                            sb.Append($"    Ptr<NetDevice> D{domain.Name}_{node.Name};\n");
                        }
                    }
                    var domains = sb.ToString();

                    sb.Clear();
                    foreach (var node in Nodes)
                    {
                        sb.Append($"    try {{ printMyAttributes(\"{node.Text}\", \"\", attsfile, net.N{node.Name}); }}\n");
                        sb.Append($"    catch (...) {{ attsfile << \"Unable to print the attributes of net.N{node.Name}\"; }}\n");
                    }
                    var nodeAtts = sb.ToString();

                    sb.Clear();
                    foreach (var link in Links)
                    {
                        sb.Append($"    printMyAttributes(\"{link.Text}\", \"\", attsfile, net.L{link.Name});\n");
                        if (link.Duplex)
                        {
                            sb.Append($"    printMyAttributes(\"{link.Text}_rev\", \"\", attsfile, net.L{link.Name}_rev);\n");
                        }
                    }
                    var linkAtts = sb.ToString();

                    sb.Clear();
                    foreach (var stream in Streams)
                    {
                        sb.Append($"    printMyAttributes(\"{stream.Text}_server\", \"\", attsfile, net.S{stream.Name});\n");
                        sb.Append($"    printMyAttributes(\"{stream.Text}_client\", \"\", attsfile, net.C{stream.Name});\n");
                    }
                    var streamAtts = sb.ToString();

                    sb.Clear();
                    foreach (var domain in Domains)
                    {
                        foreach (var node in domain.Nodes)
                        {
                            sb.Append($"    printMyAttributes(\"{domain.Name}_{node.Name}\", \"\", attsfile, net.D{domain.Name}_{node.Name});\n");
                        }
                    }
                    var domainAtts = sb.ToString();

                    template = template.Replace("<%%nodes%%>", nodes);
                    template = template.Replace("<%%links%%>", links);
                    template = template.Replace("<%%streams%%>", streams);
                    template = template.Replace("<%%domains%%>", domains);
                    template = template.Replace("<%%nodeAtts%%>", nodeAtts);
                    template = template.Replace("<%%linkAtts%%>", linkAtts);
                    template = template.Replace("<%%streamAtts%%>", streamAtts);
                    template = template.Replace("<%%domainAtts%%>", domainAtts);

                    write.Write(template);
                }
            }
        }

        // <%%variables%%> - The variables to hold the trace data.
        // <%%functions%%> - The functions for incrementing the trace data.
        // <%%commonTraces%%> - Register trace functions with common start time.
        // <%%uncommonTraces%%> - Register trace functions with uncommon start time.
        private static void GenerateTraces(System.IO.Stream OutputStream, SimulationParameters Parameters)
        {
            using (var sw = new NonClosingStreamWrapper(OutputStream))
            {
                using (var write = new StreamWriter(sw))
                {
                    var template = File.ReadAllText(@"Source Fragments/traces.txt");

                    var sb = new StringBuilder();
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"// Process trace {trace.Name}\n");
                        sb.Append($"static int T{trace.Name};\n");
                        sb.Append($"static std::ofstream T{trace.Name}_FStream;\n");
                        sb.Append($"static int SeqFirst{trace.Name};\n");
                        sb.Append($"static int AckFirst{trace.Name};\n\n");
                    }
                    var variables = sb.ToString();

                    sb.Clear();
                    var tracers = new Dictionary<string, int>();
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"// Process trace {trace.Name}\n");
                        foreach (var traceAttribute in trace.Attributes)
                        {
                            if (traceAttribute.Element is Domain)
                            {
                                var domain = traceAttribute.Element as Domain;
                                foreach (var node in domain.Nodes)
                                {
                                    var tracerName = $"T{trace.Name}_{node.Name}_{traceAttribute.TraceSource}Tracer";
                                    if (tracers.ContainsKey(tracerName))
                                    {
                                        tracers[tracerName]++;
                                    }
                                    else
                                    {
                                        tracers.Add(tracerName, 1);
                                    }
                                    sb.Append($"void {tracerName}{tracers[tracerName]}({traceAttribute.AttributeType})\n");
                                    sb.Append($"{{\n");
                                    sb.Append($"    T{trace.Name}_FStream << Simulator::Now().GetSeconds() << \" \" << {traceAttribute.Code} << std::endl;\n");
                                    sb.Append($"}}\n\n");
                                }
                            }
                            else
                            {
                                var tracerName = $"T{trace.Name}_{traceAttribute.TraceSource}Tracer";
                                if (tracers.ContainsKey(tracerName))
                                {
                                    tracers[tracerName]++;
                                }
                                else
                                {
                                    tracers.Add(tracerName, 1);
                                }
                                sb.Append($"void {tracerName}{tracers[tracerName]}({traceAttribute.AttributeType})\n");
                                sb.Append($"{{\n");
                                sb.Append($"    T{trace.Name}_FStream << Simulator::Now().GetSeconds() << \" \" << {traceAttribute.Code} << std::endl;\n");
                                sb.Append($"}}\n\n");
                            }
                        }
                    }
                    var functions = sb.ToString();

                    sb.Clear();
                    var sbUncommon = new StringBuilder();
                    tracers = new Dictionary<string, int>();
                    foreach (var trace in Parameters.Traces)
                    {
                        // Change the output string builder so that common and uncommon traces are split.
                        var outputBuilder = trace.CommonStartTime ? sb : sbUncommon;

                        foreach (var attribute in trace.Attributes)
                        {
                            if (attribute.Element is Domain)
                            {
                                var domain = attribute.Element as Domain;
                                foreach (var node in domain.Nodes)
                                {
                                    var networkItem = $"N{node.Name}";

                                    var tracerName = $"T{trace.Name}_{node.Name}_{attribute.TraceSource}Tracer";
                                    if (tracers.ContainsKey(tracerName))
                                    {
                                        tracers[tracerName]++;
                                    }
                                    else
                                    {
                                        tracers.Add(tracerName, 1);
                                    }

                                    if (!trace.CommonStartTime)
                                    {
                                        outputBuilder.Append($"void setupT{trace.EscapedName}_{attribute.TraceSource}()\n{{\n");
                                    }

                                    outputBuilder.Append($"    // Set up tracing for T{trace.Name} of type {attribute.AttributeType}\n");
                                    outputBuilder.Append($"    if (TraceConnectNoContext(net.{networkItem}, \"{attribute.TraceSource}\", MakeCallback(&{tracerName}{tracers[tracerName]})))\n");
                                    outputBuilder.Append($"    {{\n");
                                    outputBuilder.Append($"        NS_LOG_INFO(\"Tracing for {attribute.TraceSource} at node {networkItem} was successfully set up\");\n");
                                    outputBuilder.Append($"    }}\n");
                                    outputBuilder.Append($"    else\n");
                                    outputBuilder.Append($"    {{\n");
                                    outputBuilder.Append($"        NS_LOG_ERROR(\"Tracing for {attribute.TraceSource} at node {networkItem} was not successfully set up\");\n");
                                    outputBuilder.Append($"    }}\n\n");

                                    if (!trace.CommonStartTime)
                                    {
                                        outputBuilder.Append($"}}\n");
                                    }
                                }
                            }
                            else
                            {
                                var networkItem = "";
                                if (attribute.Element is Link)
                                {
                                    networkItem = $"L{(attribute.Element as Link).Name}{(attribute.LinkReverse ? "_rev" : "")}";
                                }
                                else if (attribute.Element is Stream)
                                {
                                    switch (attribute.TraceSource)
                                    {
                                        case "Tx":
                                            {
                                                networkItem = $"C{(attribute.Element as Stream).Name}";
                                                break;
                                            }
                                        case "Rx":
                                            {
                                                networkItem = $"S{(attribute.Element as Stream).Name}";
                                                break;
                                            }
                                        case "CongestionWindow":
                                            {
                                                networkItem = $"N{(attribute.Element as Stream).StartNode.Name}";
                                                break;
                                            }
                                    }
                                }
                                else if (attribute.Element is Node)
                                {
                                    networkItem = $"N{(attribute.Element as Node).Name}";
                                }

                                var tracerName = $"T{trace.Name}_{attribute.TraceSource}Tracer";
                                if (tracers.ContainsKey(tracerName))
                                {
                                    tracers[tracerName]++;
                                }
                                else
                                {
                                    tracers.Add(tracerName, 1);
                                }

                                if (!trace.CommonStartTime)
                                {
                                    outputBuilder.Append($"void setupT{trace.EscapedName}_{attribute.TraceSource}()\n{{\n");
                                }

                                outputBuilder.Append($"    // Set up tracing for T{trace.Name} of type {attribute.AttributeType}\n");
                                outputBuilder.Append($"    if (TraceConnectNoContext(net.{networkItem}, \"{attribute.TraceSource}\", MakeCallback(&{tracerName}{tracers[tracerName]})))\n");
                                outputBuilder.Append($"    {{\n");
                                outputBuilder.Append($"        NS_LOG_INFO(\"Tracing for {attribute.TraceSource} at node {networkItem} was successfully set up\");\n");
                                outputBuilder.Append($"    }}\n");
                                outputBuilder.Append($"    else\n");
                                outputBuilder.Append($"    {{\n");
                                outputBuilder.Append($"        NS_LOG_ERROR(\"Tracing for {attribute.TraceSource} at node {networkItem} was not successfully set up\");\n");
                                outputBuilder.Append($"    }}\n\n");

                                if (!trace.CommonStartTime)
                                {
                                    outputBuilder.Append($"}}\n");
                                }
                            }
                        }
                    }
                    var commonTrace = sb.ToString();
                    var uncommonTrace = sbUncommon.ToString();

                    template = template.Replace("<%%variables%%>", variables);
                    template = template.Replace("<%%functions%%>", functions);
                    template = template.Replace("<%%commonTrace%%>", commonTrace);
                    template = template.Replace("<%%uncommonTrace%%>", uncommonTrace);

                    write.Write(template);
                }
            }
        }

        private static void GenerateStreamParameters(System.IO.Stream OutputStream, SimulationParameters Parameters, IEnumerable<Stream> Streams)
        {
            using (var sw = new NonClosingStreamWrapper(OutputStream))
            {
                using (var write = new StreamWriter(sw))
                {
                    foreach (var stream in Streams)
                    {
                        var sb = new StringBuilder();

                        sb.Append($"void SetPars_{stream.Name}()\n{{\n");

                        if (stream.Type == Stream.StreamType.BulkFTP)
                        {
                            sb.Append($"    Ptr<Object> streamob;\n\n");

                            sb.Append($"    UintegerValue {stream.Name}_SndBufSize_in_bytes = {stream.StartSendBufferSize};\n");
                            sb.Append($"    streamob = getInnerObject(net.N{stream.StartNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "SndBufSize_in_bytes", stream.StartSendBufferSize.ToString()));
                            sb.Append($"    UintegerValue {stream.Name}_MaxWindowSize_in_bytes = {stream.StartMaxWindowSize};\n");
                            sb.Append($"    streamob = getInnerObject(net.N{stream.StartNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "MaxWindowSize_in_bytes", stream.StartMaxWindowSize.ToString()));

                            sb.Append($"    {stream.Name}_SndBufSize_in_bytes = {stream.EndSendBufferSize};\n");
                            sb.Append($"    streamob = getInnerObject(net.N{stream.EndNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "SndBufSize_in_bytes", stream.StartSendBufferSize.ToString()));
                            sb.Append($"    {stream.Name}_MaxWindowSize_in_bytes = {stream.EndMaxWindowSize};\n");
                            sb.Append($"    streamob = getInnerObject(net.N{stream.EndNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "MaxWindowSize_in_bytes", stream.StartMaxWindowSize.ToString()));
                            sb.Append($"    \n");
                        }

                        sb.Append($"}}\n\n");

                        write.Write(sb.ToString());
                    }
                }
            }
        }

        // <%%printAtts%%> - Whether to print network attributes.
        // <%%asciiTrace%%> - Whether to output ascii tracing.
        // <%%networkName%%> - The name of the network.
        // <%%openTracing%%> - Initial set up for tracing filestreams and buffers.
        // <%%componentLog%%> - Enabling logging for components.
        // <%%nodes%%> - Set up of the nodes.
        // <%%links%%> - Set up of the links.
        // <%%streams%%> - Set up of the streams.
        // <%%domains%%> - Set up of the domains.
        // <%%stopTime%%> - Time in seconds after starting to end the simulation.
        // <%%observationTime%%> - Time after the simulation starts to begin observation.
        // <%%closeTracing%%> - Close the trace streams.
        private static void GenerateMain(System.IO.Stream OutputStream, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams, IEnumerable<Domain> Domains)
        {
            using (var sw = new NonClosingStreamWrapper(OutputStream))
            {
                using (var write = new StreamWriter(sw))
                {
                    var template = File.ReadAllText(@"Source Fragments/main.txt");

                    var sb = new StringBuilder();
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"    T{trace.Name} = 0; // Avoid warnings\n");
                        sb.Append($"    SeqFirst{trace.Name} = -1; // Initialise\n");
                        sb.Append($"    AckFirst{trace.Name} = -1; // Initialise\n");
                        sb.Append($"    T{trace.Name}_FStream.open(\"{Parameters.Name}_T{trace.Name}_trace.txt\");\n");
                        sb.Append($"    // Stop gnuplot complaining.\n    T{trace.Name}_FStream << \"0 0\" << std::endl;\n");
                        if (!trace.CommonStartTime)
                        {
                            foreach (var attribute in trace.Attributes)
                            {
                                sb.Append($"    Simulator::Schedule(Seconds({trace.StartTime}), &setupT{trace.EscapedName}_{attribute.TraceSource});\n");
                            }
                        }
                    }
                    var openTracing = sb.ToString();

                    sb.Clear();
                    foreach (var componentLog in Parameters.ComponentLogs)
                    {
                        sb.Append($"    LogComponentEnable(\"{componentLog.LoggingModule.ToString()}\", (LogLevel)({componentLog.LoggingLevel.ToString()} | LOG_PREFIX_FUNC | LOG_PREFIX_TIME));\n");
                    }
                    var componentLogs = sb.ToString();

                    sb.Clear();
                    foreach (var node in Nodes)
                    {
                        sb.Append($"    nodeptr = CreateObject<Node>();\n");
                        sb.Append($"    net.N{node.Name} = nodeptr;\n");
                        // TODO: Handle non terrestrial networks.
                        sb.Append($"    nodes->Add(nodeptr);\n");
                        sb.Append($"    Names::Add(\"N{node.Name}\", net.N{node.Name});\n");
                    }
                    var nodes = sb.ToString();
                    
                    var links = GenerateLinks(Parameters, Links);

                    var streams = GenerateStreams(Parameters, Streams);

                    var domains = GenerateDomains(Parameters, Domains);

                    sb.Clear();
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"    T{trace.Name}_FStream.close();\n");
                    }
                    var closeTracing = sb.ToString();

                    template = template.Replace("<%%printAtts%%>", Parameters.PrintAttributes ? "true" : "false");
                    template = template.Replace("<%%asciiTrace%%>", Parameters.AsciiTrace ? "true" : "false");
                    template = template.Replace("<%%networkName%%>", Parameters.Name);
                    template = template.Replace("<%%openTracing%%>", openTracing);
                    template = template.Replace("<%%componentLog%%>", componentLogs);
                    template = template.Replace("<%%nodes%%>", nodes);
                    template = template.Replace("<%%links%%>", links);
                    template = template.Replace("<%%streams%%>", streams);
                    template = template.Replace("<%%domains%%>", domains);
                    template = template.Replace("<%%stopTime%%>", Parameters.ObservationStopTime.ToString());
                    template = template.Replace("<%%observationTime%%>", Parameters.ObservationStartTime.ToString());
                    template = template.Replace("<%%closeTracing%%>", closeTracing);

                    write.Write(template);
                }
            }
        }

        // <%%nodes%%> - The nodes the link connects.
        // <%%linkName%%> - The name of link.
        // <%%dataRate%%> - The data rate of the link.
        // <%%delay%%> - The delay of the link.
        // <%%linkMode%%> - The mode of the link (PACKETS / BYTES).
        // <%%bufferSize%%> - The size of the buffer on the link.
        // <%%queueType%%> - The type of queue used by the link.
        // <%%bufferType%%> - The type of buffer used by the link.
        // <%%devices%%> - Set up the network devices.
        // <%%startNode%%> - The node the link starts at.
        // <%%endNode%%> - The node the link ends at.
        // <%%ipAddress%%> - The base address of the link.
        // <%%ipMask%%> - The mask of the link address.
        // <%%mtu%%> - The Mtu in bytes.
        // <%%linkAttributes%%> - The attributes of the link.
        private static string GenerateLinks(SimulationParameters Parameters, IEnumerable<Link> Links)
        {
            var sbRet = new StringBuilder();
            if (Links.Count() > 0)
            {
                sbRet.Append($"    NetDeviceContainer* devices;\n");
                sbRet.Append($"    NodeContainer* linknodes;\n");
                sbRet.Append($"    Ipv4InterfaceContainer* linkinterfaces;\n");

                foreach (var link in Links)
                {
                    if (link.StartNode == null || link.EndNode == null)
                    {
                        continue;
                    }

                    var template = File.ReadAllText(@"Source Fragments/link.txt");

                    var sb = new StringBuilder();
                    sb.Append($"    linknodes->Add(net.N{link.StartNode.Name});\n");
                    sb.Append($"    linknodes->Add(net.N{link.EndNode.Name});\n");
                    var nodes = sb.ToString();

                    var queueType = "";
                    switch (link.Queue)
                    {
                        case Link.QueueType.DropTailQueue:
                            {
                                queueType = "DropTailQueue";
                                break;
                            }
                        case Link.QueueType.RandomEarlyDiscard:
                            {
                                queueType = "RedQueue";
                                break;
                            }
                    }

                    var queue = "";
                    if (link.Queue == Link.QueueType.DropTailQueue)
                    {
                        queue = $"    pointToPoint.SetQueue(\"ns3::{queueType}\", \"Mode\", {link.Name}qMode, \"{(link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes")}\", {link.Name}bufferSize);";
                    }
                    else
                    {
                        queue = $"    pointToPoint.SetQueue(\"ns3::{queueType}\", \"Mode\", {link.Name}qMode);";
                    }

                    sb.Clear();
                    sb.Append($"    net.L{link.Name} = devices->Get(0);\n");
                    if (link.Duplex)
                    {
                        sb.Append($"    net.L{link.Name}_rev = devices->Get(1);\n");
                    }
                    var devices = sb.ToString();

                    sb.Clear();
                    sb.Append($"    linkob = getInnerObject(net.L{link.Name}, \"PointToPointNetDevice\");\n");
                    sb.Append(GenerateAttribute("linkob", "PointToPointNetDevice", link.Name, "Mtu_in_bytes", link.Mtu.ToString()));
                    if (link.Queue == Link.QueueType.DropTailQueue)
                    {
                        sb.Append($"    linkob = getInnerObject(net.L{link.Name}, \"{queueType}\");\n");
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString()));
                    }
                    else if (link.Queue == Link.QueueType.RandomEarlyDiscard)
                    {
                        sb.Append($"    linkob = getInnerObject(net.L{link.Name}, \"{queueType}\");\n");
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "MeanPktSize", link.MeanPacketSize.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "IdlePktSize", link.IdlePacketSize.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "Gentle", link.Gentle ? "true" : "false"));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "Wait", link.Wait ? "true" : "false"));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "QW", link.QW.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "LInterm", link.Linterm.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "LinkBandwidth", link.LinkBandwidth.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "LinkDelay", link.LinkDelay.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "Mode", link.Mode == Link.LinkMode.Packets ? "PACKETS" : "BYTES"));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "MinTh", link.MinTh.ToString()));
                        sb.Append(GenerateAttribute("linkob", queueType, link.Name, "MaxTh", link.MaxTh.ToString()));
                    }

                    // Only update the reverse link if it's duplex.
                    if (link.Duplex)
                    {
                        sb.Append($"    linkob_rev = getInnerObject(net.L{link.Name}_rev, \"PointToPointNetDevice\");\n");
                        sb.Append(GenerateAttribute("linkob_rev", "PointToPointNetDevice", $"{link.Name}_rev", "Mtu_in_bytes", link.Mtu.ToString()));
                        if (link.Queue == Link.QueueType.DropTailQueue)
                        {
                            sb.Append($"    linkob = getInnerObject(net.L{link.Name}, \"{queueType}\");\n");
                            sb.Append(GenerateAttribute("linkob", queueType, link.Name, link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString()));
                        }
                        else if (link.Queue == Link.QueueType.RandomEarlyDiscard)
                        {
                            sb.Append($"    linkob_rev = getInnerObject(net.L{link.Name}_rev, \"{queueType}\");\n");
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "MeanPktSize", link.MeanPacketSize.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "IdlePktSize", link.IdlePacketSize.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "Gentle", link.Gentle ? "true" : "false"));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "Wait", link.Wait ? "true" : "false"));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "QW", link.QW.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "LInterm", link.Linterm.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "LinkBandwidth", link.LinkBandwidth.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "LinkDelay", link.LinkDelay.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "Mode", link.Mode == Link.LinkMode.Packets ? "PACKETS" : "BYTES"));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "MinTh", link.MinTh.ToString()));
                            sb.Append(GenerateAttribute("linkob_rev", queueType, link.Name, "MaxTh", link.MaxTh.ToString()));
                        }
                    }
                    var linkAttributes = sb.ToString();

                    template = template.Replace("<%%nodes%%>", nodes);
                    template = template.Replace("<%%queue%%>", queue);
                    template = template.Replace("<%%linkName%%>", link.Name);
                    template = template.Replace("<%%dataRate%%>", link.DataRate);
                    template = template.Replace("<%%delay%%>", link.Delay);
                    template = template.Replace("<%%linkMode%%>", link.Mode == Link.LinkMode.Packets ? "PACKETS" : "BYTES");
                    template = template.Replace("<%%bufferSize%%>", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString());

                    template = template.Replace("<%%meanPacketSize%%>", link.MeanPacketSize.ToString());
                    template = template.Replace("<%%idlePacketSize%%>", link.IdlePacketSize.ToString());
                    template = template.Replace("<%%gentle%%>", link.Gentle ? "true" : "false");
                    template = template.Replace("<%%wait%%>", link.Wait ? "true" : "false");
                    template = template.Replace("<%%QW%%>", link.QW.ToString());
                    template = template.Replace("<%%lInterm%%>", link.Linterm.ToString());
                    template = template.Replace("<%%linkBandwidth%%>", link.LinkBandwidth);
                    template = template.Replace("<%%linkDelay%%>", link.LinkDelay.ToString());
                    template = template.Replace("<%%minTh%%>", link.MinTh.ToString());
                    template = template.Replace("<%%maxTh%%>", link.MaxTh.ToString());

                    template = template.Replace("<%%queueType%%>", queueType);
                    template = template.Replace("<%%bufferType%%>", link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes");
                    template = template.Replace("<%%devices%%>", devices);
                    template = template.Replace("<%%startNode%%>", $"{link.StartNode.Name}");
                    template = template.Replace("<%%endNode%%>", $"{link.EndNode.Name}");
                    template = template.Replace("<%%ipAddress%%>", link.BaseAddress);
                    template = template.Replace("<%%ipMask%%>", link.Mask);
                    template = template.Replace("<%%mtu%%>", link.Mtu.ToString());
                    template = template.Replace("<%%linkAttributes%%>", linkAttributes);

                    sbRet.Append(template);
                }
            }

            return sbRet.ToString();
        }

        private static string GenerateAttribute(string Object, string Type, string Name, string Attribute, string Value)
        {
            var sb = new StringBuilder();

            sb.Append($"    if (!{Object})\n");
            sb.Append($"    {{\n        NS_LOG_ERROR(\"No luck setting the attribute {Attribute} of {Name} because no {Type} object was not found.\");\n    }}\n");
            sb.Append($"    else if (!{Object}->SetAttributeFailSafe(\"{Attribute}\", {Name}_{Attribute}))\n");
            sb.Append($"    {{\n        NS_LOG_ERROR(\"No luck setting the attribute {Attribute} of {Name} because SetAttributeFailSafe failed.\");\n    }}\n");
            sb.Append($"    else\n");
            sb.Append($"    {{\n        NS_LOG_INFO(\"The attribute {Attribute} of {Name} has been set to {Value}\");\n    }}\n");

            return sb.ToString();
        }

        // <%%serverHelpers%%> - The helper applications.
        // <%%streams%%> - The stream configuration.
        private static string GenerateStreams(SimulationParameters Parameters, IEnumerable<Stream> Streams)
        {
            if (Streams.Count() == 0)
            {
                return "";
            }

            var template = File.ReadAllText(@"Source Fragments/streams.txt");

            var ftpEnabled = false;
            var udpEnabled = false;
            var onOffEnabled = false;
            foreach (var stream in Streams)
            {
                if (stream.Type == Stream.StreamType.BulkFTP)
                {
                    ftpEnabled = true;
                }
                if (stream.Type == Stream.StreamType.UDPPing)
                {
                    udpEnabled = true;
                }
                if (stream.Type == Stream.StreamType.OnOff)
                {
                    onOffEnabled = true;
                }
            }

            var sb = new StringBuilder();
            if (ftpEnabled)
            {
                sb.Append($"    BulkSendHelper* ftpClient;\n");
                sb.Append($"    uint16_t ftpport = 21;  // well-known ftp port number\n");
            }
            if (ftpEnabled || onOffEnabled)
            {
                sb.Append($"    PacketSinkHelper* packetSinkHelper;\n");
            }
            if (udpEnabled)
            {
                sb.Append($"    UdpEchoServerHelper echoServer(9);\n");
                sb.Append($"    UdpEchoClientHelper* echoClient;\n");
            }
            if (onOffEnabled)
            {
                sb.Append($"    OnOffHelper* onOffHelper;\n");
                sb.Append($"    AddressValue *remoteAddress;\n");
                sb.Append($"    Address *localAddress;\n");
            }
            var serverHelpers = sb.ToString();
            sb.Clear();

            // <%%startNode%%> - The source node for this stream.
            // <%%server%%> - The server portion of this stream.
            // <%%client%%> - The client portion of this stream.
            // <%%endNode%%> - The destination node for this stream.
            // <%%startTime%%> - The start time for this stream.
            // <%%endTime%%> - The end time for this stream.
            // <%%delayedStartTime%%> - The start time plus 0.001 seconds for some reason.
            // <%%streamName%%> - The name of this stream.
            foreach (var stream in Streams)
            {
                var streamTemplate = File.ReadAllText(@"Source Fragments/stream.txt");

                var streamSB = new StringBuilder();
                if (stream.Type == Stream.StreamType.BulkFTP)
                {
                    streamSB.Append($"    packetSinkHelper = new PacketSinkHelper(\"ns3::TcpSocketFactory\", InetSocketAddress(destnadd, ftpport));\n");
                    streamSB.Append($"    serverApps->Add(packetSinkHelper->Install(net.N{stream.EndNode.Name}->GetObject<Node>()));\n");
                }
                else if (stream.Type == Stream.StreamType.UDPPing)
                {
                    streamSB.Append($"    serverApps->Add(echoServer.Install(net.N{stream.EndNode.Name}));\n");
                }
                else if (stream.Type == Stream.StreamType.OnOff)
                {
                    streamSB.Append($"    localAddress = new Address(InetSocketAddress(Ipv4Address::GetAny(), 50000));\n");
                    streamSB.Append($"    packetSinkHelper = new PacketSinkHelper(\"ns3::TcpSocketFactory\", *localAddress);\n");
                    streamSB.Append($"    serverApps->Add(packetSinkHelper->Install(net.N{stream.EndNode.Name}));\n");
                }
                var server = streamSB.ToString();

                streamSB.Clear();
                if (stream.Type == Stream.StreamType.BulkFTP)
                {
                    streamSB.Append($"    ftpClient = new BulkSendHelper(\"ns3::TcpSocketFactory\", InetSocketAddress(destnadd, ftpport));\n");
                    streamSB.Append($"    ftpClient->SetAttribute(\"MaxBytes\", UintegerValue({stream.MaxBytes}));\n");
                    streamSB.Append($"    clientApps->Add(ftpClient->Install(net.N{stream.StartNode.Name}->GetObject<Node>()));\n");
                }
                else if (stream.Type == Stream.StreamType.UDPPing)
                {
                    streamSB.Append($"    echoClient = new UdpEchoClientHelper(destnadd, 9);\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"MaxPackets\", UintegerValue({stream.MaxPackets}));\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"Interval\", TimeValue(Seconds({stream.Interval})));\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"PacketSize\", UintegerValue({stream.PacketSize}));\n");
                    streamSB.Append($"    clientApps->Add(echoClient->Install(net.N{stream.StartNode.Name}));\n");
                }
                else if (stream.Type == Stream.StreamType.OnOff)
                {
                    streamSB.Append($"    onOffHelper = new OnOffHelper(\"ns3::TcpSocketFactory\", Address());\n");
                    streamSB.Append($"    onOffHelper->SetAttribute(\"OnTime\", StringValue(\"ns3::{stream.OnDistribution}RandomVariable[{(stream.OnDistribution == Stream.Distribution.Constant ? "Constant" : "Mean")}={stream.OnInterval}]\"));\n");
                    streamSB.Append($"    onOffHelper->SetAttribute(\"DataRate\", StringValue(\"{stream.OnCBRRate}\"));\n");
                    streamSB.Append($"    onOffHelper->SetAttribute(\"Protocol\", StringValue(\"ns3::{stream.TransportProtocol}SocketFactory\"));\n");
                    streamSB.Append($"    onOffHelper->SetAttribute (\"OffTime\", StringValue(\"ns3::{stream.OffDistribution}RandomVariable[{(stream.OffDistribution == Stream.Distribution.Constant ? "Constant" : "Mean")}={stream.OffInterval}]\"));\n");
                    streamSB.Append($"    remoteAddress = new AddressValue(InetSocketAddress(destnadd, 50000));\n");
                    streamSB.Append($"    onOffHelper->SetAttribute (\"Remote\", *remoteAddress);\n");
                    streamSB.Append($"    clientApps->Add(onOffHelper->Install(net.N{stream.StartNode.Name}));\n");
                }
                var client = streamSB.ToString();

                streamTemplate = streamTemplate.Replace("<%%startNode%%>", stream.StartNode.Name);
                streamTemplate = streamTemplate.Replace("<%%server%%>", server);
                streamTemplate = streamTemplate.Replace("<%%client%%>", client);
                streamTemplate = streamTemplate.Replace("<%%endNode%%>", stream.EndNode.Name);
                streamTemplate = streamTemplate.Replace("<%%startTime%%>", stream.StartTime.ToString());
                streamTemplate = streamTemplate.Replace("<%%endTime%%>", stream.EndTime.ToString());
                streamTemplate = streamTemplate.Replace("<%%delayedStartTime%%>", (stream.StartTime + 0.001).ToString());
                streamTemplate = streamTemplate.Replace("<%%streamName%%>", stream.Name);

                sb.Append(streamTemplate);
            }
            var streams = sb.ToString();

            template = template.Replace("<%%serverHelpers%%>", serverHelpers);
            template = template.Replace("<%%streams%%>", streams);

            return template;
        }

        // <%%domains%%> - The contents of the domain.
        // <%%dataRate%%> - Data rate of the domain.
        // <%%nodes%%> - Add nodes to the domain.
        // <%%delay%%> - Delay of the domain.
        // <%%baseAddress%%> - Base address of the domain
        // <%%nodeAddresses%%> - Set up the node addresses in the domain.
        // <%%devices%%> - Assign devices to net object.
        private static string GenerateDomains(SimulationParameters Parameters, IEnumerable<Domain> Domains)
        {
            if (Domains.Count() == 0)
            {
                // Don't return the unused variables.
                return "";
            }

            var template = File.ReadAllText(@"Source Fragments/domains.txt");

            var sb = new StringBuilder();
            foreach (var domain in Domains)
            {
                var domainTemplate = File.ReadAllText(@"Source Fragments/domain.txt");

                var domainSb = new StringBuilder();
                foreach (var node in domain.Nodes)
                {
                    domainSb.Append($"    csmaNodes->Add(net.N{node.Name});\n");
                }
                var nodes = domainSb.ToString();

                domainSb.Clear();
                foreach (var node in domain.Nodes)
                {
                    domainSb.Append($"    myaddr = net.N{node.Name}->GetObject<Ipv4MyAddress>();\n");
                    domainSb.Append($"    if (!myaddr)\n    {{\n");
                    domainSb.Append($"        myaddr = CreateObject<Ipv4MyAddress>();\n");
                    domainSb.Append($"        myaddr->setAddr(csmaInterfaces->GetAddress(ifkount));\n");
                    domainSb.Append($"        NS_LOG_INFO(\"N{node.Name} has IP address \" << myaddr->getAddr());\n");
                    domainSb.Append($"        net.N{node.Name}->AggregateObject(myaddr);\n");
                    domainSb.Append($"    }}\n    ifkount++;\n\n");
                }
                var nodeAddresses = domainSb.ToString();

                domainSb.Clear();
                foreach (var node in domain.Nodes)
                {
                    domainSb.Append($"    net.D{domain.Name}_{node.Name} = csmaDevices->Get(csma_intfcount);\n");
                    domainSb.Append($"    csma_intfcount++;\n");
                }
                var devices = domainSb.ToString();

                domainTemplate = domainTemplate.Replace("<%%nodes%%>", nodes);
                domainTemplate = domainTemplate.Replace("<%%nodeAddresses%%>", nodeAddresses);
                domainTemplate = domainTemplate.Replace("<%%devices%%>", devices);
                domainTemplate = domainTemplate.Replace("<%%dataRate%%>", domain.DataRate);
                domainTemplate = domainTemplate.Replace("<%%delay%%>", domain.Delay);
                domainTemplate = domainTemplate.Replace("<%%baseAddress%%>", domain.BaseAddress);

                sb.Append(domainTemplate);
            }
            var domains = sb.ToString();

            template = template.Replace("<%%domains%%>", domains);

            return template;
        }

        public static void GeneratePlots(SimulationParameters Parameters)
        {
            var directory = Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out");

            foreach (var plot in Parameters.Plots)
            {
                using (var w = new StreamWriter(Path.Combine(directory, $"{plot.EscapedName}.gpl")))
                {
                    w.WriteLine($"set key on outside box\n");
                    w.WriteLine($"set terminal png size {plot.Width},{plot.Height}\n");
                    w.WriteLine($"set xlabel \"{plot.XAxisLabel}\"");
                    w.WriteLine($"set ylabel \"{plot.YAxisLabel}\"");
                    w.WriteLine($"set y2label \"{plot.Y2AxisLabel}\"");
                    w.WriteLine($"set autoscale y2");
                    w.WriteLine($"set y2tics");
                    w.WriteLine($"unset xrange");
                    w.WriteLine($"set output \"{plot.OutputName}\"");
                    w.WriteLine($"set key {plot.LegendString}");
                    w.WriteLine($"plot {plot.PlotString(Parameters)}");
                }
            }
        }
    }
}
