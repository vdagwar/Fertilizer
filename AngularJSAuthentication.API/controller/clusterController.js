'use strict';
app.controller('clusterController', ['$scope', 'ClusterService', "$filter", "$http", "ngTableParams", '$modal',
    function ($scope, ClusterService, $filter, $http, ngTableParams, $modal) {
    console.log(" Cluster  Controller reached");
    $scope.currentPageStores = {};     
    $scope.open = function () {
        console.log("Modal opened  ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myClusterModal.html",
                controller: "ModalInstanceCtrlCluster", resolve: { cluster: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                   $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion"); 
            })
    };
    $scope.edit = function (item) {
        console.log("Edit Dialog called cluster ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myClusterPut.html",
                controller: "ModalInstanceCtrlCluster", resolve: { cluster: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                console.log("sELECT wAREHOUSE..")
                $scope.cluster.push(selectedItem);
                _.find($scope.cluster, function (cluster) {
                    if (cluster.id == selectedItem.id) {
                        cluster = selectedItem;
                    }
                });
                $scope.cluster = _.sortBy($scope.cluster, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
            })
    };
    $scope.opendelete = function (data, $index) {
        console.log(data);
        console.log("Delete Dialog called for warehouse ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeletecluster.html",
                controller: "ModalInstanceCtrldeleteCluster", resolve: { cluster: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");
            })
    };

    $scope.Clusters = [];
    ClusterService.getcluster().then(function (results) {
        $scope.Clusters = results.data;
        $scope.callmethod();
    }, function (error) {
        
    });
    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.Clusters,

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

app.controller("ModalInstanceCtrlCluster", ["$scope", '$http', 'ngAuthSettings', "ClusterService", "$modalInstance", "cluster",
     function ($scope, $http, ngAuthSettings, ClusterService, $modalInstance, cluster) {
         console.log("cluster");
         $scope.ClusterData = { };
         $scope.ok = function () { $modalInstance.close(); },
         $scope.cancel = function () { $modalInstance.dismiss('canceled'); },
         $scope.warehouse = [];
         ClusterService.getwarehouse().then(function (results) {
             $scope.warehouse = results.data;
         }, function (error) {
         });
         if (cluster) {
             $scope.ClusterData = cluster;
             console.log("found Puttt Warehouse ");
             console.log(cluster);
             console.log($scope.ClusterData);
         }
         $scope.AddCluster = function (data) {
             console.log("Cluster");
             console.log(data);
             var url = serviceBase + "api/cluster/add";
             var dataToPost = {
                 ClusterName: $scope.ClusterData.ClusterName,
                 Warehouseid: $scope.ClusterData.Warehouseid,
                 Address: $scope.ClusterData.Address,
                 Phone: $scope.ClusterData.Phone,
                 Active: $scope.ClusterData.Active
                    };
             console.log("kkkkkk");
             console.log(dataToPost);
             $http.post(url, dataToPost)
             .success(function (data) {
                 console.log(data);
                 console.log($scope.cluster);
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
         };
         $scope.putCluster = function (data) {
                        
             $scope.ok = function () { $modalInstance.close(); },
         $scope.cancel = function () { $modalInstance.dismiss('canceled'); },
             console.log("Update cluster ");

             var url = serviceBase + "api/cluster/update";
             var dataToPost = {
                 ClusterId: $scope.ClusterData.ClusterId,
                 ClusterName: $scope.ClusterData.ClusterName,
                 Warehouseid: $scope.ClusterData.Warehouseid,
                 Address: $scope.ClusterData.Address,
                 Phone: $scope.ClusterData.Phone,
                 Active: $scope.ClusterData.Active,
             };

             console.log(dataToPost);
             $http.put(url, dataToPost)
             .success(function (data) {
                 console.log("Error Gor Here");
                 console.log(data);
                 if (data.id == 0) {
                     $scope.gotErrors = true;
                     if (data[0].exception == "Already") {
                         console.log("Got This cluster Already Exist");
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
         };
     }])
app.controller("ModalInstanceCtrldeleteCluster", ["$scope", '$http', "$modalInstance", "ClusterService", 'ngAuthSettings', "cluster",
    function ($scope, $http, $modalInstance, ClusterService, ngAuthSettings, cluster) {
        console.log("delete modal opened");
        function ReloadPage() {
            location.reload();
        };
        $scope.cluster = [];

        if (cluster) {
            $scope.ClusterData = cluster;
            console.log("found cluster ");
            console.log($scope.ClusterData);
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        $scope.deleteCluster = function (dataToPost, $index) {
            console.log(dataToPost);
            console.log("Delete  cluster  controller");

            ClusterService.deletecluster(dataToPost).then(function (results) {
                console.log("Del");

                console.log("index of item " + $index);
                console.log($scope.cluster.length);
                $modalInstance.close(dataToPost);
            }, function (error) {
                alert(error.data.message);
            });
        }
    }])
