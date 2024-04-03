namespace LeafletPoc.Client.Models
{
    public class Locations
    {
        public List<Location> LocationsList { get; set; } = new List<Location>();
        public LocationType LocationType { get; set; }
        public string Color { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
