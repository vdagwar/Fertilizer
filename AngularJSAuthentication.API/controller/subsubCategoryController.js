﻿'use strict';
app.controller('subsubCategoryController', ['$scope', 'SubsubCategoryService', 'CategoryService', 'SubCategoryService', "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, SubsubCategoryService, CategoryService, SubCategoryService, $filter, $http, ngTableParams, $modal, FileUploader) {

    console.log(" subsubcategory  Controller reached");

    $scope.currentPageStores = {};

    $scope.update = function ()
    {
        console.log($scope.selectedItem);
        if ($scope.selectedItem == "TextBox")
        {
            //$scope.thisDay();
        }
        else if ($scope.selectedItem == "RadioButton")
        {
            //$scope.thisWeek();
        }
        else if ($scope.selectedItem == "MultiSelect")
        {
            //$scope.thisMonth();
        }
      
    }

    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }

    function sendFileToServer(formData, status) {
        var uploadURL = "/api/subsubcategoryupload/post"; //Upload URL
        var extraData = {}; //Extra Data.
        var jqXHR = $.ajax({
            xhr: function () {
                var xhrobj = $.ajaxSettings.xhr();
                if (xhrobj.upload) {
                    xhrobj.upload.addEventListener('progress', function (event) {
                        var percent = 0;
                        var position = event.loaded || event.position;
                        var total = event.total;
                        if (event.lengthComputable) {
                            percent = Math.ceil(position / total * 100);
                        }
                        //Set progress
                        status.setProgress(percent);
                    }, false);
                }
                return xhrobj;
            },
            url: uploadURL,
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: formData,
            success: function (data) {
                status.setProgress(100);

                $("#status1").append("Data from Server:" + data + "<br>");
            }
        });

        status.setAbort(jqXHR);
    }

    var rowCount = 0;
    function createStatusbar(obj) {
        rowCount++;
        var row = "odd";
        if (rowCount % 2 == 0) row = "even";
        this.statusbar = $("<div class='statusbar " + row + "'></div>");
        this.filename = $("<div class='filename'></div>").appendTo(this.statusbar);
        this.size = $("<div class='filesize'></div>").appendTo(this.statusbar);
        this.progressBar = $("<div class='progressBar'><div></div></div>").appendTo(this.statusbar);
        this.abort = $("<div class='abort'>Abort</div>").appendTo(this.statusbar);
        obj.after(this.statusbar);

        this.setFileNameSize = function (name, size) {
            var sizeStr = "";
            var sizeKB = size / 1024;
            if (parseInt(sizeKB) > 1024) {
                var sizeMB = sizeKB / 1024;
                sizeStr = sizeMB.toFixed(2) + " MB";
            }
            else {
                sizeStr = sizeKB.toFixed(2) + " KB";
            }

            this.filename.html(name);
            this.size.html(sizeStr);
        }
        this.setProgress = function (progress) {
            var progressBarWidth = progress * this.progressBar.width() / 100;
            this.progressBar.find('div').animate({ width: progressBarWidth }, 10).html(progress + "%&nbsp;");
            if (parseInt(progress) >= 100) {
                this.abort.hide();
            }
        }
        this.setAbort = function (jqxhr) {
            var sb = this.statusbar;
            this.abort.click(function () {
                jqxhr.abort();
                sb.hide();
            });
        }
    }
    function handleFileUpload(files, obj) {
        for (var i = 0; i < files.length; i++) {
            var fd = new FormData();
            fd.append('file', files[i]);

            var status = new createStatusbar(obj); //Using this we can set progress.
            status.setFileNameSize(files[i].name, files[i].size);
            sendFileToServer(fd, status);

        }
    }
    $(document).ready(function () {
        var obj = $("#dragandrophandler");
        obj.on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $(this).css('border', '2px solid #0B85A1');
        });
        obj.on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });
        obj.on('drop', function (e) {

            $(this).css('border', '2px dotted #0B85A1');
            e.preventDefault();
            var files = e.originalEvent.dataTransfer.files;

            //We need to send dropped files to Server
            handleFileUpload(files, obj);
        });
        $(document).on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });
        $(document).on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
            obj.css('border', '2px dotted #0B85A1');
        });
        $(document).on('drop', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });

    });

    $scope.open = function () {
        console.log("Modal opened SubsubCategory ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mySubsubcategoryModal.html",
                controller: "ModalInstanceCtrlSubsubcatedit", resolve: { subsubcat: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                   $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion");

                
            })

    };
    $scope.edit = function (item) {
        console.log("Edit Dialog called SubsubCategory ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mySubsubCategoryPut.html",
                controller: "ModalInstanceCtrlSubsubcatedit", resolve: { subsubcat: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.subsubcats.push(selectedItem);
                _.find($scope.subsubcats, function (subsubcat) {
                    if (subsubcat.id == selectedItem.id) {
                        subsubcat = selectedItem;
                    }
                });

                $scope.subsubcats = _.sortBy($scope.subsubcats, 'Id').reverse();
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
                controller: "ModalInstanceCtrlSubsubcatedit", resolve: { subsubcat: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.subsubcats.push(selectedItem);
                _.find($scope.subsubcats, function (subsubcat) {
                    if (subsubcat.id == selectedItem.id) {
                        subsubcat = selectedItem;
                    }
                });

                $scope.subsubcats = _.sortBy($scope.subsubcats, 'Id').reverse();
                $scope.selected = selectedItem;

            },
            function () {

            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log("Delete Dialog called for subsubcategory ");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeletSubsubCategory.html",
                controller: "ModalInstanceCtrldeletesubsubcat", resolve: { subsubcat: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };

    $scope.subsubcats = [];
    SubsubCategoryService.getsubsubcats().then(function (results) {
        $scope.subsubcats = results.data;
        $scope.callmethod();
    }, function (error) {
        
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.subsubcats,

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

app.controller("ModalInstanceCtrlSubsubcatedit", ["$scope", '$http', 'ngAuthSettings', "SubsubCategoryService", 'CategoryService', 'SubCategoryService', "$modalInstance", 'FileUploader', "subsubcat", function ($scope, $http, ngAuthSettings, SubsubCategoryService, CategoryService, SubCategoryService, $modalInstance, FileUploader, subsubcat) {

    console.log(subsubcat);
    
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

    console.log("subsubcat");
    $scope.SubsubCategoryData = {};

    $scope.categorys = [];
    CategoryService.getcategorys().then(function (results) {
        $scope.categorys = results.data;
    }, function (error) {
    });

    $scope.subcategorys = [];
    SubCategoryService.getsubcategorys().then(function (results) {
        $scope.subcategorys = results.data;
    }, function (error) {
    });

    $scope.ssCatType = [
        { Type: "A", text: "A" },
        { Type: "B", text: "B" },
        { Type: "C", text: "C" },
    ];

    if (subsubcat) {
        console.log(" if conditon");
        $scope.SubsubCategoryData = subsubcat;
        console.log($scope.SubsubCategoryData.CategoryName);
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddSubsubCategorys = function (data) {

         //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
         //console.log(FileUrl);
         //console.log("Image name in Insert function :" + $scope.uploadedfileName);
         //$scope.SubsubCategoryData.FileUrl = FileUrl;
         //console.log($scope.SubsubCategoryData.FileUrl);
         var LogoUrl = serviceBase + "../../UploadedLogos/" + $scope.uploadedfileName;
         console.log(LogoUrl);
         console.log("Image name in Insert function :" + $scope.uploadedfileName);
         $scope.SubsubCategoryData.LogoUrl = LogoUrl;
         console.log($scope.SubsubCategoryData.LogoUrl);


         console.log("SubsubCategory");
         var url = serviceBase + "api/SubsubCategory";
         var dataToPost = {
             SubsubCategoryid: $scope.SubsubCategoryData.SubsubCategoryid,
             SubsubcategoryName: $scope.SubsubCategoryData.SubsubcategoryName,
             Categoryid: $scope.SubsubCategoryData.Categoryid,
             CategoryName: $scope.SubsubCategoryData.CategoryName,
             SubCategoryId: $scope.SubsubCategoryData.SubCategoryId,
             LogoUrl: $scope.SubsubCategoryData.LogoUrl,
             SubcategoryName: $scope.SubsubCategoryData.SubcategoryName,
             CreatedDate: $scope.SubsubCategoryData.CreatedDate,
             UpdatedDate: $scope.SubsubCategoryData.UpdatedDate,
             Type: $scope.SubsubCategoryData.Type,
             CreatedBy: $scope.SubsubCategoryData.CreatedBy,
             UpdateBy: $scope.SubsubCategoryData.UpdateBy,
             IsActive: $scope.SubsubCategoryData.IsActive,
             Code: $scope.SubsubCategoryData.Code
         };


         console.log("$scope.SubsubCategoryData.");
         console.log(dataToPost);



         $http.post(url, dataToPost)
         .success(function (data) {
             console.log(data);
             console.log($scope.subsubcat);
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

    $scope.PutSubsubCategory = function (data) {
        $scope.SubsubCategoryData = {

        };
        $scope.loogourl = subsubcat.LogoUrl;
        if (subsubcat) {
            $scope.SubsubCategoryData = subsubcat;
            console.log("found Puttt subsubcat ");
            console.log(subsubcat);
            console.log($scope.SubsubCategoryData);

        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update SubsubCategory ");
        if ($scope.uploadedfileName == null || $scope.uploadedfileName == '') {
            console.log("iffff   loooppppp");
            var url = serviceBase + "api/SubsubCategory";
            var dataToPost = {
                LogoUrl: $scope.loogourl,
                IsActive: $scope.SubsubCategoryData.IsActive,
                SubsubCategoryid: $scope.SubsubCategoryData.SubsubCategoryid,
                SubsubcategoryName: $scope.SubsubCategoryData.SubsubcategoryName,
                Categoryid: $scope.SubsubCategoryData.Categoryid,
                CategoryName: $scope.SubsubCategoryData.CategoryName,
                SubCategoryId: $scope.SubsubCategoryData.SubCategoryId,
                SubcategoryName: $scope.SubsubCategoryData.SubcategoryName,
                CreatedDate: $scope.SubsubCategoryData.CreatedDate,
                UpdatedDate: $scope.SubsubCategoryData.UpdatedDate,
                CreatedBy: $scope.SubsubCategoryData.CreatedBy,
                UpdateBy: $scope.SubsubCategoryData.UpdateBy,
                Type: $scope.SubsubCategoryData.Type,
                Code: $scope.SubsubCategoryData.Code
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

                 // return $scope.showInfoOnSubmit = !0, $scope.revert()
             })
        }
        else {
            console.log("else loooooooooopppp");


            var LogoUrl = serviceBase + "../../UploadedLogos/" + $scope.uploadedfileName;
            console.log(LogoUrl);
            console.log("Image name in Insert function :" + $scope.uploadedfileName);
            $scope.SubsubCategoryData.LogoUrl = LogoUrl;
            console.log($scope.SubsubCategoryData.LogoUrl);

            console.log("SubsubCategory");
            var url = serviceBase + "api/SubsubCategory";
            var dataToPost = {
                LogoUrl: $scope.SubsubCategoryData.LogoUrl,
                IsActive: $scope.SubsubCategoryData.IsActive,
                SubsubCategoryid: $scope.SubsubCategoryData.SubsubCategoryid,
                SubsubcategoryName: $scope.SubsubCategoryData.SubsubcategoryName,
                Categoryid: $scope.SubsubCategoryData.Categoryid,
                CategoryName: $scope.SubsubCategoryData.CategoryName,
                SubCategoryId: $scope.SubsubCategoryData.SubCategoryId,
                SubcategoryName: $scope.SubsubCategoryData.SubcategoryName,
                CreatedDate: $scope.SubsubCategoryData.CreatedDate,
                UpdatedDate: $scope.SubsubCategoryData.UpdatedDate,
                CreatedBy: $scope.SubsubCategoryData.CreatedBy,
                UpdateBy: $scope.SubsubCategoryData.UpdateBy,
                Code: $scope.SubsubCategoryData.Code
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

                 // return $scope.showInfoOnSubmit = !0, $scope.revert()
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


app.controller("ModalInstanceCtrldeletesubsubcat", ["$scope", '$http', "$modalInstance", "SubsubCategoryService", 'ngAuthSettings', "subsubcat", function ($scope, $http, $modalInstance, SubsubCategoryService, ngAuthSettings, subsubcat) {
    console.log("delete modal opened");
    function ReloadPage() {
        location.reload();
    };
    $scope.subsubcats = [];

    if (subsubcat) {
        $scope.SubsubCategoryData = subsubcat;
        console.log("found subsubcat ");
        console.log($scope.SubsubCategoryData);
   
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deletesubsubcategory = function (dataToPost, $index) {
        console.log(dataToPost);
        console.log("Delete  subsubcat  controller");
      
        SubsubCategoryService.deletesubsubcategorys(dataToPost).then(function (results) {
            console.log("Del");

            console.log("index of item " + $index);
            console.log($scope.subsubcats.length);
            //$scope.subsubcats.splice($index, 1);
            console.log($scope.subsubcats.length);
        
            $modalInstance.close(dataToPost);
            //ReloadPage();

        }, function (error) {
            alert(error.data.message);
        });
    }

}])