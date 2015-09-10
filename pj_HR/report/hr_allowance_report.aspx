<%@ Page Language="C#"  MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="hr_allowance_report.aspx.cs" Inherits="pj_hr_report_hr_allowance_report" %>
<%@ Register src="../../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report HR Allowance</title>
    <link href="../../script/style.css" type="text/css" rel="stylesheet" />

    <script src="../../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../../Include/JavaScript/Eloan.js" type="text/javascript"></script>

</head>
<body>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../../script/Style.css" type="text/css" rel="stylesheet" />
    <script src="../../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../../script/JavaScript/Eloan.js" type="text/javascript"></script>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlSearch" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" Width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">REPORT HR ALLOWANCE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" Width="95%">
                <tr>
                    <td class="tdganjil" width="17%">SiteCard</td>
                    <td class="tdganjil" width="43%">
                        <asp:TextBox ID="txtSiteCard" runat="server" Width="100px" CssClass="inptypemandatory" ></asp:TextBox>  
                        <asp:Label ID="lblSitecard" runat="server"> </asp:Label>                                            
                        <asp:ImageButton runat="server" ID="imbRefresh" ImageUrl ="../../images/toolbar/autocomplete.jpg" 
                            CausesValidation="false" OnClick="imbRefresh_Click" />
                        <asp:RequiredFieldValidator ID="rfvSiteCard" runat="server" ErrorMessage="Please, fill SiteCard..." ControlToValidate="txtSiteCard"></asp:RequiredFieldValidator>
                    </td>                   
                </tr>
                <%--<tr>
                    <td class="tdganjil" >NIK</td>
                    <td class="tdganjil" >
                        <asp:TextBox ID="txtNIK" runat="server" Width="100px" CssClass="inptypemandatory"></asp:TextBox>                    
                    </td>
                    <td class="tdganjil" colspan="2"></td> 
                </tr>--%>
                <tr>
                    <td class="tdganjil" >Periode</td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucStartDate" />&nbsp;
                        s/d&nbsp;
                        <uc1:ValidDate runat="server" ID="ucEndDate" />
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="60%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../../Images/button/buttonSearch.gif" CausesValidation="true"
                            onclick="btnSearch_Click" />&nbsp;
                        <asp:ImageButton ID="btnPreview" runat="server" Visible="false"
                            ImageUrl="../../Images/button/buttonPreview.gif" 
                            onclick="btnPreview_Click" />
                    </td>                    
                </tr>
                
            </table>   
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDataList" Width="100%">
            <table runat="server" border="0" cellpadding="0" cellspacing="0" Width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi"></td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" Width="95%">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            onitemcommand="dgListData_ItemCommand" CellPadding="3" CellSpacing="1" 
                            onsortcommand="dgListData_SortCommand" 
                            onitemdatabound="dgListData_ItemDataBound" >
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
                                        <asp:Label ID="lblSiteCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sitecard") %>'></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblSiteCardName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sitecardname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="START DATE" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allowance_startdate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="END DATE" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allowance_enddate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NIK" HeaderStyle-Width="8%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNIK" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nik") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NAMA" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nama") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ALLOW TYPE1" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowType1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_typeid1") %>'></asp:Label>
                                        <asp:Label ID="lblAllowName1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AllowName1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ALLOW AMOUNT1" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowAmount1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_amount1", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ALLOW TYPE2" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowType2" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_typeid2") %>'></asp:Label>
                                        <asp:Label ID="lblAllowName2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AllowName2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ALLOW AMOUNT2" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowAmount2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "allow_amount2", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReport" Width="100%" Visible="false">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr>
                    <td>                        
                        <asp:ImageButton ID="imbBack" runat="server" ImageUrl="../../Images/button/ButtonBack.gif"
                            OnClick="imbBack_Click" />
                        <br />                        
                        <rsweb:ReportViewer  Width="100%" ProcessingMode="Local" SizeToReportContent="true"
                            ZoomMode="FullPage" Height="100%" ID="ReportViewer1" AsyncRendering="false" runat="server">

                        </rsweb:ReportViewer>

                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</asp:Content>
<%--</body>
</html>--%>
