<script>
  import { CardApiService } from "@/dtaquito/services/card.api.service.js";
  import { Card } from "@/dtaquito/model/card.entity.js";
export default {
  props: ['userID'],
  data() {
    return {
      cardNumber: "",
      cardHolder: "",
      expiryDate: "",
      cardIssuer: "",
      cvv: "",
      balance: "",
    };
  },
  methods: {
    async addCard() {
      const cardsService = new CardApiService();
      const card = new Card( this.cardNumber, this.expiryDate, this.cardHolder, this.cardIssuer, this.cvv, this.userID, this.balance);
      try {
        await cardsService.create(card);
        alert("Card added successfully");
        location.reload();
      } catch (error) {
        console.error("Error adding card: ", error);
      }

    },
  },
};
</script>

<template>
    <form @submit.prevent="addCard">
      <div class="form-group">
        <label for="cardNumber">Card Number</label>
        <input type="text" id="cardNumber" v-model="cardNumber" required>
      </div>
      <div class="form-group">
        <label for="cardHolder">Card Holder</label>
        <input type="text" id="cardHolder" v-model="cardHolder" required>
      </div>
      <div class="form-group">
        <label for="expiryDate">Expiry Date</label>
        <input type="date" id="expiryDate" v-model="expiryDate" required>
      </div>
      <div class="form-group">
        <label for="cardIssuer">Card Issuer</label>
        <input type="text" id="cardIssuer" v-model="cardIssuer" required>
      </div>
      <div class="form-group">
        <label for="cvv">CVV</label>
        <input type="text" id="cvv" v-model="cvv" required>
      </div>
      <div class="form-group">
        <label for="balance">Balance</label>
        <input type="number" id="balance" v-model="balance" required>
      </div>
      <button type="submit">Agregar Tarjeta</button>
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