const router = require('express').Router();
const userController = require('../controllers/user.Controller');

router.route('/').post(userController.register);
router.route('/login').post(userController.login);
router.route('/register').post(userController.register);
router.route('/delete').post(userController.delete);
router.route('/update').put(userController.update);




router.route('/changepassword').post(userController.changePassword);
module.exports = router;