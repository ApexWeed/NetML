using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MiscUtil.IO;

namespace NetML
{
    public static class SourceGenerator
    {
        public static void GenerateSource(string OutputPath, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams)
        {
            using (var fs = File.Open(OutputPath, FileMode.Create))
            {
                GenerateHeader(fs, Parameters);
                GenerateNet(fs, Parameters, Nodes, Links, Streams);
                GenerateTraces(fs, Parameters);
                GenerateStreamParameters(fs, Parameters, Streams);
                GenerateMain(fs, Parameters, Nodes, Links, Streams);
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
        // <%%nodeAtts%%> - Attribute display of the network nodes.
        // <%%linkAtts%%> - Attribute display of the network links.
        // <%%streamAtts%%> - Attribute display of the network streams.
        private static void GenerateNet(System.IO.Stream OutputStream, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams)
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

                    template = template.Replace("<%%nodes%%>", nodes);
                    template = template.Replace("<%%links%%>", links);
                    template = template.Replace("<%%streams%%>", streams);
                    template = template.Replace("<%%nodeAtts%%>", nodeAtts);
                    template = template.Replace("<%%linkAtts%%>", linkAtts);
                    template = template.Replace("<%%streamAtts%%>", streamAtts);

                    write.Write(template);
                }
            }
        }

        // <%%variables%%> - The variables to hold the trace data.
        // <%%functions%%> - The functions for incrementing the trace data.
        // <%%regTrace%%> - Register trace functions.
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
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"// Process trace {trace.Name}\n");
                        foreach (var traceAttribute in trace.Attributes)
                        {
                            sb.Append($"void T{trace.Name}_{traceAttribute.TraceSource}Tracer({traceAttribute.AttributeType})\n");
                            sb.Append($"{{\n");
                            sb.Append($"    T{trace.Name}_FStream << Simulator::Now().GetSeconds() << \":\" << {traceAttribute.Code} << std::endl;\n");
                            sb.Append($"}}\n\n");
                        }
                    }
                    var functions = sb.ToString();

                    sb.Clear();
                    foreach (var trace in Parameters.Traces)
                    {
                        foreach (var traceAttribute in trace.Attributes)
                        {
                            sb.Append($"    // Set up tracing for T{trace.Name} of type {traceAttribute.AttributeType}\n");
                            sb.Append($"    if (TraceConnectNoContext(net.{(traceAttribute.Element is Link ? ($"L{(traceAttribute.Element as Link).Name}") : ($"N{(traceAttribute.Element as Node).Name}"))}, \"{traceAttribute.TraceSource}\", MakeCallback(&T{trace.Name}_{traceAttribute.TraceSource}Tracer)))\n");
                            sb.Append($"    {{\n");
                            sb.Append($"        NS_LOG_INFO(\"Tracing for {traceAttribute.TraceSource} at node {(traceAttribute.Element is Link ? (traceAttribute.Element as Link).Name : (traceAttribute.Element as Node).Name)} was successfully set up\");\n");
                            sb.Append($"    }}\n");
                            sb.Append($"    else\n");
                            sb.Append($"    {{\n");
                            sb.Append($"        NS_LOG_ERROR(\"Tracing for {traceAttribute.TraceSource} at node {(traceAttribute.Element is Link ? (traceAttribute.Element as Link).Name : (traceAttribute.Element as Node).Name)} was not successfully set up\");\n");
                            sb.Append($"    }}\n\n");
                        }
                    }
                    var regTrace = sb.ToString();

                    template = template.Replace("<%%variables%%>", variables);
                    template = template.Replace("<%%functions%%>", functions);
                    template = template.Replace("<%%regTrace%%>", regTrace);

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

                            //sb.Append($"    UintegerValue {stream.Name}_SndBufSize_in_bytes = {stream.StartSendBufferSize}\n");
                            sb.Append($"    streamob = getInnerObject(net.N{stream.StartNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "SndBufSize_in_bytes", stream.StartSendBufferSize.ToString()));
                            sb.Append($"    streamob = getInnerObject(net.N{stream.StartNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "MaxWindowSize_in_bytes", stream.StartMaxWindowSize.ToString()));

                            sb.Append($"    streamob = getInnerObject(net.N{stream.EndNode.Name}, \"TcpNewReno\");\n");
                            sb.Append(GenerateAttribute("streamob", "TcpNewReno", stream.Name, "SndBufSize_in_bytes", stream.StartSendBufferSize.ToString()));
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
        // <%%networkName%%> - The name of the network.
        // <%%openTracing%%> - Initial set up for tracing filestreams and buffers.
        // <%%componentLog%%> - Enabling logging for components.
        // <%%nodes%%> - Set up of the nodes.
        // <%%links%%> - Set up of the links.
        // <%%streams%%> - Set up of the streams.
        // <%%stopTime%%> - Time in seconds after starting to end the simulation.
        // <%%observationTime%%> - Time after the simulation starts to begin observation.
        // <%%closeTracing%%> - Close the trace streams.
        private static void GenerateMain(System.IO.Stream OutputStream, SimulationParameters Parameters, IEnumerable<Node> Nodes, IEnumerable<Link> Links, IEnumerable<Stream> Streams)
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
                        sb.Append($"    T{trace.Name}_FStream << std::endl; // Endlines for days!\n");
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

                    // TODO: Tracing.
                    sb.Clear();
                    foreach (var trace in Parameters.Traces)
                    {
                        sb.Append($"    T{trace.Name}_FStream.close();\n");
                    }
                    var closeTracing = sb.ToString();

                    template = template.Replace("<%%printAtts%%>", Parameters.PrintAttributes ? "true" : "false");
                    template = template.Replace("<%%networkName%%>", Parameters.Name);
                    template = template.Replace("<%%openTracing%%>", openTracing);
                    template = template.Replace("<%%componentLog%%>", componentLogs);
                    template = template.Replace("<%%nodes%%>", nodes);
                    template = template.Replace("<%%links%%>", links);
                    template = template.Replace("<%%streams%%>", streams);
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
        // <%%ipAddress%%> - The base address of the link.
        // <%%ipMask%%> - The mask of the link address.
        // <%%mtu%%> - The Mtu in bytes.
        // <%%linkAttributes%%> - The attributes of the link.
        private static string GenerateLinks(SimulationParameters Parameters, IEnumerable<Link> Links)
        {
            var sbRet = new StringBuilder();

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
                sb.Append($"    linkob = getInnerObject(net.L{link.Name}, \"{link.Queue.ToString()}\");\n");
                sb.Append(GenerateAttribute("linkob", link.Queue.ToString(), link.Name, link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString()));

                // Only update the reverse link if it's duplex.
                if (link.Duplex)
                {
                    sb.Append($"    linkob_rev = getInnerObject(net.L{link.Name}_rev, \"PointToPointNetDevice\");\n");
                    sb.Append(GenerateAttribute("linkob_rev", "PointToPointNetDevice", $"{link.Name}_rev", "Mtu_in_bytes", link.Mtu.ToString()));
                    sb.Append($"    linkob_rev = getInnerObject(net.L{link.Name}_rev, \"{link.Queue.ToString()}\");\n");
                    sb.Append(GenerateAttribute("linkob_rev", link.Queue.ToString(), $"{link.Name}_rev", link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString()));
                }
                var linkAttributes = sb.ToString();

                template = template.Replace("<%%nodes%%>", nodes);
                template = template.Replace("<%%linkName%%>", link.Name);
                template = template.Replace("<%%dataRate%%>", link.DataRate);
                template = template.Replace("<%%delay%%>", link.Delay);
                template = template.Replace("<%%linkMode%%>", link.Mode == Link.LinkMode.Packets ? "PACKETS" : "BYTES");
                template = template.Replace("<%%bufferSize%%>", link.Mode == Link.LinkMode.Packets ? link.MaxPackets.ToString() : link.MaxBytes.ToString());
                template = template.Replace("<%%queueType%%>", link.Queue.ToString());
                template = template.Replace("<%%bufferType%%>", link.Mode == Link.LinkMode.Packets ? "MaxPackets" : "MaxBytes");
                template = template.Replace("<%%devices%%>", devices);
                template = template.Replace("<%%ipAddress%%>", link.BaseAddress);
                template = template.Replace("<%%ipMask%%>", link.Mask);
                template = template.Replace("<%%mtu%%>", link.Mtu.ToString());
                template = template.Replace("<%%linkAttributes%%>", linkAttributes);

                sbRet.Append(template);
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
            }

            var sb = new StringBuilder();
            if (ftpEnabled)
            {
                sb.Append($"    BulkSendHelper* ftpClient;\n");
                sb.Append($"    uint16_t ftpport = 21;  // well-known ftp port number\n");
                sb.Append($"    PacketSinkHelper* packetSinkHelper;\n");
            }
            if (udpEnabled)
            {
                sb.Append($"    UdpEchoServerHelper echoServer(9);\n");
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
                else
                {
                    streamSB.Append($"    serverApps->Add(echoServer.Install(net.N{stream.EndNode.Name}));\n");
                }
                var server = streamSB.ToString();

                streamSB.Clear();
                if (stream.Type == Stream.StreamType.BulkFTP)
                {
                    streamSB.Append($"    ftpClient = new BulkSendHelper(\"ns3::TcpSocketFactory\", InetSocketAddress(destnadd, ftpport));\n");
                    streamSB.Append($"    ftpClient->SetAttribute(\"MaxBytes\", UintegerValue({stream.MaxBytes}));\n");
                    streamSB.Append($"    clientApps->Add(ftpClient->Install(net.N{stream.StartNode.Name}->GetObject<Node>()));\n");
                }
                else
                {
                    streamSB.Append($"    echoClient = new UdpEchoClientHelper(destnadd, 9);\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"MaxPackets\", UintegerValue({stream.MaxPackets}));\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"Interval\", TimeValue(Seconds({stream.Interval})));\n");
                    streamSB.Append($"    echoClient->SetAttribute(\"PacketSize\", UintegerValue({stream.PacketSize}));\n");
                    streamSB.Append($"    clientApps->Add(echoClient->Install(net.N{stream.StartNode.Name}));\n");
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
    }
}
