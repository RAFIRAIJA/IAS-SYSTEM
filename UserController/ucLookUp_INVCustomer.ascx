<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLookUp_INVCustomer.ascx.cs" Inherits="usercontroller_ucLookUp_INVCustomer" %>

<script type="text/javascript">
    function OpenWinLookUp(pCustomerIDHid, pCustomerID, pCustomerName, pCustomerNameHid, pStyle) {
        var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
    var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
    window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_INVCustomer.aspx?style=' + pStyle + '&hdnCustomerID=' + pCustomerIDHid + '&txtCustomerID=' + pCustomerID + '&hdnCustomerName=' + pCustomerNameHid + '&txtCustomerName=' + pCustomerName, 'UserLookup', 'left=50, top=10, width=900, height=600, menubar=0, scrollbars=yes');
        }
        function manualInput(txtCustomerID, hdnCustomerID, hdnDesc) {
            hdnCustomerID.value = txtCustomerID.value;
            hdnDesc.value = '-';
        }
</script>
<asp:Panel ID="pnlCustomer" runat="server" Width="75%">
    <table class="tablegrid" cellspacing="0" cellpadding="0" border="0" >
	    <tr>
		    <td class="tdganjil">
			    <asp:textbox id="txtCustomerID" cssclass="inptype" runat="server" ReadOnly="True" Width="50px"></asp:textbox>&nbsp;
			    <asp:textbox id="txtCustomerName" cssclass="inptype" runat="server" ReadOnly="True" Width="150px"></asp:textbox>&nbsp;
			    <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
			    <input type="hidden" id="hdnCustomerID" runat="server" name="hdnCustomerID" class="inptype"/>
			    <input type="hidden" id="hdnCustomerName" runat="server" name="hdnCustomerName" class="inptype"/>
			    <asp:requiredfieldvalidator id="RFVCustomer" runat="server" ErrorMessage="Fill..Customer" ControlToValidate="txtCustomerID"
				    Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
		    </td>		    
	    </tr>
    </table>
</asp:Panel>