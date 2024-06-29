export class Card {
    constructor( cardNumber, expirationDate, cardHolder, cardIssuer,cvv,userId,balance) {
        this.cardNumber = cardNumber;
        this.expirationDate = expirationDate;
        this.cardHolder = cardHolder;
        this.cardIssuer = cardIssuer;
        this.cvv = cvv;
        this.userId = userId;
        this.balance = balance;
    }

    static FromDisplayableCard(displayableField) {
        return new Card(
            displayableField.cardNumber,
            displayableField.expirationDate,
            displayableField.cardHolder,
            displayableField.cardIssuer,
            displayableField.cvv,
            displayableField.userId,
            displayableField.balance
        );
    }

    static toDisplayableCard(balance) {
        return {
            cardNumber: balance.cardNumber,
            expirationDate: balance.expirationDate,
            cardHolder: balance.cardHolder,
            cardIssuer: balance.cardIssuer,
            cvv: balance.cvv,
            userId: balance.userId,
            balance: balance.balance
        };
    }
}
