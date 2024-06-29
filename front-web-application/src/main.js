import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from "primevue/config";
import router from "@/router/index.js";

import Button from "primevue/button";
import Toolbar from "primevue/toolbar";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Row from "primevue/row";



import 'primeicons/primeicons.css';
import 'primevue/resources/themes/mdc-dark-indigo/theme.css';
import 'primeflex/primeflex.css';
import Card from "primevue/card";
import Dialog from "primevue/dialog";



createApp(App)
    .use(PrimeVue, {ripple: true})
    .use(router)
    .component('pv-button', Button)
    .component('pv-toolbar', Toolbar)
    .component('pv-card', Card)
    .component('pv-data-table', DataTable)
    .component('pv-column', Column)
    .component('pv-row', Row)
    .component('pv-dialog', Dialog)
    .mount('#app')
