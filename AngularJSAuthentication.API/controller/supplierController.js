'use strict';
app.controller('supplierController', ['$scope','FileUploader', 'supplierService', 'supplierCategoryService', "$filter", "$http", "ngTableParams", '$modal', function ($scope,FileUploader, supplierService, supplierCategoryService, $filter, $http, ngTableParams, $modal) {
    console.log(" Supplier Controller reached");
    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }

    function sendFileToServer(formData, status) {
        var uploadURL = "/api/supplierupload/post"; //Upload URL
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

    console.log("Vikash start");
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

    //*************************************************************************************************************//

    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData1 = function () {
        alasql('SELECT SupplierId,SUPPLIERCODES,Name,Brand,MobileNo,OfficePhone,BillingAddress,TINNo,Bank_AC_No,Bank_Name,Bank_Ifsc,ShippingAddress INTO XLSX("Supplier.xlsx",{headers:true}) FROM ?', [$scope.stores]);
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("Supplier.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };

    //***************************************************************************************************************

    $scope.currentPageStores = {};

    $scope.open = function (supp) {
        console.log("Modal opened supplier");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mySupplierModal.html",
                controller: "ModalInstanceCtrlSupplier", resolve: { supplier: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {


                $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };
  
    $scope.edit = function (supplier) {
        console.log("Edit Dialog called supplier");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mySupplierModalPut.html",
                controller: "ModalInstanceCtrlSupplier", resolve: { supplier: function () { return supplier } }
            }), modalInstance.result.then(function (selectedsupplier) {
                $scope.supplier.push(selectedsupplier);
                _.find($scope.supplier, function (supplier) {
                    if (supplier.id == selectedsupplier.id) {
                        supplier = selectedsupplier;
                    }
                });
                $scope.supplier = _.sortBy($scope.supplier, 'Id').reverse();
                $scope.selected = selectedsupplier;            
            },
            function () {
                console.log("Cancel Condintion");              
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log("Delete Dialog called for supplier");

        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteSupplier.html",
                controller: "ModalInstanceCtrldeleteSupplier", resolve: { supplier: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");           
            })
    };

    $scope.supplier = [];
    supplierService.getsuppliers().then(function (results) {
        console.log("ingetfn");
        console.log(results.data);
        $scope.supplier = results.data;
        $scope.callmethod();
    }, function (error) {       
    });

    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.supplier,
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

            $scope.numPerPageOpt = [20, 50, 100, 200],
            $scope.numPerPage = $scope.numPerPageOpt[1],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })

        ()


    }
}]);

app.controller("ModalInstanceCtrlSupplier", ["$scope", '$http', 'ngAuthSettings', "supplierService", 'supplierCategoryService', "$modalInstance", "supplier", function ($scope, $http, ngAuthSettings, supplierService,supplierCategoryService, $modalInstance, supplier) {
    console.log("supplier");

    var today = new Date();
    $scope.today = today.toISOString();

    $scope.SupplierData = {
      
    };
    $scope.supplierCategorys = [];
    supplierCategoryService.getsupplierCategory().then(function (results) {
        $scope.supplierCategorys = results.data;
    }, function (error) {
    });
    if (supplier) {
        console.log("supplier if conditon");

        $scope.SupplierData = supplier;
        console.log($scope.SupplierData.Name);
        console.log($scope.SupplierData.Address);
        console.log($scope.SupplierData.Avaiabletime);
        console.log($scope.SupplierData.PhoneNumber);
        console.log($scope.SupplierData.rating);

        console.log($scope.SupplierData.SupplierCaegoryId);
        console.log($scope.SupplierData.CategoryName);
    }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddSupplier = function (data) {
         console.log("Supplier");
         var url = serviceBase + "api/Suppliers";
         var dataToPost = {
             SupplierId: $scope.SupplierData.SupplierId,
             SupplierCaegoryId: $scope.SupplierData.SupplierCaegoryId,
             CategoryName: $scope.SupplierData.CategoryName,
             Name: $scope.SupplierData.Name,
             Avaiabletime: $scope.SupplierData.Avaiabletime,
             PhoneNumber: $scope.SupplierData.PhoneNumber,
             BillingAddress: $scope.SupplierData.BillingAddress,
             ShippingAddress: $scope.SupplierData.ShippingAddress,
             Comments: $scope.SupplierData.Comments,
             TINNo: $scope.SupplierData.TINNo,
             OfficePhone: $scope.SupplierData.OfficePhone,
             MobileNo: $scope.SupplierData.MobileNo,
             EmailId: $scope.SupplierData.EmailId,
             WebUrl: $scope.SupplierData.WebUrl,
             SUPPLIERCODES: $scope.SupplierData.SUPPLIERCODES,
             ContactPerson: $scope.SupplierData.ContactPerson,
            
             rating: $scope.SupplierData.rating
             //ContactImage: $scope.SupplierData.ContactImage,
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



    $scope.PutSupplier = function (data) {
        $scope.SupplierData = {
            
          
        };
        if (supplier) {
            $scope.SupplierData = supplier;
            console.log("found Put Supplier");
            console.log(supplier);
            console.log($scope.SupplierData);
         
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update Supplier");

        var url = serviceBase + "api/Suppliers";
        var dataToPost = {
            SupplierId: $scope.SupplierData.SupplierId,
            SupplierCaegoryId: $scope.SupplierData.SupplierCaegoryId,
            CategoryName: $scope.SupplierData.CategoryName,
            Name: $scope.SupplierData.Name,
            Avaiabletime: $scope.SupplierData.Avaiabletime,
            PhoneNumber: $scope.SupplierData.PhoneNumber,
            BillingAddress: $scope.SupplierData.BillingAddress,
            ShippingAddress: $scope.SupplierData.ShippingAddress,
            Comments: $scope.SupplierData.Comments,
            TINNo: $scope.SupplierData.TINNo,
            OfficePhone: $scope.SupplierData.OfficePhone,
            MobileNo: $scope.SupplierData.MobileNo,
            EmailId: $scope.SupplierData.EmailId,
            WebUrl: $scope.SupplierData.WebUrl,
            SUPPLIERCODES: $scope.SupplierData.SUPPLIERCODES,
            ContactPerson: $scope.SupplierData.ContactPerson,

            rating: $scope.SupplierData.rating
            //ContactImage: $scope.SupplierData.ContactImage,
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

    };


  


}])

app.controller("ModalInstanceCtrldeleteSupplier", ["$scope", '$http', "$modalInstance", "supplierService", 'ngAuthSettings', "supplier", function ($scope, $http, $modalInstance, supplierService, ngAuthSettings, supplier) {
    console.log("delete modal opened");


    $scope.suppliers = [];

    function ReloadPage() {
        location.reload();
    };



    if (supplier) {
        $scope.SupplierData = supplier;
        console.log("found supplier");
        console.log(supplier);
        console.log($scope.SupplierData);
      
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deletesuppliers = function (dataToPost, $index) {

        console.log("Delete  supplier controller");

        supplierService.deletesuppliers(dataToPost).then(function (results) {
            console.log("Del");
            $modalInstance.close(dataToPost);
            //ReloadPage();

        }, function (error) {
            alert(error.data.message);
        });
    }

}])