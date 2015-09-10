<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormEditReceipt.aspx.cs" Inherits="FormEditReceipt" %>
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

        //<!--
        function popup(url,nama) 
        {
         params  = 'width='+screen.width;
         params += ', height='+screen.height;
         params += ', top=0, left=0'
         params += ', fullscreen=yes';

         newwin=window.open(url,nama, params);
         if (window.focus) {newwin.focus()}
         return false;
        }
        // -->

        function pop_up(url, nama, wi, he) {
            var left = (screen.width / 2) - (wi / 2);
            var top = (screen.height / 2) - (he / 2);
            var newwin;
            newwin = window.open(url, nama, 'width=' + wi + ', height=' + he + ', top=' + top + ', left=' + left + ',scrollbars=yes, resizable=no');
            if (window.focus)
            { newwin.focus() }
            return false;
        }
    </script>
</head>
<body onunload ="javascript:window.opener.location.href = window.opener.location.href;window.close()">
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
                                <td valign="top"><asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" ImageUrl="~/images/toolbar/print.jpg" runat="server" /></td>
                                <%--<td valign="top"><asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" ImageUrl="~/images/toolbar/print.jpg" runat="server" OnClientClick="javascript:popup('../pj_delinv/FormPrint.aspx?mpFP=1&data='+ Session["FormReceiptData"] +'&userid='+ Session["mgUSERID"] +'&name='+ Session["mgNAME"] +','FormPrint')"/></td>
                                OnClick="btPrintRecord_Click"--%>
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
                                <asp:TextBox ID="mlTXTDATE" runat="server" Width="100"></asp:TextBox>                                                                                                          
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
                                <asp:TextBox ID="mlTXTRESITIKI" runat="server" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLNIKMESS" runat="server" Text="NIK Messanger" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="mlDDLNIKMESS" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged = "mlDDLNIKMESS_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mlLBLNAMEMESS" runat="server" Text="Nama Messanger" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="mlTXTNAMEMESS" runat="server" Width="150px"></asp:TextBox>
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
                    </table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <div id="space"></div>
                    <asp:Panel ID="mlPNLGRID" runat="server">
                        <asp:DataGrid runat="server" ID="mlDGEDITRECEIPT" 
                        CssClass="Grid"
                        autogeneratecolumns="false"
                        OnPageIndexChanged="mlDGEDITRECEIPT_PageIndexChanged"
                        OnItemCommand="mlDGEDITRECEIPT_ItemCommand">
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <EditItemStyle  CssClass="GridItem" />
                        <PagerStyle  CssClass="GridItem" />
                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                        <Columns>

                            <asp:templatecolumn headertext="No">
                                <ItemTemplate>        
                                    <%#Container.ItemIndex + 1%>
                                </ItemTemplate>        
                            </asp:templatecolumn>
                   
                            <%--<asp:templatecolumn headertext="Invoice No">
                                <ItemTemplate>        
                                    <asp:hyperlink  Target="_blank"  runat="server" id="Hyperlink2" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("InvNo")) %>' text='<%# Eval("InvNo") %>'></asp:hyperlink>
                                </ItemTemplate>        
                            </asp:templatecolumn>--%>
                            
                            <asp:BoundColumn HeaderText="Company" DataField="CompanyCode" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice No" DataField="InvNo" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Name" DataField="InvCustName" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Amount" DataField="InvAmount" DataFormatString ="{0:#,##0}" ItemStyle-HorizontalAlign="right" Visible="true"></asp:BoundColumn>

                            <asp:BoundColumn HeaderText="Receipt Code" DataField="InvReceiptCode" Visible="false" ></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Status" DataField="InvStatus" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Description" DataField="InvDesc" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Prepared For" DataField="InvPreparedForDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Delivered" DataField="InvDeliveredDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Resi TIKI" DataField="InvResiTiki" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger NIK" DataField="InvCodeMess" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger Name" DataField="InvMessName" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User NIK" DataField="UserID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User Name" DataField="UserName" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Code" DataField="InvCustCode" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice Date" DataField="InvDate" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Branch" DataField="InvBranch" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Proceeds" DataField="InvProceedsDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Returned" DataField="InvReturnedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Done" DataField="InvDoneDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Penerima" DataField="InvCustPenerima" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Site Card" DataField="InvSiteCard" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Product Offering" DataField="InvProdOffer" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="OCM" DataField="InvOCM" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="FCM" DataField="InvFCM" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Collector" DataField="InvCollector" Visible="false"></asp:BoundColumn>
                            
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton id="mlLINKBUTTON" runat="server" CommandArgument="InvNo" CommandName="Batal" Text="Batal"/>
                                    <%--<%# Eval("InvNo") %>--%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>

    </form>
</body>
</html>
