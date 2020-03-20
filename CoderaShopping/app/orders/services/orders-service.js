app.factory("OrdersService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var ordersService = {};

        ordersService.pay = function (model, userId) {
            return $http.post(serverUrl + "Orders/Pay", {model: model, userId : userId});
        };
        ordersService.getAllOrders = function () {
            return $http.post(serverUrl + "Orders/GetAll", null );
        };
        ordersService.getOrderById = function (id) {
            return $http.post(serverUrl + "Orders/GetById", {id:id});
        };
        ordersService.update = function (model) {
            return $http.post(serverUrl + "Orders/Update", model);
        };
        ordersService.getOrderItems = function (id) {
            return $http.post(serverUrl + "Orders/GetOrderItems", {id: id});
        };
        ordersService.getOrdersAndItemsByUser = function (userId, parameter) {
            return $http.post(serverUrl + "Orders/GetOrdersandItemsByUser", { userId: userId, parameter: parameter });
        };
        ordersService.getOrdersCountByMonth = function (month) {
            return $http.post(serverUrl + "Orders/GetOrdersCountByMonth", { month:month });
        };
        return ordersService;
    }
]);