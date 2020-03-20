
//filter startFrom
app.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});
app.controller("HomeController", [
    "$cookies", "$rootScope", "$scope", "CategoriesService", "$uibModal", "ProductsService",
    function ($cookies, $rootScope, $scope, CategoriesService, $uibModal, ProductsService) {
        $scope.searchText = "";
        $scope.searchCriteria = "";
        $scope.listedProducts = [];
        $scope.ascdesc = true;
        $scope.currentPage = 0;
        $scope.pageSize = 8;
        $scope.showCarousel = true;
        //initializing order items
        if ($cookies.getObject('orderItems') === undefined) {
            $cookies.putObject('orderItems', []);
            $scope.orderItems = $cookies.getObject('orderItems');
        }
        else {
            $scope.orderItems = $cookies.getObject('orderItems');
        }
        //ProductsService.getAllProducts().then(
        //    function (response) {
        //        $scope.listedProducts = response.data;
        //    },
        //    function (response) {
        //        alert(response.data);
        //    }
        //);
        //dynamic order by
        $scope.dynamicOrderBy = function (criteria, flag) {
            if (criteria == "price" && flag == true) {
                $scope.searchCriteria = "Price";
                $scope.ascdes = true;
            }
            if (criteria == "name" && flag == true) {
                $scope.searchCriteria = "Name";
                $scope.ascdes = true;
            }
            if (criteria == "price" && flag == false) {
                $scope.searchCriteria = "Price";
                $scope.ascdes = false;
            }
            if (criteria == "name" && flag == false) {
                $scope.searchCriteria = "Name";
                $scope.ascdes = false;
            }
        };

        //clear search
        $scope.clear = function () {
            $scope.searchText = "";
        }

        //emmiting cookie event
        var userId = $cookies.get("userId");
        if (userId) {
            $rootScope.$emit("cookie-data", { Id: userId });
        }

        //getting product from selected category
        $scope.getProducts = function (id) {
            CategoriesService.getProductsById(id).then(
                function (response) {
                    $scope.listedProducts = response.data;
                    $scope.showCarousel = false;
                    $scope.category = $scope.listedProducts[1].category;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        //calculating number of pages
        $scope.numberOfPages = function () {
            return Math.ceil($scope.listedProducts.length / $scope.pageSize);
        }

        //open add order item modal
        $scope.openAddItem = function (id) {
            $scope.product = {};
            ProductsService.getProductById(id).then(
                function (response) {
                    $scope.product = response.data;
                    var modalInstance = $uibModal.open(
                        {
                            controller: "AddItemModalController",
                            templateUrl: "app/home/add-item-modal.html",
                            backdrop: false,
                            resolve: {
                                product: function () {
                                    return $scope.product;
                                }
                            }
                        });

                    modalInstance.result.then(
                        function (item) {
                            $scope.orderItems = $cookies.getObject("orderItems");
                            var keys = Object.keys($scope.orderItems);
                            var added = false;
                            for (var key in keys) {
                                if ($scope.orderItems[key].Product.Name == item.Product.Name) {
                                    $scope.orderItems[key].Quantity += item.Quantity;
                                    added = true;
                                }
                                $cookies.putObject('orderItems', $scope.orderItems);
                            }
                            if (!added) {
                                $scope.orderItems.push(item);
                                $cookies.putObject('orderItems', $scope.orderItems);
                            }
                        },
                        function (response) {
                            //alert(response.data);
                        }
                    );
                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        //open my cart modal
        $scope.openMyCart = function () {
            var modalInstance = $uibModal.open(
                {
                    controller: "MyCartModalController",
                    templateUrl: "app/home/my-cart-modal.html",
                    resolve: {
                        orderItems: function () {
                            return $cookies.getObject("orderItems");
                        }
                    }
                });

            modalInstance.result.then(
                function (result) {
                    if (result == true) {
                        $scope.orderItems = [];
                    }
                    else {
                        $scope.orderItems = result;
                    }
                }
            );
        }

        //open details modal 
        $scope.openDetailsModal = function (product) {
            var modalInstance = $uibModal.open(
                {
                    controller: "DetailsModalController",
                    templateUrl: "app/products/details-modal.html",
                    backdrop: false,
                    resolve: {
                        product: function () {
                            return product;
                        }
                    }
                });
            modalInstance.result.then(
                function (result) {
                },
                function (result) {

                }
            );    
        }

    }
]);