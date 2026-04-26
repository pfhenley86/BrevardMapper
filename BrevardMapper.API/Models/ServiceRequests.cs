namespace BrevardMapper.API.Models
{
    public class ServiceRequests
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // GIS fields
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Status { get; set; }
    }
}
