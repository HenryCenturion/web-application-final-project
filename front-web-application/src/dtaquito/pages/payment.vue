<<script>
import {CardApiService} from "@/dtaquito/services/card.api.service.js";
import AddFieldForm from "@/dtaquito/components/AddFieldForm.vue";
import AddCardForm from "@/dtaquito/components/AddCardForm.vue";

export default {
  name: "payment",
  components: {AddCardForm, AddFieldForm},
  title: "Payment",
  data() {
    return {
      userId: '',
      userCards: [],
      CardService: null,
      showForm: false,
      userID: null,
    }
  },
  async created() {
    this.userId = localStorage.getItem('userId');
    this.CardService = new CardApiService();
    this.CardService.getByUserId(this.userId).then(response => {
      if (response.data) {
        this.userCards = response.data;
        console.log("Card Number: " + this.userCards.cardNumber);
        console.log("Expiration Date: " + this.userCards.expirationDate);
        console.log("Card Holder: " + this.userCards.cardHolder);
        console.log("Card Issuer: " + this.userCards.cardIssuer);
        console.log("CVV: " + this.userCards.cvv);
        console.log("Balance: " + this.userCards.balance);
      } else {
        console.error('Error: response.data is not defined');
      }
    });

    if (this.userCards.length === 0) {
      console.log('El usuario no tiene una tarjeta asignada');
    }else {
      console.log(this.userCards);
    }
  },
}
</script>

<template>
  <h1>Payments</h1>
  <div v-if="userCards">
    <pv-card >
        <template #title>
        <div>{{ userCards.cardHolder }}</div>
      </template>
        <template #content>
        <p>Card Number: {{ userCards.cardNumber }}</p>
        <p>Expiry Date: {{ userCards.expirationDate }}</p>
        <p>Card Issuer: {{ userCards.cardIssuer }}</p>
        <p>CVV: {{ userCards.cvv }}</p>
        <p>Balance: {{ userCards.balance }}</p>
        </template>
    </pv-card>
  </div>
  <div v-else>
    <p>El usuario no tiene una tarjeta asignada</p>
  </div>
  <pv-button class="p-button-rounded p-button-success p-button-sticky"
             @click="showForm = true">
    Agregar Tarjeta
    <i class="pi pi-plus"></i>
  </pv-button>
  <pv-dialog v-model:visible="showForm" header="Tarjeta nueva">
    <add-card-form :userID="userId" />
  </pv-dialog>
</template>

<style scoped>

</style>