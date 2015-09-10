<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="Template.aspx.cs" Inherits="Template_Template" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <form runat="server">
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
                            ImageUrl="~/images/toolbar/new.jpg" runat="server" 
                            onclick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" 
                            ImageUrl="~/images/toolbar/find.jpg" runat="server" 
                            onclick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />    
                    </td>
                </tr>                      
            </table>
        <hr />
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
                    <td class="tdganjil" width="17%">Search  By</td>
                    <td class="tdganjil" width="33%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>                        
                            <asp:ListItem Value="Allow_id">ALLOWANCE ID</asp:ListItem>
                            <asp:ListItem Value="sitecard">SITECARD</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox ID="txtSearchBy" runat="server" Width="200px" CssClass="inptype"></asp:TextBox>                    
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="40%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" onclick="btnSearch_Click" />&nbsp;
                        <asp:ImageButton ID="btnReset" runat="server" 
                            ImageUrl="../Images/button/buttonReset.gif" onclick="btnReset_Click" />
                    </td>                    
                </tr>
            </table>   
            <hr />         
        </asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server" Width="100%" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">HR ALLOWANCE LIST</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1" 
                            onsortcommand="dgListData_SortCommand" 
                            onitemdatabound="dgListData_ItemDataBound" OnItemCommand="dgListData_ItemCommand">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="DATA 1 " ItemStyle-HorizontalAlign="Left" SortExpression="Allow_id" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlAllowID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_id") %>' NavigateUrl="#"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA 2" ItemStyle-HorizontalAlign="Center" SortExpression="transaction_date" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblAllowDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"transaction_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="EDIT" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="../Images/button/IconEdit.gif" CommandName="Edit" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DELETE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="../Images/button/IconDelete.gif" CommandName="Delete" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="REVIEW" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbReview" runat="server" ImageUrl="../Images/button/checklist.gif" CommandName="Review" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="AUTHORIZE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbAuth" runat="server" ImageUrl="../Images/button/iconapproval.gif" CommandName="Authorize" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingAllow" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="10"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInput" runat="server" Width="100%" Visible="false" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">HR ALLOWANCE INPUT</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
             <table runat="server" cellSpacing="1" cellPadding="2" width="100%" border="0" >
                <tr>
                    <td class="tdganjil" width="20%">
                        Entity ID
                    </td>
                    <td class="tdganjil" width="30%">
                        <asp:DropDownList id="ddlEntity" runat="server" CssClass="inptypemandatory">
                            <asp:ListItem Value="ISS" Selected="True">ISS</asp:ListItem>
                        </asp:DropDownList>                                                
                    </td>
                    <td class="tdganjil" width="20%">
                        Data 1
                    </td>
                    <td class="tdganjil">
                        <asp:Label runat="server" id="Data1" ForeColor="OrangeRed" Font-Bold="true">[Auto Generate]</asp:Label>                           
                    </td>
                </tr>                
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlSave" runat="server" Visible="false" Width="100%">
            <table runat="server" cellSpacing="1" cellPadding="2" width="60%" border="0">
                    <tr>
                        <td width="50%"  class="tdganjil" align="right">                            
                        </td>                        
                        <td class="tdganjil" align="right">                            
                            <asp:ImageButton runat="server" ID="imbBack" CausesValidation="false"
                                ImageUrl="~/images/button/buttonBack.gif" onclick="imbBack_Click" />&nbsp;
                            <asp:ImageButton runat="server" ID="imbSave" 
                                ImageUrl="~/images/button/buttonsave.gif" onclick="imbSave_Click" />
                        </td>
                    </tr>
                </table>
        </asp:Panel>


    </form>
</asp:Content>

