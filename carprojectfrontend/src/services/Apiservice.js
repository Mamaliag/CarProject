import axios from 'axios';

let http = axios.create({
    baseURL: "https://localhost:44317"
})



export default{

    AddCar(car) {
        return http.post(`/car`, car);
    },

    GetAllCars() {
        // empty string OR nothing, not sure yet
        return http.get('/car');
    },

    DeleteCar(carId) {
        return http.delete(`/car/${carId}`)
      },
}