namespace ppedv.CarRentalXPress.Model
{
    public class Rent : Entity
    {
        public DateTime OrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
        public string EndLocation { get; set; } = string.Empty;

        public required virtual Car Car { get; set; }
        public required virtual Customer Customer { get; set; }
    }

}
