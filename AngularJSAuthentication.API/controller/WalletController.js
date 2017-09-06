'use strict'
app.controller('WalletController', ['$scope', "$filter", "$http", "ngTableParams", '$modal', function ($scope, $filter, $http, ngTableParams, $modal) {
    $scope.wallets = [];

    $scope.getWalletData = function () { 
        var url = serviceBase + "api/wallet";
        $http.get(url).success(function (response) {
            
            $scope.wallets = response;
        });
    };

    // For export data
    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData = function () {
        alasql('SELECT CustomerId,Skcode,ShopName,TotalAmount,CreatedDate INTO XLSX("WalletPointCustomer.xlsx",{headers:true}) FROM ?', [$scope.wallets]);
    };

    $scope.getWalletData();
   
    $scope.open = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myADDModal.html",
                controller: "WalletControllerAddController", resolve: { object: function () { return $scope.data } }
            }), modalInstance.result.then(function (selectedItem) {
            },
            function () {
            })
    };
    $scope.edit = function (wallets) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myEDITModal.html",
                controller: "WalletControllerAddController", resolve: { object: function () { return wallets } }
            }), modalInstance.result.then(function (wallets) {
            },
            function () {
            })
    };
    $scope.openCashConvesion = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "cashConversionModal.html",
                controller: "WalletConversionController", resolve: { object: function () { return $scope.data } }
            }), modalInstance.result.then(function (selectedItem) {
            },
            function () {
            })
    };
}]);
app.controller("WalletControllerAddController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", '$modal', 'customerService', function ($scope, $http, ngAuthSettings, $modalInstance, object, $modal, customerService) {
    $scope.saveData = {};
    if (object) { $scope.saveData = object; }
    
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.customer = {};   
    $scope.GetCustomer = function (skcode) {
        $scope.customer = {};
        customerService.getcustomerBySkcode(skcode).then(function (results) {
            if (results.data != null) { 
                $scope.customer = results.data;
                alert("SHOP NAME:" + $scope.customer.ShopName + " & MOBILE:" + $scope.customer.Mobile);
            }
            else {
                $scope.customer = null;
                alert("Enter Correct Skcode:");
            }
        }, function (error) {
        });
    };

    $scope.AddWallet = function () {
        if (object) {
            $scope.customer.CustomerId = $scope.saveData.CustomerId
        }
        var dataToPost = {
            CustomerId: $scope.customer.CustomerId,
            CreditAmount: $scope.saveData.CreditAmount
        }
        console.log(dataToPost);
        var url = serviceBase + "api/wallet";
        $http.post(url, dataToPost).success(function (data) {
            if (data != null && data != "null") {
                alert("Wallet Amount Added Successfully... :-)");
                $modalInstance.close();
                location.reload();
            }
            else {
                alert("got some error... :-)");
                $modalInstance.close();
            }
        })
     .error(function (data) {
     })
    };


   

}]);
app.controller("WalletConversionController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", '$modal', function ($scope, $http, ngAuthSettings, $modalInstance, $modal) {
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.pointData = [];

    $http.get(serviceBase + "api/wallet/cash").success(function (data) {
        if (data != null && data != "null") {
            $scope.pointData = data;
        }
    })
    $scope.AddcashData = function () {
        var dataToPost = {
            Id: $scope.pointData.Id,
            point: $scope.pointData.point,
            rupee: $scope.pointData.rupee
        }
        console.log(dataToPost);
        var url = serviceBase + "api/wallet/cash";
        $http.post(url, dataToPost).success(function (data) {
            if (data != null && data != "null") {
                alert("margin point Added Successfully... :-)");
                $modalInstance.close();
            }
            else {
                alert("got some error... :-)");
                $modalInstance.close();
            }
        })
     .error(function (data) {
     })
    };

}]);
