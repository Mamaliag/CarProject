using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;

namespace CarProject.DAO
{
    public interface ICarDao
    {
        Car GetCar(int carId);
        Car AddCar(Car car);
        Car UpdateCar(int cardId, Car car);
        bool DeleteCar(int carId);
        List<Car> GetAllCars();



    }
}
