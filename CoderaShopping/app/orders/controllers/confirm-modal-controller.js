app.controller("ConfirmModalController", [
    "$scope", "$uibModalInstance", "message",
    function ($scope, $uibModalInstance, message) {
        $scope.message = null;
        $scope.status = message;
        switch (message) {
            case "Delivered":
                $scope.message = "Do you want to change order status to delievered? This means that you recieved your order successfully.";
                break;
            case "Canceled":
                $scope.message = "Do you want to change order status to canceled?";
                break;
            case "Disputed":
                $scope.message = "Do you want to change order status to disputed?";
                break;

        }
        $scope.submit = function () {
            $uibModalInstance.close(true);
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss(false);
        }
    }
]);