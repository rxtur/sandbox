(function () {
    "use strict";
    angular.module("blogifire").controller("ProfileCtrl", [ProfileCtrl]);

    function ProfileCtrl() {
        var vm = this;

        vm.Title = "Profile";
    }
}());