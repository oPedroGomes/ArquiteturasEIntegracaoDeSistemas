const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');

const Jwt = require('jsonwebtoken');


//Model
require('../frameworks_drivers/database/models/user');

const jwtsecret = process.env.JWT_SECRET;
const User = mongoose.model('User');
exports.userloginPersistence = async (userEntity) => {

    const {username,password} = userEntity;
    const userFind = await User.findOne({username:username}).lean();
    if(!userFind){
        return ({status:400, message:"User not found"})
    } 
    if(await bcrypt.compare(password, userFind.password)){
        try{
            const token = Jwt.sign({
                id: userFind._id,
                username: userFind.username
            }, jwtsecret, { expiresIn: '24h' });
            return ({status:200, message:"OK",token:token})
        }catch(err){ 
            console.log(err);
            return ({status:400, message:"Error"})
        }
    }
}