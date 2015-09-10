<%@ Page Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="hr_allowance.aspx.cs" Inherits="pj_HR_hr_allowance" Title="Untitled Page" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <link href="../script/style.css" type="text/css" rel="stylesheet" />

    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

</head>--%>
<%--<body>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />

    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>
    <form id="frmAllowance" runat="server">
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
                                <asp:TemplateColumn HeaderText="ALLOWANCE ID" ItemStyle-HorizontalAlign="Left" SortExpression="Allow_id" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlAllowID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_id") %>' NavigateUrl="#"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ALLOWANCE DATE" ItemStyle-HorizontalAlign="Center" SortExpression="transaction_date" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblAllowDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"transaction_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SITE CARD" ItemStyle-HorizontalAlign="Center" SortExpression="sitecardID" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sitecardID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="START DATE" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "startdate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="END DATE" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "enddate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="KETERANGAN" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "keterangan") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATUS" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status") %>'></asp:Label>
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
                <%--<tr>
                    <td align="left">
                    <asp:ImageButton ID="imbFirstPage" runat="server" ImageUrl="~/Images/button/butkiri1.gif" CommandName="First" OnCommand="NavigationEvent" CausesValidation="false" />
              &nbsp;<asp:ImageButton ID="imbPrevPage" runat="server" ImageUrl="~/Images/button/butkiri.gif" CommandName="Prev" OnCommand="NavigationEvent" CausesValidation="false" />
              &nbsp;<asp:ImageButton ID="imbNextPage" runat="server" ImageUrl="~/Images/button/butkanan.gif" CommandName="Next" OnCommand="NavigationEvent" CausesValidation="false" />
              &nbsp;<asp:ImageButton ID="imbLastPage" runat="server" ImageUrl="~/Images/button/butkanan1.gif" CommandName="Last" OnCommand="NavigationEvent" CausesValidation="false" />
              &nbsp;Page:
                    <asp:TextBox ID="txtPageNo" runat="server" Width="62px" CssClass="inptype"></asp:TextBox>
              &nbsp;<asp:ImageButton ID="imbPageGo" runat="server" ImageUrl="~/Images/button/buttonGO.gif" onclick="imbPageGo_Click" />
                    <asp:RangeValidator ID="rgvGo" runat="server" ControlToValidate="txtPageNo" 
                            Display="Dynamic" ErrorMessage="Page is not valid" Font-Names="Verdana" 
                            Font-Size="Smaller" MinimumValue="0"></asp:RangeValidator>
                    </td>
                    <td align="right">
                        <font color="#999999"><font face="Verdana" size="2">Page&nbsp; </font>
                            <asp:Label ID="lblPage" runat="server" Font-Names="Verdana" Font-Size="Smaller"></asp:Label>
                            <font face="Verdana" size="2">&nbsp;of</font>
                            <asp:Label ID="lblTotPage" runat="server" Font-Names="Verdana" 
                                Font-Size="Smaller"></asp:Label>
                            <font face="Verdana" size="2">, Total&nbsp; </font>
                            <asp:Label ID="lblTotRec" runat="server" Font-Size="Smaller"></asp:Label>
                            &nbsp;<font face="Verdana" size="2">record(s)</font></font>
                    </td>
                </tr>--%>
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
                        Allowance ID
                    </td>
                    <td class="tdganjil">
                        <asp:Label runat="server" id="lblAllowID" ForeColor="OrangeRed" Font-Bold="true">[Auto Generate]</asp:Label>                           
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Allowance Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucAllowDate" />                         
                    </td>
                    <td class="tdganjil" >
                        SiteCard 
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox ID="txtSiteCard" runat="server" Width="150px" CssClass="inptypemandatory" ></asp:TextBox>
                        <asp:Label ID="lblSiteCard" runat="server"></asp:Label>
                        <asp:ImageButton runat="server" ID="imbRefresh" 
                            ImageUrl="../images/button/iconrefresh.png" onclick="imbRefresh_Click" 
                            CausesValidation="true" style="width: 24px"/>
                        <asp:RequiredFieldValidator ID="RFVSitecard" ControlToValidate="txtSiteCard" runat="server" ErrorMessage="Please, Fill SiteCard.."
                             ForeColor="Red" Font-Size="10"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Payroll Periode Type
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="ddlPayrolPeriodeType_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp;
                        <asp:DropDownList runat="server" ID="ddlPayrolPeriodeType" AutoPostBack="true"
                             OnSelectedIndexChanged="ddlPayrolPeriodeType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tdganjil">
                        Payroll Periode 
                    </td>
                    <td class="tdganjil">
                        <uc1:ValidDate ID="ucStartDate" runat="server" />&nbsp;s/d&nbsp;
                        <uc1:ValidDate ID="ucEndDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Keterangan
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox CssClass="inptype" runat="server" ID="txtKeterangan" 
                             Width="300px" Height="60" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Mode Type Detail SiteCard
                    </td>
                    <td class="tdganjil">
                        <asp:RadioButtonList runat="server" ID="rblSCDetail" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblSCDetailChanged">
                            <asp:ListItem Value="DDL" Selected="True">Mode 1</asp:ListItem>
                            <asp:ListItem Value="TBL">Mode 2</asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                    <td colspan="2"></td>
                </tr>
            </table>
            <br />            
            <asp:Panel ID="pnlDetail" runat="server" Width="100%" Visible="false" >
                <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                    <TR class="trtopi">
                        <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                        <TD class="tdtopi" align="center">DETAIL SITECARD LIST</TD>
                        <TD class="tdtopikanan" width="10">&nbsp;</TD>
                    </TR>
                </table>
                <asp:Panel ID="PnlDetailModeDDL" runat="server" Width="100%" Visible="false" >
                    <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgSCDetail" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="True" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                onitemcommand="dtgSCDetail_ItemCommand" CellPadding="3" CellSpacing="1" 
                                onsortcommand="dtgSCDetail_SortCommand" 
                                onitemdatabound="dtgSCDetail_ItemDataBound">
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="NIK" ItemStyle-HorizontalAlign="Left" SortExpression="nik" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label id="lblNIK" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"nik") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="NAME" ItemStyle-HorizontalAlign="Left" SortExpression="transaction_date" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label id="lblName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"nama") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="POSISI" ItemStyle-HorizontalAlign="Left" SortExpression="posisi" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label id="lblPosisi" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"posisi") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ALLOWANCE 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAllowance1"></asp:DropDownList> 
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT ALLOW 1" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <uc4:ucInputNumber runat="server" ID="ucAmountAllow1"  />
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ALLOWANCE 2" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAllowance2" ></asp:DropDownList>   
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT ALLOW 2" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <uc4:ucInputNumber runat="server" ID="ucAmountAllow2" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDescription" Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDetailModeTabel" runat="server" Width="100%" Visible="false" >
                    <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgSCTabel" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="True" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1" 
                                onsortcommand="dtgSCTabel_SortCommand" 
                                onitemdatabound="dtgSCTabel_ItemDataBound">
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="NIK" ItemStyle-HorizontalAlign="Left" SortExpression="nik" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label id="lblNIK" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"nik") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="NAME" ItemStyle-HorizontalAlign="Left" SortExpression="transaction_date" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label id="lblName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"nama") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="POSISI" ItemStyle-HorizontalAlign="Left" SortExpression="posisi" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label id="lblPosisi" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"posisi") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ALLOWANCE TYPE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <table runat="server" width="100%" border="1" cellpadding="1" cellspacing="1" >
                                                <tr>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType1"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType1" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType1" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType2"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType2" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType2" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType3"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType3" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType3" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType4"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType4" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType4" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType5"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType5" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType5" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType6"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType6" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType6" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdganjil" width="30%" >
                                                        <asp:Label runat="server" ID="lblAllowType7"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType7" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType7" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType8"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType8" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType8" />
                                                    </td>
                                                    <td class="tdganjil" width="30%">
                                                        <asp:Label runat="server" ID="lblAllowType9"></asp:Label>
                                                        <asp:Label runat="server" ID="lblIDAllowType9" Visible="false"></asp:Label>
                                                        <uc4:ucInputNumber runat="server" ID="ucAmountAllowType9" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <%--<asp:TemplateColumn HeaderText="ALLOWANCE 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAllowance1"></asp:DropDownList> 
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT ALLOW 1" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <uc4:ucInputNumber runat="server" ID="ucAmountAllow1"  />
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ALLOWANCE 2" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAllowance2" ></asp:DropDownList>   
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT ALLOW 2" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <uc4:ucInputNumber runat="server" ID="ucAmountAllow2" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDescription" Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
                <table runat="server" cellSpacing="1" cellPadding="2" width="60%" border="0">
                    <tr>
                        <td class="tdganjil" align="left">                            
                            <asp:ImageButton runat="server" ID="imbBack" CausesValidation="false"
                                ImageUrl="~/images/button/buttonBack.gif" onclick="imbBack_Click" />&nbsp;
                            <asp:ImageButton runat="server" ID="imbSave" 
                                ImageUrl="~/images/button/buttonsave.gif" onclick="imbSave_Click" />
                        </td>
                        <td width="50%"  class="tdganjil" align="right">
                            
                        </td>                        
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </form>
</asp:Content>
<%--</body>--%>
<%--</html>--%>

