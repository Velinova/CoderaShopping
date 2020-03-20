app.factory("RatingsService", ["$http", "serverUrl",
    function ($http, serverUrl) {
        var ratingsService = {};

        ratingsService.getAverageRatingValue = function (id, type) {
            return $http.post(serverUrl + "/Rating/GetAverageRatingValue", {id: id, type:type});
        };
        ratingsService.getCommentRatingsById = function (id, type) {
            return $http.post(serverUrl + "/Rating/GetCommentRatingsById", { id: id, type: type });
        };
        ratingsService.submitRating = function (model) {
            return $http.post(serverUrl + "/Rating/SubmitRating", model);
        };
        ratingsService.getRatingValues = function (id, type) {
            return $http.post(serverUrl + "/Rating/GetRatingValues", { id: id, type: type });
        };
        ratingsService.checkRatingUserObject = function (userId, objectId) {
            return $http.post(serverUrl + "/Rating/CheckRatingUserObject", { userId: userId, objectId: objectId });
        };
        return ratingsService;
    }
]);