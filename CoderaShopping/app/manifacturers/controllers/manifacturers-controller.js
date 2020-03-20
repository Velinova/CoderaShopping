app.controller("ManifacturersController", [
    "$scope", "ManifacturersService", "$uibModal",
    function ($scope, ManifacturersService, $uibModal) {
        $scope.manifacturers = [];
        
        fetchData();

        $scope.summonCreateDialog = function () {
            var modalInstance = $uibModal.open(
                {
                    controller: "AddEditManifacturerController",
                    templateUrl: "app/manifacturers/add-edit-manifacturer.html",
                    resolve: {
                        manifacturerId: function () {
                            return undefined;
                        }
                    }
                });

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        $scope.summonEditDialog = function (manifacturerId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "AddEditManifacturerController",
                    templateUrl: "app/manifacturers/add-edit-manifacturer.html",
                    resolve: {
                        manifacturerId: function () {
                            return manifacturerId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        $scope.summonDeleteDialog = function (manifacturerId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "DeleteModalController",
                    templateUrl: "app/common/delete-modal/delete-modal.html",
                    resolve: {
                        service: function () {
                            return ManifacturersService.delete;
                        },
                        params: function () {
                            return manifacturerId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        function fetchData() {
            ManifacturersService.getAll().then(
                // success
                function (response) {
                    $scope.manifacturers = response.data;
                },
                // error
                function (response) {
                    alert(response.data);
                }
            );
        }    
    }
]);