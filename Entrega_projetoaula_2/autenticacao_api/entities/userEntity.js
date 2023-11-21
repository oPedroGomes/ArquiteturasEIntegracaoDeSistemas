exports.UserEntity = new class{
    constructor(username,email,password){
        this.username = username;
        this.email = email;
        this.password = password;
    }
    async validator(){
        if(this.username == null || this.username == undefined || this.username == ""){
            return ({status:400, message:"Username is required"})
        }
        if(this.email == null || this.email == undefined || this.email == ""){
            return ({status:400, message:"Email is required"})
        }
        if(this.password == null || this.password == undefined || this.password == ""){
            return ({status:400, message:"Password is required"})
        }
        return ({status:200, message:"OK"})
    }
}