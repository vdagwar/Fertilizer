'use strict';
app.controller('WarehousesubCategoryController',
    ['$scope', 'WarehouseSubCategoryService', "WarehouseCategoryService", 'CityService', 'StateService', "WarehouseService", "SubCategoryService", "$filter", "$http", "ngTableParams", '$modal',
    function ($scope, WarehouseSubCategoryService, WarehouseCategoryService, CityService, StateService, WarehouseService, SubCategoryService, $filter, $http, ngTableParams, $modal)
    {

    console.log("WarehouseSubCategoryController reached");

    $scope.WarehousecurrentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened sub category");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myWhSubCategoryModal.html",
                controller: "ModalInstanceCtrlWarehouseSubCategory", resolve: { whsubcategory: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {


                $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion");
             
            })
    };


    $scope.edit = function (item) {
        console.log("Edit Dialog called subcategory");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myWhSubCategoryModalPut.html",
                controller: "ModalInstanceCtrlWarehouseSubCategory", resolve: { whsubcategory: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.whsubcategorys.push(selectedItem);
                _.find($scope.whsubcategorys, function (whsubcategory) {
                    if (whsubcategory.id == selectedItem.id) {
                        whsubcategory = selectedItem;
                    }
                });

                $scope.whsubcategorys = _.sortBy($scope.whsubcategorys, 'Id').reverse();
                $scope.selected = selectedItem;
         
            },
            function () {
                console.log("Cancel Condintion");
               
            })
    };

    $scope.opendelete = function (data) {
        console.log(data);
        console.log("Delete Dialog called for whsubcategory");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeletewhSubCategory.html",
                controller: "ModalInstanceCtrlWarehousedeletesubcategory", resolve: { whsubcategory: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {


            },
            function () {
                console.log("Cancel Condintion");
             
            })
    };

    $scope.whsubcategorys = [];

    WarehouseSubCategoryService.getwhsubcategorys().then(function (results) {
        console.log("ingetfn");
        console.log(results.data);
        $scope.whsubcategorys = results.data;
      
        $scope.callmethod();
    }, function (error) {
      
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.whsubcategorys,

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

app.controller("ModalInstanceCtrlWarehouseSubCategory",
    ["$scope", '$http', 'ngAuthSettings', "WarehouseSubCategoryService", "WarehouseCategoryService", 'CityService', 'StateService', "WarehouseService", "SubCategoryService", "$modalInstance", 'FileUploader',
        function ($scope, $http, ngAuthSettings, WarehouseSubCategoryService, WarehouseCategoryService, CityService, StateService, WarehouseService, SubCategoryService, $modalInstance, FileUploader) {
    console.log("Warehousesubcategory");

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





    $scope.WhSubCategoryData = {};
//-------------------------------------------------------------------------------------------------------------------------------------------
    $scope.WarhouseStates = [];
    WarehouseService.getwarehousedistinctstates().then(function (results) {
        console.log("show state");
        console.log(results.data);
        $scope.WarhouseStates = results.data;
    }, function (error) {
    });

    $scope.citys = [];
    CityService.getcitys().then(function (results) {
        $scope.citys = results.data;
    }, function (error) {
    });

    $scope.warehouses = []
    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouses = results.data;
        //$scope.callmethod();
    }, function (error) {

    });


    //$scope.WhCategoryData = {};

    $scope.Wsubcategorys = [];
    $scope.GetWarhouseCategory = function (WareHouse) {
        console.log(WareHouse);
        console.log("sumit get  Warehouse  sub category controller");
        console.log(WareHouse);
        SubCategoryService.getWarhouseSubCategory(WareHouse).then(function (results) {
            console.log(results.data);
            console.log("get sassas Warehouse sub category Wcategorys");


            $scope.Wsubcategorys = results.data;

        }, function (error) { });

    }
//-------------------------------------------------------------------------------------------------------------------------------

    //if (whsubcategory) {
    //    console.log("WarehouseSubCategory if conditon");

    //    $scope.WhSubCategoryData = whsubcategory;
    //    console.log("kkkkkk");
    //    console.log($scope.WhSubCategoryData.WhCategoryid);
    //    console.log($scope.WhSubCategoryData.WhSubcategoryName);
       
    //}


    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddWhSubCategory = function (data) {

         console.log(" Group  entity Data");
         console.log(data);
         console.log(" single entity Data");


         var url = serviceBase + "api/WarehouseSubCategory";
         var dataToPost = data;
         dataToPost[0].Discription = $scope.WhCategoryData.Discription;

         console.log("sumit");
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

//-------------------------------------------------------------------------------------------------------------------

    $scope.PutWhSubcategory = function (data) {
        $scope.WhSubCategoryData = {

        };
        if (whsubcategory) {
            $scope.WhSubCategoryData = whsubcategory;
            console.log("found Puttt SubCategory");
            console.log(whsubcategory);
            console.log($scope.WhSubCategoryData);
           
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update sub WhSubCategory");

        //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
        //console.log(FileUrl);
        //console.log("Image name in Insert function :" + $scope.uploadedfileName);
        //$scope.AssetsCategoryData.FileUrl = FileUrl;
        //console.log($scope.AssetsCategoryData.FileUrl);


        var url = serviceBase + "api/WarehouseSubCategory";
        var dataToPost = { WhSubCategoryId: $scope.WhSubCategoryData.WhSubCategoryId, WhSubcategoryName: $scope.WhSubCategoryData.WhSubcategoryName, Discription: $scope.WhSubCategoryData.Discription, CreatedDate: $scope.WhSubCategoryData.CreatedDate, UpdatedDate: $scope.WhSubCategoryData.UpdatedDate, CreatedBy: $scope.WhSubCategoryData.CreatedBy, UpdateBy: $scope.WhSubCategoryData.UpdateBy, WhCategoryid: $scope.WhSubCategoryData.WhCategoryid };


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

         })

    };


    /////////////////////////////////////////////////////// angular upload code


    //var uploader = $scope.uploader = new FileUploader({
    //    url: serviceBase + 'api/upload'
    //});

    ////FILTERS

    //uploader.filters.push({
    //    name: 'customFilter',
    //    fn: function (item /*{File|FileLikeObject}*/, options) {
    //        return this.queue.length < 10;
    //    }
    //});

    ////CALLBACKS

    //uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
    //    console.info('onWhenAddingFileFailed', item, filter, options);
    //};
    //uploader.onAfterAddingFile = function (fileItem) {
    //    console.info('onAfterAddingFile', fileItem);
    //};
    //uploader.onAfterAddingAll = function (addedFileItems) {
    //    console.info('onAfterAddingAll', addedFileItems);
    //};
    //uploader.onBeforeUploadItem = function (item) {
    //    console.info('onBeforeUploadItem', item);
    //};
    //uploader.onProgressItem = function (fileItem, progress) {
    //    console.info('onProgressItem', fileItem, progress);
    //};
    //uploader.onProgressAll = function (progress) {
    //    console.info('onProgressAll', progress);
    //};
    //uploader.onSuccessItem = function (fileItem, response, status, headers) {
    //    console.info('onSuccessItem', fileItem, response, status, headers);
    //};
    //uploader.onErrorItem = function (fileItem, response, status, headers) {
    //    console.info('onErrorItem', fileItem, response, status, headers);
    //};
    //uploader.onCancelItem = function (fileItem, response, status, headers) {
    //    console.info('onCancelItem', fileItem, response, status, headers);
    //};
    //uploader.onCompleteItem = function (fileItem, response, status, headers) {
    //    console.info('onCompleteItem', fileItem, response, status, headers);
    //    console.log("File Name :" + fileItem._file.name);
    //    $scope.uploadedfileName = fileItem._file.name;
    //};
    //uploader.onCompleteAll = function () {
    //    console.info('onCompleteAll');
    //};

    //console.info('uploader', uploader);


}])

app.controller("ModalInstanceCtrlWarehousedeletesubcategory", ["$scope", '$http', "$modalInstance", "WarehouseSubCategoryService", 'ngAuthSettings', "whsubcategory", function ($scope, $http, $modalInstance, WarehouseSubCategoryService, ngAuthSettings, whsubcategory) {
    console.log("delete modal opened");

    $scope.whsubcategorys = [];

    if (whsubcategory) {
        $scope.WhSubCategoryData = whsubcategory;
        console.log("found  subcategory");
        console.log(whsubcategory);
        console.log($scope.WhSubCategoryData);
     
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteWhsubCategory = function (dataToPost, $index) {

        console.log("Delete subcategorys");
    
        WarehouseSubCategoryService.deletewhsubcategorys(dataToPost).then(function (results) {
            console.log("Del");

            console.log("index of item " + $index);
           // console.log($scope.Whsubcategorys);
           // $scope.Whsubcategorys.splice($index, 1);
           // console.log($scope.Whsubcategorys);
       
            $modalInstance.close(dataToPost);

        }, function (error) {
            alert(error.data.message);
        });
    }

}])