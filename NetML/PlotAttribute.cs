namespace NetML
{
    public class PlotAttribute
    {
        public enum Averaging
        {
            Average,
            Delta,
            Raw
        }

        public enum PlotType
        {
            Trace,
            Histogram
        }

        [Newtonsoft.Json.JsonConverter(typeof(TraceConverter))]
        public Trace TraceParameter;
        public Averaging AveragingType;
        public PlotType Type;
        public float WindowWidth;
        public bool ModuloEnabled;
        public float ModuloValue;
        public bool MinEnabled;
        public float MinValue;
        public bool MaxEnabled;
        public float MaxValue;
        public bool UseY2Axis;
        public bool ShowPoints;
        public bool ShowLines;
        public bool ScaleData;
        public string ScaleDataCommand;

        public PlotAttribute()
        {
            WindowWidth = 0.1f;
            ScaleDataCommand = "div double 131072 end";
            MinValue = 0.0f;
            MaxValue = 10.0f;
        }

        public PlotAttribute(PlotAttribute Other)
        {
            this.Set(Other);
        }

        public void Set(PlotAttribute Other)
        {
            this.TraceParameter = Other.TraceParameter;
            this.AveragingType = Other.AveragingType;
            this.Type = Other.Type;
            this.WindowWidth = Other.WindowWidth;
            this.ModuloEnabled = Other.ModuloEnabled;
            this.ModuloValue = Other.ModuloValue;
            this.MinEnabled = Other.MinEnabled;
            this.MinValue = Other.MinValue;
            this.MaxEnabled = Other.MaxEnabled;
            this.MaxValue = Other.MaxValue;
            this.UseY2Axis = Other.UseY2Axis;
            this.ShowPoints = Other.ShowPoints;
            this.ShowLines = Other.ShowLines;
            this.ScaleData = Other.ScaleData;
            this.ScaleDataCommand = Other.ScaleDataCommand;
        }

        public string GetFileName(bool IgnoreScaling = false)
        {
            if (IgnoreScaling)
            {
                return $"{TraceParameter.EscapedName}_{(AveragingType == Averaging.Raw ? "trace" : $"{(AveragingType == Averaging.Average ? "avg" : "del")}_{WindowWidth}")}";

            }
            else
            {
                return $"{TraceParameter.EscapedName}_{(AveragingType == Averaging.Raw ? "trace" : $"{(AveragingType == Averaging.Average ? "avg" : "del")}_{WindowWidth}")}{(ScaleData ? $"_{ScaleDataCommand.GetHashCode()}" : "")}";
            }
        }

        public override string ToString()
        {
            return $"{(UseY2Axis ? "Y2" : "Y")}: {TraceParameter.Name} -> {AveragingType}{(AveragingType != Averaging.Raw ? $" ({WindowWidth})" : "")}{(ModuloEnabled ? $", Modulo: {ModuloValue}" : "")}";
        }
    }
}
