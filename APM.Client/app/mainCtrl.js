(function() {

    'use strict';

    angular
        .module('productManagement')
        .controller('MainCtrl', ['userAccount', MainCtrl]);

    function MainCtrl(userAccount) {

        var vm = this;

        vm.isLoggedIn = false;

        vm.message = '';

        vm.userData = {
            userName: '',
            email: '',
            password: '',
            confirmPassword: ''
        };

        vm.registerUser = function () {

            vm.userData.confirmPassword = vm.userData.password;

            userAccount.registration.registerUser(vm.userData,
                function(data) {
                    vm.confirmPassword = '';
                    vm.message = '... Registration successful';
                    console.log('data: ' + data);
                    vm.login();
                },
                function(response) {
                    vm.isLoggedIn = false;

                    vm.message = response.statusText + '\r\n';

                    if (response.data && response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                    }

                    // Validation errors.
                    if (response.data && response.data.modelState) {
                        for (var key in response.data.modelState) {
                            vm.message += response.data.modelState[key] + '\r\n';
                        }
                    }

                    if (response && !response.data) {
                        vm.message = '... Registration failed. response: ' + response;
                    }
                });
        }

        vm.login = function() {

            console.log('in login');
            vm.userData.grant_type = 'password';
            vm.userData.userName = vm.userData.email;

            userAccount.login.loginUser(vm.userData,
                function(data) {
                    vm.isLoggedIn = true;
                    vm.message = '';
                    vm.password = '';
                    vm.token = data.access_token;
                },
                function(response) {
                    vm.password = '';
                    vm.isLoggedIn = false;
                    vm.message = response.statusText + '\r\n';

                    if (response.data && response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                    }

                    if (response.data && response.data.error) {
                        vm.message += response.data.error;
                    }
                });
        }
    }

})();