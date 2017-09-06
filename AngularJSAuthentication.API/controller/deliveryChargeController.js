'use strict';
app.controller('deliveryChargeController', ['$scope', "$filter", "$http", "ngTableParams", '$modal', 'DeliveryChargeService', function ($scope, $filter, $http, ngTableParams, $modal, DeliveryChargeService) {

    console.log(" deliveryCharge Controller reached");
    $scope.currentPageStores = {};
    $scope.DeliveryData = [];
    DeliveryChargeService.getDeliveryData().then(function (results) {
        $scope.DeliveryData = results.data;
        $scope.callmethod();
    }, function (error) {
    });

    $scope.open = function () {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "muDeliveryAdd.html",
                controller: "ModalInstanceCtrldelivery", resolve: { delivery: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {
                console.log("Cancel Condintion");              
            })
    };

    $scope.edit = function (item) {
        console.log("Edit Dialog called survey");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "muDeliveryAdd.html",
                controller: "ModalInstanceCtrldelivery", resolve: { delivery: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.DeliveryData.push(selectedItem);
                _.find($scope.DeliveryData, function (delivery) {
                    if (delivery.id == selectedItem.id) {
                        delivery = selectedItem;
                    }
                });
                $scope.DeliveryData = _.sortBy($scope.DeliveryData, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
            })
    };

    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.DeliveryData,
            $scope.searchKeywords = "",
            $scope.filteredStores = [],
            $scope.row = "",
            $scope.select = function (page) {
                var end, start; console.log("select"); console.log($scope.stores);
                return start = (page - 1) * $scope.numPerPage, end = start + $scope.numPerPage, $scope.currentPageStores = $scope.filteredStores.slice(start, end)
            },
            $scope.onFilterChange = function () {
                console.log("onFilterChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1, $scope.row = ""
            },
            $scope.onNumPerPageChange = function () {
                console.log("onNumPerPageChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },
            $scope.onOrderChange = function () {
                console.log("onOrderChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },
            $scope.search = function () {
                console.log("search");
                console.log($scope.stores);
                console.log($scope.searchKeywords);
                return $scope.filteredStores = $filter("filter")($scope.stores, $scope.searchKeywords), $scope.onFilterChange()
            },
            $scope.order = function (rowName) {
                console.log("order"); console.log($scope.stores);
                return $scope.row !== rowName ? ($scope.row = rowName, $scope.filteredStores = $filter("orderBy")($scope.stores, rowName), $scope.onOrderChange()) : void 0
            },
            $scope.numPerPageOpt = [3, 5, 10, 20],
            $scope.numPerPage = $scope.numPerPageOpt[2],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })
        ()
    }
}]);

app.controller("ModalInstanceCtrldelivery", ["$scope", '$http', 'ngAuthSettings', "DeliveryChargeService", "$modalInstance", "delivery", function ($scope, $http, ngAuthSettings, DeliveryChargeService, $modalInstance, delivery) {
    console.log("delivery Charge");
    console.log(delivery);

    $scope.DeliveryData = {};
    if (delivery) {
        console.log("category if conditon");
        $scope.DeliveryData = delivery;
        console.log($scope.DeliveryData.id);
    }
    DeliveryChargeService.getDeliveryData().then(function (results) {
        $scope.Cluster = results.data;
    }, function (error) {
    });
    DeliveryChargeService.getWarhouse().then(function (results) {
        $scope.warehouse  = results.data;
    }, function (error) {
    });
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.AddDeliveryCharge = function () {
        var url = serviceBase + "api/deliverycharge";
        if ($scope.DeliveryData.warhouse_Id != null || $scope.DeliveryData.warhouse_Id != undefined) {

            var dataToPost = {
                min_Amount: $scope.DeliveryData.min_Amount,
                max_Amount: $scope.DeliveryData.max_Amount,
                del_Charge: $scope.DeliveryData.del_Charge,
                warhouse_Id: $scope.DeliveryData.warhouse_Id,
                cluster_Id: $scope.DeliveryData.cluster_Id,
                IsActive:true,
            };
            console.log(dataToPost);
            $http.post(url, dataToPost)
            .success(function (data) {
                console.log("Error Gor Here");
                console.log(data);
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        console.log("Got This User Already Exist");
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                    $modalInstance.close(data);
                }
            })
             .error(function (data) {
                 console.log("Error Got Heere is ");
                 console.log(data);
             })  

        }
        else {
            alert("Select warehouse");
        }
    };    
}])
