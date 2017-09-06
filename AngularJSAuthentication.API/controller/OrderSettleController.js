'use strict';
app.controller('OrderSettleController', ['$scope', 'OrderMasterService', 'OrderDetailsService', '$http', 'ngAuthSettings', '$filter', "ngTableParams", '$modal', "DeliveryService",
    function ($scope, OrderMasterService, OrderDetailsService, $http, ngAuthSettings, $filter, ngTableParams, $modal, DeliveryService) {
        console.log("orderMasterController start loading OrderDetailsService");
        //new pagination 
        $scope.pageno = 1; //initialize page no to 1
        $scope.total_count = 0;
        $scope.itemsPerPage = 50; //this could be a dynamic value from a drop down
        $scope.numPerPageOpt = [50, 100, 150];//dropdown options for no. of Items per page
        $scope.onNumPerPageChange = function () {
            $scope.itemsPerPage = $scope.selected;
        }
        $scope.selected = $scope.numPerPageOpt[0];// for Html page dropdown
        DeliveryService.getdboys().then(function (results) {
            $scope.DBoys = results.data;
        }, function (error) {
        });

        $scope.deliveryBoy = {};
        
        $scope.getData = function (pageno, Dboyno, startdate, enddate, OrderId) { // This would fetch the data on page change.  //In practice this should be in a factory.
            
            $scope.customers = [];
            if (!Dboyno) Dboyno = "all";
            $scope.listMaster = [];
            if ($scope.deliveryBoy.Mobile) Dboyno = $scope.deliveryBoy.Mobile;
            var url = serviceBase + "api/OrderDispatchedMaster" + "?list=" + $scope.itemsPerPage + "&page=" + pageno + "&DBoyNo=" + Dboyno + "&datefrom=" + startdate + "&dateto=" + enddate + "&OrderId=" + $scope.OrderId;
            $http.get(url).success(function (response) {
                if (response.ordermaster.length == 0) {
                    alert("Not Found");
                }
                else {

                    $scope.listMaster = response.ordermaster;  //ajax request to fetch data into vm.data
                    $scope.listMasterold = angular.copy(response.ordermaster);
                    console.log("get all Order:");
                    console.log($scope.customers);
                    $scope.orders = $scope.customers;
                    $scope.total_count = response.total_count;
                    $scope.tempuser = response.ordermaster;
                }
               
            });
        };

        $scope.getData($scope.pageno, "all", "", "", "");

        $scope.checkAll = function () {

            if ($scope.selectedAll) {
                $scope.selectedAll = false;
            } else {
                $scope.selectedAll = true;
            }
            angular.forEach($scope.listMaster, function (trade) {
                trade.check = $scope.selectedAll;
            });

        };
        $scope.selectedsettled = function () {
            $scope.assignedorders = [];
            $scope.selectedorders = [];
            for (var i = 0; i < $scope.listMaster.length; i++) {
                if ($scope.listMaster[i].check == true && $scope.listMaster[i].ShortAmount > 0 && ($scope.listMaster[i].ShortReason == "" || $scope.listMaster[i].ShortReason == undefined || $scope.listMaster[i].ShortReason == null || $scope.listMaster[i].ShortReason == "null")) {
                    alert('please select reason for short Amount');
                }

                else {
                    $scope.assignedorders.push($scope.listMaster[i]);
                }
            }
            $scope.selectedorders = angular.copy($scope.assignedorders);
            var dataToPost = {
                AssignedOrders: $scope.selectedorders
            };
            var url = serviceBase + 'api/OrderDispatchedMasterFinal/Multisettle';
            $http.post(url, dataToPost)
                .success(function (data) {
                    if (data != null) {
                        alert(" Selected order settled succefully");
                        location.reload();
                    }
                    else {
                        alert("Selected order not settled  succefully");
                    }

                })
                 .error(function (data) {
                     console.log("Error Got Heere is ");
                     console.log(data);
                 })

        }

        $scope.filterstatus = function (data) {
            if (data.name === 'Show All') {
                $scope.listMaster = $scope.listMasterold;
            } else {
                $scope.listMaster = $filter('filter')($scope.listMasterold, { Status: data.name });
            }
        }

        $scope.Sattled = function (ord) {


            if (ord.ShortAmount > 0 && (ord.ShortReason == "" || ord.ShortReason == undefined || ord.ShortReason == null || ord.ShortReason == "null")) {
                alert('please select reason for short Amount');
            }
            else {

                var orddetails = ord.orderDetails;
                var url = serviceBase + 'api/OrderDispatchedMasterFinal';
                $http.post(url, ord)
                .success(function (data) {
                    $scope.FdispatchedMasterID = data.FinalOrderDispatchedMasterId;
                    $scope.dispatchedDetailFinal(orddetails);
                    $("#st" + data.OrderId).prop("disabled", true);
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
        }

        $scope.dispatchedDetailFinal = function (orddetails) {
            $scope.orderDetailsDisp = orddetails;
            var url = serviceBase + 'api/OrderDispatchedDetailsFinal';
            $http.post(url, $scope.orderDetailsDisp)
            .success(function (data) {
                alert('insert successfully');
                // location.reload();
                window.location = "#/orderSettle";
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

        $(function () {
            $('input[name="daterange"]').daterangepicker({
                timePicker: true,
                timePickerIncrement: 5,
                timePicker12Hour: true,
                format: 'MM/DD/YYYY h:mm A'
            });

        });


        $scope.Search = function (data) {
            
            $scope.dataforsearch = { datefrom: "", dateto: "" };
            var f = $('input[name=daterangepicker_start]');
            var g = $('input[name=daterangepicker_end]');
            if (!$('#dat').val()) {
                $scope.dataforsearch.datefrom = '';
                $scope.dataforsearch.dateto = '';
            }
            else {
                $scope.dataforsearch.datefrom = f.val();
                $scope.dataforsearch.dateto = g.val();
            }
            if (data != undefined) {
                $scope.deliveryBoy = JSON.parse(data);

                if ($scope.deliveryBoy.Mobile) {
                    $scope.getData($scope.pageno, $scope.deliveryBoy.Mobile, $scope.dataforsearch.datefrom, $scope.dataforsearch.dateto, $scope.OrderId);
                }
            }
            else if (data == undefined) {

                $scope.getData($scope.pageno, "all", $scope.dataforsearch.datefrom, $scope.dataforsearch.dateto, $scope.OrderId);
            }

            else {
                $scope.getData($scope.pageno, "all", $scope.dataforsearch.datefrom, $scope.dataforsearch.dateto, $scope.OrderId);
            }
        }

        $scope.show = true;
        $scope.order = false;

        $scope.showalldetails = function () {
            $scope.order = !$scope.order;
            $scope.show = !$scope.show;
            // $scope.callmethoddetails();
        };






        //$scope.Return = function (data) {
        //    OrderMasterService.saveReturn(data);
        //    console.log("Order Detail Dialog called ...");
        //};

        //$scope.showDetail = function (data) {
        //    OrderMasterService.save(data);
        //    console.log("Order Detail Dialog called ...");
        //    //var modalInstance;
        //    //modalInstance = $modal.open(
        //    //    {
        //    //        templateUrl: "myModaldeleteOrderGet.html",
        //    //        controller: "ModalInstanceCtrlOrderDetail",
        //    //        resolve:
        //    //            {
        //    //                order: function () {
        //    //                    return data
        //    //                }
        //    //            }
        //    //    }), modalInstance.result.then(function () {
        //    //    },
        //    //    function () {
        //    //        console.log("Cancel Condintion");
        //    //    })
        //};


        //$scope.showInvoice = function (data) {
        //    OrderMasterService.save1(data);
        //    console.log("Order Invoice  called ...");
        //    var modalInstance;
        //    modalInstance = $modal.open(
        //        {
        //            templateUrl: "myModaldeleteOrderInvoice.html",
        //            controller: "ModalInstanceCtrlOrderInvoice",
        //            resolve:
        //                {
        //                    order: function () {
        //                        return data
        //                    }
        //                }
        //        }), modalInstance.result.then(function () {
        //        },
        //        function () {
        //            console.log("Cancel Condintion");
        //        })
        //};

        //$scope.open = function (data) {
        //    OrderMasterService.save(data);
        //    //.then(function (results) {
        //    //    console.log("master save fn");
        //    //    console.log(results);
        //    //}, function (error) {
        //    //    console.log(error);
        //    //});
        //};


        //$scope.invoice = function (data) {
        //    OrderMasterService.view(data).then(function (results) {
        //    }, function (error) {
        //    });
        //};


        //$scope.orderDetails = {};
        //OrderDetailsService.getdetails().then(function (results) {
        //    console.log("kkkkkk");
        //    console.log(results.data);
        //    $scope.orderDetails = results.data;
        //}, function (error) {
        //});

        //$scope.callmethoddetails = function () {
        //    var init;
        //    return $scope.stores = $scope.orderDetails,
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

        //$scope.showDetailDelivery = function (data) {
        //    console.log("Edit Dialog called city");
        //    var modalInstance;
        //    modalInstance = $modal.open(
        //        {
        //            templateUrl: "deliveredorderdetail.html",
        //            controller: "myredispatchdetailCtrl", resolve: { order: function () { return data } }
        //        }), modalInstance.result.then(function (selectedItem) {


        //        },
        //        function () {
        //            console.log("Cancel Condintion");

        //        })
        //};

    }]);

app.controller("ModalInstanceCtrlOrderDetail", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order detail modal opened");

        $scope.orderDetails = [];
        if (order) {
            $scope.OrderData = order;
            $scope.orderDetails = $scope.OrderData.orderDetails;
            console.log("found order");
            console.log($scope.OrderData);
            console.log($scope.OrderData.orderDetails);
            console.log($scope.orderDetails);
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        $scope.Details = function (dataToPost, $index) {
            console.log("Show Order Details  controller");
            console.log(dataToPost);
            OrderMasterService.getDeatil.then(function (results) {
                console.log("Details");
                console.log("index of item " + $index);
                console.log($scope.order.length);
                $scope.order.splice($index, 1);
                console.log($scope.order.length);
                $modalInstance.close(dataToPost);
                ReloadPage();
            }, function (error) {
                alert(error.data.message);
            });
        }
    }]);

app.controller("ModalInstanceCtrlOrderInvoice", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order invoice modal opened");
        $scope.OrderDetails = {};
        $scope.OrderData = {};
        var d = OrderMasterService.getDeatil();

        $scope.OrderData = d;
        $scope.orderDetails = d.orderDetails;
        $scope.Itemcount = 0;
        for (var i = 0 ; i < $scope.orderDetails.length; i++) {
            $scope.Itemcount = $scope.Itemcount + $scope.orderDetails[i].qty;
        }

        $scope.totalfilterprice = 0;
        _.map($scope.OrderData.orderDetails, function (obj) {
            console.log("count total");
            $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
            console.log(obj.TotalAmt);
            console.log($scope.totalfilterprice);
        })
    }]);

app.controller("ModalInstanceCtrldeleteOrder", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, myData) {
        console.log("delete modal opened");
        function ReloadPage() {
            location.reload();
        };

        $scope.orders = [];
        if (myData) {
            $scope.orders = myData.order1;
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        $scope.deleteorders = function (dataToPost, $index) {
            console.log("Delete  controller");
            console.log(dataToPost);
            OrderMasterService.deleteorder(dataToPost).then(function (results) {
                console.log("Del");
                $modalInstance.close(dataToPost);
            }, function (error) {
                alert(error.data.message);
            });
        }
    }])