using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.UI.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly IRepository repo;

        public CarsController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: CarsController
        public ActionResult Index()
        {
            return View(repo.GetAll<Car>());
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById<Car>(id));
        }

        // GET: CarsController/Create
        public ActionResult Create()
        {
            return View(new Car() { Color = "Gelb", KW = 95, Manufacturer = "NEU" });
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                repo.Add(car);
                repo.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repo.GetById<Car>(id));
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                repo.Update(car);
                repo.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetById<Car>(id));
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Car car)
        {
            try
            {
                repo.Delete(car);
                repo.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
