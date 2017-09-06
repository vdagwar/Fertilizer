'use strict';
app.controller('CurrentStockController', ['CurrentStockService', '$scope', "$filter", "$http", "ngTableParams",'$modal',
    function (CurrentStockService, $scope, $filter, $http, ngTableParams,$modal) {
    console.log(" Current Stock Controller reached");
    $scope.currentPageStores = {};
    $scope.Getstock = [];
    CurrentStockService.getstock().then(function (results) {
        $scope.Getstock = results.data;
        console.log("orders..........");
        console.log($scope.Getstock);
        $scope.callmethod();
    }, function (error) {
    });
        //*************************************************************************************************************//
    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData1 = function () {
        alasql('SELECT ItemNumber,StockId,ItemName,CurrentInventory,WarehouseName INTO XLSX("Currentstock.xlsx",{headers:true}) FROM ?', [$scope.stores]);
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("CStock.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };
        //***************************************************************************************************************
    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }  
    function sendFileToServer(formData, status) {
        var uploadURL = "/api/currentstockupload/post"; //Upload URL
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

                $("#status1").append("Data from Server:" + data + "<br>");
            }
        });

        status.setAbort(jqXHR);
    }
    console.log("Vikash start");
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

            //We need to send dropped files to Server
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
        //****************upload****************************************************************************************///
    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.Getstock,

            $scope.searchKeywords = "",
            $scope.filteredStores = [],
            $scope.row = "",

            $scope.select = function (page) {
                var end, start; console.log("select"); console.log($scope.stores);
                return start = (page - 1) * $scope.numPerPage, end = start + $scope.numPerPage, $scope.currentPageStores = $scope.filteredStores.slice(start, end)
            },

            $scope.onFilterChange = function () {
                console.log("onFilterChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1, $scope.row = ""
            },

            $scope.onNumPerPageChange = function () {
                console.log("onNumPerPageChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },

            $scope.onOrderChange = function () {
                console.log("onOrderChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },

            $scope.search = function () {
                console.log("search");
                console.log($scope.stores);
                console.log($scope.searchKeywords);

                return $scope.filteredStores = $filter("filter")($scope.stores, $scope.searchKeywords), $scope.onFilterChange()
            },

            $scope.order = function (rowName) {
                console.log("order"); console.log($scope.stores);
                return $scope.row !== rowName ? ($scope.row = rowName, $scope.filteredStores = $filter("orderBy")($scope.stores, rowName), $scope.onOrderChange()) : void 0
            },

            $scope.numPerPageOpt = [30, 50, 100, 200],
            $scope.numPerPage = $scope.numPerPageOpt[1],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })

        ()


    }
        //-----for popupdial-----
        // new pagination 
    $scope.pageno = 1; //initialize page no to 1
    $scope.total_count = 0;

    $scope.itemsPerPage = 30;  //this could be a dynamic value from a drop down

    $scope.numPerPageOpt = [30, 60, 90, 100];  //dropdown options for no. of Items per page

    $scope.onNumPerPageChange = function () {
        $scope.itemsPerPage = $scope.selected;

    }
    $scope.selected = $scope.numPerPageOpt[0];  //for Html page dropdown

    $scope.$on('$viewContentLoaded', function () {
        $scope.oldStocks($scope.pageno);
    });

    $scope.StockId = 0;
    $scope.oldStock = function (data) {

        $scope.StockId = data.StockId;
        $scope.oldStocks($scope.pageno);
    }
   
    $scope.oldStocks = function (pageno) {
        $scope.OldStockData = [];
        var url = serviceBase + "api/CurrentStock"+"?StockId=" + $scope.StockId + "&list=" + $scope.itemsPerPage + "&page=" + pageno;
        $http.get(url).success(function (response) {
            $scope.OldStockData = response.ordermaster;
            $scope.total_count = response.total_count;
            console.log($scope.OldStockData);
           
        })
      .error(function (data) {
      })
    }

   
//for manual inventory
    $scope.edit = function (item) {
        
        console.log("Edit Dialog called survey");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myInventoryModalPut.html",
                controller: "ModalInstanceCtrlInventoryedit", resolve: { inventory: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.Getstock.push(selectedItem);
                _.find($scope.Getstock, function (inventory) {
                    if (inventory.StockId == selectedItem.StockId) {
                        inventory = selectedItem;
                    }
                });
                $scope.Getstock = _.sortBy($scope.Getstock, 'StockId').reverse();
                $scope.selected = selectedItem;
            },
            function () {
                console.log("Cancel Condintion");
            })
    };
    }]);
app.controller("ModalInstanceCtrlInventoryedit", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "inventory", function ($scope, $http, ngAuthSettings, $modalInstance, inventory) {
    console.log("Iventory");
    $scope.xy = true;
    $scope.inventoryData = {};
    if (inventory) {
        
        console.log("category if conditon");
        $scope.inventoryData = inventory;
    }
    $scope.updatelineitem = function (data) {
  
        $scope.inventoryData.CurrentInventory = data.CurrentInventory - 1;
    }
    $scope.updatelineitem1 = function (data) {
        $scope.inventoryData.CurrentInventory = data.CurrentInventory + 1;
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.Putinventory = function (data) {
        
        if ($scope.inventoryData.ManualReason != null) {

            var url = serviceBase + "api/CurrentStock";
            var dataToPost = {
                CurrentInventory: $scope.inventoryData.CurrentInventory,
                StockId: $scope.inventoryData.StockId,
                ManualReason: $scope.inventoryData.ManualReason,
            };
            console.log(dataToPost);
            $http.put(url, dataToPost)
            .success(function (data) {
                
                $modalInstance.close(data);
            })
             .error(function (data) {
                 console.log(data);
             })
        }
        else {

            alert('please enter reason for change Qty');
        }
        }
}])



  







