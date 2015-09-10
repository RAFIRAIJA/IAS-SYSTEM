<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad_main.aspx.cs" Inherits="pj_ad_ad_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ISS - MR ONLINE</title>
    <META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=iso8859-1">
	<META http-equiv="PICS-Label" content='(PICS-1.1 "http://www.rsac.org/ratingsv01.html" l gen true comment "RSACi North America Server" by "inet@microsoft.com" for "http://www.microsoft.com/" on "1997.06.30T14:21-0500" r (n 0 s 0 v 0 l 0))'>
	<META NAME="KEYWORDS" CONTENT="ISS System and Administration">
	<META NAME="DESCRIPTION" CONTENT="ISS System and Administration">
	<META NAME="ROBOTS" CONTENT="NOINDEX">
	<META NAME="MS.LOCALE" CONTENT="EN-US">
	<META NAME="CATEGORY" CONTENT="Error 404 page">
    <script src="../include/JavaScript/Elsa.js"></script>
    <link href="../include/CSS" rel="stylesheet" type="text/css">
    <%--<STYLE TYPE="text/css"> 
        A:link {color:"#003399";}
	    A:visited {color:"#003399";}
	    A:hover {color:"red";}
	</STYLE>--%>
    <% 
	string strUrl;
	string strUrlMenu;
	string strWidth;
    strUrl = "ad_mainmenu.aspx";
    strUrlMenu = "ad_treemenu.aspx";
    strWidth = "325";
	%>
</head>
    <frameset rows="100,*,20" border="0" frameSpacing="0" frameBorder="0" onkeydown="checkKP()">
		<frame name="header" src="ad_headermenu.aspx" scrolling="auto"  id="header" frameBorder="0" >
		<frameset cols=200,*" border="0" frameSpacing="3" frameBorder="0" id="Mainframe">
			<frame name="contents" src="ad_treemenu.aspx" id="menu" scrolling="no" frameborder="0" MARGINHEIGHT="0" MARGINWIDTH="0" target="ContentFrame" >&nbsp;
			<frame name="ContentFrame" src="ad_mainmenu.aspx" id="content" scrolling="auto" frameborder="0" MARGINHEIGHT="0" MARGINWIDTH="0"  onkeydown="checkKP()">
		</frameset>
		<%--<frame name="footer" src="am_copyright.aspx" scrolling="no" noresize id="footer">
		<noframes>
			<body>
				<p>This page uses frames, but your browser doesn't support them.</p>
			</body>
		</noframes>--%>
	</frameset>
</html>
