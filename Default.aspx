<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" CodeBehind="~/default.aspx.cs"  %>
<!DOCTYPE html>
<html >
<head>
  <meta charset="UTF-8">
  <title>Customer Contract Management System</title>
  
  <link rel="stylesheet" href="css/reset.min.css">
  <link rel="stylesheet" href="css/style_login.css">
  <link rel="stylesheet" href="css/sweetalert.css" /> 
</head>

<body>
  <div class="container">
  <div class="login">
  	<h1 class="login-heading"><strong>Customer Contract Management System</strong></h1>
      <form method="post" enctype="multipart/form-data" name="frm_main" id="frm_main" runat="server">
          <div id="err_msg" runat="server" class="alert alert-warning" >&nbsp;</div>
          <input type="text" autocomplete="off" id="username" name="username" placeholder="Username" required="required" class="input-txt" runat="server" />
          <input type="password" autocomplete="off" id="password" name="password" placeholder="Password" required="required" class="input-txt" runat="server" />
          <div class="login-footer">
            <span style="color:#fff; font-size:small">&copy; PT ISS Powered by Techno Media</span>
            <asp:Button ID="btn_login" CommandName="btn_login" runat="server" class="btn btn--right" Text="Login" OnClick="btn_login_Click" />
          </div>
      </form>
  </div>
</div>
    <script src="js/jquery.min.js"></script>
    <script src="js/iss.js"></script>
    <script src="js/sweetalert-dev.js"></script> 
    <script type="text/javascript">        
        $(function () {
            
        })
    </script>    
</body>
</html>