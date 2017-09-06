'use strict';
app.controller('RedeemOrderCtrl', ['$scope', '$http', 'ngAuthSettings', '$filter', "ngTableParams", '$modal', function ($scope, $http, ngAuthSettings, $filter, ngTableParams, $modal) {
    $scope.OrderData = [];

    // new pagination 
    $scope.pageno = 1;
    $scope.total_count = 0;
    $scope.itemsPerPage = 30; //this could be a dynamic value from a drop down
    $scope.numPerPageOpt = [30, 50, 100, 200];//dropdown options for no. of Items per page
    $scope.onNumPerPageChange = function () { $scope.itemsPerPage = $scope.selected; }
    $scope.selected = $scope.numPerPageOpt[0];// for Html page dropdown

    $scope.$on('$viewContentLoaded', function () {
        $scope.getData($scope.pageno);
    });

    $scope.getData = function (pageno) {
        console.log("In get data function");
        $scope.OrderData = [];
        var url = serviceBase + "api/OrderMastersAPI/dreamitem" + "?list=" + $scope.itemsPerPage + "&page=" + pageno;
        $http.get(url).success(function (response) {
            $scope.OrderData = response.ordermaster;  //ajax request to fetch data into vm.data
            $scope.total_count = response.total_count;
        });
    };

    $scope.showDetail = function (data) {
        $scope.items = data;
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myOrderdetail.html",
                controller: "ModalRedeemOrderCtrl", resolve: { order: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                console.log("modal close");
                console.log(selectedItem);
            })
    };
}]);

app.controller("ModalRedeemOrderCtrl", ["$scope", '$http', "$modalInstance",'peoplesService', 'ngAuthSettings', 'order', function ($scope, $http, $modalInstance,peoplesService, ngAuthSettings, order) {
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }
    $scope.OrderData = {};
    if (order) { 
        $scope.OrderData = order;
        $scope.DBname = $scope.OrderData.DboyName;
    }
    $scope.selcteddboy = function (db) {
        $scope.Dboy = JSON.parse(db);
    }
    peoplesService.getpeoples().then(function (results) {
        $scope.User = results.data;
        console.log("Got people collection");
        console.log($scope.User);
    }, function (error) {

    });
    $scope.save = function () {
        console.log($scope.Dboy);
        var data = $scope.orderDetails;
        var flag = true;
        if ($scope.Dboy == undefined) {
            alert("Please Select Delivery Boy");
            flag = false;
        }
        if (flag == true) {
            try {
                var obj = ($scope.Dboy);
            } catch (err) {
                alert("Please Select Delivery Boy");
                console.log(err);
            }
            $scope.OrderData["DboyName"] = obj.DisplayName;
            $scope.OrderData["DboyMobileNo"] = obj.Mobile;
            $scope.OrderData["Status"] = "Dispatched";
            $scope.OrderData;
            var url = serviceBase + 'api/OrderMastersAPI/dreamitem';
            $http.put(url, $scope.OrderData)
            .success(function (data) {
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        console.log("Got This User Already Exist");
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                }
                $modalInstance.close();
            })
             .error(function (data) {
                 console.log("Error Got Heere is ");
                 console.log(data);
                 $modalInstance.close();
             })
        }
    }
    $scope.Delivered = function () {
        $scope.OrderData["Status"] = "Delivered";
        var url = serviceBase + 'api/OrderMastersAPI/dreamitem';
        $http.put(url, $scope.OrderData)
        .success(function (data) {
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    console.log("Got This User Already Exist");
                    $scope.AlreadyExist = true;
                }
            }
            else {
            }
            $modalInstance.close();
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
             $modalInstance.close();
         })
    }
    $scope.cancelOrder = function () {
        $scope.OrderData["Status"] = "Canceled";
        var url = serviceBase + 'api/OrderMastersAPI/cancel?cancel=cl&id='+ $scope.OrderData.Order_Id;
        $http.put(url)
        .success(function (data) {
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    console.log("Got This User Already Exist");
                    $scope.AlreadyExist = true;
                }
            }
            else {
            }
            $modalInstance.close();
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
             $modalInstance.close();
         })
    }
}]);