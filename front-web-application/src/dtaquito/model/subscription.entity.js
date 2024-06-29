
export class Subscription {
    constructor(  planId, userId) {
        this.planId = planId;
        this.userId = userId;
    }

    static FromDisplayableSubscription(displayableField) {
        return new Subscription(
            displayableField.planId,
            displayableField.userId,
        );
    }

    static toDisplayableSubscription(Subscription) {
        return {
            planId: Subscription.planId,
            userId: Subscription.userId,
        };
    }
}
