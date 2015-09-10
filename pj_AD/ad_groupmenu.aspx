<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ad_groupmenu.aspx.cs" Inherits="pj_ad_ad_groupmenu" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />

    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>
    <form id="frmAllowance" runat="server">
        <table runat="server" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0">
            <tr>
                <td class="tdganjil" >
                    <asp:Label ID="mlMessage" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlTOOLBAR" runat="server" Width="100%" Visible="true">
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
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" 
                            onclick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" 
                            onclick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" 
                            CausesValidation="false" runat="server" OnClick="btCancelOperation_Click" />    
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
                            <asp:ListItem Value="gm.GroupMenu">GROUP ID</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox ID="txtSearchBy" runat="server" Width="200px" CssClass="inptype"></asp:TextBox>                    
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="40%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />&nbsp;
                    </td>                    
                </tr>
            </table>   
            <hr />         
        </asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server" Width="100%" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">GROUP MENU LIST</TD>
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
                                <asp:TemplateColumn HeaderText="EDIT" HeaderStyle-Width="3%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/images/toolbar/edit.jpg" CommandName="Edit" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DELETE" HeaderStyle-Width="3%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/images/toolbar/delete.jpg" CommandName="Delete" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="REPLICATE" HeaderStyle-Width="3%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbReplicate" runat="server" ImageUrl="../Images/button/page.gif" CommandName="Replicate" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GROUP ID" ItemStyle-HorizontalAlign="Left" SortExpression="groupmenu" HeaderStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlgroupid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "groupmenu") %>' NavigateUrl="#"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GROUP NAME" ItemStyle-HorizontalAlign="Center" SortExpression="description" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblgroupname" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingGroupMenu" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20" ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
         <asp:Panel ID="pnlDetail" runat="server" Width="100%" Visible="false">
             <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">DETAIL GROUP MENU</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
             <table runat="server" cellSpacing="1" cellPadding="2" width="95%" border="0">
                 <tr>
                     <td class="tdganjil" width="20%">
                         Group ID
                     </td>
                     <td class="tdganjil" width="30%">
                         <asp:TextBox runat="server" ID="txtGroupID" Width="150px" ReadOnly="true" CssClass="inptypemandatory"></asp:TextBox>
                         <asp:RequiredFieldValidator runat="server" ID="rfvGroupID" ForeColor="Red" ErrorMessage ="Please, Fill GroupID..." ControlToValidate="txtGroupID"></asp:RequiredFieldValidator>
                     </td>                 
                     <td class="tdganjil" colspan="2">
                     </td>
                 </tr>
                 <tr>
                     <td class="tdganjil" width="20%">
                         Group Name
                     </td>
                     <td class="tdganjil" width="30%">
                         <asp:TextBox runat="server" ID="txtGroupName" Width="150px" ReadOnly="true" CssClass="inptypemandatory"></asp:TextBox>
                         <asp:RequiredFieldValidator runat="server" ID="rfvGroupName" ForeColor="Red" ErrorMessage ="Please, Fill GroupName..." ControlToValidate="txtGroupName"></asp:RequiredFieldValidator>
                     </td>                 
                     <td class="tdganjil" colspan="2">
                     </td>
                 </tr>
             </table>
             <br />
             <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">LIST DETAIL GROUP MENU</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
             <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dtgDetailGroup" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1" 
                            onsortcommand="dtgDetailGroup_SortCommand" 
                            onitemdatabound="dtgDetailGroup_ItemDataBound" OnItemCommand="dtgDetailGroup_ItemCommand">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="SELECT" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSelect" Visible="false" Text="0" ></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkSelect" />                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MENU ID" ItemStyle-HorizontalAlign="Left" SortExpression="menu_id" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlmenuid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "menu_id") %>' NavigateUrl="#"></asp:HyperLink>
                                        <asp:Label runat="server" ID="lblFlagMenu" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "menu_flag") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MENU NAME" ItemStyle-HorizontalAlign="Left" SortExpression="menu_name" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label id="lblmenuname" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ACCESS CREATE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccessCreate" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"access_Create") %>'></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkAccessCreate" Enabled="false"/>                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ACCESS READ" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccessRead" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"access_Read") %>'></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkAccessRead" Enabled="false" />                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ACCESS WRITE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccessWrite" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"access_Write") %>'></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkAccessWrite" Enabled="false"/>                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ACCESS DELETE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccessDelete" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"access_Delete") %>'></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkAccessDelete" Enabled="false" />                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ACCESS APPROVAL" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkAccessApproval" Enabled="false"/>                      
                                        <asp:Label runat="server" ID="lblAccessApproval" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"access_Approval") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="APPROVAL TYPE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblApprovalTypeID" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"ApprovalTypeID") %>'></asp:Label>
                                       <asp:DropDownList runat="server" ID="ddlApprovalType" Enabled="false"></asp:DropDownList>                      
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingDtGroup" 
                            OnNavigationButtonClicked="NavigationButtonClickedDt" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton runat="server" id="imbSelected" ImageUrl="../Images/button/buttonSaveSelectedNew.gif" OnClick="imbSelected_Click" />&nbsp;
                        <asp:ImageButton runat="server" id="imbNext" ImageUrl="../Images/button/buttonNext.gif" OnClick="imbNext_Click" Visible="false" />&nbsp;
                    </td>
                </tr>

            </table>
         </asp:Panel>
         <%--<asp:Panel runat="server" ID="pnlSave" Width="100%" Visible="false">
             <table runat="server" border="0" cellpadding="2" cellspacing="1" width="100%">
                 <tr>
                     <td class="tdganjil" align="right">
                         <asp:ImageButton runat="server" ID="imbSave" ImageUrl="../Images/button/buttonSave.gif" />&nbsp;
                         <asp:ImageButton runat="server" ID="imbCancel" ImageUrl="../Images/button/buttonCancel.gif" />
                     </td>
                 </tr>
             </table>
         </asp:Panel>--%>
    </form>
</asp:Content>

