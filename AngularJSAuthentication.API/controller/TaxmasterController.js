'use strict';
app.controller('TaxmasterController', ['$scope', 'TaxMasterService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, TaxMasterService, $filter, $http, ngTableParams, $modal) {

    console.log(" State Controller reached");

    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened tax");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myaddTaxModal.html",
                controller: "ModalInstanceCtrlTaxmaster", resolve: { taxmaster: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {
                console.log("Cancel Condintion");
            })
    };


    $scope.edit = function (item) {
        console.log("Edit Dialog called ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myTaxModalPut.html",
                controller: "ModalInstanceCtrlTaxmaster", resolve: { taxmaster: function () { return item } }
               
            }), modalInstance.result.then(function (selectedItem) {
                
                $scope.taxmasters.push(selectedItem);
                _.find($scope.taxmasters, function (taxmaster) {
                    if (taxmaster.id == selectedItem.id) {
                        taxmaster = selectedItem;
                        }
                });
                $scope.taxmasters = _.sortBy($scope.taxmaster, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log("Delete Dialog called for state");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteTax.html",
                controller: "ModalInstanceCtrldeletetax", resolve: { taxmaster: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");
           
            })
    };

    $scope.taxmasters = [];

    TaxMasterService.getTaxmaster().then(function (results) {
       $scope.taxmasters = results.data;
        $scope.callmethod();
    }, function (error) {
       
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.taxmasters,

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

app.controller("ModalInstanceCtrlTaxmaster", ["$scope", '$http', 'ngAuthSettings', "TaxMasterService", "$modalInstance", "taxmaster", 'FileUploader', function ($scope, $http, ngAuthSettings, TaxMasterService, $modalInstance, taxmaster, FileUploader) {
    console.log("tax master");

  


    $scope.TaxData = {

    };
    if (taxmaster) {
        console.log("state if conditon");

        $scope.TaxData = taxmaster;

        console.log($scope.TaxData.TaxName);
       
    }


    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddTax = function (data) {
         console.log("add Tax master ");

         var url = serviceBase + "api/TaxMaster";
         var dataToPost = {
             TaxID: $scope.TaxData.TaxID,
         TaxName: $scope.TaxData.TaxName,
         TAlias: $scope.TaxData.TAlias,
         TPercent: $scope.TaxData.TPercent,
         TDiscription: $scope.TaxData.TDiscription,
         CompanyId: $scope.TaxData.CompanyId,
         Active: $scope.TaxData.Active,
         CreatedDate: $scope.TaxData.CreatedDate,
         UpdatedDate: $scope.TaxData.UpdatedDate,
         Deleted: $scope.TaxData.Deleted
         };

         console.log(dataToPost);
         $http.post(url, dataToPost)
         .success(function (data) {

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
                 
                 $modalInstance.close(data);
             }

         })
          .error(function (data) {
              console.log("Error Got Heere is ");
              console.log(data);
          })
     };



    $scope.PutTax= function (data) {
        $scope.TaxData = {

        };
        if (taxmaster) {
            $scope.TaxData = taxmaster;
            console.log("found Puttt state");
            console.log(taxmaster);
            console.log($scope.TaxData);
         
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update ");

   

        var url = serviceBase + "api/TaxMaster";
        var dataToPost = {
            TaxID: $scope.TaxData.TaxID,
            TaxName: $scope.TaxData.TaxName,
            TAlias: $scope.TaxData.TAlias,
            TPercent: $scope.TaxData.TPercent,
            TDiscription: $scope.TaxData.TDiscription,
            CompanyId: $scope.TaxData.CompanyId,
            Active: $scope.TaxData.Active,
            CreatedDate: $scope.TaxData.CreatedDate,
            UpdatedDate: $scope.TaxData.UpdatedDate,
            Deleted: $scope.TaxData.Deleted
        };
        console.log(dataToPost);


        $http.put(url, dataToPost)
        .success(function (data) {

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
                $modalInstance.close(data);
            }

        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);

         })

    };




}])

app.controller("ModalInstanceCtrldeletetax", ["$scope", '$http', "$modalInstance", "TaxMasterService", 'ngAuthSettings', 'taxmaster', function ($scope, $http, $modalInstance, TaxMasterService, ngAuthSettings, taxmaster) {
    console.log("delete modal opened");


    $scope.taxmasters = [];
    if (taxmaster) {
        $scope.TaxData = taxmaster;
        console.log("found TaxData");
        console.log(taxmaster);
        console.log($scope.TaxData);
      
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteTax = function (dataToPost, $index) {

        console.log("Delete  controller");
      

        TaxMasterService.deletesTaxmaster(dataToPost).then(function (results) {
            console.log("Del");

           // console.log("index of item " + $index);
            console.log($scope.taxmasters);
            
            $modalInstance.close(dataToPost);
           

        }, function (error) {
            alert(error.data.message);
        });
    }

}])