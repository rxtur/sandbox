(function () {
    "use strict";
    var app = angular.module("blogifire", ["common.services", "ui.router"]); // , "postResourceMock"

    app.config(function ($provide) {
        $provide.decorator("$exceptionHandler",
        ["$delegate", function ($delegate) {
            return function (exception, cause) {
                exception.message = "Error happened. \n Message: " + exception.message;
                $delegate(exception, cause);
                toastr.error(exception.message);
            };
        }]);
    });

    app.config(["$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise("/");
            //var x = userService();

            toastr.options.positionClass = 'toast-bottom-right';
            toastr.options.backgroundpositionClass = 'toast-bottom-right';

            $stateProvider
            .state("postList", {
                url: "/",
                templateUrl: "app/posts/postListView.html",
                controller: "postListCtrl as vm"
            })
 			.state("postEdit", {
 				url: "/posts/:Id",
 				templateUrl: "app/posts/postEditView.html",
 				controller: "postEditCtrl as vm",
 				resolve: {
 				    postResource: "postResource",
 				    post: function (postResource, $stateParams) {
 				        var Id = $stateParams.Id;
 				        return postResource.get({ Id: Id }).$promise;
 				    }
 				}
 			})
            .state("settings", {
                url: "/settings",
                templateUrl: "app/settings/settings.html",
                controller: "SettingsCtrl as vm"
            })
            .state("help", {
                url: "/help",
                templateUrl: "app/help/help.html",
                controller: "HelpCtrl as vm"
            })
            .state("profile", {
                url: "/profile",
                templateUrl: "app/profile/profile.html",
                controller: "ProfileCtrl as vm"
            })
        }]
    );
}());