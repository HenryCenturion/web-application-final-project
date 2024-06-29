import http from "@/shared/services/http-common.js";

export class UserApiService {
    getAll(email, password){
        return http.get(`/users?email=${email}&password=${password}`);
    }

    getById(id){
        return http.get(`/users/${id}`);
    }

    create(user){
        return http.post('/users', user);
    }

    update(id, user){
        return http.put(`/users/${id}`, user);
    }

    delete(id){
        return http.delete(`/user/${id}`);
    }
}