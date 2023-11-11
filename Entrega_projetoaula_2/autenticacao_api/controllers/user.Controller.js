const bcrypt = require('bcrypt');
const mongoose = require('mongoose');
require('dotenv').config();
const User = mongoose.model('User');
const jwt = require('jsonwebtoken');


/**
 * @api {post} /user/changepassword alterar pass
 * @apiVersion 1.0.0
 * @apiName Changepassword
 * @apiGroup Autentication
 * @apiParam {String} pass_old .
 * @apiParam {String} pass_new .
 * @apiParam {String} token .
 * @apiSuccessExample {Json} Sucesso
 *  HTTP/1.1 200 ok 
 * {
    "status": "200", 
    "message": "password changed"
 * }
 */
exports.changePassword = async (req, res) => {
    console.log(req.body);
    const {token, oldPassword, newPassword } = req.body;
    if(!oldPassword || !newPassword){
        return res.json({
            message: 'all fields are required',
            status: 400
        })
    }
    if(newPassword.length < 6){
        return res.json({
            message: 'password must be at least 6 characters',
            status: 400
        })
    }
    try{
        const user=jwt.verify(token, process.env.TOKEN_SECRET);
        const _id=user.id;
        const hashedPassword = await bcrypt.hash(newPassword, 10);
        console.log('JWTDecoded', user);
        await User.updateOne({ _id }, { password: hashedPassword });
        return res.json({
            message: 'password changed',
            status: 200
        })
    }catch(err){
        console.log(err);
        return res.json({
            message: 'error changing password',
            status: 400
        })
    }
}
/**
 * @api {post} /user/login Login
 * @apiVersion 1.0.0
 * @apiName Login
 * @apiGroup Autentication
 * @apiParam {String} username Utilizador.
 * @apiParam {String} password Password.
 * @apiSuccessExample {Json} Sucesso
 *  HTTP/1.1 200 ok 
 * {
    "status": "200",
    "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYzNmVhYTk1MGM4NjM5ODllODQzZGRjZSIsInVzZXJuYW1lIjoiQnJ1bm8iLCJpYXQiOjE2Njg3OTY3MjcsImV4cCI6MTY2ODg4MzEyN30.5XJAjn5cDxEyLoIosYPL-J37jPw8810aVJb89WRE3oM"
 * }
 */
exports.login = async (req, res) => {
    const{ email, password } = req.body;
    if(!email || !password){
        return res.json({
            message: 'all fields are required',
            status: 400
        })
    }

    const user = await User.findOne({ email }).lean();
    if(!user){
        return res.json({
            message: 'user not found',
            status: 404
        })
    }
    if(await bcrypt.compare(password, user.password)){
        try{
            
            User.updateOne({ email }, { DataAcesso: Date.now() });

            const token = jwt.sign({
                id: user._id,
                email: user.email
            }, process.env.TOKEN_SECRET, { expiresIn: '24h' });
            return res.json({
                message: 'login success',
                status: 200,
                token
            })
        }catch(err){ 
            console.log(err);
            return res.json({
                message: 'login error',
                status: 400
            })
        }
    }else{
        return res.json({
            message: 'invalid credentials',
            status: 400
        })
    }
}
/**
 * @api {post} /user/register registar
 * @apiVersion 1.0.0
 * @apiName Register
 * @apiGroup Autentication
 * @apiParam {String} email Utilizador.
 * @apiParam {String} password Password.
 * @apiParam {String} email Email.
 * @apiSuccessExample {Json} Sucesso
 *  HTTP/1.1 200 ok 
 * {
    "status": "200",
    "message": "register"
 * }
 */
exports.register = async (req, res) => {
    console.log(req.body);
    //criar um novo user
    const { primeironome,ultimonome,dataNasc, email, password } = req.body;
    // fields treatment
    if(!primeironome ||!ultimonome || !dataNasc || !email || !password){
        return res.json({
            message: 'all fields are required',
            status: 400
        })
    }
    if(password.length < 6){
        return res.json({
            message: 'password must be at least 6 characters',
            status: 400
        })
    }
    if(!email.includes('@')){
        return res.json({
            message: 'invalid email',
            status: 400
        })
    }

    // encriptar as senhas
    const passwordEncrypted = await bcrypt.hash(password, 10); // number of times to encrypt
    console.log(passwordEncrypted);
    // kitchen password
    // salt, papper, oil -> food list
    // funcon especial password
    // colisao de senhas
    // algoritm of autentication slow for security questions
    // hash the password
    // save user on database
    try{
        const newUser = await User.create({                      
            email,
            primeironome: primeironome,
            ultimonome: ultimonome,
            DataNascimento: dataNasc,
            DataRegisto: Date.now(),
            Ativo: true,
            DataAcesso: undefined,            
            password: passwordEncrypted
        });
        console.log(newUser);
    }catch(err){
        console.log(err);
        if(err.code === 11000){
            // duplicate key
            return res.json({
                message: 'user already exists',
                status: 400
            })
        }
        return res.json({
            message: 'register error',
            status: 400
        })
        
    }
    res.json({
        message: 'register',
        status: 200
    })
}



/**
 * @api {post} /user/delete delete
 * @apiVersion 1.0.0
 * @apiName delete
 * @apiGroup Autentication
 * @apiParam {String} username Utilizador.
 * @apiParam {String} password Password.
 * @apiSuccessExample {Json} Sucesso
 *  HTTP/1.1 200 ok 
 * {
    "status": "200",
    "message": "user deleted"
 * }
 */
    exports.delete = async (req, res) => {
        const{ email, password } = req.body;
        if(!email || !password){
            return res.json({
                message: 'all fields are required',
                status: 400
            })
        }
    
        const user = await User.findOne({ email }).lean();
        if(!user){
            return res.json({
                message: 'user not found',
                status: 404
            })
        }
        if(await bcrypt.compare(password, user.password)){
            try{
                
                await User.deleteOne({ email }).lean();
                return res.json({
                    message: 'user deleted',
                    status: 200
                })
            }catch(err){ 
                console.log(err);
                return res.json({
                    message: 'login error',
                    status: 400
                })
            }
        }else{
            return res.json({
                message: 'invalid credentials',
                status: 400
            })
        }
    }



    
/**
 * @api {post} /user/update update
 * @apiVersion 1.0.0
 * @apiName update
 * @apiGroup Autentication
 * @apiParam {String} password Password.
 * @apiParam {String} email Email.
 * @apiParam {String} primeironome Primeiro Nome.
 * @apiParam {String} ultimonome Ultimo Nome.
 * @apiParam {String} dataNasc Data de Nascimento.
 * @apiParam {String} dataRegisto Data de Registo.
 * @apiParam {String} ativo Ativo.
 * 
 * @apiSuccessExample {Json} Sucesso
 *  HTTP/1.1 200 ok 
 * {
    "status": "200",
    "message": "user deleted"
 * }
 */
    exports.update = async (req, res) => {
        const { primeironome,ultimonome,dataNasc, email, password } = req.body;

        if(!primeironome ||!ultimonome || !dataNasc || !email || !password){
            return res.json({
                message: 'all fields are required',
                status: 400
            })
        }
    
        const user = await User.findOne({ email }).lean();
        if(!user){
            return res.json({
                message: 'user not found',
                status: 404
            })
        }
        if(await bcrypt.compare(password, user.password)){
            try{
                
                await User.updateOne({ email }, { primeironome: primeironome, ultimonome: ultimonome, DataNascimento: dataNasc });
                return res.json({
                    message: 'user atualizado',
                    status: 200
                })
            }catch(err){ 
                console.log(err);
                return res.json({
                    message: 'login error',
                    status: 400
                })
            }
        }else{
            return res.json({
                message: 'invalid credentials',
                status: 400
            })
        }
    }