import http from "@/shared/services/http-common.js";

export class CardApiService {
    getAll() {
        return http.get('/payments');
    }

    getById(id){
        return http.get(`/payments/${id}`);
    }

    getByUserId(id){
        return http.get(`/payments/user/${id}`);
    }

    create(card){
        return http.post('/payments', card);
    }

    update(id, cardResource){
        return http.put(`/payments/${id}`, cardResource);
    }

    delete(id){
        return http.delete(`/payments/${id}`);
    }
}