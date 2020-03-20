
var app = angular.module("codera.shopping", [
    "ngMessages",
    "ui.bootstrap",
    "ui.router",
    "ngCookies"

]);
app.constant("serverUrl", "http://localhost:1246/");
app.constant("emailRegex", "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

app.factory('notAuthorizedInterceptor', function ($q, $location, $cookies) {
    return {
        response: function (response) {
            return response;
        },
        responseError: function (response) {
            var userRole = $cookies.get('userRole');
            if (response.status === 401 && userRole === undefined) {
                $location.path('signin');
            }
            if (response.status === 401 && userRole !== 0) {
                $location.path("home");
            }

            return $q.reject(response);
        }
    };
});

app.factory('')

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('notAuthorizedInterceptor');
});

$('img').on('dragstart', function (event) { event.preventDefault(); });

//index controller
app.controller("indexController" , [
    "$scope", "$cookies", "UsersService", "$state", "$rootScope", "CategoriesService",
    function ($scope, $cookies, UsersService, $state, $rootScope, CategoriesService) {
        $scope.user = null;
        $scope.categories = [];
        $scope.dropdownStatus = false;

        $scope.statusToggle = function () {
            $scope.dropdownStatus = !$scope.dropdownStatus;
        }
        //init function getting active categories
        $scope.init = function () {
            CategoriesService.getActiveCategories().then(
                function (response) {
                    $scope.categories = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        var userId = $cookies.get('userId');
        
        if (userId) {
            var userName = $cookies.get('userName');
            var userRole = $cookies.get('userRole');

            $scope.user = {
                Id: userId,
                Name: userName,
                Role: userRole
            }
        }

        //event listener for cookie
        $rootScope.$on("cookie-data", function (event, data) {
            UsersService.getById(data.Id).then(
                function (response) {
                    $scope.user = response.data;
                    $cookies.put('userId', $scope.user.Id);
                    $cookies.put('userRole', $scope.user.Role);
                    $cookies.put('userName', $scope.user.Name);
                },
                function (response) {
                    alert(response.data);
                }
            );
        });

        //sign out function
        $scope.signOut = function () {
            $cookies.remove('userId');
            $cookies.remove('userRole');
            $cookies.remove('userName');
            $scope.user = null;
            $state.go("home");
        }

        //calling init
        $scope.init();
    }
])