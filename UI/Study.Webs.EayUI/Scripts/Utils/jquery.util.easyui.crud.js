(function ($) {
    //更新表单Id
    $.easyui.formId = "form";
    //查询表单Id
    $.easyui.queryFormId = "formQuery";
    //表格Id
    $.easyui.gridId = "grid";
    //更新按钮Id
    $.easyui.btnEditId = "btnEdit";
    //查看按钮Id
    $.easyui.btnLookId = "btnLook";
    //表格右键菜单Id
    $.easyui.gridMenuId = "menuGrid";
    //删除的后台Url
    $.easyui.deleteUrl = "";

    //获取更新表单jQuery对象
    function getForm$() {
        return $("#" + $.easyui.formId);
    }

    //获取上传表单jQuery对象
    function getUpLoadForm$() {
        return $("#form_upload");
    }

    //获取查询表单jQuery对象
    function getQueryForm$() {
        return $("#" + $.easyui.queryFormId);
    }

    //获取表格jQuery对象
    function getGrid$() {
        return $("#" + $.easyui.gridId);
    }

    //提交失败回调函数
    function submitError(xhr, ajaxOptions, thrownError) {
        $.easyui.warn("Http status: " + xhr.status + " " + xhr.statusText + "\najaxOptions: " + ajaxOptions + "\nthrownError:" + thrownError + "\n" + xhr.responseText);
    }

    //初始化弹出窗口参数
    function initDialog(options,msg) {
        var row = getGrid$().datagrid('getSelected');
        if (!row) {
            $.easyui.warn(msg);
            return false;
        }
        options.url = $.joinUrl(options.url, "id=" + row.Id);
        return true;
    }

    $.easyui.refresh = function () {
        ///	<summary>
        ///	刷新查询表单
        ///	</summary>
        $.easyui.clearForm($.easyui.queryFormId);
        $.easyui.query();
    };
    $.easyui.clearForm = function (formId) {
        ///	<summary>
        ///	清空表单
        ///	</summary>
        ///	<param name="formId" type="String">
        ///	表单Id
        ///	</param>
        $('#' + formId).form("reset");
    };
    $.easyui.query = function () {
        ///	<summary>
        ///	查询
        ///	</summary>
        getGrid$().datagrid({
            pageNumber: 1,
            queryParams: getQueryForm$().serializeJson()
        });
    };

    $.easyui.download = function (downloadurl) {
        var parameter = "?";
        var array = getQueryForm$().serializeArray();
        var len = array.length;
        for (var i = 0; i < len; i++)
        {
            if (i == len - 1)
            {
                parameter = parameter + array[i].name + "=" + array[i].value;
            }
            else
            {
                parameter = parameter + array[i].name + "=" + array[i].value + "&";
            }
        }

        window.location.href = downloadurl + parameter;
    };

    //显示更新窗口
    $.easyui.showEditDialog = function () {
        $("#" + $.easyui.btnEditId).click();
    };
    //显示查看窗口
    $.easyui.showLookDialog = function () {
        $("#" + $.easyui.btnLookId).click();
    };
    $.easyui.submit = function (callback) {
        ///	<summary>
        ///	提交更新表单
        ///	</summary>
        ///	<param name="callback" type="Function">
        ///	成功回调函数
        ///	</param>
        var $form = getForm$();
        if (!validate())
            return;
        ajaxSubmit();

        //验证表单
        function validate() {
            return $form.form('validate');
        }

        //提交
        function ajaxSubmit() {
            var message = $form.attr("confirm");
            if (message)
                $.easyui.confirm(message, ajax);
            else
                ajax();
        }

        //发送请求
        function ajax() {
            $.easyui.showLoading();
            $.ajax({
                type: 'POST',
                url: $form.attr("action"),
                data: $form.serializeArray(),
                dataType: "json",
                cache: false,
                success: function (result) {
                    $.easyui.removeLoading();
                    if (callback)
                        callback(result);
                    else
                        submitSuccess(result);
                },
                error: function (result) {
                    $.easyui.removeLoading();
                    submitError(result);
                }
            });
        };

        //成功回调函数
        function submitSuccess(result) {
            var $current = $.easyui.getCurrent$();
            $current('#' + $.easyui.gridId).datagrid('reload');
            $.easyui.closeDialog();
            $.easyui.showFormMessage(result);
        }
    };
    //显示表单消息
    $.easyui.showFormMessage = function (result) {
        if (result.Code === $.easyui.state.ok)
            $.easyui.topShow(result.Message);
        else if (result.Code === $.easyui.state.fail)
            $.easyui.warn(result.Message);
    };
    //状态:ok为成功，fail为失败
    $.easyui.state = {
        ok: 1,
        fail: 2
    };
    $.easyui.delete = function (url, callback) {
        ///	<summary>
        ///	删除记录
        ///	</summary>
        ///	<param name="url" type="String">
        ///	处理删除的后台url
        ///	</param>
        ///	<param name="callback" type="Function">
        ///	成功回调函数
        ///	</param>
        url = getUrl();
        if (!url) {
            $.easyui.warn("删除Url未设置，请联系管理员");
            return;
        }
        var $grid = getGrid$();
        var rows = getRows();
        if ($.isEmptyArray(rows)) {
            $.easyui.warn("请选择待删除的记录");
            return;
        }
        $.easyui.confirm("您确定删除选中的记录吗?", ajaxDelete);

        //获取删除url
        function getUrl() {
            return url || $.easyui.deleteUrl;
        }

        //获取待删除的记录
        function getRows() {
            var result = $grid.datagrid("getChecked");
            if (!$.isEmptyArray(result))
                return result;
            var row = $grid.datagrid('getSelected');
            if (!row)
                return result;
            result.push(row);
            return result;
        }

        //发送删除请求
        function ajaxDelete() {
            $.easyui.showLoading();
            var ids = getIds();
            ajax(ids);
        }

        //获取id字符串，用逗号拼接
        function getIds() {
            var ids = "";
            $(rows).each(function (i, row) {
                ids += i == 0 ? row.Id : "," + row.Id;
            });
            return ids;
        }

        //发送请求
        function ajax(id) {
            $.ajax({
                type: 'POST',
                url: url || $.easyui.deleteUrl,
                data: "ids=" + id,
                dataType: "json",
                cache: false,
                success: function (result) {
                    $.easyui.removeLoading();
                    if (callback)
                        callback(result);
                    else
                        deleteSuccess(result);
                },
                error: function (result) {
                    $.easyui.removeLoading();
                    submitError(result);
                }
            });
        };

        //删除成功回调函数
        function deleteSuccess(result) {
            $grid.datagrid('reload');
            $.easyui.showFormMessage(result);
        }
    };
    $.easyui.fnRightClickGridRow = function (e, index, row) {
        ///	<summary>
        ///	右键单击表格行事件处理
        ///	</summary>
        ///	<param name="e" type="Event">
        ///	事件
        ///	</param>
        ///	<param name="index" type="int">
        ///	行索引
        ///	</param>
        ///	<param name="row" type="Object">
        ///	行对象
        ///	</param>
        getGrid$().datagrid("selectRow", index);
        $.easyui.showMenu($.easyui.gridMenuId, e);
    };
    $.easyui.fnClickGridMenu = function (item) {
        ///	<summary>
        ///	单击表格菜单事件处理
        ///	</summary>
        ///	<param name="item" type="Object">
        ///	右键菜单项
        ///	</param>
        switch (item.id) {
            case "menuItem_Edit":
                return editRow();
            case "menuItem_Delete":
                return deleteRow();
            case "menuItem_Look":
                return lookRow();
        }
        return true;

        //编辑
        function editRow() {
            $.easyui.showEditDialog();
        }

        //删除
        function deleteRow() {
            $.easyui.delete();
        }

        //查看
        function lookRow() {
            $.easyui.showLookDialog();
        }
    };
    $.easyui.fnInitEdit = function(options) {
        ///	<summary>
        ///	弹出编辑窗口初始化事件处理
        ///	</summary>
        ///	<param name="options" type="Object">
        ///	弹出窗口参数
        ///	</param>
        return initDialog(options,"请选择待更新的记录");
    };
    $.easyui.fnInitLook = function (options) {
        ///	<summary>
        ///	弹出查看窗口初始化事件处理
        ///	</summary>
        ///	<param name="options" type="Object">
        ///	弹出窗口参数
        ///	</param>
        return initDialog(options,"请选择待查看的记录");
    };

    $.easyui.getDefaultParameters = function () {
        return getQueryForm$().serializeJson();      
    };

    function NotificationArea($container) {
        this.showProgressNotification = function ($progress, $isVisible) {
            $container.html("<span>Progress : " + $progress + " %</span>");

            /*if ($isVisible == false) {
                $container.fadeIn();
            }*/
        };

        this.showErrorNotification = function () {
            $container.removeAttr("class");
            $container.addClass("alert error pull-right");
            $container.html("<span>Upload error.</span>");
        };

        this.showSuccessNotification = function () {
            $container.removeAttr("class");
            $container.addClass("alert info pull-right");
            $container.html("<span>Uploaded successfully.</span>");
        };
    }


    function FileUpload() {
        this.guid = "";
        this.onUploadProgress = false;
        this.notificationObject = null;
        this.trackUrl = "";

        this.uploadSingleFile = function ($form, $guid, $url, $notificationObject, $trackUrl) {
            if ($form != null) {
                this.guid = $guid;
                this.trackUrl = $trackUrl;
                var trackTimer = setInterval(function () {
                    trackUploadProgress($trackUrl, $notificationObject, $guid);
                }, 3000);

                $form.ajaxSubmit({
                    url: $url,
                    data: {
                        guid: $guid
                    },
                    beforeSend: function () {
                        $notificationObject.showProgressNotification(0, false);
                    },
                    success: function (data) {
                        console.log("sukses");

                        if (data == true) {
                            clearTimeout(trackTimer);
                            $notificationObject.showSuccessNotification();
                        }
                        else {
                            $notificationObject.showErrorNotification();
                        }
                    },
                    error: function (xhr, ajaxOptions, error) {
                        $notificationObject.showErrorNotification();
                    },
                    complete: function () {
                        clearTimeout(trackTimer);
                    }
                });
            }
        };     
    }
    
    function trackUploadProgress($url, $notificationObject, $guid) {
        
        $.ajax({
            url: $url,
            type: "post",
            async: true,
            cache: false,
            data: {
                guid: $guid
            },
            success: function (data) {
                //console.log(i);
                //console.log(data);
                //i++;
                $notificationObject.showProgressNotification(data, true);
            }
        });
    }


    $.easyui.upload = function (uploadurl) {
        var gid = $.newGuid();
        var trackUrl = "TrackProgress";
        var index = uploadurl.lastIndexOf("/");
        if (index > 0)
        {
            trackUrl = uploadurl.substr(0, index + 1) + trackUrl;
        }
        var notificationArea = new NotificationArea($("#notification-area"));
        var fileUpload = new FileUpload();
        fileUpload.uploadSingleFile(getUpLoadForm$(), gid, uploadurl, notificationArea, trackUrl);
    };

    $.easyui.uploadDialog = function () {
        $('#upLoadDialog').dialog({
            title:"上传文件",
            open: true,
            closed: false,
            modal: false,
            height:150
        }).dialog('center');
    };
})(jQuery);

