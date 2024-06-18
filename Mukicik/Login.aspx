<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mukicik.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mukicik: Login</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.validate/1.19.3/jquery.validate.min.js"></script>
    <style>
        .navbar {
            overflow: hidden;
            background-color: #333;
        }

        .navbar a, .dropbtn {
            float: left;
            display: block;
            text-align: center;
            text-decoration: none;
            background-color: inherit;
            color: #007bff;
        }

        .navbar a:hover, .dropdown:hover .dropbtn {
            background-color: red;
        }

        .dropdown {
            float: left;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

        .dropdown-content a {
            float: none;
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
            text-align: left;
        }

        .dropdown-content a:hover {
            background-color: #ddd;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>
    <link href="styles.css" rel="stylesheet" />
</head>
<body>
    <header class="jumbotron jumbotron-fluid text-center" style="padding: 0; height:200px">
        <h1 class="display-4">Mukicik</h1>
        <p class="lead">A fun and educational music learning platform</p>
        <nav class="nav justify-content-around">
            <div class="d-flex flex-row">
                <a class="nav-link" href="Index.aspx">Home</a>
                <div class="dropdown">
                  <p class="dropbtn nav-link">Products</p>
                  <div class="dropdown-content">
                    <a href="ProductList.aspx">View Products</a>
                  </div>
                </div>
            </div>
            <div class="d-flex flex-row">
                <a class="nav-link" href="Register.aspx">Register</a>
                <a class="nav-link" href="Login.aspx">Login</a>
            </div>
        </nav>
    </header>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mt-4">Login</h2>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="LabelEmail" runat="server" Text="Email:" AssociatedControlID="TextBoxEmail" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelPassword" runat="server" Text="Password:" AssociatedControlID="TextBoxPassword" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" />
                       
                    <div class="form-check mt-2">
                        <asp:CheckBox ID="CheckBoxRememberMe" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="LabelRememberMe" runat="server" Text="Remember Me" AssociatedControlID="CheckBoxRememberMe" CssClass="form-check-label"></asp:Label>
                    </div>

                    <asp:Button ID="ButtonLogin" runat="server" CssClass="btn btn-primary btn-block mt-3" Text="Login" OnClick="ButtonLogin_Click" />
                    <asp:Label ID="LabelErrorMessage" runat="server" CssClass="text-danger mt-2" Visible="False"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Image ID="ViolinImage" runat="server" ImageUrl="dd25914bb9debff8935338f60c650b56.png" style="max-height: 450px" />
                </div>
            </div>
        </div>
    </form>
    <footer>
        2024 - Mukicik.
    </footer>
</body>
</html>

