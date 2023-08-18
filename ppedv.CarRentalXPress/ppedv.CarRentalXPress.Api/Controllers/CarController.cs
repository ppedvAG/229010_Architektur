using Microsoft.AspNetCore.Mvc;
using ppedv.CarRentalXPress.Api.Model;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;


namespace ppedv.CarRentalXPress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IRepository repo;

        CarMapper mapper = new CarMapper();

        public CarController(IRepository repo)
        {
            this.repo = repo;
        }


        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<CarDTO> Get()
        {
            return repo.GetAll<Car>().ToList().Select(x => mapper.MapToDTO(x));
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public CarDTO Get(int id)
        {
            return mapper.MapToDTO(repo.GetById<Car>(id));
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] CarDTO value)
        {
            repo.Add(mapper.MapToEntity(value));
            repo.SaveAll();
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CarDTO value)
        {
            repo.Update(mapper.MapToEntity(value));
            repo.SaveAll();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.Delete<Car>(repo.GetById<Car>(id));
            repo.SaveAll();
        }
    }
}
