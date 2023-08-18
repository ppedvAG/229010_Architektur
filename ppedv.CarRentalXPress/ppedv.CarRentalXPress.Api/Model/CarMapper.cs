using ppedv.CarRentalXPress.Model;

namespace ppedv.CarRentalXPress.Api.Model
{
    public class CarMapper
    {
        public CarDTO MapToDTO(Car car)
        {
            return new CarDTO
            {
                Id = car.Id,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Color = car.Color,
                KW = car.KW
            };
        }

        public Car MapToEntity(CarDTO carDTO)
        {
            return new Car
            {
                Id = carDTO.Id,
                Manufacturer = carDTO.Manufacturer,
                Model = carDTO.Model,
                Color = carDTO.Color,
                KW = carDTO.KW
            };
        }
    }

}
