<%@ Control Language="vb" AutoEventWireup="false" CodeFile="ValidDate.ascx.vb" Inherits="ValidDate" %>
<%
dim AppInfo,App
AppInfo = Request.servervariables("PATH_INFO")
App = AppInfo.substring(1, AppInfo.indexOf("/", 1) - 1)
%>
<script  type="text/javascript">
<!--

/*
var a = new Date()
a.setDate(31)
a.setMonth(1)
a.setFullYear(2002)
alert(a.getDate())
alert(a.getMonth())
alert(a.getFullYear())
*/

function isValidDate(source,arguments) {
    try {	
	    
		var ArrDate = arguments.Value.split("/");
		
		if (ArrDate.length != 3)
			arguments.IsValid = false;
		else{
			if (ArrDate[2].length != 4) {
				arguments.IsValid = false;
			}
			else {
			    if (isNaN(ArrDate[2]) || isNaN(ArrDate[1]) || isNaN(ArrDate[0])) {
					arguments.IsValid = false;
				}			
				else {
					var objDate1 = new Date();
					objDate1.setFullYear(ArrDate[2]);
					objDate1.setMonth(0);
					objDate1.setDate(1);

					objDate1.setDate(parseInt(ArrDate[0],10));
					objDate1.setMonth(parseInt(ArrDate[1], 10) - 1);
		
					if (objDate1.getDate() == parseInt(ArrDate[0], 10) && objDate1.getMonth() == parseInt(ArrDate[1],10)-1 && objDate1.getFullYear() == parseInt(ArrDate[2], 10))
					{
						arguments.IsValid = true;
					}else{
						arguments.IsValid = false;
					}
				}
			}
		}
	}
	catch (e){		
		arguments.IsValid = false;
	}
	
	return arguments.IsValid;
}

function OpenWin(strParam,X,Y){
    var objWindow = window.open('http://<%=Request.servervariables("SERVER_NAME")%>:<%=Request.servervariables("SERVER_PORT")%>/<%=App%>/usercontroller/form/Calendar.aspx?parent=' + strParam + '&sesBusinessDate=<%=session("mgDateTime")%>&postback=<%=lcase(txtDate.AutoPostBack)%>', 'Calender', 'height=200,width=190,left=' + X + ',top=' + Y + ',menubar=no,status=no,toolbar=no,scrollbars=no,resizable=no,titlebar=no');
	//alert('http://<%=Request.servervariables("SERVER_NAME")%>/<%=App%>/usercontroller/Calendar.asp')
	objWindow.focus();	
	return false;	
}

function dosubmit() {
	document.forms[0].submit();
}
//-->
</script>
<asp:textbox id="txtDate"  visible="true" CssClass="inptypemandatory" Columns="10" runat="server" MaxLength="10" Width="70px"></asp:textbox>
<span id="divButton" runat="server">
<%
dim AppInfo,App
AppInfo = Request.servervariables("PATH_INFO")
App = AppInfo.substring(1, AppInfo.indexOf("/", 1) - 1)
%>
<%--	<input type="image" id="<%= getClientID() %>_imgCalender" src="http://<%=(Request.servervariables("SERVER_NAME") & "/" & App )%>/Images/button/iconcalendar.gif" onclick="javascript:return OpenWin('<%= getClientID() %>',window.event.clientX,window.event.clientY);"/>--%>
	<input type="image" id="<%= getClientID() %>_imgCalender" 
        src="http://<%=(Request.servervariables("SERVER_NAME") & "/" & App )%>/images/button/iconcalendar.gif" 
        onclick="javascript:return OpenWin('<%= getClientID() %>',window.event.clientX,window.event.clientY);"/>
</span>
<input type="hidden" id="hidFlag"/> <input type="hidden" id="isCalendarPostBack" name="isCalendarPostBack"/> 
<asp:customvalidator id="cvlDate" visible="true" runat="server" ClientValidationFunction="isValidDate" OnServerValidate="isValidDate" ControlToValidate="txtDate" Display="dynamic" Enabled="true"></asp:customvalidator>

<asp:requiredfieldvalidator id="rfvRequired" visible="true" runat="server" ControlToValidate="txtDate" Display="dynamic" Enabled="true"></asp:requiredfieldvalidator>