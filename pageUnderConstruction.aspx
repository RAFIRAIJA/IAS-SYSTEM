<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pageUnderConstruction.aspx.cs" Inherits="pageUnderConstruction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	    <title>Page Under Construction</title>
	    <link type="text/css" href="Script/UnderConstruction/styles/reset.css" rel="stylesheet" media="all" />
	    <link type="text/css" href="Script/UnderConstruction/styles/text.css" rel="stylesheet" media="all" />
	    <link type="text/css" href="Script/UnderConstruction/styles/960.css" rel="stylesheet" media="all" />
	    <link type="text/css" href="Script/UnderConstruction/styles/style.css" rel="stylesheet" media="all" />
		<link type="text/css" href="Script/UnderConstruction/styles/jquery.countdown.css" rel="stylesheet" media="all" />

	    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
		<script type="text/javascript" src="Script/UnderConstruction/script/jquery.countdown.js"></script>
		<script type="text/javascript">
		    $(function () {
		        var austDay = new Date();
		        //austDay = new Date(austDay.getFullYear() + 1, 1 - 1, 26);
		        austDay = new Date(austDay.getFullYear(), 11 - 1, 23);
		        $('#defaultCountdown').countdown({ until: austDay });
		        $('#year').text(austDay.getFullYear());
		    });
</script>
	</head>
	 
	<body>
	<!--Header-->
    <div class="header_cotainer">
	       <div class="container_12">
		   <div class="grid_5">
	          <p> WE GO LIVE IN</p>
	        </div>
			<!-- Timer -->
			<div class="grid_7"><div id="defaultCountdown"></div></div>
			
			</div>
	</div>
<!--Content-->
	    <div class="content_container container_12">
  <div class="container_12 content_bg">
  <div class="container_12 content_bg2">
  <!-- Top line-->
  <div class="grid_2 logo"><a href="#"><img src="Images/company/logo.gif" alt="Logo" align="center" /></a>
  <p class="slogan">PT. ISS INDONESIA<p></div>
  <div class="grid_10"><img src="Script/UnderConstruction/images/dottedline.png" alt="Logo" align="left" /></div>

  <div class="clear"></div>
  
  <!-- Text -->
  <div class="grid_7">
  <p>WE ARE CURRENTLY WORKING ON AN AWESOME NEW SITE.</p>
  <p style="color:#842606;">STAY TUNED!</p>
  <p>PLEASE DON'T FORGET TO CHECK OUT OUR WEBSITE AND TO SUBSCRIBE TO OUR UPDATES</p></div>
  <div class="clear"></div>
  <!-- Subscription -->
  <div class="grid_7">
  <%--<div id="contact-area">
    <form method="post" action="">
			<input type="text" name="Email" id="Email" value="Enter your email to subscribe"/>
			<input type="submit" name="submit" value="SUBMIT" class="submit-button" />
	</form>
   </div>--%>
  </div>
  <div class="clear"></div>
  <!-- Social Icons -->
  <%--<div class="grid_7">
  <a href="http://www.twitter.com/nataly_birch"><img src="images/twitter.png" alt="Twitter" /></a>
  <a href="http://feeds.feedburner.com/land-of-web/YPDH"><img src="images/rss.png" alt="RSS Feed" /></a>
  <a href="http://land-of-web.com"><img src="images/dribble.png" alt="Dribble" /></a>
  <a href="http://www.facebook.com//pages/Landofweb/203453913014366?sk=wall"><img src="images/facebook.png" alt="Facebook" /></a>
  <a href="http://www.stumbleupon.com/stumbler/NatalyBirch"><img src="images/stumbleupon.png" alt="Stumbleupon" /></a>
  </div>--%>
  <div class="clear"></div>
  </div>
  </div>
</div>

		<!--Footer section-->
	    <div class="footer_container">
		    <div class="container_12">
		          <p> Copyright &#169; 2016  <a href="#">Information Management System Application</a> </p>
             </div>
		</div>
<!-- Scripts -->				  
        <script type="text/javascript">
            var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
            document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
        </script>
        <script type="text/javascript">
            var pageTracker = _gat._getTracker("UA-4715900-1");
            pageTracker._initData();
            pageTracker._trackPageview();
        </script>
	</body>
</html>
