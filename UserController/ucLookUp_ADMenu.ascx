<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUp_ADMenu.ascx.cs" Inherits="UserController_ucLookUp_ADMenu" %>
<script type="text/javascript">
    function OpenWinLookUp(pMenuParentIDHid, pMenuParentID, pMenuParentName, pMenuParentNameHid, pStyle) {
        var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
        var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
        window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_ADMenu.aspx?style=' + pStyle + '&hdnMenuParentID=' + pMenuParentIDHid + '&txtMenuParentID=' + pMenuParentID + '&hdnMenuParentName=' + pMenuParentNameHid + '&txtMenuParentName=' + pMenuParentName, 'UserLookup', 'left=50, top=10, width=900, height=900, menubar=0, scrollbars=yes');
}
function manualInput(txtMenuParentID, hdnMenuParentID, hdnDesc) {
    hdnMenuParentID.value = txtMenuParentID.value;
    hdnDesc.value = '-';
}
</script>
<asp:Panel ID="pnlMenuParent" runat="server" Width="85%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtMenuParentID" cssclass="inptype" runat="server" ReadOnly="True" Width="50px"></asp:textbox>&nbsp;
			    <asp:textbox id="txtMenuParentName" cssclass="inptype" runat="server" ReadOnly="True" Width="150px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
			    <input type="hidden" id="hdnMenuParentID" runat="server" name="hdnMenuParentID" class="inptype"/>
			    <input type="hidden" id="hdnMenuParentName" runat="server" name="hdnMenuParentName" class="inptype"/>
			    <asp:requiredfieldvalidator id="RFVMenuParent" runat="server" ErrorMessage="Fill..MenuParent" ControlToValidate="txtMenuParentID"
				    Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>