<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPrintRDLC.aspx.cs" Inherits="FormPrintRDLC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="../script/calendar.js"></script>
    <link rel="stylesheet" href="script/style-page.css" type="text/css" media="all" />

    <script type="text/javascript" language="Javascript">
        //<!-- hide script from older browsers
        function openwindow(url, nama, width, height) {
            OpenWin = this.open(url, nama);
            if (OpenWin != null) {
                if (OpenWin.opener == null)
                    OpenWin.opener = self;
            }
            OpenWin.focus();
        }
        // End hiding script-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" EnablePartialRendering="true" ID="ToolkitScriptManager1" />
        <%----%>

        <ajax:UpdatePanel ID = "upPrint" runat="server" EnableViewState="true" UpdateMode="Conditional" >
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="btPrint" runat="server" CommandName="Print" 
                                ImageAlign="Middle" ImageUrl="~/images/toolbar/print.jpg" 
                                OnClick="btPrint_Click" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer runat="server" ID="ReportViewer1" Height="100%" Width="100%" SizeToReportContent="True"
                                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                                WaitMessageFont-Size="14pt" InteractiveDeviceInfos="(Collection)" >
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <%--<Triggers>
                <ajax:AsyncPostBackTrigger ControlID="ReportViewer1" EventName="ReportViewer1_ReportRefresh" />
            </Triggers>--%>
        </ajax:UpdatePanel>

    </form>
</body>
</html>
