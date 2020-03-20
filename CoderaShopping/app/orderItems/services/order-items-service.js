app.factory("OrderItemsService", [
    "$http", "serverUrl",
    function ($http, serverUrl) {
        var orderItemsService = {};

        orderItemsService.add = function (model) {
            return $http.post(serverUrl + "OrderItems/Create", model);
        };

       
        return orderItemsService;
    }
]);