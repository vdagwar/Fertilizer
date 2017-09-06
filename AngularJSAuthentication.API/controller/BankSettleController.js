app.controller('BankSettleController', ['$scope', '$http', '$timeout', "$location", "$modal",  "localStorageService", "FileUploader",
function ($scope, $http, $timeout, $location, $modal, localStorageService, FileUploader) {
    console.log(" BankSettleController reached");
    var input = document.getElementById("file");
    $scope.BankStockCurrencys = [];
    $scope.BanksettleAmount = function () {
     
        $http.get(serviceBase + 'api/CurrencyStock/BankSettleAmount').then(function (results) {
          
            $scope.BankStockCurrencys = results.data;
            console.log(" $scope.BankStockCurrencys ", $scope.BankStockCurrencys);
            //$scope.callmethod();
        });
    }
    $scope.BanksettleAmount();
    $scope.$watch('files', function () {
        
        $scope.upload($scope.files);
    });
    $scope.uploadedfileName = '';
    $scope.upload = function (files) {
        
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var fileuploadurl = '/api/logoUpload/post', files;
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
  
    $scope.testk = "";
    $scope.view = function (CurrencyBankSettleId) {
       
        $http.get(serviceBase + 'api/CurrencyStock/BankSettleAmountGet?id='+CurrencyBankSettleId).then(function (results) {
            $scope.BankStock = results.data;
            $scope.testk = $scope.BankStock[0].DepositedBankSlip;
            alert($scope.testk);
            console.log(" $scope.BankStock ", $scope.BankStock);
            //$scope.callmethod();
        });
    }
 
    /////////////////////////////////////////////////////// angular upload cod

    var uploader = $scope.uploader = new FileUploader({
        url: 'api/logoUpload',

    });
    //FILTERS

    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 1;
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
    $scope.Add = function (data) {
       
        console.log("data", data);

        var LogoUrl = $scope.uploadedfileName;
        $scope.Image = serviceBase + "../../UploadedLogos/" + LogoUrl;


        var dataToPost = {
            CurrencyBankSettleId: data.CurrencyBankSettleId,
            DepositedBankSlip: $scope.Image,
        };

        console.log("dataToPost", dataToPost);
        var url = serviceBase + 'api/CurrencyStock/BankSettleAmountPut';
        $http.put(url, dataToPost)
        .success(function (data) {
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    $scope.AlreadyExist = true;
                }
            }
            else {

                // $modalInstance.close(data);
            }
        })
         .error(function (data) {

         })
    }

}]);