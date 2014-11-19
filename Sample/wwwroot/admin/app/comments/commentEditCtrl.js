(function () {
    "use strict";
    angular.module("blogifire").controller("CommentEditCtrl", ["comment", "$state", CommentEditCtrl]);

    function CommentEditCtrl(comment, $state) {
        var vm = this;

        vm.Title = "Edit comment";
        vm.comment = comment;

        if (vm.comment && vm.comment.Id) {
            vm.Title = "Edit: " + vm.comment.Title;
        }
        else {
            vm.Title = "New comment";
        }

        $('.summernote').code(comment.Content);

    }
}());