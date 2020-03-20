app.controller("OrderDetailsModalController", [
    "$scope", "$uibModalInstance", "orderId","OrdersService",
    function ($scope, $uibModalInstance, orderId, OrdersService) {
        $scope.orderItems = [];
        //get items
        OrdersService.getOrderItems(orderId).then(
            function (response) {
                $scope.orderItems = response.data;
            },
            function (response) {
                alert(response.data);
            }
        );

        //close details modal
        $scope.cancel = function () {
            $uibModalInstance.close(false);
        }
    }
]);