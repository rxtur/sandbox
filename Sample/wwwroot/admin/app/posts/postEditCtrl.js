(function () {
    "use strict";
    angular.module("blogifire").controller("postEditCtrl", ["post", "$state", "$window", postEditCtrl]);

    function postEditCtrl(post, $state, $window) {
        var vm = this;

        vm.post = post;
        vm.postCopy = angular.copy(vm.post);
        $('.summernote').code(vm.post.Content);

        this.save = function () {
            vm.post.Content = $('.summernote').code();
            vm.post.Saved = new Date();
            vm.post.Slug = this.convertToSlug(vm.post.Title);

            if (!vm.post.BlogId) {
                vm.post.BlogId = 0;
            }         
            
            vm.post.$save(function (data) {
                toastr.success('saved');
            }, function (data) {
                toastr.error('failed');
            });
        }

        this.publish = function () {
            vm.post.Published = new Date();
            this.save();
        }

        this.cancel = function () {
            vm.post = vm.postCopy;
            $window.history.back();
        }

        this.convertToSlug = function (title) {
            return title.toLowerCase()
                .replace(/[^\w ]+/g, '')
                .replace(/ +/g, '-');
        }
    }
}());