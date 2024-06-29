export class User {
    constructor(id, name, roleId, email, password) {
        this.id = id;
        this.name = name;
        // this.lastname = lastname;
        // this.age = age;
        this.roleId = roleId;
        this.email = email;
        this.password = password;
    }

    static FromDisplayableUser(displayableUser) {
        return new User(
            displayableUser.id,
            displayableUser.name,
            displayableUser.roleId,
            displayableUser.email,
            displayableUser.password,
        );
    }

    static toDisplayableUser(User) {
        return {
            id: User.id,
            name: User.name,
            roleId: User.roleId,
            email: User.email,
            password: User.password,
        };
    }
}