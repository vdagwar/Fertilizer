﻿'use strict';
app.controller('assetsController', ['$scope', 'AssetsService', 'AssetsCategoryService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, AssetsService, AssetsCategoryService, $filter, $http, ngTableParams, $modal) {

    console.log(" assets  Controller reached");

    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened assets ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myAssetsModal.html",
                controller: "ModalInstanceCtrlAssets", resolve: { assets: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {


               // $scope.currentPageStores.push(selectedItem);

                //$scope.tableParams.reload();
                //$scope.categorys = _.sortBy($scope.categorys, 'Id').reverse();
                //$scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
                // $scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //$log.info("Modal dismissed at: " + new Date)
            })
    };


    $scope.edit = function (item) {
        console.log("Edit Dialog called assets ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myAssetsModalPut.html",
                controller: "ModalInstanceCtrlAssets", resolve: { assets: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.assetss.push(selectedItem);
                _.find($scope.assetss, function (assets) {
                    if (assets.id == selectedItem.id) {
                        assets = selectedItem;
                    }
                });

                $scope.assetss = _.sortBy($scope.assetss, 'Id').reverse();
                $scope.selected = selectedItem;
                //$scope.customers.push(selectedItem);

                //$scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //$scope.selected = selectedItem;


            },
            function () {
                console.log("Cancel Condintion");
                // $scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //  $log.info("Modal dismissed at: " + new Date)
            })
    };

    $scope.opendelete = function (data) {
        console.log(data);
        console.log("Delete Dialog called for assets ");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteAssets.html",
                controller: "ModalInstanceCtrldeleteAssets", resolve: { assets: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {


                //$scope.tasktypes.push(selectedItem);

                //_.filter($scope.tasktypes, function (a) {

                //    if (a.id == selectedItem.id) {

                //        a.Name = selectedItem.Name;
                //        a.Desc = selectedItem.Desc;
                //    }

                //});

                //$scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //$scope.selected = selectedItem;


            },
            function () {
                console.log("Cancel Condintion");
                // $scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //  $log.info("Modal dismissed at: " + new Date)
            })
    };

    $scope.assetss = [];

    AssetsService.getassetss().then(function (results) {

        $scope.assetss = results.data;


        $scope.callmethod();
    }, function (error) {
        //alert(error.data.message);
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.assetss,

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

app.controller("ModalInstanceCtrlAssets", ["$scope", '$http', 'ngAuthSettings', "AssetsService", 'AssetsCategoryService', "$modalInstance", 'FileUploader', "assets", function ($scope, $http, ngAuthSettings, AssetsService, AssetsCategoryService, $modalInstance, FileUploader, assets) {
    console.log("assets");


    var input = document.getElementById("file");
    console.log(input);

    var today = new Date();
    $scope.today = today.toISOString();

    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });

    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        console.log(files);
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                console.log(config.file.name);

                console.log("File Name is " + $scope.uploadedfileName);
                var fileuploadurl = '/api/upload/post', files;
                $upload.upload({
                    url: fileuploadurl,
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: file
                }).progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    console.log('progress: ' + progressPercentage + '% ' +
                                evt.config.file.name);
                }).success(function (data, status, headers, config) {


                    console.log('file ' + config.file.name + 'uploaded. Response: ' +
                                JSON.stringify(data));
                    cosole.log("uploaded");
                });
            }
        }
    };






    $scope.AssetsData = {

    };

    $scope.assetscategorys = [];
    AssetsCategoryService.getassetscategorys().then(function (results) {
        $scope.assetscategorys = results.data;
        //$scope.callmethod();
    }, function (error) {
        //alert(error.data.message);
    });


    //$scope.assetss = [];
    //AssetsService.getassetss().then(function (results) {
    //    $scope.assetss = results.data;
    //    //$scope.callmethod();
    //}, function (error) {
    //    //alert(error.data.message);
    //});


    if (assets) {
        console.log("assets if conditon");

        $scope.AssetsData = assets;

        console.log($scope.AssetsData.AssetCategoryName);
        //console.log($scope.ProjectData.Description);
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddAssets = function (data) {

         console.log("Assets");

         var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
         console.log(FileUrl);
         console.log("Image name in Insert function :" + $scope.uploadedfileName);
         $scope.AssetsData.FileUrl = FileUrl;
         console.log($scope.AssetsData.FileUrl);



         var url = serviceBase + "api/Assetss";
         var dataToPost = { AssetCategoryId: $scope.AssetsData.AssetCategoryId, AssetCategoryName:$scope.AssetsData.AssetCategoryName, FileUrl: $scope.AssetsData.FileUrl, LastOwnership: $scope.AssetsData.LastOwnership, VendorAddress: $scope.AssetsData.VendorAddress, PurchaseDate: $scope.AssetsData.PurchaseDate, SerialNumber: $scope.AssetsData.SerialNumber, PONumber: $scope.AssetsData.PONumber, AssetCost: $scope.AssetsData.AssetCost, ModelNumber: $scope.AssetsData.ModelNumber, WarrantyPeriod: $scope.AssetsData.WarrantyPeriod, VendorContactNo: $scope.AssetsData.VendorContactNo, LastUseDate: $scope.AssetsData.LastUseDate, CreatedDate: $scope.AssetsData.CreatedDate, UpdatedDate: $scope.AssetsData.UpdatedDate, CreatedBy: $scope.AssetsData.CreatedBy, UpdateBy: $scope.AssetsData.UpdateBy };
         console.log("$scope.AssetsData.AssetId");
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
                 //console.log(data);
                 //  console.log(data);
                 $modalInstance.close(data);
             }

         })
          .error(function (data) {
              console.log("Error Got Heere is ");
              console.log(data);
              // return $scope.showInfoOnSubmit = !0, $scope.revert()
          })
     };

    $scope.PutAssets = function (data) {
        $scope.AssetsData = {

        };
        if (assets) {
            $scope.AssetsData = assets;
            console.log("found Puttt Assets ");
            console.log(assets);
            console.log($scope.AssetsData);
           
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update assets ");

        var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
        console.log(FileUrl);
        console.log("Image name in Insert function :" + $scope.uploadedfileName);
        $scope.AssetsData.FileUrl = FileUrl;
        console.log($scope.AssetsData.FileUrl);

        var url = serviceBase + "api/Assetss";
        var dataToPost = {AssetId:$scope.AssetsData.AssetId, AssetCategoryId: $scope.AssetsData.AssetCategoryId, AssetCategoryName:$scope.AssetsData.AssetCategoryName, FileUrl: $scope.AssetsData.FileUrl, LastOwnership: $scope.AssetsData.LastOwnership, VendorAddress: $scope.AssetsData.VendorAddress, PurchaseDate: $scope.AssetsData.PurchaseDate, SerialNumber: $scope.AssetsData.SerialNumber, PONumber: $scope.AssetsData.PONumber, AssetCost: $scope.AssetsData.AssetCost, ModelNumber: $scope.AssetsData.ModelNumber, WarrantyPeriod: $scope.AssetsData.WarrantyPeriod, VendorContactNo: $scope.AssetsData.VendorContactNo, LastUseDate: $scope.AssetsData.LastUseDate, CreatedDate: $scope.AssetsData.CreatedDate, UpdatedDate: $scope.AssetsData.UpdatedDate, CreatedBy: $scope.AssetsData.CreatedBy, UpdateBy: $scope.AssetsData.UpdateBy };
       

        console.log(dataToPost);


        $http.put(url, dataToPost)
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

             // return $scope.showInfoOnSubmit = !0, $scope.revert()
         })

    };

    /////////////////////////////////////////////////////// angular upload code


    var uploader = $scope.uploader = new FileUploader({
        url: serviceBase + 'api/upload'
    });

    //FILTERS

    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });

    //CALLBACKS

    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
        console.info('onWhenAddingFileFailed', item, filter, options);
    };
    uploader.onAfterAddingFile = function (fileItem) {
        console.info('onAfterAddingFile', fileItem);
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
        console.info('onAfterAddingAll', addedFileItems);
    };
    uploader.onBeforeUploadItem = function (item) {
        console.info('onBeforeUploadItem', item);
    };
    uploader.onProgressItem = function (fileItem, progress) {
        console.info('onProgressItem', fileItem, progress);
    };
    uploader.onProgressAll = function (progress) {
        console.info('onProgressAll', progress);
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        console.info('onSuccessItem', fileItem, response, status, headers);
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        console.info('onErrorItem', fileItem, response, status, headers);
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
        console.info('onCancelItem', fileItem, response, status, headers);
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        console.info('onCompleteItem', fileItem, response, status, headers);
        console.log("File Name :" + fileItem._file.name);
        $scope.uploadedfileName = fileItem._file.name;
    };
    uploader.onCompleteAll = function () {
        console.info('onCompleteAll');
    };

    console.info('uploader', uploader);



}])

app.controller("ModalInstanceCtrldeleteAssets", ["$scope", '$http', "$modalInstance", "AssetsService", 'ngAuthSettings', "assets", function ($scope, $http, $modalInstance, AssetsService, ngAuthSettings, assets) {
    console.log("delete modal opened");


    //$scope.CategoryData = {

    //};

    //$scope.currentPageStores = [];
    $scope.assetss = [];

    if (assets) {
        $scope.AssetsData = assets;
        console.log("found assets ");
        console.log(assets);
        console.log($scope.AssetsData);
        //console.log($scope.Customer.name);
        //console.log($scope.Customer.description);
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteassetss = function (dataToPost, $index) {

        console.log("Delete  assets  controller");
        //alert(Id);
        //Id = window.encodeURIComponent(Id);

        AssetsService.deleteassetss(dataToPost).then(function (results) {
            console.log("Del");

            console.log("index of item " + $index);
            console.log($scope.assetss.length);
            $scope.assetss.splice($index, 1);
            console.log($scope.assetss.length);
            //$scope.Deletecategorys.splice($index, 1);

            $modalInstance.close(dataToPost);

        }, function (error) {
            alert(error.data.message);
        });
    }

}])