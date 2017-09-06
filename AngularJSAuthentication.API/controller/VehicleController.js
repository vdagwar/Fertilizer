'use strict';
app.controller('VehicleController', ['$scope',"$filter", "$http", "ngTableParams", '$modal', "ngAuthSettings", function ($scope,$filter, $http, ngTableParams, $modal, ngAuthSettings) {
    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myVehicleModal.html",
                controller: "ModalInstanceCtrlvehicle", resolve: { obj: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {
            })
    };

    $scope.edit = function (item) {
        console.log("Edit Dialog called ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myVehicleModalPut.html",
                controller: "ModalInstanceCtrlvehicle", resolve: { obj: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {},
            function () {
                console.log("Cancel Condintion");
               
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log($index);
        console.log("Delete Dialog called for");
       
        var myData = { all: $scope.currentPageStores ,city1:data};

       
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteVehicle.html",
                controller: "deletevehicleCtrl", resolve: { obj: function () { return myData } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.currentPageStores.splice($index,1);
            },
            function () {
                console.log("Cancel Condintion");
             
            })
        //$scope.city.splice($scope.city.indexOf($scope.city), 1)
       // $scope.city.splice($index, 1);
    };
    $scope.Vehicles = [];

    $http.get(serviceBase + 'api/Vehicles').then(function (results) {
        if (results.data != "null") {
            $scope.Vehicles = results.data;
            $scope.callmethod();
        }
    });

    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.Vehicles,
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
app.controller("ModalInstanceCtrlvehicle", ["$scope", '$http', 'ngAuthSettings', "WarehouseService", "CityService", "StateService", "$modalInstance", "obj", 'FileUploader', function ($scope, $http, ngAuthSettings, WarehouseService, CityService, StateService, $modalInstance, obj, FileUploader) {
    $scope.VehicleData = {};
    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouse = results.data;
    }, function (error) { });

    $scope.states = [];
    StateService.getstates().then(function (results) {
        $scope.states = results.data;
    }, function (error) { });

    $scope.citys = [];
    CityService.getcitys().then(function (results) {
        $scope.citys = results.data;
    }, function (error) { });

    if (obj) {
        console.log("city if conditon");
        $scope.VehicleData = obj;
        console.log($scope.VehicleData);
    }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.AddVehicle = function (data) {
        console.log("AddCity");
        var url = serviceBase + "api/Vehicles";
        var dataToPost = {
            Capacity: $scope.VehicleData.Capacity,
            VehicleName: $scope.VehicleData.VehicleName,
            VehicleNumber: $scope.VehicleData.VehicleNumber,
            Warehouseid: $scope.VehicleData.Warehouseid,
            Cityid: $scope.VehicleData.Cityid,
            isActive: $scope.VehicleData.isActive,
            OwnerAddress: $scope.VehicleData.OwnerAddress,
            OwnerName: $scope.VehicleData.OwnerName
        };
        console.log(dataToPost);
        $http.post(url, dataToPost)
        .success(function (data) {
            console.log("Error Got Here");
            console.log(data);
            if (data.VehicleId == 0) {
                alert("Vehicle with same number already exists");
            }
            else {
                $modalInstance.close(data);
            }
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
         })
    };

    $scope.PutVehicle = function (data) {
        $scope.VehicleData = {};
        if (obj) {
            $scope.VehicleData = obj;
            console.log("found Puttt ");
            console.log(obj);
            console.log($scope.VehicleData);
            console.log("selected City");
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Put");
        var url = serviceBase + "api/Vehicles";
        var dataToPost = {
            VehicleId: $scope.VehicleData.VehicleId,
            Capacity: $scope.VehicleData.Capacity,
            VehicleName: $scope.VehicleData.VehicleName,
            VehicleNumber: $scope.VehicleData.VehicleNumber,
            Warehouseid: $scope.VehicleData.Warehouseid,
            Cityid: $scope.VehicleData.Cityid,
            isActive: $scope.VehicleData.isActive,
            OwnerAddress: $scope.VehicleData.OwnerAddress,
            OwnerName: $scope.VehicleData.OwnerName
        }
        console.log(dataToPost);
        $http.put(url, dataToPost)
        .success(function (data) {
            console.log("Put");

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
                console.log("save data");
            }

        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
         })
    };
}]);
app.controller("deletevehicleCtrl", ["$scope", '$http', "$modalInstance", "CityService", 'ngAuthSettings', "obj", function ($scope, $http, $modalInstance, CityService, ngAuthSettings, obj) {
    console.log("delete modal opened");
    $scope.city = [];
    if (obj)

        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        $scope.deletevehicle = function (dataToPost, $index) {
            console.log("Delete  controller");
            $http.delete(serviceBase + 'api/Vehicles/?id=' + $scope.VehicleData.VehicleId).then(function (results) {
                $modalInstance.close(results);
            });
        }
}]);