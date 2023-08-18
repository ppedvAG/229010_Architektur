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
        private readonly IUnitOfWork unitOfWork;

        CarMapper mapper = new CarMapper();

        public CarController(IUnitOfWork uow)
        {
            this.unitOfWork = uow;
        }


        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<CarDTO> Get()
        {
            return unitOfWork.CarRepository.GetAll().ToList().Select(x => mapper.MapToDTO(x));
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public CarDTO Get(int id)
        {
            return mapper.MapToDTO(unitOfWork.CarRepository.GetById(id));
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] CarDTO value)
        {
            unitOfWork.CarRepository.Add(mapper.MapToEntity(value));
            unitOfWork.SaveAll();
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CarDTO value)
        {
            unitOfWork.CarRepository.Update(mapper.MapToEntity(value));
            unitOfWork.SaveAll();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            unitOfWork.CarRepository.Delete(unitOfWork.CarRepository.GetById(id));
            unitOfWork.SaveAll();
        }
    }
}
