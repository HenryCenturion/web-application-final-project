<script>
import {CardApiService} from "@/dtaquito/services/card.api.service.js";
import {Card} from "@/dtaquito/model/card.entity.js";
import {SubscriptionApiService} from "@/dtaquito/services/subscription.api.service.js";
import {Subscription} from "@/dtaquito/model/subscription.entity.js";

export default {
  props: ['userID'],
  data() {
    return {
      selectedCard: null,
      cardsService: new CardApiService(),
      subsService: new SubscriptionApiService(),
      userCards: [],
      price : 70,
    };
  },
  async created() {
    const response = await this.cardsService.getByUserId(this.userID);
    this.userCards = response.data;


  },
  methods: {
    async upgradeSub() {
      const subsres = await this.subsService.getById(this.userID);
      let userSubscription = subsres.data;

      userSubscription.plan = 1;
      userSubscription = new Subscription( userSubscription.plan, userSubscription.userId );



      await this.subsService.update(this.userID, userSubscription);
      location.reload();
    },
  },
};
</script>

<template>
  <form @submit.prevent="upgradeSub">
    <p>Precio del plan premium: {{ price }}</p>
    <div class="form-group">
      <label for="card">Card</label>
      <select id="card" v-model="selectedCard" required>
        <option v-if="this.userCards" :value="userCards.id">{{ userCards.cardNumber }}</option>
      </select>
    </div>
    <button type="submit">confirm</button>
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