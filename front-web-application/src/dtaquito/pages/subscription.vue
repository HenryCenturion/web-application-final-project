<script>
import {CardApiService} from "@/dtaquito/services/card.api.service.js";
import {SubscriptionApiService} from "@/dtaquito/services/subscription.api.service.js";
import UpgradeSubscription from "@/dtaquito/components/UpgradeSubscription.vue";


export default {
  name: "subscription",
  components: {UpgradeSubscription},
  title: "Subscription",
  data() {
    return {
      userId: '',
      userCards: [],
      userSubscription: null,
      CardService: null,
      SubscriptionService: null,
      showForm: false,
      userID: null,
      userRole: null,
    }
  },
  async created() {
    this.userId = localStorage.getItem('userId');
    this.userRole = localStorage.getItem('userRole');
    this.CardService = new CardApiService();
    this.SubscriptionService = new SubscriptionApiService();

    this.userSubscription = await this.SubscriptionService.getById(this.userId);
    this.userSubscription = this.userSubscription.data
    //  this.userSubscription = response.data.find(subscription => subscription.userId === this.userId);
    console.log(this.userSubscription);
    console.log(this.userSubscription.planId, "planID");

  },
}
</script>

<template>
  <h1>Subscription</h1>
  <pv-card v-if="this.userSubscription">
    <template #title>
      <div>Tu Plan</div>
    </template>
    <template #content>
      <p v-if="this.userSubscription.planId === 1">Plan: Premium</p>
      <p v-else-if="this.userSubscription.planId === 2">Plan: Free</p>
      <p>Role: {{ userRole }}</p>
      <p>ID: {{ this.userSubscription.userId }}</p>
      <div v-if="this.userSubscription.planId === 2">
        <pv-button class="p-button-rounded p-button-success p-button-sticky"
                   @click="showForm = true">
          Mejorar Plan
          <i class="pi pi-plus"></i>
        </pv-button>
      </div>
      <div v-else>
        <p>Gracias por mejorar tu plan, disfrútalo</p>
      </div>
    </template>
  </pv-card>
  <pv-dialog v-model:visible="showForm" header="Upgrade subscription">
    <upgrade-subscription :userID="userId" />
  </pv-dialog>
</template>

<style>

</style>