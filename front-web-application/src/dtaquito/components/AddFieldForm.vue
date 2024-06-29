<script>
import { FieldsApiService } from "@/dtaquito/services/field.api.service.js";
import {Field} from "@/dtaquito/model/field.entity.js";

export default {
  props: ['userID'],
  data() {
    return {
      name: "",
      price: "",
      rating: "",
      image: "",
      description: "",
      ability: "",
      startTime: "",
      endTime: "",
    };
  },
  methods: {
    async addField() {
      const fieldsService = new FieldsApiService();
      const id = Math.random().toString().slice(2);
      const field = new Field(id,this.name, this.price, this.rating, this.image, this.userID, this.description, this.startTime, this.endTime);
      try {
        await fieldsService.create(field);
        alert("Field added successfully");
        location.reload();
      } catch (error) {
        console.error("Error adding field: ", error);
      }
    },
  },
};
</script>

<template>
  <form @submit.prevent="addField">
    <div class="form-group">
      <label for="name">Name</label>
      <input type="text" id="name" v-model="name" required>
    </div>
    <div class="form-group">
      <label for="price">Price</label>
      <input type="number" id="price" v-model="price" required>
    </div>
    <div class="form-group">
      <label for="rating">Rating</label>
      <input type="number" id="rating" v-model="rating" required>
    </div>
    <div class="form-group">
      <label for="description">Description</label>
      <input type="text" id="description" v-model="description" required>
    </div>
    <div class="form-group">
      <label for="image">Image URL</label>
      <input type="text" id="image" v-model="image" required>
    </div>
    <div class="form-group">
      <label for="startTime">Start Time</label>
      <input type="text" id="startTime" v-model="startTime" required>
    </div>
    <div class="form-group">
      <label for="endTime">End Time</label>
      <input type="text" id="endTime" v-model="endTime" required>
    </div>
    <button type="submit">Add Field</button>
  </form>
</template>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 300px;
  margin: 0 auto;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
  width: 100%;
}

.form-group label {
  margin-bottom: 5px;
  font-weight: bold;
}

.form-group input {
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
}

button {
  padding: 10px;
  border-radius: 5px;
  border: none;
  background-color: forestgreen;
  color: white;
  cursor: pointer;
  width: 100%;
  font-size: 16px;
}

button:hover {
  background-color: goldenrod;
}
</style>