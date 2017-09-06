'use strict';
app.controller('ShortageSettleController', ['$scope', 'OrderMasterService', 'OrderDetailsService', '$http', 'ngAuthSettings', '$filter', "ngTableParams", '$modal', "DeliveryService",
    function ($scope, OrderMasterService, OrderDetailsService, $http, ngAuthSettings, $filter, ngTableParams, $modal, DeliveryService) {
        $(function () {
            $('input[name="daterange"]').daterangepicker({
                timePicker: true,
                timePickerIncrement: 5,
                timePicker12Hour: true,
                format: 'MM/DD/YYYY h:mm A'
            });

        });
        console.log("orderMasterController start loading OrderDetailsService");
        $scope.currentPageStores = {};
        $scope.statuses = [];
        $scope.selected = {};
        // new pagination 
        $scope.pageno = 1; // initialize page no to 1
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

        $scope.getData = function (pageno, Dboyno, startdate, enddate) { // This would fetch the data on page change.
            //In practice this should be in a factory.
            $scope.customers = [];

            if (!Dboyno) Dboyno = "all";
            if ($scope.deliveryBoy.Mobile) Dboyno = $scope.deliveryBoy.Mobile;
            var url = serviceBase + "api/ShortageSettle" + "?list=" + $scope.itemsPerPage + "&page=" + pageno + "&DBoyNo=" + Dboyno + "&datefrom=" + startdate + "&dateto=" + enddate;;
            $http.get(url).success(function (response) {
                
                $scope.listMaster = response.ordermaster;  //ajax request to fetch data into vm.data
                $scope.listMasterold = angular.copy(response.ordermaster);
                console.log("get all Order:");
                console.log($scope.customers);
                $scope.orders = $scope.customers;
                //$scope.listMaster = $scope.customers;
                $scope.total_count = response.total_count;
                $scope.tempuser = response.ordermaster;
            });
        };
        //$scope.getData($scope.pageno, "all", "", "");

        $scope.deliveryBoy = {};
        $scope.dataforsearch = { datefrom: "", dateto: "" };
        $scope.getdborders = function (DB) {
            $scope.totalAmountOfOrders = 0;
            if (DB != "") {
                $scope.deliveryBoy = JSON.parse(DB);
                var f = $('input[name=daterangepicker_start]');
                var g = $('input[name=daterangepicker_end]');
                console.log("sumit");
                console.log(f);
                console.log(g);
                $scope.dataforsearch.datefrom = f.val();
                $scope.dataforsearch.dateto = g.val();
                $scope.getData($scope.pageno, $scope.deliveryBoy.Mobile, $scope.dataforsearch.datefrom, $scope.dataforsearch.dateto);
            }
        }
    }]);