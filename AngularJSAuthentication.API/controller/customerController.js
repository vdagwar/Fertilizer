'use strict';
app.controller('customerController', ['$scope', 'customerService', 'CityService', 'WarehouseService', "$filter", "$http", "ngTableParams", 'FileUploader', '$modal', '$log', function ($scope, customerService, CityService, WarehouseService, $filter, $http, ngTableParams, FileUploader, $modal, $log) {

    //.................File Uploader method start..................
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A'
        });
    });

    $scope.citys = [];
    CityService.getcitys().then(function (results) {
        $scope.citys = results.data;
    }, function (error) {  });

    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {
        $scope.warehouse = results.data;
    }, function (error) {  });

    $scope.uploadshow = true;
    $scope.toggle = function () {
        $scope.uploadshow = !$scope.uploadshow;
    }

    function sendFileToServer(formData, status) {
        var uploadURL = "/api/customerupload/post"; //Upload URL
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

    //............................File Uploader Method End.....................//

    //............................Exel export Method.....................//

    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.exportData1 = function () {
        var url = serviceBase + "api/Customers/export";
        $http.get(url).then(function (results) {
           
            $scope.storesitem = results.data;
            alasql('SELECT RetailerId,RetailersCode,ShopName,RetailerName,Mobile,Address,Area,Warehouse,ExecutiveName,Emailid,ClusterId,Day,latitute,longitute,BeatNumber,ExecutiveId,ClusterName,[Active],[Deleted] INTO XLSX("Customer.xlsx",{headers:true}) FROM ?', [$scope.storesitem]);
        }, function (error) {
        });
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("Customer.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };

    //............................Exel export Method.....................//

    $scope.currentPageStores = {};
    $scope.open = function () {
        console.log("Modal opened customer");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModalContent.html",
                controller: "ModalInstanceCtrl1", resolve: { customer: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.push(selectedItem);
            },
            function () {
                console.log("Cancel Condintion");
                $log.info("Modal dismissed at: " + new Date)
            })
    };

    $scope.customercategorys = {};
    $scope.opendelete = function (data, $index) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myCustomerModaldelete.html",
                controller: "ModalInstanceCtrldeleteCustomer", resolve: { customer: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.data.splice($index, 1);
                $scope.tableParams.reload();
            },
            function () {
            })
    };

    $scope.edit = function (item) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModalContentPut.html",
                controller: "ModalInstanceCtrl1", resolve: { customer: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {
                //$scope.customers.push(selectedItem);
                //_.find($scope.customers, function (customer) {
                //    if (customer.id == selectedItem.id) {
                //        customer = selectedItem;
                //    }
                //});
                //$scope.customers = _.sortBy($scope.customers, 'CustomerId').reverse();
                //$scope.selected = selectedItem;
            },
            function () {
            })
    };

    $scope.customers = [];
    //$http.get(serviceBase + 'api/Customers/InActive').success(function (results) {
    //    $scope.customers = results;
    //    $scope.data = $scope.customers;
    //    $scope.tableParams = new ngTableParams({
    //        page: 1,
    //        count: 50
    //    }, {
    //        total: $scope.data.length,
    //        getData: function ($defer, params) {
    //            var orderedData = params.sorting() ? $filter('orderBy')($scope.data, params.orderBy()) : $scope.data;
    //            orderedData = params.filter() ?
    //                    $filter('filter')(orderedData, params.filter()) :
    //                    orderedData;
    //            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //        }
    //    });
    //}, function (error) {
    //});

    $scope.allcusts = false;
    $scope.customers = [];
    $scope.getallcustomers = function () {
        customerService.getcustomers().then(function (results) {
            $scope.customers = results.data;
            $scope.data = $scope.customers;
            $scope.allcusts = true;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 50
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderedData = params.sorting() ? $filter('orderBy')($scope.data, params.orderBy()) : $scope.data;
                    orderedData = params.filter() ?
                            $filter('filter')(orderedData, params.filter()) :
                            orderedData;
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        }, function (error) {
        });

    }

    //search fn
    $scope.dataforsearch = { Cityid: "", mobile: "", datefrom: "", dateto: "", skcode: "" };
    $scope.Search = function (data) {
        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');

        $scope.dataforsearch.Cityid = data.Cityid;
        $scope.dataforsearch.mobile = data.mobile;
        $scope.dataforsearch.Cityid = data.Cityid;
        $scope.dataforsearch.skcode = data.skcode;
        if (!$('#dat').val()) {
            $scope.dataforsearch.datefrom = '';
            $scope.dataforsearch.dateto = '';
        }
        else {
            $scope.dataforsearch.datefrom = f.val();
            $scope.dataforsearch.dateto = g.val();
        }
        customerService.getfiltereddetails($scope.dataforsearch).then(function (results) {
            $scope.customers = results.data;
            if ($scope.customers.length > 0) {
            
            $scope.data = $scope.customers;
            alert($scope.customers.length);
            $scope.allcusts = true;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 100,
                ngTableParams: $scope.customers
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderedData = params.sorting() ? $filter('orderBy')($scope.data, params.orderBy()) : $scope.data;
                    orderedData = params.filter() ?
                            $filter('filter')(orderedData, params.filter()) :
                            orderedData;
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
            }
            else
            { alert("No customers found for search"); }
        });
    }
}]);

app.controller("ModalInstanceCtrl1", ["$scope", "CityService", "peoplesService", '$http', "$modalInstance", "customer", 'ngAuthSettings', 'WarehouseService', 'CustomerCategoryService', 'ClusterService', 'AreaService', function ($scope, CityService, peoplesService, $http, $modalInstance, customer, ngAuthSettings, WarehouseService, CustomerCategoryService, ClusterService, AreaService) {
    
    $scope.citydata = [];
    CityService.getcitys().then(function (results) {        
        $scope.citydata = results.data;
    }, function (error) {
    });

    $scope.getExecutive = [];
    peoplesService.getpeoples().then(function (results){
        $scope.getExecutive = results.data;
    })
    console.log($scope.getExecutive);

    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {        
        $scope.warehouse = results.data;
    }, function (error) {
    });

    CustomerCategoryService.getcustomercategorys().then(function (results) {
        $scope.customercategorys = results.data;       
    }, function (error) {
        //alert(error.data.message);
    });
    $scope.CustomerData = { Name: '',  Description: '' };
    if (customer) {
        $scope.CustomerData = customer;
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.Clusters = [];
    ClusterService.getcluster().then(function (results) {
        $scope.Clusters = results.data;
    }, function (error) {
    });

    $scope.Area = [];
    AreaService.getarea().then(function (results) {
        $scope.Area = results.data;
    }, function (error) {
    });

    $scope.examplemodel = [];
    $scope.exampledata = $scope.Area;
    $scope.examplesettings = {
        displayProp: 'AreaName', idProp: 'AreaName',
        scrollableHeight: '250px',
        scrollableWidth: '500px',
        enableSearch: true,
        scrollable: true
    };

   
    $scope.AddCustomer = function (data) {
        var area = [];
        _.each($scope.examplemodel, function (o2) {
            var Row = o2.id;
            area.push(Row);
        });
        var abs = $('#Customer-mobile').val().length;

        if (data.ShopName == null) {
            alert('Please Enter ShopName ');
        }
        else if (data.Warehouseid == null) {
            alert('Please Select Warehouse ');
        }
        else if (data.ClusterId == null) {
            alert('Please Select Cluster ');
        }
        else if (area.length == 0) {
            alert('Please Enter LandMark/Area ');
        }
        else if (data.Cityid == null) {
            alert('Please Select City ');
        }
        else if (data.BillingAddress == null) {
            alert('Please Enter BillingAddress ');
        }
        else if (data.Mobile == null) {
            
            alert('Please Enter Mobile Number ');
        }
        else if (abs < 10 || abs > 10) {
            alert('Please Enter Mobile Number in 10 digit ');
        }        
        else {
            var url = serviceBase + "api/customers";
            var dataToPost = {
                CustomerCategoryId: $scope.CustomerData.CustomerCategoryId,
                Name: $scope.CustomerData.Name,
                ShopName: $scope.CustomerData.ShopName,
                Skcode: $scope.CustomerData.Skcode,
                LandMark: area[0],
                ExecutiveId: $scope.CustomerData.ExecutiveId,
                Password: $scope.CustomerData.Password,
                Description: $scope.CustomerData.Description,
                CustomerType: $scope.CustomerData.CustomerType,
                CustomerCategoryName: $scope.CustomerData.CustomerCategoryName,
                BillingAddress: $scope.CustomerData.BillingAddress,
                ShippingAddress: $scope.CustomerData.ShippingAddress,
                Cityid: $scope.CustomerData.Cityid,
                Mobile: $scope.CustomerData.Mobile,
                Warehouseid: $scope.CustomerData.Warehouseid,
                BAGPSCoordinates: $scope.CustomerData.BAGPSCoordinates,
                SAGPSCoordinates: $scope.CustomerData.SAGPSCoordinates,
                RefNo: $scope.CustomerData.RefNo,
                OfficePhone: $scope.CustomerData.OfficePhone,
                Emailid: $scope.CustomerData.Emailid,
                Familymember: $scope.CustomerData.Familymember,
                CreatedDate: $scope.CustomerData.CreatedDate,
                UpdatedDate: $scope.CustomerData.UpdatedDate,
                CreatedBy: $scope.CustomerData.CreatedBy,
                LastModifiedBy: $scope.CustomerData.LastModifiedBy,
                Active: $scope.CustomerData.Active,
                ClusterId: $scope.CustomerData.ClusterId,
                SizeOfShop: $scope.CustomerData.SizeOfShop,
                lat: $scope.CustomerData.lat,
                lg: $scope.CustomerData.lg,
                MonthlyTurnOver: $scope.CustomerData.MonthlyTurnOver
            };

            $http.post(url, dataToPost)
            .success(function (data) {
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                    $modalInstance.close(data);
                }
            })
             .error(function (data) {
             })
        }
     };

    $scope.PutCustomer = function (data) {
        
        var area = [];
        _.each($scope.examplemodel, function (o2) {
            var Row = o2.id;
            area.push(Row);
        });
        $scope.CustomerData = { };        
        if (customer) {
            $scope.CustomerData = customer;           
        }
        var abs = $('#Customer-mobile').val().length;

        if (data.ShopName == null) {
            alert('Please Enter ShopName ');
        }
        else if (data.Warehouseid == null) {
            alert('Please Select Warehouse ');
        }
        else if (data.ClusterId == null) {
            alert('Please Select Cluster ');
        }
        else if (area.length == 0 && data.LandMark == null && data.LandMark == "null" && data.LandMark == "") {
            alert('Please Enter LandMark/Area ');
        }
        else if (data.Cityid == null) {
            alert('Please Select City ');
        }
        else if (data.BillingAddress == null) {
            alert('Please Enter BillingAddress ');
        }
        else if (data.Mobile == null) {

            alert('Please Enter Mobile Number ');
        }
        else if (abs < 10 || abs > 10) {

            alert('Please Enter Mobile Number in 10 digit ');
        }
        else {
            if (area.length == 0) {
                area[0] = data.LandMark;
            }
            var url = serviceBase + "api/customers";
            var dataToPost = {
                CustomerId: $scope.CustomerData.CustomerId,
                CustomerCategoryId: $scope.CustomerData.CustomerCategoryId,
                Name: $scope.CustomerData.Name,
                Password: $scope.CustomerData.Password,
                LandMark: area[0],
                Description: $scope.CustomerData.Description,
                CustomerType: $scope.CustomerData.CustomerType, ShopName: $scope.CustomerData.ShopName,
                CustomerCategoryName: $scope.CustomerData.CustomerCategoryName,
                BillingAddress: $scope.CustomerData.BillingAddress,
                ShippingAddress: $scope.CustomerData.ShippingAddress,
                City: $scope.CustomerData.City,
                Cityid: $scope.CustomerData.Cityid,
                BAGPSCoordinates: $scope.CustomerData.BAGPSCoordinates,
                SAGPSCoordinates: $scope.CustomerData.SAGPSCoordinates,
                RefNo: $scope.CustomerData.RefNo,
                Mobile: $scope.CustomerData.Mobile,
                Warehouseid: $scope.CustomerData.Warehouseid,
                OfficePhone: $scope.CustomerData.OfficePhone,
                Emailid: $scope.CustomerData.Emailid,
                Familymember: $scope.CustomerData.Familymember,
                CreatedDate: $scope.CustomerData.CreatedDate,
                UpdatedDate: $scope.CustomerData.UpdatedDate,
                CreatedBy: $scope.CustomerData.CreatedBy,
                LastModifiedBy: $scope.CustomerData.LastModifiedBy,
                Active: $scope.CustomerData.Active,
                ExecutiveId: $scope.CustomerData.ExecutiveId,
                Skcode: $scope.CustomerData.Skcode,
                lat: $scope.CustomerData.lat,
                lg: $scope.CustomerData.lg,
                ClusterId: $scope.CustomerData.ClusterId
            };

            $http.put(url, dataToPost)
            .success(function (data) {
                if (data.id == 0) {
                    $scope.gotErrors = true;
                    if (data[0].exception == "Already") {
                        $scope.AlreadyExist = true;
                    }
                }
                else {
                    $modalInstance.close(data);
                }
            })
             .error(function (data) {
             })
        }
    };
}])

app.controller("ModalInstanceCtrldeleteCustomer", ["$scope", '$http', "$modalInstance", "customerService", 'ngAuthSettings', "customer", function ($scope, $http, $modalInstance, customerService, ngAuthSettings, customer) {
    function ReloadPage() {
        location.reload();
    };
    $scope.CustomerData = { };
    if (customer) {
        $scope.CustomerData = customer;
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },
    $scope.deletecustomers = function (dataToPost, index) {
        customerService.deletecustomers(dataToPost).then(function (results) {
            $modalInstance.close(dataToPost);
        }, function (error) {
            alert(error.data.message);
        });
    }
}])