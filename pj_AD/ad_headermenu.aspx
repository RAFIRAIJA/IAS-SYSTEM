<%@ Page Language="C#" EnableEventValidation="false" EnableViewState="False" AutoEventWireup="true" CodeFile="ad_headermenu.aspx.cs" Inherits="pj_ad_ad_headermenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link rel="stylesheet" type="text/css" href="../script/style.css" />--%>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

    <script src="../include/JavaScript/Elsa.js"></script>
    <script type="text/javascript">

        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }

        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
            }
        }

        function MM_findObj(n, d) { //v4.0
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && document.getElementById) x = document.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }

        function fGo() {
            //parent.frames.location.href='am_logout.aspx';
        }
        //-->

    </script>
    <script language="javascript">
        var ResizeTimer = null;
			    function ResizeMenu(abSwitch)
			    {
			       if (abSwitch==true)
			       {
			          if (ResizeTimer!=null)
			          {
			             window.clearTimeout(ResizeTimer);
			             ResizeTimer = null;
			          }
			          PnlMenuResizeBlock.style.display='';
			          TDMenuResize.width="250px"
			          window.status = "PT. ISS Indonesia © 2012, all rigths reserved";
			       }
			       else
			       {
			          PnlMenuResizeBlock.style.display='none';
			          TDMenuResize.width="20px"
			          window.status = "Position your mouse to your center and left most screen to display Menu";
			       }
			       return false;
			    }
			    function MouseOutOfMenuArea()
			    {
				    ResizeTimer=window.setTimeout("ResizeMenu(false);",60000);					
			    }
    </script>    
    
</head>
<%--<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" >
                <tr>
                    <td  height="50" align="center" colspan="4" >
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="30" ForeColor="Desktop">ISS APPLICATION SYSTEM - MR ONLINE</asp:Label> 
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>--%>
<body bgcolor="#ffffff" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    onunload="
    if( typeof XMLHttpRequest == 'undefined' )
    XMLHttpRequest = function()
    {
        try { return new ActiveXObject('Msxml2.XMLHTTP.6.0'); } catch(e) {};
        try { return new ActiveXObject('Msxml2.XMLHTTP.3.0'); } catch(e) {};
        try { return new ActiveXObject('Msxml2.XMLHTTP'); }     catch(e) {};
        try { return new ActiveXObject('Microsoft.XMLHTTP'); }  catch(e) {};
   
        throw new Error('This browser does not support XMLHttpRequest or XMLHTTP.');
    };
    var request = new XMLHttpRequest();

    <%--var url = 'onunload.aspx';--%>
    var url = ''
    request.open('GET',url,true);
    request.send(null);
    ">
      <%--
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td rowspan="2" width="15%" background="../images/background/header_left.png">
                    <img src="../images/background/header_left.png" runat="server" />
                </td>
                <td colspan="2" background="../images/background/header_center.png" height="10" align="center">
                    <font size="1" face="Verdana, Arial, Helvetica, sans-serif">
                        <asp:Label runat="server" EnableViewState="False" ID="lblDate"></asp:Label>
                        <asp:Label runat="server" EnableViewState="False" ID="Timer" ></asp:Label>
                        <br>
                        <strong><%= Session["mgNAME"]%></strong> - <strong>[<%= Session["mgUSERID"]%>]</strong>
                    </font>
                </td>                

                <td rowspan="2" width="40%" background="../images/background/header_right.png">                    
                </td>
            </tr>
            <tr>
                <td colspan="2" background="../images/background/header_center.png" height="10" align="center" valign="bottom">
                    <a href="javascript:parent.frames.location.href='ad_menuutama.aspx'"  target="main">
                        <img id="imgHome" runat="server" border="0" src="../Images/button/buttonhome.gif" />
                    </a>
                    <a href="javascript:parent.frames.location.href='ad_logout.aspx'" target="_top">
                        <img id="imgLogout" runat="server" border="0" src="../Images/button/buttonlogout.gif" />
                    </a>
                </td>                
            </tr>
        </table>                     
           --%>   
 
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td background="../images/background/headermenu.png" height="50" >
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" >
                        <tr>
                            <td rowspan="2" runat="server" align="center" width="20%">                            
                            </td>
                            <td align="center" height=30>
                                <font size="1" face="Verdana, Arial, Helvetica, sans-serif">
                                    <asp:Label runat="server" EnableViewState="False" ID="lblDate"></asp:Label>                                    
                                    <br>
                                    <strong><%= Session["mgNAME"]%></strong> - <strong>[<%= Session["mgUSERID"]%>]</strong>
                                </font>
                            </td>
                            <td rowspan="2" runat="server" align="center" width="20%">                            
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height=20>
                                <a href="javascript:parent.frames.location.href='ad_menuutama.aspx'"  target="main">
                                    <img id="imgHome" runat="server" border="0" src="../Images/button/buttonhome.gif" />
                                </a>
                                <a href="javascript:parent.frames.location.href='ad_logout.aspx'" target="_top">
                                    <img id="imgLogout" runat="server" border="0" src="../Images/button/buttonlogout.gif" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     
</body>
</html>
