app.controller("CategoriesController", [
    "$scope", "CategoriesService", "$uibModal",
    function ($scope, CategoriesService, $uibModal) {
        $scope.categories = [];
        
        fetchData();

        $scope.summonCreateDialog = function () {
            var modalInstance = $uibModal.open(
                {
                    controller: "AddEditCategoryController",
                    templateUrl: "app/categories/add-edit-category.html",
                    resolve: {
                        categoryId: function () {
                            return undefined;
                        }
                    }
                });

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        $scope.summonEditDialog = function (categoryId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "AddEditCategoryController",
                    templateUrl: "app/categories/add-edit-category.html",
                    resolve: {
                        categoryId: function () {
                            return categoryId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        $scope.summonDeleteDialog = function (categoryId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "DeleteModalController",
                    templateUrl: "app/common/delete-modal/delete-modal.html",
                    resolve: {
                        service: function () {
                            return CategoriesService.delete;
                        },
                        params: function () {
                            return categoryId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        function fetchData() {
            CategoriesService.getAllCategories().then(
                // success
                function (response) {
                    $scope.categories = response.data;
                },
                // error
                function (response) {
                    alert(response.data);
                }
            );
        }    
    }
]);