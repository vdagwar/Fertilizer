'use strict';
app.controller('GoodsRecivedController', ['$scope', 'SearchPOService', 'supplierService', 'PurchaseODetailsService', "$filter", '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams", "$modal",
    function ($scope, SearchPOService, supplierService, PurchaseODetailsService, $filter, $http, $window, $timeout, ngAuthSettings, ngTableParams, $modal) {
        console.log("PODetailsController start loading PODetailsService");
        
        $scope.saved = false;
        $scope.nosaved = true;
        $scope.frShow = false;
        $scope.irShow = false;
        $scope.totalIRAmount = 0;
        $scope.currentPageStores = {};
        $scope.PurchaseorderDetails = {};
        $scope.PurchaseOrderData = [];
        var d = SearchPOService.getDeatil();
        console.log(d);
        
        $scope.PurchaseOrderData = d;
        console.log("PurchaseOrderData");
        console.log($scope.PurchaseOrderData);

        $scope.AddFreeItem = function () {
            var modalInstance;
            var data = {}
            data = $scope.PurchaseOrderData;
            modalInstance = $modal.open(
                {
                    templateUrl: "addfreeItem.html",
                    controller: "FreeItemAddController", resolve: { object: function () { return data } }
                }), modalInstance.result.then(function (selectedItem) {
                },
                function () {
                })
        };
                 
        $scope.IRrecived = function (PurchaseorderDetails) {
            
            SearchPOService.IRrecived($scope.PurchaseOrderData).then(function (results) {
                console.log("master invoice fn");
                console.log(results);
            }, function (error) {
            });
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
        PurchaseODetailsService.getPODetalis($scope.PurchaseOrderData.PurchaseOrderId).then(function (results) {
            $scope.myid = $scope.PurchaseOrderData.PurchaseOrderId;
            $scope.searchReceived(results);
        }, function (error) {
        });
        // api/PurchaseOrderDetail/?id=
        $scope.searchReceived = function (results) {
            
            var url = serviceBase + 'api/PurchaseOrderDetailRecived?id=' + $scope.myid + '&a=abc';
            $http.get(url)
            .success(function (data) {
                $http.get(serviceBase + "api/IR?PurchaseOrderId=" + $scope.PurchaseOrderData.PurchaseOrderId).success(function (data) {
                    if (data.length != 0) {
                        $scope.irShow = true;
                        $scope.purchaseIR = data;
                        angular.forEach($scope.purchaseIR, function (value, key) {
                            $scope.totalIRAmount += value.IRAmount;
                        });
                    }
                });
                if (data.purDetails.length > 0) {
                    if ($scope.PurchaseOrderData.Status == "Received") {
                        for (var i = 0; i < data.length; i++) {
                            data.purDetails[i].QtyRecived = data.purDetails[i].QtyRecived1 + data.purDetails[i].QtyRecived2 + data.purDetails[i].QtyRecived3 + data.purDetails[i].QtyRecived4 + data.purDetails[i].QtyRecived5;
                        }
                        var a = "Recept Generated!!";
                        $scope.recept = a;
                        $scope.xdata = data.purDetails;
                        document.getElementById("btnSave").hidden = true;
                        document.getElementById("btnSave1").hidden = true;
                        $scope.saved = true;
                        $scope.nosaved = false;                        
                        $http.get(serviceBase + "api/freeitem?PurchaseOrderId=" + $scope.PurchaseOrderData.PurchaseOrderId).success(function (data) {
                            if (data.length != 0) {
                                $scope.frShow = true;
                                $scope.FreeItems = data;
                            }
                        });
                    }
                    else {
                        data.purDetails.recCount = 1;
                        for (var i = 0; i < data.purDetails.length; i++) {
                            data.purDetails[i].QtyRecived = data.purDetails[i].QtyRecived1 + data.purDetails[i].QtyRecived2 + data.purDetails[i].QtyRecived3 + data.purDetails[i].QtyRecived4 + data.purDetails[i].QtyRecived5;
                            
                            for (var i = 0; i < data.purDetails.length; i++) {
                                if (data.purDetails[i].QtyRecived5 != 0) {
                                    data.purDetails.recCount = 0;
                                }
                                else if (data.purDetails.recCount == 1) {
                                    if (data.purDetails[i].QtyRecived5 == 0) {
                                        data.purDetails.recCount = 5;
                                        if (data.purDetails[i].QtyRecived4 == 0) {
                                            data.purDetails.recCount = 4;
                                            if (data.purDetails[i].QtyRecived3 == 0) {
                                                data.purDetails.recCount = 3;
                                                if (data.purDetails[i].QtyRecived2 == 0) {
                                                    data.purDetails.recCount = 2;
                                                    if (data.purDetails[i].QtyRecived1 == 0) {
                                                        data.purDetails.recCount = 1;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (data.purDetails.recCount == 2) {
                                    if (data.purDetails[i].QtyRecived5 == 0) {
                                        data.purDetails.recCount = 5;
                                        if (data.purDetails[i].QtyRecived4 == 0) {
                                            data.purDetails.recCount = 4;
                                            if (data.purDetails[i].QtyRecived3 == 0) {
                                                data.purDetails.recCount = 3;
                                                if (data.purDetails[i].QtyRecived2 == 0) {
                                                    data.purDetails.recCount = 2;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (data.purDetails.recCount == 3) {
                                    if (data.purDetails[i].QtyRecived5 == 0) {
                                        data.purDetails.recCount = 5;
                                        if (data.purDetails[i].QtyRecived4 == 0) {
                                            data.purDetails.recCount = 4;
                                            if (data.purDetails[i].QtyRecived3 == 0) {
                                                data.purDetails.recCount = 3;
                                            }
                                        }
                                    }
                                }
                                else if (data.purDetails.recCount == 4) {
                                    if (data.purDetails[i].QtyRecived5 == 0) {
                                        data.purDetails.recCount = 5;
                                    }
                                    else {
                                        data.purDetails.recCount = 4;
                                    }
                                } 
                            }
                            for (var i = 0; i < data.purDetails.length; i++) {
                                if (data.purDetails.recCount == 1) {
                                    data.purDetails[i].Price1 = data.purDetails[i].Price;
                                }
                                else if (data.purDetails.recCount == 2) {
                                    data.purDetails[i].Price2 = data.purDetails[i].Price;
                                }
                                else if (data.purDetails.recCount == 3) {
                                    data.purDetails[i].Price3 = data.purDetails[i].Price;
                                }
                                else if (data.purDetails.recCount == 4) {
                                    data.purDetails[i].Price4 = data.purDetails[i].Price;
                                }
                                else if (data.purDetails.recCount == 5) {
                                    data.purDetails[i].Price5 = data.purDetails[i].Price;
                                }
                            }
                        }
                        
                        $scope.PurchaseorderDetails = data;
                    }
                }
                else if (data.purDetails.length == 0) {
                    results.data.recCount = 1;
                    for (var i = 0; i < results.data.length; i++) {
                        results.data[i].QtyRecived = 0;
                        results.data[i].QtyRecived1 = 0;
                        results.data[i].QtyRecived2 = 0;
                        results.data[i].QtyRecived3 = 0;
                        results.data[i].QtyRecived4 = 0;
                        results.data[i].QtyRecived5 = 0;
                        results.data[i].Price1 = results.data[i].Price;
                    }
                    
                    $scope.PurchaseorderDetails.purDetails = results.data;
                }

                console.log(data.purDetails);
                if (data.purDetails.id == 0) {
                    $scope.gotErrors = true;
                    if (data.purDetails[0].exception == "Already") {
                        console.log("Got This User Already Exist");
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                }
                console.log("orders..........");
                console.log($scope.PurchaseorderDetails);
                $scope.totalfilterprice = 0;
                $scope.AmountCalculation($scope.PurchaseorderDetails);
                //PurchaseorderDetails.TotalAmount = $scope.totalfilterprice;
                //$scope.callmethod();
                $timeout(function () {;
                }, 3000)
            })
             .error(function (data) {
                 console.log("Error Got Heere is ");
                 console.log(data);
                 // return $scope.showInfoOnSubmit = !0, $scope.revert()
             })
        }
        
        $scope.AmountCalculation = function (data) {
            $scope.totalfilterprice = 0.0;
            var PurData = data.purDetails;
            _.map(data.purDetails, function (obj)
            {
                if (PurData.recCount == 1 && obj.QtyRecived1 != 0 && obj.QtyRecived1 != null) {
                    if (obj.dis1 != undefined)
                        $scope.totalfilterprice += (obj.QtyRecived1 * obj.Price1 * 100) / (100 + obj.dis1);
                    else
                        $scope.totalfilterprice += obj.QtyRecived1 * obj.Price1;
                }
                else if (PurData.recCount == 2 && obj.QtyRecived2 != 0 && obj.QtyRecived2 != null) {
                    if (obj.dis2 != undefined)
                        $scope.totalfilterprice += (obj.QtyRecived2 * obj.Price2 * 100) / (100 + obj.dis2);
                    //else if(PurData.recCount == 2 && obj.QtyRecived2 != 0 && obj.QtyRecived2 != nullobj.Price2==null)
                    //    alert('input value');
                        else
                        $scope.totalfilterprice += obj.QtyRecived2 * obj.Price2;
                }
                else if (PurData.recCount == 3 && obj.QtyRecived3 != 0 && obj.QtyRecived3 != null) {
                    if (obj.dis3 != undefined)
                        $scope.totalfilterprice += (obj.QtyRecived3 * obj.Price3 * 100) / (100 + obj.dis3);
                    else
                        $scope.totalfilterprice += obj.QtyRecived3 * obj.Price3;
                }
                else if (PurData.recCount == 4 && obj.QtyRecived4 != 0 && obj.QtyRecived4 != null) {
                    if (obj.dis4 != undefined)
                        $scope.totalfilterprice += (obj.QtyRecived4 * obj.Price4 * 100)/(100 + obj.dis4);
                    else
                        $scope.totalfilterprice += obj.QtyRecived4 * obj.Price4;
                }
                else if (PurData.recCount == 5 && obj.QtyRecived5 != 0 && obj.QtyRecived5 != null) {
                    if (obj.dis5 != undefined)
                        $scope.totalfilterprice += (obj.QtyRecived5 * obj.Price5 * 100)/(100 + obj.dis5);
                   else
                        $scope.totalfilterprice += obj.QtyRecived5 * obj.Price5;
                }
            });

            if (PurData.recCount == 1) {
                if (data.discount1 != undefined && data.discount1 != null )
                    $scope.totalfilterprice = ($scope.totalfilterprice * 100) / (100 + data.discount1);
                
            }
            else if (PurData.recCount == 2) {
                if (data.discount2 != undefined && data.discount2 != null)
                    $scope.totalfilterprice = ($scope.totalfilterprice * 100) / (100 + data.discount2);
            }
            else if (PurData.recCount == 3) {
                if (data.discount3 != undefined && data.discount3 != null)
                    $scope.totalfilterprice = ($scope.totalfilterprice * 100) / (100 + data.discount3);
            }
            else if (PurData.recCount == 4) { 
                if (data.discount4 != undefined && data.discount4 != null)
                    $scope.totalfilterprice = ($scope.totalfilterprice * 100) / (100 + data.discount4);
            }
            else if (PurData.recCount == 5 ) {
                if (data.discount5 != undefined && data.discount5 != null)
                    $scope.totalfilterprice = ($scope.totalfilterprice * 100) / (100 + data.discount5);
            }
        };
        //$scope.callmethod = function () {
        //    var init;
        //    return $scope.stores = $scope.PurchaseorderDetails,
        //        $scope.searchKeywords = "",
        //        $scope.filteredStores = [],
        //        $scope.row = "",
        //        $scope.select = function (page) {
        //            var end, start; console.log("select"); console.log($scope.stores);
        //            return start = (page - 1) * $scope.numPerPage, end = start + $scope.numPerPage, $scope.currentPageStores = $scope.filteredStores.slice(start, end)
        //        },
        //        $scope.onFilterChange = function () {
        //            console.log("onFilterChange"); console.log($scope.stores);
        //            return $scope.select(1), $scope.currentPage = 1, $scope.row = ""
        //        },
        //        $scope.onNumPerPageChange = function () {
        //            console.log("onNumPerPageChange"); console.log($scope.stores);
        //            return $scope.select(1), $scope.currentPage = 1
        //        },
        //        $scope.onOrderChange = function () {
        //            console.log("onOrderChange"); console.log($scope.stores);
        //            return $scope.select(1), $scope.currentPage = 1
        //        },
        //        $scope.search = function () {
        //            console.log("search");
        //            console.log($scope.stores);
        //            console.log($scope.searchKeywords);
        //            return $scope.filteredStores = $filter("filter")($scope.stores, $scope.searchKeywords), $scope.onFilterChange()
        //        },
        //        $scope.order = function (rowName) {
        //            console.log("order"); console.log($scope.stores);
        //            return $scope.row !== rowName ? ($scope.row = rowName, $scope.filteredStores = $filter("orderBy")($scope.stores, rowName), $scope.onOrderChange()) : void 0
        //        },
        //        $scope.numPerPageOpt = [3, 5, 10, 20],
        //        $scope.numPerPage = $scope.numPerPageOpt[2],
        //        $scope.currentPage = 1,
        //        $scope.currentPageStores = [],
        //        (init = function () {
        //            return $scope.search(), $scope.select($scope.currentPage)
        //        })
        //    ()
        //}
        //----------------------------------------------------------------------------------------------------        
        $scope.savegr = function () {            
            var saveItem = 0;
            document.getElementById("btnSave").disabled = true;
            console.log($scope.PurchaseOrderData);
            for (var i = 0; i < $scope.PurchaseorderDetails.purDetails.length; i++) {
                if ($scope.PurchaseorderDetails.purDetails[i].QtyRecived1 == undefined || $scope.PurchaseorderDetails.purDetails[i].QtyRecived2 == undefined || $scope.PurchaseorderDetails.purDetails[i].QtyRecived3 == undefined || $scope.PurchaseorderDetails.purDetails[i].QtyRecived4 == undefined || $scope.PurchaseorderDetails.purDetails[i].QtyRecived5 == undefined) {
                    saveItem = 1;
                }
                else if ($scope.PurchaseorderDetails.purDetails[i].TotalQuantity < ($scope.PurchaseorderDetails.purDetails[i].QtyRecived1 + $scope.PurchaseorderDetails.purDetails[i].QtyRecived2 + $scope.PurchaseorderDetails.purDetails[i].QtyRecived3 + $scope.PurchaseorderDetails.purDetails[i].QtyRecived4 + $scope.PurchaseorderDetails.purDetails[i].QtyRecived5)) {
                    saveItem = 2;
                } 
                $scope.PurchaseorderDetails.purDetails[i].PurchaseOrderMasterRecivedId = $scope.purchaseMasterReceiveID;
            }
            if (saveItem == 0) {
                var retrn = 0;
                angular.forEach($scope.PurchaseorderDetails.purDetails, function (value, key) {
                    if ($scope.PurchaseorderDetails.purDetails.recCount == 1) {
                        if (value.Price1 > 0) { }
                        else {
                            retrn = 1;
                        }
                    } else if ($scope.PurchaseorderDetails.purDetails.recCount == 2) {
                        if (value.Price2 > 0) { }
                        else {
                            retrn = 1;
                        }
                    } else if ($scope.PurchaseorderDetails.purDetails.recCount == 3) {
                        if (value.Price3 > 0) { }
                        else {
                            retrn = 1;
                        }
                    } else if ($scope.PurchaseorderDetails.purDetails.recCount == 4) {
                        if (value.Price4 > 0) { }
                        else {
                            retrn = 1;
                        }
                    } else if ($scope.PurchaseorderDetails.purDetails.recCount == 5) {
                        if (value.Price5 > 0) { }
                        else {
                            retrn = 1;
                        }
                    }
                });
                if (retrn ==0) {
                    //$scope.PurchaseorderDetails.TotalAmount = $scope.totalfilterprice;
                    var url = serviceBase + 'api/PurchaseOrderDetailRecived';
                    $http.post(url, $scope.PurchaseorderDetails)
                    .success(function (data) {
                        if (data != "null") {
                            alert('insert successfully');
                        }
                        else {
                            alert('Got some thing wrong!.....data does not insert! ');
                        }
                        document.getElementById("btnSave").disabled = false;
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
                         document.getElementById("btnSave").disabled = false;
                         console.log("Error Got Heere is ");
                         console.log(data);
                     })
                }
                else {
                    alert("please fill purchase item price");
                    document.getElementById("btnSave").disabled = false;
                }
            }
            else if (saveItem == 2) {
                alert("Recieve Quantity must equal or less then Purchase Quantity");
                document.getElementById("btnSave").disabled = false;
            }
            else {
                alert("please put correct values");
                document.getElementById("btnSave").disabled = false;
            }
        }
        /// close po ----------------------        
        $scope.closePO = function () {
            document.getElementById("btnSave1").disabled = true;
            $http.post(serviceBase + 'api/PurchaseOrderDetailRecived/closePO?id=' + $scope.PurchaseOrderData.PurchaseOrderId, $scope.PurchaseorderDetails)
            .success(function (data) {
                alert('insert successfully');
                document.getElementById("btnSave1").disabled = false;
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
                 document.getElementById("btnSave1").disabled = false;
                 console.log("Error Got Heere is ");
                 console.log(data);
             })
        };

        $scope.open = function (item) {
            console.log("in open");
            console.log(item);
        };

        $scope.invoice = function (invoice) {
            console.log("in invoice Section");
            console.log(invoice);
        };
    }]);

app.controller("FreeItemAddController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", '$modal', 'PurchaseODetailsService', function ($scope, $http, ngAuthSettings, $modalInstance, object, $modal, PurchaseODetailsService) {
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    if (object) { $scope.saveData = object; }
    $scope.frShow = false;
    $scope.FreeItems = [];
    $http.get(serviceBase + "api/freeitem?PurchaseOrderId=" + $scope.saveData.PurchaseOrderId).success(function (data) {
        if (data.length != 0) {
            $scope.frShow = true;
            $scope.FreeItems = data;
        }
    })
    .error(function (data) {
    })
    
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    PurchaseODetailsService.GetItemMaster($scope.saveData).then(function (results) {
        $scope.itemMasterrr = results.data;
    });
        
    $scope.AddItem = function (item, quantity) {
        
        var obj = JSON.parse(item);
        var TotalQuantity = JSON.parse(quantity);        

        var dataToPost = {
            PurchaseOrderId: $scope.saveData.PurchaseOrderId,
            SupplierName: $scope.saveData.SupplierName,
            supplierId: $scope.saveData.SupplierId,
            WarehouseId: obj.warehouse_id,
            TotalQuantity: TotalQuantity,
            Status: "Free Item",
            ItemId: obj.ItemId,
            itemname: obj.PurchaseUnitName,
            PurchaseSku: obj.PurchaseSku,
            itemNumber: obj.Number
        }
        console.log(dataToPost);
        var url = serviceBase + "api/freeitem/add";
        $http.post(url, dataToPost).success(function (data) {
            if (data != null) {
                alert("Free Item Addition, successful.. :-)");
                $modalInstance.close();
            }
            else {
                alert("Error Occured.. :-)");
            }
        })
     .error(function (data) {
     })
    };

}]);
