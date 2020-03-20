app.controller("OrdersController", [
    "$scope", "UsersService", "OrdersService", "$uibModal",
    function ($scope, UsersService, OrdersService, $uibModal) {
        //scope variables
        $scope.orders = [];
        $scope.selectedUser = null;
        $scope.selectedStatus = null;
        $scope.users = [];
        $scope.orderToUpdate = null; // updated model
        $scope.orderItems = [];
        $scope.disableAll = false;

        //open details modal
        $scope.openOrderDetails = function (orderId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "OrderDetailsModalController",
                    templateUrl: "app/orders/order-details-modal.html",
                    resolve: {
                        orderId: function () {
                            return orderId;
                        }
                    }
                });

            modalInstance.result.then(function (result) {
                return undefined;
            });

        }
        
        //};
        //show order items
        $scope.showOrderItems = function (order) {
            OrdersService.getOrderItems(order.Id).then(
                function (response) {
                    $scope.orderItems = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }
        $scope.statusDictionary = [{ key: 0, value: "Paid" }, { key: 1, value: "Shipped" }, { key: 2, value: "Delivered" }, { key: 3, value: "Frozen" }, { key: 4, value: "Canceled" }, { key: 5, value: "Disputed" }];
        //fuction fetch data
        $scope.fetchData = function () {
                OrdersService.getAllOrders().then(
                    // success
                    function (response) {
                        $scope.orders = response.data;
                    },
                    // error
                    function (response) {
                        alert(response.data);
                    }
                );
        }

        //change status
        $scope.changeStatus = function (order) {
            $scope.orderToUpdate = order;
            $scope.disableAll = true;
            $scope.selectedStatus = null;
            $scope.selectedUser = null;
        }
        
        //update status
        $scope.updateStatus = function (order) {
            var model = {
                UserId: order.User.Id,
                OrderId: order.Id,
                Status: order.Status
            };

            OrdersService.update(model).then(
                function () {
                    $scope.fetchData();
                    $scope.orderToUpdate = null;
                    $scope.tempStatus = null;
                },
                function () {
                    alert("failed");
                }
            );
            $scope.disableAll = false;
        }

        // list all products
        $scope.listAllOrders = function () {
            $scope.selectedUser = null;
            $scope.selectedStatus = null;
            $scope.fetchData();

        }

        //get users
        UsersService.getAll().then(
            function (response) {
                $scope.users = response.data;
            },
            function (response) {
                alert(response.data);
            }
        );     

        $scope.fetchData();
    }
]);