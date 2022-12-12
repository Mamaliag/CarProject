using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CarProject.Models;

namespace CarProject.DAO
{
    public class CarDao : ICarDao
    {
        private readonly string connectionString;
        public CarDao(string connString)
        {
            connectionString = connString;
        }

        public List<Car> GetAllCars()
        {
            List<Car> carList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM cars", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    carList = new List<Car>();
                    while (reader.Read())
                    {
                        carList.Add(CreateCarFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return carList;
        }


        public Car GetCar(int carId)
        {
            // establish the SQL connection
            Car car = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM cars WHERE car_id = @car_id;", conn);
                cmd.Parameters.AddWithValue("@car_id", carId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    car = CreateCarFromReader(reader); // create a deck object from the information passed in by the database
                }
            }

            return car;
        }

        public Car AddCar(Car car)
        {
            int newCarId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO cars (car_make, car_model, " +
                                                "year, car_details, image) " +
                                                "OUTPUT INSERTED.car_id " +
                                                "VALUES ( @car_make, @car_model, " +
                                                "@car_year, @car_details, @image);", conn);
               
                cmd.Parameters.AddWithValue("@car_make", car.CarMake);
                cmd.Parameters.AddWithValue("@car_details", car.Details);
                cmd.Parameters.AddWithValue("@car_year", car.Year);
                cmd.Parameters.AddWithValue("@car_model", car.CarModel);
                cmd.Parameters.AddWithValue("@image", car.Image);

                newCarId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCar(newCarId);
        }

        public Car UpdateCar(int carId, Car car)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE cars " +
                                                "SET car_id = @car_id, car_make = @car_make, car_details = @car_details, year = @car_year, car_model = @car_model " +
                                                "WHERE car_id = @car_id;", conn);
                cmd.Parameters.AddWithValue("@car_id", car.CarId);
                cmd.Parameters.AddWithValue("@car_make", car.CarMake);
                cmd.Parameters.AddWithValue("@car_model", car.CarModel);
                cmd.Parameters.AddWithValue("@car_year", car.Year);
                cmd.Parameters.AddWithValue("@car_details", car.Details);

                cmd.ExecuteNonQuery();
            }

            Car checkCar = GetCar(carId);


            return checkCar;
        }

        public bool DeleteCar(int carId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM cars WHERE car_id = @car_id;", conn);
                cmd.Parameters.AddWithValue("@car_id", carId);

                cmd.ExecuteNonQuery();
            }

            // If there is still information at the location of this deck, it was not deleted properly
            if (GetCar(carId) != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private Car CreateCarFromReader(SqlDataReader reader)
        {
            Car car = new Car();
            car.CarId = Convert.ToInt32(reader["car_id"]);
            car.CarMake = Convert.ToString(reader["car_make"]);
            car.CarModel = Convert.ToString(reader["car_model"]);
            car.Details = Convert.ToString(reader["car_details"]);
            car.Year = Convert.ToInt32(reader["year"]);
            car.Image = Convert.ToString(reader["image"]);


            return car;
        }



    }
}
