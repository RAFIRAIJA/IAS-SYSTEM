<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="gl_rpt_profitandloss_cb1.aspx.cs" Inherits="pj_GL_gl_rpt_profitandloss_cb1" %>

<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

    <form id="frm_gl_pnl_cb1" runat="server">
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
                    <td>
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />&nbsp;    
                        <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" visible="false"/>&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" visible="false"/>&nbsp;
                        <asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" ImageUrl="~/images/toolbar/print.jpg" runat="server" Visible="false" />    
                    </td>
                </tr>                      
            </table>
        <hr />        
        </asp:Panel>
        <asp:Panel ID="pnlSearch" runat="server" Width="100%" Visible="true" >
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr>
                    <td class="tdganjil" width="15%">
                        Periode
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="UcStartDate" />&nbsp;&nbsp;s/d&nbsp;&nbsp;
                        <uc1:ValidDate runat="server" ID="UcEndDate" />
                    </td>
                    <td class="tdganjil" width="25%"></td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Branch
                    </td>
                    <td class="tdganjil" >
                        <asp:dropdownlist runat="server" id="ddlBranch"></asp:dropdownlist>
                    </td>
                    <td class="tdganjil"></td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        SiteCard
                    </td>
                    <td class="tdganjil" >
                        <asp:dropdownlist runat="server" id="ddlSitecard"></asp:dropdownlist>
                    </td>
                    <td class="tdganjil"></td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        PIC User
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox CssClass="inptype" runat="server" ID="txtPIC" Width="120px"></asp:TextBox>&nbsp;
                        <asp:ImageButton runat="server" ID="imbRefresh" ImageUrl="~/images/toolbar/autocomplete.jpg" />&nbsp;&nbsp;-&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblNamaPIC"></asp:Label>
                    </td>
                    <td class="tdganjil"></td>
                </tr>
            </table>
            <hr />
            <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="true" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
                            onitemdatabound="dgListData_ItemDataBound" >
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="COA" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOA" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="COA DESC" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOADesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COA_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="Total Amount" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="% ALL" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpercentageAll" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "percentage_All") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                                
            </table>
        </asp:Panel>
    </form>

</asp:Content>

