app.controller("MyOrdersController", [
    "$scope", "OrdersService", "RatingsService", "$cookies", "$uibModal" ,"$window",
    function ($scope, OrdersService, RatingsService, $cookies, $uibModal, $window) {
        $scope.myorders = [];
        $scope.userId = $cookies.get('userId');
        $scope.dictionary = {};
        $scope.ratingPanelShow = false;
        $scope.model = {};
        $scope.ratingObjectId = null;
        //fetch data
        $scope.fetchData = function (parameter) {
            OrdersService.getOrdersAndItemsByUser($scope.userId, parameter).then(
                function (response) {
                    $scope.myorders = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }
        // check stock 
        function checkOutOfStock(order) {
            var outOfStock = false;
            for (var item in order.Items) {
                if (order.Items[item].Quantity > order.Items[item].Product.Quantity) {
                    outOfStock = true;
                    break;
                }
            }
                var modalIstance = $uibModal.open(
                    {
                        controller: "RepeatOrderModalController",
                        templateUrl: "app/orders/repeat-order-modal.html",
                        resolve: {
                            outOfStock: function () {
                                return outOfStock;
                            }
                        }
                    });
                modalIstance.result.then(function () { }
                );
            return outOfStock;
        }
        //repeat order
        $scope.repeatOrder = function (order) {
            var result = checkOutOfStock(order);
            if (!result) {
                var orderItems = $cookies.getObject("orderItems");
                for (var item in order.Items) {
                    var model = {
                        Product: order.Items[item].Product,
                        Quantity: order.Items[item].Quantity
                    }

                    var keys = Object.keys(orderItems);
                    var added = false;
                    for (var key in keys) {
                        if (orderItems[key].Product.Name == model.Product.Name) {
                            orderItems[key].Quantity += model.Quantity;
                            added = true;
                        }
                    }
                    if (!added) {
                        orderItems.push(model);
                    }
                }
                $cookies.putObject("orderItems", orderItems);
            }
        }
        //show rating panel
        $scope.showRatingPanel = function (orderId) {
            $scope.ratingPanelShow = true;
            $scope.ratingObjectId = orderId;
            //check if rating exists
            RatingsService.checkRatingUserObject($scope.userId, orderId).then(
                function (response) {
                    $scope.model = {
                        Id: response.data.Id,
                        UserId: $scope.userId,
                        ObjectId: orderId,
                        ObjectType: 0,
                        Value: response.data.Value,
                        Comment: response.data.Comment,
                        ShowName: response.data.ShowName
                    }
                },
                function (response) {
                    alert(response.data);
                }
            );
        }
        //submit rating & review
        $scope.addReview = function () {
            RatingsService.submitRating($scope.model).then(
                function () {
                    $scope.ratingPanelShow = false;
                    $scope.ratingObjectId = null;
                },
                function (response) {
                    alert(response.data);
                }
            );

        }
        //dispute order 
        $scope.disputeOrder = function (order) {
            var modalIstance = $uibModal.open(
                {
                    controller: "ConfirmModalController",
                    templateUrl: "app/orders/confirm-modal.html",
                    backdrop: false,
                    resolve: {
                        message: function () {
                            return "Disputed";
                        }
                    }
                });
            modalIstance.result.then(function () {
                var model = {
                    UserId: order.User.Id,
                    OrderId: order.Id,
                    Status: "Disputed"
                }
                OrdersService.update(model).then(
                    function () {
                        $window.location.reload();
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            );
        }
        //cancel order
        $scope.cancelOrder = function(order){
            var modalIstance = $uibModal.open(
                {
                    controller: "ConfirmModalController",
                    templateUrl: "app/orders/confirm-modal.html",
                    backdrop: false,
                    resolve: {
                        message: function () {
                            return "Canceled";
                        }
                    }
                });
            modalIstance.result.then(function () {
                var model = {
                    UserId: order.User.Id,
                    OrderId: order.Id,
                    Status: "Canceled"
                }
                OrdersService.update(model).then(
                    function () {
                        $window.location.reload();
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            );
        }
        //confirm delivery
        $scope.confirmOrder = function(order){
            var modalIstance = $uibModal.open(
                {
                    controller: "ConfirmModalController",
                    templateUrl: "app/orders/confirm-modal.html",
                    backdrop: false,
                    resolve: {
                        message: function () {
                            return "Delivered";
                        }
                    }
                });
            modalIstance.result.then(function () {
                var model = {
                    UserId: order.User.Id,
                    OrderId: order.Id,
                    Status: "Delivered"
                }
                OrdersService.update(model).then(
                    function () {
                        $window.location.reload();
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
            }
            );
        }
        //all
        $scope.all = function () {
            $scope.fetchData(0);
        }
        //newest
        $scope.newest = function () {
            $scope.fetchData(1);
        }
        //oldest
        $scope.oldest = function () {
            $scope.fetchData(2);
        }
        //Delivered
        $scope.delivered = function () {
            $scope.fetchData(3);
        }
        //Paid
        $scope.paid = function () {
            $scope.fetchData(4);
        }
        //Shipped
        $scope.shipped = function () {
            $scope.fetchData(5);
        }
        //frozen
        $scope.frozen = function () {
            $scope.fetchData(6);
        }
        //Disputed
        $scope.disputed = function () {
            $scope.fetchData(7);
        }
        //Canceled
        $scope.canceled = function () {
            $scope.fetchData(8);
        }
        // init
        $scope.fetchData(0);
    }
]);