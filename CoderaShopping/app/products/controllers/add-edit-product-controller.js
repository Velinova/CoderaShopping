app.controller("AddEditProductsController", [
    "$scope", "$uibModalInstance", "ProductsService", "ManifacturersService", "productId", "CategoriesService",
    function ($scope, $uibModalInstance, ProductsService, ManifacturersService, productId, CategoriesService) {
        $scope.submitted = false;
        $scope.categories = [];
        $scope.defaultCategory = {};
        $scope.categoryEmpty = false;
        $scope.manifacturers = [];
        $scope.manifacturerEmpty = false;
        CategoriesService.getDefaultCategory()
            .then(function (response) {
                $scope.defaultCategory = response.data;
                $scope.model.Category = response.data;
            }, function (response) {
                alert(response.data);
            });
        CategoriesService.getActiveCategories()
            .then(function (response) {
                $scope.categories = response.data;
            }, function (response) {
                    alert(response.data);
            });
        ManifacturersService.getAll().then(
            function (response) {
                $scope.manifacturers = response.data;
            },
            function (response) {
                alert(response.data);
            }
        );
        // if it is create
        $scope.model = {
            Name: "",
            Description: "",
            Quantity: "",
            Price: "",
            Category: null,
            Manifacturer: null
        };

        // if it is update fetch data to be updated
        if (productId) {
            ProductsService.getProductById(productId).then(
                function (response) {
                    $scope.model = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        $scope.submit = function (form) {
            $scope.categoryEmpty = false;
            $scope.manifacturerEmpty = false;
            $scope.submitted = true;
            if ($scope.model.Category === null) {
                $scope.categoryEmpty = true;
            }
            if ($scope.model.Manifacturer === null) {
                $scope.manifacturerEmpty = true;
            }

            if (form.$invalid || $scope.categoryEmpty || $scope.manifacturerEmpty) {
                return;
            }

            // update
            if (productId) {
                ProductsService.update($scope.model).then(
                    function (response) {
                        $uibModalInstance.close(true);
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            // create
            else {
                ProductsService.create($scope.model).then(
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