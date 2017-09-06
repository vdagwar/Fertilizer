'use strict';
app.controller('unitEcoController', ['$scope', "$filter", "$http", "ngTableParams", '$modal', function ($scope, $filter, $http, ngTableParams, $modal) {
    console.log("UE Controller reached");
    var input = document.getElementById("file");
    $scope.unitEconomics = {};

    $http.get(serviceBase + "api/uniteconomic")
    .success(function (data) {
        if (data.length != 0) {
            $scope.unitEconomics = data;
        }
    });
    
    ////.................File Uploader method start..................
    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }
    function sendFileToServer(formData, status) {
        var uploadURL = "/api/UnitEconomicupload/post"; //Upload URL
        var extraData = {}; //Extra Data.
        var jqXHR = $.ajax({
            xhr: function () {
                var xhrobj = $.ajaxSettings.xhr();
                if (xhrobj.upload) {
                    xhrobj.upload.addEventListener('progress', function (event) {
                        var percent = 0;
                        var position = event.loaded || event.position;
                        var total = event.total;
                        if (event.lengthComputable) {
                            percent = Math.ceil(position / total * 100);
                        }
                        //Set progress
                        status.setProgress(percent);
                    }, false);
                }
                return xhrobj;
            },
            url: uploadURL,
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: formData,
            success: function (data) {
                status.setProgress(100);
                $("#status1").append("Data from Server: " + data + "<br>");
                //alert("Succesfully Submitted...........");               
            },
        });
        status.setAbort(jqXHR);
    }
    var rowCount = 0;
    function createStatusbar(obj) {
        rowCount++;
        var row = "odd";
        if (rowCount % 2 == 0) row = "even";
        this.statusbar = $("<div class='statusbar " + row + "'></div>");
        this.filename = $("<div class='filename'></div>").appendTo(this.statusbar);
        this.size = $("<div class='filesize'></div>").appendTo(this.statusbar);
        this.progressBar = $("<div class='progressBar'><div></div></div>").appendTo(this.statusbar);
        this.abort = $("<div class='abort'>Abort</div>").appendTo(this.statusbar);
        obj.after(this.statusbar);
        this.setFileNameSize = function (name, size) {
            var sizeStr = "";
            var sizeKB = size / 1024;
            if (parseInt(sizeKB) > 1024) {
                var sizeMB = sizeKB / 1024;
                sizeStr = sizeMB.toFixed(2) + " MB";
            }
            else {
                sizeStr = sizeKB.toFixed(2) + " KB";
            }
            this.filename.html(name);
            this.size.html(sizeStr);
        }
        this.setProgress = function (progress) {
            var progressBarWidth = progress * this.progressBar.width() / 100;
            this.progressBar.find('div').animate({ width: progressBarWidth }, 10).html(progress + "%&nbsp;");
            if (parseInt(progress) >= 100) {
                this.abort.hide();
            }
        }
        this.setAbort = function (jqxhr) {
            var sb = this.statusbar;
            this.abort.click(function () {
                jqxhr.abort();
                sb.hide();
            });
        }
    }
    function handleFileUpload(files, obj) {
        for (var i = 0; i < files.length; i++) {
            var fd = new FormData();
            fd.append('file', files[i]);
            var status = new createStatusbar(obj); //Using this we can set progress.
            status.setFileNameSize(files[i].name, files[i].size);
            sendFileToServer(fd, status);
        }
    }
    $(document).ready(function () {
        var obj = $("#dragandrophandler");
        obj.on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $(this).css('border', '2px solid #0B85A1');
        });
        obj.on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });
        obj.on('drop', function (e) {

            $(this).css('border', '2px dotted #0B85A1');
            e.preventDefault();
            var files = e.originalEvent.dataTransfer.files;
            handleFileUpload(files, obj);
        });
        $(document).on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });
        $(document).on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
            obj.css('border', '2px dotted #0B85A1');
        });
        $(document).on('drop', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });

    });

    //............................Exel export Method.....................//
    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    };

    $scope.exportData = function () {
        $scope.stores = $scope.unitEconomics;
        alasql('SELECT unitId,Label1,Label2,Label3,Warehouseid,Amount,CreatedDate,Discription,[IsActive],[Deleted] INTO XLSX("Unit Economic.xlsx",{headers:true}) FROM ?', [$scope.stores]);

    };

    $scope.open = function () {
        console.log("Modal opened");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myUEModal.html",
                controller: "unitEcoAddController", resolve: { object: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.unitEconomics.push(selectedItem);
            },
            function () {
                console.log("Cancel Condintion");             
            })
    };

    $scope.edit = function (item) {
        console.log("Edit Dialog");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myUEModal.html",
                controller: "unitEcoAddController", resolve: { object: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.unitEconomics.push(selectedItem);
            },
            function () {
                console.log("Cancel Condintion");               
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log($index);
        console.log("Delete Dialog called for city");
       
        var myData = { all: $scope.currentPageStores ,city1:data};

       
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteCity.html",
                controller: "ModalInstanceCtrldeleteCity", resolve: { city: function () { return myData } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.currentPageStores.splice($index,1);
            },
            function () {
                console.log("Cancel Condintion");
             
            })
        //$scope.city.splice($scope.city.indexOf($scope.city), 1)
       // $scope.city.splice($index, 1);
    };       
}]);

app.controller("unitEcoAddController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", 'object', 'WarehouseService', function ($scope, $http, ngAuthSettings, $modalInstance, object, WarehouseService) {
    console.log("city");
    $scope.UnitEconomicData = {};
    if (object) {
        $scope.UnitEconomicData = object;
    }
    
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.lablel1type = [
        { name: "Sales Exp" }, { name: "Logistic Exp" }, { name: "Operation Exp" }
    ]
    $scope.lablel2type = [
        { name: "People" }, { name: "Marketing  & promo" }, { name: "Del Logistics" }, { name: "Pur logisticsp" }, { name: "Rent" }, { name: "Other exp" }
    ]
    $scope.lablel3type = [
      { name: "Vehicle" }, { name: "People" }, { name: "Operation" }, { name: "Purchase" }, { name: "Helper" }
    ]

    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouses = results.data;
    }, function (error) {
    });
    $scope.AddUnitEconomic = function () {
        console.log("Add UnitEconomic");
        var url = serviceBase + "api/uniteconomic";
        var dataToPost = {
            unitId: $scope.UnitEconomicData.unitId,
            Warehouseid: $scope.UnitEconomicData.Warehouseid,
            Label1: $scope.UnitEconomicData.label1,
            Label2: $scope.UnitEconomicData.label2,
            Label3: $scope.UnitEconomicData.label3,
            Amount: $scope.UnitEconomicData.Amount,
            Discription: $scope.UnitEconomicData.Discription,
        };
        console.log(dataToPost);

        $http.post(url, dataToPost)
        .success(function (data) {
            console.log("Error Got Here");
            console.log(data);
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    console.log("Got This User Already Exist");
                    $scope.AlreadyExist = true;
                }
            }
            else {
                $modalInstance.close(data);
            }
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
         })
    };
}])