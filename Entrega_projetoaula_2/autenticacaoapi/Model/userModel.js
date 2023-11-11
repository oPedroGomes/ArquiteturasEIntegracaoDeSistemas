const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const UserSchema= new Schema(
    {username: {type:String,require:true,unique:true},
    email: {type:String,require:true},
    password: {type:String,require:true}},
    {collection:'users'})
const User=mongoose.model('User',UserSchema)
module.exports=User