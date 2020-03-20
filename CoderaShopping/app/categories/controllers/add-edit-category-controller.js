app.controller("AddEditCategoryController", [
    "$scope", "$uibModalInstance", "CategoriesService", "categoryId",
    function ($scope, $uibModalInstance, CategoriesService, categoryId) {
        $scope.submitted = false;

        $scope.model = {
            Name: "",
            Status: true, 
            IsDefault: false
        };

        if (categoryId) {
            CategoriesService.getCategoryById(categoryId).then(
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
            if (categoryId) {
                CategoriesService.update($scope.model).then(
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
                CategoriesService.create($scope.model).then(
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