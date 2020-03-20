app.directive("categories", [
    function () {
        var categories = {
            restrict: 'E',
            templateUrl: 'app/categories/categories.html',
            controller: "CategoriesController"
        }
        return categories;
    }
])
app.directive("manufacturers", [
    function () {
        var manufacturers = {
            restrict: 'E',
            templateUrl: 'app/manifacturers/manifacturers.html',
            controller: "ManifacturersController"
        }
        return manufacturers;
    }
])
app.directive("orders", [
    function () {
        var orders = {
            restrict: 'E',
            templateUrl: 'app/orders/orders.html',
            controller: "OrdersController"
        }
        return orders;
    }
])
app.directive("products", [
    function () {
        var products = {
            restrict: 'E',
            templateUrl: 'app/products/products.html',
            controller: "ProductsController"
        }
        return products;
    }
])
app.directive("users", [
    function () {
        var users = {
            restrict: 'E',
            templateUrl: 'app/users/users.html',
            controller: "UsersController"
        }
        return users;
    }
])