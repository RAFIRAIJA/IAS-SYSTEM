<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ar_dynamicreport.aspx.cs" Inherits="pj_ar_report_ar_dynamicreport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/button/ButtonBack.gif"
           />
        <br />
        <%--<rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true"
            ZoomMode="FullPage" Height="100%" ID="ReportViewer1" AsyncRendering="false" runat="server">
        </rsweb:ReportViewer>--%>
        <rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true"
            ZoomMode="FullPage" Height="100%" ID="rptViewer" AsyncRendering="false" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
