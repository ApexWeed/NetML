using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetML
{
    public class Plot
    {
        public enum Legend
        {
            TopRightOutside,
            TopRightInside,
            TopRightAbove,
            TopLeftOutside,
            TopLeftInside,
            TopLeftAbove,
            TopCentreOutside,
            TopCentreInside,
            TopCentreAbove,
            BottomRightOutside,
            BottomRightInside,
            BottomRightAbove,
            BottomLeftOutside,
            BottomLeftInside,
            BottomLeftAbove,
            BottomCentreOutside,
            BottomCentreInside,
            BottomCentreAbove
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                escapedName = new Regex($"[{Path.GetInvalidFileNameChars()}]").Replace(Name, "");
            }
        }
        private string escapedName;
        [Newtonsoft.Json.JsonIgnore]
        public string EscapedName
        {
            get { return escapedName; }
        }
        public Legend LegendLocation;
        public string XAxisLabel;
        public string YAxisLabel;
        public string Y2AxisLabel;
        public int Width;
        public int Height;
        public bool Disabled;
        public List<PlotAttribute> Attributes;

        [Newtonsoft.Json.JsonIgnore]
        public string OutputName
        {
            get { return $"{EscapedName}.png"; }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string LegendString
        {
            get
            {
                var stringVersion = LegendLocation.ToString();
                // Silly American software and their funny spellings.
                stringVersion = stringVersion.Replace("Centre", "Center");

                // Convert first letter to lower case.
                stringVersion = char.ToLower(stringVersion[0]) + stringVersion.Substring(1);
                // Find second and third capital, add a space and convert them to lower case.
                for (int i = 0; i < 2; i++)
                {
                    var pos = stringVersion.IndexOf(stringVersion.First((x) => char.IsUpper(x)));
                    stringVersion = stringVersion.Substring(0, pos) + " " + char.ToLower(stringVersion[pos]) + stringVersion.Substring(pos + 1);
                }

                return stringVersion;
            }
        }

        public string PlotString(SimulationParameters Parameters)
        {
            var sb = new StringBuilder();

            var lineCount = Attributes.Count((x) => x.ShowLines);
            var lineIndex = 0;
            foreach (var attribute in Attributes)
            {
                // Filename.
                sb.Append($"\"{Parameters.EscapedName}_T{attribute.GetFileName()}.txt\"");
                // Data set up.
                sb.Append($" using ($1) : ($2) axes x1y{(attribute.UseY2Axis ? "2" : "1")} title \"{attribute.TraceParameter.Name}\"");
                if (attribute.ShowLines)
                {
                    sb.Append($" with lines lw {lineCount - lineIndex} lt {lineCount - lineIndex}");
                    lineIndex++;
                }
                sb.Append($", ");
            }

            // Cut off the last comma and space
            return sb.ToString().Substring(0, sb.Length - 2);
        }

        public Plot()
        {
            this.Attributes = new List<PlotAttribute>();
            this.Width = 1000;
            this.Height = 800;
            this.XAxisLabel = "x-axis";
            this.YAxisLabel = "y-axis";
            this.Y2AxisLabel = "y2-axis";
        }

        public Plot(Plot Other)
        {
            this.Set(Other);
        }

        public void Set(Plot Other)
        {
            this.Name = Other.Name;
            this.LegendLocation = Other.LegendLocation;
            this.XAxisLabel = Other.XAxisLabel;
            this.YAxisLabel = Other.YAxisLabel;
            this.Y2AxisLabel = Other.Y2AxisLabel;
            this.Width = Other.Width;
            this.Height = Other.Height;
            this.Disabled = Other.Disabled;
            this.Attributes = new List<PlotAttribute>();
            foreach (var attribute in Other.Attributes)
            {
                this.Attributes.Add(new PlotAttribute(attribute));
            }
        }
    }
}
