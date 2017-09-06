'use strict';
app.controller('SalesBounceController', ['$scope', "$filter", "$http", "ngTableParams", "SalesService", function ($scope, $filter, $http, ngTableParams, SalesService) {
    
   var dd = SalesService.getDeatil();
   console.log("bounce selected")
       //alert(dd);
       console.log(dd);
       $scope.d = dd;

       $scope.CheqBounceupdate = function (data) {
           var url = serviceBase + "api/salessettlement/Bounce";
           $http.put(url, data)
       .then(function (response) {
           $scope.cash = response.data;
           location.reload('/SalesSettlement');

       });
       }
  
}]);
