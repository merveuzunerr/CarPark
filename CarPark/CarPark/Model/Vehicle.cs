namespace CarPark.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Class { get; set; }
        public string Colour { get; set; }

        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public string ModelName { get; set; }

        public bool? AutoPilot { get; set; } //autopilot property defined for 1. class vehicle

        public int? Price { get; set; } //Price property defined for 1. class vehicle

        public int? LuggageCappacity { get; set; } //luggage capacity property defined for 2. class vehicle

        public bool? SpareTyre { get; set; } //spare tyre property defined for 2. class vehicle

        public bool? CarWash { get; set; }
        public bool? TireChange { get; set; }

        public int Kilowatt { get; set; } //kilowatt rating of the vehicle

        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

    }
}
