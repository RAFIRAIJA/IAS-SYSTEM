<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormRangeDate.aspx.cs" Inherits="FormRangeDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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

        function SetRangeDate() {
            if (window.opener != null && !window.opener.closed) {
                var hidStatus = window.opener.document.getElementById("ctl00_mpCONTENT_mlHIDESTATUS");
                hidStatus.value = document.getElementById("mlDDLSTATUS").value;
                var hidDateFrom = window.opener.document.getElementById("ctl00_mpCONTENT_mlHIDEDATEFROM");
                hidDateFrom.value = document.getElementById("mlTXTFROMDATE").value;
                var hidDateTo = window.opener.document.getElementById("ctl00_mpCONTENT_mlHIDEDATETO");
                hidDateTo.value = document.getElementById("mlTXTTODATE").value;
                var hidFormRangeDate = window.opener.document.getElementById("ctl00_mpCONTENT_mlFORMRANGEDATE");
                hidFormRangeDate.value = 'true';
            }
            //window.opener.location.href = window.opener.location.href;
            window.close();
        }

//        function PassValues() {
//            window.opener.document.forms(0).submit();
//            self.close();
//        }

    </script>
</head>
<body>
    <form id="mpFORM" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

        <asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
            <asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
            
            <asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLSTATUS" runat="server" Text="Status" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="mlDDLSTATUS" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLCOMPCODE" runat="server" Text="Company" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="mlDDLCOMPCODE" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLFROMDATE" runat="server" Text="Date From" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTFROMDATE" runat="server" Width="100"></asp:TextBox>                                                                                                          
                                <input id="Button1" runat="server" onclick="displayCalendar(mlTXTFROMDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                                <font color="blue">dd/mm/yyyy</font>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="mlTXTFROMDATE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLTODATE" runat="server" Text="Date To" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTTODATE" runat="server" Width="100"></asp:TextBox>                                                                                                          
                                <input id="Button2" runat="server" onclick="displayCalendar(mlTXTTODATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                                <font color="blue">dd/mm/yyyy</font>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="mlTXTTODATE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btDone" runat="server" Text="DONE" Height="25px" Width="125px" OnClick="btDone_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
