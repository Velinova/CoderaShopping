app.factory("CategoriesService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var categoriesService = {};

        categoriesService.create = function (model) {
            return $http.post(serverUrl + "Categories/Create", model);
        };

        categoriesService.getCategoryById = function (id) {
            return $http.post(serverUrl + "Categories/GetById", { id: id });
        };

        categoriesService.update = function (model) {
            return $http.post(serverUrl + "Categories/Update", model);
        };

        categoriesService.delete = function (id) {
            return $http.post(serverUrl + "Categories/Delete", { id: id });
        };

        categoriesService.getAllCategories = function () {
            return $http.post(serverUrl + "Categories/GetAll", null);
        };
        categoriesService.getDefaultCategory = function () {
            return $http.post(serverUrl + "Categories/GetDefault", null);
        };

        categoriesService.getActiveCategories = function () {
            return $http.post(serverUrl + "Categories/GetActive", null)
        };

        categoriesService.getProductsById = function (id) {
            return $http.post(serverUrl + "Categories/GetProductsById", { id: id })
        };
        return categoriesService;
    }
]);