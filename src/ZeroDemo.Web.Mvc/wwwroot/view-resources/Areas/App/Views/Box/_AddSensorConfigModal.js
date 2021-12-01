(function ($) {
    app.modals.AddSensorConfigModal = function () {
        var _boxService = abp.services.app.box;

        var _modalManager;
        var _$boxInformationForm = null;
        var _sensorConfigService = abp.services.app.sensorConfig;
        var countAdded = 0;
        var _$sensorTable = $('#SensorTable');
        var _sensorService = abp.services.app.sensor;
        var currentBoxId = parseInt($('input[name=BoxId]').val());
        var rangePort = [];
        var sensorConfigsAdded = [];
        var countInitComplete = 0;

        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$boxInformationForm = _modalManager.getModal().find('form[name=BoxInformationsForm]');
            _$boxInformationForm.validate();
            rangePort = Array.from({ length: parseInt($('#MaxBoxPort').val()) }, (_, i) => i + 1);
        };

        var _permissions = {
            createSensorConfig: abp.auth.hasPermission('Pages.SensorConfig.Create'),
            //edit: abp.auth.hasPermission('Pages.Sensor.Edit'),
            //'delete': abp.auth.hasPermission('Pages.Sensor.Delete')
        };

        var dataTable = _$sensorTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            createdRow: function (row, data, index) {
                //$compile(row)($scope);
                if (currentBoxId && data.sensorConfigs && data.sensorConfigs.length > 0) {
                    let count = _.where(data.sensorConfigs, { boxId: currentBoxId }).length;
                    if (count > 0)
                        $(row).addClass('dt-sensor-added-bg');
                }
            },
            listAction: {
                ajaxFunction: _sensorService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SensorTableFilter').val(),
                        //boxId: currentBoxId,
                        isDeletedSensor: $("#SensorTable_IsDeletedSensor").is(':checked')
                    };
                },
            },
            initComplete: function (settings, result) {
                result.data.forEach(function (row) {
                    sensorConfigsAdded;
                    if (currentBoxId && row.sensorConfigs && row.sensorConfigs.length > 0) {
                        sensorConfigsAdded = _.where(row.sensorConfigs, { boxId: currentBoxId }).map(function (p) { return p.boxPort; });
                    }
                    if (sensorConfigsAdded && sensorConfigsAdded.length > 0) {
                        sensorConfigsAdded.forEach(function (p) {
                            rangePort = _.without(rangePort, p);
                        })
                    }
                })
                $.each(rangePort, function (i, item) {
                    $('select[name=boxPortDefault]').append($('<option>', {
                        value: item,
                        text: item
                    }));
                });
                countInitComplete++;
            },
            drawCallback: function (settings) {
                if (countInitComplete > 0) {
                    $.each(rangePort, function (i, item) {
                        $('select[name=boxPortDefault]').append($('<option>', {
                            value: item,
                            text: item
                        }));
                    });
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    title: app.localize("Actions"),
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    width: 100,
                    rowAction: {
                        element: $("<div/>")
                            //.addClass("text-center")
                            .append($("<button/>")
                                .addClass("btn btn-outline-primary btn-sm")
                                .attr("title", app.localize("Add"))
                                .text(app.localize("Add"))
                                .append($("<i/>").addClass("la la-plus pull-left"))
                            ).click(function () {
                                addSensorConfig($(this).data(), $(this).closest('tr').index());
                            }),
                        defaultelement: $("<div/>"),
                        visible: function (record) {
                            //return Math.round(Math.random()) === 1;
                            //return record.visible; //visible is record property.
                            return _permissions.createSensorConfig;
                        }
                    }
                },
                {
                    title: app.localize("Id"),
                    targets: 2,
                    width: 20,
                    data: "id"
                },
                {
                    title: app.localize("SensorName"),
                    targets: 3,
                    data: "sensorName",
                    render: function (data, type, row, meta) {
                        return data;
                    }
                },
                {
                    title: app.localize("Alarm"),
                    targets: 4,
                    orderable: false,
                    data: null,
                    //width: 30,
                    render: function (data, type, row, meta) {
                        let text =
                            '<label for="IsAlarmDefault' + row.id + '" class="checkbox checkbox-outline">' +
                            '<input id="IsAlarmDefault' + row.id + '" type="checkbox" name="isAlarmDefault" value="true" checked>' +
                            app.localize("Yes") +
                            '<span></span>' +
                            '</label>';
                        return text;
                    }
                },
                {
                    title: app.localize("Port"),
                    targets: 5,
                    orderable: false,
                    data: null,
                    render: function (data, type, row, meta) {
                        let text = '<select style="width: 70px;" name="boxPortDefault" class="form-control"></select>';
                        return text;
                    }
                },
                {
                    title: app.localize("High"),
                    targets: 6,
                    data: "highValueDefault",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="width: 80px;" class="form-control" placeholder="' + app.localize('HighValue') + '" name="highValueDefault" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("Low"),
                    targets: 7,
                    data: "lowValueDefault",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="width: 80px;" class="form-control" placeholder="' + app.localize('LowValue') + '" name="lowValueDefault" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("Target"),
                    targets: 8,
                    data: "targetValueDefault",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="width: 80px;" class="form-control" placeholder="' + app.localize('TargetValue') + '" name="targetValueDefault" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("Message"),
                    targets: 9,
                    orderable: false,
                    data: null,
                    render: function (data, type, row, meta) {
                        let text = '<input type="text" style="width: 100%;" class="form-control" placeholder="' + app.localize('AlarmMessage') + '" name="alarmMessageDefault">';
                        return text;
                    }
                }
            ]
        });

        function getSensor() {
            dataTable.ajax.reload();
        }

        function addSensorConfig(sensor, rowIndex) {
            let row = dataTable.data()[rowIndex];
            let highValueDefault = dataTable.cell(rowIndex, 6).nodes().to$().find('input[name ="highValueDefault"]').val();
            let lowValueDefault = dataTable.cell(rowIndex, 7).nodes().to$().find('input[name ="lowValueDefault"]').val();
            let targetValueDefault = dataTable.cell(rowIndex, 8).nodes().to$().find('input[name ="targetValueDefault"]').val();
            let boxPort = dataTable.cell(rowIndex, 5).nodes().to$().find('select[name ="boxPortDefault"]').val();
            let alarmMessage = dataTable.cell(rowIndex, 9).nodes().to$().find('input[name ="alarmMessageDefault"]').val();
            let isAlarm = dataTable.cell(rowIndex, 4).nodes().to$().find('input[name ="isAlarmDefault"]').prop('checked');
            abp.message.confirm(
                app.localize('Sensor') + ': ' + sensor.sensorName,
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy('.modal-body');
                        let sensorConfig = {
                            boxId: currentBoxId, //window.location.href.split('/')[window.location.href.split('/').length-1]
                            sensorId: sensor.id,
                            highValue: highValueDefault,
                            lowValue: lowValueDefault,
                            targetValue: targetValueDefault,
                            inrangeColor: '',
                            outrangeColor: '',
                            boxPort: parseInt(boxPort),
                            alarmMessage: alarmMessage,
                            isAlarm: isAlarm,
                        }
                        _sensorConfigService.createOrUpdate(sensorConfig).done(function () {
                            countAdded++;
                            //getSensor(true);
                            $('#SensorTable option[value=' + boxPort + ']').remove();
                            dataTable.rows(rowIndex).nodes().to$().addClass('dt-sensor-added-bg');
                            abp.notify.success(app.localize('SuccessfullyAdded'));
                        }).always(function () {
                            abp.ui.clearBusy('.modal-body');
                        });
                    }
                }
            );
        }

        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateSensorButton').click(function () {
            _createOrEditModal.open();
        });

        var getSortingFromDatatable = function () {
            if (dataTable.ajax.params().order.length > 0) {
                var columnIndex = dataTable.ajax.params().order[0].column;
                var dir = dataTable.ajax.params().order[0].dir;
                var columnName = dataTable.ajax.params().columns[columnIndex].data;

                return columnName + ' ' + dir;
            } else {
                return '';
            }
        };

        $('#ExportSensorToExcelButton').click(function (e) {
            e.preventDefault();
            _sensorService
                .getSensorToExcel({
                    filter: $('#SensorTableFilter').val(),
                    permissions: _selectedPermissionNames,
                    role: $("#RoleSelectionCombo").val(),
                    onlyLockedSensor: $("#SensorTable_OnlyLockedSensor").is(':checked'),
                    sorting: getSortingFromDatatable()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        $('#GetSensorButton, #RefreshSensorListButton').click(function (e) {
            e.preventDefault();
            getSensor();
        });

        $('#SensorTableFilter').on('keydown', function (e) {
            if (e.keyCode !== 13) {
                return;
            }

            e.preventDefault();
            getSensor();
        });

        abp.event.on('app.createOrEditSensorModalSaved', function () {
            getSensor();
        });

        $('#SensorTableFilter').focus();

        $('#ImportSensorFromExcelButton').fileupload({
            url: abp.appPath + 'Sensor/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#SensorTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('ImportSensorProcessStart'));
                } else {
                    abp.notify.warn(app.localize('ImportSensorUploadFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

        $('button.close-button[data-dismiss=modal]').click(function () {
            if (countAdded > 0) {
                abp.event.trigger('app.sensorConfigModalAdded');
            }

        })
    };
})(jQuery);