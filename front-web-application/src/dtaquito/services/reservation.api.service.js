import http from "@/shared/services/http-common.js";

export class ReservationApiService {
    getAll() {
        return http.get('/reservations');
    }

    getById(id){
        return http.get(`/reservations/${id}`);
    }

    create(reservation){
        return http.post('/reservations', reservation);
    }

    update(id, reservationResource){
        return http.put(`/reservations/${id}`, reservationResource);
    }

    delete(id){
        return http.delete(`/reservations/${id}`);
    }
}