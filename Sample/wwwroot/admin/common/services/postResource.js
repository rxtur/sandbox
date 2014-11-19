(function () {
    "use strict";

    angular.module("common.services").factory("postResource", ["$resource", postResource]);

    function postResource($resource) {
        return $resource("/blog/api/posts/:Id")
    }
}());
