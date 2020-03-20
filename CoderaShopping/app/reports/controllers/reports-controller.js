app.controller("ReportsController", [
    "$scope", "OrdersService",
    function ($scope, OrdersService) {
        $scope.ordersThisMonth = 0;
        $scope.topManufacturers = [];
        var date = new Date();
        OrdersService.getOrdersCountByMonth(date.getMonth()).then(
            function (response) {
                $scope.ordersThisMonth = response.data;
            },
            function (response) {
                alert(response.data);
            }
        );
        ManufacturersService.getTopFiveManufacturers().then(
            function (response) {
                $scope.topManufacturers = response.data;
            },
            function (response) {

            }
        );
        

        
    }
]);