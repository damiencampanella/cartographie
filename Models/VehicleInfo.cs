using Flowly.Api.Positions;
using BlazorLeaflet.Models;

namespace BlazorFlowly.Models {
    public class VehicleInfo {
        public ApiPosition Position { get; set; }
        public bool Filtered { get; set; }
        public bool Hidden { get; set; }
        public bool Assigned { get; set; }
        public Layer Layer { get; set; }


        public VehicleInfo (ApiPosition position, bool filtered, bool hidden, bool assigned, Layer layer) {
            Position = position;
            Filtered = filtered;
            Hidden = hidden;
            Assigned = assigned;
            Layer = layer;
        }
    }
}