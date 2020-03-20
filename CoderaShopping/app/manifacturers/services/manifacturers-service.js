app.factory("ManifacturersService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var manifacturersService = {};

        manifacturersService.getAll = function (model) {
            return $http.post(serverUrl + "Manifacturers/GetAll", model);
        };
        manifacturersService.create = function (model) {
            return $http.post(serverUrl + "Manifacturers/Create", model);
        };
        manifacturersService.update = function (model) {
            return $http.post(serverUrl + "Manifacturers/Update", model);
        };
        manifacturersService.getById = function (id) {
            return $http.post(serverUrl + "Manifacturers/GetById", {id:id});
        };
        manifacturersService.delete = function (id) {
            return $http.post(serverUrl + "Manifacturers/Delete", { id: id });
        };
        return manifacturersService;
    }
]);