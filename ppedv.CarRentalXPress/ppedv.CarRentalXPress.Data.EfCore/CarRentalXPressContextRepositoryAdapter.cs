using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.Data.EfCore
{
    public class CarRentalXPressContextRepositoryAdapter : IRepository
    {
        readonly CarRentalXPressContext con;

        public CarRentalXPressContextRepositoryAdapter(string conString)
        {
            con = new CarRentalXPressContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            con.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            con.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return con.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return con.Set<T>().Find(id);  
        }

        public int SaveAll()
        {
            return con.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            con.Update(entity);
        }
    }
}
