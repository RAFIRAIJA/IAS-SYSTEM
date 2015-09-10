<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_uploadworksheet.aspx.cs" Inherits="pj_ar_ar_uploadworksheet" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />

    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

    <form id="frmUploadWorksheet" runat="server">
        <input type="hidden" id="hdnMessangerID" runat="server" name="hdnMessangerID" class="inptype"/>
		<input type="hidden" id="hdnMessangerName" runat="server" name="hdnMessangerName" class="inptype"/>

        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlTOOLBAR" runat="server" Width="100%">  
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
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />&nbsp;
                        <asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" Visible="false" ImageUrl="~/images/toolbar/print.jpg" runat="server" OnClick="btPrintRecord_Click" />                           
    
                    </td>
                </tr>                      
            </table>
        <hr />
        <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="20%">Entity ID</td>
                    <td class="tdganjil" width="30%">
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
                    <td class="tdganjil" width="20%">Search By</td>
                    <td class="tdganjil" width="30%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.InvoiceNo">Invoice No</asp:ListItem>
                            <asp:ListItem Value="a.CustName">Customer Name</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="'tdganjil" width="20%">
                        <asp:CheckBox runat="server" ID="chkPeriode" AutoPostBack="true" OnCheckedChanged="ChkPeriodeCheckedChanged" />&nbsp;
                        Periode Upload
                    </td>
                    <td class="tdganjil" colspan="3">
                        <uc1:ValidDate runat="server" ID="ucStartDate" isEnabled="false" />&nbsp;s/d&nbsp;
                        <uc1:ValidDate runat="server" ID="ucEndDate" isEnabled="false" />&nbsp;&nbsp;
                    </td>
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
        <asp:Panel ID="pnlListInvoice" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">LIST INVOICE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
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
                                 <asp:TemplateColumn HeaderText="UPLOAD DATE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblUploadDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"UploadDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVOICE NO" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblInvoiceNo" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"InvoiceNo") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVOICE DATE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblInvoiceDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"InvDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CUST.CODE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblCustCode" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CustCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CUSTOMER NAME" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblCustName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CustName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVOICE AMOUNT" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblInvoiceAmount" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"InvoiceAmount","{0:n}") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SITE CARD" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCard") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Branch") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="PRODUCT OFFERING" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductOffering" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProdOffering") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="OCM" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOCM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OCM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="FCM" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFCM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FCM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="COLLECTOR" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCollector" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Collector") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="ENTITY" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Entity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingWorksheet" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlUpload" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">UPLOAD FILE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="20%">File Upload</td>
                    <td class="tdganjil" width="30%">
                        <asp:FileUpload runat="server" ID="fuWorksheet" />              
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center">
                        <asp:ImageButton id="btnUpload" ToolTip="Upload File" ImageUrl="~/images/button/btnUploadLong.gif" runat="server" Height="33px" OnClick="btnUpload_Click" />

                    </td>
                    <td class="tdganjil" colspan="2"></td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlPrint" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">PRINT LIST INVOICE UPLOAD</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/button/ButtonBack.gif" OnClick="imbBack_Click"/>               

            <rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true"
                ZoomMode="FullPage" Height="100%" ID="rptViewer" AsyncRendering="false" runat="server"></rsweb:ReportViewer>
            </div>
        </asp:Panel>
        
    </form>
</asp:Content>

