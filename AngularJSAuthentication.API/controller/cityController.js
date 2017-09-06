﻿'use strict';
app.controller('cityController', ['$scope', "CityService", 'StateService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, CityService, StateService, $filter, $http, ngTableParams, $modal) {

    console.log(" city Controller reached");

    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened city");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myCityModal.html",
                controller: "ModalInstanceCtrlCity", resolve: { city: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {


                $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion");
             
            })
    };


    $scope.edit = function (item) {
        console.log("Edit Dialog called city");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myCityModalPut.html",
                controller: "ModalInstanceCtrlCity", resolve: { city: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.city.push(selectedItem);
                _.find($scope.city, function (city) {
                    if (city.id == selectedItem.id) {
                        city = selectedItem;
                    }
                });

                $scope.city = _.sortBy($scope.city, 'Id').reverse();
                $scope.selected = selectedItem;
         
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

    $scope.city = [];

    CityService.getcitys().then(function (results) {
        console.log("ingetfn");
        console.log(results.data);
        $scope.city = results.data;
      
        $scope.callmethod();
    }, function (error) {
      
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.city,

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

            $scope.numPerPageOpt = [3, 5, 10, 20],
            $scope.numPerPage = $scope.numPerPageOpt[2],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })

        ()


    }
}]);

app.controller("ModalInstanceCtrlCity", ["$scope", '$http', 'ngAuthSettings', "CityService", "StateService", "$modalInstance", "city", 'FileUploader', function ($scope, $http, ngAuthSettings, CityService, StateService, $modalInstance, city, FileUploader) {
    console.log("city");

    var input = document.getElementById("file");
    console.log(input);

    var today = new Date();
    $scope.today = today.toISOString();

    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });

    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        console.log(files);
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                console.log(config.file.name);

                console.log("File Name is " + $scope.uploadedfileName);
                var fileuploadurl = '/api/upload/post', files;
                $upload.upload({
                    url: fileuploadurl,
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: file
                }).progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    console.log('progress: ' + progressPercentage + '% ' +
                                evt.config.file.name);
                }).success(function (data, status, headers, config) {


                    console.log('file ' + config.file.name + 'uploaded. Response: ' +
                                JSON.stringify(data));
                    cosole.log("uploaded");
                });
            }
        }
    };





    $scope.CityData = {

    };

    $scope.states = [];
    StateService.getstates().then(function (results) {
        console.log("sumit");
        console.log(results.data);
        $scope.states = results.data;
    }, function (error) {
    });

    if (city) {
        console.log("city if conditon");

        $scope.CityData = city;
        console.log("kkkkkk");
        console.log($scope.CityData.Stateid);
        console.log($scope.CityData.StateName);
       
    }


    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddCity = function (data) {


         console.log("AddCity");

         var url = serviceBase + "api/City";
         var dataToPost = {
             Cityid: $scope.CityData.Cityid, CityName: $scope.CityData.CityName, aliasName: $scope.CityData.aliasName, CreatedDate: $scope.CityData.CreatedDate, UpdatedDate: $scope.CityData.UpdatedDate, CreatedBy: $scope.CityData.CreatedBy, UpdateBy: $scope.CityData.UpdateBy, Stateid: $scope.CityData.Stateid,
             Code:$scope.CityData.Code 
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
                 //console.log(data);
                 //  console.log(data);
                 $modalInstance.close(data);
             }

         })
          .error(function (data) {
              console.log("Error Got Heere is ");
              console.log(data);
              // return $scope.showInfoOnSubmit = !0, $scope.revert()
          })
     };



    $scope.PutCity = function (data) {
        $scope.CityData = {

        };
        if (city) {
            $scope.CityData = city;
            console.log("found Puttt City");
            console.log(city);
            console.log($scope.CityData);
            console.log("selected City");
           
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("PutCity");

        

        var url = serviceBase + "api/City";
        var dataToPost = { Cityid: $scope.CityData.Cityid, CityName: $scope.CityData.CityName, aliasName: $scope.CityData.aliasName, CreatedDate: $scope.CityData.CreatedDate, UpdatedDate: $scope.CityData.UpdatedDate, CreatedBy: $scope.CityData.CreatedBy, UpdateBy: $scope.CityData.UpdateBy, Stateid: $scope.CityData.Stateid, Code: $scope.CityData.Code };


        console.log(dataToPost);


        $http.put(url, dataToPost)
        .success(function (data) {
            console.log("PutCity");
           
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
                console.log("save data");
            }

        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);

         })

    };


    /////////////////////////////////////////////////////// angular upload code


    //var uploader = $scope.uploader = new FileUploader({
    //    url: serviceBase + 'api/upload'
    //});

    ////FILTERS

    //uploader.filters.push({
    //    name: 'customFilter',
    //    fn: function (item /*{File|FileLikeObject}*/, options) {
    //        return this.queue.length < 10;
    //    }
    //});

    ////CALLBACKS

    //uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
    //    console.info('onWhenAddingFileFailed', item, filter, options);
    //};
    //uploader.onAfterAddingFile = function (fileItem) {
    //    console.info('onAfterAddingFile', fileItem);
    //};
    //uploader.onAfterAddingAll = function (addedFileItems) {
    //    console.info('onAfterAddingAll', addedFileItems);
    //};
    //uploader.onBeforeUploadItem = function (item) {
    //    console.info('onBeforeUploadItem', item);
    //};
    //uploader.onProgressItem = function (fileItem, progress) {
    //    console.info('onProgressItem', fileItem, progress);
    //};
    //uploader.onProgressAll = function (progress) {
    //    console.info('onProgressAll', progress);
    //};
    //uploader.onSuccessItem = function (fileItem, response, status, headers) {
    //    console.info('onSuccessItem', fileItem, response, status, headers);
    //};
    //uploader.onErrorItem = function (fileItem, response, status, headers) {
    //    console.info('onErrorItem', fileItem, response, status, headers);
    //};
    //uploader.onCancelItem = function (fileItem, response, status, headers) {
    //    console.info('onCancelItem', fileItem, response, status, headers);
    //};
    //uploader.onCompleteItem = function (fileItem, response, status, headers) {
    //    console.info('onCompleteItem', fileItem, response, status, headers);
    //    console.log("File Name :" + fileItem._file.name);
    //    $scope.uploadedfileName = fileItem._file.name;
    //};
    //uploader.onCompleteAll = function () {
    //    console.info('onCompleteAll');
    //};

    //console.info('uploader', uploader);


}])

app.controller("ModalInstanceCtrldeleteCity", ["$scope", '$http', "$modalInstance", "CityService", 'ngAuthSettings', "city", function ($scope, $http, $modalInstance, CityService, ngAuthSettings, myData) {
    console.log("delete modal opened");

   
    $scope.city = [];


    if (myData) {
        $scope.CityData = myData.city1;
       
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deleteCity = function (dataToPost, $index) {
        console.log("Delete  controller");
    
        CityService.deletecitys(dataToPost).then(function (results) {
            console.log("Del"); 
            //myData.all.splice($index, 1);

            $modalInstance.close(dataToPost);
           // ReloadPage();

        }, function (error) {
            alert(error.data.message);
        });
    }

}])