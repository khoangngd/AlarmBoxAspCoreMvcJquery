(function () {
    $(function () {
        var _boxService = abp.services.app.box;
        var _$boxForm = null;
        var rangePort = [];
        var currentBoxId = parseInt($('input[name=BoxId]').val());
        var _$sensorConfigTable = $('#SensorConfigTable');
        var _sensorConfigService = abp.services.app.sensorConfig;
        var _$boxManagerTable = $('#BoxManagerTable');
        var _boxManagerService = abp.services.app.boxManager;
        var _permissions = {
            create: abp.auth.hasPermission('Pages.SensorConfig.Create'),
            edit: abp.auth.hasPermission('Pages.SensorConfig.Edit'),
            'delete': abp.auth.hasPermission('Pages.SensorConfig.Delete')
        };

        $('.save-button').click(function () {
            if (!_$boxForm.valid()) {
                return;
            }
            abp.ui.setBusy('.tab-content');
            var box = _$boxForm.serializeFormToObject();
            box.id = box.BoxId;
            _boxService.createOrUpdate(box)
                .done(function () {
                    //$('#MaxBoxPort').value = box.MaxBoxPort;
                    //getRangePort(true);
                    getSensorConfig(true);
                    getBoxManager(true);
                    abp.notify.info(app.localize('SavedSuccessfully'));
                }).always(function () {
                    $('.subheader .text-muted').html(box.BoxName);
                    //document.getElementById('PostID').value = val;
                    abp.ui.clearBusy('.tab-content');
                });
        })

        $(function () {
            _$boxForm = $('form[name=BoxInformationsForm]');
            getRangePort(true);

            $('#SensorConfigTable').DataTable().responsive.recalc();
          
            $('#BoxManagerTable').DataTable().responsive.recalc();


            //$('#SensorConfigTable').DataTable().responsive.rebuild();
            //$('#BoxManagerTable').DataTable().responsive.rebuild();
        });

        function getRangePort() {
            rangePort = Array.from({ length: parseInt($('#MaxBoxPort').val()) }, (_, i) => i + 1);
            rangePortManager = Array.from({ length: parseInt($('#MaxBoxManagerPort').val()) }, (_, i) => i + 1);
        }



        var _addSensorConfigModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Box/AddSensorConfigModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Box/_AddSensorConfigModal.js',
            modalClass: 'AddSensorConfigModal',
            modalSize: 'modal-xl'
        });

        //$("#SensorConfigTable tbody").on('click', '.edit-button', function () {
        //    debugger
        //    alert('Row index: ' + $(this).closest('tr').index());
        //});              

        var dataTable = _$sensorConfigTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _sensorConfigService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SensorConfigTableFilter').val(),
                        boxId: currentBoxId,
                        //isDeletedSensorConfig: $("#SensorConfigTable_IsDeletedSensorConfig").is(':checked')
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
                    title: app.localize("Actions"),
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
                            name: 'editBtn',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                //addSensorConfig(data.record);
                            },
                            //className: 'editButton'
                        }, {
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteSensorConfig(data.record);
                                
                            }
                        }]
                    }
                },
                {
                    title: app.localize("Id"),
                    targets: 2,
                    width: 20,
                    data: "id"
                },
                {
                    title: app.localize("Sensor"),
                    targets: 3,
                    data: "sensor.sensorName",
                    render: function (data, type, row, meta) {
                        return data;
                    }
                },
                {
                    title: app.localize("Alarm"),
                    targets: 4,
                    orderable: false,
                    data: "isAlarm",
                    //render: function (isAlarm) {
                    //    var $span = $("<span/>").addClass("label");
                    //    if (isAlarm) {
                    //        $span.addClass("label label-success label-inline").text(app.localize('Yes'));
                    //    } else {
                    //        $span.addClass("label label-dark label-inline").text(app.localize('No'));
                    //    }
                    //    return $span[0].outerHTML;
                    //},  
                    render: function (data, type, row, meta) {//@Html.Raw(Model.User.IsActive ? "checked=\"checked\"" : "")
                        let text =
                            '<label for="IsAlarm' + row.id + '" class="checkbox checkbox-outline">' +
                            '<input id="IsAlarm' + row.id + '" type="checkbox" name="isAlarm" value="true" ' + (data ? 'checked' : '') + '>' +
                            app.localize("Yes") +
                            '<span></span>' +
                            '</label>';
                        return text;
                        //var $span = $("<span/>").addClass("label");
                        //if (isAlarm) {
                        //    $span.addClass("label label-success label-inline").text(app.localize('Yes'));
                        //} else {
                        //    $span.addClass("label label-dark label-inline").text(app.localize('No'));
                        //}
                        //return $span[0].outerHTML;
                    },
                },
                {
                    title: app.localize("Port"),
                    targets: 5,
                    data: "boxPort",
                    render: function (data, type, row, meta) {
                        let optionsHasSelected = '';
                        rangePort.forEach(function (ele) {
                            optionsHasSelected += '<option name="boxPort" ' + (ele == data ? 'selected' : '') + ' value="' + ele + '">' + /*app.localize('Port') +*/ ele + '</option>';
                        })
                        let text = '<select style="width: 70px;" name="boxPort" class="form-control">'
                            + optionsHasSelected
                            + '</select>';
                        return text;
                    }
                },
                {
                    title: app.localize("HighValue"),
                    targets: 6,
                    data: "highValue",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="max-width: 80px;" class="form-control" placeholder="' + app.localize('HighValue') + '" name="highValue" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("LowValue"),
                    targets: 7,
                    data: "lowValue",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="max-width: 80px;" class="form-control" placeholder="' + app.localize('LowValue') + '" name="lowValue" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("TargetValue"),
                    targets: 8,
                    data: "targetValue",
                    render: function (data, type, row, meta) {
                        let text = '<input type="number" style="max-width: 80px;" class="form-control" placeholder="' + app.localize('TargetValue') + '" name="targetValue" value="' + data + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("Message"),
                    targets: 9,
                    orderable: false,
                    data: "alarmMessage",
                    render: function (data, type, row, meta) {
                        let text = '<input type="text" style="min-width: 200px;" class="form-control" placeholder="' + app.localize('Message') + '" name="alarmMessage" value="' + (data ? data : '') + '">';
                        return text;
                    }
                },

            ]
        });

        $('#SensorConfigTable tbody').on('click', 'tr .dropdown-item:even', function () {//vị trí lẻ 1,3,5
            let rowIndex = $(this).closest("tr").index();               
            addSensorConfig(rowIndex);
            //console.log('click', rowIndex);
        });

        function getSensorConfig() {
            getRangePort(true);
            dataTable.ajax.reload();
        }

        function addSensorConfig(rowIndex) {
            let row = dataTable.data()[rowIndex];
            let highValue = dataTable.cell(rowIndex, 6).nodes().to$().find('input[name ="highValue"]').val();
            let lowValue = dataTable.cell(rowIndex, 7).nodes().to$().find('input[name ="lowValue"]').val();
            let targetValue = dataTable.cell(rowIndex, 8).nodes().to$().find('input[name ="targetValue"]').val();
            let boxPort = dataTable.cell(rowIndex, 5).nodes().to$().find('select[name ="boxPort"]').val();
            let alarmMessage = dataTable.cell(rowIndex, 9).nodes().to$().find('input[name ="alarmMessage"]').val();
            let isAlarm = dataTable.cell(rowIndex, 4).nodes().to$().find('input[name ="isAlarm"]').prop('checked');


            abp.message.confirm(
                app.localize('Sensor') + ': ' + row.sensor.sensorName,
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy('.tab-content');
                        let sensorConfig = {
                            id: row.id,
                            boxId: currentBoxId, //window.location.href.split('/')[window.location.href.split('/').length-1]
                            sensorId: row.sensorId ? row.sensorId : row.sensor.id,
                            highValue: highValue,
                            lowValue: lowValue,
                            targetValue: targetValue,
                            inrangeColor: '',
                            outrangeColor: '',
                            boxPort: parseInt(boxPort),
                            alarmMessage: alarmMessage,
                            isAlarm: isAlarm,
                        }
                        _sensorConfigService.createOrUpdate(sensorConfig).done(function () {
                            //countAdded++;
                            getSensorConfig(true);
                            abp.notify.success(app.localize('SuccessfullyAdded'));
                        }).always(function () {
                            abp.ui.clearBusy('.tab-content');
                        });
                    }
                }
            );
        }

        function deleteSensorConfig(sensorConfig) {
            abp.message.confirm(
                app.localize('Sensor') + ': ' + sensorConfig.sensor.sensorName,
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy('.tab-content');
                        _sensorConfigService.delete({
                            id: sensorConfig.id
                        }).done(function () {
                            getSensorConfig(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        }).always(function () {
                            abp.ui.clearBusy('.tab-content');
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

        $('#AddSensorConfigButton').click(function () {
            _addSensorConfigModal.open({ id: currentBoxId });
        });

        $('#GetSensorConfigButton, #RefreshSensorConfigListButton').click(function (e) {
            e.preventDefault();
            getSensorConfig();
        });

        $('#SensorConfigTableFilter').on('keydown', function (e) {
            if (e.keyCode !== 13) {
                return;
            }

            e.preventDefault();
            getSensorConfig();
        });

        abp.event.on('app.sensorConfigModalAdded', function () {
            getSensorConfig();
        });

        $('#SensorConfigTableFilter').focus();

        var dataTableBoxManager = _$boxManagerTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _boxManagerService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#BoxManagerTableFilter').val(),
                        boxId: currentBoxId,
                        //isDeletedBoxManager: $("#BoxManagerTable_IsDeletedBoxManager").is(':checked')
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
                    title: app.localize("Actions"),
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
                            },
                        }, {
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteBoxManager(data.record);
                            }
                        }]
                    }
                },
                {
                    title: app.localize("Id"),
                    targets: 2,
                    width: 20,
                    data: "id"
                },
                {
                    title: app.localize("ManagerName"),
                    targets: 3,
                    orderable: false,
                    data: "managerName",
                    render: function (data, type, row, meta) {
                        let text = '<input type="text" style="width: 100%;" class="form-control" placeholder="' + app.localize('ManagerName') + '" name="managerName" value="' + (data ? data : '') + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("Alarm"),
                    targets: 4,
                    orderable: false,
                    data: "isAlarm", 
                    render: function (data, type, row, meta) {//@Html.Raw(Model.User.IsActive ? "checked=\"checked\"" : "")
                        let text =
                            '<label for="IsAlarmManager' + row.id + '" class="checkbox checkbox-outline">' +
                            '<input id="IsAlarmManager' + row.id + '" type="checkbox" name="isAlarmManager" value="true" ' + (data ? 'checked' : '') + '>' +
                            app.localize("Yes") +
                            '<span></span>' +
                            '</label>';
                        return text;
                    },
                },
                {
                    title: app.localize("Port"),
                    targets: 5,
                    data: "managerPort",
                    render: function (data, type, row, meta) {
                        let optionsHasSelected = '';
                        rangePortManager.forEach(function (ele) {
                            optionsHasSelected += '<option name="managerPort" ' + (ele == data ? 'selected' : '') + ' value="' + ele + '">' + /*app.localize('Port') +*/ ele + '</option>';
                        })
                        let text = '<select style="width: 70px;" name="managerPort" class="form-control">'
                            + optionsHasSelected
                            + '</select>';
                        return text;
                    }
                },
                {
                    title: app.localize("ManagerPhoneNumber"),
                    targets: 6,
                    orderable: false,
                    data: "managerPhoneNumber",
                    render: function (data, type, row, meta) {
                        let text = '<input type="text" style="width: 100%;" class="form-control" placeholder="' + app.localize('ManagerPhoneNumber') + '" name="managerPhoneNumber" value="' + (data ? data : '') + '">';
                        return text;
                    }
                },
                {
                    title: app.localize("ManagerEmail"),
                    targets: 7,
                    orderable: false,
                    data: "managerEmail",
                    render: function (data, type, row, meta) {
                        let text = '<input type="text" style="width: 100%;" class="form-control" placeholder="' + app.localize('ManagerEmail') + '" name="managerEmail" value="' + (data ? data : '') + '">';
                        return text;
                    }
                }

            ]
        });

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) { //when datatable in tabs, bootstrap responsive not run
            $($.fn.dataTable.tables(true)).DataTable()
                .columns.adjust()
                .responsive.recalc();
        });

        $('#BoxManagerTable tbody').on('click', 'tr .dropdown-item:even', function () {//vị trí lẻ 1,3,5
            let rowIndex = $(this).closest("tr").index();
            addBoxManager(rowIndex);
        });

        function addBoxManager(rowIndex) {
            let row = dataTableBoxManager.data()[rowIndex];
            let managerName = dataTableBoxManager.cell(rowIndex, 3).nodes().to$().find('input[name ="managerName"]').val();
            let isAlarm = dataTableBoxManager.cell(rowIndex, 4).nodes().to$().find('input[name ="isAlarmManager"]').prop('checked');
            let managerPort = dataTableBoxManager.cell(rowIndex, 5).nodes().to$().find('select[name ="managerPort"]').val();
            let managerPhoneNumber = dataTableBoxManager.cell(rowIndex, 6).nodes().to$().find('input[name ="managerPhoneNumber"]').val();
            let managerEmail = dataTableBoxManager.cell(rowIndex, 7).nodes().to$().find('input[name ="managerEmail"]').val();
            abp.message.confirm(
                //app.localize('ManagerName') + ': ' + (row.managerName ? row.managerName : '') + ' &#8594; ' + managerName,
                app.localize('ManagerName') + ': ' + managerName,
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy('.tab-content');
                        let boxManager = {
                            id: row.id ? row.id : null,
                            boxId: currentBoxId, //window.location.href.split('/')[window.location.href.split('/').length-1]
                            managerEmail: managerEmail,
                            managerPhoneNumber: managerPhoneNumber,
                            managerName: managerName,
                            managerPort: parseInt(managerPort),
                            isAlarm: isAlarm,
                        }
                        _boxManagerService.createOrUpdate(boxManager).done(function () {
                            //countAdded++;
                            getBoxManager(true);
                            abp.notify.success(app.localize('SuccessfullyAdded'));
                        }).always(function () {
                            abp.ui.clearBusy('.tab-content');
                        });
                    }
                }
            );
        }

        function deleteBoxManager(boxManager) {
            abp.message.confirm(
                app.localize('Sensor') + ': ' + boxManager.managerName,
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy('.tab-content');
                        _boxManagerService.delete({
                            id: boxManager.id
                        }).done(function () {
                            getBoxManager(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        }).always(function () {
                            abp.ui.clearBusy('.tab-content');
                        });
                    }
                }
            );
        }

        function getBoxManager() {
            getRangePort(true);
            dataTableBoxManager.ajax.reload();
        }

        $('#SensorConfigTable').DataTable().responsive.recalc();

        $('#BoxManagerTable').DataTable().responsive.recalc();
    });
})();
