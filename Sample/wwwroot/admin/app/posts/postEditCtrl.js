(function () {
    "use strict";
    angular.module("blogifire").controller("postEditCtrl", ["post", "$window", postEditCtrl]);

    function postEditCtrl(post, $window) {
        var vm = this;

        vm.post = post;
        vm.postCopy = angular.copy(vm.post);
        $('.summernote').code(vm.post.Content);

        this.save = function () {
            vm.post.Content = $('.summernote').code();
            vm.post.$save({}, function (data, headers) {
                toastr.success('saved');
            }, function (data, headers) {
                toastr.error('failed');
            });
        }

        this.publish = function () {
            vm.post.Content = $('.summernote').code();
            vm.post.Published = true;
            this.save();
        }

        this.cancel = function () {
            vm.post = vm.postCopy;
            $window.history.back();
        }
    }
}());