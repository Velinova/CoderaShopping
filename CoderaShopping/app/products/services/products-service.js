app.factory("ProductsService", ["$http", "serverUrl",
    function ($http, serverUrl) {
        var productsService = {};

        productsService.create = function (model) {
            return $http.post(serverUrl + "/Products/Create", model);
        };

        productsService.update = function (model) {
            return $http.post(serverUrl + "/Products/Update", model);
        };

        productsService.getProductById = function (id) {
            return $http.post(serverUrl + "/Products/GetById", { id: id });
        };

        productsService.delete = function (id) {
            return $http.post(serverUrl + "/Products/Delete", { id: id });
        };

        productsService.getAllProducts = function () {
            return $http.post(serverUrl + "/Products/GetAll", null);
        };

        productsService.getProductsInStock = function(){
            return $http.post(serverUrl + "/Products/GetProductInStock", null);
        };

        productsService.getCategories = function () {
            return $http.post(serverUrl + "/Categories/GetAll" , null);
        };

        return productsService;
    }
]);