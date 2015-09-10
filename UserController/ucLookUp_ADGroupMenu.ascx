<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUp_ADGroupMenu.ascx.cs" Inherits="UserController_ucLookUp_ADGroupMenu" %>
<script type="text/javascript">
    function OpenWinLookUp(pGroupMenuHid, pGroupMenu, pStyle) {
        var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
        var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
        window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_ADGroupMenu.aspx?style=' + pStyle + '&hdnGroupMenu=' + pGroupMenuHid + '&txtGroupMenu=' + pGroupMenu, 'UserLookup', 'left=50, top=10, width=900, height=700, menubar=0, scrollbars=yes');
    }
    
</script>
<asp:Panel ID="pnlGroupMenu" runat="server" Width="85%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtGroupMenu" cssclass="inptype" runat="server" ReadOnly="True" Width="150px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
			    <input type="hidden" id="hdnGroupMenu" runat="server" name="hdnMenuParentID" class="inptype"/>&nbsp;
			    <asp:requiredfieldvalidator id="RFVGroupmenu" runat="server" ErrorMessage="Fill..GroupMenu" ForeColor="Red" ControlToValidate="txtGroupMenu"
				    Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>