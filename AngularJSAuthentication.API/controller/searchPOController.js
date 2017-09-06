'use strict';
app.controller('searchPOController', ['$scope', 'SearchPOService', 'PurchaseODetailsService', '$http', 'ngAuthSettings', '$filter', "ngTableParams", '$modal',  function ($scope, SearchPOService, PurchaseODetailsService, $http, ngAuthSettings, $filter, ngTableParams, $modal) {

    console.log("searchPOController start loading OrderDetailsService");
    $scope.currentPageStores = {};

    $scope.Porders = [];
    SearchPOService.getPorders().then(function (results) {
        $scope.Porders = results.data;
        console.log("orders..........");
        console.log($scope.Porders);
        $scope.callmethod();
    }, function (error) {
    });
    
    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.Porders,
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
            $scope.numPerPageOpt = [10, 50, 100, 200],
            $scope.numPerPage = $scope.numPerPageOpt[2],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })
        ()
    }
    $scope.open = function (data) {
        console.log("open fn");
        SearchPOService.save(data).then(function (results) {
            console.log("master save fn");
            console.log(results);
        }, function (error) {
        });        
    };
    $scope.invoice = function (data) {
        SearchPOService.view(data).then(function (results) {
            console.log("master invoice fn");
            console.log(results);
        }, function (error) {
        });
    };
    $scope.goodrecived = function (data) {
        SearchPOService.goodsrecived(data).then(function (results) {
            console.log("master invoice fn");
            console.log(results);
        }, function (error) {
        });
    };

    $scope.idata={};
    $scope.Search = function (key) {
        var url = serviceBase + "api/itemMaster/Searchinitem?key=" + key;
        $http.get(url).success(function (data) {
            $scope.itemData = data;
            $scope.idata= angular.copy($scope.itemData);
        })
    };
    
    
    $scope.iidd = 0;
    $scope.Minqtry = function (key) {
        $scope.itmdata = [];
        $scope.iidd = Number(key);
        for (var c = 0; c < $scope.idata.length; c++) {
            if ($scope.idata.length != null) {
                if ($scope.idata[c].ItemId == $scope.iidd) {
                    $scope.itmdata.push($scope.idata[c]);
                }
            }
            else {
            }
        }
    }
      

    $scope.SearchSupplier = function (key) {
        var url = serviceBase + "api/Suppliers/search?key=" + key;
        $http.get(url).success(function (data) {
            $scope.Suppliers = data;
        })
    };
    $scope.openmodel = function (data) {
        console.log("Modal opened Role");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mySearchModal.html",
                controller: "searchPOController", resolve: { role: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {})
    };
    $scope.ok = function () { $modalInstance.close(); },
 $scope.cancel = function () { $modalInstance.dismiss('canceled'); }

    $scope.Searchsave = function (item) {
        
        $scope.PurchaseMinOrderQty=item.Noofset*item.PurchaseMinOrderQty;
      //  alert($scope.PurchaseMinOrderQty)
        console.log("Action");
        var url = serviceBase + "api/PurchaseOrderList/addPo?ItemId=" + item.ItemId + "&qty=" + $scope.PurchaseMinOrderQty + "&SupplierId=" + item.SupplierId;
        $http.post(url).success(function (data) {
            console.log("Error Got Here");
            console.log(data);
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    console.log("Got This User Already Exist");
                    $scope.AlreadyExist = true;
                }
            }
            else {
                //$modalInstance.close(data);
                window.location.reload()
            }

        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
             // return $scope.showInfoOnSubmit = !0, $scope.revert()
         })

       
    };

    
}]);