using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBatisQueryGenerator.EasyUI
{
    public class GenerateHtml
    {
        public string Name = string.Empty;
        public string Url = string.Empty;
        private string newLine = "\r\n";
        private string prefix = "   ";
        public string Index()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("@model " + Name + "Dto");
            builder.Append(newLine);
            builder.Append("@{");
            builder.Append(newLine);
            builder.Append(prefix + "var x = @Html.EasyUi();");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("@section head {");
            builder.Append(newLine);
            builder.Append(prefix + "<script type=\"text/javascript\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "$(function () {");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "$.easyui.deleteUrl = \""+Url+"/delete\";");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "});");
            builder.Append(newLine);
            builder.Append(prefix + "</script>");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append(newLine);
            builder.Append("@section top {");
            builder.Append(newLine);
            builder.Append(prefix + "<div class=\"toolbar\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@x.DialogButton(\"添加\", \""+Url+"/add\").Icon(\"icon-add\").Plain().DialogSize(420, 260)");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@x.DialogButton(\"编辑\", \"" + Url + "/Edit\").Id(\"btnEdit\").OnInit(\"$.easyui.fnInitEdit\").Icon(\"icon-edit\").Plain().DialogSize(420, 260)");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@x.LinkButton(\"删除\").Icon(\"icon-delete\").Plain().Click(\"$.easyui.delete()\")");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@x.DialogButton(\"查看详细\", \""+Url+"/look\").Id(\"btnLook\").OnInit(\"$.easyui.fnInitLook\").Icon(\"icon-look\").Plain().DialogSize(500, 240)");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@x.LinkButton(\"刷 新\").Icon(\"icon-refresh\").Plain().Click(\"$.easyui.refresh()\")");
            builder.Append(newLine);
            builder.Append(prefix + "</div>");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("@section content {");
            builder.Append(newLine);
            builder.Append(prefix + "@Html.Partial(\"Parts/QueryForm\")");
            builder.Append(newLine);
            builder.Append(prefix + "@(x.Grid(\"grid\").Toolbar(\"divQuery\").Url(\"" + Url + "/Query\")");
            builder.Append(newLine);
            builder.Append(prefix + ".OnDblClickRow(\"$.easyui.showEditDialog\").OnRowContextMenu(\"$.easyui.fnRightClickGridRow\")");
            builder.Append(newLine);
            builder.Append(prefix + ".Columns(");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.GridColumn(\"FiledName\", \"ShowName\", 100),");
            builder.Append(newLine);
            builder.Append(prefix + ")");
            builder.Append(newLine);
            builder.Append(prefix + ")");
            builder.Append(newLine);
            builder.Append(prefix + "@x.Menu(\"menuGrid\").Click(\"$.easyui.fnClickGridMenu\").Duration(600).Items(");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(\"menuItem_Edit\", \"编 辑\", \"icon-edit\"),");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(\"menuItem_Delete\", \"删 除\", \"icon-delete\"),");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(\"menuItem_Look\", \"查看详细\", \"icon-look\")");
            builder.Append(newLine);
            builder.Append(prefix + ")");
            builder.Append(newLine);
            builder.Append("}");
            return builder.ToString();
        }
    
        public string QueryForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("@model "+Name + "Query");
            builder.Append(newLine);
            builder.Append("@{");
            builder.Append(newLine);
            builder.Append(prefix + "var x = @Html.EasyUi();");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("<div id=\"divQuery\" class=\"divQueryForm\">");
            builder.Append(newLine);
            builder.Append(prefix + "<form id=\"formQuery\" class=\"form\" action=\"" + Url + "/Query\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dt>@Html.LabelFor( t =>  )</dt>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + prefix + " @x.TextBox( t => t.Code )");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "</dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<span class=\"button\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix +"@x.LinkButton( \"查 询\" ).Width( 70 ).Icon( \"icon-search\" ).Click( \"$.easyui.query()\" )");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "</span>");
            builder.Append(newLine);
            builder.Append(prefix + "</form>");
            builder.Append(newLine);
            builder.Append("</div>");
            return builder.ToString();
        }
        
        public string AddForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("@model " + Name + "Dto");
            builder.Append(newLine);
            builder.Append("@{");
            builder.Append(newLine);
            builder.Append(prefix + "var x = @Html.EasyUi();");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("<div class=\"divForm\">");
            builder.Append(newLine);
            builder.Append(prefix + "<form id=\"form\" class=\"form\" action=\"" + Url + "/Save\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dt>@Html.LabelFor( t =>  )</dt>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + prefix + " @x.TextBox( t => t.Code )");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "</dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "</dl>");
            builder.Append(newLine);
            builder.Append(prefix + "</form>");
            builder.Append(newLine);
            builder.Append("</div>");
            builder.Append("<div id=\"dialogButtons\">");
            builder.Append(newLine);
            builder.Append("@x.LinkButton(\"保 存\").Icon(\"icon-ok\").Width(90).Click(\"$.easyui.submit()\")");
            builder.Append(newLine);
            builder.Append("@x.DialogButton(\"关 闭\").Icon(\"icon-cancel\").Width(90).CloseDialog()");
            builder.Append(newLine);
            builder.Append("</div>");
            return builder.ToString();
        }

        public string UpdateForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("@model " + Name + "Dto");
            builder.Append(newLine);
            builder.Append("@{");
            builder.Append(newLine);
            builder.Append(prefix + "var x = @Html.EasyUi();");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("<div class=\"divForm\">");
            builder.Append(newLine);
            builder.Append(prefix + "<form id=\"form\" class=\"form\" action=\"" + Url + "/Update\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dt>@Html.LabelFor( t =>  )</dt>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + prefix + " @x.TextBox( t => t.Code )");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "</dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "</dl>");
            builder.Append(newLine);
            builder.Append(prefix + "</form>");
            builder.Append(newLine);
            builder.Append("</div>");
            builder.Append("<div id=\"dialogButtons\">");
            builder.Append(newLine);
            builder.Append("@x.LinkButton(\"保 存\").Icon(\"icon-ok\").Width(90).Click(\"$.easyui.submit()\")");
            builder.Append(newLine);
            builder.Append("@x.DialogButton(\"关 闭\").Icon(\"icon-cancel\").Width(90).CloseDialog()");
            builder.Append(newLine);
            builder.Append("</div>");
            return builder.ToString();
        }

        public void s()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
            builder.Append("");
        }
    }
}
