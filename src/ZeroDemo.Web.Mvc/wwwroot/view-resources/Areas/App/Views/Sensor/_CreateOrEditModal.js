(function ($) {
    app.modals.CreateOrEditSensorModal = function () {
        var _sensorService = abp.services.app.sensor;

        var _modalManager;
        var _$sensorInformationForm = null;
              
        this.init = function (modalManager) {
            _modalManager = modalManager;                     

            _$sensorInformationForm = _modalManager.getModal().find('form[name=SensorInformationsForm]');
            _$sensorInformationForm.validate();
        };

        this.save = function () {
            if (!_$sensorInformationForm.valid()) {
                return;
            }

            var sensor = _$sensorInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _sensorService.createOrUpdate(sensor)
                .done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditSensorModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);