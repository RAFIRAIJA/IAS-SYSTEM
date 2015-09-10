<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ut_mappingitem.aspx.cs" Inherits="pj_UT_ut_mappingitem" %>

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
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" CausesValidation="false" />&nbsp;
                        <asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" Visible="false" ImageUrl="~/images/toolbar/print.jpg" runat="server" OnClick="btPrintRecord_Click"  />                           
    
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
                            <asp:ListItem Value="ISSN">ISS</asp:ListItem>
                            <asp:ListItem Value="IFSN" Selected="True">IFS</asp:ListItem>
                            <asp:ListItem Value="IPMN">IPM</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ErrorMessage="Select Entity...Please..." ForeColor="Red" ControlToValidate="ddlEntity"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdganjil" colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdganjil" width="20%">Mapping Type</td>
                    <td class="tdganjil" width="30%">
                        <asp:DropDownList runat="server" ID="ddlMappingType" OnSelectedIndexChanged="ddlMappingType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="SiteCard">SiteCard</asp:ListItem>
                            <asp:ListItem Value="Item" Selected="True">Item</asp:ListItem>
                            <asp:ListItem Value="Vendor">Vendor</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvMMappingType" runat="server" ErrorMessage="Select MappingType...Please..." ForeColor="Red" ControlToValidate="ddlMappingType"></asp:RequiredFieldValidator>
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
                            <asp:ListItem Value="a.ItemNo">Item No</asp:ListItem>
                            <asp:ListItem Value="a.Itemname">Item Name</asp:ListItem>
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
        <asp:Panel ID="pnlListMapping" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">LIST MAPPING</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" Visible="true"
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
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
                                <asp:TemplateColumn HeaderText="ITEM NO" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblItemNo" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"ItemNo") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ITEM NO (old) " ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label id="lblItemNo_old" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"ItemNo_old") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ITEM DESC." ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label id="lblItemDesc" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"ItemDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MEASURE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblMeasure" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"Measure") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                            </Columns>
                        </asp:DataGrid>

                        <asp:DataGrid ID="dgListDataVendor" runat="server" AutoGenerateColumns="False" Visible="false"
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
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
                                <asp:TemplateColumn HeaderText="VENDOR NO" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblVendorNo" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"VendorID") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VENDOR NO (old) " ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label id="lblVendorID_old" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"VendorID_old") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VENDOR NAME" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label id="lblVendorName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"VendorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                            </Columns>
                        </asp:DataGrid>
                        
                        <asp:DataGrid ID="dgListDataSiteCard" runat="server" Width="100%" AutoGenerateColumns="false" Visible="false"
                            AllowSorting="True" borderwidth="0px" cellpadding="3" cellspacing="1"  CssClass="tablegrid" 
                            HorizontalAlign="Center">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul"></HeaderStyle>
                            <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="SITECARD" SortExpression="sitecard">
                                    <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                       <asp:Label ID="lblSitecard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sitecard") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SITECARD NAME" SortExpression="SiteCardName">
                                    <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>                               
                                       <asp:Label ID="lblSiteCardName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCardName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="JOB NO" SortExpression="JobNo">
                                    <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>                               
                                       <asp:Label ID="lblJobNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Job_No")%>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="JOB TASK NO" SortExpression="JobTaskNo">
                                    <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>                               
                                       <asp:Label ID="lblJobTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Job_TaskNo")%>' />
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
        
    </form>
</asp:Content>

