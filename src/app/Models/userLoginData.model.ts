export class UserLoginData{
    loginData:string;
    password:string;
    constructor(){
        this.loginData="",
        this.password=""
    }
}


export interface IResult{
data:any,
result:boolean,
message:string,
error:{};
}