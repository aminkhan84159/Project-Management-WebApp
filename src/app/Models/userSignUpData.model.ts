export class userSignupData{
    email:string;
    username:string;
    password:string;
    userId:number;

    usersSignupData:{
        email:string,
        username:string,
        password:string,
        userId:number,
    }[];
    constructor(){
        this.email="",
        this.username="";
        this.password="";
        this.userId=100;
        this.usersSignupData = [];
    }
}
