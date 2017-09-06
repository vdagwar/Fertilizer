﻿app.controller('CurrencySettleController', ['$scope', '$http', '$timeout', "$location", "$modal", "DeliveryService", "localStorageService",
function ($scope, $http, $timeout, $location, $modal, DeliveryService, localStorageService) {
    console.log(" CurrencyController reached");
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A',
        });
    });
    $scope.selcteddQTR = function (data) {
       
        $scope.DueAmountstatus = data;
    }

    $scope.selectedItemChanged = function (id) {
        
        $('#' + id).removeClass('hd');
    }

    $scope.DBoys = [];
    DeliveryService.getdboys().then(function (results) {
        
        $scope.DBoys = results.data;
    }, function (error) {
    });
    $scope.oldpords = false;
    $scope.deliveryBoy = {};
    $scope.value1 = 0;
    $scope.getdborders = function (DB) {

        $scope.deliveryBoy = JSON.parse(DB);
        if (DB != "") {
            
            DeliveryService.getDBoyCurrencyID($scope.deliveryBoy.PeopleID).then(function (results) {
               
                $scope.DBoyCurrency = results.data;
               
                $scope.oldpords = true;
            }, function (error) {
            });
        }
    }


    $scope.checkAll = function () {
        
        if ($scope.selectedAll) {
            $scope.selectedAll = false;
        } else {
            $scope.selectedAll = true;
        }
        angular.forEach($scope.DBoyCurrency, function (trade) {
            trade.check = $scope.selectedAll;
        });
    };

    //*****updateCode****// 
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
    $scope._cash = [];

    $scope.CheckChanged = function (pod) {
        
        if (pod.check == true) {
            $scope.denominations[1] = parseInt($scope.denominations[1]) + parseInt(pod.onerscount);
            $scope.denominationsrupee[1] = parseInt($scope.denominationsrupee[1]) + parseInt(pod.OneRupee);
            $scope.denominations[2] = parseInt($scope.denominations[2]) + parseInt(pod.tworscount);
            $scope.denominationsrupee[2] = parseInt($scope.denominationsrupee[2]) + parseInt(pod.TwoRupee);
            $scope.denominations[5] = parseInt($scope.denominations[5]) + parseInt(pod.fiverscount);
            $scope.denominationsrupee[5] = parseInt($scope.denominationsrupee[5]) + parseInt(pod.FiveRupee);
            $scope.denominations[10] = parseInt($scope.denominations[10]) + parseInt(pod.tenrscount);
            $scope.denominationsrupee[10] = parseInt($scope.denominationsrupee[10]) + parseInt(pod.TenRupee);
            $scope.denominations[20] = parseInt($scope.denominations[20]) + parseInt(pod.Twentyrscount);
            $scope.denominationsrupee[20] = parseInt($scope.denominationsrupee[20]) + parseInt(pod.TwentyRupee);
            $scope.denominations[50] = parseInt($scope.denominations[50]) + parseInt(pod.fiftyrscount);
            $scope.denominationsrupee[50] = parseInt($scope.denominationsrupee[50]) + parseInt(pod.fiftyRupee);
            $scope.denominations[100] = parseInt($scope.denominations[100]) + parseInt(pod.hunrscount);
            $scope.denominationsrupee[100] = parseInt($scope.denominationsrupee[100]) + parseInt(pod.HunRupee);
            $scope.denominations[500] = parseInt($scope.denominations[500]) + parseInt(pod.fivehrscount);
            $scope.denominationsrupee[500] = parseInt($scope.denominationsrupee[500]) + parseInt(pod.fiveHRupee);
            $scope.denominations[2000] = parseInt($scope.denominations[2000]) + parseInt(pod.twoTHrscount);
            $scope.denominationsrupee[2000] = parseInt($scope.denominationsrupee[2000]) + parseInt(pod.twoTHRupee);
            $scope.totalbillAmounts = parseInt($scope.totalbillAmounts) + parseInt(pod.TotalAmount);

           // $scope.value1 = parseInt($scope.value1) + parseInt(pod.checkTotalAmount);



        }
        else {
            if ($scope.denominations[1] > 0) {
                $scope.denominations[1] = parseInt($scope.denominations[1]) - parseInt(pod.onerscount);
            }
            else {
                $scope.denominations[1] = 0;
            }
            if ($scope.denominations[2] > 0) {
                $scope.denominations[2] = parseInt($scope.denominations[2]) - parseInt(pod.tworscount);
            }
            else {
                $scope.denominations[2] = 0;
            }

            if ($scope.denominations[5] > 0) {
                $scope.denominations[5] = parseInt($scope.denominations[5]) - parseInt(pod.fiverscount);
            }
            else {
                $scope.denominations[5] = 0;
            }
            if ($scope.denominations[10] > 0) {
                $scope.denominations[10] = parseInt($scope.denominations[10]) - parseInt(pod.tenrscount);
            }
            else {
                $scope.denominations[10] = 0;
            }
            if ($scope.denominations[20] > 0) {
                $scope.denominations[20] = parseInt($scope.denominations[20]) - parseInt(pod.Twentyrscount);
            }
            else {
                $scope.denominations[20] = 0;
            }
            if ($scope.denominations[50] > 0) {
                $scope.denominations[50] = parseInt($scope.denominations[50]) - parseInt(pod.fiftyrscount);
            }
            else {
                $scope.denominations[50] = 0;
            }
            if ($scope.denominations[100] > 0) {
                $scope.denominations[100] = parseInt($scope.denominations[100]) - parseInt(pod.hunrscount);
            }
            else {
                $scope.denominations[100] = 0;
            }
            if ($scope.denominations[500] > 0) {
                $scope.denominations[500] = parseInt($scope.denominations[500]) - parseInt(pod.fivehrscount);
            }
            else {
                $scope.denominations[500] = 0;
            }
            if ($scope.denominations[2000] > 0) {
                $scope.denominations[2000] = parseInt($scope.denominations[2000]) - parseInt(pod.twoTHrscount);
            }
            else {
                $scope.denominations[2000] = 0;
            }
            $scope.totalbillAmounts = parseInt($scope.totalbillAmounts) - parseInt(pod.TotalAmount);
           // $scope.value1 = parseInt($scope.value1) - parseInt(pod.checkTotalAmount);


        }
    }



    $scope.addManualCash = function (a, b) {
       
        b = parseInt(b);
        $scope.newcash = [];
        _.each($scope._cash, function (obj) {
            if (a != obj) {
                $scope.newcash.push(obj);
            }
        })
        $scope._cash = $scope.newcash;
       
        if (b > 0) {
            $scope.TotalAmount = 0;
            var lp = parseInt(b);
            for (var i = 0; i < lp; i++) {
                $scope._cash.push(a);
            }
            angular.forEach($scope._cash, function (value) {
                $scope.TotalAmount = $scope.TotalAmount + value;
                $scope.order.Tendered = $scope.TotalAmount;
                console.log("Total val " + $scope.TotalAmount);
            });
        }
        else {
            
            $scope.TotalAmount = 0;
            var lp = parseInt(b);
            for (var i = 0; i < lp; i++) {
                $scope._cash.push(a);
                //$scope.TotalAmount = 0;
            }
            angular.forEach($scope._cash, function (value) {
                $scope.TotalAmount = $scope.TotalAmount + value;
                $scope.order.Tendered = $scope.TotalAmount;
                console.log("Total val " + $scope.TotalAmount);
            });
        }
    }


    //Update on submit Button
    $scope.Dbtotal = [];
    $scope.Dbtotalid = [];
    $scope.totalbillAmounts = 0;
    $scope.Dbcurrency = function () {
        
        for (var i = 0; i < $scope.DBoyCurrency.length; i++) {
            if ($scope.DBoyCurrency[i].check == true) {
                $scope.Dbtotal.push($scope.DBoyCurrency[i]);
            }
        }
        if ($scope.Dbtotal.length > 0 || $scope.Dbtotal != null) {
            for (var j = 0; j < $scope.Dbtotal.length; j++) {
                $scope.Dbtotalid.push($scope.Dbtotal[j].DBoyCId);
                $scope.totalbillAmounts = $scope.totalbillAmounts + $scope.Dbtotal[j].TotalAmount;
                $scope.order.Tendered = $scope.order.Tendered + $scope.Dbtotal[j].TotalAmount;
                //Binod
                $scope.denominations[1] = $scope.denominations[1] + $scope.Dbtotal[j].onerscount;
                $scope.denominations[2] = $scope.denominations[2] + $scope.Dbtotal[j].tworscount;
                $scope.denominations[5] = $scope.denominations[5] + $scope.Dbtotal[j].fiverscount;
                $scope.denominations[10] = $scope.denominations[10] + $scope.Dbtotal[j].tenrscount;
                $scope.denominations[20] = $scope.denominations[20] + $scope.Dbtotal[j].Twentyrscount;
                $scope.denominations[50] = $scope.denominations[50] + $scope.Dbtotal[j].fiftyrscount;
                $scope.denominations[100] = $scope.denominations[100] + $scope.Dbtotal[j].hunrscount;
                $scope.denominations[500] = $scope.denominations[500] + $scope.Dbtotal[j].fivehrscount;
                $scope.denominations[2000] = $scope.denominations[2000] + $scope.Dbtotal[j].twoTHrscount;
            }
            console.log($scope.totalbillAmounts);

        }
    }



    $scope.order = {};
    $scope.totalbillAmount = 0;
    $scope.order.SumTaxAmt = 0;
   
    $scope.cash = [];

    //Clear Cash
    $scope.clearCash = function () {
        
        $scope.checkall = false;
        window.location.reload(true);

        $scope.totalbillAmounts = 0;
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
            1: 0,

        }, $scope._cash = [], $scope.cash = []
    };
    $scope.getAmountRemaining = function () {
        var a = parseFloat(this.getTotalCashPayments()) + parseFloat(this.getTotalCouponPayment()) + parseFloat(this.getTotalCardPayments()) + parseFloat(this.getAdvanceBooking());
        return (parseFloat(this.getSumKotsAndTotalBill()) - parseFloat(a)).toFixed(2)
    };
    $scope.addCashWithoutDenominations = function () {
        var a = parseFloat(this.getSumKotsAndTotalBill());
        a -= parseFloat(this.getTotalCouponPayment()) + parseFloat(this.getTotalCardPayments()) + parseFloat(this.getAdvanceBooking()), $scope._cash = [];
        var b = 0,
            c = !0;
        $scope.cash = [];
        for (var d in this.denominations)
            for (var e = 0; e < parseInt(this.denominations[d]) ; e++) {
                var f = parseInt(d);
                b += f, $scope._cash.push(parseInt(d)), a >= b ? $scope.cash.push(f) : (c && $scope.cash.push(f >= a ? a - (b - f) : a - (b - f)), c = !1)
            }
        console.log($scope.cash)
    }


   
    $scope.getTotalCash = function () {
        var a = 0;
        return _.forEach($scope._cash, function (b) {
            a += b
        }), parseFloat(a).toFixed(2)
    },
     $scope.getTotalCashPayments = function () {
         var a = 0;
         return _.forEach($scope.cash, function (b) {
             a += b
         }), parseFloat(a).toFixed(2)
     };
    $scope.order.Tendered = 0;


    $scope._cash = [];
    $scope.Dbtotal = [];
    $scope.addCash = function (a) {
        
        $scope._cash.push(a);
        $scope.TotalAmount = 0;
        angular.forEach($scope._cash, function (value) {

            
            $scope.TotalAmount = $scope.TotalAmount + value;
        });
        console.log("Total Sum " + $scope.TotalAmount);
        $scope.order.Tendered = $scope.TotalAmount;
        $scope.order.Remaining = $scope.totalbillAmount - $scope.order.Tendered;
        $scope.order.ReturnToCustomer = -$scope.order.Remaining;
        console.log(a);
        console.log($scope._cash);
        console.log($scope.TotalAmount);
        console.log($scope.denominations[a])
        $scope.denominations[a] = "" == $scope.denominations[a] ? 1 : parseInt($scope.denominations[a]) + 1;
    }

    $scope.Dbtotal = [];
    $scope.DBoyCurrency;
    $scope.applyOnBill = function (a) {
        
        console.log(a);
      
        for (var i = 0; i < $scope.DBoyCurrency.length; i++) {
            if ($scope.DBoyCurrency[i].check == true) {
                $scope.v=$scope.DBoyCurrency[i].DueAmount;
                $scope.Dbtotal.push($scope.DBoyCurrency[i]);

            }
        }
        $scope.selecteAmount = angular.copy($scope.Dbtotal);
        $scope.Dbtotalid;
        var datatopost = {
            onerscount: parseInt($scope.denominations[1]),
            OneRupee: parseInt($scope.denominations[1]) * 1,
            tworscount: parseInt($scope.denominations[2]),
            TwoRupee: parseInt($scope.denominations[2]) * 2,
            fiverscount: parseInt($scope.denominations[5]),
            FiveRupee: parseInt($scope.denominations[5]) * 5,
            tenrscount: parseInt($scope.denominations[10]),
            TenRupee: parseInt($scope.denominations[10]) * 10,
            Twentyrscount: parseInt($scope.denominations[20]),
            TwentyRupee: parseInt($scope.denominations[20]) * 20,
            fiftyrscount: parseInt($scope.denominations[50]),
            fiftyRupee: parseInt($scope.denominations[50]) * 50,
            hunrscount: parseInt($scope.denominations[100]),
            HunRupee: parseInt($scope.denominations[100]) * 100,
            fivehrscount: parseInt($scope.denominations[500]),
            fiveHRupee: parseInt($scope.denominations[500]) * 500,
            twoTHrscount: parseInt($scope.denominations[2000]),
            twoTHRupee: parseInt($scope.denominations[2000]) * 2000,
            DueAmount: $scope.v,
            DueAmountstatus: $scope.DueAmountstatus,
        
            //DBoyCId: $scope.Dbtotalid[0],
            TotalAmount:2000 *  $scope.denominations[2000] + 500 * $scope.denominations[500] + 100 * $scope.denominations[100] + 50 * $scope.denominations[50] + 20 * $scope.denominations[20] + 10 * $scope.denominations[10] + 5 *  $scope.denominations[5] + 2 * $scope.denominations[2] + 1 * $scope.denominations[1],
            AssignAmountId: $scope.selecteAmount
        };
        console.log("datatopost", datatopost);
        var txt;
        var r = confirm("Press a button!");
        if (r == true) {
            
            txt = "You pressed OK!";
            //var url = serviceBase + "api/CurrencySettle/Stock?PeopleId=" + $scope.deliveryBoy.PeopleID;
            var url = serviceBase + "api/CurrencySettle/Stockhistory?PeopleId=" + $scope.deliveryBoy.PeopleID  ;
            $http.post(url, datatopost).then(function (results) {
                
             
              
                if (results != null) {
                    alert('Data update successfully');
                    $location.path("/CurrencyStock");
                } else {
                    txt = "You pressed Cancel!";
                   
                }
            })
        }
        else {
            alert('Unsuccessfully update');
            window.location.reload("true");
        }
    

}
    //***** ///////Due amount code start /////////****//  
    $scope.dueamountget = {};
    $scope.dueamount = function () {
        
        var url = serviceBase + "api/CurrencySettle/dueamountget?PeopleId=" + $scope.deliveryBoy.PeopleID + "&status=  Partial Settle";
        $http.get(url).success(function (result) {
            

            $scope.dueamountget = result;
            
        
        });

    }
  

   
    $scope.denominationsrupeed = {
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
    $scope.denominationsd = {
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
    $scope.newcashdue = {};
    $scope.CheckChangeddue = function (pod1) {
        
        $scope.newcashdue = pod1;
        if (pod1.check == true) {
            $scope.denominationsd[1] = parseInt($scope.denominationsd[1]);
            $scope.denominationsrupeed[1] = parseInt($scope.denominationsrupeed[1]);
            $scope.denominationsd[2] = parseInt($scope.denominationsd[2]);
            $scope.denominationsrupeed[2] = parseInt($scope.denominationsrupeed[2]);
            $scope.denominationsd[5] = parseInt($scope.denominationsd[5]);
            $scope.denominationsrupeed[5] = parseInt($scope.denominationsrupeed[5]);
            $scope.denominationsd[10] = parseInt($scope.denominationsd[10]);
            $scope.denominationsrupeed[10] = parseInt($scope.denominationsrupeed[10]);
            $scope.denominationsd[20] = parseInt($scope.denominationsd[20]);
            $scope.denominationsrupeed[20] = parseInt($scope.denominationsrupeed[20]) 
            $scope.denominationsd[50] = parseInt($scope.denominationsd[50]) 
            $scope.denominationsrupeed[50] = parseInt($scope.denominationsrupeed[50]);
            $scope.denominationsd[100] = parseInt($scope.denominationsd[100]);
            $scope.denominationsrupeed[100] = parseInt($scope.denominationsrupeed[100]);
            $scope.denominationsd[500] = parseInt($scope.denominationsd[500]);
            $scope.denominationsrupeed[500] = parseInt($scope.denominationsrupeed[500]);
            $scope.denominationsd[2000] = parseInt($scope.denominationsd[2000]);
            $scope.denominationsrupeed[2000] = parseInt($scope.denominationsrupeed[2000]);
        



        }
        else {
            if ($scope.denominationsd[1] > 0) {
                $scope.denominationsd[1] = parseInt($scope.denominationsd[1]);
            }
            else {
                $scope.denominationsd[1] = 0;
            }
            if ($scope.denominationsd[2] > 0) {
                $scope.denominationsd[2] = parseInt($scope.denominationsd[2]);
            }
            else {
                $scope.denominationsd[2] = 0;
            }

            if ($scope.denominationsd[5] > 0) {
                $scope.denominationsd[5] = parseInt($scope.denominationsd[5]);
            }
            else {
                $scope.denominationsd[5] = 0;
            }
            if ($scope.denominationsd[10] > 0) {
                $scope.denominationsd[10] = parseInt($scope.denominationsd[10]);
            }
            else {
                $scope.denominationsd[10] = 0;
            }
            if ($scope.denominationsd[20] > 0) {
                $scope.denominationsd[20] = parseInt($scope.denominationsd[20]);
            }
            else {
                $scope.denominationsd[20] = 0;
            }
            if ($scope.denominationsd[50] > 0) {
                $scope.denominationsd[50] = parseInt($scope.denominationsd[50]);
            }
            else {
                $scope.denominationsd[50] = 0;
            }
            if ($scope.denominationsd[100] > 0) {
                $scope.denominationsd[100] = parseInt($scope.denominationsd[100]);
            }
            else {
                $scope.denominationsd[100] = 0;
            }
            if ($scope.denominationsd[500] > 0) {
                $scope.denominationsd[500] = parseInt($scope.denominationsd[500]);
            }
            else {
                $scope.denominationsd[500] = 0;
            }
            if ($scope.denominationsd[2000] > 0) {
                $scope.denominationsd[2000] = parseInt($scope.denominationsd[2000]);
            }
            else {
                $scope.denominationsd[2000] = 0;
            }
      


        }
    }


    $scope.addManualCash1 = function (a1, b1) {

        $scope.newcash1 = [];
        _.each($scope._cash1, function (obj1) {
            if (a1 != obj1) {
                $scope.newcash1.push(obj1);
            }

        })
        $scope._cash1 = $scope.newcash1;
        if (b1 > 0) {

            $scope.TotalAmount1 = 0;
            var lp1 = parseInt(b1);
            for (var j = 0; j < lp1; j++) {
                $scope._cash.push(a);

            }
            angular.forEach($scope._cash1, function (value1) {

                console.log("value1", value1);
                $scope.TotalAmount1 = $scope.TotalAmount1 + value1;

                //$scope.order.Tendered = $scope.TotalAmount1;
                console.log(" $scope.TotalAmount1 " + $scope.TotalAmount1);
            });
           
        }
    else {

            $scope.TotalAmount1 = 0;
            var lp1 = parseInt(b1);
            for (var g = 0; g < lp1; g++) {
                $scope._cash.push(a);
                //////////$scope.TotalAmount = 0;
            }
            angular.forEach($scope._cash1, function (value1) {
                $scope.TotalAmount1 = $scope.TotalAmount1 + value1;
                //$scope.order.Tendered = $scope.TotalAmount1;
                console.log(" $scope.TotalAmount1 " + $scope.TotalAmount1);
            });
        }


    }
    $scope.clearCash = function () {

        $scope.denominationsd = {
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
        }, $scope._cash1 = [], $scope.cash1 = []
        $scope.TotalAmount1 = " ";

       
    };
   
    $scope.Submit = function (pod1) {
        
        $scope.newcashdue;
        alert("Are you show click Settle");
        console.log("    $scope.newcashdue", $scope.newcashdue);
        //var url = serviceBase + "api/CurrencyStock/BanksettleCurrency?CurrencyStockid=" + 9;
        var datatopost = {
            onerscount: parseInt($scope.denominationsd[1]),
            OneRupee: parseInt($scope.denominationsd[1] * 1),
            tworscount: parseInt($scope.denominationsd[2]),
            TwoRupee: parseInt($scope.denominationsd[2] * 2),
            fiverscount: parseInt($scope.denominationsd[5]),
            FiveRupee: parseInt($scope.denominationsd[5] * 5),
            tenrscount: parseInt($scope.denominationsd[10]),
            TenRupee: parseInt($scope.denominationsd[10] * 10),
            Twentyrscount: parseInt($scope.denominationsd[20]),
            TwentyRupee: parseInt($scope.denominationsd[20] * 20),
            fiftyrscount: parseInt($scope.denominationsd[50]),
            fiftyRupee: parseInt($scope.denominationsd[50] * 50),
            hunrscount: parseInt($scope.denominationsd[100]),
            HunRupee: parseInt($scope.denominationsd[100] * 100),
            fivehrscount: parseInt($scope.denominationsd[500]),
            fiveHRupee: parseInt($scope.denominationsd[500] * 500),
            twoTHrscount: parseInt($scope.denominationsd[2000]),
            twoTHRupee: parseInt($scope.denominationsd[2000] * 2000),
            DueAmount: $scope.newcashdue.Dueamount,
            DBoyCId: $scope.newcashdue.DBoyCId,
        };
        console.log("datatopost", datatopost);
        var txt;
        var r = confirm("Press a button!");
        if (r == true) {
            
            txt = "You pressed OK!";
            //var url = serviceBase + "api/CurrencySettle/Stock?PeopleId=" + $scope.deliveryBoy.PeopleID;
            var url = serviceBase + "api/CurrencySettle/Stockhistorydue?PeopleId=" + $scope.deliveryBoy.PeopleID;
            $http.post(url, datatopost).then(function (results) {
                


                if (results != null) {
                    alert('Data update successfully');
                    $location.path("/CurrencyStock");
                } else {
                    txt = "You pressed Cancel!";

                }
            })
        }
        else {
            alert('Unsuccessfully update');
            window.location.reload("true");
        }
    }
    //***** ///////Due amount code End /////////****// 

    $scope.Checkdetails = function () {
        
     
        var url = serviceBase + "api/CurrencySettle/checkdata?PeopleId=" + $scope.deliveryBoy.PeopleID;
        $http.get(url).success(function (result) {
            
            $scope.checkdt = result;
            
        })
    }

    $scope.openrecive = function (data) {
        
    
        alert("Are you show click Settle");
      
        var datatopost = {
          
            DeliveryIssuanceId: data.DeliveryIssuanceId,
            PeopleId: data.PeopleId,
            Peoplename:data.Peoplename,
            DBoyCId: data.DBoyCId,
            checkamount: data.checkamount,
            checknumber: data.checknumber,
            checkTotalAmount: data.checkTotalAmount,
        };
        console.log("datatopost", datatopost);
        var txt;
        var r = confirm("Press a button!");
        if (r == true) {
            
            txt = "You pressed OK!";
            //var url = serviceBase + "api/CurrencySettle/Stock?PeopleId=" + $scope.deliveryBoy.PeopleID;
            var url = serviceBase + "api/CurrencySettle/Checkdeatil?PeopleId=" + $scope.deliveryBoy.PeopleID;
            $http.post(url, datatopost).then(function (results) {
                


                if (results != null) {
                    alert('Data update successfully');
                    $location.path("/CurrencyStock");
                } else {
                    txt = "You pressed Cancel!";

                }
            })
        }
        else {
            alert('Unsuccessfully update');
            window.location.reload("true");
        }
    
    }
}]);





