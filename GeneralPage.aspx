<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneralPage.aspx.cs" Inherits="GeneralPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Main Menu Application Selection</title>
	
    <!-- css -->
    <link href="Script/valera/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="Script/valera/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<link href="Script/valera/css/nivo-lightbox.css" rel="stylesheet" />
	<link href="Script/valera/css/nivo-lightbox-theme/default/default.css" rel="stylesheet" type="text/css" />
	<link href="Script/valera/css/owl.carousel.css" rel="stylesheet" media="screen" />
    <link href="Script/valera/css/owl.theme.css" rel="stylesheet" media="screen" />
	<link href="Script/valera/css/flexslider.css" rel="stylesheet" />
	<link href="Script/valera/css/slippry.css" rel="stylesheet" >
	<link href="Script/valera/css/animate.css" rel="stylesheet" />
    <link href="Script/valera/css/style.css" rel="stylesheet">
	<link href="Script/valera/color/default.css" rel="stylesheet">
</head>

<body id="page-top" data-spy="scroll" data-target=".navbar-custom">

    <form runat="server" method="post">

    <!-- page loader -->
    <div id="page-loader">
      <div class="loader">
        <div class="spinner">
          <div class="spinner-container con1">
            <div class="circle1"></div>
            <div class="circle2"></div>
            <div class="circle3"></div>
            <div class="circle4"></div>
          </div>
          <div class="spinner-container con2">
            <div class="circle1"></div>
            <div class="circle2"></div>
            <div class="circle3"></div>
            <div class="circle4"></div>
          </div>
          <div class="spinner-container con3">
            <div class="circle1"></div>
            <div class="circle2"></div>
            <div class="circle3"></div>
            <div class="circle4"></div>
          </div>
        </div>
      </div>
    </div>
    <!-- /page loader -->

	<!-- Section: home slide -->
    <section id="intro" class="home-slide text-light">
		<ul id="valera-slippry">
		  <li>
			<div><img src="Script/valera/img/slides/1.jpg" alt="ISS - APPLICATION SYSTEM <br /><a href='#about' class='btn btn-skin btn-slide'>START NOW</a>"></div>
		  </li>
		  <li>
			<div><img src="Script/valera/img/slides/2.jpg" alt="ISS - APPLICATION SYSTEM <br /><a href='#about' class='btn btn-skin btn-slide'>START NOW</a>"></div>
		  </li>
		  <li>
			<div><img src="Script/valera/img/slides/3.jpg" alt="ISS - APPLICATION SYSTEM <br /><a href='#about' class='btn btn-skin btn-slide'>START NOW</a>"></div>
		  </li>
		</ul>
    </section>
	<!-- /Section: intro -->
	
	
    <!-- Navigation -->
    <div id="navigation">
        <nav class="navbar navbar-custom" role="navigation">
                              <div class="container">
                                    <div class="row">
                                          <div class="col-md-2">
                                                   <div class="site-logo">
                                                            <a href="GeneralPage.aspx"><img src="Images/company/header.gif" alt=""  /></a>
                                                    </div>
                                          </div>
                                          

                                          <div class="col-md-10">
                         
                                                      <!-- Brand and toggle get grouped for better mobile display -->
                                          <div class="navbar-header">
                                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#menu">
                                                <i class="fa fa-bars"></i>
                                                </button>
                                          </div>
                                                <!-- Collect the nav links, forms, and other content for toggling -->
                                                <div class="collapse navbar-collapse" id="menu">
                                                    <ul class="nav navbar-nav navbar-right">
                                                            <li><a href="GeneralPage.aspx">Home</a></li>
                                                            <li><a href="#about">About Us</a></li>
															<li><a href="#service">Services</a></li>
                                                            <li><a href="#works">Works</a></li>				                                                                                                                                    
                                                            <li><a href="#contact">Contact</a></li>
															<li class="dropdown nav-toggle">
                                                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">LIST APPLICATION<b class="caret"></b></a>
                                                                <div>
                                                                <ul class="dropdown-menu">
                                                                        <li><a class="external" href="http://my.iss.co.id/IAS/pj_AD/ad_menuutama.aspx"><img src="Script/valera/img/team/MROnline.ico" alt=""  /><span> MR Online</span></a></li>
                                                                        <li><a class="external" href="#">SparePart Online</a></li>
                                                                        <li><a class="external" href="#"><img src="Script/valera/img/team/SunFish.ico" alt=""  /><span> Sunfish</span></a></li>
                                                                        <li><a class="external" href="#">Contract Management System</a></li>
                                                                        <li><a class="external" href="GeneralLogin.aspx">Log Out</a></li>
                                                                </ul>
                                                                </div>
                                                            </li>
                                                    </ul>
                                                </div>
                                                <!-- /.Navbar-collapse -->
                             
                                          </div>
                                    </div>
                              </div>
                              <!-- /.container -->
                        </nav>
    </div> 
    <!-- /Navigation -->  

	<!-- Section: about -->
    <section id="about" class="home-section color-dark bg-white">
		<%--<div class="container marginbot-50">
			<div class="row">
				<div class="col-lg-8 col-lg-offset-2">
					<div class="wow flipInY" data-wow-offset="0" data-wow-delay="0.4s">
					<div class="section-heading text-center">
					<h2 class="h-bold">About</h2>
					<div class="divider-header"></div>
					<p>Lorem ipsum dolor sit amet, agam perfecto sensibus usu at duo ut iriure.</p>
					</div>
					</div>
				</div>
			</div>

		</div>--%>
		
		<div class="text-center">
		<div class="container">
            <div class="row">
                <div class="col-xs-6 col-sm-3 col-md-3">				
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.2s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>	
                <div class="col-xs-6 col-sm-3 col-md-3">				
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.5s">
                            <div class="team-wrapper-overlay">
                              <%--<a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/MROnlineLogo.png" alt="" /></a>--%>
                              <asp:LinkButton runat="server" ID="imgMROnline" OnClick="MRLogin_Click" ><img src="Script/valera/img/team/MROnlineLogo.png" alt="" /></asp:LinkButton>
                            </div>
                            <img src="Script/valera/img/team/MROnlineLogo.png" alt="" />
                    </div>
                </div>		
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.8s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="1s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/MROnlineLogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/MROnlineLogo.png" alt="" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-6 col-sm-3 col-md-3">				
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.2s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/e_procurement.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/e_procurement.png" alt="" />
                    </div>
                </div>			
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.5s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.8s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/e_procurement.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/e_procurement.png" alt="" />
                    </div>
                </div>
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="1s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-6 col-sm-3 col-md-3">				
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.2s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>			
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.5s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/e_procurement.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/e_procurement.png" alt="" />
                    </div>
                </div>
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="0.8s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/SunFishlogo.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/SunFishlogo.png" alt="" />
                    </div>
                </div>
			    <div class="col-xs-6 col-sm-3 col-md-3">
				    <div class="team-wrapper-big wow bounceInUp" data-wow-delay="1s">
                            <div class="team-wrapper-overlay">
                              <a href='http://my.iss.co.id/ias/pj_AD/ad_menuutama.aspx'><img src="Script/valera/img/team/e_procurement.png" alt="" /></a>
                            </div>
                            <img src="Script/valera/img/team/e_procurement.png" alt="" />
                    </div>
                </div>
            </div>
		</div>
		</div>
        
	</section>
	<!-- /Section: about -->
	
	<!-- Section: parallax 1 -->	
	<%--<section id="parallax1" class="home-section parallax text-light" data-stellar-background-ratio="0.5">	
           <div class="container">
				<div class="row">
					<div class="col-md-12">
						<div class="text-center">
						<h2 class="big-heading highlight-dark wow bounceInDown" data-wow-delay="0.2s">We start from pixel perfect pattern</h2>
						</div>
					</div>				
				</div>
            </div>
	</section>	--%>
	
	<!-- Section: services -->
    <section id="service" class="home-section color-dark bg-white">
		<div class="container marginbot-50">
			<div class="row">
				<div class="col-lg-8 col-lg-offset-2">
					<div class="wow flipInY" data-wow-offset="0" data-wow-delay="0.4s">
					<div class="section-heading text-center">
					<h2 class="h-bold">Services</h2>
					<div class="divider-header"></div>
					<p>Lorem ipsum dolor sit amet, agam perfecto sensibus usu at duo ut iriure.</p>
					</div>
					</div>
				</div>
			</div>

		</div>

		<div class="text-center">
		<div class="container">

        <div class="row">
            <div class="col-xs-6 col-sm-3 col-md-3">
				<div class="wow fadeInLeft" data-wow-delay="0.2s">
                <div class="service-box">
					<div class="service-icon">
						<span class="pe-7s-monitor pe-5x"></span> 
					</div>
					<div class="service-desc">						
						<h5>Web Design</h5>
						<p>
						Ad denique euripidis signiferumque vim, iusto admodum quo cu. No tritani neglegentur mediocritatem duo.
						</p>
						<a href="#" class="btn btn-skin">Learn more</a>
					</div>
                </div>
				</div>
            </div>
			<div class="col-xs-6 col-sm-3 col-md-3">
				<div class="wow fadeInUp" data-wow-delay="0.2s">
                <div class="service-box">
					<div class="service-icon">
						<span class="pe-7s-camera pe-5x"></span> 
					</div>
					<div class="service-desc">
						<h5>Photography</h5>
						<p>
						Ad denique euripidis signiferumque vim, iusto admodum quo cu. No tritani neglegentur mediocritatem duo.
						</p>
						<a href="#" class="btn btn-skin">Learn more</a>
					</div>
                </div>
				</div>
            </div>
			<div class="col-xs-6 col-sm-3 col-md-3">
				<div class="wow fadeInUp" data-wow-delay="0.2s">
                <div class="service-box">
					<div class="service-icon">
						<span class="pe-7s-note pe-5x"></span> 
					</div>
					<div class="service-desc">
						<h5>Graphic design</h5>
						<p>
						Ad denique euripidis signiferumque vim, iusto admodum quo cu. No tritani neglegentur mediocritatem duo.
						</p>
						<a href="#" class="btn btn-skin">Learn more</a>
					</div>
                </div>
				</div>
            </div>
			<div class="col-xs-6 col-sm-3 col-md-3">
				<div class="wow fadeInRight" data-wow-delay="0.2s">
                <div class="service-box">
					<div class="service-icon">
						<span class="pe-7s-phone pe-5x"></span> 
					</div>
					<div class="service-desc">
						<h5>Mobile apps</h5>
						<p>
						Ad denique euripidis signiferumque vim, iusto admodum quo cu. No tritani neglegentur mediocritatem duo.
						</p>
						<a href="#" class="btn btn-skin">Learn more</a>
					</div>
                </div>
				</div>
            </div>
        </div>		
		</div>
		</div>
	</section>
	<!-- /Section: services -->
	
	<!-- Section: parallax 2 -->	
	<%--<section id="parallax2" class="home-section parallax text-light" data-stellar-background-ratio="0.5">	
           <div class="container">
				<div class="row appear stats">
					<div class="col-md-3">
						<div class="align-center color-white txt-shadow">
							<div class="icon">
								<i class="pe-7s-stopwatch pe-5x"></i>
							</div>
						<strong id="counter-coffee" class="number">1142</strong><br />
						<span class="text">Minutes</span>
						</div>
					</div>
					<div class="col-md-3">
						<div class="align-center color-white txt-shadow">
							<div class="icon">
								<i class="pe-7s-music pe-5x"></i>
							</div>
						<strong id="counter-music" class="number">229</strong><br />
						<span class="text">Tracks</span>
						</div>
					</div>
					<div class="col-md-3">
						<div class="align-center color-white txt-shadow">
							<div class="icon">
								<i class="pe-7s-coffee pe-5x"></i>
							</div>
						<strong id="counter-clock" class="number">451</strong><br />
						<span class="text">Cokes</span>
						</div>
					</div>
					<div class="col-md-3">
						<div class="align-center color-white txt-shadow">
							<div class="icon">
								<i class="pe-7s-cup pe-5x"></i>
							</div>
						<strong id="counter-heart" class="number">112</strong><br />
						<span class="text">Awwards</span>
						</div>
					</div>
				</div>
            </div>
	</section>--%>	
	

	<!-- Section: works -->
    <section id="works" class="home-section color-dark text-center bg-white">
		<div class="container marginbot-50">
			<div class="row">
				<div class="col-lg-8 col-lg-offset-2">
					<div class="wow flipInY" data-wow-offset="0" data-wow-delay="0.4s">
					<div class="section-heading text-center">
					<h2 class="h-bold">Portfolio</h2>
					<div class="divider-header"></div>
					<p>Lorem ipsum dolor sit amet, agam perfecto sensibus usu at duo ut iriure.</p>
					</div>
					</div>
				</div>
			</div>

		</div>

		<div class="container">
			<div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" >
					<div class="wow bounceInUp" data-wow-delay="0.4s">
                    <div id="owl-works" class="owl-carousel">
                        <div class="item"><a href="Script/valera/img/works/1.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/1@2x.jpg"><img src="Script/valera/img/works/1.jpg" class="img-responsive" alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/2.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/2@2x.jpg"><img src="Script/valera/img/works/2.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/3.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/3@2x.jpg"><img src="Script/valera/img/works/3.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/4.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/4@2x.jpg"><img src="Script/valera/img/works/4.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/5.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/5@2x.jpg"><img src="Script/valera/img/works/5.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/6.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/6@2x.jpg"><img src="Script/valera/img/works/6.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/7.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/7@2x.jpg"><img src="Script/valera/img/works/7.jpg" class="img-responsive " alt="img"></a></div>
                        <div class="item"><a href="Script/valera/img/works/8.jpg" title="This is an image title" data-lightbox-gallery="gallery1" data-lightbox-hidpi="Script/valera/img/works/8@2x.jpg"><img src="Script/valera/img/works/8.jpg" class="img-responsive " alt="img"></a></div>
                    </div>
					</div>
                </div>
            </div>
		</div>

	</section>
	<!-- /Section: works -->

	<!-- Section: parallax 3 -->	
	<%--<section id="parallax3" class="home-section parallax text-light text-center" data-stellar-background-ratio="0.5">	
           <div class="container">
				<div class="row">
					<div class="col-md-12">
						<div class="testimonialslide clearfix flexslider">
							<ul class="slides">
								<li><blockquote>
								Usu ei porro deleniti similique, per no consetetur necessitatibus. Ut sed augue docendi alienum, ex oblique scaevola inciderint pri, unum movet cu cum. Et cum impedit epicuri
									</blockquote>
									<h4>Daniel Dan <span>&#8213; MA System</span></h4> 
								</li>
								<li><blockquote>
								Usu ei porro deleniti similique, per no consetetur necessitatibus. Ut sed augue docendi alienum, ex oblique scaevola inciderint pri, unum movet cu cum. Et cum impedit epicuri 
									</blockquote>
									<h4>Mark Wellbeck <span>&#8213; AC Software </span></h4>
								</li>	
							</ul>
						</div>					
					</div>	
				</div>
            </div>
	</section>--%>	
	

	<!-- Section: contact -->
    <section id="contact" class="home-section nopadd-bot color-dark bg-white text-center">
		<div class="container marginbot-50">
			<div class="row">
				<div class="col-lg-8 col-lg-offset-2">
					<div class="wow flipInY" data-wow-offset="0" data-wow-delay="0.4s">
					<div class="section-heading text-center">
					<h2 class="h-bold">Contact us</h2>
					<div class="divider-header"></div>
					<p>Lorem ipsum dolor sit amet, agam perfecto sensibus usu at duo ut iriure.</p>
					</div>
					</div>
				</div>
			</div>

		</div>
		
		<div class="container">

			<div class="row marginbot-80">
				<div class="col-md-8 col-md-offset-2">
		            <div id="sendmessage">Your message has been sent. Thank you!</div>
                    <div id="errormessage"></div>
                    <form action="" method="post" role="form" class="contactForm">
                        <div class="form-group">
                            <input type="text" name="name" class="form-control" id="name" placeholder="Your Name" data-rule="minlen:4" data-msg="Please enter at least 4 chars" />
                            <div class="validation"></div>
                        </div>
                        <div class="form-group">
                            <input type="email" class="form-control" name="email" id="email" placeholder="Your Email" data-rule="email" data-msg="Please enter a valid email" />
                            <div class="validation"></div>
                        </div>
                        <div class="form-group">
                            <input type="text" class="form-control" name="subject" id="subject" placeholder="Subject" data-rule="minlen:4" data-msg="Please enter at least 8 chars of subject" />
                            <div class="validation"></div>
                        </div>
                        <div class="form-group">
                            <textarea class="form-control" name="message" rows="5" data-rule="required" data-msg="Please write something for us" placeholder="Message"></textarea>
                            <div class="validation"></div>
                        </div>
                        
                        <div class="text-center"><button type="submit" class="btn btn-skin btn-lg btn-block">Send Message</button></div>
                    </form>
				</div>
			</div>	


		</div>
	</section>
	<!-- /Section: contact -->

	<!-- google map -->
	<div id="map-btn1-div">
		<a id="map-btn1" class="gmap-btn close-map-button btn-show" href="#map">
		Click here to open the map
		</a>
	</div>
	<a id="map-btn2" class="btn btn-skin btn-lg btn-noradius gmap-btn close-map-button btn-hide" href="#map" title="Close google map" data-toggle="tooltip" data-placement="top">
	795 Folsom Ave, Suite 600 San Francisco, CA 94107
	</a>
	
	<!-- google map -->
	<section id="map" class="close-map">
		<div id="google-map"></div>
	</section>
	<!-- /google map -->		
	

	<footer>
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3">
					
					<div class="text-center">
						<a href="#intro" class="totop"><i class="pe-7s-angle-up pe-3x"></i></a>
						
						<div class="social-widget">
							
							
							<ul class="team-social">
									<li class="social-facebook"><a href="#"><i class="fa fa-facebook"></i></a></li>
									<li class="social-twitter"><a href="#"><i class="fa fa-twitter"></i></a></li>
									<li class="social-google"><a href="#"><i class="fa fa-google-plus"></i></a></li>
									<li class="social-dribbble"><a href="#"><i class="fa fa-dribbble"></i></a></li>
							</ul>						
						
						</div>
						<p>&copy;Valera Theme. All Rights Reserved</p>
                        <div class="credits">
                            <!-- 
                                All the links in the footer should remain intact. 
                                You can delete the links only if you purchased the pro version.
                                Licensing information: https://bootstrapmade.com/license/
                                Purchase the pro version with working PHP/AJAX contact form: https://bootstrapmade.com/buy/?theme=Valera
                            -->
                            <a href="https://bootstrapmade.com/">Bootstrap Themes</a> by <a href="https://bootstrapmade.com/">BootstrapMade</a>
                        </div>
					</div>
				</div>
			</div>	
		</div>
	</footer>

    <!-- Core JavaScript Files -->
    <script src="Script/valera/js/jquery.min.js"></script>	 
    <script src="Script/valera/js/bootstrap.min.js"></script>
	<script src="http://maps.google.com/maps/api/js?sensor=false"></script>
	<script src="Script/valera/js/jquery.sticky.js"></script>
	<script src="Script/valera/js/slippry.min.js"></script> 
	<script src="Script/valera/js/jquery.flexslider-min.js"></script>
	<script src="Script/valera/js/morphext.min.js"></script>
	<script src="Script/valera/js/gmap.js"></script>
	<script src="Script/valera/js/jquery.mb.YTPlayer.js"></script>
    <script src="Script/valera/js/jquery.easing.min.js"></script>	
	<script src="Script/valera/js/jquery.scrollTo.js"></script>
	<script src="Script/valera/js/jquery.appear.js"></script>
	<script src="Script/valera/js/stellar.js"></script>
	<script src="Script/valera/js/wow.min.js"></script>
	<script src="Script/valera/js/owl.carousel.min.js"></script>
	<script src="Script/valera/js/nivo-lightbox.min.js"></script>
	<script src="Script/valera/js/jquery.nicescroll.min.js"></script>
    <script src="Script/valera/js/custom.js"></script>
    <script src="Script/valera/contactform/contactform.js"></script>
    


    </form>

</body>

</html>
