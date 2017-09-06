'use strict';
app.controller('WarehousecategoryController',
    ['$scope', 'WarehouseCategoryService', 'CityService', 'StateService', "WarehouseService", "CategoryService", "$filter", "$http", "ngTableParams", '$modal',
        function ($scope, WarehouseCategoryService, CityService, StateService, WarehouseService, CategoryService, $filter, $http, ngTableParams, $modal)
        {

    console.log(" category Controller reached");




    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }


    function sendFileToServer(formData, status) {

       

        var uploadURL = "/api/WarehouseCategoryUpload/post"; //Upload URL
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
            $("body").on("drop", "#dragandrophandler", function () {
                var allowedFiles = [".xlsx"];
                var fileUpload = $("#fileUpload");
                var lblError = $("#lblError");
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                if (!regex.test(fileUpload.val().toLowerCase())) {
                    lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
                    return false;
                }
                lblError.html('');
                return true;
            });
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

        //$("body").on("drop", "#dragandrophandler", function () {
        //    var allowedFiles = [".xlsx"];
        //    var fileUpload = $("#fileUpload");
        //    var lblError = $("#lblError");
        //    var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
        //    if (!regex.test(fileUpload.val().toLowerCase())) {
        //        lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
        //        return false;
        //    }
        //    lblError.html('');
        //    return true;
        //});

    });

            //............................File Uploader Method End.....................//

            //............................Exel export Method.....................//

    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData1 = function () {
        alasql('SELECT Name,Description,Address,CustomerCategoryName,CreatedDate INTO XLSX("Customer.xlsx",{headers:true}) FROM ?', [$scope.stores]);
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("Customer.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };


    $scope.currentPageStores = {};

   

    $scope.open = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mywhCategoryModal.html",
                controller: "ModalInstanceCtrlwhCategory", resolve: { whcategory: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {

            },
            function () {
              
            })
    };


    $scope.edit = function (item) {
        
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myWhCategoryModalPut.html",
                controller: "ModalInstanceCtrlwhCategory", resolve: { whcategory: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                
                $scope.whcategorys.push(selectedItem);
                _.find($scope.whcategorys, function (whcategory) {
                    if (whcategory.id == selectedItem.id) {
                        whcategory = selectedItem;
                    }
                });

                $scope.whcategorys = _.sortBy($scope.whcategorys, 'Id').reverse();
                $scope.selected = selectedItem;
            
            },
            function () {
              
            })
    };

    $scope.opendelete = function (data, $index) {
        
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteWhCategory.html",
                controller: "ModalInstanceCtrlWarehousedeleteCategory", resolve: { whcategory: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);

            },
            function () {
               
            })
        
    };

    $scope.whcategorys = [];

    WarehouseCategoryService.getwhcategorys().then(function (results) {
       $scope.whcategorys = results.data;
        $scope.callmethod();
    }, function (error) {
       
    });

   
    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.whcategorys,

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

app.controller("ModalInstanceCtrlwhCategory",
    ["$scope", '$http', 'ngAuthSettings', "WarehouseCategoryService", 'CityService', 'StateService', "WarehouseService", "CategoryService", "$modalInstance", 'FileUploader','whcategory',
        function ($scope, $http, ngAuthSettings, WarehouseCategoryService, CityService, StateService, WarehouseService, CategoryService, $modalInstance, FileUploader, whcategory)
        {
    console.log("Warehousecategory");


    function ReloadPage() {
        location.reload();
    };
    var today = new Date();
    $scope.today = today.toISOString();

    var input = document.getElementById("file");
    console.log(input);

   
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

   
    $scope.WarhouseStates = [];
    WarehouseService.getwarehousedistinctstates().then(function (results) {
        console.log("This is warehouse state");
        console.log(results.data);
        $scope.WarhouseStates = results.data;
    }, function (error) {
    });

    $scope.citys = [];
    CityService.getcitys().then(function (results) {
        console.log("This is city in city master");
        $scope.citys = results.data;
    }, function (error) {
    });

    $scope.warehouses = []
    WarehouseService.getwarehouse().then(function (results) {
        console.log("This is Warehouse name in Warehouse master");
        $scope.warehouses = results.data;
        console.log($scope.warehouses);
        
    }, function (error) {

    });
   
    
    $scope.WhCategoryData = {};

    $scope.Wcategorys = [];
    $scope.WhCategoryold = [];
    $scope.WhCategorynew = [];
    $scope.GetWarhouseCategory = function (WareHouse) {
        console.log(WareHouse);
        
        console.log("get Warehousecategory controller");
        console.log(WareHouse);
        CategoryService.getWarhouseCategory(WareHouse).then(function (results) {
            console.log(results.data);
            console.log("get Warehousecategory Wcategorys");
            $scope.Wcategorys = results.data;
            $scope.WhCategoryold = $scope.Wcategorys;
            console.log($scope.Wcategorys);
        }, function (error) { });

    }

    if (whcategory) {
        console.log("start wAREHOUSE Category BIND edit...");
        $scope.WhCategoryData = whcategory;
        console.log($scope.WhCategoryData);

    }
  
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddWhCategory = function (data) {
         console.log(" Group  entity Data");
         console.log(data);
         console.log(" single entity Data");
         console.log($scope.WhCategoryold);



         var url = serviceBase + "api/WarehouseCategory";
         var dataToPost = data;
         console.log("DATA:");
         console.log(dataToPost);
         dataToPost[0].Discription = $scope.WhCategoryData.Discription;
         console.log("DATA+ DISCRIPTION:");
         console.log(dataToPost);

         $http.post(url,dataToPost)
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
                 //ReloadPage();
             }

         })
          .error(function (data) {
              console.log("Error Got Heere is ");
              console.log(data);
              // return $scope.showInfoOnSubmit = !0, $scope.revert()
          })
     };

//------------------------------ Update Model--------------------------------------------------------------------------------------
   
    
    $scope.WcategorysPut = [];
    $scope.GetWarhouseCategoryById = function (WareHouse) {
        console.log(WareHouse);
        
        console.log("get Warehousecategory controller");
        console.log(WareHouse);
        CategoryService.getWarhouseCategorybyid(WareHouse).then(function (results) {
            console.log(results.data);
            console.log("get Warehousecategory Wcategorys");
            $scope.WcategorysPut = results.data;

        }, function (error) { });

    }
    $scope.PutWhCategory = function (data) {
       
        $scope.WhCategoryData = { };
        console.log(data);
        if (whcategory) {
            console.log("start Category edit...");
            $scope.WhCategoryData = whcategory;
           
            console.log(whcategory);
            console.log($scope.WhCategoryData);
         
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update category");

        var url = serviceBase + "api/WarehouseCategory";
       
        var dataToPost = data;
        //dataToPost[0].WhCategoryid = $scope.WhCategoryData.Discription
        dataToPost[0].Discription = $scope.WhCategoryData.Discription;
        console.log(dataToPost);

        $http.put(url, dataToPost)
        .success(function (data) {
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
                //ReloadPage();
            }

        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);

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

app.controller("ModalInstanceCtrlWarehousedeleteCategory", ["$scope", '$http', "$modalInstance", "WarehouseCategoryService", 'ngAuthSettings', "whcategory", function ($scope, $http, $modalInstance, WarehouseCategoryService, ngAuthSettings, whcategory) {
    console.log("delete modal opened");
   
    $scope.Whcategorys = [];


    if (whcategory) {
        $scope.WhCategoryData = whcategory;
       
      
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteWhCategory = function (dataToPost, $index) {


        WarehouseCategoryService.deleteWhCategorys(dataToPost).then(function (results) {
           
            $modalInstance.close(dataToPost);
           // ReloadPage();
        }, function (error) {
            alert(error.data.message);
        });
    }

}])