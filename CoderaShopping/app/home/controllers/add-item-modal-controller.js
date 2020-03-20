app.controller("AddItemModalController", [
    "$scope", "$uibModalInstance", "product",
    function ($scope, $uibModalInstance, product ) {
        $scope.quantity = 1;
        if (product.Quantity > 0) {
            $scope.canOrder = true;
        }
        else {
            $scope.canOrder = false;
        }
        
        $scope.increase = function (quantity) {
            $scope.quantity = quantity + 1;
        }
        $scope.decrease = function (quantity) {
            $scope.quantity = quantity - 1;
        }
        $scope.product = product;
        
        $scope.add = function () {
            $scope.item = {
                Product: $scope.product,
                Quantity: $scope.quantity
            }
            $scope.product = {};
            $uibModalInstance.close($scope.item);
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss(false);
        }
        //$scope.cancel = function () {
        //    $scope.product = {};
        //    $uibModalInstance.dismiss(false);
        //}
    }
]);