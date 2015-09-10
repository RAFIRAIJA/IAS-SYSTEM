<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormConfirmDone.aspx.cs" Inherits="FormConfirmDone" %>
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
                                <asp:DropDownList ID="mlDDLSTATUS" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLCONFIRMDATE" runat="server" Text="Date From" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTCONFIRMDATE" runat="server" Width="100"></asp:TextBox>                                                                                                          
                                <input id="Button1" runat="server" onclick="displayCalendar(mlTXTCONFIRMDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                                <font color="blue">dd/mm/yyyy</font>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="mlTXTCONFIRMDATE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLPENERIMA" runat="server" Text="Penerima" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTPENERIMA" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLDESCR" runat="server" Text="Description" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTDESCR" runat="server" Width="200px" TextMode="MultiLine" MaxLength="1024"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
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
