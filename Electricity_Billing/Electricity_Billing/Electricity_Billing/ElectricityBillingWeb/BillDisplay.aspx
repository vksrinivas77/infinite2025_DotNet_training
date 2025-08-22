<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillDisplay.aspx.cs" Inherits="Electricity_Billing.ElectricityBillingWeb.BillDisplay" %>
<!DOCTYPE html>
<html>
<head>
    <title>Display Last N Bills</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Retrieve Last N Bill Details</h2>
        <asp:Label ID="lblNumToRetrieve" runat="server" Text="Number of Bills to Retrieve:"></asp:Label>
        <asp:TextBox ID="txtNumToRetrieve" runat="server"></asp:TextBox>
        <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" />
        <asp:Button ID="back" runat="server" OnClick="back_Click" Text="back" />
        <br /><br />
        <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer Number" />
                <asp:BoundField DataField="ConsumerName" HeaderText="Consumer Name" />
                <asp:BoundField DataField="UnitsConsumed" HeaderText="Units Consumed" />
                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
