(function () {
    $(function () {

        var _$sensorTable = $('#SensorTable');
        var _sensorService = abp.services.app.sensor;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Sensor.Create'),
            edit: abp.auth.hasPermission('Pages.Sensor.Edit'),
            'delete': abp.auth.hasPermission('Pages.Sensor.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Sensor/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Sensor/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditSensorModal',
        });

        var dataTable = _$sensorTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _sensorService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SensorTableFilter').val(),
                        //permissions: _selectedPermissionNames,
                        //role: $("#RoleSelectionCombo").val(),
                        isDeletedSensor: $("#SensorTable_IsDeletedSensor").is(':checked')
                    };
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
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    width: 120,
                    rowAction: {
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id});
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteSensor(data.record);
                            }
                        }]
                    }
                },
                {
                    targets: 2,
                    width: 20,
                    data: "id"
                },
                {
                    targets: 3,
                    data: "sensorName",
                    render: function (data, type, row, meta) {
                        return data;
                    }
                },
                {
                    targets: 4,
                    data: "highValueDefault"
                },
                {
                    targets: 5,
                    data: "lowValueDefault"
                },
                {
                    targets: 6,
                    data: "targetValueDefault"
                },
                {
                    targets: 7,
                    data: "creationTime",
                    render: function (creationTime) {
                        return moment(creationTime).format('L') + ' ' + moment(creationTime).format('LT');
                    }
                }
                //{
                //    targets: 7,
                //    data: "isEmailConfirmed",
                //    render: function (isEmailConfirmed) {
                //        var $span = $("<span/>").addClass("label");
                //        if (isEmailConfirmed) {
                //            $span.addClass("label label-success label-inline").text(app.localize('Yes'));
                //        } else {
                //            $span.addClass("label label-dark label-inline").text(app.localize('No'));
                //        }
                //        return $span[0].outerHTML;
                //    }
                //},

            ]
        });

        function getSensor() {
            dataTable.ajax.reload();
        }

        function deleteSensor(sensor) {
            //if (sensor.sensorName === app.consts.sensorManagement.defaultAdminSensorName) {
            //    abp.message.warn(app.localize("{0}SensorCannotBeDeleted", app.consts.sensorManagement.defaultAdminSensorName));
            //    return;
            //}

            abp.message.confirm(
                app.localize('SensorDeleteWarningMessage', sensor.sensorName),
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _sensorService.delete({
                            id: sensor.id
                        }).done(function () {
                            getSensor(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
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

        //$("#FilterByPermissionsButton").click(function () {
        //    _$permissionFilterModal.open({ grantedPermissionNames: _selectedPermissionNames });
        //});
    });
})();
