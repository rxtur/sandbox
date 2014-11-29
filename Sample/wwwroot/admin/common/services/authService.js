angular.module('blogifire').factory("authService", [function () {
    /*
    used by client-side code to check if user authenticated.
    using synchronous ajax call waiting for result from back-end
    */
    var name = 'anonymous';
    var getName = function () {
        $.ajax({
            url: '/blog/api/auth',
            success: function (data) { name = data; },
            async: false
        });
    }
    getName();

    return {
        currentUser: name,
        isAuthenticated: name != 'anonymous'
    }
}]);