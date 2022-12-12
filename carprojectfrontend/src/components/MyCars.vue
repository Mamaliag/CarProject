<template>
  <div id="MyCarsPage">
    <div class="Nav">
      <h1>My Cars</h1>
    </div>

    <button class="AddCar" v-on:click="showAddCar = !showAddCar">
      Add New Car
    </button>
     <button class="AddCar" v-on:click="ShowDetails = !ShowDetails">More Details</button>
    <form v-if="showAddCar" @submit.prevent="submitForm">
      Car Make:
      <input type="text" class="form-control" v-model="newCar.carMake" /><br />
      Car Model:
      <input type="text" class="form-control" v-model="newCar.carModel" /><br />
      Car Year:
      <input type="text" class="form-control" v-model="newCar.year" /><br />
      Details:
      <input type="text" class="form-control" v-model="newCar.details" /><br />
      Image URL:
      <input type="text" class="form-control" v-model="newCar.image" /><br />
      <button type="submit" class="btn btn-submit">Save</button>

      <button class="btn btn-cancel" v-on:click="showAddCar = !showAddCar">
        Cancel
      </button>
    </form>
    <div class="cars" v-for="car in $store.state.cars" v-bind:key="car.CarId">
      <p class="eachCar">
        {{ car.carMake }}<br />
        {{ car.carModel }}<br />
        <button id="delete" class="btn btn-submit" v-on:click="deleteCar(car.carId)">Delete</button>
       
      </p>
      <img class="image" v-if="ShowDetails" v-bind:src="car.image" />
      <p class="moreDetails" v-if="ShowDetails">
        {{ car.details }}<br />
        {{ car.year }}<br />
      </p>
    </div>
  </div>
</template>

<script>
import apiService from "../services/Apiservice.js";

export default {
  name: "myCars",
  data() {
    return {
      showAddCar: false,
      ShowDetails: false,
      newCar: {
        carMake: "",
        carModel: "",
        year: "",
        details: "",
        carId: "0",
        image: "",
      },
      errorMsg: "",
    };
  },
  created() {
    this.retrieveCars();
  },
  methods: {
    retrieveCars() {
      this.$store.commit("SET_CARS", []);
      apiService
        .GetAllCars()
        .then((response) => {
          console.log(response.data);
          this.$store.commit("SET_CARS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
    submitForm() {
      const tempCar = {
        carMake: this.newCar.carMake,
        details: this.newCar.details,
        carModel: this.newCar.carModel,
        year: parseInt(this.newCar.year),
        image: this.newCar.image,
      };
      console.log(tempCar);
      this.showAddCar = false;

      // take away the form so the user can't click the 'save' button 823,492 times while waiting for the Promise to resolve
      apiService
        .AddCar(tempCar)
        .then(() => {
          this.retrieveCars();
          //reset new car object
          this.newCar = {
            carMake: "",
            details: "",
            carModel: "",
            year: "",
            image: "",
          };
        })
        .catch((error) => {
          if (error.response) {
            //if the error object has a response, I know I made it to the server
            this.errorMsg =
              "Error adding a new car, response received from the server was " +
              error.response.statusText +
              ".";
          } else if (error.request) {
            //we never got a response, so there was a problem reaching the server
            this.errorMsg =
              "Error adding a new car, could not reach the server.";
          } else {
            //no request, no response, something has gone terribly wrong in the application
            this.errorMsg =
              "Error adding a new car, request could not be created.";
          }
        });
    },
    deleteCar(carId) {
      if (
        confirm(
          "Are you sure you want to delete this car? This action cannot be undone."
        )
      ) {
        apiService
          .DeleteCar(carId)
          .then((response) => {
            if (response.status === 204) {
              alert("Car successfully deleted");
              this.retrieveCars();
            }
          })
          .catch((error) => {
            if (error.response) {
              this.errorMsg =
                "Error deleting car. Response received was '" +
                error.response.statusText +
                "'.";
            } else if (error.request) {
              this.errorMsg =
                "Error deleting car. Server could not be reached.";
            } else {
              this.errorMsg =
                "Error deleting car. Request could not be created.";
            }
          });
      }
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

@import "https://fonts.googleapis.com/css?family=Lily+Script+One";


.MyCarsPage {
  display: flex;
  align-items: center;
 
}
h1 {
  text-align: center;
  font-family: "Lily Script One", "Trebuchet MS", "Lucida Sans Unicode",
  "Lucida Grande", "Lucida Sans", Arial, sans-serif;
}

div.cars {
  flex-direction: column;
  border-width: 3px;
  height:fit-content;
  align-items: stretch;
  font-size: 16px;
  width: 60%;
  /*margin: 50px;*/
  margin: auto;
  padding: 20px;
  cursor: pointer;
  font-weight: bold;
}


.eachCar {

text-align: center;
align-items: center;
color: #f7fafc;
border-style: solid;
border-radius: 10px;
border-width: 2px;
border-color: black;
background-color: rgb(79, 189, 240);
font-size: 16px;
width: 90%;
margin: auto;
padding: 20px;
/*margin-bottom: 35px;*/
cursor: pointer;
}
.AddCar {
  align-items: center;
  color: #f7fafc;
  border-radius: 10px;
  background-color: mediumaquamarine;
  font-size: 16px;
  width: 90%;
  margin: 10px;
  padding: 20px;
  margin-bottom: 35px;
  cursor: pointer;
}
.form-control {
  display: flexbox;
  margin-bottom: 20px;
}
form {
  margin: 0 auto;
  height:fit-content;
  width: 400px;
  padding: 20px;
  border-radius: 1rem;
  background-color: whitesmoke;
  border: 4px solid hsl(0, 0%, 90%);
  display: grid;

  gap: 3px;
}
.addCar:hover {
  font-weight: bold;
}
.image {
  position: relative;
  justify-content: left;
  justify-items: stretch;
  width: 300px;
  height: 200px;
}
</style>
