app.controller("MyCartModalController", [
    "$scope", "$uibModalInstance", "orderItems", "OrdersService", "$cookies","$state",
    function ($scope, $uibModalInstance, orderItems, OrdersService, $cookies, $state) {
      
        $scope.orderItems = $cookies.getObject('orderItems');
        $scope.count = $scope.orderItems.length;
        $scope.userId = $cookies.get('userId');
        $scope.show = false;
        $scope.submitted = false;
        $scope.model = {
            Holder: "",
            CardNumber: "",
            ExpirationDate: "",
            CVC: ""
        };
       
        //total price
        $scope.total=0;
        if ($scope.orderItems.length > 0) {
            $scope.keys = Object.keys(orderItems);
            for (var key in $scope.keys) {
                $scope.temp = $scope.orderItems[key].Product.Price * $scope.orderItems[key].Quantity;
                $scope.total += $scope.temp;
            }
        }
        else {
            $scope.total = 0;
        }
        
        //delete item from order
        $scope.delete = function (index) {
            $scope.keys = Object.keys(orderItems);
            $scope.orderItems.splice(index, 1);
            $cookies.remove("orderItems");
            $cookies.putObject("orderItems", $scope.orderItems);
            $scope.orderItems = $cookies.getObject("orderItems");
            $scope.count = $scope.orderItems.length;
            $scope.total = $scope.total - (index.Product.Price * index.Quantity);
        };

        //change show
        $scope.changeShow = function () {
            $scope.show = true;
        }


        //pay order
        $scope.pay = function (form) {
            
            $scope.userId = $cookies.get('userId');
            if ($scope.userId === undefined) {
                $uibModalInstance.dismiss(orderItems);
                $state.go("signin");
            }
            else {
                $scope.submitted = true;

                if (form.$invalid) {
                    return;
                }

                OrdersService.pay($scope.orderItems, $scope.userId).then(
                    function (response) {
                        $scope.orderItems = [];
                        $cookies.remove("orderItems");
                        $cookies.putObject('orderItems', $scope.orderItems);
                        $uibModalInstance.dismiss(true);
                        alert(response.data + "success");
                        $scope.show = false;
                    },
                    function (response) {
                        alert(response.data);
                        $scope.show = false;
                    }
                );
                
            }
        }

        //close my cart modal
        $scope.close = function () {
            $uibModalInstance.dismiss($scope.orderItems);
        }

    }
]);