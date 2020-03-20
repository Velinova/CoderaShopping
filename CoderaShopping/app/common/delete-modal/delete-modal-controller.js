app.controller("DeleteModalController", [
    "$scope", "$uibModalInstance", "service", "params",
    function ($scope, $uibModalInstance, service, params) {
        $scope.submit = function () {
            service(params).then(
                function (response) {
                    $uibModalInstance.close(response.data);
                },
                function (response) {
                    console.log(response);
                    alert(response.data);
                });
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss(false);
        }
    }
]);