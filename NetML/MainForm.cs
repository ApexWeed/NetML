using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Apex.Extensions;

namespace NetML
{
    public partial class MainForm : Form
    {
        private enum CanvasState
        {
            None,
            Create,
            Move,
            Link,
            Stream,
            Domain,
            Select
        }

        private CanvasState State;
        private DateTime ClickTime;
        private Point ClickPosition;
        private IDrawable ClickItem;
        private IDrawable Connection;

        new private ContextMenu ContextMenu;
        private Traces Traces;
        private Plots Plots;
        private NodeEditor NodeEditor;
        private StreamEditor StreamEditor;
        private LinkEditor LinkEditor;
        private DomainEditor DomainEditor;
        private NetworkPropertiesEditor NetworkPropertiesEditor;
        private DisplayPropertiesEditor DisplayPropertiesEditor;
        private Console Console;

        public SimulationParameters NetworkParameters;

        public IEnumerable<Node> Nodes
        {
            get { return canNetwork.Items.Where((x) => x is Node).Select((x) => x as Node); }
        }
        public IEnumerable<Link> Links
        {
            get { return canNetwork.Items.Where((x) => x is Link).Select((x) => x as Link); }
        }
        public IEnumerable<Stream> Streams
        {
            get { return canNetwork.Items.Where((x) => x is Stream).Select((x) => x as Stream); }
        }
        public IEnumerable<Domain> Domains
        {
            get { return canNetwork.Items.Where((x) => x is Domain).Select((x) => x as Domain); }
        }
        public List<IDrawable> Items
        {
            get { return canNetwork.Items; }
        }

        public MainForm()
        {
            InitializeComponent();

            canNetwork.Sorter = new NetMLSorter();
            NetworkParameters = new SimulationParameters
            {
                Name = "unnamed",
                ComponentLogs = new List<ComponentLog>(),
                ObservationStartTime = 0.1f,
                ObservationStopTime = 10.0f,
                PrintAttributes = false,
                Traces = new List<Trace>()
            };
            SetTitle();

            ContextMenu = new ContextMenu();
            DisplayProperties.Reset();

            if (Properties.Settings.Default.NS3Dir == "<unset>")
            {
                var folderPicker = new FolderPicker("Select NS3 root", "Select the root for NS3, this is the folder that contains waf, using the Windows path.", $"C:\\cygwin64\\home\\{Environment.UserName}\\ns-allinone-3.22\\ns-3.22");
                if (folderPicker.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.NS3Dir = folderPicker.ChosenFolder;
                }
                else
                {
                    MessageBox.Show("You know it won't work if you don't tell the program where NS3 is.");
                }
            }

            if (Properties.Settings.Default.CygwinDir == "<unset>")
            {
                var folder = Apex.Prompt.Prompt.ShowStringDialog("Select the root for NS3, this is the folder that contains waf, using the Cygwin path.", "Select NS3 root", $"/home/{Environment.UserName}/ns-allinone-3.22/ns-3.22");
                if (folder == null)
                {
                    MessageBox.Show("You know it won't work if you don't tell the program where NS3 is.");
                }
                else
                {
                    Properties.Settings.Default.CygwinDir = folder;
                }
            }

            if (Properties.Settings.Default.BashPath == "<unset>")
            {
                var filePicker = new FilePicker("Select bash executable", "Select the bash executable, using the Windows path.", "C:\\cygwin64\\bin\\bash.exe", "Executable files (*.exe)|*.exe");
                if (filePicker.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.BashPath = filePicker.ChosenFile;
                }
                else
                {
                    MessageBox.Show("You know it won't work if you don't tell the program where bash is.");
                }
            }

            Properties.Settings.Default.Save();
        }

        public void SetTitle()
        {
            this.Text = $"NetML - {NetworkParameters.Name}";
        }

        private void canNetwork_MouseDown(object sender, MouseEventArgs e)
        {
            ClickTime = DateTime.Now;
            ClickItem = canNetwork.GetItem(e.X, e.Y);
            ClickPosition = new Point(e.X, e.Y);

            if (ClickItem == null)
            {
                State = CanvasState.Create;
            }
            else
            {
                State = CanvasState.None;
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        break;
                    }
                case MouseButtons.None:
                    {
                        break;
                    }
                case MouseButtons.Right:
                    {
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        break;
                    }
                case MouseButtons.XButton1:
                    {
                        break;
                    }
                case MouseButtons.XButton2:
                    {
                        break;
                    }
            }
        }

        private void canNetwork_MouseMove(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - ClickPosition.X) + Math.Abs(e.Y - ClickPosition.Y) > 5)
            {
                if (ClickItem != null)
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            {
                                switch (State)
                                {
                                    case CanvasState.Move:
                                    case CanvasState.None:
                                        {
                                            State = CanvasState.Move;
                                            if (ClickItem is Node)
                                            {
                                                var node = ClickItem as Node;
                                                var startRect = node.DrawableBounds();
                                                var oldPos = node.Position;
                                                node.X = e.X;
                                                node.Y = e.Y;
                                                var drawRect = node.DrawableBounds().Merge(startRect);

                                                // Update link positions.
                                                var links = canNetwork.Items.Where((x) => x is Link);
                                                foreach (Link link in links)
                                                {
                                                    if (link.StartNode == node || link.EndNode == node)
                                                    {
                                                        var other = link.StartNode == node ? link.EndNode.Position : link.StartNode.Position;
                                                        var mid = link.StartNode.Position.Mid(link.EndNode.Position);
                                                        var oldMid = oldPos.Mid(other);

                                                        // Reset to the midpoint if within tolerance.
                                                        if (link.Position.Distance(oldMid) < 10)
                                                        {
                                                            link.Position = mid;
                                                        }
                                                        // Shift link label otherwise.
                                                        else
                                                        {
                                                            var oldAngle = oldPos.Angle();
                                                            var newAngle = node.Position.Angle();
                                                            var oldMidAngle = oldMid.Angle(link.Position);
                                                            var newMidAngle = oldMidAngle + newAngle - oldAngle;
                                                            var dist = oldMid.Distance(link.Position);
                                                            var unit = new PointF((float)Math.Cos(newMidAngle), (float)Math.Sin(newMidAngle));
                                                            var offset = unit.Multiply((float)dist);
                                                            link.X = mid.X + offset.X;
                                                            link.Y = mid.Y + offset.Y;
                                                        }

                                                        drawRect = link.DrawableBounds().Merge(drawRect);
                                                    }
                                                }

                                                // Update stream positions.
                                                foreach (Stream stream in Streams)
                                                {
                                                    if (stream.StartNode == node || stream.EndNode == node)
                                                    {
                                                        var other = stream.StartNode == node ? stream.EndNode.Position : stream.StartNode.Position;
                                                        var mid = stream.StartNode.Position.Mid(stream.EndNode.Position);
                                                        var oldMid = oldPos.Mid(other);

                                                        // Reset to the midpoint if within tolerance.
                                                        if (stream.Position.Distance(oldMid) < 10)
                                                        {
                                                            stream.Position = mid;
                                                        }
                                                        // Shift link label otherwise.
                                                        else
                                                        {
                                                            var oldAngle = oldPos.Angle();
                                                            var newAngle = node.Position.Angle();
                                                            var oldMidAngle = oldMid.Angle(stream.Position);
                                                            var newMidAngle = oldMidAngle + newAngle - oldAngle;
                                                            var dist = oldMid.Distance(stream.Position);
                                                            var unit = new PointF((float)Math.Cos(newMidAngle), (float)Math.Sin(newMidAngle));
                                                            var offset = unit.Multiply((float)dist);
                                                            stream.X = mid.X + offset.X;
                                                            stream.Y = mid.Y + offset.Y;
                                                        }

                                                        drawRect = stream.DrawableBounds().Merge(drawRect);
                                                    }
                                                }

                                                // Redraw changed region.
                                                canNetwork.Invalidate(startRect.Merge(drawRect));
                                            }
                                            else if (ClickItem is Link)
                                            {
                                                var link = ClickItem as Link;
                                                var startRect = link.DrawableBounds();
                                                link.X = e.X;
                                                link.Y = e.Y;
                                                var endRect = link.DrawableBounds();
                                                // Redraw changed region.
                                                canNetwork.Invalidate(startRect.Merge(endRect));
                                            }
                                            else if (ClickItem is Stream)
                                            {
                                                var stream = ClickItem as Stream;
                                                var startRect = stream.DrawableBounds();
                                                stream.X = e.X;
                                                stream.Y = e.Y;
                                                var endRect = stream.DrawableBounds();
                                                // Redraw changed region.
                                                canNetwork.Invalidate(startRect.Merge(endRect));
                                            }
                                            else if (ClickItem is Domain)
                                            {
                                                var domain = ClickItem as Domain;
                                                var startRect = domain.DrawableBounds();
                                                domain.X = e.X;
                                                domain.Y = e.Y;
                                                var endRect = domain.DrawableBounds();
                                                // Redraw changed region.
                                                canNetwork.Invalidate(startRect.Merge(endRect));
                                            }
                                            break;
                                        }
                                    case CanvasState.Create:
                                        {
                                            break;
                                        }
                                    case CanvasState.Link:
                                        {
                                            break;
                                        }
                                    case CanvasState.Select:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case MouseButtons.None:
                            {
                                break;
                            }
                        case MouseButtons.Right:
                            {
                                switch (State)
                                {
                                    case CanvasState.Move:
                                    case CanvasState.None:
                                        {
                                            if (ClickItem is Node)
                                            {
                                                State = CanvasState.Link;
                                                var node = ClickItem as Node;
                                                var link = new Link
                                                {
                                                    X = e.X,
                                                    Y = e.Y,
                                                    StartNode = node
                                                };

                                                var links = Links;
                                                var index = 0;
                                                var name = $"link_{index}";

                                                while (links.Count((x) => x.Name == name) > 0)
                                                {
                                                    name = $"link_{++index}";
                                                }

                                                link.Name = name;
                                                link.BaseAddress = $"10.0.{index + 1}.0";
                                                Connection = link;
                                                canNetwork.AddItem(link);
                                            }
                                            break;
                                        }
                                    case CanvasState.Create:
                                        {

                                            break;
                                        }
                                    case CanvasState.Link:
                                        {
                                            var link = Connection as Link;
                                            var startRect = link.DrawableBounds();
                                            link.X = e.X;
                                            link.Y = e.Y;
                                            var endRect = link.DrawableBounds();
                                            // Redraw changed region.
                                            canNetwork.Invalidate(startRect.Merge(endRect));
                                            break;
                                        }
                                    case CanvasState.Domain:
                                        {
                                            break;
                                        }
                                    case CanvasState.Select:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case MouseButtons.Middle:
                            {
                                switch (State)
                                {
                                    case CanvasState.Move:
                                    case CanvasState.None:
                                        {
                                            if (ClickItem is Node)
                                            {
                                                State = CanvasState.Stream;
                                                var node = ClickItem as Node;
                                                var stream = new Stream
                                                {
                                                    X = e.X,
                                                    Y = e.Y,
                                                    StartNode = node
                                                };

                                                var streams = Streams;
                                                var index = 0;
                                                var name = $"stream_{index}";

                                                while (streams.Count((x) => x.Name == name) > 0)
                                                {
                                                    name = $"stream_{++index}";
                                                }

                                                stream.Name = name;
                                                Connection = stream;
                                                canNetwork.AddItem(stream);
                                            }
                                            break;
                                        }
                                    case CanvasState.Stream:
                                        {
                                            var stream = Connection as Stream;
                                            var startRect = stream.DrawableBounds();
                                            stream.X = e.X;
                                            stream.Y = e.Y;
                                            var endRect = stream.DrawableBounds();
                                            // Redraw changed region.
                                            canNetwork.Invalidate(startRect.Merge(endRect));
                                            break;
                                        }
                                }
                                break;
                            }
                        case MouseButtons.XButton1:
                            {
                                break;
                            }
                        case MouseButtons.XButton2:
                            {
                                break;
                            }
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        switch (State)
                        {
                            case CanvasState.Create:
                                {
                                    State = CanvasState.Domain;
                                    var domain = new Domain
                                    {
                                        X = e.X,
                                        Y = e.Y,
                                        Radius = (float)e.Location.Distance(ClickPosition)
                                    };
                                    Connection = domain;

                                    var domains = Domains;
                                    var index = 0;
                                    var name = $"domain_{index}";

                                    while (domains.Count((x) => x.Name == name) > 0)
                                    {
                                        name = $"domain_{++index}";
                                    }

                                    domain.Name = name;
                                    canNetwork.AddItem(domain);
                                    break;
                                }
                            case CanvasState.Domain:
                                {
                                    var domain = Connection as Domain;
                                    var startRect = domain.DrawableBounds();
                                    domain.Radius = (float)e.Location.Distance(ClickPosition);
                                    var endRect = domain.DrawableBounds();
                                    // Redraw changed region.
                                    canNetwork.Invalidate(startRect.Merge(endRect));
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void canNetwork_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        switch (State)
                        {
                            case CanvasState.None:
                                {
                                    break;
                                }
                            case CanvasState.Create:
                                {
                                    var node = new Node
                                    {
                                        X = e.X,
                                        Y = e.Y
                                    };
                                    var nodes = Nodes;
                                    var index = 0;
                                    var name = $"node_{index}";
                                    
                                    while (nodes.Count((x) => x.Name == name) > 0)
                                    {
                                        name = $"node_{++index}";
                                    }

                                    node.Name = name;
                                    canNetwork.AddItem(node);
                                    break;
                                }
                            case CanvasState.Move:
                                {
                                    State = CanvasState.None;
                                    ClickItem = null;
                                    break;
                                }
                            case CanvasState.Link:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case MouseButtons.None:
                    {
                        break;
                    }
                case MouseButtons.Right:
                    {
                        switch (State)
                        {
                            case CanvasState.None:
                                {
                                    if (Connection == null && ClickItem != null)
                                    {
                                        var itemType = ClickItem is Node ? nameof(Node) : ClickItem is Link ? nameof(Link) : ClickItem is Stream ? nameof(Stream) : nameof(Domain);
                                        ContextMenu.MenuItems.Clear();
                                        ContextMenu.MenuItems.Add($"Edit {itemType}", EditElement);
                                        ContextMenu.MenuItems.Add($"Delete {itemType}", DeleteElement);
                                        ContextMenu.MenuItems.Add("Close Menu");
                                        ContextMenu.Show(this, new Point(e.X, e.Y));
                                    }
                                    break;
                                }
                            case CanvasState.Create:
                                {
                                    var domain = new Domain
                                    {
                                        X = e.X,
                                        Y = e.Y,
                                    };
                                    var domains = Domains;
                                    var index = 0;
                                    var name = $"domain_{index}";

                                    while (domains.Count((x) => x.Name == name) > 0)
                                    {
                                        name = $"domain_{++index}";
                                    }

                                    domain.Name = name;
                                    canNetwork.AddItem(domain);
                                    break;
                                }
                            case CanvasState.Move:
                                {
                                    State = CanvasState.None;
                                    ClickItem = null;
                                    break;
                                }
                            case CanvasState.Domain:
                                {
                                    var domain = Connection as Domain;
                                    // TODO: add nodes to domain.
                                    var nodes = Nodes.Where((x) => x.Position.Distance(domain.Position) < domain.Radius);
                                    domain.Nodes.AddRange(nodes);
                                    Connection = null;
                                    ClickItem = null;
                                    canNetwork.Invalidate();
                                    break;
                                }
                            case CanvasState.Link:
                                {
                                    var item = canNetwork.GetItem(e.X, e.Y, (x) => x is Node);
                                    if (item != null && ClickItem != null && ClickItem is Node)
                                    {
                                        var start = ClickItem as Node;
                                        var end = item as Node;
                                        var link = Connection as Link;
                                        link.EndNode = end;
                                        link.X = (start.X + end.X) / 2;
                                        link.Y = (start.Y + end.Y) / 2;
                                        // Update drawn area.
                                        canNetwork.UpdateItem(link);
                                        Connection = null;
                                        ClickItem = null;
                                    }
                                    else
                                    {
                                        canNetwork.Items.Remove(Connection);
                                        canNetwork.Invalidate();
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        switch (State)
                        {
                            case CanvasState.None:
                                {
                                    break;
                                }
                            case CanvasState.Create:
                                {
                                    break;
                                }
                            case CanvasState.Move:
                                {
                                    State = CanvasState.None;
                                    ClickItem = null;
                                    break;
                                }
                            case CanvasState.Stream:
                                {
                                    var item = canNetwork.GetItem(e.X, e.Y, (x) => x is Node);
                                    if (item != null && ClickItem != null && ClickItem is Node)
                                    {
                                        var start = ClickItem as Node;
                                        var end = item as Node;
                                        var stream = Connection as Stream;
                                        stream.EndNode = end;
                                        stream.X = (start.X + end.X) / 2;
                                        stream.Y = (start.Y + end.Y) / 2;
                                        // Update drawn area.
                                        canNetwork.UpdateItem(stream);
                                        Connection = null;
                                        ClickItem = null;
                                    }
                                    else
                                    {
                                        canNetwork.Items.Remove(Connection);
                                        canNetwork.Invalidate();
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case MouseButtons.XButton1:
                    {
                        break;
                    }
                case MouseButtons.XButton2:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void EditElement(object sender, EventArgs e)
        {
            if (ClickItem is Node)
            {
                if (NodeEditor == null)
                {
                    NodeEditor = new NodeEditor(this, ClickItem as Node);
                    NodeEditor.Show();
                }
                else
                {
                    NodeEditor.LoadNode(ClickItem as Node);
                }
            }
            else if(ClickItem is Link)
            {
                if (LinkEditor == null)
                {
                    LinkEditor = new LinkEditor(this, ClickItem as Link);
                    LinkEditor.Show();
                }
                else
                {
                    LinkEditor.LoadLink(ClickItem as Link);
                }
            }
            else if (ClickItem is Stream)
            {
                if (StreamEditor == null)
                {
                    StreamEditor = new StreamEditor(this, ClickItem as Stream);
                    StreamEditor.Show();
                }
                else
                {
                    StreamEditor.LoadStream(ClickItem as Stream);
                }
            }
            else if (ClickItem is Domain)
            {
                if (DomainEditor == null)
                {
                    DomainEditor = new DomainEditor(this, ClickItem as Domain);
                    DomainEditor.Show();
                }
                else
                {
                    DomainEditor.LoadDomain(ClickItem as Domain);
                }
            }
        }

        private void DeleteElement(object sender, EventArgs e)
        {
            DeleteElement(ClickItem);
            ClickItem = null;
        }

        public void DeleteElement(IDrawable Element)
        {
            // Need to remove connected links and streams too if the item is a node.
            if (Element is Node)
            {
                var links = Links.ToArray();
                var streams = Streams.ToArray();
                foreach (var link in links)
                {
                    if (link.StartNode == Element || link.EndNode == Element)
                    {
                        canNetwork.Items.Remove(link);
                    }
                }
                foreach (var stream in streams)
                {
                    if (stream.StartNode == Element || stream.EndNode == Element)
                    {
                        canNetwork.Items.Remove(stream);
                    }
                }
            }
            canNetwork.Items.Remove(Element);
            canNetwork.Invalidate();
        }

        private void runSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceGenerator.GenerateSource(Path.Combine(Properties.Settings.Default.NS3Dir, "scratch", $"{NetworkParameters.EscapedName}.cc"), NetworkParameters, Nodes, Links, Streams, Domains);
            if (Console == null)
            {
                Console = new Console(this, NetworkParameters);
                Console.Show();
            }
            else
            {
                Console.LoadParameters(NetworkParameters);
                Console.BringToFront();
            }
        }

        private void tracesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Traces == null)
            {
                Traces = new Traces(this);
                Traces.Show();
            }
            else
            {
                Traces.BringToFront();
            }
        }

        private void plotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Plots == null)
            {
                Plots = new Plots(this);
                Plots.Show();
            }
            else
            {
                Plots.BringToFront();
            }
        }

        public void ChildClosing(Form Child)
        {
            if (Child is Traces)
            {
                Traces = null;
            }
            else if (Child is Plots)
            {
                Plots = null;
            }
            else if (Child is NodeEditor)
            {
                NodeEditor = null;
            }
            else if (Child is StreamEditor)
            {
                StreamEditor = null;
            }
            else if (Child is LinkEditor)
            {
                LinkEditor = null;
            }
            else if (Child is DomainEditor)
            {
                DomainEditor = null;
            }
            else if (Child is NetworkPropertiesEditor)
            {
                NetworkPropertiesEditor = null;
            }
            else if (Child is DisplayPropertiesEditor)
            {
                DisplayPropertiesEditor = null;
            }
            else if (Child is Console)
            {
                Console = null;
            }
        }

        public void RefreshCanvas()
        {
            canNetwork.Invalidate();
        }

        private void Save()
        {
            using (var fs = File.Create($"{NetworkParameters.EscapedName}.network"))
            {
                using (var w = new StreamWriter(fs))
                {
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(NetworkParameters, Newtonsoft.Json.Formatting.Indented));
                }
            }

            if (!Directory.Exists(NetworkParameters.EscapedName))
            {
                Directory.CreateDirectory(NetworkParameters.EscapedName);
            }

            using (var fs = File.Create(Path.Combine(NetworkParameters.EscapedName, "nodes.json")))
            {
                using (var w = new StreamWriter(fs))
                {
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Nodes, Newtonsoft.Json.Formatting.Indented));
                }
            }

            using (var fs = File.Create(Path.Combine(NetworkParameters.EscapedName, "links.json")))
            {
                using (var w = new StreamWriter(fs))
                {
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Links, Newtonsoft.Json.Formatting.Indented));
                }
            }

            using (var fs = File.Create(Path.Combine(NetworkParameters.EscapedName, "streams.json")))
            {
                using (var w = new StreamWriter(fs))
                {
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Streams, Newtonsoft.Json.Formatting.Indented));
                }
            }

            using (var fs = File.Create(Path.Combine(NetworkParameters.EscapedName, "domains.json")))
            {
                using (var w = new StreamWriter(fs))
                {
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Domains, Newtonsoft.Json.Formatting.Indented));
                }
            }

            using (var fs = File.Create(Path.Combine(NetworkParameters.EscapedName, "display.json")))
            {
                using (var w = new StreamWriter(fs))
                {
                    var TypeBlob = typeof(DisplayProperties).GetFields().ToDictionary(x => x.Name, x => new Tuple<Type, object>(x.FieldType, x.GetValue(null)));
                    w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(TypeBlob, Newtonsoft.Json.Formatting.Indented));
                }
            }
        }

        private void Load(string Path)
        {
            if (File.Exists(Path))
            {
                canNetwork.Items.Clear();
                NetworkParameters = Newtonsoft.Json.JsonConvert.DeserializeObject<SimulationParameters>(File.ReadAllText(Path));

                var networkDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), System.IO.Path.GetFileNameWithoutExtension(Path));

                try
                {
                    var nodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Node>>(File.ReadAllText(System.IO.Path.Combine(networkDir, "nodes.json")));
                    canNetwork.AddItems(nodes);
                    NodeStore.Nodes = nodes;

                    try
                    {
                        var links = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Link>>(File.ReadAllText(System.IO.Path.Combine(networkDir, "links.json")));
                        canNetwork.AddItems(links);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Links are corrupt.");
                    }

                    try
                    {
                        var streams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stream>>(File.ReadAllText(System.IO.Path.Combine(networkDir, "streams.json")));
                        canNetwork.AddItems(streams);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Streams are corrupt.");
                    }

                    try
                    {
                        var domains = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Domain>>(File.ReadAllText(System.IO.Path.Combine(networkDir, "domains.json")));
                        canNetwork.AddItems(domains);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Domains are corrupt.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nodes are corrupt.");
                }

                try
                {
                    DisplayProperties.Reset();
                    var displayProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Tuple<Type, object>>>(File.ReadAllText(System.IO.Path.Combine(networkDir, "display.json")));

                    foreach (var property in displayProperties)
                    {
                        if (property.Value.Item1.IsEnum)
                        {
                            dynamic value = Enum.ToObject(property.Value.Item1, property.Value.Item2);

                            var field = typeof(DisplayProperties).GetField(property.Key);
                            field.SetValue(null, value);
                        }
                        else
                        {
                            var field = typeof(DisplayProperties).GetField(property.Key);
                            field.SetValue(null, property.Value.Item2);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Display properties are corrupt.");
                }

                // JSON can't store circular reference to parent or reference to actual Node / Link / Stream / Domain object.
                foreach (var trace in NetworkParameters.Traces)
                {
                    foreach (var attribute in trace.Attributes)
                    {
                        attribute.Parent = trace;
                        if (attribute.Element is Node)
                        {
                            attribute.Element = NodeStore.Nodes.First((x) => x.Name == (attribute.Element as Node).Name);
                        }
                        else if (attribute.Element is Link)
                        {
                            attribute.Element = Links.First((x) => x.Name == (attribute.Element as Link).Name);
                        }
                        else if (attribute.Element is Stream)
                        {
                            attribute.Element = Streams.First((x) => x.Name == (attribute.Element as Stream).Name);
                        }
                        else if (attribute.Element is Domain)
                        {
                            attribute.Element = Domains.First((x) => x.Name == (attribute.Element as Domain).Name);
                        }
                    }
                }

                // Fix up the links in plot attributes.
                foreach (var plot in NetworkParameters.Plots)
                {
                    foreach (var attribute in plot.Attributes)
                    {
                        attribute.TraceParameter = NetworkParameters.Traces.First((x) => x.Name == attribute.TraceParameter.Name);
                    }
                }

                canNetwork.Invalidate();
                NodeStore.Nodes = null;
                SetTitle();
            }
            else
            {
                MessageBox.Show($"Cannot find file \"{Path}\"");
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var name = Apex.Prompt.Prompt.ShowStringDialog("Enter new network name:", "New Network", "unnamed");

            if (name != null)
            {
                NetworkParameters.Name = name;
                NetworkParameters.ComponentLogs = new List<ComponentLog>();
                NetworkParameters.ObservationStartTime = 0.1f;
                NetworkParameters.ObservationStopTime = 10.0f;
                NetworkParameters.PrintAttributes = false;
                NetworkParameters.Traces = new List<Trace>();
                NetworkParameters.Plots = new List<Plot>();

                canNetwork.Items.Clear();
                canNetwork.Invalidate();
                SetTitle();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK)
            {
                Load(ofdOpen.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var name = Apex.Prompt.Prompt.ShowStringDialog("Enter new network name:", "Save Network As", NetworkParameters.Name);
            if (name != null)
            {
                NetworkParameters.Name = name;
                SetTitle();
                Save();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void networkParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NetworkPropertiesEditor == null)
            {
                NetworkPropertiesEditor = new NetworkPropertiesEditor(this, NetworkParameters);
                NetworkPropertiesEditor.Show();
            }
            else
            {
                NetworkPropertiesEditor.BringToFront();
            }
        }

        private void displayOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DisplayPropertiesEditor == null)
            {
                DisplayPropertiesEditor = new DisplayPropertiesEditor(this);
                DisplayPropertiesEditor.Show();
            }
            else
            {
                DisplayPropertiesEditor.BringToFront();
            }
        }
    }
}
