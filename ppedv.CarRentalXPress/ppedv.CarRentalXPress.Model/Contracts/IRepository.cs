namespace ppedv.CarRentalXPress.Model.Contracts
{
    public interface ICarRepository:IRepository<Car>
    {
        IEnumerable<Car> GetAllTheSpecialCars();
    }

    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        T? GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Query();

    }

    public interface IUnitOfWork
    {
        public ICarRepository CarRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Rent> RentRepository { get; }
        int SaveAll();
    }
}
