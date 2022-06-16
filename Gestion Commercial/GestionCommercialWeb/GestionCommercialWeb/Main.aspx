<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GestionCommercialWeb.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
    background: #dfe4ea;
    text-align: center;
    margin: 15% auto;
}

form#form1 {
    display: inline-block;
    background: #a4b0be;
    padding: 30px 40px;
    border-radius: 3px;
}

li.static a {
    color: #57606f;
    font-family: system-ui;
}

li.static {
    background: #ced6e0;
    margin: 15px;
    padding: 12px 46px;
    border-radius: 3px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Menu ID="Menu1" runat="server">
                <Items>
                    <asp:MenuItem Text="Gestion Commandes" Value="Gestion Commandes" NavigateUrl="~/GestionCommandes.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Factures" Value="Factures" NavigateUrl="~/Factures.aspx"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
    </form>
</body>
</html>
