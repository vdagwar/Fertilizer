'use strict';
app.controller('IRController', ['$scope', 'SearchPOService', 'supplierService', "$filter", '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams", "$modal",
    function ($scope, SearchPOService, supplierService, $filter, $http, $window, $timeout, ngAuthSettings, ngTableParams, $modal) {
        console.log("PODetailsController start loading PODetailsService");
        
        $scope.frShow = false;
        $scope.PurchaseorderDetail = {};
        $scope.PurchaseorderDetail.discountt = 0;
        $scope.PurchaseOrderData = [];
        $scope.PurchaseOrderData = SearchPOService.getDeatil();
        console.log($scope.PurchaseOrderData);

        console.log("PurchaseOrderData");
        console.log($scope.PurchaseOrderData);          
         
        $scope.AddIR = function () {
            
            var modalInstance;
            var data = {};
            data = $scope.PurchaseOrderData;
            modalInstance = $modal.open(
                {
                    templateUrl: "addIR.html",
                    controller: "AddIRController", resolve: { object: function () { return data } }
                }), modalInstance.result.then(function (selectedItem) {
                },
                function () {
                })
        };

        $scope.view = function (irImage) {
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "imageView.html",
                    controller: "IRImageController", resolve: { object: function () { return irImage } }
                }), modalInstance.result.then(function (irImage) {                    
                },
                function () { })
        };
        
        supplierService.getsuppliersbyid($scope.PurchaseOrderData.SupplierId).then(function (results) {
            console.log("ingetfn");
            console.log(results);
            $scope.supaddress = results.data.BillingAddress;
            $scope.SupContactPerson = results.data.ContactPerson;
            $scope.supMobileNo = results.data.MobileNo;
        }, function (error) {
        });
        SearchPOService.getWarehousebyid($scope.PurchaseOrderData.Warehouseid).then(function (results) {
            console.log("get warehouse id");
            console.log(results);
            $scope.WhAddress = results.data.Address;
            $scope.WhCityName = results.data.CityName;
            $scope.WhPhone = results.data.Phone;
        }, function (error) {
        });
        
        $scope.searchReceived = function () {
            $http.get(serviceBase + 'api/IR/getIR?PurchaseOrderId=' + $scope.PurchaseOrderData.PurchaseOrderId)
            .success(function (result) {
                
                if (result == "null" || result == null) {
                var url = serviceBase + 'api/PurchaseOrderDetailRecived?id=' + $scope.PurchaseOrderData.PurchaseOrderId + '&a=abc';
                $http.get(url)
                .success(function (data) {
                    if (data.purDetails.length > 0) {
                        var a = "Recept Generated!!";
                        $scope.recept = a;
                        $scope.PurchaseorderDetail = data;
                        $scope.frShow = false;
                        $http.get(serviceBase + "api/IR?PurchaseOrderId=" + $scope.PurchaseorderDetail.PurchaseOrderId).success(function (data) {
                            if (data.length != 0) {
                                $scope.frShow = true;
                                $scope.IR_Images = data;
                            }
                        });
                    }
                    $scope.totalfilterprice = 0;
                    $scope.AmountCalculation($scope.PurchaseorderDetail);

                    $timeout(function () {;
                    }, 3000)
                })
                 .error(function (data) {
                     console.log("Error Got Heere is ");
                     console.log(data);
                 })
                }
                else {
                    if (result.purDetails.length > 0) {
                            var a = "Recept Generated!!";
                            $scope.recept = a;
                            $scope.PurchaseorderDetail = result;
                            $scope.frShow = false;
                            $http.get(serviceBase + "api/IR?PurchaseOrderId=" + $scope.PurchaseorderDetail.PurchaseOrderId).success(function (data) {
                                if (data.length != 0) {
                                    $scope.frShow = true;
                                    $scope.IR_Images = data;
                                }
                            });
                        }
                        $scope.totalfilterprice = 0;
                        $scope.AmountCalculation($scope.PurchaseorderDetail);

                        $timeout(function () {;
                        }, 3000)
                }
            })
        }

        $scope.searchReceived();

        $scope.AmountCalculation = function (data) {
            $scope.totalfilterprice = 0.0;
            var PurData = data.purDetails;           

            var gr = 0;
            var TAWT =0
            _.map(data.purDetails, function (obj) {                
                if (obj.Qty != 0 && obj.Qty != null) {
                    var pr = 0;
                    pr = obj.Qty * obj.Price;
                    var AWT = (pr * 100) / (100 + obj.TotalTaxPercentage);
                    TAWT += AWT;
                    if (obj.discountItem != undefined && obj.discountItem > 0) {
                        obj.discount = (AWT * obj.discountItem)/100;
                        pr -= obj.discount;
                        TAWT -= obj.discount;
                    }
                    obj.PriceRecived = pr;
                    gr += pr;
                }
            });
            if (data.discountt != undefined && data.discountt > 0) {
                data.discount = (TAWT * data.discountt) / 100;
                gr = gr - data.discount;
            }
            $scope.totalfilterprice = gr;
        };

        $scope.savegr = function () {
            //angular.forEach($scope.PurchaseorderDetail.purDetails, function (value, key) {
            //    var IRQuantity = 0;
            //    try {
            //        IRQuantity = value.IRQuantity;
            //        if (IRQuantity==undefined) {
            //            IRQuantity = 0;
            //        }
            //    } catch (ex) {
            //        IRQuantity = 0
            //    }
            //    if (value.QtyRecived < IRQuantity + value.Qty) {
            //        console.log("quatity mismatch");
            //        alert("Invoice Qty is greter than received Qty..");
            //    }                    
            //});
            var msg = "";
            var qtymsg = "";
            _.map($scope.PurchaseorderDetail.purDetails, function (obj) {
                try {
                    if (obj.Qty == undefined) {
                        obj.Qty = 0;
                    }
                    else if (obj.Qty > 0) {
                        if (obj.Price > 0) {
                        }
                        else
                            msg = "Please Put Correct Price"
                    }
                } catch (ex) {
                    obj.IRQuantity = 0
                }
                if (obj.QtyRecived < obj.IRQuantity + obj.Qty) {
                    console.log("quatity mismatch");
                    qtymsg = "Invoice Qty is greter than received Qty..";
                }
            });
            if (msg == "") {
                if (qtymsg !="")
                    alert(qtymsg);
                var url = serviceBase + 'api/IR';
                $scope.PurchaseorderDetail.TotalAmount = $scope.totalfilterprice;
                $http.post(url, $scope.PurchaseorderDetail)
                .success(function (data) {
                    if (data != "null") {
                        alert('insert successfully');
                    }
                    else {
                        alert('Got some thing wrong!.....data does not insert! ');
                    }
                    window.location = "#/SearchPurchaseOrder";
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
                    }
                })
                 .error(function (data) {
                     console.log("Error Got Heere is ");
                     console.log(data);
                 })
            }
            else
                alert(msg);
        }
        $scope.open = function (item) {
            console.log("in open");
            console.log(item);
        };

        $scope.invoice = function (invoice) {
            console.log("in invoice Section");
            console.log(invoice);
        };
    }]);

app.controller("AddIRController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", '$modal', 'FileUploader', function ($scope, $http, ngAuthSettings, $modalInstance, object, $modal, FileUploader) {
    
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    if (object) { $scope.saveData = object; }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },
    $scope.TotalAmount = Math.round(object.TotalAmount, 2);
      
    /////////////////////////////////////////////////////// angular upload code for images
    $scope.uploadedfileName;
    var uploader = $scope.uploader = new FileUploader({
        url: 'api/IRUpload'
    });
    //FILTERS
    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });
    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
    };
    uploader.onAfterAddingFile = function (fileItem) {
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
    };
    uploader.onBeforeUploadItem = function (item) {
    };
    uploader.onProgressItem = function (fileItem, progress) {
    };
    uploader.onProgressAll = function (progress) {
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        $scope.uploadedfileName = fileItem._file.name;
        alert("Image Uploaded Successfully");
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        alert("Image Upload failed");
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        $scope.uploadedfileName = fileItem._file.name;
    };
    uploader.onCompleteAll = function () {
    };

    $scope.AddIR = function (IRAmount,InvoiceNumber) {
        if ($scope.uploadedfileName!= null) {
            {
                var IRAmount = JSON.parse(IRAmount);
                console.log("Add IR");
                var LogoUrl = serviceBase + "../IRImages/" + $scope.uploadedfileName;
                console.log(LogoUrl);

                var dataToPost = {
                    Id: $scope.saveData.Id,
                    PurchaseOrderId: $scope.saveData.PurchaseOrderId,
                    WarehouseId: $scope.saveData.Warehouseid,
                    InvoiceNumber: InvoiceNumber,
                    IRAmount: IRAmount,
                    IRLogoURL: LogoUrl
                }
                console.log(dataToPost);
                var url = serviceBase + "api/IR/add";
                $http.post(url, dataToPost).success(function (data) {
                    if (data != null) {
                        alert("IR Addition, successful.. :-)");
                        $modalInstance.close();
                    }
                    else {
                        alert("Error Occured.. :-)");
                    }
                })
             .error(function (data) {
             })
            }
        }
        else {
            alert('please select image');
        }
        
    }; 
}]);

app.controller("IRImageController", ["$scope", "$modalInstance", "object", '$modal', function ($scope, $modalInstance, object, $modal) {
    
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    if (object) {
        $scope.irImage = object;
    };

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); };
}]);