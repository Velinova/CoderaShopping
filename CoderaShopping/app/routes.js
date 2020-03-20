
angular.module("codera.shopping").config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/home");

    //todo: define ur states here
    $stateProvider
        .state('home', {
            url: '/home',
            templateUrl: 'app/home/home.html',
            controller: 'HomeController'

        })
        .state('signin', {
            url: '/signin',
            templateUrl: 'app/users/account/sign-in.html',
            controller: "SignInController"
        })
        
        .state('categories', {
            url: '/categories',
            templateUrl: 'app/categories/categories.html',
            controller: "CategoriesController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('products', {
            url: '/products',
            templateUrl: 'app/products/products.html',
            controller: "ProductsController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('users', {
            url: '/users',
            templateUrl: 'app/users/users.html',
            controller: "UsersController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('orders', {
            url: '/orders',
            templateUrl: 'app/orders/orders.html',
            controller: "OrdersController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('manifacturers', {
            url: '/manifacturers',
            templateUrl: 'app/manifacturers/manifacturers.html',
            controller: "ManifacturersController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('myorders', {
            url: '/myorders',
            templateUrl: 'app/orders/myorders.html',
            controller: "MyOrdersController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userId = $cookies.get("userId");

                    if (userId == undefined) {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
        .state('admin-panel', {
            url: '/admin-panel/:id',
            templateUrl: 'app/admin-panel/admin-panel.html',
            controller: "AdminPanelController",
            resolve: {
                loggedIn: function ($q, $cookies) {
                    var userRole = $cookies.get("userRole");

                    if (userRole != "1") {
                        return $q.reject();
                    }
                    else {
                        return $q.resolve();
                    }
                }
            }
        })
});