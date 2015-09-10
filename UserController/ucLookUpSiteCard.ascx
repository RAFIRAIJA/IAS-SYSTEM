<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUpSiteCard.ascx.cs" Inherits="UserController_ucLookUpSiteCard" %>

<script type="text/javascript">
    function OpenWinLookUp(pSiteCardID, pSiteCardName, pSiteCardIDHdn,pSiteCardNameHdn, pJobNo, pJobTaskNo,pJobNoHdn,pJobTaskNoHdn, pStyle) {
			var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
			var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
			window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn, 'UserLookup', 'left=100, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
			}
	function manualInput(txtSiteCardID,hdnSiteCardID,hdnDesc){
	    hdnSiteCardID.value = txtSiteCardID.value;
	    hdnDesc.value = '-';
	}
</script>
<link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../script/JavaScript/Elsa.js"></script>
<script type="text/javascript" src="../script/JavaScript/Eloan.js"></script>

<asp:Panel ID="pnlSiteCard" runat="server" Width="50%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtJobNo" cssclass="inptype" runat="server" ReadOnly="True" Width="70px"></asp:textbox>&nbsp;
			    <asp:textbox id="txtJobTaskNo" cssclass="inptype" runat="server" ReadOnly="True" Width="60px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>&nbsp;&nbsp;
                <asp:textbox runat="server" ID="lblSiteCard" cssclass="inptype" runat="server" ReadOnly="True" Width="80px" ></asp:textbox>&nbsp;
                <asp:textbox runat="server" ID="lblSiteCardName" cssclass="inptype" runat="server" ReadOnly="True" Width="100px" ></asp:textbox>&nbsp;
			    <input type="hidden" id="hdnSiteCardID" runat="server" name="hdnSiteCardID" class="inptype"/>
			    <input type="hidden" id="hdnSiteCardName" runat="server" name="hdnSiteCardName" class="inptype"/>
			    <input type="hidden" id="hdnJobNo" runat="server" name="hdnJobNo" class="inptype"/>
			    <input type="hidden" id="hdnJobTaskNo" runat="server" name="hdnJobTaskNo" class="inptype"/>
			    <asp:requiredfieldvalidator id="RFVSitecard" runat="server" ErrorMessage="Fill...the blank JobNo..!!" ForeColor="Red" Font-Size="10" ControlToValidate="txtJobNo"
				Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>