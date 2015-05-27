ko.bindingHandlers.popHanlde = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var allBindings = allBindingsAccessor();
        var $element = $(element);
        $element.addClass('hide modal');

        if (allBindings.modalOptions) {
            if (allBindings.modalOptions.beforeClose) {
                $element.on('hide', function () {
                    return allBindings.modalOptions.beforeClose();
                });
            }
        }
        return ko.bindingHandlers['with'].init.apply(this, arguments);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        if (value) {
            $.facebox(element);
        } else {
            $(document).trigger('close.facebox');
        }

        if (ko.bindingHandlers['with'].update != null)
            return ko.bindingHandlers['with'].update.apply(this, arguments);
        else
            return 0;
    }
};

var reasonViewModel = function (reason) {
    var self = this;
    self.Text= ko.observable(reason);

    self.EditText= ko.observable(reason);

    self.accept = function() {
        self.Text(self.EditText());
    };

    self.reset = function() {
        self.EditText(self.EditText());
    };
}

var AlertViewModel = function(data) {
    var self = this;
    self.ID = ko.observable(data.Alert.ID);
    self.alertTime = ko.observable(data.Alert.AlertTime);
    self.companyName = ko.observable(data.Company.Name);
    self.dangerSource = ko.observable("高危");
    self.status = ko.observable(data.Alert.Status);
    self.statusDesc = ko.observable(data.Alert.StatusDescription);
    self.phone = ko.observable(data.Company.Phone);
    self.reasonViewModel = new reasonViewModel("yyy");

    self.statusStyle = ko.computed(function () {
        if (self.status() == 0)
            return "alert2";
        else if (self.status() == 1)
            return "alert3";
        else if (self.status() == 2)
            return "alert1";
        else if (self.status() == 3)
            return "alert4";
    });

    self.editingReason = ko.observable();
    self.editReason= function(model) {
        self.editingReason(model.reasonViewModel);
        self.editingReason().reset();
    }

    self.saveReason = function() {
        console.log("save");
        self.editingReason().accept();
        self.editingReason(undefined);
    }

    self.cancelReason =function() {
        self.editingReason(undefined);
    }

    self.MistakeOp = function () {
        if (confirm("确认要标记为误报吗？")) {
            self.status(1);
            self.statusDesc("误报");
        }
    }
}



var CombinedViewModel = function(data) {
    var self = this;
    self.alertViewModel = new AlertViewModel(data);
}



var combinedData = {
    Alert: jsonAlertData,
    Company: jsonCompanyDate
}

var combinedModel = new CombinedViewModel(combinedData);
ko.applyBindings(combinedModel);