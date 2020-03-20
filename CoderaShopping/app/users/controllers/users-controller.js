app.controller("UsersController", [
    "$scope", "$uibModal", "UsersService","$cookies",
    function ($scope, $uibModal, UsersService, $cookies) {
        $scope.users = [];
        $scope.cookieId = $cookies.get('userId');
        fetchData();

        $scope.summonDeleteDialog = function (userId) {
            if (userId === $scope.cookieId)
                return;

            var modalInstance = $uibModal.open(
                {
                    controller: "DeleteModalController",
                    templateUrl: "app/common/delete-modal/delete-modal.html",
                    backdrop:false,
                    resolve: {
                        service: function () {
                            return UsersService.delete;
                        },
                        params: function () {
                            return userId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }

        $scope.summonChangeRoleDialog = function (userId) {
            var modalInstance = $uibModal.open(
                {
                    controller: "ChangeRoleModalController",
                    templateUrl: "app/users/change-role-modal.html",
                    backdrop: false,
                    resolve: {
                        service: function () {
                            return UsersService.changeRole;
                        },
                        params: function () {
                            return userId;
                        }
                    }
                }
            );

            modalInstance.result.then(function (result) {
                fetchData();
            });
        }


        function fetchData() {
           UsersService.getAll().then(
                // success
                function (response) {
                    $scope.users = response.data;
                },
                // error
                function (response) {
                    alert(response.data);
                }
            );
        }
    }
]);