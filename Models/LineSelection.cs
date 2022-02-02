namespace BlazorFlowly.Models {
    public class LineSelection {
        public string LineId { get; set; } = string.Empty;
        public int Direction { get; set; } = 0;
        public int VariationNumber { get; set; } = 0;

        public bool Display { get; set; } = true;

        public bool Develop { get; set; } = false;

        public int CategoryIndex { get; set; } = 0;
    }
}
