export class Reservation {
    constructor(sportSpaceId, userId, time, hours) {

        this.sportSpaceId = sportSpaceId;
        this.userId = userId;
        this.time = time;
        this.hours = hours;
    }

    static FromDisplayableReservation(displayableReservation) {
        return new Reservation(
            displayableReservation.sportSpaceId,
            displayableReservation.userId,
            displayableReservation.time,
            displayableReservation.hours,
        );
    }

    static toDisplayableReservation(Reservation) {
        return {
            sportSpaceId: Reservation.sportSpaceId,
            userId: Reservation.userId,
            time: Reservation.time,
            hours: Reservation.hours,
        };
    }
}