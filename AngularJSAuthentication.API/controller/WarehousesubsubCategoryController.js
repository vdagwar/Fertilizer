'use strict';
app.controller('WarehousesubsubCategoryController', ['$scope', 'WarehouseSubsubCategoryService', 'WarehouseCategoryService', 'WarehouseSubCategoryService', "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, WarehouseSubsubCategoryService, WarehouseCategoryService, WarehouseSubCategoryService, $filter, $http, ngTableParams, $modal, FileUploader) {
                                                                
    console.log(" Warehouse subsubcategory  Controller reached");

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
        var uploadURL = "/api/quesansupload/post"; //Upload URL
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
                templateUrl: "myWarehouseSubsubcategoryModal.html",
                controller: "ModalInstanceCtrlwhSubsubcat", resolve: { whsubsubcat: function () { return $scope.items } }
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
                templateUrl: "myWarehouseSubsubCategoryPut.html",
                controller: "ModalInstanceCtrlwhSubsubcat", resolve: { whsubsubcat: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.whsubsubcats.push(selectedItem);
                _.find($scope.whsubsubcats, function (whsubsubcat) {
                    if (whsubsubcat.id == selectedItem.id) {
                        whsubsubcat = selectedItem;
                    }
                });

                $scope.whsubsubcats = _.sortBy($scope.whsubsubcats, 'Id').reverse();
                $scope.selected = selectedItem;
   
            },
            function () {
                console.log("Cancel Condintion");
              
            })

    };
    $scope.opendelete = function (data) {
        console.log(data);
        console.log("Delete Dialog called for whsubsubcategory ");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeletWarehouseSubsubCategory.html",
                controller: "ModalInstanceCtrldeletewhsubsubcat", resolve: { whsubsubcat: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {

            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };

    $scope.Whsubsubcats = [];
    WarehouseSubsubCategoryService.getwhsubsubcats().then(function (results) {
        $scope.Whsubsubcats = results.data;
        $scope.callmethod();
    }, function (error) {
        
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.Whsubsubcats,

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

app.controller("ModalInstanceCtrlwhSubsubcat", ["$scope", '$http', 'ngAuthSettings', "WarehouseSubsubCategoryService", 'WarehouseCategoryService', 'WarehouseSubCategoryService', "$modalInstance", 'FileUploader', "whsubsubcat", function ($scope, $http, ngAuthSettings, WarehouseSubsubCategoryService, WarehouseCategoryService, WarehouseSubCategoryService, $modalInstance, FileUploader, whsubsubcat) {

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



    console.log("Whsubsubcat");
    $scope.WhSubsubCategoryData =
        {
         };

    $scope.Whcategorys = [];
    WarehouseCategoryService.getwhcategorys().then(function (results) {
        $scope.Whcategorys = results.data;
    }, function (error) {
    });


    $scope.Whsubcategorys = [];
    WarehouseSubCategoryService.getwhsubcategorys().then(function (results) {
        $scope.Whsubcategorys = results.data;
        }, function (error) {
        });

    

    if (whsubsubcat) {
        console.log(" if conditon");
        $scope.WhSubsubCategoryData = whsubsubcat;
        console.log($scope.WhSubsubCategoryData.WhCategoryName);
    }
      $scope.ok = function () { $modalInstance.close(); },
      $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


      $scope.AddWhSubsubCategorys = function (data) {

          //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
          //console.log(FileUrl);
          //console.log("Image name in Insert function :" + $scope.uploadedfileName);
          //$scope.SubsubCategoryData.FileUrl = FileUrl;
          //console.log($scope.SubsubCategoryData.FileUrl);



          console.log("AddWhSubsubCategorys ");
          var url = serviceBase + "api/WarehouseSubsubCategory";
          var dataToPost = { WhSubsubCategoryid: $scope.WhSubsubCategoryData.WhSubsubCategoryid, WhSubsubcategoryName: $scope.WhSubsubCategoryData.WhSubsubcategoryName, WhCategoryid: $scope.WhSubsubCategoryData.WhCategoryid, WhCategoryName: $scope.WhSubsubCategoryData.WhCategoryName, WhSubCategoryId: $scope.WhSubsubCategoryData.WhSubCategoryId, WhSubcategoryName: $scope.WhSubsubCategoryData.WhSubcategoryName, CreatedDate: $scope.WhSubsubCategoryData.CreatedDate, UpdatedDate: $scope.WhSubsubCategoryData.UpdatedDate, CreatedBy: $scope.WhSubsubCategoryData.CreatedBy, UpdateBy: $scope.WhSubsubCategoryData.UpdateBy };


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

      $scope.PutWhSubsubCategory = function (data) {
          $scope.WhSubsubCategoryData = {

                  };
                  if (whsubsubcat) {
                      $scope.WhSubsubCategoryData = whsubsubcat;
                      console.log("found Puttt subsubcat ");
                      console.log(whsubsubcat);
                      console.log($scope.WhSubsubCategoryData);

                  }
                  $scope.ok = function () { $modalInstance.close(); },
              $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

                  console.log("Update WhSubsubCategory ");

                  //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
                  //console.log(FileUrl);
                  //console.log("Image name in Insert function :" + $scope.uploadedfileName);
                  //$scope.SubsubCategoryData.FileUrl = FileUrl;
                  //console.log($scope.SubsubCategoryData.FileUrl);



                  console.log("WhSubsubCategory");
                  var url = serviceBase + "api/WarehouseSubsubCategory";
                  var dataToPost = { WhSubsubCategoryid: $scope.WhSubsubCategoryData.WhSubsubCategoryid, WhSubsubcategoryName: $scope.WhSubsubCategoryData.WhSubsubcategoryName, WhCategoryid: $scope.WhSubsubCategoryData.WhCategoryid, WhCategoryName: $scope.WhSubsubCategoryData.WhCategoryName, WhSubCategoryId: $scope.WhSubsubCategoryData.WhSubCategoryId, WhSubcategoryName: $scope.WhSubsubCategoryData.WhSubcategoryName, CreatedDate: $scope.WhSubsubCategoryData.CreatedDate, UpdatedDate: $scope.WhSubsubCategoryData.UpdatedDate, CreatedBy: $scope.WhSubsubCategoryData.CreatedBy, UpdateBy: $scope.WhSubsubCategoryData.UpdateBy };

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

app.controller("ModalInstanceCtrldeletewhsubsubcat", ["$scope", '$http', "$modalInstance", "WarehouseSubsubCategoryService", 'ngAuthSettings', "whsubsubcat", function ($scope, $http, $modalInstance, WarehouseSubsubCategoryService, ngAuthSettings, whsubsubcat) {
    console.log("delete modal opened");

    $scope.whsubsubcats = [];

    if (whsubsubcat) {
        $scope.WhSubsubCategoryData = whsubsubcat;
        console.log("found whsubsubcat ");
        console.log($scope.SubsubCategoryData);
   
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteswhubsubcategory = function (dataToPost, $index) {
        console.log(dataToPost);
        console.log("Delete  subsubcat  controller");
      
        WarehouseSubsubCategoryService.deletewhsubsubcategorys(dataToPost).then(function (results) {
            console.log("Del");

            console.log("index of item " + $index);
            console.log($scope.whsubsubcats.length);
            $scope.whsubsubcats.splice($index, 1);
            console.log($scope.whsubsubcats.length);
        
            $modalInstance.close(dataToPost);

        }, function (error) {
            alert(error.data.message);
        });
    }

}])