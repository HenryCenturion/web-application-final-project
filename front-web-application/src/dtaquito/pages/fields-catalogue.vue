<script>
import {FieldsApiService} from "@/dtaquito/services/field.api.service.js";
import {Field} from "@/dtaquito/model/field.entity.js";
import AddFieldForm from "@/dtaquito/components/AddFieldForm.vue";
import {SubscriptionApiService} from "@/dtaquito/services/subscription.api.service.js";
import Subscription from "@/dtaquito/pages/subscription.vue";


export default {
  name: "fields-catalogue",
  title: "Fields Catalogue",
  components: {
    AddFieldForm,
  },
  data(){
    return {
      title: "Fields-Catalogue",
      fields: [],
      fieldDialog: false,
      deleteFieldDialog: false,
      deleteFieldsDialog: false,
      field: null,
      selectedFields: null,
      filters:{},
      submitted: false,
      FieldsService: null,
      showAddFieldForm: false,
      userRole: null,
      userID: null,
      subsService: new SubscriptionApiService(),
      userSubscription: null,
    }
  },
  async created() {
    this.userID = Number(localStorage.getItem('userId'));
    this.userRole = localStorage.getItem('userRole');

    this.FieldsService = new FieldsApiService();

    this.FieldsService.getAll().then(response => {
      if (this.userRole === '2') {
        this.fields = response.data.filter(fields=> fields.user.id === this.userID);
        console.log(this.fields);
        console.log(this.userID);
      } else {
        this.fields = response.data;
        console.log(this.fields);

      }
      this.fields = this.fields.map(tutorial => {
        return Field.toDisplayableField(tutorial);
      });
    });
    //const subsResponse = await this.subsService.getAll();
    //this.userSubscription = subsResponse.data.find(subscription => subscription.userId === this.userID);
    this.userSubscription= await this.subsService.getById(this.userID);
    this.userSubscription = this.userSubscription.data;

    console.log(this.userSubscription, 'userSubscription');
    console.log(this.userSubscription.planId, 'userSubscription.planId');
    if(localStorage.getItem('isLoggedIn') === 'true'){
      localStorage.setItem('isLoggedIn', 'false');
      location.reload();
    }
  },
  methods: {
    async deleteField(fieldId) {
      try {
        await this.FieldsService.delete(fieldId);
        this.fields = this.fields.filter(field => field.id !== fieldId);
      } catch (error) {
        console.error("Error al eliminar el campo: ", error);
      }
    },
    viewMore(fieldId) {
      this.$router.push(`/fields/${fieldId}`);
      localStorage.setItem('fieldId', fieldId);
    },
    alerta() {
      alert('You need to upgrade to premium to add more fields.');
    }
  }
}
</script>
<template>
  <div class="field-container">
    <pv-card class="field-card" v-for="field in fields" :key="field.id">

      <template #title>
        <div class="field-card-title">{{ field.name }}</div>
      </template>
      <template #content>
        <div class="field-card-content">
          <img :src="field.imageUrl" alt="field image" class="field-image">
          <p>ID: {{ field.id }}</p>
          <p>Price: {{ field.price }}</p>
          <p>Rating: {{ field.rating }}</p>
          <p>Open Time: {{ field.startTime}}</p>
          <p>Close Time: {{ field.endTime}}</p>
          <pv-button v-if="userRole === '2' && this.userSubscription && this.userSubscription.planId === 1" @click="deleteField(field.id)">Borrar</pv-button>
          <pv-button v-if="userRole === '1'" @click="viewMore(field.id)">Ver más</pv-button>
        </div>
      </template>
    </pv-card>

    <pv-button class="p-button-rounded p-button-success p-button-sticky"
                v-if="userRole === '2' && this.userSubscription && this.userSubscription.planId === 1"
               @click="showAddFieldForm = true">
      Agregar Cancha
      <i class="pi pi-plus"></i>
    </pv-button>
    <pv-button class="p-button-rounded p-button-success p-button-sticky"
               v-if="userRole === '2' && this.userSubscription && this.userSubscription.planId === 2"
               @click="alerta">
      Agregar Cancha
      <i class="pi pi-plus"></i>
    </pv-button>
    <pv-dialog v-model:visible="showAddFieldForm" header="Add Field">
      <add-field-form :userID="userID" />
    </pv-dialog>
  </div>
</template>

<style>
.field-container {
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
.p-button-sticky {
  position: fixed;
  right: 20px;
  bottom: 20px;
  z-index: 1000;
}
</style>