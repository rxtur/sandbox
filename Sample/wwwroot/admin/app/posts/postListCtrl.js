(function () {
    "use strict";
    angular.module("blogifire").controller("postListCtrl", ["postResource", postListCtrl]);
    function postListCtrl(postResource) {
        var vm = this;
        postResource.query(function (data) {
            vm.posts = data;
        });
    }
}());
