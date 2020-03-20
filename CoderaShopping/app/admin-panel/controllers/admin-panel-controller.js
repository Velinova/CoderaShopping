app.controller("AdminPanelController", [
    "$scope", "$stateParams", 
    function ($scope, $stateParams) {
        $scope.activeTab = Number($stateParams.id);

        $scope.tabs = [{ Header: "Orders", Index: 0 }, { Header: "Users", Index: 1 },
            { Header: "Products", Index: 2 }, { Header: "Categories", Index: 3 }, { Header: "Manufacturers", Index: 4 }];
        

    }
]);