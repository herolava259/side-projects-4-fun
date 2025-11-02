
class User
{
    constructor(firstName, lastName, userName, password)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.userName = userName;
        this.password = password;
    }
}

function parseUser(userData)
{
    const user = new User(userData.firstname, userData.lastname, userData.username, userData.password);

    return user;
}