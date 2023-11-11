'use strict';
//database schema
const mongoose = require('mongoose');
const schema = mongoose.Schema;
const userSchema = new schema(
    {
        primeironome: {
        type: String,
        required: true,
        unique: true //username must be unique
    },
        ultimonome: {
        type: String,
        required: true,
        unique: true //username must be unique
    },
        DataNascimento: {
        type: Date,
        required: true,
        },

        DataRegisto: {
            type: Date,
            },
            Ativo: {
                type: Boolean,
                },

                DataAcesso: {
                    type: Date,
                    },           
    email: {
        type: String,
        required: true
    },
    password: {
        type: String,
        required: true
    }},
    {collection: 'users'}
)
const User = mongoose.model('User', userSchema);

module.exports = User