<script>
import {FieldsApiService} from "@/dtaquito/services/field.api.service.js";
import {ReservationApiService} from "@/dtaquito/services/reservation.api.service.js";
import AddReservationForm from "@/dtaquito/components/AddReservationForm.vue";
import addReservationForm from "@/dtaquito/components/AddReservationForm.vue";

export default {
  name: "field-detail",
  computed: {
    addReservationForm() {
      return addReservationForm
    }
  },
  components: {AddReservationForm},
  title: "Field Details",
  data() {
    return {
      startTime: "",
      endTime: "",
      dateTime: "",
      field: null,
      FieldsService: null,
      ReservationService: null,
      userId: localStorage.getItem('userId'),
      showForm: false,
      userID: null,
      totalCost: 0,
      hours: 0,

    }
  },
  async created() {
    this.userId = localStorage.getItem('userId');
    this.ReservationService = new ReservationApiService();
    this.FieldsService = new FieldsApiService();
    const fieldId = this.$route.params.id;
    const response = await this.FieldsService.getById(fieldId);
    this.field = response.data;


  },
  methods: {

    async reserveField() {
      const currentDateTime = new Date();
      const selectedDateTime = new Date(this.dateTime);

      if (selectedDateTime < currentDateTime) {
        alert('No puedes seleccionar una fecha y hora pasadas.');
        return;
      }

      if ( !this.dateTime  ){
        alert('Por favor, complete todos los campos.');
        return;
      }
      this.totalCost= this.field.price * this.hours;
      console.log('Total cost:', this.totalCost);
      console.log('Hours:', this.hours);
      console.log('Field price:', this.field.price);
      console.log('dateTime:', this.dateTime);
      this.showForm = true;

      //console.log('Reserving field with userId:', this.userId, 'and fieldId:', this.field.id);
      //const reservation= new Reservation("",this.field.id, this.userId);
      //await this.ReservationService.create(reservation);
      //this.$router.push('/fields');
    },
    cancel() {
      this.$router.push('/fields');
    }
  }
}
</script>
<template>
  <div v-if="field" class="field-detail">
    <div class="image-container">
      <img :src="field.imageUrl" alt="field image">
    </div>
    <div class="info-container">

      <h1>{{ field.name }}</h1>
      <p>ID: {{ field.id }}</p>
      <p>Price: {{ field.price }}</p>
      <p>Rating: {{ field.rating }}</p>
      <p>Description:{{ field.description }}</p>

      <div class="form-group">
        <label for="datetime">Date and Time</label>
        <input type="datetime-local" id="datetime" v-model="dateTime"  required>
      </div>
      <div class="form-group">
        <label for="hours">Hours</label>
        <input type="number" id="hours" v-model="hours" min="1" required>
      </div>
      <button  @click="reserveField">Reservar</button>
      <button @click="cancel">Cancelar</button>
      <pv-dialog v-model:visible="showForm" header="New reservation">
        <add-reservation-form :userID="userId" :fieldId="field.id"
                              :totalCost="totalCost"  :date="dateTime" :hours="hours"/>
      </pv-dialog>
    </div>
  </div>

</template>

<style scoped>
.field-detail {
  display: flex;
  height: 100vh;
}

.image-container {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.info-container {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;

}

img {
  width: 300px;
  height: 200px;
  object-fit: cover;
  border-radius: 5px;
}

p {
  margin: 10px 0;
}

button {
  padding: 5px;
  margin: 10px 0;
  font-size: 14px;
  border-radius: 5px;
  border: none;
  background-color: forestgreen;
  color: white;
  cursor: pointer;
  width: 50%;
  align-items: center;
}

button:hover {
  background-color: goldenrod;
}
.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
  width: 17%;
}

.form-group label {
  margin-bottom: 5px;
  font-weight: bold;
}

.form-group input {
  padding: 20px;
  border-radius: 5px;
  border: 1px solid #ccc;
}
</style>