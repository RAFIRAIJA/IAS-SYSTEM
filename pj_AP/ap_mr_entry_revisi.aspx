<%@ Page Title="" Language="VB" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="false" CodeFile="ap_mr_entry_revisi.aspx.vb" Inherits="pj_AP_ap_mr_entry_revisi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>
<%@ Register src="../usercontroller/ucLookUpSiteCard.ascx" tagname="ucLUSitecard" tagprefix="uc1" %>
<%@ Register src="../usercontroller/ucLookUpItem.ascx" tagname="ucLUItem" tagprefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    
    <link href="~/script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="~/script/calendar.js"></script>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <script type="text/javascript">
        function OpenWinLookUpSiteCard(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn,pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity='+ pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }

        function OpenWinLookUpItem(pItemNo, pItemName, pItemNoHdn, pItemNameHdn, pTemplate, pStyle) {
            var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUp_Item.aspx?hdnItemNo=' + pItemNoHdn + '&txtItemNo=' + pItemNo + '&hdnItemName=' + pItemNameHdn + '&txtItemName=' + pItemName + '&templateItem=' + pTemplate, 'UserLookup', 'left=100, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
        }

    </script>
    <script type="text/javascript" language="Javascript">
    <!-- hide script from older browsers
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

        <input type="hidden" id="hdnSiteCardID" runat="server" name="hdnSiteCardID" class="inptype"/>
        <input type="hidden" id="hdnSiteCardName" runat="server" name="hdnSiteCardName" class="inptype"/>
        <input type="hidden" id="hdnJobNo" runat="server" name="hdnJobNo" class="inptype"/>
        <input type="hidden" id="hdnJobTaskNo" runat="server" name="hdnJobTaskNo" class="inptype"/>
        <input type="hidden" id="hdnItemNo" runat="server" name="hdnItemNo" class="inptype"/>
        <input type="hidden" id="hdnItemName" runat="server" name="hdnItemName" class="inptype"/>


        <asp:Panel ID="pnTOOLBAR" runat="server">  
            <table border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td valign="top">
                        <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />
                    </td>            
                </tr>    
                <tr>
                    <td valign="top">
                        <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
                        <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                        <asp:HiddenField ID="mlSYSCODE" runat="server"/>
                        <p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p>
                    </td>
                </tr>           
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDATALIST" >
            <table runat="server" width="100%" border ="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="mlDGDATALIST" runat="server" autogeneratecolumns="false" HeaderStyle-BackColor="orange" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="top"
                                OnItemCommand="mlDGDATALIST_ItemCommand" 
                                OnItemDataBound="mlDGDATALIST_ItemDataBound">
                                <AlternatingItemStyle BackColor="#F9FCA8" />
                                <Columns>                                    
                                    <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") & "#" &  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")  %>' CommandName="BrowseRecord">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>           
        
                                    <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") + "#" +  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")%>' CommandName="EditRecord">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>   
        
                                     <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") & "#" &  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>    
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="DocNo" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Date" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>

                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="MRType" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="SiteCardID" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteCardID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCardID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobNo" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobTaskNo" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobTaskNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Period" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Period")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Address" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Address")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="City" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="MRStatus" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Created" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreateID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreateID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn headertext="VW" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>        
                                        <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("DocNo")) %>' text="VW"></asp:hyperlink>
                                    </ItemTemplate>
                                    </asp:templatecolumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                <tr>
                    <td colspan ="4">
                        <br />
                        <p><i>
                        Keterangan MR Status :  <br />
                        Wait1 = Permintaan Baru, Menunggu Proses Review <br />
                        Wait2 = Selesai Review, Menunggu Proses Authorize <br />
                        New = Selesai Authorize, Menunggu Proses Procurement <br />
                        </i></p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlTemplate" runat="server">
            <table runat="server" width="100%" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="tdganjil" width="15%">
                        Template
                    </td>
                    <td class="tdganjil" colspan ="3">
                        <asp:DropDownList runat="server" ID="mpMR_TEMPLATE">
                        </asp:DropDownList>&nbsp;
                        <asp:ImageButton ID="btSUBMITTEMPLATE" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                    </td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlFILL">
            <table runat="server" width="100%" border ="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="tdganjil" width="15%">
                        Entity
                    </td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat ="server" ID="ddlEntity">

                        </asp:DropDownList>
                    </td>
                    <td class="tdganjil" width="15%" rowspan ="6" valign="top">
                        Alamat Pengiriman
                    </td>
                    <td class="tdganjil" rowspan ="6">
                        <asp:TextBox runat ="server" ID="txADDR" CssClass ="inptype" TextMode ="MultiLine" Height="160" Width="350px">
                        </asp:TextBox>                        
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        No MR
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat ="server" ID="mpDOCUMENTNO" CssClass ="inptype" Width="150px" Text ="--AUTONUMBER--" BackColor ="#ffffcc"  >
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Tanggal
                    </td>                    
                    <td class="tdganjil" >
                        <asp:TextBox ID="mpDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                    
                        <input id="btDOCDATE" runat="server" onclick="displayCalendar(mpCONTENT_mpDOCDATE, 'dd/mm/yyyy', this)" type="button" value="D" style="background-color:Yellow " />                                                      
                        <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                        <%--<ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mpDOCDATE" TargetControlID="mpDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> --%>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Job [Task] No
                    </td>                    
                    <td class="tdganjil">
                        <%--<uc1:ucLUSiteCard runat="server" id="LUSiteCard"></uc1:ucLUSiteCard>--%>
                        <asp:TextBox ID="mpJobNo" runat="server" Width="80" ></asp:TextBox>&nbsp;-&nbsp;
                        <asp:TextBox ID="mpJobTaskNo" runat="server" Width="60" ></asp:TextBox>
                        <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil">
                        Site Card
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox ID="mpSITECARD" runat="server" Width="100" ></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imbRefresh" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />&nbsp;
                        <asp:Label ID="mpSITEDESC" CssClass ="inptype" Text="Site Desc" runat="server" Font-Size ="8"  ></asp:Label>
                    </td> 
                </tr>
                <tr>
                     <td class="tdganjil">
                        Dept.
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="mpDEPT" runat="server" Font-Size="11px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Periode MR
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlPeriodeMR" AutoPostBack="true">
                            
                        </asp:DropDownList>&nbsp;-&nbsp;
                        <asp:TextBox Visible="false" ID="mpPERIOD" runat="server" Width="20"></asp:TextBox>                                                                                                    
                        <input visible="false" id="btPERIOD" runat="server" onclick="displayCalendar(mpCONTENT_mpPERIOD, 'mm/yyyy', this)" type="button" value="D" style="background-color:Yellow " />                                                      
                        <asp:ImageButton Visible="false" runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="mpPERIOD" TargetControlID="mpPERIOD" Format="MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
                        <font color="blue">cth MR agst: 08/2013</font>  
                    </td>
                    <td class="tdganjil" width="15%">
                        
                    </td>
                    <td class="tdganjil ">
                        <asp:CheckBox runat ="server" ID="mpLOCSAVE" Text ="Informasikan HO untuk Update Alamat Site Card"  Font-Italic="true" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" rowspan ="5" valign="top">
                        Keterangan
                    </td>
                    <td class="tdganjil" rowspan ="5" valign="top">
                        <asp:TextBox runat ="server" ID="mpDESC" Width="350px" Height="140" TextMode ="MultiLine" ></asp:TextBox>
                    </td>
                    <td class="tdganjil" >
                        Kota
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox ID="txCITY" width="350px" runat="server" CssClass ="inptype" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Propinsi
                    </td>
                    <td class="tdganjil" >
                        <asp:DropDownList ID="ddSTATE" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Negara
                    </td>
                    <td class="tdganjil" >
                        <asp:DropDownList ID="ddCOUNTRY" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Kode Pos
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox ID="txZIP" width="350px" runat="server" CssClass ="inptype" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Telp
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox ID="txPHONE1" width="350px" runat="server" CssClass ="inptype" />
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" width="50%" class="tdganjil">
                        <p><b><i>Diisi bila Request dilakukan oleh Internal HO</i></b></p>
                    </td>
                    <td class ="tdganjil">
                        PIC & HP
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txPHONE1_PIC" width="350px" runat="server" CssClass ="inptype" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Dept Code
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="ddDEPTCODE" runat="server" Font-Size="11px"></asp:DropDownList>
                    </td> 
                     <td class="tdganjil">

                    </td>
                    <td class="tdganjil">
                        <asp:TextBox ID="mpLOCATION" runat="server" Width="350px" Height="30" TextMode="MultiLine" Visible ="false"  ></asp:TextBox>    
                    </td>
                </tr>
                <tr id="tr2" runat="server" visible ="false" >
                    <td class ="tdganjil"><asp:Label ID="lbPERCENTAGE" Text="MR %" runat="server"></asp:Label></td>
                    <td class ="tdganjil"><asp:TextBox ID="txPERCENTAGE" runat="server" Width="50" ></asp:TextBox><b>%</b></td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGRID" Visible ="false" >
            <table runat="server" width="100%" border ="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class ="tdganjil" width="15%">
                        Item Selection
                    </td>
                    <td class ="tdganjil" colspan="3">
                        <asp:TextBox runat="server" ID="txtItemNo" Width="80px" CssClass="inptype" ReadOnly="true"></asp:TextBox>&nbsp;
                        <asp:hyperlink id="hlLookUp" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>&nbsp;
                        <asp:ImageButton ID="imbSubMit" ToolTip="Submit Item" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server"  />&nbsp;
                        <asp:TextBox runat="server" ID="txtItemName" Width="350px" CssClass="inptype" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <caption>
                    <br />
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="mlDATAGRIDITEMLIST" runat="server" autogeneratecolumns="false" HeaderStyle-BackColor="orange" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="top"
                                OnItemCommand="mlDATAGRIDITEMLIST_ItemCommand" 
                                OnItemDataBound="mlDATAGRIDITEMLIST_ItemDataBound">
                                <AlternatingItemStyle BackColor="#F9FCA8" />
                                <Columns>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="VW" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Hyperlink2" runat="server" navigateurl='<%# String.Format("../pj_in/in_imagedoc.aspx?mpID={0}", Eval("ItemKey")) %>' Target="_blank" text="Img"></asp:HyperLink>
                                            <asp:Label ID="lblFlagView" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagView")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="DELETE" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="../images/toolbar/delete.jpg"  />
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="7%" headertext="Item Key" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%--<uc2:ucLUItem runat="server" ID="luItem" />--%>
                                            <asp:Label ID="lblItemkey" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemKey")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn headertext="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Satuan" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSatuan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "uom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Ukuran" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fsize")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Qty" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="inptype" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="15%" headertext="Saldo di Area" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <uc4:ucInputNumber ID="ucBalanceAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BalanceAmount")%>' />
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="8%" headertext="Keterangan" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeterangan" runat="server" CssClass="inptype" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </caption>
            </table>
        </asp:Panel>
    </form>
</asp:Content>

