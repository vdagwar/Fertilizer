'use strict';
app.controller('orderdetailsController', ['$scope', 'OrderMasterService', 'OrderDetailsService', '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams", "peoplesService", '$modal', 'BillPramotionService', "$modalInstance", function ($scope, OrderMasterService, OrderDetailsService, $http, $window, $timeout, ngAuthSettings, ngTableParams, peoplesService, $modal, BillPramotionService, $modalInstance) {
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }
    
    console.log("orderdetailsController start loading OrderDetailsService");
    $scope.Payments = {};
    $scope.currentPageStores = {};
    $scope.isShoW = false;
    $scope.finaltable = false;
    $scope.dispatchtable = false;
    $scope.OrderDetails = {};
    $scope.OrderData = {};
    $scope.OrderData1 = {};
    var d = OrderMasterService.getDeatil();
    console.log(d);
    $scope.signdata = {};
    $scope.viewsign = function () {
        var Signimg = $scope.signdata.Signimg;
        console.log("$scope.signdata");
        if (Signimg != null) {
            window.open(Signimg);
        } else { alert("no sign present") }
    }
    $scope.count = 1;
    $scope.OrderData = d;
    console.log("$scope.OrderDatamk");
    console.log($scope.OrderData);
    if ($scope.OrderData.Status == "Order Canceled") { //$scope.OrderData.Status == "Cancel" ||
        $scope.finaltable = false;
        $scope.dispatchtable = false;
        //document.getElementById("btnSave").hidden = true;
    }
    if ($scope.OrderData.Status == "Ready to Dispatch") {
        //document.getElementById("tbl").hidden = true;
        //need to change
    }
    $scope.orderDetails = d.orderDetails;
    $scope.orderDetailsINIT = d.orderDetails;
    $scope.checkInDispatchedID = $scope.orderDetails[0].OrderId;

    $scope.pramotions = {};
    $scope.selectedpramotion = {};
    BillPramotionService.getbillpramotion().then(function (results) {
        $scope.pramotions = results.data;
    }, function (error) {
    });
    //for display info in print final order
    OrderMasterService.saveinfo($scope.OrderData);
    // end 
    $scope.Finalbutton = false;
    $scope.displayDiscountamount = true;
    if ($scope.OrderData.Status == "Cancel") {
        $scope.cancledispatch = true;
        $scope.Finalbutton = true;
        $scope.ReDispatchButton = true;
    }

    $scope.callForDropdown = function () {
   
        var url = serviceBase + 'api/OrderDispatchedMaster?id=' + $scope.checkInDispatchedID;
        $http.get(url)
        .success(function (data) {
            if (data == "null") {
                $scope.dispatchtable = false;
            } else {
                $scope.signdata = data;
                OrderMasterService.saveDispatch(data);
                $scope.dispatchtable = true;
                $scope.DBname = {};
                $scope.DBname = data.DboyName;
                $scope.OrderData1 = data;
                if ($scope.OrderData1.ReDispatchCount > 1) {
                    $scope.ReDispatchButton = true;
                }
            }
        });
    };
    $scope.callForDropdown();
    // checking order is dispatched or not here
    var url = serviceBase + 'api/OrderDispatchedDetails?id=' + $scope.checkInDispatchedID;
    $http.get(url).success(function (data1) {
        if (data1.length > 0) {
            $scope.count = 0;
            $scope.orderDetails11 = data1;
            $scope.orderDetailsDisp = data1;
            $scope.msg = "Order is Dispatched";
            document.getElementById("btnSave").hidden = true;
        }
        $http.get(serviceBase + "api/freeitem/SkFree?oderid=" + $scope.checkInDispatchedID).then(function (results) {
            
            $scope.freeitem = results.data;
            try {
                if (results.data.length > 0) {
                    $scope.isShoW = true;
                }
            } catch (ex) { }
        }, function (error) {
        });
    });
    //end

    // check Final master Sattled order done
    var url = serviceBase + 'api/OrderDispatchedMasterFinal?id=' + $scope.checkInDispatchedID;
    $http.get(url).success(function (data1) {
        if (data1 == "null") {
            $scope.SHOWPAYMENT = false;
        } else {
            $scope.SHOWPAYMENT = true;
            $scope.SHOWPAYMENTTABLE = data1;
            $scope.Finalbutton = true;
            $scope.myMasterbutton = true;
            $scope.finalLastReturn = true;
            //$scope.cancledispatch = true;
            $scope.FinalbuttonLAST = true;
            $scope.cancledispatch = true;
            $scope.ReDispatchButton = true;
            $scope.FinalinvoiceButtonWithoutMasterLast = true;
        }
    });
    //end

    // update orderdispatch master
    $scope.ReDispatch = function (Dboy) {
        try { var obj = JSON.parse(Dboy); } catch (err) { alert("Select Delivery boy") }
        var dboyMob = obj.Mobile;
        console.log(dboyMob);
        $scope.Did = $scope.orderDetailsDisp[0].OrderDispatchedMasterId;
        var url = serviceBase + 'api/OrderDispatchedMaster?id=' + $scope.Did + '&DboyNo=' + obj.Mobile;
        $http.put(url)
        .success(function (data) {

            if (data.length > 0) {

            }
            alert("Delivey Boy update successfully");
            window.location = "#/orderMaster";
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);

         });

    }
    // end

    $scope.Itemcount = 0;
    ///check for return

    var url = serviceBase + 'api/OrderDispatchedDetailsReturn?id=' + $scope.checkInDispatchedID;
    $http.get(url).success(function (data) {

        if (data.length > 0) {
            $scope.count = 0;
            $scope.Finalbutton = true;  // disable chckbox
            //$scope.cancledispatch = true; // disable cancle button
            // $scope.dboyname = data.DboyName;
            $scope.orderDetailsR = data;
            $scope.myVar = true;
            $scope.myVar1 = true;
            $scope.finalobj = [];
            document.getElementById("FinalInvoice").hidden = false;
            for (var i = 0; i < $scope.orderDetailsINIT.length; i++) {
                for (var j = 0; j < $scope.orderDetailsR.length; j++) {
                    if ($scope.orderDetailsINIT[i].ItemId == $scope.orderDetailsR[j].ItemId) {
                        //if ($scope.orderDetailsINIT[i].qty == $scope.orderDetails1[j].qty) {
                        //    $scope.orderDetailsINIT.splice(i, 1);
                        //} else {
                        $scope.orderDetailsINIT[i].qty = $scope.orderDetailsINIT[i].qty - $scope.orderDetailsR[j].qty;

                        //}
                        $scope.finalobj.push($scope.orderDetailsINIT[i]);
                    }
                }
            }
        }
        else {
            $scope.myVar = false;
            //document.getElementById("btnFinalInvoice").hidden = true;
        }
    });
    //end check return
    $scope.settleAmountLast = function () {

        return $scope.PaymentsLAST.CheckAmount + $scope.PaymentsLAST.ElectronicAmount + $scope.PaymentsLAST.CashAmount;
    }

    $scope.duensettleequalLAST = function () {
        var payamount = Math.round($scope.myMaster.GrossAmount);
        if ($scope.settleAmountLast() == payamount) {
            return true;
        } else {

            return false;
        }
    }

    $scope.settleAmount = function () {

        return $scope.Payments.CheckAmount + $scope.Payments.ElectronicAmount + $scope.Payments.CashAmount;
    }

    $scope.duensettleequal = function () {

        if ($scope.settleAmount() == $scope.OrderData1.GrossAmount) {
            return true;
        } else {

            return false;
        }
    }

    //check for final detail Sattled
    var url = serviceBase + 'api/OrderDispatchedDetailsFinal?id=' + $scope.checkInDispatchedID;
    $http.get(url).success(function (data) {
        if (data.length > 0) {

            $scope.count = 0;
            $scope.orderDetailsFinal = data;
            $scope.finaltable = true;
            $scope.dispatchtable = false;
            $scope.finalAmountLAST = 0;
            $scope.finalTaxAmountLAST = 0;
            $scope.finalGrossAmountLAST = 0;
            $scope.finalTotalTaxAmountLAST = 0;
            $scope.finalLast = true;

            if ($scope.orderDetailsFinal[0].FinalOrderDispatchedMasterId == 0) {
                for (var i = 0; i < $scope.orderDetailsFinal.length; i++) {
                    $scope.finalAmountLAST = $scope.finalAmountLAST + $scope.orderDetailsFinal[i].TotalAmt;
                    $scope.finalTaxAmountLAST = $scope.finalTaxAmountLAST + $scope.orderDetailsFinal[i].TaxAmmount;
                    $scope.finalGrossAmountLAST = $scope.finalGrossAmountLAST + $scope.orderDetailsFinal[i].TotalAmountAfterTaxDisc;
                    $scope.finalTotalTaxAmountLAST = $scope.finalTotalTaxAmountLAST + $scope.orderDetailsFinal[i].TotalAmountAfterTaxDisc;
                }


                $scope.TotalAmount = $scope.finalAmountLAST;
                $scope.TaxAmount = $scope.finalTaxAmountLAST;
                $scope.GrossAmount = $scope.finalGrossAmountLAST;
                $scope.DiscountAmount = $scope.finalTotalTaxAmountLAST - $scope.finalAmountLAST;


                $scope.myDetail = {};
                $scope.myMaster = {};
                $scope.myDetail = $scope.orderDetailsFinal;
                $scope.myMaster = [];
                var newdata = angular.copy($scope.OrderData1);
                $scope.myMaster = newdata;

                $scope.myMaster.TotalAmount = $scope.TotalAmount;
                $scope.myMaster.TaxAmount = $scope.TaxAmount;
                $scope.myMaster.GrossAmount = $scope.GrossAmount;
                $scope.myMaster.DiscountAmount = $scope.DiscountAmount;
                $scope.FinalinvoiceButtonLast = true;
                $scope.showpaybleButton = true;
                $scope.finalLastReturn = true;

            }

        }
        else {
            $scope.finalLastReturn = true;
            $scope.FinalinvoiceButtonWithoutMasterLast = true;
        }
    });

    $scope.showInvoiceWithoutMasterFinal = function () {
        OrderMasterService.saveDispatch($scope.myDetail);
        OrderMasterService.save1($scope.myMaster);
        console.log("Order Invoice  called ...");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteOrderInvoice1.html",
                controller: "ModalInstanceCtrlOrderInvoiceDispatch",
                resolve:
                    {
                        order: function () {
                            return $scope.myDetail
                        }
                    }
            }), modalInstance.result.then(function () {
            },
            function () {
                console.log("Cancel Condintion");

            })
    };

    $scope.showInvoice = function (data) {
        OrderMasterService.save1(data);
        console.log("Order Invoice  called ...");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteOrderInvoice1.html",
                controller: "ModalInstanceCtrlOrderInvoice1",
                resolve:
                    {
                        order: function () {
                            return data
                        }
                    }
            }), modalInstance.result.then(function () {
            },
            function () {
                console.log("Cancel Condintion");
            })
    };
    $scope.showInvoiceDispatch = function (Detail, master) {
        OrderMasterService.saveDispatch(Detail);
        OrderMasterService.save1(master);
        console.log("Order Invoice  called ...");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteOrderInvoice1.html",
                controller: "ModalInstanceCtrlOrderInvoiceDispatch",
                resolve:
                    {
                        order: function () {
                            return Detail
                        }
                    }
            }), modalInstance.result.then(function () {
            },
            function () {
                console.log("Cancel Condintion");
            })
    };
    //get peoples for delivery boy
    peoplesService.getpeoples().then(function (results) {
        $scope.User = results.data;
        console.log("Got people collection");
        console.log($scope.User);
    }, function (error) {

    });
    // end
    for (var i = 0 ; i < $scope.orderDetails.length; i++) {
        $scope.Itemcount = $scope.Itemcount + $scope.orderDetails[i].qty;
    }
    _.map($scope.OrderData.orderDetails, function (obj) {
        $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
        console.log("$scope.OrderData");
        console.log($scope.totalfilterprice);

    })
    var s_col = false;
    var del = '';
    $scope.set_color = function (orderDetail) {
        if (orderDetail.qty > orderDetail.CurrentStock) {
            s_col = true;
            return { background: "#ff9999" }
        }
        else { s_col = false; }
    }

    $scope.giveDiscount = function (DISCOUNT) {
        //$scope.discountAmount = (DISCOUNT * $scope.OrderData.TotalAmount) / 100;
        //$scope.OrderData.DiscountAmount = $scope.discountAmount;
        //$scope.FinalTotalAmount = $scope.OrderData.TotalAmount - $scope.discountAmount;
        //$scope.displayDiscountamount = false;       
    }

    $scope.selcteddboy = function (db) {
        $scope.Dboy = JSON.parse(db);
    }
    $scope.selcteddQTR = function (data) {
        $scope.QtyChangeReason = data;
    }

    $scope.selectedItemChanged = function (id) {
        $('#' + id).removeClass('hd');
    }
    $scope.save = function (orderDetail) {

   
        console.log($scope.Dboy);
        var data = $scope.orderDetails;
        var flag = true;
        for (var i = 0; i < $scope.orderDetails.length; i++) {
            data = $scope.orderDetails[i];
            if ((data.qty || data.qty == null || data.qty == undefined) > data.CurrentStock && data.Deleted == false) {
                alert("your stock not sufficient please purchase or remove item then dispatched");
                flag = false;
                break;
            }
        }

        if ($scope.Dboy == undefined) {
            alert("Please Select Delivery Boy");
            flag = false;
        }
        if (flag == true) {
            try {
                var obj = ($scope.Dboy);
            } catch (err) {
                alert("Please Select Delivery Boy");
                console.log(err);
            }
            $scope.OrderData["DboyName"] = obj.DisplayName;
            $scope.OrderData["DboyMobileNo"] = obj.Mobile;
            $scope.OrderData;
            console.log($scope.OrderData);
            console.log("save orderdetailfunction");
            console.log("Selected Pramtion");
            console.log($scope.selectedpramotion);
            for (var i = 0; i < $scope.orderDetails.length; i++) {
                console.log($scope.orderDetails[i]);
                console.log($scope.orderDetails[i].DiscountPercentage);
                $scope.orderDetails[i].DiscountPercentage = $scope.selectedpramotion;
                console.log($scope.orderDetails[i].DiscountPercentage);
            }
            var url = serviceBase + 'api/OrderDispatchedMaster';
            $http.post(url, $scope.OrderData)
            .success(function (data) {
                $scope.dispatchedMasterID = data.OrderDispatchedMasterId;
                $scope.orderDetails = data.orderDetails;
                $scope.dispatchedDetail();
                $modalInstance.close(data);
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
    };

    $scope.dispatchedDetail = function () {
    
        for (var i = 0; i < $scope.orderDetails.length; i++) {
            $scope.orderDetails[i].OrderDispatchedMasterId = $scope.dispatchedMasterID;
            if ($scope.orderDetails[i].qty > $scope.orderDetails[i].CurrentStock) {
                delete $scope.orderDetails[i];
            }
        }
        $scope.orderDetails;

        var url = serviceBase + 'api/OrderDispatchedDetails';
        $http.post(url, $scope.orderDetails)
        .success(function (data) {

            alert('insert successfully');
          
            window.location = "#/orderMaster";
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

    $scope.open = function (item) {
        console.log("in open");
        console.log(item);


    };

    $scope.invoice = function (invoice) {
        console.log("in invoice Section");
        console.log(invoice);

    };
    // cancle dispatch
    $scope.CancleDispatch = function () {
        var status = "cancle";

        var url = serviceBase + 'api/OrderDispatchedDetails?cancle=' + status;
        $http.put(url, $scope.orderDetailsDisp)
        .success(function (data) {

            alert('Cancle successfully');
            // location.reload();
            window.location = "#/orderMaster";
            console.log("Error Gor Here");
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);

         })
    }
    //end

    // add payment FOR DISPATCH FINAL
    $scope.Payments = {
        PaymentAmount: null,
        CheckNo: null,
        CheckAmount: null,
        ElectronicPaymentNo: null,
        ElectronicAmount: null,
        CashAmount: null
    }
    // final bill sattled
    $scope.SaveFinalSattled = function (Payments) {
        $scope.Payments;
        $scope.Payments.PaymentAmount = $scope.OrderData1.GrossAmount
        $scope.finalDataMaster = $scope.OrderData1;
        $scope.finalDataMaster["PaymentAmount"] = $scope.Payments.PaymentAmount;
        $scope.finalDataMaster["CheckNo"] = $scope.Payments.CheckNo;
        $scope.finalDataMaster["CheckAmount"] = $scope.Payments.CheckAmount;
        $scope.finalDataMaster["ElectronicPaymentNo"] = $scope.Payments.ElectronicPaymentNo;
        $scope.finalDataMaster["ElectronicAmount"] = $scope.Payments.ElectronicAmount;
        $scope.finalDataMaster["CashAmount"] = $scope.Payments.CashAmount;


        var url = serviceBase + 'api/OrderDispatchedMasterFinal';
        $http.post(url, $scope.finalDataMaster)
        .success(function (data) {

            $scope.FdispatchedMasterID = data.FinalOrderDispatchedMasterId;
            alert('payment insert successfully');
            // location.reload();
            window.location = "#/orderMaster";
            $scope.dispatchedDetailFinal();
            console.log("Error Gor Here");
            console.log(data);
            if (data.FinalOrderDispatchedMasterId == 0) {
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
    // dispatch detail final add
    $scope.dispatchedDetailFinal = function () {
        for (var i = 0; i < $scope.orderDetailsDisp.length; i++) {
            $scope.orderDetailsDisp[i].FinalOrderDispatchedMasterId = $scope.FdispatchedMasterID;
            //if ($scope.orderDetailsDisp[i].qty > $scope.orderDetailsDisp[i].CurrentStock) {
            //    delete $scope.orderDetailsDisp[i];
            //}
        }

        $scope.orderDetailsDisp;

        var url = serviceBase + 'api/OrderDispatchedDetailsFinal';
        $http.post(url, $scope.orderDetailsDisp)
        .success(function (data) {

            alert('insert successfully');
            // location.reload();
            window.location = "#/orderMaster";
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
    // FOR AFTER RETURN LAST PAYMENT

    $scope.PaymentsLAST = {
        PaymentAmount: null,
        CheckNo: null,
        CheckAmount: null,
        ElectronicPaymentNo: null,
        ElectronicAmount: null,  
        CashAmount: null
    }
    $scope.SaveFinalSattledLAST = function (PaymentsLAST) {
        $scope.PaymentsLAST;


        var payamount = parseInt($scope.myMaster.GrossAmount);
        $scope.PaymentsLAST.PaymentAmount = payamount;



        $scope.finalDataMasterLAST = $scope.OrderData1;

        if ($scope.finalAmountLAST > 0) {
            $scope.finalDataMasterLAST.TotalAmount = $scope.finalAmountLAST;
        }


        $scope.finalDataMasterLAST["PaymentAmount"] = $scope.PaymentsLAST.PaymentAmount;
        $scope.finalDataMasterLAST["CheckNo"] = $scope.PaymentsLAST.CheckNo;
        $scope.finalDataMasterLAST["CheckAmount"] = $scope.PaymentsLAST.CheckAmount;
        $scope.finalDataMasterLAST["ElectronicPaymentNo"] = $scope.PaymentsLAST.ElectronicPaymentNo;
        $scope.finalDataMasterLAST["ElectronicAmount"] = $scope.PaymentsLAST.ElectronicAmount;
        $scope.finalDataMasterLAST["CashAmount"] = $scope.PaymentsLAST.CashAmount;
        $scope.finalDataMasterLAST.TotalAmount = $scope.TotalAmount;
        $scope.finalDataMasterLAST.TaxAmount = $scope.TaxAmount;
        $scope.finalDataMasterLAST.GrossAmount = $scope.GrossAmount;
        $scope.finalDataMasterLAST.DiscountAmount = $scope.DiscountAmount;

        var url = serviceBase + 'api/OrderDispatchedMasterFinal';
        $http.post(url, $scope.finalDataMasterLAST)
        .success(function (data) {

            $scope.FdispatchedMasterIDLAST = data.FinalOrderDispatchedMasterId;
            $scope.FdispatchedMasterORDERIDLAST = data.OrderId;
            alert('payment insert successfully');
            // location.reload();
            //   window.location = "#/orderMaster";
            $scope.dispatchedDetailFinalLAST();
            console.log("Error Gor Here");
            console.log(data);
            if (data.FinalOrderDispatchedMasterId == 0) {
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

    $scope.dispatchedDetailFinalLAST = function () {


        var url = serviceBase + 'api/OrderDispatchedDetailsFinal?oID=' + $scope.FdispatchedMasterORDERIDLAST + '&fID=' + $scope.FdispatchedMasterIDLAST;
        $http.put(url)
        .success(function (data) {

            alert('insert successfully');
            // location.reload();
            window.location = "#/orderMaster";
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
    $scope.freeitem1 = function (item) {
        
        var modalInstance;
        modalInstance = $modal.open({
            templateUrl: "addFreeItem.html",
            controller: "ModalInstanceCtrlFreeItems", resolve: { order: function () { return item } }
        }), modalInstance.result.then(function () {
        },
            function () {
                console.log("Cancel Condintion");
            })
    };
}]);

app.controller("ModalInstanceCtrlOrderInvoice1", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order invoice modal opened");
        $scope.OrderDetails = {};
        $scope.OrderData = {};
        var d = OrderMasterService.getDeatil();
        $scope.OrderData = d;
        var info = OrderMasterService.getDeatilinfo();
        $scope.OrderData1 = info;
        
        if (info.Status == 'Pending')
            $scope.OrderData1.OrderedDate = info.CreatedDate;
        $scope.Itemcount = 0;

        for (var i = 0 ; i < $scope.OrderData.length; i++) {
            $scope.Itemcount = $scope.Itemcount + $scope.OrderData[i].qty;
        }

        $scope.totalfilterprice = 0;
        _.map($scope.OrderData, function (obj) {
            console.log("count total");
            $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
            console.log(obj.TotalAmt);
            console.log($scope.totalfilterprice);
        })

        if ($scope.OrderData1.Status == 'Pending' || $scope.OrderData1.Status == 'Process' || $scope.OrderData1.Status == 'Cancel') {
            setTimeout(function () {
                $(".taxtable").remove();
            }, 500)

        }
    }]);

app.controller("ModalInstanceCtrlOrderInvoiceDispatch", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order invoice modal opened");
        $scope.OrderData = {};
        $scope.isShoW = false;
        var d = OrderMasterService.getDeatil();

        $scope.OrderData1 = d;
        var info = OrderMasterService.getDispatchMaster();
        $scope.OrderData = info;
        $scope.Itemcount = 0;

        var data = _.map(_.groupBy($scope.OrderData, function (cat) {
            return cat.TaxPercentage;
        }), function (grouped) {
            return grouped;
        });

        $scope.data1 = [];
        for (var j = 0; j < data.length; j++) {
       
            var tl = data[j];
            $scope.tts = { TaxPercentage: '', itmQty1: '', TaxAmt1: '', HSNCode: '' }
            var TaxPercentage = tl[0].TaxPercentage;
            //var SGSTTaxPercentage = tl[0].SGSTTaxPercentage;
            //var IGSTTaxPercentage = tl[0].IGSTTaxPercentage;
            //var HSNCode = tl[0].HSNCode;
            
            var ttlTax = 0;
            var tx = 0;
            var tx1 = 0;
            var tx2 = 0;
            var qt = 0;

            for (var i = 0; i < tl.length; i++) {
                tx += tl[i].TaxAmmount;
                //tx1 += tl[i].SGSTTaxAmmount;
                //tx2 += tl[i].IGSTTaxAmmount;
                qt += tl[i].qty;
            }
            var itmQty1 = qt;
            var TaxAmt1 = tx;
            //var SGSTTaxAmt = tx1;
            //var IGSTTaxAmt = tx2;

            $scope.tts.TaxPercentage = TaxPercentage;

            //$scope.tts.SGSTTaxPercentage = SGSTTaxPercentage;
            //$scope.tts.IGSTTaxPercentage = IGSTTaxPercentage;
            //$scope.tts.HSNCode = HSNCode;
            $scope.tts.itmQty1 = itmQty1;
            $scope.tts.TaxAmt1 = TaxAmt1;
            //$scope.tts.SGSTTaxAmt = SGSTTaxAmt;
            //$scope.tts.IGSTTaxAmt = IGSTTaxAmt;
            

            $scope.data1.push($scope.tts);
            console.log($scope.data1);
        }

        for (var i = 0 ; i < $scope.OrderData.length; i++) {
            $scope.Itemcount = $scope.Itemcount + $scope.OrderData[i].qty;
        }

        $scope.totalfilterprice = 0;
        _.map($scope.OrderData, function (obj) {
            console.log("count total");
            $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
            console.log(obj.TotalAmt);
            console.log($scope.totalfilterprice);
        });

        $http.get(serviceBase + "api/freeitem/SkFree?oderid=" + $scope.OrderData1.OrderId).then(function (results) {
            
            $scope.freeitem = results.data;
            try {
                if (results.data.length > 0) {
                    $scope.isShoW = true;
                }
            } catch (ex) { }
        }, function (error) {
        });
    }]);

app.controller("ModalInstanceCtrlFreeItems", ["$scope", '$http', "$modalInstance", 'ngAuthSettings', 'order', 'itemMasterService', function ($scope, $http, $modalInstance, ngAuthSettings, order, itemMasterService) {
    $scope.OrderData = {};
    $scope.itemMaster = [];
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }

    if (order) {
        $scope.Customer = order;
    }

    var url = serviceBase + "api/itemMaster/freeItem";
    $http.get(url).then(function (results) {
        
        $scope.itemMaster = results.data;
    }, function (error) {
    });

    $scope.add = function (data) {
        var price = 0;
        angular.forEach($scope.itemMaster, function (value, key) {
            if (value.ItemId == data.ItemId) {
                price = value.price;
            }
        });
        var url = serviceBase + "api/freeitem/addSkFree";
        var dataToPost = {
            OrderId: $scope.Customer.OrderId,
            CustomerId: $scope.Customer.CustomerId,
            SkCode: $scope.Customer.Skcode,
            ItemId: data.ItemId,
            WarehouseId: $scope.Customer.Warehouseid,
            Status: $scope.Customer.Status,
            TotalQuantity: data.Quantity,
            Amount : data.Quantity*price
        };
        $http.post(url, dataToPost)
        .success(function (data) {
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    $scope.AlreadyExist = true;
                    $modalInstance.close(data);
                }
                $modalInstance.close(data);
            }
            else {
                $modalInstance.close(data);
            }
        })
         .error(function (data) {
             $modalInstance.close(data);
         })
    };
}]);