<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="fa_moving_entry.aspx.cs" Inherits="pj_FA_fa_moving_entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>
<%@ Register src="../usercontroller/ucLookUpSiteCard.ascx" tagname="ucLUSitecard" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="~/script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="~/script/calendar.js"></script>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <script type="text/javascript">
        function OpenWinLookUpSiteCard_From(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn, pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
                    var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
                    window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }
        function OpenWinLookUpSiteCard_To(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn, pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }
        function OpenWinLookUpAsset(pAssetID, pAssetDesc, pAssetIDHdn, pAssetDescHdn, pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_Asset.aspx?hdnAssetID=' + pAssetIDHdn + '&AssetID=' + pAssetID + '&hdnAssetDesc=' + pAssetDescHdn + '&AssetDesc=' + pAssetDesc + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
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


    <form runat ="server" id="frmReport">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
        
        <input type="hidden" id="hdnSiteCardID_From" runat="server" name="hdnSiteCardID" class="inptype"/>
        <input type="hidden" id="hdnSiteCardName_From" runat="server" name="hdnSiteCardName" class="inptype"/>
        <input type="hidden" id="hdnJobNo_From" runat="server" name="hdnJobNo" class="inptype"/>
        <input type="hidden" id="hdnJobTaskNo_From" runat="server" name="hdnJobTaskNo" class="inptype"/>

        <input type="hidden" id="hdnSiteCardID_TO" runat="server" name="hdnSiteCardID" class="inptype"/>
        <input type="hidden" id="hdnSiteCardName_TO" runat="server" name="hdnSiteCardName" class="inptype"/>
        <input type="hidden" id="hdnJobNo_TO" runat="server" name="hdnJobNo" class="inptype"/>
        <input type="hidden" id="hdnJobTaskNo_TO" runat="server" name="hdnJobTaskNo" class="inptype"/>

        <input type="hidden" id="hdnAssetID" runat="server" name="hdnAssetID" class="inptype"/>
        <input type="hidden" id="hdnAssetDesc" runat="server" name="hdnAssetDesc" class="inptype"/>

        <asp:Panel ID="pnTOOLBAR" runat="server">  
            <table border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td valign="top">
                        <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" OnClick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btPrint" ToolTip="csv" ImageUrl="~/images/toolbar/iconprinter.jpg" runat="server"  />
                    </td>            
                </tr>    
                <tr>
                    <td valign="top">
                        <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
                        <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                        <asp:HiddenField ID="mlSYSCODE" runat="server"/>
                        <p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p>
                        <asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" />
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
                    <td class="tdganjil" width="15%">Search By</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.DocNo">Request No</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
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
        <asp:Panel runat="server" ID="pnlDATALIST" >
            <table runat="server" width="100%" border ="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="mlDGDATALIST" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
                            onitemdatabound="mlDGDATALIST_ItemDataBound" 
                            OnItemCommand="mlDGDATALIST_ItemCommand">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap" ></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>                                    
                                    <%--<asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandName="BrowseRecord">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn> --%>          
        
                                    <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center" HeaderText="EDIT">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandName="EditRecord">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>   
        
                                     <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center" HeaderText="DEL">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg"  OnClientClick="return confirm('Delete Record ?');">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>  

                                    <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center" HeaderText="RET">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btReturnRecord" Runat="server" AlternateText="Return" ImageUrl="~/images/toolbar/return.jpg"  OnClientClick="return confirm('Delete Record ?');">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>  

                                    <asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center" HeaderText="EXT">
                                        <ItemTemplate>
                                        <asp:imagebutton id="btExtendRecord" Runat="server" AlternateText="Extend" ImageUrl="~/images/toolbar/extend.jpg"  OnClientClick="return confirm('Delete Record ?');">
                                        </asp:imagebutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>  
                                     
                                    <asp:templatecolumn headertext="VW" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>        
                                        <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("DocNo")) %>' text="VW"></asp:hyperlink>
                                    </ItemTemplate>
                                    </asp:templatecolumn>                                     
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Req.No" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>                                    
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Periode Req" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Period")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Req.Status" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>                                    
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="User ID" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="User Name" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="SiteCard From" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteCardIDFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCardIDFrom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobNo From" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNoFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobNoFrom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobTaskNo From" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTaskNoFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobTaskNoFrom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="SiteCard To" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteCardIDTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCardIDTo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobNo To" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNoTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobNoTo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="JobTaskNo To" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTaskNoTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobTaskNoTo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Keterangan" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>                                    
                                    <asp:templatecolumn HeaderStyle-Width="5%" headertext="Aging" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAging" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AgingDays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="4">
                            <uc2:ucPaging runat="server" id="pagingRequest" 
                                OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                        </td>
                    </tr>
                <tr>
                    <td colspan ="4">
                        <br />
                        <p><i>
                        Keterangan Moving Status :  <br />
                        Wait1 = Permintaan Baru, Menunggu Proses Review <br />
                        Wait2 = Selesai Review, Menunggu Proses Authorize <br />
                        New = Selesai Authorize, Menunggu Proses Upload to NAV <br />
                        </i></p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlTemplate" runat="server" Visible="false">
            <table runat="server" width="100%" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="tdganjil" width="15%">
                        Template
                    </td>
                    <td class="tdganjil" colspan ="3">
                        <asp:DropDownList runat="server" ID="mpMR_TEMPLATE">
                        </asp:DropDownList>&nbsp;
                        <asp:ImageButton ID="btSUBMITTEMPLATE" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" OnClick="btSUBMITTEMPLATE_Click" />  
                    </td>
                </tr>
                <tr>
                    <td>
                        Type Request
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdblTypeRequest" OnSelectedIndexChanged="rdblTypeRequest_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Value="Mutasi" Selected="True">Mutasi Asset</asp:ListItem>
                            <asp:ListItem Value="Moving" Selected="False">Moving Asset</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlFILL" Visible="false">
            <table runat="server" width="100%" border ="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="tdganjil" width="15%">
                        Entity
                    </td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat ="server" ID="ddlEntity">
                            <asp:ListItem Value="ISSN3">ISS</asp:ListItem>
                            <asp:ListItem Value="IFS3">IFS</asp:ListItem>
                            <asp:ListItem Value="ICS3">ICS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        No Request
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat ="server" ID="mpDOCUMENTNO" CssClass ="inptype" Width="150px" Text ="--AUTONUMBER--" BackColor ="#ffffcc"  >
                        </asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Tanggal
                    </td>                    
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucReqDate" />
                    </td>
                    <td class="tdganjil">
                        Periode Req
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlPeriodeReq" AutoPostBack="true">
                            
                        </asp:DropDownList>&nbsp;-&nbsp;
                        <font color="blue">cth Req agst: 08/2013</font>  
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Job [Task] No - From
                    </td>                    
                    <td class="tdganjil">
                        <%--<uc1:ucLUSiteCard runat="server" id="LUSiteCard"></uc1:ucLUSiteCard>--%>
                        <asp:TextBox ID="mpJobNoFrom" runat="server" Width="80" ></asp:TextBox>&nbsp;-&nbsp;
                        <asp:TextBox ID="mpJobTaskNoFrom" runat="server" Width="60" ></asp:TextBox>
                        <asp:hyperlink id="hpLookupFrom" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
                    </td>   
                    <td class="tdganjil" width="15%">
                        Job [Task] No - To
                    </td>                    
                    <td class="tdganjil" >
                        <%--<uc1:ucLUSiteCard runat="server" id="LUSiteCard"></uc1:ucLUSiteCard>--%>
                        <asp:TextBox ID="mpJobNoTo" runat="server" Width="80" ></asp:TextBox>&nbsp;-&nbsp;
                        <asp:TextBox ID="mpJobTaskNoTo" runat="server" Width="60" ></asp:TextBox>
                        <asp:hyperlink id="hpLookupTo" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
                    </td>                   
                </tr>
                <tr>
                    <td class="tdganjil">
                        Site Card - From
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox ID="mpSITECARDFrom" runat="server" Width="100" ></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imbRefreshFrom" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />&nbsp;
                        <asp:TextBox ID="mpSITEDESCFrom" runat="server" Width="200" ></asp:TextBox>&nbsp;
                    </td> 
                    <td class="tdganjil">
                        Site Card - To
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox ID="mpSITECARDTo" runat="server" Width="100" ></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imbRefreshTo" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />&nbsp;
                        <asp:TextBox ID="mpSITEDESCTo" runat="server" Width="200" ></asp:TextBox>&nbsp;
                    </td> 
                </tr>                
                <tr>
                    <td class ="tdganjil">
                        PIC & HP
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txPHONE1_PIC" width="350px" runat="server" CssClass ="inptype" />
                    </td>
                    <td class ="tdganjil" rowspan ="2" valign="top">
                        Alamat
                    </td>
                    <td class ="tdganjil" rowspan ="2" valign="top">
                        <asp:TextBox ID="txtAlamat" width="350px" runat="server" CssClass ="inptype" TextMode ="MultiLine" Height ="40" />
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
                    <td class="tdganjil" rowspan ="5" valign="top">
                        Keterangan
                    </td>
                    <td class="tdganjil" rowspan ="5" valign="top">
                        <asp:TextBox runat ="server" ID="mpDESC" Width="350px" Height="80" TextMode ="MultiLine" CssClass ="inptype"></asp:TextBox>
                    </td>
                </tr>
                <tr>
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
                    <td colspan ="2" width="50%" class="tdganjil">
                        <p><b><i>Diisi bila Request dilakukan oleh Internal HO</i></b></p>
                    </td>
                    
                </tr>
                <tr visible="false" >
                    <td class="tdganjil">
                        Dept. From
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="mpDEPTFrom" runat="server" Font-Size="11px"></asp:DropDownList>
                    </td>
                    <td class="tdganjil">
                        Dept. To
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="mpDEPTTo" runat="server" Font-Size="11px"></asp:DropDownList>
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
            </table>
            <asp:Panel runat="server" ID="pnlMovingEntry" Width="100%" Visible="false">
                <hr />
                <table runat="server" width="100%" cellpadding="1" cellspacing="1">
                     <tr>
                        <td colspan ="4" class="tdganjil">
                            <p><b><i>Diisi Khusus untuk Type Request Moving Asset</i></b></p>
                        </td>
                    
                    </tr>
                    <tr>
                        <td class="tdganjil" width="15%">
                            Start Date
                        </td>
                        <td class="tdganjil" width="35%">
                            <uc1:ValidDate runat="server" ID="ucStartDate" />
                        </td>
                        <td class="tdganjil" width="15%">
                            End Date
                        </td>
                        <td class="tdganjil" >
                            <uc1:ValidDate runat="server" ID="ucEndDate" />&nbsp;
                            <asp:ImageButton runat="server" ID="imbCalcDay" CausesValidation="false" ImageUrl="../Images/toolbar/Calc.jpg" OnClick="imbCalcDay_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdganjil" width="15%">
                            Lama Peminjaman
                        </td>
                        <td class="tdganjil" width="35%">
                            <asp:TextBox runat="server" ID="txtLama" Width="60px" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblHari">Hari</asp:Label>
                        </td>
                        <td class="tdganjil" width="15%">
                            Sending Date
                        </td>
                        <td class="tdganjil" >
                            <uc1:ValidDate runat="server" ID="ucSendingDate" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />
            <table runat="server" width="100%" cellpadding="1" cellspacing="1">
                <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="mlDATAGRIDITEMLIST" runat="server" autogeneratecolumns="false" HeaderStyle-BackColor="orange" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="top"
                                OnItemCommand="mlDATAGRIDITEMLIST_ItemCommand" 
                                OnItemDataBound="mlDATAGRIDITEMLIST_ItemDataBound">
                                <AlternatingItemStyle BackColor="#F9FCA8" />
                                <Columns>
                                    <%--<asp:templatecolumn HeaderStyle-Width="3%" headertext="VW" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Hyperlink2" runat="server" navigateurl='<%# String.Format("../pj_in/in_imagedoc.aspx?mpID={0}", Eval("ItemKey")) %>' Target="_blank" text="Img"></asp:HyperLink>
                                            <asp:Label ID="lblFlagView" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagView")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>--%>
                                    <asp:templatecolumn HeaderStyle-Width="3%" headertext="DEL" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="../images/toolbar/delete.jpg"  />
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="3%" headertext="ADD" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbadd" runat="server" CausesValidation="false" CommandName="Add" ImageUrl="../images/toolbar/addnew.jpg"  />
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="45%" headertext="Asset" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:HyperLink runat ="server" ID="hlLookUpAsset" ImageUrl ="../images/toolbar/find.jpg"></asp:HyperLink>
                                            <asp:TextBox ID="txtAssetKey" runat="server" Width="100px" CssClass="inptype"  Text='<%# DataBinder.Eval(Container.DataItem, "AssetID")%>'></asp:TextBox>
                                            <asp:TextBox ID="txtAssetName" runat="server" Width="400px" CssClass="inptype" Text='<%# DataBinder.Eval(Container.DataItem, "AssetDesc")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                     <asp:templatecolumn HeaderStyle-Width="6%" headertext="Quantity" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="inptype" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:templatecolumn HeaderStyle-Width="15%" headertext="Keterangan" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeterangan" runat="server" CssClass="inptype" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
            </table>
        </asp:Panel>


    </form>

</asp:Content>

