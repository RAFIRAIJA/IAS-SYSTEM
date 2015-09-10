<%@ Page Language="C#" MasterPageFile="~/pagesetting/MasterInternCS.master" AutoEventWireup="false" CodeFile="Worksheet.aspx.cs" Inherits="Worksheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <link rel="stylesheet" href="../script/style-page.css" type="text/css" media="all" />
    <script type="text/javascript" src="../script/calendar.js"></script>

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
                    <div id="lblTitle" style="font-size:16px;"><strong>TODAY'S REPORT</strong></div>
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
                    <div id="lblSpace1"></div>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                    <div id="lblTitle1" style="font-size:16px;"><strong>DATABASE</strong></div>
                    <div id="space"></div>
                    <asp:Panel ID="mlPNLGRID" runat="server">
                        <asp:DataGrid runat="server" ID="mlDGWORKSHEET" 
                        CssClass="Grid"
                           
                        autogeneratecolumns="false">	    
                            <%--OnItemCommand="mlDGWORKSHEET_ItemCommand" --%>
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <EditItemStyle  CssClass="GridItem" />
                        <PagerStyle  CssClass="GridItem" />
                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                        <Columns>
    
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                <asp:CheckBox id="mlCHECKBOX" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>   

                            <asp:templatecolumn headertext="No">
                                <ItemTemplate>        
                                    <%#Container.ItemIndex + 1%>
                                </ItemTemplate>        
                            </asp:templatecolumn>               
                   
                            <%--<asp:templatecolumn headertext="Invoice No">
                                <ItemTemplate>        
                                    <asp:hyperlink  Target="_blank"  runat="server" id="Hyperlink2" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("InvNo")) %>' text='<%# Eval("InvNo") %>'></asp:hyperlink>
                                </ItemTemplate>        
                            </asp:templatecolumn>--%>
        
                            <asp:BoundColumn HeaderText="Invoice No" DataField="InvNo"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Code" DataField="InvCustCode"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Name" DataField="InvCustName"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice Date" DataField="InvDate"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Branch" DataField="InvBranch"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Amount" DataField="InvAmount" DataFormatString ="{0:n}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Status" DataField="InvStatus"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Receipt Code" DataField="InvReceiptCode"></asp:BoundColumn>    
                            <asp:BoundColumn HeaderText="Proceeds" DataField="InvProceedsDate" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Delivered" DataField="InvDeliveredDate" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Returned" DataField="InvReturnedDate" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Done" DataField="InvDoneDate" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Penerima" DataField="InvCustPenerima"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger NIK" DataField="InvCodeMess"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger Name" DataField="InvMessName"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User NIK" DataField="UserID"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User Name" DataField="UserName"></asp:BoundColumn>
    
                        </Columns>
                        </asp:DataGrid>
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
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                              </div>
                            <%--<form action="" method="post" enctype="multipart/form-data" name="upload" id="upload" onsubmit="return cek();">
                                <div id="txt-h1">File Upload : 
                                <input name="userfile" type="file" />
                                <input name="upload" type="submit" value="Upload" />
                                <input type="reset" name="Reset" id="button" value="Cancel" />
                                </div>
                            </form>--%>
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
