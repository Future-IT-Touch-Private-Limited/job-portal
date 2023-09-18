<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="adm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="vendor/fontawesome/css/all.min.css" rel="stylesheet" />
    <link href="vendor/icofont/icofont.min.css" rel="stylesheet" />
    <link href="vendor/select2/css/select2.min.css" rel="stylesheet" />
    <link href="css/osahan.css" rel="stylesheet" />
    <meta name="robots" content="noindex,nofollow" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row no-gutter">
                <div class="col-md-12 col-lg-12">
                    <div class="login d-flex align-items-center py-5">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-9 col-lg-8 mx-auto pl-5 pr-5">
                                    <p class="text-center">
                                        <img src="emoji.png" class="img-fluid" style="height: 120px" />
                                    </p>
                                    <h3 class="login-heading mb-4  text-center">Admin Login!</h3>
                                    <div class="form-group">
                                        <asp:TextBox ID="usernametxt" placeholder="Username" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="passwordtxt" placeholder="Password *" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <p class="text-center">
                                        <asp:Label Visible="False" ID="invalidloginerror" runat="server" ForeColor="Red" Font-Bold="True" Text="Invalid Username or Password"></asp:Label>
                                    </p>
                                    <asp:Button ID="loginbtn" runat="server" OnClick="loginbtn_Click" class="btn btn-lg btn-outline-primary btn-block btn-login text-uppercase font-weight-bold mb-2" Text="Sign In" />
                                    <hr class="my-4">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- jQuery -->
        <script src="vendor/jquery/jquery-3.3.1.slim.min.js"></script>
        <!-- Bootstrap core JavaScript-->
        <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
        <!-- Select2 JavaScript-->
        <script src="vendor/select2/js/select2.min.js"></script>
        <!-- Custom scripts for all pages-->
        <script src="js/custom.js"></script>
    </form>
</body>
</html>
