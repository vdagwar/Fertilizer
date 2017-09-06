﻿'use strict';
app.controller('categoryController', ['$scope', 'CategoryService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, CategoryService, $filter, $http, ngTableParams, $modal) {

    console.log(" category Controller reached");
    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myCategoryModal.html",
                controller: "ModalInstanceCtrlCategoryedit", resolve: { category: function () { return $scope.items } }
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
                templateUrl: "myCategoryModalPut.html",
                controller: "ModalInstanceCtrlCategoryedit", resolve: { category: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.categorys.push(selectedItem);
                _.find($scope.categorys, function (category) {
                    if (category.id == selectedItem.id) {
                        category = selectedItem;
                    }
                });
                $scope.categorys = _.sortBy($scope.categorys, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
            })
    };
    
    $scope.SetActive = function (item) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myactivemodal.html",
                controller: "ModalInstanceCtrlCategoryedit", resolve: { category: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.categorys.push(selectedItem);
                _.find($scope.categorys, function (category) {
                    if (category.id == selectedItem.id) {
                        category = selectedItem;
                    }
                });
                $scope.categorys = _.sortBy($scope.categorys, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log("Delete Dialog called for category");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteCategory.html",
                controller: "ModalInstanceCtrldeleteCategory", resolve: { category: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");           
            })
    };

    $scope.categorys = [];
    CategoryService.getcategorys().then(function (results) {
       $scope.categorys = results.data;
        $scope.callmethod();
    }, function (error) {       
    });

    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.categorys,
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

app.controller("ModalInstanceCtrlCategoryedit", ["$scope", '$http', 'ngAuthSettings', "CategoryService", "$modalInstance", "category", 'FileUploader', function ($scope, $http, ngAuthSettings, CategoryService, $modalInstance, category, FileUploader) {
    console.log("category");
    var input = document.getElementById("file");

    var today = new Date();
    $scope.today = today.toISOString();

    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });

    ////for image
    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        console.log(files);
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                console.log(config.file.name);

                console.log("File Name is " + $scope.uploadedfileName);
                var fileuploadurl = '/api/logoUpload/post', files;
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

    $scope.BaseCategories = [];
    var caturl = serviceBase + "api/BaseCategory";
    $http.get(caturl)
     .success(function (data) {
         console.log("Got Here");
         console.log(data);
         $scope.BaseCategories = data;
     })
      .error(function (data) {
          console.log("Error Got Heere is ");
          console.log(data);
      })

    $scope.CategoryData = {  };
    if (category) {
        console.log("category if conditon");
        $scope.CategoryData = category;
        console.log($scope.CategoryData.CategoryName);
    }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.AddCategory = function (data) {   
        console.log("Add category");   
        var LogoUrl = serviceBase + "../../UploadedLogos/" + $scope.uploadedfileName;
            console.log(LogoUrl);
            console.log("Image name in Insert function :" + $scope.uploadedfileName);
            $scope.CategoryData.LogoUrl = LogoUrl;
            console.log($scope.CategoryData.LogoUrl);
            var url = serviceBase + "api/Category";
            var dataToPost = {
                LogoUrl: $scope.CategoryData.LogoUrl,
                BaseCategoryId: $scope.CategoryData.BaseCategoryId,
                Categoryid: $scope.CategoryData.Categoryid,
                CategoryName: $scope.CategoryData.CategoryName,
                Discription: $scope.CategoryData.Discription,
                CreatedDate: $scope.CategoryData.CreatedDate,
                UpdatedDate: $scope.CategoryData.UpdatedDate,
                CreatedBy: $scope.CategoryData.CreatedBy,
                UpdateBy: $scope.CategoryData.UpdateBy,
                Code: $scope.CategoryData.Code,
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
    };
    
    $scope.PutCategory = function (data) {
        $scope.CategoryData = {  };
        $scope.loogourl = category.LogoUrl;

        if (category) {
            $scope.CategoryData = category;
            console.log("found Puttt category");
            console.log(category);
            console.log($scope.CategoryData);
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update category");
        if ($scope.uploadedfileName == null || $scope.uploadedfileName == '') {
            console.log("if looppppppppppp");
            var url = serviceBase + "api/Category";
            var dataToPost = {
                LogoUrl: $scope.loogourl,
                Categoryid: $scope.CategoryData.Categoryid,
                BaseCategoryId: $scope.CategoryData.BaseCategoryId,
                CategoryName: $scope.CategoryData.CategoryName,
                Discription: $scope.CategoryData.Discription,
                CreatedDate: $scope.CategoryData.CreatedDate,
                UpdatedDate: $scope.CategoryData.UpdatedDate,
                CreatedBy: $scope.CategoryData.CreatedBy,
                UpdateBy: $scope.CategoryData.UpdateBy,
                IsActive: $scope.CategoryData.IsActive,
                Code: $scope.CategoryData.Code,
            };
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
        }
        else {
            console.log("Else loppppppppp ");
            var LogoUrl = serviceBase + "../../UploadedLogos/" + $scope.uploadedfileName;
            console.log(LogoUrl);
            console.log("Image name in Insert function :" + $scope.uploadedfileName);
            $scope.CategoryData.LogoUrl = LogoUrl;
            console.log($scope.CategoryData.LogoUrl);
            var url = serviceBase + "api/Category";
            var dataToPost = {
                LogoUrl: $scope.CategoryData.LogoUrl,
                Categoryid: $scope.CategoryData.Categoryid,
                CategoryName: $scope.CategoryData.CategoryName,
                Discription: $scope.CategoryData.Discription,
                CreatedDate: $scope.CategoryData.CreatedDate,
                UpdatedDate: $scope.CategoryData.UpdatedDate,
                CreatedBy: $scope.CategoryData.CreatedBy,
                UpdateBy: $scope.CategoryData.UpdateBy,
                IsActive: $scope.CategoryData.IsActive,
                Code: $scope.CategoryData.Code
            };
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
        }
    };
    /////////////////////////////////////////////////////// angular upload code
    var uploader = $scope.uploader = new FileUploader({
        url: serviceBase + 'api/logoUpload'
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

app.controller("ModalInstanceCtrldeleteCategory", ["$scope", '$http', "$modalInstance", "CategoryService", 'ngAuthSettings', "category", function ($scope, $http, $modalInstance, CategoryService, ngAuthSettings, category) {
    console.log("delete modal opened");
    function ReloadPage() {
        location.reload();
    };
    $scope.categorys = [];
    if (category) {
        $scope.CategoryData = category;
        console.log("found category");
        console.log(category.Categoryid);
        console.log($scope.CategoryData);      
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.deleteCategory = function (dataToPost, $index) {
        console.log("Delete  category controller");
        CategoryService.deleteCategorys(dataToPost).then(function (results) {
            console.log("Del");
            console.log(results);
            console.log("index of item " + $index);
            console.log($scope.categorys);
            $modalInstance.close(dataToPost);
        }, function (error) {
            alert(error.data.message);
        });
    }
}])