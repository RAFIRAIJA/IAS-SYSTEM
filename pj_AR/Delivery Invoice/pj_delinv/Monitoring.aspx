<%@ Page Title="" Language="C#" MasterPageFile="~/pj_delinv/MasterInternCS.master" AutoEventWireup="true" CodeFile="Monitoring.aspx.cs" Inherits="pj_delinv_Monitoring" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <meta content="BlendTrans(Duration=0)" http-equiv="Page-Exit" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="../script/calendar.js"></script>
    <link rel="stylesheet" href="script/style-page.css" type="text/css" media="all" />

    <script type="text/javascript" language="Javascript">
        //<!-- hide script from older browsers
        function openwindow(url, nama, width, height) {
            OpenWin = this.open(url, nama);
            if (OpenWin != null) {
                if (OpenWin.opener == null)
                    OpenWin.opener = self;
            }
            OpenWin.focus();
        }
        // End hiding script-->

        var popup;
        function SelectData(url) {
            popup = window.open(url, "Popup", "width=300,height=200");
            popup.focus();
            return false
        }

        function myConfirm(message) {
            var start = Number(new Date());
            var result = confirm(message);
            var end = Number(new Date());
            return (end < (start + 10) || result == true);
        }
        
    </script>

    <form id="mpFORM" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

        <asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pnTOOLBAR" runat="server">  
                        <table border="0" cellpadding="3" cellspacing="3">
                            <tr>
                                <td valign="top"><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
                                <td valign="top"><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
                            </tr>               
                        </table>
                        <hr />
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="headline-worksheet"></div>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                    <div id="txt-title">TODAY'S REPORT</div>
                    <%--<div id="lblTitle" style="font-size:16px;"><strong>TODAY'S REPORT</strong></div>--%>
                    <br />
                    <br />
                    <div id="div-today-report">
                      <table border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="450" align="center" valign="middle" style="font-size:16px;"><strong>PROCEEDS</strong></td>
                          <td width="450" align="center" valign="middle" style="font-size:16px;"><strong>DELIVERED</strong></td>
                          <td width="450" align="center" valign="middle" style="font-size:16px;"><strong>RETURNED</strong></td>
                          <td width="450" align="center" valign="middle" style="font-size:16px;"><strong>DONE</strong></td>
                        </tr>
                        <tr>
                          <td height="100" align="center" valign="middle" style="font-size:16px;"><strong><asp:Label ID="mlPROCEEDS" runat="server" Text="99"></asp:Label></strong></td>
                          <td height="100" align="center" valign="middle" style="font-size:16px;"><strong><asp:Label ID="mlDELIVERED" runat="server" Text ="99"></asp:Label></strong></td>
                          <td height="100" align="center" valign="middle" style="font-size:16px;"><strong><asp:Label ID="mlRETURNED" runat="server" Text="0"></asp:Label></strong></td>
                          <td height="100" align="center" valign="middle" style="font-size:16px;"><strong><asp:Label ID="mlDONE" runat="server" Text="99"></asp:Label></strong></td>
                        </tr>
                        <tr>
                          <td align="center" valign="middle">&nbsp;</td>
                          <td align="center" valign="middle">&nbsp;</td>
                          <td align="center" valign="middle">&nbsp;</td>
                          <td align="center" valign="middle">&nbsp;</td>
                        </tr>
                      </table>
                    </div> 
                    <div id="space"></div>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                    <div id="lblTitle1" style="font-size:16px;"><strong>DATABASE</strong></div>
                    <div id="space"></div>
                    
                    <asp:Panel ID="mlPNLGRID" runat="server" ScrollBars="Auto" Width="1020px">
                        <table>
                        <tr>
                            <td align="center">
                                <asp:CheckBox ID="mlCHECKALL" runat="server" AutoPostBack="true" OnCheckedChanged="mlCHECKALL_CheckedChanged" />
                            </td>
                            <td><span class="add" style="padding-left: 20px;"><asp:Button ID="mlBTNADD" runat="server" Text="Add" Height="25px" Width="125px" OnClick="mlBTNADD_Click" OnClientClick="return confirm('Add an item(s) ?');"></asp:Button></span></td>
                            <td><span class="delete" style="padding-left: 20px;"><asp:Button ID="mlBTNDELETE" runat="server" Text="Delete" Height="25px" Width="125px" OnClick="mlBTNDELETE_Click" OnClientClick="return confirm('Delete an item(s) ?');" ></asp:Button></span></td>
                            <td><span class="select" style="padding-left: 20px;"><asp:Button ID="mlBTNDONE" runat="server" Text="CONFIRM" Height="25px" Width="125px" OnClick="mlBTNDONE_Click" OnClientClick="return myConfirm('Confirm to Done an item(s) ?');"></asp:Button></span></td>
                            <td><span class="add" style="padding-left: 20px;"><asp:Button ID="mlBTNADDMORE" runat="server" Text="Add More" Height="25px" Width="125px" OnClick="mlBTNADDMORE_Click" OnClientClick="return confirm('Add more an item(s) to the receipt code ?');"></asp:Button></span></td>
                            <td><span class="select" style="padding-left: 20px;"><asp:Button ID="mlBTNRANGEDATE" runat="server" Text="Select Range Date" Height="25px" Width="125px" OnClick="mlBTNRANGEDATE_Click"></asp:Button></span></td>
                        </tr>
                        </table>

                        <%--<div id="GridViewContainer" class="GridViewContainer" style="width:1020px;height:400px;" >--%>
                        <asp:DataGrid runat="server" ID="mlDGWORKSHEET"
                        CssClass="Grid"
                        UseAccessibleHeader="True"
                        autogeneratecolumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        OnPageIndexChanged="mlDGWORKSHEET_PageIndexChanged">

                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <EditItemStyle  CssClass="GridItem" />
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Left" PageButtonCount="20" BorderStyle="Solid"/>
                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                        <Columns>
                            
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="mlCHECKBOX" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn HeaderText="Company" DataField="CompanyCode" SortExpression = "CompanyCode" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice No" DataField="InvNo" SortExpression = "InvNo" ItemStyle-Wrap="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Code" DataField="InvCustCode" SortExpression = "InvCustCode" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Name" DataField="InvCustName" SortExpression = "InvCustName" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice Date" DataField="InvDate" SortExpression = "InvDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Branch" DataField="InvBranch" SortExpression = "InvBranch" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Amount" DataField="InvAmount" SortExpression = "InvAmount" DataFormatString ="{0:#,##0}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Status" DataField="InvStatus" SortExpression = "InvStatus" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Description" DataField="InvDesc" SortExpression = "InvDesc" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Receipt Code" DataField="InvReceiptCode" SortExpression = "InvReceiptCode" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Proceeds" DataField="InvProceedsDate" SortExpression = "InvProceedsDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Delivered" DataField="InvDeliveredDate" SortExpression = "InvDeliveredDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Returned" DataField="InvReturnedDate" SortExpression = "InvReturnedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Done" DataField="InvDoneDate" SortExpression = "InvDoneDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Penerima" DataField="InvCustPenerima" SortExpression = "InvCustPenerima" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger NIK" DataField="InvCodeMess" SortExpression = "InvCodeMess" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger Name" DataField="InvMessName" SortExpression = "InvMessName" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User NIK" DataField="UserID" SortExpression = "UserID" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User Name" DataField="UserName" SortExpression = "UserName" ItemStyle-Wrap="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Created Date" DataField="InvCreatedDate" SortExpression = "InvCreatedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Created By" DataField="InvCreatedBy" SortExpression = "InvCreatedBy" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Modified Date" DataField="InvModifiedDate" SortExpression = "InvModifiedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Modified By" DataField="InvModifiedBy" SortExpression = "InvModifiedBy" Visible="false" ItemStyle-Wrap="False" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Site Card" DataField="InvSiteCard" SortExpression = "InvSiteCard" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Resi TIKI" DataField="InvResiTiki" SortExpression = "InvResiTiki" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Prepared For" DataField="InvPreparedForDate" SortExpression = "InvPreparedForDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Product Offering" DataField="InvProdOffer" SortExpression = "InvProdOffer" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="OCM" DataField="InvOCM" Visible="false" SortExpression = "InvOCM" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="FCM" DataField="InvFCM" Visible="false" SortExpression = "InvFCM" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Collector" DataField="InvCollector" SortExpression = "InvCollector" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Receipt Flag" DataField="InvReceiptFlag" SortExpression = "InvReceiptFlag" Visible="false" HeaderStyle-Wrap="False"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <%--</div>--%>

                    <asp:Panel ID ="pnlSEARCH" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table id="mlTABLESEARCH">
                                        <tr>
                                            <td>
                                                <asp:Label ID="mlLBLSEARCH" runat="server" Text="Find"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="mlTXTSEARCH" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btSearch" runat="server" ImageUrl="~/images/toolbar/find.jpg" OnClick="btSearch_Click" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="mlDDLSEARCH" runat="server">
                                                    <asp:ListItem Text="Invoice No" Value="InvNo"></asp:ListItem>
                                                    <asp:ListItem Text="Customer Code" Value="InvCustCode"></asp:ListItem>
                                                    <asp:ListItem Text="Customer Name" Value="InvCustName"></asp:ListItem>
                                                    <asp:ListItem Text="Invoice Date" Value="InvDate"></asp:ListItem>
                                                    <asp:ListItem Text="Branch" Value="InvBranch"></asp:ListItem>
                                                    <asp:ListItem Text="Amount" Value="InvAmount"></asp:ListItem>
                                                    <asp:ListItem Text="Status" Value="InvStatus"></asp:ListItem>
                                                    <asp:ListItem Text="Description" Value="InvDesc"></asp:ListItem>
                                                    <asp:ListItem Text="Receipt Code" Value="InvReceiptCode"></asp:ListItem>
                                                    <asp:ListItem Text="Proceeds" Value="InvProceedsDate"></asp:ListItem>
                                                    <asp:ListItem Text="Delivered" Value="InvDeliveredDate"></asp:ListItem>
                                                    <asp:ListItem Text="Returned" Value="InvReturnedDate"></asp:ListItem>
                                                    <asp:ListItem Text="Done" Value="InvDoneDate"></asp:ListItem>
                                                    <asp:ListItem Text="Penerima" Value="InvCustPenerima"></asp:ListItem>
                                                    <asp:ListItem Text="Messenger NIK" Value="InvCodeMess"></asp:ListItem>
                                                    <asp:ListItem Text="Messenger Name" Value="InvMessName"></asp:ListItem>
                                                    <asp:ListItem Text="User NIK" Value="UserID"></asp:ListItem>
                                                    <asp:ListItem Text="User Name" Value="UserName"></asp:ListItem>
                                                    <asp:ListItem Text="Created Date" Value="InvCreatedDate"></asp:ListItem>
                                                    <asp:ListItem Text="Created By" Value="InvCreatedBy"></asp:ListItem>
                                                    <asp:ListItem Text="Modified Date" Value="InvModifiedDate"></asp:ListItem>
                                                    <asp:ListItem Text="Modified By" Value="InvModifiedBy"></asp:ListItem>
                                                    <asp:ListItem Text="Site Card" Value="InvSiteCard"></asp:ListItem>
                                                    <asp:ListItem Text="Resi TIKI" Value="InvResiTiki"></asp:ListItem>
                                                    <asp:ListItem Text="Prepared For" Value="InvPreparedForDate"></asp:ListItem>
                                                    <asp:ListItem Text="Product Offering" Value="InvProdOffer"></asp:ListItem>
                                                    <asp:ListItem Text="OCM" Value="InvOCM"></asp:ListItem>
                                                    <asp:ListItem Text="FCM" Value="InvFCM"></asp:ListItem>
                                                    <asp:ListItem Text="Collector" Value="InvCollector"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btClear" runat="server" Text="Clear" OnClick="btClear_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="mlDDLPAGESIZE" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="mlDDLPAGESIZE_SelectedIndexChanged">
                                                    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                    <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                    <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="line-blue"></div>
                    <div id="lblTitle2" style="font-size:16px;"><strong>UPLOAD DATABASE</strong></div>
                    <div id="div-upload">
                      <table id="tb-upload" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="75" rowspan="4">&nbsp;</td>
                          <td width="431" rowspan="4"><img src="../images/project/logo-excel.jpg" width="400" height="280" alt="" /></td>
                          <td width="512" height="60">&nbsp;</td>
                          <td width="130" rowspan="4">&nbsp;</td>
                        </tr>
                        <tr>
                          <td height="64" id="txt-info">Silahkan mengunggah data anda dalam bentuk file Excel. <br /><br />
                    Sistem hanya akan membaca tabel Excel dalam format (.XLS) <br /></td>
                        </tr>
                        <tr>
                          <td height="66">
                              <div id="txt-h1">File Upload : 
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <%--<asp:Label ID="mlLBLSHEETS" runat="server" Text="Select Sheet" />
                                <asp:DropDownList ID="mlDDLSHEETS" runat="server" AppendDataBoundItems = "true"></asp:DropDownList>--%>
                                <asp:Button ID="mlBTNUPLOAD" runat="server" Text="Upload" Width="100px" OnClick="mlBTNUPLOAD_Click" />
                                <asp:Button ID="mlBTNCANCEL" runat="server" Text="Cancel" Width="100px" OnClick="mlBTNCANCEL_Click" />
                              </div>
                          </td>
                        </tr>
                        <tr>
                          <td>&nbsp;</td>
                        </tr>
                      </table>
                    </div>
                    <div id="space"></div>
                    <div id="space"></div>
                    <div id="space"></div>

                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </form>

</asp:Content>

