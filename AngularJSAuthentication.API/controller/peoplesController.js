'use strict';
app.controller('peoplesController', ['$scope', 'peoplesService', 'CityService', 'StateService', "$filter", "$http", "ngTableParams", '$modal', 'WarehouseService',
    function ($scope, peoplesService, CityService, StateService, $filter, $http, ngTableParams, $modal, WarehouseService) {

        console.log("People Controller reached");
        //.................File Uploader method start..................    
        $(function () {
            $('input[name="daterange"]').daterangepicker({
                timePicker: true,
                timePickerIncrement: 5,
                timePicker12Hour: true,

                //locale: {
                format: 'MM/DD/YYYY h:mm A'
                //}
            });
        });

        $scope.citys = [];
        CityService.getcitys().then(function (results) {

            $scope.citys = results.data;
        }, function (error) {
        });

        $scope.warehouse = [];
        WarehouseService.getwarehouse().then(function (results) {
            $scope.warehouse = results.data;

        }, function (error) {
        });

        $scope.uploadshow = true;
        $scope.toggle = function () {
            $scope.uploadshow = !$scope.uploadshow;
        }

        function sendFileToServer(formData, status) {
            var uploadURL = "/api/peopleupload/post"; //Upload URL
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

        $scope.currentPageStores = {};

        $scope.open = function () {
            console.log("Modal opened people");
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "myPeopleModal.html",
                    controller: "ModalInstanceCtrlPeople", resolve: { people: function () { return $scope.items } }
                }), modalInstance.result.then(function (selectedItem) {
                },
                function () {
                    console.log("Cancel Condintion");
                })
        };

        $scope.edit = function (item) {
            console.log("Edit Dialog called people");
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "myPeopleModalPut.html",
                    controller: "ModalInstanceCtrlPeople", resolve: { people: function () { return item } }
                }), modalInstance.result.then(function (selectedItem) {

                    $scope.peoples.push(selectedItem);
                    _.find($scope.peoples, function (people) {
                        if (people.id == selectedItem.id) {
                            people = selectedItem;
                        }
                    });
                    $scope.peoples = _.sortBy($scope.peoples, 'Id').reverse();
                    $scope.selected = selectedItem;
                },
                function () {
                    console.log("Cancel Condintion");
                })
        };

        $scope.opendelete = function (data, $index) {
            console.log(data);
            console.log("Delete Dialog called for people");
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "myModaldeletepeople.html",
                    controller: "ModalInstanceCtrldeletepeople", resolve: { people: function () { return data } }
                }), modalInstance.result.then(function (selectedItem) {
                    $scope.currentPageStores.splice($index, 1);
                },
                function () {
                    console.log("Cancel Condintion");
                })
        };

        $scope.peoples = [];
        peoplesService.getpeoples().then(function (results) {

            $scope.peoples = results.data;
            console.log("Got people collection");
            console.log($scope.peoples);
            $scope.callmethod();
        }, function (error) {
        });

        $scope.warehouse = [];
        WarehouseService.getwarehouse().then(function (results) {

            $scope.warehouse = results.data;
        }, function (error) {
        });

        $scope.callmethod = function () {

            var init;
            return $scope.stores = $scope.peoples,

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
    }]);

app.controller("ModalInstanceCtrlPeople", ["$scope", '$http','$modal', 'ngAuthSettings', "peoplesService", 'CityService', 'StateService', "$modalInstance", "people", 'WarehouseService', 'authService',
    function ($scope, $http,$modal, ngAuthSettings, peoplesService, CityService, StateService, $modalInstance, people, WarehouseService, authService) {
        console.log("People");
        $scope.PeopleData = {}; var alrt = {};
        if (people) {
            $scope.PeopleData = people;
        }

        $scope.citys = [];
        CityService.getcitys().then(function (results) {
            $scope.citys = results.data;
        }, function (error) {
        });

        $scope.states = [];
        StateService.getstates().then(function (results) {
            $scope.states = results.data;
        }, function (error) {
        });

        $scope.warehouse = [];
        WarehouseService.getwarehouse().then(function (results) {
            $scope.warehouse = results.data;
        }, function (error) {
        });
       
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        $scope.AddPeople = function () {
                authService.saveRegistrationpeople($scope.PeopleData).then(function (response) {
                    console.log(response);
                    $modalInstance.close(response);
                    if (response.status == "ok") {
                        alrt.msg = "Record has been Save successfully";
                        $modal.open(
                                        {
                                            templateUrl: "PopUpModel.html",
                                            controller: "PopUpController", resolve: { message: function () { return alrt } }
                                        }), modalInstance.result.then(function (selectedItem) {
                                        },
                                    function () {
                                        console.log("Cancel Condintion");
                                    })
                    }
                });
        };
        $scope.PutPeople = function (data) {
            $scope.PeopleData = {};
            if (people) {
                $scope.PeopleData = people;
                console.log("found Puttt People");
                console.log(people);
                console.log($scope.PeopleData);
                $scope.ok = function () { $modalInstance.close(); },
                $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

                console.log("Update People");
                var url = serviceBase + "/api/Peoples";
                var dataToPost = {
                    PeopleID: $scope.PeopleData.PeopleID, Password: $scope.PeopleData.Password,
                    Active: $scope.PeopleData.Active,
                    Stateid: $scope.PeopleData.Stateid, Cityid: $scope.PeopleData.Cityid, Mobile: $scope.PeopleData.Mobile,
                    PeopleFirstName: $scope.PeopleData.PeopleFirstName,
                    PeopleLastName: $scope.PeopleData.PeopleLastName,
                    Warehouseid: $scope.PeopleData.Warehouseid,
                    CreatedDate: $scope.PeopleData.CreatedDate,
                    UpdatedDate: $scope.PeopleData.UpdatedDate,
                    CreatedBy: $scope.PeopleData.CreatedBy,
                    UpdateBy: $scope.PeopleData.UpdateBy,
                    Email: $scope.PeopleData.Email,
                    Department: $scope.PeopleData.Department,
                    BillableRate: $scope.PeopleData.BillableRate,
                    CostRate: $scope.PeopleData.CostRate,
                    Permissions: $scope.PeopleData.Permissions,
                    Skcode: $scope.PeopleData.Skcode,
                    Type: $scope.PeopleData.Department,
                    ImageUrl: data.ImageUrl
                };
                console.log(dataToPost);               
                $http.put(url, dataToPost)
                .success(function (data) {
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
                        $modalInstance.close(data);
                        alrt.msg = "Record has been Update successfully";
                        $modal.open(
                                        {
                                            templateUrl: "PopUpModel.html",
                                            controller: "PopUpController", resolve: { message: function () { return alrt } }
                                        }), modalInstance.result.then(function (selectedItem) {
                                        },
                                    function () {
                                        console.log("Cancel Condintion");
                                    })
                    }
                })
                 .error(function (data) {
                     console.log("Error Got Heere is ");
                     console.log(data);
                 })
            };
        }
    }])

app.controller("ModalInstanceCtrldeletepeople", ["$scope", '$http', '$modal', "$modalInstance", "peoplesService", 'ngAuthSettings', "people", function ($scope, $http, $modal, $modalInstance, peoplesService, ngAuthSettings, people) {
    console.log("delete modal opened");
    var alrt = {};
    if (people) {
        $scope.PeopleData = people;
        console.log("found people");
        console.log(people);
        console.log($scope.PeopleData);

    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deletepeoples = function (dataToPost) {

        console.log("Delete people controller");


        peoplesService.deletepeoples(dataToPost).then(function (results) {
            console.log("Del");
            $modalInstance.close(dataToPost);
            alrt.msg = "Entry Deleted";
            $modal.open(
                            {
                                templateUrl: "PopUpModel.html",
                                controller: "PopUpController", resolve: { message: function () { return alrt } }
                            }), modalInstance.result.then(function (selectedItem) {
                            },
                        function () {
                            console.log("Cancel Condintion");
                        })
        }, function (error) {
            alert(error.data.message);
        });
    }
}])