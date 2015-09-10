<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad_treemenusetting.aspx.cs" Inherits="pj_ad_ad_treemenusetting" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
	<link href="../include/css" type="text/css" rel="stylesheet">
	<style type="text/css">
	A:link {color:"#003399";}
	A:visited {color:"#003399";}
	A:hover {color:"red";font-weight: bold;}

	</style>
	<script src="~/include/JavaScript/Elsa.js"></script>
		<script type="text/javascript">
function fmin1(){
	top.Mainframe.cols="0,*";
	top.ContentFrame.display();
}
function fshow(){
	document.all.maximize.src="../images/button/close_bttn.gif";
}
function fhide(){
    document.all.maximize.src = "../images/button/maximize.gif";
}

function fmin(flag){
if(!flag){
    var width = 0.90;
    var counter = 20;
    var myInterval = window.setInterval(function(){
         counter--;
        top.Mainframe.cols= (width * counter)  + "%,*";
        if(width * counter < 3)  {
            clearInterval(myInterval);
	        top.Mainframe.cols="16px,*";
	        Minimum.style.display="none";
	        Maximum.style.display="inline";			
	        MenuTree.style.display="none";
	    }
    },10);
 }else{
	top.Mainframe.cols="16px,*";
	Minimum.style.display="none";
	Maximum.style.display="inline";			
	MenuTree.style.display="none";
 }
	//top.Mainframe.cols="16px,*";
}
function fmax(){
    var width = 0.90;
    var counter = 1;
    var myInterval = window.setInterval(function(){
         counter++;
        top.Mainframe.cols= (width * counter)  + "%,*";
        if(width*counter >= 20)  clearInterval(myInterval);
    },10);
	//top.Mainframe.cols="18%,*";
	Maximum.style.display="none";
	Minimum.style.display="inline";			
	MenuTree.style.display="inline";
}
		</script>
	</head>
	<body bgcolor="#FFFFFF" onload="fmin(true)">
		<div id="Minimum" style="display:none">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#F5F5F5">
				<tr>
					<td align="right" valign="middle" width="170">&nbsp;</td>
					<td align="right" width="20"><a href="javascript:fmin(false);">
					    <img src="../Images/button/butkiri.gif"  width="20" height="20" border="0" alt="&lt;&lt; hide menu" />
					</td>
				</tr>
			</table>
		</div>
		<div id="Maximum" style="display:inline">
			<table width="1%" border="0" cellspacing="0" cellpadding="0" bgcolor="#F5F5F5" height="100%">
				<tr>
					<td height="1" align="center"><a href="javascript:fmax();">
					    <img src="../Images/button/butkanan.gif"  width="20" height="20" border="0" alt="show menu &gt;&gt;" />
					</td>
				</tr>
				<tr bgcolor="#F5F5F5">
					<td bgcolor="#F5F5F5">&nbsp;</td>
				</tr>
			</table>
		</div>
		<div id="MenuTree" style="display:none">
			<iframe name="NavigasiFrame" border="0"  frameborder="0" width="100%" height="100%" framespacing="1" topmargin="0" leftmargin="0" marginheight="0" marginwidth="0" src="ad_treemenu.aspx">
		</div>
	</body>
</html>
