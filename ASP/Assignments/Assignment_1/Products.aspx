<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ASP_A1.Products" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Products</title>
    <style>
        body { font-family: Segoe UI, Arial; }
        .wrap { margin:20px; }
        img { border:1px solid #ddd; padding:6px; }
        .row { margin:10px 0; }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div class="wrap">
    <h3>Pick a product</h3>

    <div class="row">
        <asp:DropDownList ID="ddlProducts" runat="server"
            AutoPostBack="true" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

    <div class="row">
        <asp:Image ID="imgProduct" runat="server" Width="260" Height="180" />
    </div>

    <div class="row">
        <asp:Button ID="btnGetPrice" runat="server" Text="Get Price"
            OnClick="btnGetPrice_Click" />
    </div>

    <div class="row">
        <asp:Label ID="lblPrice" runat="server" Font-Size="Large"></asp:Label>
    </div>
</div>
</form>
</body>
</html>
