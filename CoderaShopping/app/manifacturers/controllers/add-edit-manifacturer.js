app.controller("AddEditManifacturerController", [
    "$scope", "$uibModalInstance", "ManifacturersService", "manifacturerId",
    function ($scope, $uibModalInstance, ManifacturersService, manifacturerId) {
        $scope.submitted = false;

        $scope.model = {
            Name: "", 
            City: "",
            Country: "",
            Address: ""
        };

        if (manifacturerId) {
            ManifacturersService.getById(manifacturerId).then(
                function (response) {
                    $scope.model = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        $scope.submit = function (form) {
            $scope.submitted = true;

            if (form.$invalid) {
                return;
            }

            // update mode
            if (manifacturerId) {
                ManifacturersService.update($scope.model).then(
                    function (response) {
                        $uibModalInstance.close(true);
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            // create mode
            else {
                ManifacturersService.create($scope.model).then(
                    function (response) {
                        $uibModalInstance.close(true);
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
        }

        $scope.close = function () {
            $uibModalInstance.dismiss(false);
        }
    }
]);