<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormAddMoreReceipt.aspx.cs" Inherits="FormAddMoreReceipt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <form id="mpFORM" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

        <asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pnTOOLBAR" runat="server">  
                        <table border="0" cellpadding="3" cellspacing="3">
                            <tr>
                                <td valign="top"><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClick="btSaveRecord_Click" OnClientClick="return confirm('Save Record ?');" /></td>
                                <td valign="top"><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" /></td>            
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
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLADDRECEIPTCODE" runat="server" Text="Receipt Code" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTADDRECEIPTCODE" runat="server" Width="150px" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btCheck" runat="server" Width="100px" Text="Check" OnClick="btCheck_Click" />
                            </td>
                        </tr>
                    </table>
                    <div id="space"></div>
                    <table id="mlTABLEADDMORERECEIPT" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLRECEIPTCODE" runat="server" Text="Receipt Code" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTRECEIPT" runat="server" Width="150px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLDATE" runat="server" Text="Date" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTDATE" runat="server" Width="100" Enabled="false" BackColor="Gainsboro"></asp:TextBox>                                                                                                          
                                <input id="Button1" runat="server" onclick="displayCalendar(mlTXTDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                                <font color="blue">dd/mm/yyyy</font>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="mlTXTDATE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLRESITIKI" runat="server" Text="No. Resi TIKI" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTRESITIKI" runat="server" Width="150px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLNIKMESS" runat="server" Text="NIK Messanger" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTNIKMESS" runat="server" Width="150px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLNAMEMESS" runat="server" Text="Nama Messanger" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTNAMEMESS" runat="server" Width="150px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:25px">
                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <strong><asp:Label ID="mlLBLPRINTEDBY" runat="server" Text="Printed By" Width="150px"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLNIKUSER" runat="server" Text="NIK User" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTNIKUSER" runat="server" Width="200px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLUSERNAME" runat="server" Text="Nama User" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTUSERNAME" runat="server" Width="200px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="mlLBLMESSAGE" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>

                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>

    </form>

</body>
</html>
