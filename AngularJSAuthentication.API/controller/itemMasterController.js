'use strict';
app.controller('itemMasterController', ['$scope', 'itemMasterService', 'SubsubCategoryService', 'SubCategoryService', 'CategoryService', 'unitMasterService', 'WarehouseService', "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, itemMasterService, SubsubCategoryService, SubCategoryService, CategoryService, unitMasterService, WarehouseService, $filter, $http, ngTableParams, $modal, FileUploader) {
     
    $scope.warehouse = [];
   $scope.warehousename = "W1";
    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouse = results.data;
        $scope.warehousename = $scope.warehouse[0].WarehouseName;
    }, function (error) {
    });
    
    // new pagination 
    $scope.pagenoOne = 0;
    $scope.pageno = 1; // initialize page no to 1
    $scope.total_count = 0;
    $scope.numPerPageOpt = [50, 100, 200];//dropdown options for no. of Items per page
    $scope.itemsPerPage = $scope.numPerPageOpt[0]; //this could be a dynamic value from a drop down
    $scope.onNumPerPageChange = function () {
        $scope.itemsPerPage = $scope.selectedPagedItem;
        $scope.getData1($scope.pageno);
    }
    $scope.selectedPagedItem = $scope.numPerPageOpt[0];// for Html page dropdown
    $scope.getData1 = function (pageno) { // This would fetch the data on page change.
        //In practice this should be in a factory.
        debugger;
        if ($scope.pagenoOne != pageno) {
            $scope.pagenoOne = pageno;
            $scope.itemMasters = [];
            var url = serviceBase + "api/itemMaster" + "?list=" + $scope.itemsPerPage + "&page=" + pageno + "&warehouse=" + $scope.warehousename;
            $http.get(url).success(function (response) {
                debugger;
                $scope.itemMasters = response.ordermaster;  //ajax request to fetch data into vm.data
                console.log("get current Page items:");
                console.log($scope.itemMasters);
                $scope.total_count = response.total_count;
                $scope.currentPageStores = $scope.itemMasters;
                //$scope.tempuser = response.ordermaster;
            });
        }
    };

    $scope.getData1($scope.pageno);
    
    $scope.search = function () {
        
        var url = serviceBase + "api/itemMaster/Searchinitemat?key=" + $scope.searchKeywords;
        $http.get(url).success(function (response) {
            $scope.currentPageStores = response;
        });
    };
    
    $scope.refresh = function () {
        $scope.currentPageStores = $scope.itemMasters;
        $scope.pagenoOne = 0;
    };

    ////.................File Uploader method start..................
    $scope.uploadshow = true;
    $scope.toggle = function () {

        debugger;

        $scope.uploadshow = !$scope.uploadshow;
    }
    function sendFileToServer(formData, status) {
        var uploadURL = "/api/ItemMasterUpload/post"; //Upload URL
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
                $("#status1").append("Data from Server: " + data + "<br>");
                //alert("Succesfully Submitted...........");               
            },
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

    //............................Exel export Method.....................//
    
    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData1 = function () {

        debugger;

        $scope.stores = [];
        var warehouseid = 1;
        $http.get(serviceBase + "api/itemMaster/export?warehouseid=" + warehouseid)
           .success(function (data) {               
               $scope.stores = data;
              // alasql('SELECT CityName,Cityid,CategoryName, CategoryCode,SubcategoryName,SubsubcategoryName,BrandCode,itemname,itemcode,Number,SellingSku,price,PurchasePrice,UnitPrice,MinOrderQty,SellingUnitName,PurchaseMinOrderQty,StoringItemName,PurchaseSku,PurchaseUnitName,SupplierName,SUPPLIERCODES,BaseCategoryName,TGrpName,TotalTaxPercentage,WarehouseName,HindiName,SizePerUnit,Barcode, Active,Deleted INTO XLSX("Items.xlsx",{headers:true}) FROM ?', [$scope.stores]);
               alasql('SELECT CityName,Cityid,CategoryName, CategoryCode,SubcategoryName,SubsubcategoryName,BrandCode,itemname,itemcode,Number,SellingSku,price,PurchasePrice,UnitPrice,MinOrderQty,SellingUnitName,PurchaseMinOrderQty,StoringItemName,PurchaseSku,PurchaseUnitName,SupplierName,SUPPLIERCODES,BaseCategoryName,TGrpName,TotalTaxPercentage,WarehouseName,HindiName,SizePerUnit,Barcode,[Active],[Deleted],Margin,PromoPoint,HSNCode INTO XLSX("Items.xlsx",{headers:true}) FROM ?', [$scope.stores]);
           })
            .error(function (data) {
            })
        //itemMasterService.GetitemMaster().then(function (results) {
        //    $scope.stores = results.data;
        //    alasql('SELECT CityName,Cityid,CategoryName, CategoryCode,SubcategoryName,SubsubcategoryName,BrandCode,itemname,itemcode,Number,SellingSku,price,PurchasePrice,UnitPrice,MinOrderQty,SellingUnitName,PurchaseMinOrderQty,StoringItemName,PurchaseSku,PurchaseUnitName,SupplierName,SUPPLIERCODES,BaseCategoryName,TGrpName,TotalTaxPercentage,WarehouseName,HindiName,SizePerUnit,Barcode INTO XLSX("Items.xlsx",{headers:true}) FROM ?', [$scope.stores]);
        //}, function (error) {
        //});
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("Items.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };

    $scope.currentPageStores = {};
    $scope.update = function () {
        if ($scope.selectedItem == "TextBox") {
        }
        else if ($scope.selectedItem == "RadioButton") {
        }
        else if ($scope.selectedItem == "MultiSelect") {
        }
    }

    $scope.open = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myitemMasterModal.html",
                controller: "ModalInstanceCtrladditem", resolve: { itemMaster: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {
            })
    };

    $scope.edit = function (item) {
        debugger;
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myitemMasterPut.html",
                controller: "ModalInstanceCtrlitemMaster", resolve: { itemMaster: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.itemMaster.push(selectedItem);
                _.find($scope.itemMaster, function (itemMaster) {
                    if (itemMaster.id == selectedItem.id) {
                        itemMaster = selectedItem;
                    }
                });
                $scope.itemMaster = _.sortBy($scope.itemMaster, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
            })
    };
    
    $scope.SetActive = function (item) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myactivemodal.html",
                controller: "ModalInstanceCtrlitemMaster", resolve: { itemMaster: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.itemMaster.push(selectedItem);
                _.find($scope.itemMaster, function (itemMaster) {
                    if (itemMaster.id == selectedItem.id) {
                        itemMaster = selectedItem;
                    }
                });
                $scope.itemMaster = _.sortBy($scope.itemMaster, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
            })
    };
    $scope.SetFree = function (item) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myfreemodal.html",
                controller: "ModalInstanceCtrlitemMaster", resolve: { itemMaster: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.itemMaster.push(selectedItem);
                _.find($scope.itemMaster, function (itemMaster) {
                    if (itemMaster.id == selectedItem.id) {
                        itemMaster = selectedItem;
                    }
                });
                $scope.itemMaster = _.sortBy($scope.itemMaster, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {
            })
    };

    $scope.opendelete = function (data, $index) {        
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteitemMaster.html",
                controller: "ModalInstanceCtrldeleteitemMaster", resolve: { itemMaster: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
            })
    };

    $scope.getWareHouseStores = function () {
        var data = $scope.warehousename;
        $scope.pagenoOne = 0;
        $scope.pageno = 1; 
        $scope.total_count = 0;
        $scope.getData1($scope.pageno);
    };

    $scope.oldprice = function (data) {
        $scope.dataoldprice = [];
        var url = serviceBase + "api/itemMaster/oldprice?ItemId=" + data.ItemId;
        $http.get(url).success(function (response) {
            $scope.dataoldprice = response;
            console.log($scope.dataoldprice);
        })
      .error(function (data) {
      })
    }
}]);

app.controller("ModalInstanceCtrlitemMaster", ["$scope", '$http', 'ngAuthSettings', "itemMasterService", 'SubsubCategoryService', 'SubCategoryService', 'CategoryService', 'unitMasterService', 'WarehouseService', 'supplierService', 'CityService', "$modalInstance", 'FileUploader', "itemMaster", 'TaxGroupService', 'WarehouseCategoryService', function ($scope, $http, ngAuthSettings, itemMasterService, SubsubCategoryService, SubCategoryService, CategoryService, unitMasterService, WarehouseService, supplierService, CityService, $modalInstance, FileUploader, itemMaster, TaxGroupService, WarehouseCategoryService) {
    $scope.xy = true;
    $scope.upld = true;
    $scope.validateUpload = function () {
        $scope.upld = true;
    }
    var input = document.getElementById("file");
    var today = new Date();
    $scope.today = today.toISOString();
    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });

    $scope.uploadedfileName = '';
    $scope.upload = function (files) {

        debugger;

        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var fileuploadurl = '/api/itemimageupload/post', files;
                $upload.upload({
                    url: fileuploadurl,
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: file
                }).progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                }).success(function (data, status, headers, config) {
                });
            }
        }
    };

    $scope.itemMasterData = {};

    $scope.city = [];
    CityService.getcitys().then(function (results) {
        $scope.city = results.data;
    }, function (error) {
    });

    $scope.unitmaster = [];
    unitMasterService.getunitMaster().then(function (results) {
        $scope.unitmaster = results.data;
    }, function (error) {
    });

    $scope.subsubcategory = [];
    SubsubCategoryService.getsubsubcats().then(function (results) {
        $scope.subsubcategory = results.data;
    }, function (error) {
    });

    $scope.subcategory = [];
    SubCategoryService.getsubcategorys().then(function (results) {
        $scope.subcategory = results.data;
    }, function (error) {
    });

    $scope.category = [];
    CategoryService.getcategorys().then(function (results) {
        $scope.category = results.data;
    }, function (error) {
    });

    $scope.taxgroups = [];
    TaxGroupService.getTaxGroup().then(function (results) {
        $scope.taxgroups = results.data;
    }, function (error) {
    });

    $scope.warehouseCategory = [];
    WarehouseCategoryService.getwhcategorys().then(function (results) {
        $scope.warehouseCategory = results.data;
    }, function (error) {
    });

    $scope.supplier = [];
    supplierService.getsuppliers().then(function (results) {
        $scope.supplier = results.data;
    }, function (error) {
    });

    if (itemMaster) {
        $scope.itemMasterData = itemMaster;
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.PutitemMaster = function (data) {

        debugger;

        $scope.loogourl = itemMaster.LogoUrl;
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log($scope.uploadedfileName);

        if ($scope.uploadedfileName == null || $scope.uploadedfileName == '') {
            var url = serviceBase + "api/itemMaster";
            var dataToPost = {
                ItemId: $scope.itemMasterData.ItemId,
                itemname: $scope.itemMasterData.itemname,
                itemcode: $scope.itemMasterData.itemcode,
                Cityid: $scope.itemMasterData.Cityid,
                Categoryid: $scope.itemMasterData.Categoryid,
                SubCategoryId: $scope.itemMasterData.SubCategoryId,
                SubsubCategoryid: $scope.itemMasterData.SubsubCategoryid,
                SupplierId: $scope.itemMasterData.SupplierId,
                MinOrderQty: $scope.itemMasterData.MinOrderQty,
                PurchaseMinOrderQty: $scope.itemMasterData.PurchaseMinOrderQty,
                UnitId: $scope.itemMasterData.UnitId,
                Id: $scope.itemMasterData.Id,
                UnitPrice: $scope.itemMasterData.UnitPrice,
                Discount: $scope.itemMasterData.Discount,
                GruopID: $scope.itemMasterData.GruopID,
                Placeorder: $scope.itemMasterData.Placeorder,
                GeneralPrice: $scope.itemMasterData.GeneralPrice,
                Number: $scope.itemMasterData.Number,
                Barcode: $scope.itemMasterData.Barcode,
                PurchaseUnitName: $scope.itemMasterData.PurchaseUnitName,
                SellingUnitName: $scope.itemMasterData.SellingUnitName,
                price: $scope.itemMasterData.price,
                PurchaseSku: $scope.itemMasterData.PurchaseSku,
                PurchasePrice: $scope.itemMasterData.PurchasePrice,
                SellingSku: $scope.itemMasterData.SellingSku,
                SellingPrice: $scope.itemMasterData.UnitPrice,
                VATTax: $scope.itemMasterData.VATTax,
                UpdatedDate: $scope.itemMasterData.UpdatedDate,
                LogoUrl: $scope.loogourl,
                active: $scope.itemMasterData.active,
                IsDailyEssential: $scope.itemMasterData.IsDailyEssential,
                Margin: $scope.itemMasterData.Margin,
                promoPoint: $scope.itemMasterData.promoPoint,
                HindiName: $scope.itemMasterData.HindiName,
                warehouse_id: $scope.itemMasterData.warehouse_id,
                promoPerItems: $scope.itemMasterData.promoPerItems,
                free: $scope.itemMasterData.free,
                HSNCode: $scope.itemMasterData.HSNCode
            };
            $http.put(url, dataToPost)
            .success(function (data) {
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                    $modalInstance.close(data);
                }
            })
             .error(function (data) {
             })
        }

        else {
            var LogoUrl = $scope.uploadedfileName;
            $scope.itemMasterData.LogoUrl = LogoUrl;
            var url = serviceBase + "api/itemMaster";
            var dataToPost = {
                ItemId: $scope.itemMasterData.ItemId,
                itemname: $scope.itemMasterData.itemname,
                itemcode: $scope.itemMasterData.itemcode,
                Cityid: $scope.itemMasterData.Cityid,
                Categoryid: $scope.itemMasterData.Categoryid,
                SubCategoryId: $scope.itemMasterData.SubCategoryId,
                SubsubCategoryid: $scope.itemMasterData.SubsubCategoryid,
                SupplierId: $scope.itemMasterData.SupplierId,
                MinOrderQty: $scope.itemMasterData.MinOrderQty,
                PurchaseMinOrderQty: $scope.itemMasterData.PurchaseMinOrderQty,
                UnitId: $scope.itemMasterData.UnitId,
                Id: $scope.itemMasterData.Id,
                UnitPrice: $scope.itemMasterData.UnitPrice,
                Discount: $scope.itemMasterData.Discount,
                GruopID: $scope.itemMasterData.GruopID,
                Placeorder: $scope.itemMasterData.Placeorder,
                GeneralPrice: $scope.itemMasterData.GeneralPrice,
                Number: $scope.itemMasterData.Number,
                Barcode: $scope.itemMasterData.Barcode,
                PurchaseUnitName: $scope.itemMasterData.PurchaseUnitName,
                SellingUnitName: $scope.itemMasterData.SellingUnitName,
                price: $scope.itemMasterData.price,
                PurchaseSku: $scope.itemMasterData.PurchaseSku,
                PurchasePrice: $scope.itemMasterData.PurchasePrice,
                SellingSku: $scope.itemMasterData.SellingSku,
                SellingPrice: $scope.itemMasterData.SellingPrice,
                VATTax: $scope.itemMasterData.VATTax,
                UpdatedDate: $scope.itemMasterData.UpdatedDate,
                LogoUrl: $scope.itemMasterData.LogoUrl,
                active: $scope.itemMasterData.active,
                Margin: $scope.itemMasterData.Margin,
                promoPoint: $scope.itemMasterData.promoPoint,
                HindiName: $scope.itemMasterData.HindiName,
                promoPerItems: $scope.itemMasterData.promoPerItems,
                warehouse_id: $scope.itemMasterData.warehouse_id,
                free: $scope.itemMasterData.free
            };
            $http.put(url, dataToPost)
            .success(function (data) {
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                    $modalInstance.close(data);
                }
            })
             .error(function (data) {
             })
        }
    };

    var uploader = $scope.uploader = new FileUploader({
        url: ""        //url: serviceBase + 'api/itemUpload/?type=' + $scope.itemMasterData.itemcode
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
    };
    uploader.onAfterAddingFile = function (fileItem) { };
    uploader.onAfterAddingAll = function (addedFileItems) {
        if ($scope.itemMasterData.itemcode == null) {
            alert("Enter item code");
            $scope.upld = false;
        } else {
            $scope.upld = true;
        }
    };
    uploader.onBeforeUploadItem = function (item) {
        item.url = serviceBase + 'api/itemimageupload/?type=' + $scope.itemMasterData.itemcode;
    };
    uploader.onProgressItem = function (fileItem, progress) { };
    uploader.onProgressAll = function (progress) { };
    uploader.onSuccessItem = function (fileItem, response, status, headers) { };
    uploader.onErrorItem = function (fileItem, response, status, headers) { };
    uploader.onCancelItem = function (fileItem, response, status, headers) { };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        $scope.uploadedfileName = fileItem._file.name;
    };
    uploader.onCompleteAll = function () { };
}]);

app.controller("ModalInstanceCtrladditem", ["$scope", '$http', 'ngAuthSettings', "itemMasterService", 'SubsubCategoryService', 'SubCategoryService', 'CategoryService', 'unitMasterService', 'WarehouseService', 'supplierService', 'CityService', "$modalInstance", 'FileUploader', "itemMaster", 'TaxGroupService', 'WarehouseCategoryService', function ($scope, $http, ngAuthSettings, itemMasterService, SubsubCategoryService, SubCategoryService, CategoryService, unitMasterService, WarehouseService, supplierService, CityService, $modalInstance, FileUploader, itemMaster, TaxGroupService, WarehouseCategoryService) {

    $scope.datas = [];
    itemMasterService.GetitemMaster().then(function (results) {
        $scope.datas = results.data;
        console.log('test');
        console.log($scope.datas);
    }, function (error) {
    });
    $scope.onSelect = function (selection) {
        
        console.log(selection);
        $scope.selectedData = selection;
    };

    //$scope.clearInput = function () {
    //    $scope.$broadcast('simple-autocomplete:clearInput');
    //};

    $scope.upld = false;
    $scope.validateUpload = function () {
        if ($scope.itemMasterData.itemcode != null) {
            $scope.upld = true;
        } else {
            $scope.upld = false;
        }
    }
    $scope.validateUploadd = function () {
        
        if ($scope.itemMasterData.SellingSku != null) {
            $scope.upld = true;
        } else {
            $scope.upld = false;
        }
    }

    var input = document.getElementById("file");
    var today = new Date();
    $scope.today = today.toISOString();

    $scope.$watch('files', function () {
        debugger;
        $scope.upload($scope.files);
    });

    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        debugger;
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var fileuploadurl = '/api/itemimageupload/post', files;
                $upload.upload({
                    url: fileuploadurl,
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: file
                }).progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                }).success(function (data, status, headers, config) {
                });
            }
        }
    };

    $scope.itemMasterData = { };

    $scope.city = [];
    CityService.getcitys().then(function (results) {

        $scope.city = results.data;

    }, function (error) {
    });

    //$scope.unitmaster = [];
    //unitMasterService.getunitMaster().then(function (results) {
    //    $scope.unitmaster = results.data;
    //}, function (error) {
    //});

    $scope.subsubcategory = [];
    SubsubCategoryService.getsubsubcats().then(function (results) {
        $scope.subsubcategory = results.data;
    }, function (error) {
    });

    $scope.subcategory = [];
    SubCategoryService.getsubcategorys().then(function (results) {
        $scope.subcategory = results.data;
    }, function (error) {
    });

    $scope.category = [];
    CategoryService.getcategorys().then(function (results) {
        $scope.category = results.data;
    }, function (error) {
    });

    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouse = results.data;
    }, function (error) {
    });

    $scope.taxgroups = [];
    TaxGroupService.getTaxGroup().then(function (results) {
        $scope.taxgroups = results.data;

    }, function (error) {

    });

    $scope.warehouseCategory = [];
    WarehouseCategoryService.getwhcategorys().then(function (results) {
        $scope.warehouseCategory = results.data;
    }, function (error) {
    });

    $scope.supplier = [];
    supplierService.getsuppliers().then(function (results) {
        $scope.supplier = results.data;
    }, function (error) {

    });

    var icode = $scope.itemMasterData.itemcode;
    if (itemMaster) {
        $scope.itemMasterData = itemMaster;
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.AdditemMaster = function (data) {
        var LogoUrl = $scope.uploadedfileName;
        //$scope.itemMasterData.LogoUrl = serviceBase + "images\\itemimages\\" + LogoUrl;
        $scope.itemMasterData.LogoUrl =LogoUrl;
        var url = serviceBase + "api/itemMaster";
        if ($scope.itemMasterData.Barcode == null || $scope.itemMasterData.Barcode == "") {
            $scope.itemMasterData.Barcode == $scope.itemMasterData.Number;
        }

        if ($scope.itemMasterData.itemname != null && $scope.itemMasterData.itemname != "") {
            if ($scope.itemMasterData.Number != null && $scope.itemMasterData.Number != "") {
                if ($scope.itemMasterData.SellingSku != null && $scope.itemMasterData.SellingSku != "") {
                    if ($scope.itemMasterData.purchaseSku != null && $scope.itemMasterData.purchaseSku != "") {
                        if ($scope.itemMasterData.Categoryid != null && $scope.itemMasterData.Categoryid != "") {
                            if ($scope.itemMasterData.SubCategoryId != null && $scope.itemMasterData.SubCategoryId != "") {
                                if ($scope.itemMasterData.SubsubCategoryid != null && $scope.itemMasterData.SubsubCategoryid != "") {
                                    if ($scope.itemMasterData.Warehouseid != null && $scope.itemMasterData.Warehouseid != "") {
                                        var dataToPost = {
                                           // ItemId: $scope.itemMasterData.ItemId,
                                            itemname: $scope.itemMasterData.itemname,
                                            Categoryid: $scope.itemMasterData.Categoryid,
                                           // CategoryName: $scope.itemMasterData.CategoryName,
                                            SubCategoryId: $scope.itemMasterData.SubCategoryId,
                                           // SubcategoryName: $scope.itemMasterData.SubcategoryName,
                                            SubsubCategoryid: $scope.itemMasterData.SubsubCategoryid,
                                            //SubsubcategoryName: $scope.itemMasterData.SubsubcategoryName,
                                            Cityid: $scope.itemMasterData.Cityid,
                                            SupplierId: $scope.itemMasterData.SupplierId,
                                            itemcode: $scope.itemMasterData.itemcode,
                                            //UnitId: $scope.itemMasterData.UnitId,
                                            //Id: $scope.itemMasterData.Id,
                                            GeneralPrice: $scope.itemMasterData.GeneralPrice,
                                            Number: $scope.itemMasterData.Number,
                                            Barcode: $scope.itemMasterData.Barcode,
                                            PurchaseUnitName: $scope.itemMasterData.PurchaseUnitName,
                                            SellingUnitName: $scope.itemMasterData.SellingUnitName,
                                            UnitPrice: $scope.itemMasterData.UnitPrice,
                                            Discount: $scope.itemMasterData.Discount,
                                            GruopID: $scope.itemMasterData.GruopID,
                                            MinOrderQty: $scope.itemMasterData.MinOrderQty,
                                            PurchaseMinOrderQty: $scope.itemMasterData.PurchaseMinOrderQty,
                                            price: $scope.itemMasterData.price,
                                            warehouse_id: $scope.itemMasterData.Warehouseid,
                                            PurchaseSku: $scope.itemMasterData.purchaseSku,
                                            PurchasePrice: $scope.itemMasterData.PurchasePrice,
                                            SellingSku: $scope.itemMasterData.SellingSku,
                                            SellingPrice: $scope.itemMasterData.UnitPrice,
                                            //VATTax: $scope.itemMasterData.VATTax,
                                            //CreatedDate: $scope.itemMasterData.CreatedDate,
                                            //UpdatedDate: $scope.itemMasterData.UpdatedDate,
                                            LogoUrl: $scope.itemMasterData.LogoUrl,
                                            active: true,
                                            Margin: $scope.itemMasterData.Margin,
                                            promoPoint: $scope.itemMasterData.promoPoint,
                                            HindiName: $scope.itemMasterData.HindiName,
                                            HSNCode: $scope.itemMasterData.HSNCode,
                                            IsDailyEssential: $scope.itemMasterData.IsDailyEssential
                                        };
                                        $http.post(url, dataToPost)
                                        .success(function (data) {
                                            if (data.ItemId == 0) {
                                                alert("Item Not Saved May be Selling SKU exist already");
                                                $scope.gotErrors = true;
                                                if (data[0].exception == "Already") {
                                                    $scope.AlreadyExist = true;
                                                }
                                            }
                                            else {
                                                $modalInstance.close(data);
                                                alert("Item created Successfully");

                                            }
                                        })
                                         .error(function (data) {
                                             alert("Item Not Saved May be Selling SKU exist already");
                                         })
                                    }
                                    else {
                                        alert("WareHouse must be filled out");
                                    }
                                }
                                else {
                                    alert("SubSubCategory must be filled out");
                                }
                            }
                            else {
                                alert("SubCategory must be filled out");
                            }
                        }
                        else {
                            alert("Category must be filled out");
                        }
                    }
                    else {
                        alert("PurchaseSku must be filled out");
                    }
                }
                else {
                    alert("SellingSku must be filled out");
                }
            }
            else {
                alert("Item Number must be filled out");
            }
        }
        else {
            alert("Item must be filled out");
        }
};
    /////////////////////////////////////////////////////// angular upload code
    //if ($scope.itemMasterData.itemcode!=null)
    var uploader = $scope.uploader = new FileUploader({
        url: ""
        //url: serviceBase + 'api/itemUpload/?type=' + $scope.itemMasterData.itemcode
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
    };
    uploader.onAfterAddingFile = function (fileItem) {
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
        if ($scope.itemMasterData.itemcode == null) {
            alert("Enter item code");
            $scope.upld = false;
        } else {
            $scope.upld = true;

        }
    };
    uploader.onBeforeUploadItem = function (item) {
        debugger;
        item.url = serviceBase + 'api/itemUpload/?type=' + $scope.itemMasterData.itemcode;

    };
    uploader.onProgressItem = function (fileItem, progress) {
    };
    uploader.onProgressAll = function (progress) {
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {

        $scope.uploadedfileName = fileItem._file.name;
    };
    uploader.onCompleteAll = function () {

    };

}])

app.controller("ModalInstanceCtrldeleteitemMaster", ["$scope", '$http', "$modalInstance", "itemMasterService", 'ngAuthSettings', "itemMaster", function ($scope, $http, $modalInstance, itemMasterService, ngAuthSettings, itemMaster) {

    $scope.warehouse = [];
    function ReloadPage() {
        location.reload();
    };
    if (itemMaster) {
        $scope.itemMasterData = itemMaster;


    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteitemMaster = function (dataToPost, $index) {

        itemMasterService.deleteitemMaster(dataToPost).then(function (results) {

            $modalInstance.close(dataToPost);
            //ReloadPage();

        }, function (error) {
            alert(error.data.message);
        });
    }

}])