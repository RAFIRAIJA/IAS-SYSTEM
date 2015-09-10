<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ws_testingNavision.aspx.cs" Inherits="pj_WS_ws_testingNavision" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="~/script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="~/script/calendar.js"></script>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>


    <form id="frm_WSNav2013" runat="server">
        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnTOOLBAR" runat="server" Width="100%">  
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr>
                    <td>
                        <b><asp:Label id="mlTITLE" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="mlSYSCODE" runat="server" Visible="false"/>
                    </td>
                </tr>  
                <tr>
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" OnClick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" style="height: 20px" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />&nbsp;    
                        <%--<asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" ImageUrl="~/images/toolbar/print.jpg" runat="server" Visible="false" OnClick="btPrintRecord_Click" />--%>    

                    </td>
                </tr>                      
            </table>
        <hr />
        <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Entity ID</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlEntity">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="ISS">ISS</asp:ListItem>
                            <asp:ListItem Value="IFS">IFS</asp:ListItem>
                            <asp:ListItem Value="IPM">IPM</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ErrorMessage="Select Entity...Please..." ControlToValidate="ddlEntity"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdganjil" colspan="2"></td>
                </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pnlSearch" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">SEARCH BY</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Search By</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.inv_receiptcode">Receipt Code</asp:ListItem>
                            <asp:ListItem Value="a.CustName">Customer Name</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center"></td>
                    <td class="tdganjil" colspan="2" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />&nbsp;                      
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGrid" Width="100%">
            <table runat="server" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1"                             
                                >
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>         
                                    <asp:BoundColumn HeaderText ="Customer" ></asp:BoundColumn>                                                      
                                </Columns>
                            </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</asp:Content>

