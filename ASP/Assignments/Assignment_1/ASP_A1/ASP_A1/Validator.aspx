<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Validator.aspx.cs" Inherits="ASP_A1.Validator" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Validator.aspx</title>
    <style>
        body { font-family: Arial; }
        .label { width: 120px; display: inline-block; }
        .input { background-color: #FFFFCC; }
        .req { color: red; }
        .hint { font-size: 12px; color: gray; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <h3>Insert your details :</h3>

    <div>
        <span class="label">Name:</span>
        <asp:TextBox ID="txtName" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvName" runat="server"
            ControlToValidate="txtName"
            ErrorMessage="Name is required." Display="Dynamic" />
    </div>

    <div>
        <span class="label">Family Name:</span>
        <asp:TextBox ID="txtFamily" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvFamily" runat="server"
            ControlToValidate="txtFamily"
            ErrorMessage="Family Name is required." Display="Dynamic" />
        <span class="hint">differs from name</span>
        <asp:CompareValidator ID="cvDiff" runat="server"
            ControlToCompare="txtName" ControlToValidate="txtFamily"
            Operator="NotEqual" Type="String"
            ErrorMessage="Family name must differ from name."
            Display="Dynamic" />
    </div>

    <div>
        <span class="label">Address:</span>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
            ControlToValidate="txtAddress"
            ErrorMessage="Address is required." Display="Dynamic" />
        <span class="hint">at least 2 chars</span>
        <asp:RegularExpressionValidator ID="revAddress" runat="server"
            ControlToValidate="txtAddress"
            ValidationExpression="^.{2,}$"
            ErrorMessage="Address must be at least 2 characters."
            Display="Dynamic" />
    </div>

    <div>
        <span class="label">City:</span>
        <asp:TextBox ID="txtCity" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server"
            ControlToValidate="txtCity"
            ErrorMessage="City is required." Display="Dynamic" />
        <span class="hint">at least 2 chars</span>
        <asp:RegularExpressionValidator ID="revCity" runat="server"
            ControlToValidate="txtCity"
            ValidationExpression="^[A-Za-z\s]{2,}$"
            ErrorMessage="City must be at least 2 letters."
            Display="Dynamic" />
    </div>

    <div>
        <span class="label">Zip Code:</span>
        <asp:TextBox ID="txtZip" runat="server" CssClass="input" MaxLength="5"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvZip" runat="server"
            ControlToValidate="txtZip"
            ErrorMessage="Zip code is required." Display="Dynamic" />
        <span class="hint">(xxxxx)</span>
        <asp:RegularExpressionValidator ID="revZip" runat="server"
            ControlToValidate="txtZip"
            ValidationExpression="^\d{5}$"
            ErrorMessage="Zip must be 5 digits."
            Display="Dynamic" />
    </div>

    <div>
        <span class="label">Phone:</span>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server"
            ControlToValidate="txtPhone"
            ErrorMessage="Phone is required." Display="Dynamic" />
        <span class="hint">(XX-XXXXXXX / XXX-XXXXXXX)</span>
        <asp:RegularExpressionValidator ID="revPhone" runat="server"
            ControlToValidate="txtPhone"
            ValidationExpression="^(\d{2}-\d{7}|\d{3}-\d{7})$"
            ErrorMessage="Invalid phone format."
            Display="Dynamic" />
    </div>

    <div>
        <span class="label">E-Mail:</span>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox>
        <span class="req">*</span>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
            ControlToValidate="txtEmail"
            ErrorMessage="Email is required." Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revEmail" runat="server"
            ControlToValidate="txtEmail"
            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
            ErrorMessage="Invalid email address."
            Display="Dynamic" />
    </div>

    <div style="margin-top:10px;">
        <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
    </div>

    <asp:ValidationSummary ID="vs" runat="server"
        HeaderText="ValidationSum"
        ShowMessageBox="true"
        ShowSummary="false" />
    </form>
</body>
</html>
