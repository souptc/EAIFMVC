var Notification = function(data) {
    
}

var NotificationList = function(data) {
    var self = this;
    self.notices = ko.observableArray(ko.utils.arrayMap(data.notificationList, function (c) {
        return new Notification(c);
    }));
}

var CombinedViewModel = function (data) {
    var self = this;
    self.notificationList = new NotificationList(data.notificationList);
}



var combinedData = {
    notificationList: jsonListData,
}

var combinedModel = new CombinedViewModel(combinedData);
ko.applyBindings(combinedModel);