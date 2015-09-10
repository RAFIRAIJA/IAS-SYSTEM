<%@ Page Title="" Language="VB" MasterPageFile="~/PageSetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_rpt_mr_request_revisi.aspx.vb" Inherits="pj_AP_ap_rpt_mr_request_revisi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../usercontroller/ucLookUpSiteCard.ascx" tagname="ucLUSitecard" tagprefix="uc1" %>
<%@ Register src="../usercontroller/ucLookUpItem.ascx" tagname="ucLUItem" tagprefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="../script/calendar.js"></script>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <script type="text/javascript">
        function OpenWinLookUpSiteCard_From(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn, pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }
        function OpenWinLookUpSiteCard_To(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn, pEntity, pStyle) {
            var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }

        function OpenWinLookUpItem_From(pItemNo, pItemName, pItemNoHdn, pItemNameHdn, pTemplate, pStyle) {
            var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUp_Item.aspx?hdnItemNo=' + pItemNoHdn + '&txtItemNo=' + pItemNo + '&hdnItemName=' + pItemNameHdn + '&txtItemName=' + pItemName + '&templateItem=' + pTemplate, 'UserLookup', 'left=100, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
        }
        function OpenWinLookUpItem_To(pItemNo, pItemName, pItemNoHdn, pItemNameHdn, pTemplate, pStyle) {
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

        <input type="hidden" id="hdnItemNo_From" runat="server" name="hdnItemNo" class="inptype"/>
        <input type="hidden" id="hdnItemName_From" runat="server" name="hdnItemName" class="inptype"/>
        <input type="hidden" id="hdnItemNo_TO" runat="server" name="hdnItemNo" class="inptype"/>
        <input type="hidden" id="hdnItemName_TO" runat="server" name="hdnItemName" class="inptype"/>

        <asp:Panel ID="pnTOOLBAR" runat="server">  
            <table border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td valign="top">
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btExCsv" ToolTip="csv" ImageUrl="~/images/toolbar/csvfile.png" runat="server"  Visible="false"/>&nbsp; 
                        <asp:ImageButton id="btPrint" ToolTip="csv" ImageUrl="~/images/toolbar/iconprinter.jpg" runat="server"  />
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

        <asp:Panel ID="pnlFILL" runat ="server">
            <table width="100%" cellpadding="2" cellspacing="1" border="0" >
                <tr>
                    <td class ="tdganjil" width="15%">
                        Entity
                    </td>
                    <td class ="tdganjil" width="35%">
                        <asp:DropDownList runat ="server" ID="ddlEntity">

                        </asp:DropDownList>
                    </td>
                    <td class ="tdganjil" colspan="2">
                        <asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" />
                    </td>                    
                </tr>
                <tr>
                    <td class ="tdganjil" width="15%">
                        Date (From)
                    </td>
                    <td class ="tdganjil" width="35%">
                        <asp:TextBox ID="txDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                    
                        <input id="btDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE1, 'dd/mm/yyyy', this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                        <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="bt_ajDOCDATE1" TargetControlID="txDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                        <font color="blue">dd/mm/yyyy</font>
                    </td>
                    <td class ="tdganjil" width="15%">
                        Date (To)
                    </td>
                    <td class ="tdganjil" >
                        <asp:TextBox ID="txDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                        <input id="btJOINDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE2, 'dd/mm/yyyy', this)" type="button" value="D" style="background-color:Yellow " />                                
                        <asp:ImageButton runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="bt_ajDOCDATE2" TargetControlID="txDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                        <font color="blue">dd/mm/yyyy</font>
                    </td>
                </tr>
                <tr>
                    <td class ="tdganjil">
                        MR No (From)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txDOCUMENTNO1" runat="server" Width="150" CssClass ="inptype"></asp:TextBox>
                    </td>
                    <td class ="tdganjil">
                        MR No (To)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txDOCUMENTNO2" runat="server" Width="150" CssClass ="inptype "></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class ="tdganjil">
                        Job Task {No] (From)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="mpJobNo_From" runat="server" Width="80" CssClass ="inptype "></asp:TextBox>&nbsp;-&nbsp;
                        <asp:TextBox ID="mpJobTaskNo_From" runat="server" Width="60" CssClass ="inptype "></asp:TextBox>
                        <asp:hyperlink id="hpLookupJobTask_From" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
                    </td>
                    <td class ="tdganjil">
                        Job Task {No] (To)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="mpJobNo_TO" runat="server" Width="80" CssClass ="inptype "></asp:TextBox>&nbsp;-&nbsp;
                        <asp:TextBox ID="mpJobTaskNo_TO" runat="server" Width="60" CssClass ="inptype "></asp:TextBox>
                        <asp:hyperlink id="hpLookupJobTask_TO" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>

                    </td>
                </tr>
                <tr>
                    <td class ="tdganjil">
                        SiteCard (From)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txLOCID1" runat="server" CssClass ="inptype "></asp:TextBox>                                    
                        <asp:ImageButton ID="btLOCID1" ToolTip="Loc ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                        <asp:TextBox ID="txLOCID1_Desc" runat="server" CssClass ="inptype "></asp:TextBox>                                    
                    </td>
                    <td class ="tdganjil">
                        SiteCard (TO)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txLOCID2" runat="server"></asp:TextBox>                                    
                        <asp:ImageButton ID="btLOCID2" ToolTip="Loc ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                        <asp:TextBox ID="txLOCID2_Desc" runat="server" CssClass ="inptype "></asp:TextBox>                                    
                    </td>
                </tr>
                <tr>
                    <td class ="tdganjil">
                        Item No (From)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txITEM1" runat="server" Width="70px" CssClass ="inptype"></asp:TextBox>
                        <asp:HyperLink ID="hpLookUpItem_From" ToolTip="Item Product" ImageUrl="~/images/toolbar/find.jpg" runat="server" />
                        <asp:TextBox runat="server" ID="txtItemName_From" Width="250px" CssClass="inptype" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lbITEMDESC1"  runat="server" Visible ="false" ></asp:Label>            
                    </td>
                    <td class ="tdganjil">
                        Item No (TO)
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txITEM2" runat="server" Width="70px" CssClass ="inptype"></asp:TextBox>
                        <asp:HyperLink ID="hpLookUpItem_TO" ToolTip="Item Product" ImageUrl="~/images/toolbar/find.jpg" runat="server" />
                        <asp:TextBox runat="server" ID="txtItemName_TO" Width="200px" CssClass="inptype" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lbITEMDESC2"  runat="server" Visible ="false" ></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td class ="tdganjil">
                        Create User ID
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="txUSERID" runat="server" CssClass ="inptype"></asp:TextBox>                                
                        <asp:ImageButton ID="btUSERID" ToolTip="Employee ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                        <asp:Label ID="lbNAME" runat="server"></asp:Label>  
                    </td>
                    <td class ="tdganjil">
                        Periode MR
                    </td>
                    <td class ="tdganjil">
                        <asp:TextBox ID="mpPERIOD" runat="server" Width="100"></asp:TextBox>                                                                    
                        <input id="btPERIOD" runat="server" onclick="displayCalendar(mpCONTENT_mpPERIOD, 'mm/yyyy', this)" type="button" value="D" style="background-color:Yellow " />                                                      
                        <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="mpPERIOD" TargetControlID="mpPERIOD" Format="MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil">
                        Status
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="ddSTATUS" runat="server"></asp:DropDownList>
                    </td>
                    <td class="tdganjil">
                        Report Type
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList ID="ddREPORT" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlGRID" runat="server">
    <asp:DataGrid runat="server" ID="mlDATAGRID"
    CssClass="Grid"
    OnItemCommand="mlDATAGRID_ItemCommand"
    
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    AllowSorting="True"
    OnSortCommand="mlDATAGRID_SortCommand"    
    OnItemDataBound ="mlDATAGRID_ItemBound"    
    AllowPaging="True"    
    PagerStyle-Mode="NumericPages"
    PagerStyle-HorizontalAlign="center"
    OnPageIndexChanged="mlDATAGRID_PageIndex"  
    PageSize="50"  
    >	    
    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>
    
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK1" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("DocNo")) %>' text="VW"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>
        
        <asp:templatecolumn headertext="LG">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK2" navigateurl='<%# String.Format("ap_doc_mr_entrylog.aspx?mpID={0}&mpID2={1}", Eval("DocNo"), Eval("Site")) %>' text="Log"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>
        
    </Columns>
 </asp:DataGrid>  
 
</asp:Panel>
    </form> 
</asp:Content>

