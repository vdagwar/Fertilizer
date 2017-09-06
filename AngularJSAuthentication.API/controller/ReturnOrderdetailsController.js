'use strict';
app.controller('ReturnOrderdetailsController', ['$scope', 'OrderMasterService', 'OrderDetailsService', '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams", "peoplesService", function ($scope, OrderMasterService, OrderDetailsService, $http, $window, $timeout, ngAuthSettings, ngTableParams, peoplesService) {

    console.log("orderdetailsController start loading OrderDetailsService");
    $scope.currentPageStores = {};
    $scope.OrderDetails = {};
    $scope.OrderData = {};
    var d = OrderMasterService.getDeatil();
    $scope.backpage = function () {
        
        if (d.Page == "Redispatch") {
            window.location.href = "#/Redispatch";
        } else { window.location.href = "#/orderMaster";}
    }
    $scope.count = 1;
    $scope.OrderData = d;
    $scope.orderDetails = d.orderDetails;
    $scope.checkInDispatchedID = $scope.orderDetails[0].OrderId;

    $scope.callForDropdown = function () {        
        var url = serviceBase + 'api/OrderDispatchedMaster?id=' + $scope.checkInDispatchedID;
        $http.get(url)
        .success(function (data) {
            $scope.DBname = {};
            $scope.DBname = data.DboyName;
            if (data.DiscountAmount != 0) {
                //document.getElementById("btnSaveReturn").disabled = true;
                $scope.discountmsg = "Discount applied !! not able to return products"
            }
        });
    };

    $scope.callForDropdown();
    // check Order is returned or not
    var url = serviceBase + 'api/OrderDispatchedDetailsReturn?id=' + $scope.checkInDispatchedID;
    $http.get(url)
    .success(function (data) {
        if (data.length > 0) {
            $scope.count = 0;
            $scope.orderDetails = data;
            //document.getElementById("btnSaveReturn").hidden = true;
        } else {
            //document.getElementById("btnSaveReturn").hidden = false;
            var url = serviceBase + 'api/OrderDispatchedDetails?id=' + $scope.checkInDispatchedID;
            $http.get(url)
            .success(function (data) {
                if (data.length > 0) {
                    $scope.count = 0;
                    $scope.orderDetails = data;
                    //document.getElementById("btnSaveReturn").hidden = false;
                } else {
                    //.getElementById("btnSaveReturn").hidden = true;
                }
            });
        }        
    });
    $scope.Itemcount = 0;
    //get peoples for delivery boy
    peoplesService.getpeoples().then(function (results) {
        $scope.User = results.data;
        console.log("Got people collection");
        console.log($scope.User);
    }, function (error) {    
    });
    // end
    for (var i = 0 ; i < $scope.orderDetails.length; i++) {
        $scope.Itemcount = $scope.Itemcount + $scope.orderDetails[i].qty;
    }
    _.map($scope.OrderData.orderDetails, function (obj) {
        $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
        console.log("$scope.OrderData");
        console.log($scope.totalfilterprice);
    })
    $scope.set_color = function (orderDetail) {
        if (orderDetail.qty > orderDetail.CurrentStock) {
            return { background: "#ff9999" }
        }
    }
    $scope.saveReturn = function () {
        
        $scope.OrderData;        
        console.log($scope.OrderData);
        console.log("save orderdetailfunction");
        angular.forEach($scope.orderDetails, function (value, key) {
            console.log(key + ': ');
            console.log(value);
        });
        var url = serviceBase + 'api/OrderDispatchedDetailsReturn';
        $http.post(url, $scope.orderDetails)
        .success(function (data) {
            window.location = "#/orderMaster";
            alert("successfully return!!");
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
         })
    }
    $scope.open = function (item) {
        console.log("in open");
        console.log(item);
    };
    $scope.invoice = function (invoice) {
        console.log("in invoice Section");
        console.log(invoice);
    }; 
}]);