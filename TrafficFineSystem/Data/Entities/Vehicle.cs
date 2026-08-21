using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Plate { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public IList<TrafficFine> TrafficFines { get; set; }
    }
}
