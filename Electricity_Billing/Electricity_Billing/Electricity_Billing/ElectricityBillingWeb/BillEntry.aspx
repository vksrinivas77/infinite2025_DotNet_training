<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillEntry.aspx.cs" Inherits="Electricity_Billing.ElectricityBillingWeb.BillEntry" %>
<!DOCTYPE html>
<html>
<head>
    <title>Electricity Bill Entry</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Add Electricity Bills</h2>
        <asp:Label ID="lblNumBills" runat="server" Text="Number of Bills to Add:"></asp:Label>
        <asp:TextBox ID="txtNumBills" runat="server"></asp:TextBox>
        <asp:Button ID="btnCreateFields" runat="server" Text="Proceed" OnClick="btnCreateFields_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" style="margin-left:10px" />
        <asp:Button ID="btnretrive" runat="server" OnClick="btnretrive_Click" Text="Retrive" />
        <br />
        <asp:Panel ID="pnlBillInputs" runat="server"></asp:Panel>
        <asp:Button ID="btnSubmitBills" runat="server" Text="Submit Bills" OnClick="btnSubmitBills_Click" Visible="false" /><br />
        <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
        <hr />
        <!-- New panel for view bills grid -->
        <asp:Panel ID="pnlAllBills" runat="server">
            <h2>All Bills</h2>
            <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="False" EmptyDataText="No bills found.">
                <Columns>
                    <asp:BoundField HeaderText="Consumer Number" DataField="consumer_number" />
                    <asp:BoundField HeaderText="Consumer Name" DataField="consumer_name" />
                    <asp:BoundField HeaderText="Units Consumed" DataField="units_consumed" />
                    <asp:BoundField HeaderText="Bill Amount" DataField="bill_amount" DataFormatString="{0:N2}" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </form>
</body>
</html>
