<%@ Page Language="C#" AutoEventWireup="False" CodeFile="ad_mainmenu.aspx.cs" Inherits="pj_ad_ad_mainmenu" EnableEventValidation="false" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MR Online - ISS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
	<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
	<meta name="vs_defaultClientScript" content="JavaScript"/>
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
    <script src="../../common/JavaScript/Elsa.js"></script>
    <link href="../../common/CSS" rel="stylesheet" type="text/css">
</head>
<body onkeydown="checkKP()" leftMargin="0" topMargin="0">
    <form id="form1" method="post" runat="server">
        <div>
            <asp:scriptmanager id="ScriptManager" runat="server" />
            <asp:updatepanel runat="server" id="UpdatePanel1">
                <contenttemplate>

                </contenttemplate>
                <triggers>
                </triggers>
            </asp:updatepanel>
        </div>
    </form>
</body>
</html>
