﻿'use strict';
app.factory('NewsService', function ($http, ngAuthSettings) {
    
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    
    var NewsServiceFactory = {};

    var _getNews = function () {
        return $http.get(serviceBase + 'api/NewsApi').then(function (results) {


            return results;
        });
    };
    NewsServiceFactory.geNews = _getNews;

    var _deleteNews = function (data) {
        return $http.delete(serviceBase + 'api/NewsApi/?id=' + data.NewsId).then(function (results) {
            return results;
        });
    };

    NewsServiceFactory.deleteNews = _deleteNews;
    NewsServiceFactory.getNews = _getNews;

    return NewsServiceFactory;

});
