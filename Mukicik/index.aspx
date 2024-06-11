<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Mukicik.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.validate/1.19.3/jquery.validate.min.js"></script>
    <style>    
        .text-danger {
            height: 16px;
        }

        .product-container {
            width: 100%;
            overflow-x: auto;
            white-space: nowrap;
            padding: 20px 0;
        }

        .product {
            display: inline-block;
            width: 150px;
            margin-right: 20px;
            text-align: center;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 10px;
            background-color: #fff;
        }

        .product img {
            width: 100px;
            height: 100px;
            object-fit: contain;
        }

        .product-title {
            font-size: 1rem;
            margin: 10px 0;
        }

        .product-price {
            color: #888;
            font-size: 0.9rem;
            margin: 5px 0;
        }

        .product-rating {
            color: #f39c12;
            font-size: 0.9rem;
        }

        .product-container::-webkit-scrollbar {
            height: 8px;
        }

        .product-container::-webkit-scrollbar-thumb {
            background: #ccc;
            border-radius: 4px;
        }

        .product-container::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

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
        <nav class="nav justify-content-center">
            <a class="nav-link" href="Index.aspx">Home</a>
            <a class="nav-link" href="Register.aspx">Register</a>
            <div class="dropdown">
              <p class="dropbtn nav-link">Dropdown</p>
              <div class="dropdown-content">
                <a href="insert.aspx">Insert Product</a>
                <a href="update_delete.aspx">Update/Delete Product</a>
              </div>
            </div>
        </nav>
    </header>
    
    <div class="container content-container">  <div class="row">
            <div class="col-md-6 image-container">  
                <asp:Image ID="Image1" runat="server" Height="520px" ImageAlign="Right" src="kisspng-acoustic-guitar-cutaway-acoustic-electric-guitar-c-spanish-guitar-5b2f9222e699d9.3264763315298442589446.jpg" ImageUrl="~/kisspng-acoustic-guitar-cutaway-acoustic-electric-guitar-c-spanish-guitar-5b2f9222e699d9.3264763315298442589446.jpg" />
            </div>
            <div class="col-md-6 text-content">  <h1 style="font-size:66px;">Mukicik</h1>
                <h3>
                    An innovative music selling store with competitive prices.
                    Lorem ipsum dolor sit amet.
                </h3>
                <form id="form1" runat="server">
                    <div>
                        <asp:Button ID="Button1" runat="server" Text="Be a new member" CssClass="btn, btn-primary" PostBackUrl="./Register.aspx" />
                    </div>
                </form>
            </div>
        </div>
    </div>

    <h2 class="section-title" style="margin: 0px 16px 8px 64px">Top Products</h2>

    <div id="ProductContainer" class="product-container" style="margin: 0px 0px 8px 8px" runat="server">
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.validate/1.19.3/jquery.validate.min.js"></script>
    <footer>
        2024 - Mukicik.
    </footer>
</body>
</html>

