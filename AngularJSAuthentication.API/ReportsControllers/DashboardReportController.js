'use strict'
app.controller('DashboardReportController', ['$scope', "$http", "ngTableParams", "WarehouseService", function ($scope, $http, ngTableParams, WarehouseService) {
    $scope.Warehouseid = 1;
    function convertDate(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-');
    }    

    WarehouseService.getwarehouse().then(function (results) { 
        $scope.warehouse = results.data;
        $scope.Warehouseid = $scope.warehouse[0].Warehouseid;
        $scope.getCustData();
    }, function (error) { });

    $scope.selectedwarehouse = function () {
        $scope.custdata = null;
        $scope.getCustData();
    };

    $scope.getCustData = function () {
        var url = serviceBase + "api/DashboardReport?Warehouseid=" + $scope.Warehouseid;
        $http.get(url).success(function (results) {
            $scope.custdata = results;
        });
    };



    $scope.PostDashData = function () {

        var url = serviceBase + "api/DashboardReport";

        $.post(url, function (data, status) {

                });    
           };
}]);