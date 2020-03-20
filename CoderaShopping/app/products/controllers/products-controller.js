app.controller("ProductsController", [
    "$scope", "$uibModal", "ProductsService","CategoriesService",
    function ($scope, $uibModal, ProductsService, CategoriesService) {
        //list all products 
        $scope.listAllProducts = function () {
            $scope.selectedCategory = null;
            $scope.fetchData();
        }


        //fuction fetch data
        $scope.fetchData = function () {
            if ($scope.selectedCategory == null) {
                ProductsService.getAllProducts().then(
                    // success
                    function (response) {
                        $scope.products = response.data;
                    },
                    // error
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            else {
                CategoriesService.getProductsById($scope.selectedCategory.Id).then(
                    function (response) {
                        $scope.products = response.data;
                    },
                    // error
                    function (response) {
                        alert(response.data);
                    }
                );
            }
        }

        //scope variables
        $scope.products = [];
        $scope.selectedCategory = null;
        $scope.categories = [];
        $scope.fetchData();

        //get categories
        CategoriesService.getActiveCategories().then(
            function (response) {
                $scope.categories = response.data;
            },
            function (response) {
                alert(response.data);
            }
        );



        //summon create dialog
        $scope.summonCreateDialog = function () {
            var modalInstance = $uibModal.open(
                {
                    controller: "AddEditProductsController",
                    templateUrl: "app/products/add-edit-product.html",
                    resolve: {
                        productId: function () {
                            return undefined;
                        }
                    }
                });
            // after executing fetch data for updating grid
            modalInstance.result.then(function (result) {
                $scope.fetchData();
            });
        }


        //summon edit dialog
        $scope.summonEditDialog = function (productId) {
            var modalIstance = $uibModal.open(
                {
                    controller: "AddEditProductsController",
                    templateUrl: "app/products/add-edit-product.html",
                    resolve: {
                        productId: function () {
                            return productId;
                        }
                    }
                });
            // after executing fetch data for updating grid
            modalIstance.result.then(function (result) {
                $scope.fetchData();
            });
        }

        //summon delete dialog
        $scope.summonDeleteDialog = function (productId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "DeleteModalController",
                    templateUrl: "app/common/delete-modal/delete-modal.html",
                    resolve: {
                        service: function () {
                            return ProductsService.delete;
                        },
                        params: function () {
                            return productId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }
        
    }
]);