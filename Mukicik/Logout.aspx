<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Mukicik.Logout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logout</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LabelMessage" runat="server" Text="You have been logged out." />
            <asp:Label ID="LabelErrorMessage" runat="server" CssClass="text-danger mt-2" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
