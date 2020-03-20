app.controller("SignInController", [
    "$scope", "$state", "SignInRegisterService", "$cookies", "$rootScope",
    function ($scope, $state, SignInRegisterService, $cookies, $rootScope) {
        //form is not submitted
        $scope.submitted = false;
        $scope.show = false;

        //show register form
        $scope.showRegister = function () {
            $scope.show = true;
        }

        //model
        $scope.signInModel = {
            Email: "",
            Password: ""
        };

        //signin function
        $scope.signIn = function (form) {
            $scope.submitted = true;
            $scope.hasError = false;

            if (form.$invalid) {
                return;
            }

            SignInRegisterService.signIn($scope.signInModel).then(
                function (response) {
                    $state.go('home');
                    $rootScope.$broadcast('cookie-data', { Id: response.data });
                },
                function () {
                    $scope.hasError = true;
                });
        }
        //form is not submitted
        $scope.submitted = false;
        $scope.passwordsMatch = true;

        //model
        $scope.registerModel = {
            UserName: "",
            Email: "",
            Password: "",
            ConfirmedPassword: "",
            Name: "",
            Surname: "",
            City: "",
            Country: "",
            Address: "",
            PostalCode: ""
        };

        $scope.$watchGroup(['model.Password', 'model.ConfirmedPassword'], function (newValues) {
            if ($scope.submitted) {
                if (newValues[0] === newValues[1]) {
                    $scope.passwordsMatch = true;
                } else {
                    $scope.passwordsMatch = false;
                }
            }
        })

        //register function
        $scope.register = function (form) {
            $scope.submitted = true;

            if (form.$invalid || $scope.passwordsMatch == false) {
                return;
            }

            SignInRegisterService.register($scope.registerModel).then(
                function (response) {
                    SignInRegisterService.signIn({ Email: $scope.registerModel.Email, Password: $scope.registerModel.Password });
                    $rootScope.$broadcast('cookie-data', { Id: response.data });
                    $state.go("home");
                },
                function () {
                    $scope.hasError = true;
                });
        }

    }
    

]);