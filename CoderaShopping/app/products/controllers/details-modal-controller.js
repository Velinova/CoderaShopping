app.controller("DetailsModalController", [
    "$scope", "$uibModalInstance", "RatingsService", "product", "$cookies",
    function ($scope, $uibModalInstance, RatingsService, product, $cookies) {
        $scope.product = product;
        $scope.ratings = [];
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.itemsPerPage = 5;
        $scope.pageratings = [];
        $scope.addRating = false;
        $scope.isAnonymous = false;
        $scope.rateValues = {};
        $scope.user = $cookies.get("userId");
        $scope.model = {};
        $scope.addModel = {};


        //check if rating exists
        RatingsService.checkRatingUserObject($scope.user, $scope.product.Id).then(
            function (response) {
                $scope.addmodel = {
                    Id: response.data.Id,
                    UserId: $scope.user,
                    ObjectId: $scope.product.Id,
                    ObjectType: 1,
                    Value: response.data.Value,
                    Comment: response.data.Comment,
                    ShowName: response.data.ShowName
                }

            },
            function (response) {
                alert(response.data);
            }
        );

        //fetch data
        $scope.fetchData = function () {
            //get comment ratings
            RatingsService.getCommentRatingsById($scope.product.Id, 1).then(
                function (response) {
                    $scope.ratings = response.data;
                    $scope.totalItems = $scope.ratings.length;
                },
                function (response) {
                    alert(response.data);
                }
            );
            // get average rating
            RatingsService.getAverageRatingValue($scope.product.Id, 1).then(
                function (response) {
                    $scope.rate = response.data;
                },
                function (response) {
                    alert(response.data);
                }
            );
            //get rating values
            RatingsService.getRatingValues($scope.product.Id, 1).then(
                function (response) {
                    $scope.rateValues = response.data;
                    $scope.percentages = {
                        One: percentage($scope.rateValues.One, $scope.rateValues.Total),
                        Two: percentage($scope.rateValues.Two, $scope.rateValues.Total),
                        Three: percentage($scope.rateValues.Three, $scope.rateValues.Total),
                        Four: percentage($scope.rateValues.Four, $scope.rateValues.Total),
                        Five: percentage($scope.rateValues.Five, $scope.rateValues.Total),
                    }

                },
                function (response) {
                    alert(response.data);
                }
            );
        }

        //percentage function
        function percentage(partialValue, totalValue) {
            if (totalValue == 0) {
                return 0;
            }
            return (100 * partialValue) / totalValue;
        } 

        //pagination
        $scope.$watch("currentPage", function () {
            setPagingData($scope.currentPage);
        });

        function setPagingData(page) {
            var pagedData = $scope.ratings.slice(
                (page - 1) * $scope.itemsPerPage,
                page * $scope.itemsPerPage
            );
            $scope.pageratings = pagedData;
        }
        
        $scope.hoveringOver = function (value) {
            $scope.overStar = value;
        };

        //submit rate
        $scope.showRatingForm = function () {
            $scope.addRating = true;
        }
        
        $scope.showAllRatings = function () {
            $scope.addRating = false;
        }
        $scope.submitRating = function () {
           
                RatingsService.submitRating($scope.addmodel).then(
                    function (response) {
                        $scope.fetchData();
                        $scope.addRating = false;
                    },
                    function (response) {
                        alert(response.data);
                    }
                );
        }
        //cancel 
        $scope.cancel = function () {
            $uibModalInstance.dismiss(false);
        }

        //init
        $scope.fetchData();
    }
]);
