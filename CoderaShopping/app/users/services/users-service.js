app.factory("UsersService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var usersService = {};

        usersService.registerUser = function (model) {
            return $http.post(serverUrl + "Users/RegisterUser", model);
        };

        usersService.signInUser = function (model) {
            return $http.post(serverUrl + "Users/SignInUser", model);
        };

        usersService.getAllAdmins = function () {
            return $http.post(serverUrl + "Users/GetAllAdmins", null);
        };

        usersService.getAllUsers = function () {
            return $http.post(serverUrl + "Users/GetAllUsers", null);
        };

        usersService.getAll = function () {
            return $http.post(serverUrl + "Users/GetAll", null);
        };

        usersService.delete = function (id) {
            return $http.post(serverUrl + "Users/Delete", {id: id});
        };

        usersService.update = function (model) {
            return $http.post(serverUrl + "Users/Update", model)
        }
        usersService.getById = function (id) {
            return $http.post(serverUrl + "Users/GetById", {id: id})
        }
        usersService.changeRole = function (id) {
            return $http.post(serverUrl + "Users/ChangeRole", {id :id})
        }
        usersService.getOrdersById = function (id) {
            return $http.post(serverUrl + "Users/GetOrdersById", { id: id })
        }
        return usersService;
    }
]);