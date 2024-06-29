import http from "@/shared/services/http-common.js";

export class FieldsApiService {
    getAll() {
        return http.get('/sport-spaces');
    }

    getById(id){
        return http.get(`/sport-spaces/${id}`);
    }

    getByUserId(id){
        return http.get(`/sport-spaces/user/${id}`);
    }
    create(field){
        return http.post('/sport-spaces', field);
    }

    update(id, fieldResource){
        return http.put(`/sport-spaces/${id}`, fieldResource);
    }

    delete(id){
        return http.delete(`/sport-spaces/${id}`);
    }
}