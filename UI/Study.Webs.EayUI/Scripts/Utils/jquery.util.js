(function ($) {
    //获取Url中的域名部分
    $.getUrlDomain = function() {
        return window.location.protocol + "//" + window.location.host;
    }
    //获取Url中的路径部分
    $.getUrlPath = function(url) {
        return url.trimStart($.getUrlDomain());
    }
    $.joinUrl = function(url, param) {
        ///	<summary>
        ///	连接url与参数
        ///	</summary>
        ///	<param name="url" type="String">
        ///	 Url
        ///	</param>
        ///	<param name="param" type="String">
        ///	 参数
        ///	</param>
        ///	<returns type="String" />
        if (url.contains("?"))
            return url + "&" + param;
        return url + "?" + param;
    }
    $.newGuid = function (separator) {
        ///	<summary>
        ///	生成Guid
        ///	</summary>
        ///	<param name="separator" type="Object">
        ///	 分隔符，默认为"-"
        ///	</param>
        ///	<returns type="String" />
        if (separator === undefined)
            separator = "-";
        if (separator == null)
            separator = "";
        var section = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return (section() + section() + separator + section() + separator + section() + separator + section() + separator + section() + section() + section()).toString();
    }
    $.toNumber = function (value, precision, isTruncate) {
        ///	<summary>
        ///	数值转换
        ///	</summary>
        ///	<param name="value" type="Object">
        ///	 输入值，一般为字符串
        ///	</param>
        ///	<param name="precision" type="int">
        ///	 数值精度，即小数位数，可选值为0-20
        ///	</param>
        ///	<param name="isTruncate" type="bool">
        ///	 是否截断，传入true，则按精度截断，否则四舍五入
        ///	</param>
        ///	<returns type="Number" />
        if( !$.isNumeric( value )  )
            return 0;
        if (!isTruncate && (precision === undefined || precision === null))
            return parseFloat(value);
        var result;
        if (isTruncate === true) {
            result = value.substring(0, value.indexOf(".") + parseInt(precision) + 1);
            return parseFloat(result);
        }
        return parseFloat(parseFloat(value).toFixed(precision) );
    }
    $.isNumber = function (value) {
        ///	<summary>
        ///	判断是否Number类型
        ///	</summary>
        ///	<param name="value" type="Object">
        ///	 数值
        ///	</param>
        ///	<returns type="bool" />
        return typeof value === "number";
    }
    $.isEmptyArray = function (value) {
        ///	<summary>
        ///	判断是否为空数组[]
        ///	</summary>
        ///	<param name="value" type="Object">
        ///	 数组
        ///	</param>
        ///	<returns type="bool" />
        return $.isArray(value) && value.length === 0;
    }
    $.parseIsoDate = function (value) {
        ///	<summary>
        ///	转换Iso日期
        ///	</summary>
        ///	<param name="value" type="String">
        ///	 Iso日期字符串
        ///	</param>
        ///	<returns type="Date" />
        var parts = value.match(/\d+/g);
        return new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    }
    $.formatIsoDate = function (value) {
        ///	<summary>
        ///	格式化Iso日期
        ///	</summary>
        ///	<param name="value" type="String">
        ///	 Iso日期
        ///	</param>
        ///	<returns type="String" />
        var date = $.parseIsoDate(value);
        if (!date)
            return "";
        return date.format("yyyy-MM-dd HH:mm");
    }
    //将表单序列化为json
    $.fn.serializeJson = function () {
        var result = {};
        var array = this.serializeArray();
        $(array).each(function () {
            if (result[this.name]) {
                if ($.isArray(result[this.name])) {
                    result[this.name].push(this.value);
                } else {
                    result[this.name] = [result[this.name], this.value];
                }
            } else {
                result[this.name] = this.value;
            }
        });
        return result;
    };
})(jQuery);