(function () {
    "use strict";
    angular.module("blogifire").controller("postListCtrl",
        ["$window", "postResource", "dataService", postListCtrl]);

    function postListCtrl($window, postResource, dataService) {
        var vm = this;

        dataService.getItems('/blog/api/author')
        .success(function (usr) {
            if (!usr || !usr.IsAuthenticated) {
                $window.location.href = '/account/login';
            }
        })
        .error(function () {
            toastr.error("error");
        });

        postResource.query(function (data) {
            vm.posts = data;
        });
    }
}());
