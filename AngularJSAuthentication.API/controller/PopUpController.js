'use strict';
app.controller("PopUpController", ["$scope", "$modalInstance", 'message', function ($scope, $modalInstance, message) {
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); };

    if (message) {
        $scope.alrt = message;
    }
}])