import http from "@/shared/services/http-common.js";

export class SubscriptionApiService {
    getAll() {
        return http.get('/suscriptions');
    }

    getById(id){
        return http.get(`/suscriptions/${id}`);
    }

    create(Subscription){
        return http.post('/suscriptions', Subscription);
    }

    update(id, SubscriptionResource){
        return http.put(`/suscriptions/${id}`, SubscriptionResource);
    }

    delete(id){
        return http.delete(`/suscriptions/${id}`);
    }
}