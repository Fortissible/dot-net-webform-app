<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Mukicik.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="styles.css" rel="stylesheet" />
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
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="LabelEmail" runat="server" CssClass="control-label" Text="Email"></asp:Label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic" />
                    <asp:CustomValidator ID="CustomValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Invalid email format" CssClass="text-danger" OnServerValidate="ValidateEmail"></asp:CustomValidator>

                    <asp:Label ID="LabelName" runat="server" CssClass="control-label" Text="Name"></asp:Label>
                    <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Name is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelPassword" runat="server" CssClass="control-label" Text="Password"></asp:Label>
                    <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelConfirmPassword" runat="server" CssClass="control-label" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="TextBoxConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPassword" runat="server" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Confirm Password is required" CssClass="text-danger" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Passwords do not match" CssClass="text-danger" Display="Dynamic" />
                    
                    <br />

                    <asp:Label ID="LabelGender" runat="server" CssClass="control-label" Text="Gender" style="display:contents; padding-right:8px; margin-right:8px"></asp:Label>
                    <asp:RadioButton ID="RadioButtonMale" runat="server" GroupName="Gender" Text="Male" style="padding-left:16px"/>
                    <asp:RadioButton ID="RadioButtonFemale" runat="server" GroupName="Gender" Text="Female" style="padding-left:16px"/>
                    <br />
                    <asp:CustomValidator ID="CustomValidatorGender" runat="server" ErrorMessage="Gender is required" CssClass="text-danger" OnServerValidate="ValidateGender"></asp:CustomValidator>

                    <asp:Label ID="LabelDateOfBirth" runat="server" CssClass="control-label" Text="Date of Birth"></asp:Label>
                    <asp:Calendar ID="CalendarDOB" runat="server" SelectedDate="<%# DateTime.Today %>" OnSelectionChanged="CalendarDOB_SelectionChanged"></asp:Calendar>
                    <asp:TextBox ID="TextBoxDOB" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDOB" runat="server" ControlToValidate="TextBoxDOB" ErrorMessage="Date of Birth is required" CssClass="text-danger" Display="Dynamic" />

                    <asp:Label ID="LabelProfilePicture" runat="server" CssClass="control-label" Text="Profile Picture"></asp:Label>
                    <asp:FileUpload ID="FileUploadProfilePicture" runat="server" CssClass="form-control" />

                    <asp:CheckBox ID="CheckBoxTerms" runat="server" Text="I agree to terms and conditions" />
                    <asp:CustomValidator ID="CustomValidatorTerms" runat="server" ErrorMessage="You must agree to the terms and conditions" CssClass="text-danger" OnServerValidate="ValidateTerms"></asp:CustomValidator>

                    <asp:Button ID="ButtonRegister" runat="server" CssClass="btn btn-primary" Text="Register" OnClick="ButtonRegister_Click"/>
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
