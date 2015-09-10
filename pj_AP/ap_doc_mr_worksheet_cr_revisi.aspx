<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="ap_doc_mr_worksheet_cr_revisi.aspx.vb" Inherits="pj_AP_ap_doc_mr_worksheet_cr_revisi" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table runat="server" width="100%" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <td>
                    <asp:Label runat="server" ID="mlMESSAGE" ForeColor="Violet" Font-Italic ="true" ></asp:Label>
                </td>
            </tr>
        </table>

        <asp:Panel runat="server" Width="100%" ID="pnlReport">
            <div>
                <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true" 
                    ZoomMode="FullPage" Height="100%" ID="rptViewer" AsyncRendering="false" runat="server">
                </rsweb:ReportViewer>
            </div>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>


