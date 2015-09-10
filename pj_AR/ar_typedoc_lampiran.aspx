<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_typedoc_lampiran.aspx.cs" Inherits="pj_ar_ar_typedoc_lampiran" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

    <form id="frmtypedoclampiran" runat="server">
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
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" 
                            ImageUrl="~/images/toolbar/new.jpg" runat="server" OnClick="btNewRecord_Click" 
                             />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" style="height: 20px" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" 
                            ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" 
                             />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />    
                    </td>
                </tr>                      
            </table>
        <hr />
            <table id="Table1" runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Entity ID</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlEntity">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="ISS">ISS</asp:ListItem>
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
                            <asp:ListItem Value="TypeDokumenID">Type Document Code</asp:ListItem>
                            <asp:ListItem Value="TypeDokumenName">Type Document Name</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center"></td>
                    <td class="tdganjil" colspan="2" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" />&nbsp;
                    </td>                     
                </tr>
            </table> 
        </asp:Panel> 
        <asp:Panel ID="pnlListData" runat="server" Width="100%"  Visible ="true" >
                <table id="Table8" runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">LIST TYPE DOCUMENT</td>
                        <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                    </table>
                <table id="Table9" runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1"                             
                                onitemdatabound="dgListData_ItemDataBound" 
                                OnItemCommand="dgListData_ItemCommand"
                                >
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>         
                                    <asp:TemplateColumn HeaderText="EDIT" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/images/toolbar/edit.jpg" ToolTip="Edit Record" CommandName="edit" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DELETE" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbdelete" runat="server" ImageUrl="~/images/toolbar/delete.jpg" ToolTip="Delete Record" CommandName="delete"/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                                     
                                    <asp:TemplateColumn HeaderText="TYPE DOCUMENT CODE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeDocID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_typedoc_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="TYPE DOCUMENT NAME" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeDocName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_typedoc_name") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                        
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <uc2:ucPaging runat="server" id="pagingDocType" 
                                OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                        </td>
                    </tr>               
                </table>
        </asp:Panel>
        <asp:Panel ID="pnlInputData" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">INPUT DATA TYPE DOCUMENT</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">                
               <tr>
                    <td class="tdganjil" width="15%">
                        Type Document Name
                    </td>
                    <td class="tdganjil" width="35%">
                        <asp:TextBox runat="server" ID="txtDocTypeName" CssClass="inptype" Width="250px"></asp:TextBox>
                    </td>
                    <td class="tdganjil" colspan="2"></td>
                </tr>
                
                
            </table>
        </asp:Panel> 
    </form>

</asp:Content>

