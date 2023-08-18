namespace ppedv.CarRentalXPress.Api.Model
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int KW { get; set; }

    }

}
