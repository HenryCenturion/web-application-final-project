<script>
import {FieldsApiService} from "@/dtaquito/services/field.api.service.js";
import {ReservationApiService} from "@/dtaquito/services/reservation.api.service.js";
import {Field} from "@/dtaquito/model/field.entity.js";

export default {
  name: "my-reserve",
  title: "My Reserve",
  data() {
    return {
      fields: [],
      reservations: [],
      userReservationFieldIds: [],
      FieldsService: null,
      ReservationService: null,
      userId: '',
    }
  },
  async created() {
    this.userId = localStorage.getItem('userId');
    this.ReservationService = new ReservationApiService();
    this.FieldsService = new FieldsApiService();
    const response2 = await this.ReservationService.getAll();
    this.reservations = response2.data.filter(reservation => reservation.idUser === this.userId);
    this.userReservationFieldIds = this.reservations.map(reservation => reservation.idField);

    const response = await this.FieldsService.getAll();

    this.fields = response.data.filter(field => this.userReservationFieldIds.includes(field.id));

  },
}
</script>

<template>
  <div v-if="fields && fields.length">
    <h1>My Reserves</h1>
    <div class="card-container">
      <pv-card class="field-card" v-for="field in fields" :key="field.id">
        <template #title>
          <div class="field-card-title">{{ field.name }}</div>
        </template>
        <template #content>
          <div class="field-card-content">
            <img :src="field.image" class="field-image" alt="field image">
            <p>ID: {{ field.id }}</p>
            <p>Price: {{ field.price }}</p>
            <p>Rating: {{ field.rating }}</p>
          </div>
        </template>
      </pv-card>
    </div>
  </div>
  <div v-else>
    <h1>No fields reserved yet.</h1>
  </div>
</template>

<style>
.card-container {
  margin-top: 1rem;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.field-card {
  margin: 1rem;
  width: auto;
  height: auto;
}

.field-card-title {
  font-weight: bold;
}

.field-card-content {
  padding: 1rem;
}

.field-image {
  width: 250px;
  height: 250px;
  border-radius: 10%;
}

</style>