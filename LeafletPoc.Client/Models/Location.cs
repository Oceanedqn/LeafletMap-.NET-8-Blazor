namespace LeafletPoc.Client.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public LocationType Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public enum LocationType
    {
        Restaurant = 0,
        Poissonerie = 1,
        Bar = 2
    }
}
