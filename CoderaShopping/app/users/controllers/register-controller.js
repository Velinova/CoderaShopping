app.controller("RegisterController", [
    "$scope", "$state", "SignInRegisterService", "$cookies", "$rootScope",
    function ($scope, $state, SignInRegisterService, $cookies, $rootScope) {
        //form is not submitted
        $scope.submitted = false;
        $scope.passwordsMatch = true;

        //model
        $scope.model = {
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

        //signin function
        $scope.register = function (form) {
            $scope.submitted = true;

            if (form.$invalid || $scope.passwordsMatch == false) {
                return;
            }

            SignInRegisterService.register($scope.model).then(
                function (response) {
                    SignInRegisterService.signIn({ Email: $scope.model.Email, Password: $scope.model.Password });
                    $rootScope.$broadcast('cookie-data', { Id: response.data });
                    $state.go("home");
                },
                function () {
                    $scope.hasError = true;
                });
        }

    }
]);