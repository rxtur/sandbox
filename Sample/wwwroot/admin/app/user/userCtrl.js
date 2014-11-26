(function () {
    "use strict";
    angular.module("blogifire").controller("userCtrl", ["userService", userCtrl]);

    function userCtrl(userService) {
        var vm = this;
        vm.user = userService();

        vm.Title = "Settings";
    }
}());