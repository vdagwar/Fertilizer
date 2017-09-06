'use strict';
app.controller('CurrencyStockController', ['$scope', 'CurrencyStockService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, CurrencyStockService, $filter, $http, ngTableParams, $modal) {
    console.log(" Currency Stock Controller reached");
    $scope.currentPageStores = {};
    $scope.StockCurrencys = {};
    $scope.TotalAmount = {};
    $scope.StockCurrencyshistory = {};
    $scope.uploadshow = true;
   
    
    $scope.open = function (data) {
       
        $scope.uploadshow = false;
        console.log("data", data);

        $http.get(serviceBase + 'api/CurrencyStock?id=' + data.CurrencyHistoryid).then(function (results) {
         
            $scope.myData = results.data;
            console.log(" $scope.myData", $scope.myData);
            for (var i = 0; i < $scope.myData.length; i++) {
                $scope.TotalAmount = $scope.myData[i].TotalAmount;
                $scope.CurrencyHistoryid = $scope.myData[i].CurrencyHistoryid;
                console.log(" $scope.CurrencyHistoryid", $scope.CurrencyHistoryid);
            }

        });
    };
    //$scope.open = function (data) {
    //    
    //    $scope.uploadshow = false;
    //    console.log("data", data);
    //    var url = serviceBase + "api/CurrencySettle/Stock";

    //    $http.post(url, data).then(function (response) {
    //        


    //        if (response != null) {
    //            $scope.myData = response.data;
    //            console.log(" $scope.myData", $scope.myData);
    //            //for (var i = 0; i < $scope.myData.length; i++) {
    //                $scope.TotalAmount = $scope.myData.TotalAmount;
    //                $scope.CurrencyStockid = $scope.myData.CurrencyStockid;
    //                console.log(" $scope.CurrencyStockid", $scope.CurrencyStockid);
    //            //}
    //            alert('Data update successfully');
    //            //$location.path("/CurrencyStock");
    //        } else {
    //            txt = "You pressed Cancel!";

    //        }
    //    });
    //} 
    $scope.Checkdetails = function () {
        
        $http.get(serviceBase + 'api/CurrencyStock/Checkget?status=1').then(function (results) {
            
            $scope.CheckStock = results.data;


        });
    };
    $scope.history = function () {
        
        $http.get(serviceBase + 'api/CurrencyStock/historyget?Stock_status=1').then(function (results) {
            
       $scope.StockCurrencyshistory = results.data;
         

       });
    };
    $scope.quantity = 1;
    $scope.bruto = 7.5;
    $scope.total = 0;

    $scope.calculate = function () {
      
        $scope.total = $scope.quantity * $scope.bruto;
    }

    $scope.calculate();
    $scope.StockCurrencys = [];
    $scope.getCurrecyStocks = function () {
        
        $http.get(serviceBase + 'api/CurrencyStock?Stock_status=1').then(function (results) {
         
            $scope.StockCurrencys = results.data;
            $scope.compare = angular.copy($scope.StockCurrencys);
         
        });
    }

    $scope.getCurrecyStocks();

    $scope.denominations = {
        2e3: 0,
        1e3: 0,
        500: 0,
        100: 0,
        50: 0,
        20: 0,
        10: 0,
        5: 0,
        2: 0,
        1: 0
    };
  
    $scope.addManualCash = function (a, b) {
       
        var b = parseInt(b);
        $scope.newcash = [];
        _.each($scope._cash, function (obj) {
            if (a != obj) {
                $scope.newcash.push(obj);
            }

        })
        $scope._cash = $scope.newcash;
     if (b > 0) {
         var b = parseInt(b);
            $scope.TotalAmount1 = 0;
            var lp = parseInt(b);
            for (var i = 0; i < lp; i++) {
                $scope._cash.push(a);

            }
            angular.forEach($scope._cash, function (value) {

                console.log("value", value);
                $scope.TotalAmount1 = $scope.TotalAmount1 + value;

                //$scope.order.Tendered = $scope.TotalAmount1;
                console.log(" $scope.TotalAmount1 " + $scope.TotalAmount1);
            });
            for (var i = 0; i < $scope.compare.length; i++) {
               
                if ($scope.denominations[b] <= $scope.compare[0].twoTHrscount) {
                
               //alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].fivehrscount) {

                   // alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].hunrscount) {

                   // alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].fiftyrscount) {

                   // alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].Twentyrscount) {

                    //alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].tenrscount) {

                    //alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].fiverscount) {

                   // alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].tworscount) {

                   // alert("enter value is correct");
                }
                else if ($scope.denominations[b] <= $scope.compare[0].onerscount) {

                   // alert("enter value is correct");
                }
                else {

                    alert("enter value is greater");
                }
            }
        
        }




        else {
         var b = parseInt(b);
            $scope.TotalAmount1 = 0;
            var lp = parseInt(b);
            for (var i = 0; i < lp; i++) {
                $scope._cash.push(a);
                //////////$scope.TotalAmount = 0;
            }
            angular.forEach($scope._cash, function (value) {
                $scope.TotalAmount1 = $scope.TotalAmount1 + value;
                //$scope.order.Tendered = $scope.TotalAmount1;
                console.log(" $scope.TotalAmount1 " + $scope.TotalAmount1);
            });
        }


    }
    $scope.clearCash = function () {
        
        $scope.denominations = {
            2e3: 0,
            1e3: 0,
            500: 0,
            100: 0,
            50: 0,
            20: 0,
            10: 0,
            5: 0,
            2: 0,
            1: 0
        }, $scope._cash = [], $scope.cash = []
        $scope.TotalAmount1 = " ";
    };

    $scope.denominationsrupee = {
        2e3: 0,
        1e3: 0,
        500: 0,
        100: 0,
        50: 0,
        20: 0,
        10: 0,
        5: 0,
        2: 0,
        1: 0
    };
 
    $scope.Settle = function (bnk) {
      
    
        alert("Are you show click Settle");
        console.log("bnk", bnk);
        var url = serviceBase + "api/CurrencyStock/BanksettleCurrency?id=" + $scope.CurrencyHistoryid;
        var datatopost = {
            onerscount: $scope.denominations[1],
            OneRupee: $scope.denominations[1] * 1,
            tworscount: $scope.denominations[2],
            TwoRupee: $scope.denominations[2] * 2,
            fiverscount: $scope.denominations[5],
            FiveRupee: $scope.denominations[5] * 5,
            tenrscount: $scope.denominations[10],
            TenRupee: $scope.denominations[10] * 10,
            Twentyrscount: $scope.denominations[20],
            TwentyRupee: $scope.denominations[20] * 20,
            fiftyrscount: $scope.denominations[50],
            fiftyRupee: $scope.denominations[50] * 50,
            hunrscount: $scope.denominations[100],
            HunRupee: $scope.denominations[100] * 100,
            fivehrscount: $scope.denominations[500],
            fiveHRupee: $scope.denominations[500] * 500,
            twoTHrscount: $scope.denominations[2000],
            twoTHRupee: $scope.denominations[2000] * 2000,
            Name: $scope.bnk.name,
            Withdrawl: $scope.TotalAmount1,
            Mobile: $scope.bnk.mobile
           };
        console.log("datatopost", datatopost);
        $http.post(url,datatopost).success(function (result) {
            if (result != null) {

                window.location.reload(true);
                console.log("successfull", result);
            }
            else {
                console.log("failed");
            }

        })
    }
  
}]);