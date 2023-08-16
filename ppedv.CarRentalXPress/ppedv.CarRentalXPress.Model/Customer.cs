namespace ppedv.CarRentalXPress.Model
{
    public class Customer : Entity
    {
        public required string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Rent> Rents { get; set; } = new HashSet<Rent>();

    }

}
