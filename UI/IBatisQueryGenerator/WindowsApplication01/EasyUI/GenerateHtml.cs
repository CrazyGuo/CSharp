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
        //这个url比较特别，因为iframe链接默认带有area部分？
        public string QueryAndDownloadUrl = string.Empty;
        private string newLine = "\r\n";
        private string prefix = "   ";
        public string Index()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("@model " + Name + "Dto");
            builder.Append(newLine);
            builder.Append("@{");
            builder.Append(newLine);
            builder.Append(prefix + "var x = Html.EasyUi();");
            builder.Append(newLine);
            builder.Append("CrudURL urls = new CrudURL();");
            builder.Append(newLine);
            builder.Append("urls.AddUrl = \"" + Url + "/add\";");
            builder.Append(newLine);
            builder.Append("urls.EditUrl = \"" + Url + "/Edit\";");
            builder.Append(newLine);
            builder.Append("urls.DeleteUrl = \"" + Url + "/delete\";");
            builder.Append(newLine);
            builder.Append("urls.LookUrl = \"" + Url + "/look\";");
            builder.Append(newLine);
            builder.Append("urls.QueryUrl = \"" + QueryAndDownloadUrl + "/Query\";");
            builder.Append(newLine);
            //下载url
            builder.Append("urls.DownloadUrl = \"" + QueryAndDownloadUrl + "/Download\";");
            builder.Append(newLine);
            builder.Append("@section top {");
            builder.Append(newLine);
            builder.Append(prefix + "<div class=\"toolbar\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@(x.DialogButton(EasyUiEvent.AddOperation, urls.AddUrl).Icon(EasyUiEvent.AddIcon).Plain().DialogSize(420, 260))");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@(x.DialogButton(EasyUiEvent.EditOperation, urls.EditUrl).Id(EasyUiEvent.ButtonEdit).OnInit(EasyUiEvent.EditDialogEvent).Icon(EasyUiEvent.EditIcon).Plain().DialogSize(420, 260))");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@(x.LinkButton(EasyUiEvent.DeleteOperation).Icon(EasyUiEvent.DeleteIcon).Plain().Click(EasyUiEvent.DeleteDialogEvent, urls.DeleteUrl))");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@(x.DialogButton(EasyUiEvent.DetailOperation, urls.LookUrl).Id(EasyUiEvent.ButtonDetail).OnInit(EasyUiEvent.LookDialogEvent).Icon(EasyUiEvent.LookIcon).Plain().DialogSize(500, 240))");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "@(x.LinkButton(EasyUiEvent.RefreshOperation).Icon(EasyUiEvent.RefreshIcon).Plain().Click(EasyUiEvent.RefreshEvent))");
            builder.Append(newLine);
            //下载按钮
            builder.Append(prefix + prefix + "@(x.LinkButton(EasyUiEvent.DownloadOperation).Icon(EasyUiEvent.DownloadIcon).Plain().Click(EasyUiEvent.DownloadEvent, urls.DownloadUrl))");
            builder.Append(newLine);
            //上传按钮
            builder.Append(prefix + prefix + "@(x.LinkButton(EasyUiEvent.UploadOperation).Icon(EasyUiEvent.UploadIcon).Plain().Click(EasyUiEvent.ShowUploadDialogEvent))");
            builder.Append(newLine);
            //上传对话框
            builder.Append(prefix + prefix + "@Html.Partial(\"Parts/Upload\")");
            builder.Append(newLine);
            builder.Append(prefix + "</div>");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("@section content {");
            builder.Append(newLine);
            builder.Append(prefix + "@Html.Partial(EasyUiEvent.QueryFormUrl)");
            builder.Append(newLine);
            builder.Append(prefix + "@(x.Grid().GetDefaultValues().Toolbar().Url(urls.QueryUrl)");
            builder.Append(newLine);
            builder.Append(prefix + ".OnDblClickRow().OnRowContextMenu()");
            builder.Append(newLine);
            builder.Append(prefix + ".Columns(");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.GridColumn(InfraUtils.GetPropertyName<"+ Name + "Dto"+">(i => i.SportName), InfraUtils.GetDisplayName <"+ Name + "Dto"+">(i=>i.SportName), 100)");
            builder.Append(newLine);
            builder.Append(prefix + ")");
            builder.Append(newLine);
            builder.Append(prefix + ")");
            builder.Append(newLine);
            builder.Append(prefix + "@(x.Menu().Duration(600).Items(");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(EasyUiEvent.MenuItemEdit, EasyUiEvent.EditOperation, EasyUiEvent.EditIcon).Click(EasyUiEvent.MenuItemEditEvent),");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(EasyUiEvent.MenuItemDelete, EasyUiEvent.DeleteOperation, EasyUiEvent.DeleteIcon).Click(EasyUiEvent.MenuItemDeleteEvent, urls.DeleteUrl),");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "x.MenuItem(EasyUiEvent.MenuItemDetail, EasyUiEvent.DetailOperation, EasyUiEvent.LookIcon).Click(EasyUiEvent.MenuItemLookEvent)");
            builder.Append(newLine);
            builder.Append(prefix + "))");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
            builder.Append("}");
            builder.Append(newLine);
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
            builder.Append("<div id=\"divQuery\">");
            builder.Append(newLine);
            //添加折叠panel
            builder.Append("<div class=\"easyui-panel divQueryForm\" title=\"筛选条件\" data-options=\"collapsible:true,fit:true\" style=\"padding:3px;\">");
            builder.Append(newLine);
            builder.Append(prefix + "<form id=\"formQuery\" class=\"form\" >");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dt>@(InfraUtils.GetDisplayName<"+Name + "Query"+">(i => i.SportName))</dt>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "<dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + prefix + " @x.TextBox( t => t.Code )");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "</dd>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "</dl>");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "<span class=\"button\">");
            builder.Append(newLine);
            builder.Append(prefix + prefix + prefix + "@x.LinkButton(EasyUiEvent.QueryOperation).Width(70).Icon(EasyUiEvent.QueryIcon).Click(EasyUiEvent.QueryEvent)");
            builder.Append(newLine);
            builder.Append(prefix + prefix + "</span>");
            builder.Append(newLine);
            builder.Append(prefix + "</form>");
            builder.Append(newLine);
            builder.Append("</div>");
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
            builder.Append(prefix + prefix + prefix + "<dt>@(InfraUtils.GetDisplayName<"+Name+"Dto"+">(i => i.SportName))</dt>");
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
            builder.Append("@x.LinkButton(EasyUiEvent.SaveOperation).Icon(EasyUiEvent.OKIcon).Width(90).Click(EasyUiEvent.AddEvent)");
            builder.Append(newLine);
            builder.Append("@x.DialogButton(EasyUiEvent.CloseOperation).Icon(EasyUiEvent.CancelIcon).Width(90).CloseDialog()");
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
            builder.Append(prefix + prefix + prefix + "<dt>@(InfraUtils.GetDisplayName<"+Name+"Dto"+">(i => i.Id))</dt>");
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
            builder.Append("@x.LinkButton(EasyUiEvent.SaveOperation).Icon(EasyUiEvent.OKIcon).Width(90).Click(EasyUiEvent.AddEvent)");
            builder.Append(newLine);
            builder.Append("@x.DialogButton(EasyUiEvent.CloseOperation).Icon(EasyUiEvent.CancelIcon).Width(90).CloseDialog()");
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
