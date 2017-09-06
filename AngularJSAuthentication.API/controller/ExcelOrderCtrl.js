//'use strict';
//app.controller('ExcelOrderCtrl', ['$scope', '$http', 'WarehouseService', function ($scope, $http, WarehouseService) {
//    $scope.warehouse = [];
//    WarehouseService.getwarehouse().then(function (results) {
//        console.log(results.data);
//        console.log("data");
//        $scope.warehouse = results.data;
//    }, function (error) {
//    });
//    $(function () {
//        $('input[name="daterange"]').daterangepicker({
//            timePicker: true,
//            timePickerIncrement: 5,
//            timePicker12Hour: true,
//            format: 'MM/DD/YYYY h:mm A'
//        });
//    });    
//    $scope.dataforsearch = { datefrom: "", dateto: "" };
//    //............................Exel export Method...........................// Anil
//    $scope.dataforsearch1 = { Cityid: "", Warehouseid: "", datefrom: "", dateto: "" };
//    $scope.exportData = function (data) {        
//        var f = $('input[name=daterangepicker_start]');
//        var g = $('input[name=daterangepicker_end]');
//        $scope.OrderByDate = [];
//        var start = f.val();
//        var end = g.val();
//        //if (!$('#dat').val() && $scope.srch == "") {
//        //    start = null;
//        //    end = null;
//        //    alert("Please select one parameter");
//        //    return;
//        //}
//        if ($scope.srch == "" && $('#dat').val()) {
//            //start = null;
//            //end = null;
//            $scope.srch = {  Warehouseid:'' }
//        }       
//        else {         
//            if (!$scope.srch.Warehouseid) {
//                $scope.srch.Warehouseid = "";
//            }
//        }       
//        var url = serviceBase + "api/ExcelOrder?type=export&start=" + start + "&end=" + end +  "&Warehouseid=" + $scope.srch.Warehouseid;
//        $http.get(url).success(function (response) {           
//            $scope.OrderByDate = response;
//            console.log("$scope.OrderByDate", $scope.OrderByDate);
//            console.log("export");
//            if ($scope.OrderByDate.length <= 0) {
//                alert("No data available between two date ")
//            }
//            else {
//                $scope.NewExportData = [];
//                for (var i = 0; i < $scope.OrderByDate.length; i++) {
//                    var OrderId = $scope.OrderByDate[i].OrderId;
//                    var Skcode = $scope.OrderByDate[i].Skcode;
//                    var ShopName = $scope.OrderByDate[i].ShopName;
//                    var OrderBy = $scope.OrderByDate[i].OrderBy;
//                    var Mobile = $scope.OrderByDate[i].Customerphonenum;
//                    var CustomerName = $scope.OrderByDate[i].CustomerName;
//                    var CustomerId = $scope.OrderByDate[i].CustomerId;
//                    var WarehouseName = $scope.OrderByDate[i].WarehouseName;
//                    var ClusterName = $scope.OrderByDate[i].ClusterName;
//                    var Deliverydate = $scope.OrderByDate[i].Deliverydate;
//                    var CompanyId = $scope.OrderByDate[i].CompanyId;
//                    var BillingAddress = $scope.OrderByDate[i].BillingAddress;
//                    var Date = $scope.OrderByDate[i].Date;
//                    var SalesPerson = $scope.OrderByDate[i].SalesPerson;
//                    var ShippingAddress = $scope.OrderByDate[i].ShippingAddress;
//                    var delCharge = $scope.OrderByDate[i].deliveryCharge;
//                    var Status = $scope.OrderByDate[i].Status;
//                    var ReasonCancle = $scope.OrderByDate[i].ReasonCancle;
//                    var comments = $scope.OrderByDate[i].comments;
//                    var UnitPricei = $scope.OrderByDate[i].UnitPrice;
//                    var qtyi = $scope.OrderByDate[i].qty;
//                    var TotalAmti = $scope.OrderByDate[i].TotalAmt;
//                    var IFRQty='';
//                    var IFRVlu = '';
//                    var ItemFillRate = '';
//                    var RetailerName = $scope.OrderByDate[i].CustomerName;
//                    var ShopName = $scope.OrderByDate[i].ShopName;
//                    var Skcode = $scope.OrderByDate[i].Skcode;
//                    var Mobile = $scope.OrderByDate[i].Mobile;
//                    var Warehouse = $scope.OrderByDate[i].WarehouseName;
//                    var OrderDispatchedDetailss = $scope.OrderByDate[i].orderDispatchedDetailsExport;
//                    for (var j = 0; j < OrderDispatchedDetailss.length; j++) {
//                        var tts = {
//                            OrderId: '', Skcode: '', ShopName: '', Mobile: '', RetailerName: '', RetailerID: '', Warehouse: '',  BillingAddress: '', Date: '',
//                            ItemID: '', ItemName: '', itemNumber: '', MRP: '', MOQ: '', UnitPrice: '', dUnitPrice: '', Quantity: '', DispatchedQuantity: '', IFRQty: '', IFRVlu: '', MOQPrice: '', Discount: '',
//                            DiscountPercentage: '', TaxPercentage: '', Tax: '', TotalAmt: '', DispatchedTotalAmt: '', CategoryName: '', BrandName: '', Status: '', QtyChangeReason: '',
//                        }
//                        tts.ItemID = OrderDispatchedDetailss[j].ItemId;
//                        tts.ItemName = OrderDispatchedDetailss[j].itemname;
//                        //tts.SKU = OrderDispatchedDetailss[j].sellingSKU;
//                        tts.itemNumber = OrderDispatchedDetailss[j].itemNumber;
//                        tts.MRP = OrderDispatchedDetailss[j].price;
//                        tts.UnitPrice = UnitPricei;
//                        tts.dUnitPrice = OrderDispatchedDetailss[j].dUnitPrice;
//                        tts.Quantity = qtyi;
//                        tts.DispatchedQuantity = OrderDispatchedDetailss[j].dqty;
//                        tts.MOQPrice = OrderDispatchedDetailss[j].MinOrderQtyPrice;
//                        tts.Discount = OrderDispatchedDetailss[j].DiscountAmmount;
//                        tts.DiscountPercentage = OrderDispatchedDetailss[j].DiscountPercentage;
//                        tts.TaxPercentage = OrderDispatchedDetailss[j].TaxPercentage;
//                        tts.Tax = OrderDispatchedDetailss[j].TaxAmmount;
//                        tts.TotalAmt = TotalAmti;
//                        tts.DispatchedTotalAmt = OrderDispatchedDetailss[j].dTotalAmt;
//                        tts.CategoryName = OrderDispatchedDetailss[j].CategoryName;
//                        tts.BrandName = OrderDispatchedDetailss[j].BrandName;

//                        tts.OrderId = OrderId;
//                        tts.RetailerID = CustomerId;
//                        tts.OrderBy = OrderBy;
//                        tts.Skcode = Skcode;
//                        tts.Mobile = Mobile;                    
//                        tts.RetailerName = CustomerName;
//                        tts.Warehouse = Warehouse;                    
//                        tts.ShopName = ShopName;
//                        tts.QtyChangeReason = OrderDispatchedDetailss[j].QtyChangeReason;
//                        //tts.CompanyId = CompanyId;
//                        tts.BillingAddress = BillingAddress;
//                        tts.Date = Date;
//                        //tts.Excecutive = SalesPerson;
//                        tts.ShippingAddress = ShippingAddress;
//                        //tts.deliveryCharge = delCharge;
//                        tts.Status = Status;
//                        //tts.ReasonCancle = ReasonCancle;
//                        //tts.comments = comments;
//                        tts.IFRQty = (OrderDispatchedDetailss[j].dqty / qtyi) * 100;
//                        tts.IFRVlu = (OrderDispatchedDetailss[j].dTotalAmt / TotalAmti) * 100;
//                        tts.ItemFillRate = (OrderDispatchedDetailss[j].dTotalAmt / TotalAmti) * 100;

//                        $scope.NewExportData.push(tts);

//                    }
//                }
//                alasql.fn.myfmt = function (n) {
//                    return Number(n).toFixed(2);
//                }
//                alasql('SELECT OrderId,Skcode,ShopName,RetailerName,Mobile,ItemID,ItemName,Warehouse,Date,MRP,MOQPrice,UnitPrice,Quantity,TotalAmt,DispatchedQuantity,DispatchedTotalAmt,ItemFillRate, Discount,DiscountPercentage,Tax,TaxPercentage,Status, QtyChangeReason INTO XLSX("OrderDetailReport.xlsx",{headers:true}) FROM ?', [$scope.NewExportData]);
//            }
//        });
//    };
//    //-------------------------------------------------------------------------//
//}]);
'use strict';
app.controller('ExcelOrderCtrl', ['$scope', '$http', 'WarehouseService', function ($scope, $http, WarehouseService) {


    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {
        console.log(results.data);
        console.log("data");
        $scope.warehouse = results.data;
    }, function (error) {
    });

    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A'
        });

    });
    var Allbydate = [];
    $scope.dataforsearch = { datefrom: "", dateto: "" };
    //............................Exel export Method...........................// Anil
    $scope.dataforsearch1 = { Cityid: "", Warehouseid: "", datefrom: "", dateto: "" };
    $scope.exportData = function (data) {

        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');
        $scope.OrderByDate = [];
        var start = f.val();
        var end = g.val();
        if ($scope.srch == "" && $('#dat').val()) {
            $scope.srch = { Warehouseid: '' }
        }

        else {

            if (!$scope.srch.Warehouseid) {
                $scope.srch.Warehouseid = "";
            }
        }


        $scope.customerdata = [];
        var url = serviceBase + "api/ExcelOrder?type=export&start=" + start + "&end=" + end + "&Warehouseid=" + $scope.srch.Warehouseid;
        $http.get(url).success(function (response) {
            
            $scope.NewExportData = [];
            $scope.OrderByDate = response;
            console.log("$scope.OrderByDate", $scope.OrderByDate);
            var Allbydate = [];

            var filterBydate = [];
            var finallistoflist = [];
            $scope.filtype = "";
            $scope.type = "";
            
            console.log("export");
            if ($scope.OrderByDate.length <= 0) {
                alert("No data available between two date ")
            }
            else {
                for (var i = 0; i < $scope.OrderByDate.length; i++) {
                    var OrderId = $scope.OrderByDate[i].OrderId;
                    var Skcode = $scope.OrderByDate[i].Skcode;
                    var ShopName = $scope.OrderByDate[i].ShopName;
                    var OrderBy = $scope.OrderByDate[i].OrderBy;
                    var Mobile = $scope.OrderByDate[i].Customerphonenum;
                    var CustomerName = $scope.OrderByDate[i].CustomerName;
                    var CustomerId = $scope.OrderByDate[i].CustomerId;
                    var WarehouseName = $scope.OrderByDate[i].WarehouseName;
                    var ClusterName = $scope.OrderByDate[i].ClusterName;
                    var Deliverydate = $scope.OrderByDate[i].Deliverydate;
                    var CompanyId = $scope.OrderByDate[i].CompanyId;
                    var BillingAddress = $scope.OrderByDate[i].BillingAddress;
                    var Date = $scope.OrderByDate[i].Date;
                    var SalesPerson = $scope.OrderByDate[i].SalesPerson;
                    var ShippingAddress = $scope.OrderByDate[i].ShippingAddress;
                    var delCharge = $scope.OrderByDate[i].deliveryCharge;
                    var Status = $scope.OrderByDate[i].Status;
                    var ReasonCancle = $scope.OrderByDate[i].ReasonCancle;
                    var comments = $scope.OrderByDate[i].comments;
                    var UnitPricei = $scope.OrderByDate[i].UnitPrice;
                    var qtyi = $scope.OrderByDate[i].qty;
                    var TotalAmti = $scope.OrderByDate[i].TotalAmt;
                    var IFRQty = '';
                    var IFRVlu = '';
                    var ItemFillRate = '';
                    var RetailerName = $scope.OrderByDate[i].CustomerName;
                    var ShopName = $scope.OrderByDate[i].ShopName;
                    var Skcode = $scope.OrderByDate[i].Skcode;
                    var Mobile = $scope.OrderByDate[i].Mobile;
                    var Warehouse = $scope.OrderByDate[i].WarehouseName;
                    var OrderDispatchedDetailss = $scope.OrderByDate[i].orderDispatchedDetailsExport;
                    for (var j = 0; j < OrderDispatchedDetailss.length; j++) {
                        var tts = {
                            OrderId: '', Skcode: '', ShopName: '', Mobile: '', RetailerName: '', RetailerID: '', Warehouse: '', BillingAddress: '', Date: '',
                            ItemID: '', ItemName: '', itemNumber: '', MRP: '', MOQ: '', UnitPrice: '', dUnitPrice: '', Quantity: '', DispatchedQuantity: '', IFRQty: '', IFRVlu: '', MOQPrice: '', Discount: '',
                            DiscountPercentage: '', TaxPercentage: '', qtychangeR: '', Tax: '', TotalAmt: '', DispatchedTotalAmt: '', CategoryName: '', BrandName: '', Status: '',
                        }
                        tts.ItemID = OrderDispatchedDetailss[j].ItemId;
                        tts.ItemName = OrderDispatchedDetailss[j].itemname;
                        tts.qtychangeR = OrderDispatchedDetailss[j].QtyChangeReason;
                        tts.itemNumber = OrderDispatchedDetailss[j].itemNumber;
                        tts.MRP = OrderDispatchedDetailss[j].price;
                        tts.UnitPrice = UnitPricei;
                        tts.dUnitPrice = OrderDispatchedDetailss[j].dUnitPrice;
                        tts.Quantity = qtyi;
                        tts.DispatchedQuantity = OrderDispatchedDetailss[j].dqty;
                        tts.MOQPrice = OrderDispatchedDetailss[j].MinOrderQtyPrice;
                        tts.Discount = OrderDispatchedDetailss[j].DiscountAmmount;
                        tts.DiscountPercentage = OrderDispatchedDetailss[j].DiscountPercentage;
                        tts.TaxPercentage = OrderDispatchedDetailss[j].TaxPercentage;
                        tts.Tax = OrderDispatchedDetailss[j].TaxAmmount;
                        tts.TotalAmt = TotalAmti;
                        tts.DispatchedTotalAmt = OrderDispatchedDetailss[j].dTotalAmt;
                        tts.CategoryName = OrderDispatchedDetailss[j].CategoryName;
                        tts.BrandName = OrderDispatchedDetailss[j].BrandName;
                        tts.OrderId = OrderId;
                        tts.RetailerID = CustomerId;
                        tts.OrderBy = OrderBy;
                        tts.Skcode = Skcode;
                        tts.Mobile = Mobile;
                        tts.RetailerName = CustomerName;
                        tts.Warehouse = Warehouse;
                        tts.ShopName = ShopName;
                        //tts.Deliverydate = Deliverydate;
                        //tts.CompanyId = CompanyId;
                        tts.BillingAddress = BillingAddress;
                        tts.Date = Date;
                        //tts.Excecutive = SalesPerson;
                        tts.ShippingAddress = ShippingAddress;
                        //tts.deliveryCharge = delCharge;
                        tts.Status = Status;
                        //tts.ReasonCancle = ReasonCancle;
                        //tts.comments = comments;
                        tts.IFRQty = (OrderDispatchedDetailss[j].dqty / qtyi) * 100;
                        tts.IFRVlu = (OrderDispatchedDetailss[j].dTotalAmt / TotalAmti) * 100;
                        tts.ItemFillRate = (OrderDispatchedDetailss[j].dTotalAmt / TotalAmti) * 100;


                        $scope.NewExportData.push(tts);
                    }
                }
                alasql.fn.myfmt = function (n) {
                    return Number(n).toFixed(2);
                }
                alasql('SELECT OrderId,Skcode,ShopName,RetailerName,Mobile,ItemID,qtychangeR,ItemName,CategoryName,BrandName,Warehouse,Date,MRP,MOQPrice,UnitPrice,Quantity,TotalAmt,DispatchedQuantity,DispatchedTotalAmt,ItemFillRate,Discount,DiscountPercentage,Tax,TaxPercentage,Status INTO XLSX("OrderDetailReport.xlsx",{headers:true}) FROM ?', [$scope.NewExportData]);
            }

            $scope.customerdata = $scope.NewExportData;
            $scope.getChart();
        });
    };
    //-------------------------------------------------------------------------//


    //for date conversion
    function convertDate(inputFormat) {
        var month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        var n = month[d.getMonth()];
        return n;
    }

    $scope.getChart = function () {
      
        Allbydate = [];
        var avgData = [];
        _.map($scope.customerdata, function (obj) {
    
            console.log($scope.customerdata);

            var formatdate = convertDate(obj.Date);

            if (avgData.length == 0) {
                var ob = { y: obj.ItemFillRate, label: formatdate, count: 1 }
                avgData.push(ob);
            }

            else {
                
                var checkexits = false;
                angular.forEach(avgData, function (value, key) {
                    if (value.label.replace(/^\s+|\s+$/g, '').toLowerCase() == formatdate.replace(/^\s+|\s+$/g, '').toLowerCase()) {
                        value.y += obj.ItemFillRate;
                        value.count += 1;
                        checkexits = true;
                    }
                });
                if (checkexits == false) {
                    var ob = { y: obj.ItemFillRate, label: formatdate, count: 1 }
                    avgData.push(ob);
                }
            }
        });
        angular.forEach(avgData, function (value, key) {
    
            if (value.y.toString() != 'NaN') {
                var obj1 = { y: (value.y / value.count), label: value.label }
                Allbydate.push(obj1);
            } else { }
           
        });

        var chart = new CanvasJS.Chart("chartContainer",
           {
               title: {
                   text: "Total Order FillRate"
               },
               animationEnabled: true,
               axisX: {
                   valueFormatString: "MMM",
                   interval: 1,
                   intervalType: "month"
               },
               axisY: {
                   includeZero: false

               },
               theme: "theme2",
               data: [
               {
                   type: "line",
                   dataPoints: Allbydate
               }]
           });
        chart.render();
    }
}]);


