<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="insert.aspx.cs" Inherits="Mukicik.insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mukicik: Insert</title>
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
    <header class="jumbotron jumbotron-fluid text-center" style="padding: 0">
        <h1 class="display-4">Mukicik</h1>
        <p class="lead">A fun and educational music learning platform</p>
        <nav class="nav justify-content-around">
            <div class="d-flex flex-row align-items-center">
                <a class="nav-link" href="Index.aspx">Home</a>
                <div class="dropdown">
                  <p class="dropbtn nav-link" style="margin-bottom: 0px">Products</p>
                  <div class="dropdown-content">
                    <a href="ProductList.aspx">View Products</a>
                    <a href="insert.aspx">Insert Product</a>
                    <a href="update_delete.aspx">Update/Delete Product</a>
                  </div>
                </div>
            </div>
            <div class="d-flex flex-row align-items-center">
                <p style="margin-bottom: 0px"><asp:Literal ID="LiteralUserName" runat="server" />!</p>
                <a class="nav-link" href="Logout.aspx">Logout</a>
            </div>
        </nav>
    </header>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mt-4">Insert Product</h2>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="LabelName" runat="server" CssClass="control-label" Text="Product Name"></asp:Label>
                    <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Name is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelPrice" runat="server" CssClass="control-label" Text="Product Price"></asp:Label>
                    <asp:TextBox ID="TextBoxPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrice" runat="server" ControlToValidate="TextBoxPrice" ErrorMessage="Price is required" CssClass="text-danger" Display="Dynamic" />
                    <asp:CustomValidator ID="CustomValidatorPrice" runat="server" ControlToValidate="TextBoxPrice" ErrorMessage="Invalid price value, use comma seperated value (,)" CssClass="text-danger" ClientValidationFunction="validatePrice"></asp:CustomValidator>
                    <script type="text/javascript">
                        function validatePrice(source, args) {
                            var price = parseFloat(args.Value);
                            if (!isNaN(price) && price >= 0) {
                                args.IsValid = true;
                            } else {
                                args.IsValid = false;
                            }
                        }
                    </script>

                    <asp:Label ID="LabelProfilePicture" runat="server" CssClass="control-label" Text="Product Picture"></asp:Label>
                    <asp:FileUpload ID="FileUploadProfilePicture" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPicture" runat="server" ControlToValidate="FileUploadProfilePicture" ErrorMessage="Image is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelRating" runat="server" CssClass="control-label" Text="Product Rating"></asp:Label>
                    <asp:TextBox ID="TextBoxRating" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRating" runat="server" ControlToValidate="TextBoxRating" ErrorMessage="Rating is required and in interval 0-5" CssClass="text-danger" Display="Dynamic" />
                    <asp:CustomValidator ID="CustomValidatorRating" runat="server" ControlToValidate="TextBoxRating" ErrorMessage="Invalid rating value, use comma seperated value (,)" CssClass="text-danger" ClientValidationFunction="validateRating" OnServerValidate="ValidateRating"></asp:CustomValidator>
                    <script type="text/javascript">
                        function validateRating(source, args) {
                            var rating = parseFloat(args.Value);
                            if (!isNaN(rating) && rating >= 0 && rating <= 5) {
                                args.IsValid = true;
                            } else {
                                args.IsValid = false;
                            }
                        }
                    </script>


                    <asp:Button ID="ButtonRegister" runat="server" CssClass="btn btn-primary" Text="Insert Product" OnClick="ButtonRegister_Click"/>
                </div>
                <div class="col-md-6">
                    <asp:Image ID="ViolinImage" runat="server" ImageUrl="dd25914bb9debff8935338f60c650b56.png" CssClass="img-responsive" />
                </div>
            </div>
        </div>
    </form>
    <footer>
        2024 - Mukicik.
    </footer>
</body>
</html>
