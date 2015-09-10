<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_allowance_viewer.aspx.cs" Inherits="pj_hr_report_hr_allowance_viewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <%--<link href="../../script/Style.css" type="text/css" rel="stylesheet" />--%>
    <script src="../../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../../Include/JavaScript/Eloan.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:ImageButton ID="imbBack" runat="server" ImageUrl="../../Images/button/ButtonBack.gif"
            OnClick="imbBack_Click" />
        <br />
                
        <rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true"
            ZoomMode="FullPage" Height="100%" ID="ReportViewer1" AsyncRendering="false" runat="server"></rsweb:ReportViewer>


    </div>
    </form>
</body>
</html>
