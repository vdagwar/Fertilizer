'use strict'
app.controller('NetprofitController', ['$scope', "$filter", "$http", "ngTableParams", function ($scope, $filter, $http, ngTableParams) {
    var User = JSON.parse(localStorage.getItem('RolePerson'));
    if (User.role == "Administrator") {
        $scope.datrange = '';
        $scope.datefrom = '';
        $scope.dateto = '';
        $(function () {
            $('input[name="daterange"]').daterangepicker({
                timePicker: true,
                timePickerIncrement: 5,
                timePicker24Hour: true,
                format: 'YYYY-MM-DD h:mm A'
            });
        });
        $scope.getFormatedDate = function (date) {
            console.log("here get for")
            date = new Date(date);
            var dd = date.getDate();
            var mm = date.getMonth() + 1; //January is 0!
            var yyyy = date.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var date = yyyy + '-' + mm + '-' + dd;
            return date;
        }
        var Allbydate = [];
        var finallistoflist = [];
        $scope.filtype = "";
        $scope.type = "";

        var titleText = "";
        var legendText = "";
        
        $scope.selTime = [
      { value: 1, text: "Day" },
      { value: 2, text: "Month" },
      { value: 3, text: "Year" }
        ];
        $scope.selType = [
           { value: 1, text: "Total Order" },
           { value: 2, text: "Total sale" },
           { value: 3, text: "Active Retailer" },
           { value: 4, text: "Active Brand" }
        ];
        $scope.selectType = [
            { value: 4, text: "Excecutive" },
            { value: 2, text: "Hub" },
            { value: 3, text: "City" },
            { value: 5, text: "Cluster" }
        ];
        $scope.dataselect = [];
        $scope.examplemodel = [];
        $scope.exampledata = $scope.dataselect;
        $scope.examplesettings = {
            displayProp: 'DisplayName', idProp: 'PeopleID',
            scrollableHeight: '300px',
            scrollableWidth: '450px',
            enableSearch: true,
            scrollable: true
        };
        $scope.subexamplemodel = [];
        $scope.subexamplemodel = $scope.dataselect;
        $scope.subexamplesettings = {
            displayProp: 'WarehouseName', idProp: 'Warehouseid',
            scrollableHeight: '300px',
            scrollableWidth: '450px',
            enableSearch: true,
            scrollable: true
        };
        $scope.ssubexamplemodel = [];
        $scope.ssubexamplemodel = $scope.dataselect;
        $scope.ssubexamplesettings = {
            displayProp: 'CityName', idProp: 'Cityid',
            scrollableHeight: '300px',
            scrollableWidth: '450px',
            enableSearch: true,
            scrollable: true
        };
        $scope.clusterModel = [];
        $scope.clusterModel = $scope.dataselect;
        $scope.clusterSetting = {
            displayProp: 'ClusterName', idProp: 'ClusterId',
            scrollableHeight: '300px',
            scrollableWidth: '450px',
            enableSearch: true,
            scrollable: true
        };

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
        };

        $scope.genereteReport = function (data1) {
            
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

            console.log("here daterange");
            console.log($scope.dataforsearch.datefrom);
            console.log($scope.dataforsearch.dateto);

            var ids = [];
            if (data1.type == 4) {
                _.each($scope.examplemodel, function (o2) {
                    var Row = o2.id;
                    ids.push(Row);
                })
            }
            else if (data1.type == 2) {
                _.each($scope.subexamplemodel, function (o2) {
                    var Row = o2.id;
                    ids.push(Row);
                })
            }
            else if (data1.type == 3) {
                _.each($scope.ssubexamplemodel, function (o2) {
                    var Row = o2.id;
                    ids.push(Row);
                })
            }
            else if (data1.type == 5) {
                _.each($scope.clusterModel, function (o2) {
                    var Row = o2.id;
                    ids.push(Row);
                })
            }

            var url = serviceBase + "api/NetProfit?datefrom=" + $scope.dataforsearch.datefrom + "&dateto=" + $scope.dataforsearch.dateto + "&type=" + data1.type + "&ids=" + ids;
            $http.get(url)
            .success(function (data) {
                
                if (data.length == 0) {
                    alert("Data Not Present");   
                }
                else {
                    $scope.proft = data[0].value;
                }
            });
        }
    }
    else {
        window.location = "#/404";
    }
}]);