const mongoose = require('mongoose');
const bcrypt = require('bcrypt');
const user = require('../Model/userModel');
const jwt = require('jsonwebtoken');

const JWT_SECRET = process.env.JWT_SECRET;

require('dotenv').config();
exports.register = async (req, res) => {
 //response
 const{username, email,password} = req.body;

if(!username || !email || !password){
    res.json({message: "Preencha todos os campos"});
    return;
};

 const passwordEncrypted = await bcrypt.hash(password, 10);
 //encrypt 
console.log(passwordEncrypted);
 res.json(req.body);
try{
    const result = await user.create({
        username: username,
        email: email,
        password: passwordEncrypted
    });
}catch(err){
    console.log(err);
    if(err.code === 11000){
        res.json({message: "Usuário já existe"});
        return;
    }
    res.json({message: "Erro ao criar usuário"});
}};


exports.login = async (req, res) => {
 const {username, password} = req.body;
 if(!username || !password){
     res.json({message: "Preencha todos os campos"});
     return;
 };
if(await bcrypt.compare(password, user.password)){
    try{
        const token = jwt.sign({username}, JWT_SECRET);
    }catch(err){

    }
}

 const user = await User.findOne({username: username}).lean();
 console.log(req.body);
 return res.json({status :200,ah: req.body});

}