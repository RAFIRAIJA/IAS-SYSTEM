<%@ Page Title="" Language="C#" MasterPageFile="~/pj_delinv/MasterInternCS.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="pj_delinv_Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <meta content="BlendTrans(Duration=0)" http-equiv="Page-Exit" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
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

        var popup;
        function SelectData(url) {
            popup = window.open(url, "Popup", "width=300,height=200");
            popup.focus();
            return false
        }

        function myConfirm(message) {
            var start = Number(new Date());
            var result = confirm(message);
            var end = Number(new Date());
            return (end < (start + 10) || result == true);
        }
        
    </script>

    <form id="mpFORM" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

        <asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pnTOOLBAR" runat="server">  
                        <table border="0" cellpadding="3" cellspacing="3">
                            <tr>
                                <td valign="top"><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
                                <td valign="top"><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
                            </tr>               
                        </table>
                        <hr />
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <div id="headline-report"></div>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    
                    <asp:Panel ID="mlPNLGRID" runat="server" ScrollBars="Auto" Width="1020px">      
                        <asp:Panel ID ="pnlSEARCH" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="mlLBLREPORTNAME" runat="server" Text="Report"></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td>
                                        <asp:Label ID="mlLBLTITIK" runat="server" Text=" : "></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="mlDDLREPORTNAME" runat="server" AutoPostBack="true" OnSelectedIndexChanged="mlDDLREPORTNAME_SelectedIndexChanged" >
                                            <asp:ListItem Text="Summary" Value="InvDeliverySummary.rdlc" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Summary Per Branch" Value="InvDeliverySummaryBranch.rdlc"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="mlLBLTANGGALFROM" runat="server" Text="From"></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td>
                                        <asp:Label ID="mlLBLTITIK1" runat="server" Text=" : "></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td>
                                        <asp:TextBox ID="mlTXTTANGGALFROM" runat="server"></asp:TextBox>
                                        <%--<input id="Button1" runat="server" onclick="displayCalendar(mlTXTTANGGALFROM,'yyyy-MM-dd',this)" type="button" value="D" style="background-color:Yellow " />--%>
                                        <asp:ImageButton ID="imgCalendar" ImageUrl="~/images/project/Calendar_scheduleHS.png" Width="25" Height="25" runat="server" />
                                        <font color="blue">dd/mm/yyyy</font>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="mlTXTTANGGALFROM" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" />
                                    </td>
                                    <td>
                                        <asp:Label ID="mlLBLTANGGALTO" runat="server" Text="To"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="mlLBLTITIK2" runat="server" Text=" : "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="mlTXTTANGGALTO" runat="server"></asp:TextBox>
                                        <%--<input id="Button2" runat="server" onclick="displayCalendar(mlTXTTANGGALTO,'yyyy-MM-dd',this)" type="button" value="D" style="background-color:Yellow " />--%>
                                        <asp:ImageButton ID="imgCalendar1" ImageUrl="~/images/project/Calendar_scheduleHS.png" Width="25" Height="25" runat="server" />
                                        <font color="blue">dd/mm/yyyy</font>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="mlTXTTANGGALTO" Format="dd/MM/yyyy" PopupButtonID="imgCalendar1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6"><div id="space"></div></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="mlLBLBRANCH" runat="server" Text="Branch"></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td>
                                        <asp:Label ID="mlLBLTITIK3" runat="server" Text=" : "></asp:Label>
                                        &nbsp &nbsp
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="mlDDLBRANCH" runat="server" >
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                        <table id="mlTABLEBRANCH" runat="server">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="mlLBLBRANCH" runat="server" Text="Branch"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="mlDDLBRANCH" runat="server" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btReport" runat="server" ImageUrl="~/images/toolbar/autocomplete.jpg" OnClick ="btReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <div id="space"></div>
                        <div id="space"></div>
                        <div id="space"></div>
                        <div id="space"></div>
                        <div id="space"></div>
                        <div id="space"></div>

                        <ajax:UpdatePanel ID = "upPrint" runat="server" EnableViewState="true" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <table>
                                    <%--<tr>
                                        <td align="center">
                                            <asp:ImageButton ID="btPrint" runat="server" CommandName="Print" 
                                                ImageAlign="Middle" ImageUrl="~/images/toolbar/print.jpg" 
                                                OnClick="btPrint_Click" Visible="False" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <rsweb:ReportViewer runat="server" ID="ReportViewer1" Height="1000" Width="1000" SizeToReportContent="True"
                                                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                                                WaitMessageFont-Size="14pt" InteractiveDeviceInfos="(Collection)" >
                                            </rsweb:ReportViewer>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:UpdatePanel>

                    </asp:Panel>

                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                </asp:TableCell>
            </asp:TableRow>
            
        </asp:Table>
    </form>
</asp:Content>

