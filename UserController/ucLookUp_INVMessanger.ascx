<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUp_INVMessanger.ascx.cs" Inherits="usercontroller_ucLookUp_INVMessanger" %>

<script type="text/javascript">
    function OpenWinLookUp(pMessangerIDHid, pMessangerID, pMessangerName, pMessangerNameHid, pStyle) {
        var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
        var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
        window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_INVMessanger.aspx?style=' + pStyle + '&hdnMessangerID=' + pMessangerIDHid + '&txtMessangerID=' + pMessangerID + '&hdnMessangerName=' + pMessangerNameHid + '&txtMessangerName=' + pMessangerName, 'UserLookup', 'left=50, top=10, width=900, height=600, menubar=0, scrollbars=yes');
}
function manualInput(txtMessangerID, hdnMessangerID, hdnDesc) {
    hdnMessangerID.value = txtMessangerID.value;
    hdnDesc.value = '-';
}
</script>
<asp:Panel ID="pnlMessanger" runat="server" Width="75%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtMessangerID" cssclass="inptype" runat="server"  Width="50px"></asp:textbox>&nbsp;
			    <asp:textbox id="txtMessangerName" cssclass="inptype" runat="server" Width="150px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
			    <input type="hidden" id="hdnMessangerID" runat="server" name="hdnMessangerID" class="inptype"/>
			    <input type="hidden" id="hdnMessangerName" runat="server" name="hdnMessangerName" class="inptype"/>
			    <asp:requiredfieldvalidator id="RFVMessanger" runat="server" ErrorMessage="Fill...the blank area..!!" ControlToValidate="txtMessangerID"
				    Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>