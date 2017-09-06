'use strict';
app.controller('CRMCtrl', ['$scope', "WarehouseService", "$filter", "$http", "ngTableParams", '$modal', 'CityService', 'supplierService', 'ClusterService','Service',
function ($scope, WarehouseService, $filter, $http, ngTableParams, $modal, CityService, supplierService, ClusterService, Service) {

    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A',
        });
    });
    $scope.CRMData = {};
    $scope.CRMData.id = 0;
   
    $scope.selectType = [
      //{ value: 1, text: "Retailer" },
      { value: 2, text: "Hub" },
      { value: 3, text: "City" },
      { value: 4, text: "Executive" },
      { value: 5, text: "Cluster" }
    ];
   
    $scope.selType = [
        { value: 1, text: "Total Order" },
        { value: 2, text: "Total sale" }
    ];
    var Allbydate = [];
    var finallistoflist = [];
    $scope.filtype = "month";
    $scope.type = "";
    var titleText = "";
    var legendText = "";

    ///////////////////
    $scope.selectedtypeChanged = function (data) {
        $scope.dataselect = [];
        var url = serviceBase + "api/Report/select?value=" + data.type;
        $http.get(url)
        .success(function (data) {
            if (data.length == 0) {
                alert("Not Found");
            }
            $scope.dataselect = data;
            console.log(data);
        });
    }     

    $scope.getdata = function (data) {
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
        if ($scope.CRMData.id != "" && $scope.CRMData.id != 0) {
        var url = "CRM/getcust?id=" + $scope.CRMData.id + "&idtype="+data.type+"&start=" + start + "&end=" + end + "&subsubcatname=tt";
        Service.get(url).then(function (results) {
            
                console.log(results);
                if (data.length == 0) {
                    alert("Not Found");
                    $scope.getChartOrder(null);
                }
                else {
                    Allbydate= results.data;
                    $scope.generatechart();
                    // Allbydate = results.data;
                    alasql.fn.myfmt = function (n) {
                        return Number(n).toFixed(2);
                    }
                    angular.forEach(results.data, function (value, key) {
                        alasql('SELECT ReportDate,CustomerId,Skcode,ShopName,Mobile,WarehouseName,thisordercount,thisordervalue,thisordercountpending,thisordercountdelivered,thisordercountRedispatch,thisordercountCancelled,thisRAppordercount,thisSAppordercount INTO XLSX("CustomerReport.xlsx",{headers:true}) FROM ?', [value.customer]);
                    });
                }
                console.log(data);
             
            }, function (error) {
            })
        } else { alert("select type")}
    }

    //$scope.saampl = [
    //    {
    //        "L0": { "level": 0, "customercount": 225, "skbrandordercount": 0, "volume": 464897, "brandsordered": 310, "RetailerAppordercount": 3, "SalesmanAppordercount": 153 },
    //        "L1": { "level": 1, "customercount": 56, "skbrandordercount": 26, "volume": 322072, "brandsordered": 208, "RetailerAppordercount": 4, "SalesmanAppordercount": 71 },
    //        "L2": { "level": 2, "customercount": 70, "skbrandordercount": 14, "volume": 50877, "brandsordered": 48, "RetailerAppordercount": 1, "SalesmanAppordercount": 20 },
    //        "L3": { "level": 3, "customercount": 35, "skbrandordercount": 11, "volume": 49681, "brandsordered": 36, "RetailerAppordercount": 3, "SalesmanAppordercount": 18 },
    //        "L4": { "level": 4, "customercount": 20, "skbrandordercount": 0, "volume": 0, "brandsordered": 0, "RetailerAppordercount": 0, "SalesmanAppordercount": 0 },
    //        "L5": { "level": 5, "customercount": 40, "skbrandordercount": 0, "volume": 0, "brandsordered": 0, "RetailerAppordercount": 0, "SalesmanAppordercount": 0 },
    //        "dt": "2017-03-01T00:00:00+05:30"
    //    }, {
    //        "L0": { "level": 0, "customercount": 220, "skbrandordercount": 0, "volume": 464897, "brandsordered": 310, "RetailerAppordercount": 3, "SalesmanAppordercount": 153 },
    //        "L1": { "level": 1, "customercount": 26, "skbrandordercount": 26, "volume": 322072, "brandsordered": 208, "RetailerAppordercount": 4, "SalesmanAppordercount": 71 },
    //        "L2": { "level": 2, "customercount": 70, "skbrandordercount": 14, "volume": 50877, "brandsordered": 48, "RetailerAppordercount": 1, "SalesmanAppordercount": 20 },
    //        "L3": { "level": 3, "customercount": 83, "skbrandordercount": 11, "volume": 49681, "brandsordered": 36, "RetailerAppordercount": 3, "SalesmanAppordercount": 18 },
    //        "L4": { "level": 4, "customercount": 70, "skbrandordercount": 0, "volume": 0, "brandsordered": 0, "RetailerAppordercount": 0, "SalesmanAppordercount": 0 },
    //        "L5": { "level": 5, "customercount": 20, "skbrandordercount": 0, "volume": 0, "brandsordered": 0, "RetailerAppordercount": 0, "SalesmanAppordercount": 0 },
    //        "dt": "2017-04-01T00:00:00+05:30"
    //    }]
    $scope.type = "1";
    $scope.filtertype = function (filterData) {
        var OrderSummary = [];
        for (var i = 0; i < filterData.length; i++) {
            if ($scope.type === "1") {
                var totalOrder = { x: new Date(filterData[i].dt), y: filterData[i].customercount, name: filterData[i].level }
                OrderSummary.push(totalOrder);
                titleText = "Customer count";
                legendText = "Total customer"
            }
            else if ($scope.type === "2") {
                var totalSale = { x: new Date(filterData[i].dt), y: filterData[i].volume / 10000, name: filterData[i].level }
                OrderSummary.push(totalSale);
                titleText = "Sale's(in 10000)";
                legendText = "Total Sale"
            }
        }
        //$scope.getChartOrder(OrderSummary, legendText);
        return OrderSummary;
    }

    $scope.chartdata = function (gd1, rang) {
        if (gd1.length > 0) {
            var orsum = {
                type: "column",
                showInLegend: true,
                lineThickness: 2,
                name: gd1[0].name,
                indexLabel: "{y}",
                markerType: "circle",
                color: rang,
                dataPoints: gd1
            }
            return orsum;
        } else {
            return null;
        }
    }

    $scope.getChartOrder = function (graphdata) {
        var chart = new CanvasJS.Chart("chartContainer",
		{
		    title: {
		        text: titleText,
		        fontSize: 30
		    },
		    animationEnabled: true,
		    axisX: {
		        intervalType: "month",
		        interval: 1,
		        gridColor: "Silver",
		        tickColor: "silver",
		        valueFormatString: "MMM YY",
		        title: "Dates",
		        labelAngle: 60,
		        titleFontSize: 25,
		        labelFontSize: 20,
		        abelAutoFit: true
		    },
		    toolTip: {
		        shared: true,
		        contentFormatter: function (e) {
		            var content = " ";
		            for (var i = 0; i < e.entries.length; i++) {
		                var dt = e.entries[i].dataPoint.label;
		                
		                if (Allbydate.length > 0) {
		                    for (var l = 0; l < Allbydate.length; l++) {
		                        if (Allbydate[l] != null && Allbydate[l].x == dt) {
		                            content += "level:-" + Allbydate[l].L0.level + ",";
		                            content += "Customers" + Allbydate[l].L0.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L0.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L0.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L0.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L0.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L0.SalesmanAppordercount + "<br/>";
		                            
		                            content += "level:-" + Allbydate[l].L1.level + ",";
		                            content += "Customers" + Allbydate[l].L1.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L1.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L1.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L1.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L1.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L1.SalesmanAppordercount + "<br/>";

		                            content += "level:-" + Allbydate[l].L2.level + ",";
		                            content += "Customers" + Allbydate[l].L2.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L2.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L2.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L2.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L2.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L2.SalesmanAppordercount + "<br/>";

		                            content += "level:-" + Allbydate[l].L3.level + ",";
		                            content += "Customers" + Allbydate[l].L3.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L3.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L3.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L3.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L3.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L3.SalesmanAppordercount + "<br/>";

		                            content += "level:-" + Allbydate[l].L4.level + ",";
		                            content += "Customers" + Allbydate[l].L4.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L4.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L4.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L4.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L4.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L4.SalesmanAppordercount + "<br/>";

		                            content += "level:-" + Allbydate[l].L5.level + ",";
		                            content += "Customers" + Allbydate[l].L5.customercount + ",";
		                            content += "volume:-" + Allbydate[l].L5.volume + ",";
		                            content += "Skbrandorders:-" + Allbydate[l].L5.skbrandordercount + ",";
		                            content += "Otherbrandsorders:-" + Allbydate[l].L5.brandsordered + ",";
		                            content += "App:-" + Allbydate[l].L5.RetailerAppordercount + ",";
		                            content += "SalesmanApp:-" + Allbydate[l].L5.SalesmanAppordercount + ",";
		                            return content;
		                        }
		                    }
		                }
		            }

		        }
		    },
		    theme: "theme1",
		    axisY: {
		        gridColor: "Silver",
		        tickColor: "silver",
		        title: legendText,
		        titleFontSize: 25,
		        labelFontSize: 20
		    },
		    legend: {
		        verticalAlign: "center",
		        horizontalAlign: "right",
		        fontSize: 25
		    },
		    data: graphdata,
		    legend: {
		        cursor: "pointer",
		        itemclick: function (e) {
		            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		                e.dataSeries.visible = false;
		            }
		            else {
		                e.dataSeries.visible = true;
		            }
		            chart.render();
		        }
		    }
		});
        chart.render();
    }

    $scope.generatechart = function () {
        //Allbydate = $scope.saampl;
        finallistoflist = [];
        var mylist = [];
        var filterData0 = [];
        var filterData1 = [];
        var filterData2 = [];
        var filterData3 = [];
        var filterData4 = [];
        var filterData5 = [];
        for (var i = 0; i < Allbydate.length; i++) {
            angular.forEach(Allbydate[i], function (value, key) {
                var obj = {};
                obj.level = "Level :" + value.level;
                obj.customercount = value.customercount;
                obj.volume = value.volume;
                obj.dt = Allbydate[i].dt;
                obj.skbrandordercount = value.skbrandordercount;
                obj.brandsordered = value.brandsordered;
                obj.RetailerAppordercount = value.RetailerAppordercount;
                obj.SalesmanAppordercount = value.SalesmanAppordercount;
                if (value.level == 0) filterData0.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
                if (value.level == 1) filterData1.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
                if (value.level == 2) filterData2.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
                if (value.level == 3) filterData3.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
                if (value.level == 4) filterData4.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
                if (value.level == 5) filterData5.push({ x: new Date(obj.dt), y: obj.customercount, name: obj.level });
            });
        }
        var gd = $scope.chartdata(filterData0, "#7f6084");
        finallistoflist.push(gd);
        var gd = $scope.chartdata(filterData1, "#ffff99");
        finallistoflist.push(gd);
        var gd = $scope.chartdata(filterData2, "#86b402");
        finallistoflist.push(gd);
        var gd = $scope.chartdata(filterData3, "#c24642");
        finallistoflist.push(gd);
        var gd = $scope.chartdata(filterData4, "#369ead");
        finallistoflist.push(gd);
        var gd = $scope.chartdata(filterData5, "#347ead");
        finallistoflist.push(gd);
        $scope.getChartOrder(finallistoflist);
    }
}]);

//$scope.city = [];
//    CityService.getcitys().then(function (results) {
//    $scope.city = results.data;
//}, function (error) {
//});
//$scope.Clusters = [];
//ClusterService.getcluster().then(function (results) {
//    $scope.Clusters = results.data;
//}, function (error) {
//});

//$scope.warehouse = [];
//WarehouseService.getwarehouse().then(function (results) {
//    $scope.warehouse = results.data;
//}, function (error) {
//})
