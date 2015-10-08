(function ($) {
    //index页面对应的jQuery对象
    var $parent = parent.$;
    $.easyui = (function () {
        //获取弹出窗口数据集合
        function getDialogs() {
            var dialogs = $parent("#div_index_data").data("dialogs");
            dialogs = dialogs || [];
            if ($.isEmptyArray(dialogs))
                return [];
            return dialogs;
        }

        //获取当前弹出窗口数据
        function getCurrentDialog() {
            var dialogs = getDialogs();
            return dialogs[dialogs.length - 1] || {};
        }

        //获取当前弹出窗口Id
        function getCurrentDialogId() {
            return getCurrentDialog().id;
        }

        //添加弹出窗口数据,包括弹出窗口Id和jQuery对象
        function addDialog(id) {
            var data = getDialogs();
            data.push({ id: id, $this: $ });
            $parent("#div_index_data").data("dialogs", data);
        }

        //移除当前弹出窗口数据
        function removeCurrentDialog() {
            var dialogs = getDialogs();
            dialogs.pop();
        }

        return {
            //获取当前jQuery对象
            getCurrent$: function () {
                return getCurrentDialog().$this;
            },
            //显示全屏加载进度条
            showLoading: function () {
                var $body = $("body");
                $("<div id=\"div_loading\" class=\"datagrid-mask\"></div>").css({ zIndex: "99998", display: "block" }).appendTo($body);
                $("<div id=\"div_loading_msg\" class=\"datagrid-mask-msg\"></div>").html("处理中，请稍候...").appendTo($body).css({ zIndex: "99999", display: "block", left: "50%", fontSize: "12px" });
            },
            //移除全屏加载进度条
            removeLoading: function () {
                $("#div_loading").remove();
                $("#div_loading_msg").remove();
            },
            //格式化日期
            formatDate: function (value) {
                return $.formatIsoDate(value);
            },
            //格式化布尔值
            formatBool: function (value) {
                if (value === true)
                    return "是";
                return "否";
            },
            addIframeToTabs: function (tabsId, title, url, icon, closable) {
                ///	<summary>
                ///	为tabs添加iframe选项卡
                ///	</summary>
                ///	<param name="tabsId" type="String">
                ///	选项卡Id
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题，可以重复
                ///	</param>
                ///	<param name="url" type="String">
                ///	网址,必须唯一
                ///	</param>
                ///	<param name="icon" type="String">
                ///	图标class
                ///	</param>
                ///	<param name="closable" type="Bool">
                ///	是否允许关闭
                ///	</param>
                if (!title && !url)
                    return;
                var tabs = $('#' + tabsId);
                var index;
                var iframe = null;
                if (exists())
                    refresh();
                else
                    createTab();
                selectTab();

                //判断选项卡是否存在,根据url进行判断
                function exists() {
                    var allTabs = tabs.tabs("tabs");
                    for (index = 0; index < allTabs.length; index++) {                        
                        iframe = allTabs[index].find('iframe');
                        if (iframe.length == 0)
                            continue;
                        var sUrl = $.getUrlPath(iframe[0].src).substring(1);
                        if (decodeURI(sUrl) === decodeURI(url))
                            return true;
                    }
                    return false;
                }
                //刷新选项卡
                function refresh() {
                    iframe[0].contentWindow.location.href = url;
                }

                //创建选项卡
                function createTab() {
                    var content = '<div class="easyui-layout" data-options="fit:true"><iframe scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe><div>';
                    tabs.tabs('add', {
                        title: title,
                        closable: closable,
                        content: content,
                        iconCls: icon,
                        selected: 0
                    });
                }

                //选中选项卡
                function selectTab() {
                    tabs.tabs('select', index);                    
                }
            },
            refreshTabs: function (tabsId) {
                ///	<summary>
                ///	刷新选项卡
                ///	</summary>
                ///	<param name="tabsId" type="String">
                ///	选项卡Id
                ///	</param>
                var tabs = $('#' + tabsId);
                var tab = tabs.tabs('getSelected');
                var iframe = tab.find('iframe');
                if (iframe.length == 0)
                    return;
                iframe[0].contentWindow.location.href = iframe[0].contentWindow.location.href;
            },
            dialog: function (options) {
                ///	<summary>
                ///	弹出模态窗，解决在Iframe中无法全屏遮罩,
                /// 注意:仅支持url弹窗
                ///	</summary>
                ///	<param name="options" type="Object">
                ///  1. title:标题
                ///  2. url:网址
                ///  3. buttons:显示在窗口底部的按钮区域div的id
                ///  4. icon:图标class
                ///  5. width:宽度
                ///  6. height:高度
                ///  7. onInit:初始化事件，返回false跳出执行
                ///	</param>
                initOptions();
                if (!options.onInit(options))
                    return;
                var dialog = createDialow();
                show();
                addDialog(options.id);

                //初始化参数
                function initOptions() {
                    options = $.extend({
                        id: $.newGuid(""),
                        title: '',
                        url: '',
                        icon: '',
                        width: 800,
                        height: 360,
                        closed: false,
                        maximizable: true,
                        resizable: true,
                        cache: false,
                        modal: true,
                        buttons: '',
                        onInit: function() {
                            return true;
                        },
                        closeCallback: function () { }
                    }, options || {});
                }

                //创建窗口div
                function createDialow() {
                    return $parent("<div id='" + options.id + "'></div>").appendTo('body');
                }

                //弹出窗口
                function show() {
                    dialog.dialog({
                        title: options.title,
                        href: options.url,
                        width: options.width,
                        height: options.height,
                        closed: options.closed,
                        maximizable: options.maximizable,
                        resizable: options.resizable,
                        cache: options.cache,
                        modal: options.modal,
                        iconCls: options.icon,
                        onLoad: function () {
                            var win = $parent("#" + options.id).window("window");
                            $parent("#" + options.buttons).addClass("dialog-button").appendTo(win);
                        },
                        onClose: function () {
                            options.closeCallback();
                            $parent("#" + getCurrentDialogId()).dialog('destroy');
                            removeCurrentDialog();
                        }
                    });
                }
            },
            //关闭弹出窗口
            closeDialog: function () {
                var dialogId = getCurrentDialogId();
                if (!dialogId)
                    return;
                $parent('#' + dialogId).dialog('close');
            },
            showMenu: function (menuId, e) {
                ///	<summary>
                ///	显示上下文菜单
                ///	</summary>
                ///	<param name="menuId" type="String">
                ///	上下文菜单Id
                ///	</param>
                ///	<param name="e" type="Event">
                ///	事件
                ///	</param>
                e.preventDefault();
                var $menu = $('#' + menuId);
                $menu.menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
                return $menu;
            },
            topShow: function (msg, title) {
                ///	<summary>
                ///	在顶部显示消息
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                $.messager.show({
                    title: title || "信息",
                    msg: msg,
                    showType: 'slide',
                    style: {
                        right: '',
                        top: document.body.scrollTop + document.documentElement.scrollTop,
                        bottom: ''
                    }
                });
            },
            info: function (msg, title) {
                ///	<summary>
                ///	弹出信息框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                $.messager.alert(title || "信息", msg, 'info');
            },
            warn: function (msg, title) {
                ///	<summary>
                ///	弹出警告框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                $.messager.alert(title || "错误", msg, 'error');
            },
            confirm: function (msg, callback, title) {
                ///	<summary>
                ///	弹出确认框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	点击ok按钮后的回调函数
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                $.messager.confirm(title || "确认", msg, function (result) {
                    if (result)
                        callback();
                });
            }
        };
    })();
})(jQuery);

