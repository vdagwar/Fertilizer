'use strict';
app.controller('ReqServiceController', ['$scope', '$http', function ($scope, $http) {

    $scope.requestService = [];

    var url = serviceBase + "/api/ReqService/get";
    $http.get(url)
        .success(function (data) {
            if (data.length == 0)
                alert("Not Found");
            else
                $scope.requestService = data;
            console.log(data);
        });

}]);