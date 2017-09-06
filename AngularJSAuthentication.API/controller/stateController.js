﻿'use strict';
app.controller('stateController', ['$scope', 'StateService', "$filter", "$http", "ngTableParams", '$modal', function ($scope, StateService, $filter, $http, ngTableParams, $modal) {

    console.log(" State Controller reached");

    $scope.currentPageStores = {};

    $scope.open = function () {
        console.log("Modal opened State");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myStateModal.html",
                controller: "ModalInstanceCtrlState", resolve: { state: function () { return $scope.items } }
            }), modalInstance.result.then(function (selectedItem) {


                $scope.currentPageStores.push(selectedItem);

            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };


    $scope.edit = function (item) {
        console.log("Edit Dialog called ");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myStateModalPut.html",
                controller: "ModalInstanceCtrlState", resolve: { state: function () { return item } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.states.push(selectedItem);
                _.find($scope.states, function (state) {
                    if (state.id == selectedItem.id) {
                        state = selectedItem;
                    }
                });

                $scope.states = _.sortBy($scope.state, 'Id').reverse();
                $scope.selected = selectedItem;
            
            },
            function () {
                console.log("Cancel Condintion");
              
            })
    };

    $scope.opendelete = function (data,$index) {
        console.log(data);
        console.log("Delete Dialog called for state");



        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myModaldeleteState.html",
                controller: "ModalInstanceCtrldeleteState", resolve: { state: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) {
                $scope.currentPageStores.splice($index, 1);
            },
            function () {
                console.log("Cancel Condintion");
           
            })
    };

    $scope.states = [];

   StateService.getstates().then(function (results) {

       $scope.states = results.data;


        $scope.callmethod();
    }, function (error) {
       
    });

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.states,

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

app.controller("ModalInstanceCtrlState", ["$scope", '$http', 'ngAuthSettings', "StateService", "$modalInstance", "state", 'FileUploader', function ($scope, $http, ngAuthSettings, StateService, $modalInstance, state, FileUploader) {
    console.log("state");

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





    $scope.StateData = {

    };
    if (state) {
        console.log("state if conditon");

        $scope.StateData = state;

        console.log($scope.StateData.StateName);
       
    }


    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

     $scope.AddState = function (data) {


         console.log("state");

         //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
         //console.log(FileUrl);
         //console.log("Image name in Insert function :" + $scope.uploadedfileName);
         //$scope.AssetsCategoryData.FileUrl = FileUrl;
         //console.log($scope.AssetsCategoryData.FileUrl);




         var url = serviceBase + "api/States";
         var dataToPost = {
             StateName: $scope.StateData.StateName,
             AliasName: $scope.StateData.AliasName,
             CreatedDate: $scope.StateData.CreatedDate,
             UpdatedDate: $scope.StateData.UpdatedDate,
             CreatedBy: $scope.StateData.CreatedBy,
             UpdateBy: $scope.StateData.UpdateBy
         };
         console.log(dataToPost);

         $http.post(url, dataToPost)
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



    $scope.PutState = function (data) {
      
        $scope.StateData = {

        };
        if (state) {
            $scope.StateData = state;
            console.log("found Puttt state");
            console.log(state);
            console.log($scope.StateData);
         
        }
        $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

        console.log("Update ");

        //var FileUrl = serviceBase + "../../UploadedFiles/" + $scope.uploadedfileName;
        //console.log(FileUrl);
        //console.log("Image name in Insert function :" + $scope.uploadedfileName);
        //$scope.AssetsCategoryData.FileUrl = FileUrl;
        //console.log($scope.AssetsCategoryData.FileUrl);

        var url = serviceBase + "api/States";
        var dataToPost = {
            Stateid: $scope.StateData.Stateid,
            StateName: $scope.StateData.StateName, AliasName: $scope.StateData.AliasName, CreatedDate: $scope.StateData.CreatedDate, UpdatedDate: $scope.StateData.UpdatedDate, CreatedBy: $scope.StateData.CreatedBy, UpdateBy: $scope.StateData.UpdateBy
        };

        //var dataToPost = { SurveyId: $scope.SurveyData.SurveyId, SurveyCategoryName: $scope.SurveyData.SurveyCategoryName, Discription: $scope.SurveyData.Discription, CreatedDate: $scope.SurveyData.CreatedDate, UpdatedDate: $scope.SurveyData.UpdatedDate, CreatedBy: $scope.SurveyData.CreatedBy, UpdateBy: $scope.SurveyData.UpdateBy };
        console.log(dataToPost);


        $http.put(url, dataToPost)
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

             // return $scope.showInfoOnSubmit = !0, $scope.revert()
         })

    };


    /////////////////////////////////////////////////////// angular upload code


    var uploader = $scope.uploader = new FileUploader({
        url: serviceBase + 'api/upload'
    });

    //FILTERS

    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });

    //CALLBACKS

    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
        console.info('onWhenAddingFileFailed', item, filter, options);
    };
    uploader.onAfterAddingFile = function (fileItem) {
        console.info('onAfterAddingFile', fileItem);
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
        console.info('onAfterAddingAll', addedFileItems);
    };
    uploader.onBeforeUploadItem = function (item) {
        console.info('onBeforeUploadItem', item);
    };
    uploader.onProgressItem = function (fileItem, progress) {
        console.info('onProgressItem', fileItem, progress);
    };
    uploader.onProgressAll = function (progress) {
        console.info('onProgressAll', progress);
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        console.info('onSuccessItem', fileItem, response, status, headers);
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        console.info('onErrorItem', fileItem, response, status, headers);
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
        console.info('onCancelItem', fileItem, response, status, headers);
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        console.info('onCompleteItem', fileItem, response, status, headers);
        console.log("File Name :" + fileItem._file.name);
        $scope.uploadedfileName = fileItem._file.name;
    };
    uploader.onCompleteAll = function () {
        console.info('onCompleteAll');
    };

    console.info('uploader', uploader);


}])

app.controller("ModalInstanceCtrldeleteState", ["$scope", '$http', "$modalInstance", "StateService", 'ngAuthSettings', "state", function ($scope, $http, $modalInstance, StateService, ngAuthSettings, state) {
    console.log("delete modal opened");

    

    $scope.states = [];

    if (state) {
        $scope.StateData = state;
        console.log("found state");
        console.log(state);
        console.log($scope.StateData);
      
    }
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


    $scope.deletestate = function (dataToPost, $index) {

        console.log("Delete  state controller");
      

        StateService.deletestate(dataToPost).then(function (results) {
            console.log("Del");

            console.log("index of item " + $index);
            console.log($scope.states.length);
            console.log($scope.states.length);
          
            $modalInstance.close(dataToPost);
           

        }, function (error) {
            alert(error.data.message);
        });
    }

}])