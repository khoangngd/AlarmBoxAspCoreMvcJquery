(function () {
    $(function () {

        var _$boxTable = $('#BoxTable');
        var _boxService = abp.services.app.box;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Box.Create'),
            edit: abp.auth.hasPermission('Pages.Box.Edit'),
            'delete': abp.auth.hasPermission('Pages.Box.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Box/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Box/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditBoxModal',
        });

        var dataTable = _$boxTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _boxService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#BoxTableFilter').val(),
                        isDeletedBox: $("#BoxTable_IsDeletedBox").is(':checked')
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
                            text: app.localize('Detail'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                window.location = 'Box/Detail/' + data.record.id;
                            }
                        }, {
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteBox(data.record);
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
                    data: "boxName",
                    render: function (data, type, row, meta) {
                        return data;
                    }
                },
                {
                    targets: 4,
                    data: "location"
                },
                {
                    targets: 5,
                    data: "maxBoxPort"
                },
                {
                    targets: 6,
                    data: "maxBoxManagerPort"
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


        function getBox() {
            dataTable.ajax.reload();
        }

        function deleteBox(box) {
            //if (box.boxName === app.consts.boxManagement.defaultAdminBoxName) {
            //    abp.message.warn(app.localize("{0}BoxCannotBeDeleted", app.consts.boxManagement.defaultAdminBoxName));
            //    return;
            //}

            abp.message.confirm(
                app.localize('BoxDeleteWarningMessage', box.boxName),
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _boxService.delete({
                            id: box.id
                        }).done(function () {
                            getBox(true);
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

        $('#CreateBoxButton').click(function () {
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

        $('#ExportBoxToExcelButton').click(function (e) {
            e.preventDefault();
            _boxService
                .getBoxToExcel({
                    filter: $('#BoxTableFilter').val(),
                    permissions: _selectedPermissionNames,
                    role: $("#RoleSelectionCombo").val(),
                    onlyLockedBox: $("#BoxTable_OnlyLockedBox").is(':checked'),
                    sorting: getSortingFromDatatable()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        $('#GetBoxButton, #RefreshBoxListButton').click(function (e) {
            e.preventDefault();
            getBox();
        });

        $('#BoxTableFilter').on('keydown', function (e) {
            if (e.keyCode !== 13) {
                return;
            }

            e.preventDefault();
            getBox();
        });

        abp.event.on('app.createOrEditBoxModalSaved', function () {
            getBox();
        });

        $('#BoxTableFilter').focus();

        $('#ImportBoxFromExcelButton').fileupload({
            url: abp.appPath + 'Box/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#BoxTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('ImportBoxProcessStart'));
                } else {
                    abp.notify.warn(app.localize('ImportBoxUploadFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

        //$("#FilterByPermissionsButton").click(function () {
        //    _$permissionFilterModal.open({ grantedPermissionNames: _selectedPermissionNames });
        //});
    });
})();
