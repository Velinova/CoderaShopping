app.factory("SignInRegisterService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var signInRegisterService = {};

        signInRegisterService.signIn = function (model) {
            return $http.post(serverUrl + "Users/SignInUser", model);
        };
        signInRegisterService.getRole = function (id) {
            return $http.post(serverUrl + "Users/GetRole", { id: id });
        };
        signInRegisterService.register = function (model) {
            return $http.post(serverUrl + "Users/RegisterUser", model);
        };
        return signInRegisterService;
    }
]);