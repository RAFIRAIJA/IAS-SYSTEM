<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ad_systemuser.aspx.cs" Inherits="pj_ad_ad_systemuser" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>
<%@ Register src="../UserController/ucLookUp_ADGroupMenu.ascx" tagname="ucGroupMenu" tagprefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <script type="text/javascript">
        function OpenWinLookUp(pNIK,pName,pGroupMenu,pBranchID, pEntityID) {
            var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/pj_ad/ad_systemuser_listsitecard.aspx?NIK=' + pNIK + '&NAMA=' + pName + '&GroupMenu=' + pGroupMenu + '&BranchID=' + pBranchID + '&EntityID=' + pEntityID, 'UserLookup', 'left=50, top=10, width=900, height=600, menubar=0, scrollbars=yes');
    }

    </script>


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
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" 
                            onclick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" 
                            ImageUrl="~/images/toolbar/find.jpg" runat="server" 
                            onclick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />    
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
                            <asp:ListItem Value="a.userid">NIK</asp:ListItem>
                            <asp:ListItem Value="a.name">NAMA</asp:ListItem>
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
                    <TD class="tdtopi" align="center">SYSTEM USER</TD>
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
                                <asp:TemplateColumn HeaderText="EDIT" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/images/toolbar/edit.jpg" CommandName="Edit" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DELETE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/images/toolbar/delete.jpg" CommandName="Delete" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NIK" ItemStyle-HorizontalAlign="Left" SortExpression="userID" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="lblNIK" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"userID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NAMA" ItemStyle-HorizontalAlign="Left" SortExpression="name" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblusername" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center" SortExpression="branchid" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblbranchid" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"branchid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="ENTITY" ItemStyle-HorizontalAlign="Center" SortExpression="CompanyID" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblEntity" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CompanyID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="EMAIL" ItemStyle-HorizontalAlign="Left" SortExpression="EmailAddr" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblEmail" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"EmailAddr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                

                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingSystemUser" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlADDNew" runat="server" Width="100%" Visible="false" >
                <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                    <TR class="trtopi">
                        <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                        <TD class="tdtopi" align="center">SYSTEM USER INPUT</TD>
                        <TD class="tdtopikanan" width="10">&nbsp;</TD>
                    </TR>
                </table>
                <table runat="server" cellSpacing="1" cellPadding="2" width="100%" border="0" >
                    <tr>
                        <td class="tdganjil" width="20%">
                            NIK Employee
                        </td>
                        <td class="tdganjil" width="30%">
                            <asp:TextBox runat="server" ID="txtNIKEmployee" CssClass="inptype" Width="80px"></asp:TextBox>&nbsp;                                               
                            <asp:ImageButton runat="server" ID="imbLookUpNIK" ImageUrl="~/images/toolbar/autocomplete.jpg" OnClick="imbLookUpNIK_Click" />
                        </td>
                        <td class="tdganjil" colspan="2">
                            
                        </td>
                    </tr>                
                </table>                
            </asp:Panel>

        <asp:Panel ID="pnlInput" runat="server" Width="100%" Visible="false" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">SYSTEM USER INPUT</TD>
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
                        
                    </td>
                    <td class="tdganjil">
                        
                    </td>
                </tr>                
                <tr>
                    <td class="tdganjil">
                        NIK
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtNIK" CssClass="inptype" Width="80px"></asp:TextBox>                                               
                    </td>
                    <td class="tdganjil">
                        Nama
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtNama" CssClass="inptype" Width="200px"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td class="tdganjil">
                        Dept.
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlDept">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                        </asp:DropDownList>                                               
                    </td>
                    <td class="tdganjil">
                        Group Menu
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtGroupMenu" CssClass="inptype" Width="200px"></asp:TextBox>                                               
                    </td>
                </tr>   
                <tr>
                    <td class="tdganjil">
                        Email
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="inptype" Width="150px"></asp:TextBox>                                               
                    </td>
                    <td class="tdganjil">
                        No.HP
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtNoHP" CssClass="inptype" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Branch
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlBranch">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                        </asp:DropDownList>                                               
                    </td>
                    <td class="tdganjil" >
                        Menu Style
                    </td>
                    <td class="tdganjil" >
                        <asp:DropDownList runat="server" ID="ddlmenustyle">
                            <asp:ListItem Value="TREE">TREE MENU</asp:ListItem>
                            <asp:ListItem Value="MENUSMENU">MENUS MENU</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr> 
                            
            </table>
            <asp:Panel ID="pnlAddGroupMenu" runat="server" Visible="false" Width="100%">
                <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                    <TR class="trtopi">
                        <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                        <TD class="tdtopi" align="center">ADD GROUP MENU</TD>
                        <TD class="tdtopikanan" width="10">&nbsp;</TD>
                    </TR>
                </table>   
                <table runat="server" cellSpacing="1" cellPadding="2" width="100%" border="0">
                    <tr>
                        <td class="tdganjil" width="20%">
                            Group Menu
                        </td>
                        <td class="tdganjil" colspan="3">
                            <uc3:ucGroupMenu runat="server" ID="ucGroupMenu" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdganjil" >
                            Entity
                        </td>
                        <td class="tdganjil" colspan="3">
                            <asp:DropDownList runat="server" ID="ddlEntityGroupMenu">
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="ISS">ISS</asp:ListItem>
                                <asp:ListItem Value="IFS">IFS</asp:ListItem>
                                <asp:ListItem Value="ICS">ICS</asp:ListItem>
                                <asp:ListItem Value="IPM">IPM</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdganjil" width="20%">
                            Branch
                        </td>
                        <td class="tdganjil" colspan="3">
                            <asp:DropDownList runat="server" ID="ddlBranchGroupMenu">
                                <asp:ListItem Value ="">Select One</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdganjil" width="20%">
                            SiteCard Flag
                        </td>
                        <td class="tdganjil" colspan="3">
                            <asp:CheckBox runat="server" ID="chkIsSiteCard" Checked="false" AutoPostBack="true" OnCheckedChanged="chkIsSiteCard_CheckedChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdganjil" colspan="4" align="right">
                            <asp:ImageButton runat="server" ID="imbInsertGroupMenu" ImageUrl="../Images/button/buttoninsert.gif" OnClick="imbInsertGroupMenu_Click" />
                        </td>
                    </tr>
                </table>                     
            </asp:Panel>
            <asp:Panel ID="pnlGridGroupMenu" runat="server" Visible="false" Width="100%">
            <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">GROUP ACCES MENU</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgGroupMenu" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1" 
                            onsortcommand="dgGroupMenu_SortCommand" 
                            OnItemCommand ="dgGroupMenu_ItemCommand" 
                            onitemdatabound="dgGroupMenu_ItemDataBound">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="DELETE" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imbDelete" ImageUrl="../Images/button/icondelete.gif" CommandName="DELETE" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GROUP MENU" ItemStyle-HorizontalAlign="Left" SortExpression="groupmenu" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblgroupmenu" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"groupmenu") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ENTITY" ItemStyle-HorizontalAlign="Center" SortExpression="entityid" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblentityid" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"entityid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center" SortExpression="branchid" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblbranchid" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"branchid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>       
                                <asp:TemplateColumn HeaderText="SITECARD" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblflagsitecard" runat="server" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"flagsitecard") %>'></asp:Label>
                                        <asp:HyperLink runat="server" ID="hlSiteCard" Visible="false" Text="SiteCard"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                                                                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td align="right">
                        <asp:ImageButton runat="server" id="imbAdd" ImageUrl="../images/button/buttonAdd.gif" OnClick="imbAdd_Click" />&nbsp;
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlSave" runat="server" Visible="false" Width="100%">
            <table runat="server" cellSpacing="1" cellPadding="2" width="60%" border="0">
                    <tr>
                        <td width="50%"  class="tdganjil" align="right">                            
                        </td>                        
                        <td class="tdganjil" align="right">                                                        
                            <asp:ImageButton runat="server" ID="imbSave" 
                                ImageUrl="~/images/button/buttonsave.gif" onclick="imbSave_Click" />
                        </td>
                    </tr>
                </table>
        </asp:Panel>


    </form>
</asp:Content>

