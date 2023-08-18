using Microsoft.EntityFrameworkCore;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.Data.EfCore
{
    public class CarRentalXPressContextUnitOfWorkAdapter : IUnitOfWork
    {
        readonly CarRentalXPressContext con;

        public CarRentalXPressContextUnitOfWorkAdapter(string conString)
        {
            con = new CarRentalXPressContext(conString);
        }

        public ICarRepository CarRepository => new CarRentalXPressContextCarRepositoryAdapter(con);

        public IRepository<Customer> CustomerRepository => new CarRentalXPressContextRepositoryAdapter<Customer>(con);

        public IRepository<Rent> RentRepository => new CarRentalXPressContextRepositoryAdapter<Rent>(con);

        public int SaveAll()
        {
            return con.SaveChanges();
        }
    }

    public class CarRentalXPressContextCarRepositoryAdapter : CarRentalXPressContextRepositoryAdapter<Car>, ICarRepository
    {
        public CarRentalXPressContextCarRepositoryAdapter(CarRentalXPressContext con) : base(con)
        { }

        public IEnumerable<Car> GetAllTheSpecialCars()
        {
            con.Database.ExecuteSql($"DELETE * FROM ALL");
            return null;
        }
    }

    public class CarRentalXPressContextRepositoryAdapter<T> : IRepository<T> where T : Entity
    {
        protected readonly CarRentalXPressContext con;

        public CarRentalXPressContextRepositoryAdapter(CarRentalXPressContext con)
        {
            this.con = con;
        }

        public void Add(T entity)
        {
            con.Add(entity);
        }

        public void Delete(T entity)
        {
            con.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return con.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return con.Set<T>().Find(id);
        }

        public IQueryable<T> Query()
        {
            return con.Set<T>();
        }

        public void Update(T entity)
        {
            con.Update(entity);
        }
    }
}
