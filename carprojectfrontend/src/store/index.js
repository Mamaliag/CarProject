import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    cars: [],
    car: {
      carMake: '',
      carModel: '',
      year: '',
      details: '',
      carId: 0,
      image: ""

    }
  },
  mutations: {
    SET_CARS(state, data) {
      state.cars = data;
    },
    DELETE_CAR(state, carIdToDelete) {
      state.cars = state.cars.filter(
        (car) => {
          return car.id !==
            carIdToDelete;
        });
    }
  },
  actions: {
  },
  modules: {
  }
})
