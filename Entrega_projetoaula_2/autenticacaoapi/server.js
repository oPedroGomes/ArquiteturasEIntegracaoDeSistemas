const express=require('express');
const path=require('path');
const app=express();
const bodyParser=require('body-parser');
const mongoose=require('mongoose');
const userRoute=require('./routes/userRoute');
require('dotenv').config();
const uri=process.env.ATLAS_URI;
mongoose.Promise=global.Promise;
mongoose.connect(uri).then(()=>{
    console.log('MongoDB connected');
}).catch((err)=>{
    console.log(err);
});
// Midellware
app.use('/',express.static(path.join(__dirname,'static'))); //http://127.0.0.1:9999/register.html
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended:false}));
// routes
app.use('/user',userRoute);
let port=9999;
app.listen(port,()=>{
    console.log(`Server running on port ${port}`);
})