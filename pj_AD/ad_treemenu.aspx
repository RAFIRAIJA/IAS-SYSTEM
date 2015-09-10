<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad_treemenu.aspx.cs" Inherits="pj_ad_ad_treemenu" %>
<%@ Register Assembly="AjaxControlToolkit" 	Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <%--<link rel="stylesheet" href="usermanagement/include/inc-0100-00.css">--%>
    <script src="../script/JavaScript/Elsa.js"></script>
    <script src="../script/JavaScript/jquery_accrd.js"></script>    
    <link href="../script/tree_menu.css" rel="stylesheet" type="text/css">
    <script type="text/javascript">
        $(document).ready(function () {
            //slides the element with class "menu_body" when paragraph with class "menu_head" is clicked 
            $("#accordion").click(function () {
                $(this).css({ backgroundImage: "url(down.png)" }).next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().css({ backgroundImage: "url(left.png)" });
            });
            //slides the element with class "menu_body" when mouse is over the paragraph
            $("#accordion").mouseover(function () {
                $(this).css({ backgroundImage: "url(down.png)" }).next("div.menu_body").slideDown(500).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().css({ backgroundImage: "url(left.png)" });                 
            });
            
        });
    </script>
    
    <style type="text/css"> 
        .contentAccordion{
            padding:2px;
            background-color:transparent ;
        }
        .headerAccordion
        {
            color:white ;
            padding:2px;
        }        
        .menu_list 
        {
	        width: 250px;
        }
        .menu_head {
	        padding: 6px 10px;
	        cursor: pointer;
            border-radius:8px;
	        position: relative;
	        margin:1px;
            font-size:12px;
            font-weight:bold;
            font-family:Verdana;
            background-color:#538fad;
            /*background: #006699 url(left.png) center right no-repeat;*/
        }
        .menu_head :hover {
            color:red ;
            text-transform :uppercase ;  
            /*background-color:#00ffff;*/
        }

        /*.menu_body {
	        display:none;
        }*/
        .menu_body a{
          display:inline;
          color:#006699;
          background-color:#EFEFEF;
          padding-left:2px;
          font-weight:normal ;
          font-size:11px; 
          font-family:Verdana;   
          text-decoration:none;
          transition:all ;
        }
        .menu_body a:hover {
          color:red ;
          text-transform :uppercase ;            
        }
       
    </style>
</head>
<body bgcolor="#f4f4f4" vLink="blue" aLink="black" link="blue" onkeydown="checkKP()">
    <form id="form1" runat="server" method="post" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:XmlDataSource ID="MySource" runat="server"/>
        <div id="accordion" class="menu_list">                        
                <asp:Accordion ID="acrDynamic" runat="server" SelectedIndex="0" HeaderCssClass="headerAccordion menu_head" ContentCssClass="contentAccordion" 
                               FadeTransitions ="true" TransitionDuration ="600" >
                </asp:Accordion>                
        </div>        

         <%--<table>
				    <tr>
					    <td><asp:panel id="Panel1" runat="server" >
							    <DIV id="outtext" runat="server">
							        <asp:XmlDataSource ID="MySource" runat="server"/>
                                    <asp:TreeView ID="TreeView2" 
                                        DataSourceId="MySource" 
                                        ExpandDepth="1"
                                        MaxDataBindDepth="10"
                                        runat="server" ontreenodedatabound="TreeView2_TreeNodeDataBound"                                      
                                        CssClass="page" ForeColor="Black"  
                                        NodeIndent="10" Width="105px" LineImagesFolder="~/TreeLineImages"
                                        ImageSet="Arrows" >
                                        <ParentNodeStyle HorizontalPadding="5px" Font-Underline="False" ForeColor="Blue"/>
                                        <HoverNodeStyle Font-Underline="True" Font-Size="10pt" ForeColor="Red" />
                                        <SelectedNodeStyle Font-Underline="True"  
                                            HorizontalPadding="0px" VerticalPadding="0px" ForeColor="Black" />
                                        <RootNodeStyle HorizontalPadding="5px" Font-Underline="False" ForeColor="BLUE" Font-Bold="True" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="#3366ff" 
                                            HorizontalPadding="10px" NodeSpacing="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
							    </DIV>
						    </asp:panel></td>
				    </tr>
         </table>
         <input  type="hidden" name="hidCaller" value="menu">--%>
         
   </form>
</body>
</html>
