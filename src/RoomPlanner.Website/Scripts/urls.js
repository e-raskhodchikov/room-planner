(function(module) {

    'use strict';

    var prefix = {
        room: '/Room/',
        furniture: '/Furniture/',
        plan: '/Plan/'
    };

    module.Urls = {
        CreateRoom: prefix.room + 'Create',
        RemoveRoom: prefix.room + 'Remove',

        CreateFurniture: prefix.furniture + 'Create',
        MoveFurniture: prefix.furniture + 'Move',

        GetRoomPlan: prefix.plan + 'GetPlan',
        GetPlansHistory: prefix.plan + 'GetHistory'
    };

})(App);
