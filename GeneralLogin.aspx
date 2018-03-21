<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneralLogin.aspx.cs" Inherits="GeneralLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="description" content="Coming soon, Bootstrap, Bootstrap 3.0, Free Coming Soon, free coming soon, free template, coming soon template, Html template, html template, html5, Code lab, codelab, codelab coming soon template, bootstrap coming soon template">
    <title>Login Application</title>
    <!-- ============ Google fonts ============ -->
    <link href='http://fonts.googleapis.com/css?family=EB+Garamond' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700,300,800'
        rel='stylesheet' type='text/css' />
    <!-- ============ Add custom CSS here ============ -->
    <link href="Script/registration/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Script/registration/css/style.css" rel="stylesheet" />
    <link href="Script/registration/css/font-awesome.css" rel="stylesheet" />

</head>
<body>
    
    <div id="custom-bootstrap-menu" class="navbar navbar-default " role="navigation">    
    <div class="container">        
        <div class="navbar-header">            
            <a class="navbar-brand" href="#" ></a>
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-menubuilder"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
            </button>
        </div>
        <div class="collapse navbar-collapse navbar-menubuilder">
            <ul class="nav navbar-nav navbar-right">
                <%--<li><a href="/">Home</a>
                </li>
                <li><a href="/products">Products</a>
                </li>
                <li><a href="/about-us">About Us</a>
                </li>
                <li><a href="/contact">Contact Us</a>
                </li>--%>                
                <li></li>
            </ul>
        </div>        

    </div>
</div>

        <div class="container">
           <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 text-center">
           <div id="banner">
             <%--<h1>Bootstrap <strong>Registration Form</strong> for .net developers</h1>--%>            
           
           </div>
                        
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">            
            <div class="registrationform">            
            <form class="form-horizontal" method="post" runat="server">
                <fieldset>
                    <legend>Login Application Form <i class="fa fa-pencil pull-right"></i></legend>
                    <div class="form-group">
                        <label for="inputEmail" class="col-lg-2 control-label">
                            User ID</label>
                        <div class="col-lg-10">
                            <input type="text" runat="server" class="form-control" id="inUserID" placeholder="NIK"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword" class="col-lg-2 control-label">
                            Password</label>
                        <div class="col-lg-10">
                            <input type="password" runat="server" class="form-control" id="inPassword" placeholder="Password"/>                            
                        </div>
                    </div>                    
                    <div class="form-group">
                        <div class="col-lg-10 col-lg-offset-2">
                            <button type="reset" class="btn btn-danger" id="BtnCancelLogin" runat="server" onserverclick="BtnCancelLogin_Click">
                                Cancel</button>
                            <button  class="btn btn-success" id= "BtnLogin" runat="server" onserverclick="BtnLogin_Click">
                                Login</button>
                        </div>
                    </div>
                </fieldset>
            </form>
         </div>



         </div>
        </div>

        <script src="Script/registration/js/jquery.js" type="text/javascript"></script>
        <script src="Script/registration/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="Script/registration/js/jquery.backstretch.js" type="text/javascript"></script>
        <script type="text/javascript">
            'use strict';

            /* ========================== */
            /* ::::::: Backstrech ::::::: */
            /* ========================== */
            // You may also attach Backstretch to a block-level element
            $.backstretch(
        [
            "Script/registration/img/44.jpg",
            "Script/registration/img/colorful.jpg",
            "Script/registration/img/34.jpg",
            "Script/registration/img/images.jpg"
        ],

        {
            duration: 4500,
            fade: 1500
        }
    );
        </script>


</body>
</html>
