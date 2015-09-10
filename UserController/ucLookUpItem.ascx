<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUpItem.ascx.cs" Inherits="UserController_ucLookUpItem" %>

<script type="text/javascript">
    function OpenWinLookUp(pItemNo, pItemName, pItemNoHdn, pItemNameHdn, pStyle) {
        var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
        var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
        window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_Item.aspx?hdnItemNo=' + pItemNoHdn  + '&ItemNo=' + pItemNo + '&hdnItemName=' + pItemNameHdn + '&ItemName=' + pItemName , 'UserLookup', 'left=100, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
        }
        function manualInput(txtSiteCardID, hdnSiteCardID, hdnDesc) {
            hdnSiteCardID.value = txtSiteCardID.value;
            hdnDesc.value = '-';
        }
</script>
<link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../script/JavaScript/Elsa.js"></script>
<script type="text/javascript" src="../script/JavaScript/Eloan.js"></script>

<asp:Panel ID="pnlItem" runat="server" Width="50%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtItemNo" cssclass="inptype" runat="server" ReadOnly="True" Width="60px"></asp:textbox>&nbsp;
			    <asp:textbox id="txtItemName" cssclass="inptype" runat="server" ReadOnly="True" Width="120px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>&nbsp;&nbsp;
			    <input type="hidden" id="hdnItemNo" runat="server" name="hdnItemNo" class="inptype"/>
			    <input type="hidden" id="hdnItemName" runat="server" name="hdnItemName" class="inptype"/>
			    <asp:requiredfieldvalidator id="RFVSitecard" runat="server" ErrorMessage="Fill...the blank Item..!!" ForeColor="Red" Font-Size="10" ControlToValidate="txtItemNo"
				Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>