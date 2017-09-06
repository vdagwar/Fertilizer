﻿'use strict';
app.factory('editPriceService', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var editPriceServiceFactory = {};



    var _Saveediteditem = function (data) {
        var url = serviceBase + 'api/editPrice';
        return $http.put(url,data).then(function (results) {
            return results;
        })
        }

    editPriceServiceFactory.Saveediteditem = _Saveediteditem;

    return editPriceServiceFactory;
});