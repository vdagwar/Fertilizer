'use strict'
app.controller('RewardItemCtrl', function ($scope, $http, $modal) {
    $scope.rewardItems = [];
    $http.get(serviceBase + "api/RewardItem/GetAll").success(function (data) {
        if (data.length == 0)
            alert("Data not Present");
        else
            $scope.rewardItems = data;
    });

    $scope.open = function () {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myADDModal.html",
                controller: "RIADDController", resolve: { object: function () { return $scope.item } }
            }), modalInstance.result.then(function (selectedNews) {
                $scope.rewardItems.push(selectedNews);
            },
            function () {
            })
    };

    $scope.edit = function (rItem) {
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myADDModal.html",
                controller: "RIADDController", resolve: { object: function () { return rItem } }
            }), modalInstance.result.then(function (selectedNews) {
                $scope.rewardItemsDb.push(selectedNews);
                _.find($scope.rewardItemsDb, function (rewardItems) {
                    if (rewardItems.id == selectedNews.id) {
                        rewardItems = selectedNews;
                    }
                });
                $scope.rewardItemsDb = _.sortBy($scope.rewardItemsDb, 'Id').reverse();
                $scope.selected = selectedNews;
            },
            function () {
            })
    };

    $scope.opendelete = function (data, $index) {
        console.log(data);
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "mydeletemodal.html",
                controller: "RIdeleteController", resolve: { object: function () { return data } }
            }), modalInstance.result.then(function (selectedComplain) {
                if (selectedComplain == null) {

                } else { $scope.currentPageStores.splice($index, 1); }
            },
            function () {
            })
    };

});

app.controller("RIADDController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", "FileUploader", function ($scope, $http, ngAuthSettings, $modalInstance, object,FileUploader) {
    var input = document.getElementById("file");
    var today = new Date();
    $scope.today = today.toISOString();

    $scope.saveData = {};
    if (object) {
        $scope.saveData = object;
    }

    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });

    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var fileuploadurl = '/api/RIUpload/post', files;
                $upload.upload({
                    url: fileuploadurl,
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: file
                }).progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                }).success(function (data, status, headers, config) {
                });
            }
        }
    }; 

    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); },

    $scope.Add = function (data) {
         var LogoUrl = $scope.uploadedfileName;
         $scope.saveData.ImageUrl = serviceBase + "../../images/RewardItem/" + LogoUrl;

         var url = serviceBase + "api/RewardItem";
         var dataToPost = {
             rName: $scope.saveData.rName,
             rPoint: $scope.saveData.rPoint,
             rItem: $scope.saveData.rItem,
             Description: $scope.saveData.Description,
             ImageUrl: $scope.saveData.ImageUrl,
             IsActive: $scope.saveData.IsActive
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
     };

    $scope.Put = function (object) {
        $scope.saveData = {};
        if (object) {
            $scope.saveData = object;
        }
        $scope.logourl = object.ImageUrl;
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('cancel'); }

        if ($scope.uploadedfileName == null || $scope.uploadedfileName == '') {
            $scope.saveData.ImageUrl = $scope.logourl;
        }
        else {
            var LogoUrl = $scope.uploadedfileName;
            $scope.saveData.ImageUrl = serviceBase + "../../images/RewardItem/" + LogoUrl;
        }
        var dataToPost = {
            rItemId: $scope.saveData.rItemId,
            rName: $scope.saveData.rName,
            rPoint: $scope.saveData.rPoint,
            rItem: $scope.saveData.rItem,
            Description: $scope.saveData.Description,
            ImageUrl: $scope.saveData.ImageUrl,
            IsActive: $scope.saveData.IsActive

        };
        var url = serviceBase + "api/RewardItem";
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

    };
    /////////////////////////////////////////////////////// angular upload cod
    var uploader = $scope.uploader = new FileUploader({
        url: 'api/RIUpload'
    });
    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });          
    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
    };
    uploader.onAfterAddingFile = function (fileItem) {
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
    };
    uploader.onBeforeUploadItem = function (item) {
    };
    uploader.onProgressItem = function (fileItem, progress) {
    };
    uploader.onProgressAll = function (progress) {
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        alert("Image Upload failed");
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        $scope.uploadedfileName = fileItem._file.name;
        alert("Image Uploaded Successfully");
    };
    uploader.onCompleteAll = function () {
    };        
}])