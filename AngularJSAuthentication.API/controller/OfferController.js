'use strict'
app.controller('OfferController', ['$scope', "$filter", "$http", "ngTableParams", '$modal', "OfferService", function ($scope, $filter, $http, ngTableParams, $modal, OfferService) {
    
    $scope.currentPageStores = {};
    $scope.coupons = [];
    $scope.open = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myADDModal.html",
                controller: "OfferADDController", resolve: { object: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {

            })
    };
    $scope.edit = function (Item) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myputmodal.html",
                controller: "OfferADDController", resolve: { object: function () { return Item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.coupons.push(selectedItem);
                _.find($scope.coupons, function (coupons) {
                    if (coupons.id == selectedItem.id) {
                        coupons = selectedItem;
                    }
                });

                $scope.coupons = _.sortBy($scope.coupons, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {

            })
    };
    $scope.opendelete = function (data, $index) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mydeletemodal.html",
                controller: "OfferdeleteController", resolve: { object: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
            })

    };
    OfferService.getoffer().then(function (results) {
       
        $scope.coupons = results.data;
        $scope.callmethod();
    })
    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.coupons,
            $scope.searchKeywords = "",
            $scope.filteredStores = [],
            $scope.row = "",

            $scope.select = function (page) {
                var end, start;
                return start = (page - 1) * $scope.numPerPage, end = start + $scope.numPerPage, $scope.currentPageStores = $scope.filteredStores.slice(start, end)
            },

            $scope.onFilterChange = function () {
                return $scope.select(1), $scope.currentPage = 1, $scope.row = ""
            },

            $scope.onNumPerPageChange = function () {
                return $scope.select(1), $scope.currentPage = 1
            },

            $scope.onOrderChange = function () {
                return $scope.select(1), $scope.currentPage = 1
            },

            $scope.search = function () {
                return $scope.filteredStores = $filter("filter")($scope.stores, $scope.searchKeywords), $scope.onFilterChange()
            },

            $scope.order = function (rowName) {
                return $scope.row !== rowName ? ($scope.row = rowName, $scope.filteredStores = $filter("orderBy")($scope.stores, rowName), $scope.onOrderChange()) : void 0
            },

            $scope.numPerPageOpt = [3, 5, 10, 20, 50],
            $scope.numPerPage = $scope.numPerPageOpt[3],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })

        ()


    }
}]);


app.controller("OfferADDController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", "OfferService", "FileUploader", function ($scope, $http, ngAuthSettings, $modalInstance, object, OfferService, FileUploader) {
    
    $scope.saveData = {};
    if (object) {
        $scope.saveData = object;
    }
    $scope.coupons = [];
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },
     $scope.Add = function (data) {
         
         var url = serviceBase + "api/offer";
         var dataToPost = {
             OfferCategory: $scope.saveData.OfferCategory,
             OfferType: $scope.saveData.OfferType,
             OfferName: $scope.saveData.OfferName,
             Description: $scope.saveData.Description,
             Amount: $scope.saveData.Amount,
             MinAmount: $scope.saveData.MinAmount,
             MaxAmount: $scope.saveData.MaxAmount,
             StartTime: $scope.saveData.StartTime,
             EndTime: $scope.saveData.EndTime,
             StartDate: $scope.saveData.StartDate,
             EndDate: $scope.saveData.EndDate
         };
         $http.post(url, dataToPost)
         .success(function (data) {
             $modalInstance.close(data);
         })
          .error(function (data) {

          })
         console.log(data);
     };



    $scope.Put = function (data) {
        
        $scope.saveData = {};
        if (object) {
            $scope.saveData = object;
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }
        var url = serviceBase + "api/offer";
        var dataToPost = {
            OfferId: $scope.saveData.OfferId,
            OfferCategory: $scope.saveData.OfferCategory,
            OfferType: $scope.saveData.OfferType,
            OfferName: $scope.saveData.OfferName,
            Description: $scope.saveData.Description,
            Amount: $scope.saveData.Amount,
            MinAmount: $scope.saveData.MinAmount,
            MaxAmount: $scope.saveData.MaxAmount,
            StartTime: $scope.saveData.StartTime,
            EndTime: $scope.saveData.EndTime,
            StartDate: $scope.saveData.StartDate,
            EndDate: $scope.saveData.EndDate
        };
        $http.put(url, dataToPost)
        .success(function (data) {
            $modalInstance.close(data);
        })
         .error(function (data) {

         })
    };
}])

app.controller("OfferdeleteController", ["$scope", '$http', "$modalInstance", "OfferService", 'ngAuthSettings', "object", function ($scope, $http, $modalInstance, OfferService, ngAuthSettings, object) {
  
    $scope.group = [];
    if (object) {
        $scope.saveData = object;

    }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.delete = function (dataToPost, $index) {
        OfferService.deleteoffer(dataToPost).then(function (results) {
            $modalInstance.close(dataToPost);
        }, function (error) {
            alert(error.data.message);
        });
    }

}])