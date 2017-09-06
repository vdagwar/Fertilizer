'use strict';
app.controller('VehicleAssissmentController', ['$scope', "DeliveryService", "localStorageService", "$filter", "$http", "ngTableParams", '$modal', function ($scope, DeliveryService, localStorageService, $filter, $http, ngTableParams, $modal) {


    console.log(" VehicleAssissmentController reached");
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A',
        });
    });
    $scope.totalproducts = false;
    $scope.chkdb = true;
    $scope.oldpords = false;
    $scope.totalpercent = 0;
    $scope.dbysz = {};
    $scope.totalproductspace = 0;
    $scope.totalAmountofallproducts = 0;

    $scope.open = function (items) {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myDboyModal.html",
                controller: "Dboyctrl", resolve: { obj: function () { return items } }
            }), modalInstance.result.then(function (selectedItem) {
            },
            function () {
                console.log("Cancel Condintion");

            })
    };
    $scope.DBoys = [];
    DeliveryService.getdboys().then(function (results) {

        $scope.DBoys = results.data;


    }, function (error) {
    });
    $scope.deliveryBoy = {};
    $scope.getdborders = function (DB) {

        $scope.deliveryBoy = JSON.parse(DB);
        localStorageService.set('DBoysData', $scope.deliveryBoy);

        $scope.chkdb = false;
    }

    $scope.DBoysData = localStorageService.get('DBoysData');
    console.log($scope.DBoysData);

    $scope.oldorders = [];
    $scope.getoldorders = function () {
        var start = "";
        var end = "";
        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');
        if (!$('#dat').val()) {
            end = '';
            start = '';
            alert("Select Start and End Date")
            return;
        }
        else {
            start = f.val();
            end = g.val();
        }
        var url = serviceBase + "api/DeliveryIssuance?id=" + $scope.deliveryBoy.PeopleID + "&start=" + start + "&end=" + end;
        $http.get(url)
        .success(function (data) {
            $scope.oldorders = data;

            console.log($scope.oldorders);
            console.log("$scope.oldorders");
            $scope.oldpords = true;
        })
         .error(function (data) {
             console.log(data);
         })
    }

    $scope.prodetails = function (items) {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myDboyModal1.html",
                controller: "VADboyctrl", resolve: { obj: function () { return items } }
            }), modalInstance.result.then(function (selectedItem) {
            },
            function () {
                console.log("Cancel Condintion");

            })
    }
    $scope.summary = function (items) {
        console.log("Modal opened ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myDboyModal.html",
                controller: "VADboyctrlorderdetails", resolve: { obj: function () { return items } }
            }), modalInstance.result.then(function (selectedItem) {
            },
            function () {
                console.log("Cancel Condintion");

            })
    }

}]);

app.controller("VADboyctrl", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "obj", "localStorageService", function ($scope, $http, ngAuthSettings, $modalInstance, obj, localStorageService) {

    $scope.DBoyData = {};
    $scope.orderdetails = [];
    $scope.Orderids = [];
    if (obj) {
        $scope.DBoyData = obj;
        console.log("kkkkkk");
        console.log($scope.DBoyData);
        $scope.orderdetails = $scope.DBoyData.details;
        var ids = $scope.DBoyData.OrderIds;
        var str_array = ids.split(',');
        $scope.Orderids = str_array;
        console.log($scope.Orderids);
    }

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }



}]);

app.controller("VADboyctrlorderdetails", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "obj", "localStorageService", function ($scope, $http, ngAuthSettings, $modalInstance, obj, localStorageService) {

    $scope.DBoysData = localStorageService.get('DBoysData');

    $scope.DBoyData = {};
    $scope.orderdetails = [];
    $scope.Orderids = [];
    $scope.delivereddata = [];
    $scope.cancelddata = [];
    $scope.redispatcheddata = [];

    if (obj) {
        $scope.DBoyData = obj;
        console.log("kkkkkk");
        console.log($scope.DBoyData);
        $scope.orderdetails = $scope.DBoyData.details;
        var ids = $scope.DBoyData.OrderIds;
        var testt = 'test';
        var url = serviceBase + "api/vehicleassissment?ids=" + ids;
        $http.get(url)
        .success(function (data) {

            $scope.dboyordersdata = data;
            $scope.TotalDeliveredOrder = 0;
            $scope.TotalDeliveredOrderAmount = 0;
            $scope.TotalDeliveredCashAmount = 0;
            $scope.TotalDeliveredChequeAmount = 0;
            //$scope.TotalRedispatchOrder = 0;
            $scope.date = data.OrderedDate;
            $scope.TotalRedispatchOrderAmount = 0;
            $scope.TotalCanceledOrderAmount = 0;
            for (var i = 0; i < data.length; i++) {
                if (data[i].Status == "Delivered" || data[i].Status == "Account settled" || data[i].Status == "sattled") {
                    $scope.delivereddata.push(data[i]);
                }
                if (data[i].Status == "Delivery Redispatch") {
                    $scope.redispatcheddata.push(data[i]);
                }
                if (data[i].Status == "Delivery Canceled" || data[i].Status == "Order Canceled") {
                    $scope.cancelddata.push(data[i]);
                }
            }
            for (var d = 0; d < $scope.delivereddata.length; d++) {
                $scope.TotalDeliveredOrderAmount = $scope.TotalDeliveredOrderAmount + $scope.delivereddata[d].GrossAmount;
                $scope.TotalDeliveredOrder = $scope.TotalDeliveredOrder + 1;
                $scope.TotalDeliveredCashAmount = $scope.TotalDeliveredCashAmount + $scope.delivereddata[d].cashAmount;
                $scope.TotalDeliveredChequeAmount = $scope.TotalDeliveredChequeAmount + $scope.delivereddata[d].chequeAmount;
            }
            for (var e = 0; e < $scope.redispatcheddata.length; e++) {

                //$scope.TotalRedispatchOrder = $scope.TotalRedispatchOrder + 1;
            }
        })
         .error(function (data) {
             console.log(data);
         })
        // for odermastr canceal
        var url = serviceBase + "api/vehicleassissment?ids=" + ids + "&testt=" + testt;
        $http.get(url)
        .success(function (data) {

            $scope.date = data[0].UpdatedDate;
            $scope.dcanceldata = data;
            $scope.itemdetail = [];
            $scope.itemdetails = [];
            $scope.itemdetailsredispatched = [];
            $scope.TotalCancelOrder = 0;
            $scope.TotalCanceledOrderqty = 0;
            $scope.TotalCanceledOrderAmount = 0;
            $scope.TotalRedispatchOrder = 0;
            $scope.TotalRedispatchOrderqty = 0;
            $scope.allproducts = [];
            $scope.allproductredispatched = [];
            for (var i = 0; i < data.length; i++) {
                if (data[i].Status == "Delivery Canceled") {
                    $scope.TotalCanceledOrderAmount = $scope.TotalCanceledOrderAmount + data[i].GrossAmount;

                    for (var o = 0; o < data[i].orderDetails.length; o++) {
                        $scope.itemdetail.push(data[i].orderDetails[o]);
                        $scope.TotalCanceledOrderqty = $scope.TotalCanceledOrderqty + data[i].orderDetails[o].qty;
                    }
                    $scope.TotalCancelOrder = $scope.TotalCancelOrder + 1;
                    $scope.itemdetails.push(data[i]);
                }
            }
            if ($scope.itemdetails.length > 0) {
                $scope.selectedorders = angular.copy($scope.itemdetails);
                console.log($scope.itemdetails);
                var firstreq = true;
                for (var k = 0; k < $scope.selectedorders.length; k++) {
                    for (var j = 0; j < $scope.selectedorders[k].orderDetails.length; j++) {
                        if (firstreq) {
                            var OD = $scope.selectedorders[k].orderDetails[j];
                            OD["OrderQty"] = ($scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty).toString();

                            $scope.allproducts.push(OD);
                            firstreq = false;
                        } else {
                            var checkprod = true;
                            _.map($scope.allproducts, function (prod) {
                                if ($scope.selectedorders[k].orderDetails[j].itemNumber == prod.itemNumber) {
                                    prod.OrderQty += ", " + $scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty;
                                    prod.qty = $scope.selectedorders[k].orderDetails[j].qty + prod.qty;
                                    prod.TotalAmt = $scope.selectedorders[k].orderDetails[j].TotalAmt + prod.TotalAmt;
                                    checkprod = false;
                                }
                            })
                            if (checkprod) {
                                var OD = $scope.selectedorders[k].orderDetails[j];
                                OD["OrderQty"] = ($scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty).toString();
                                $scope.allproducts.push(OD);
                            }
                        }
                    }
                }
                console.log("Assissment total products");
                console.log($scope.allproducts);
            }
            //else {
            //    alert("Assissnment Data");
            //}
            $scope.TotalRedispatchedOrderAmount = 0;
            for (var i = 0; i < data.length; i++) {
                if (data[i].Status == "Delivery Redispatch") {

                    $scope.TotalRedispatchedOrderAmount = $scope.TotalRedispatchedOrderAmount + data[i].GrossAmount;

                    for (var o = 0; o < data[i].orderDetails.length; o++) {
                        $scope.itemdetail.push(data[i].orderDetails[o]);
                        $scope.TotalRedispatchOrderqty = $scope.TotalRedispatchOrderqty + data[i].orderDetails[o].qty;

                    }


                    $scope.TotalRedispatchOrder = $scope.TotalRedispatchOrder + 1;
                    $scope.itemdetailsredispatched.push(data[i]);
                }
            }
            if ($scope.itemdetailsredispatched.length > 0) {
                $scope.selectedorders = angular.copy($scope.itemdetailsredispatched);
                console.log($scope.itemdetailsredispatched);
                var firstreq = true;
                for (var k = 0; k < $scope.selectedorders.length; k++) {
                    for (var j = 0; j < $scope.selectedorders[k].orderDetails.length; j++) {
                        if (firstreq) {
                            var OD = $scope.selectedorders[k].orderDetails[j];
                            OD["OrderQty"] = ($scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty).toString();

                            $scope.allproductredispatched.push(OD);
                            firstreq = false;
                        } else {
                            var checkprod = true;
                            _.map($scope.allproductredispatched, function (prod) {
                                if ($scope.selectedorders[k].orderDetails[j].itemNumber == prod.itemNumber) {
                                    prod.OrderQty += ", " + $scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty;
                                    prod.qty = $scope.selectedorders[k].orderDetails[j].qty + prod.qty;
                                    prod.TotalAmt = $scope.selectedorders[k].orderDetails[j].TotalAmt + prod.TotalAmt;
                                    checkprod = false;
                                }
                            })
                            if (checkprod) {
                                var OD = $scope.selectedorders[k].orderDetails[j];
                                OD["OrderQty"] = ($scope.selectedorders[k].orderDetails[j].OrderId + " - " + $scope.selectedorders[k].orderDetails[j].qty).toString();
                                $scope.allproductredispatched.push(OD);
                            }
                        }
                    }
                }
                console.log("Assissment redispatched total products");
                console.log($scope.allproductredispatched);
            }



        })
         .error(function (data) {
             console.log(data);
         })
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }
    $scope.printToCart = function (printSectionId) {
        var printContents = document.getElementById(printSectionId).innerHTML;
        var originalContents = document.body.innerHTML;
        if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
            var popupWin = window.open('', '_blank', 'width=800,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
            popupWin.window.focus();
            popupWin.document.write('<!DOCTYPE html><html><head>' +
                '<link rel="stylesheet" type="text/css" href="style.css" />' +
                '</head><body onload="window.print()"><div class="reward-body">' + printContents + '</div></html>');
            popupWin.onbeforeunload = function (event) {
                popupWin.close();
                return '.\n';
            };
            popupWin.onabort = function (event) {
                popupWin.document.close();
                popupWin.close();
            }
        } else {
            var popupWin = window.open('', '_blank', 'width=800,height=600');
            popupWin.document.open();
            popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</html>');
            popupWin.document.close();
        }
        popupWin.document.close();
        return true;
    }
}])