(function () {
    "use strict";
    angular.module("blogifire").controller("CommentListCtrl", ["commentResource", CommentListCtrl]);

    function CommentListCtrl(commentResource) {
        var vm = this;

        commentResource.query(function (data) {
            vm.comments = data;
        });

        vm.Title = "Comments";
    }
}());