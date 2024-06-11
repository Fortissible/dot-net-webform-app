<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update_delete.aspx.cs" Inherits="Mukicik.update_delete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mt-4">Modify Products</h2>
            <asp:GridView ID="GridView1" DataKeyNames="ProductId" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="SelectButton" runat="server" CommandName="Select" Text="Select" CssClass="btn btn-link" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductId" HeaderText="ProductId" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                    <asp:BoundField DataField="ProductPrice" HeaderText="ProductPrice" />
                    <asp:BoundField DataField="ProductImage" HeaderText="ProductImage" />
                    <asp:BoundField DataField="ProductRating" HeaderText="ProductRating" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="form-group">
                <label for="ProductId">Product Id</label>
                <asp:TextBox ID="ProductId" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ProductName">Product Name</label>
                <asp:TextBox ID="ProductName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ProductPrice">Product Price</label>
                <asp:TextBox ID="ProductPrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ProductImage">Product Image</label>
                <asp:TextBox ID="ProductImage" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ProductRating">Product Rating</label>
                <asp:TextBox ID="ProductRating" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="UpdateButton" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="UpdateButton_Click" style="margin:0 0 32px 0"/>
        </div>
    </form>
    <footer>
        2024 - Mukicik.
    </footer>
</body>
</html>
