(function () {
    "use strict";
    angular.module("blogifire").controller("SettingsCtrl", [SettingsCtrl]);

    function SettingsCtrl() {
        var vm = this;

        vm.Title = "Settings";
    }
}());