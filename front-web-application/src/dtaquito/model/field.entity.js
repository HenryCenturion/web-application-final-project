export class Field {
    constructor(id, name, price, rating, imageUrl,userId,description, startTime, endTime){
        this.id = id;
        this.imageUrl = imageUrl;
        this.name = name;
        this.price = price;
        this.rating = rating;
        this.userId = userId;
        this.description = description;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    static FromDisplayableField(displayableField) {
        return new Field(
            displayableField.id,
            displayableField.imageUrl,
            displayableField.name,
            displayableField.price,
            displayableField.rating,
            displayableField.userId,
            displayableField.description,
            displayableField.startTime,
            displayableField.endTime
        );
    }

    static toDisplayableField(field) {
        return {
            id: field.id,
            imageUrl: field.imageUrl,
            name: field.name,
            price: field.price,
            rating: field.rating,
            userId: field.userId,
            description: field.description,
            startTime: field.startTime,
            endTime: field.endTime
        };
    }
}
