(function ($) {
    app.modals.CreateOrEditBoxModal = function () {
        var _boxService = abp.services.app.box;

        var _modalManager;
        var _$boxInformationForm = null;
              
        this.init = function (modalManager) {
            _modalManager = modalManager;                     

            _$boxInformationForm = _modalManager.getModal().find('form[name=BoxInformationsForm]');
            _$boxInformationForm.validate();
        };

        this.save = function () {
            if (!_$boxInformationForm.valid()) {
                return;
            }

            var box = _$boxInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _boxService.createOrUpdate(box)
                .done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditBoxModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);