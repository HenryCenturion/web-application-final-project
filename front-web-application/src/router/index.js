import {createRouter, createWebHistory} from "vue-router";
import HomeComponent from "@/public/pages/home-component.vue";
import AboutComponent from "@/public/pages/about-component.vue";
import FieldsCatalogue from "@/dtaquito/pages/fields-catalogue.vue";
import loginComponent from "@/public/pages/login-component.vue";
import registerComponent from "@/public/pages/register-component.vue";
import FieldDetails from "@/dtaquito/pages/field-detail.vue";
import MyReserve from "@/dtaquito/pages/my-reserve.vue";
import Payment from "@/dtaquito/pages/payment.vue";
import Subscription from "@/dtaquito/pages/subscription.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/home', component: HomeComponent, meta: { title: 'Home' }, },
        { path: '/about', component: AboutComponent, meta: { title: 'About Us' },},
        { path: '/login', component: loginComponent, meta: { title: 'Login' },},
        { path: '/fields', component: FieldsCatalogue, meta: { title: 'Fields Catalogue' },},
        { path: '/fields/:id', component: FieldDetails, meta: { title: 'Field Details' },},
        { path: '/register', component: registerComponent, meta: { title: 'Register' },},
        {path: '/myreserve', component: MyReserve, meta: { title: 'My Reserves' },},
        {path: '/payment', component: Payment, meta: { title: 'Patment'},},
        {path: '/subscription', component: Subscription, meta: { title: 'Subscription'},},
        { path: '/', redirect: '/home' }
    ]
});


router.beforeEach((to, from, next) => {
    let baseTitle = 'D´Taquito';
    document.title = `${ baseTitle } | ${ to.meta["title"]}`;
    next();
});

export default router;
