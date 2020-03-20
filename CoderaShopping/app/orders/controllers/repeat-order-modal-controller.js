app.controller("RepeatOrderModalController", [
    "$scope", "$uibModalInstance", "outOfStock",
    function ($scope, $uibModalInstance, outOfStock) {
        $scope.message = null;
        $scope.headerText = null;
        if (outOfStock) {
            $scope.message = "You can't repeat this order because some of the products are out of stock."
            $scope.headerText = "Info"
        }
        else {
            $scope.message = "Order items successfully added in your shopping cart."
            $scope.headerText = "Info"
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss(false);
        }
    }
]);