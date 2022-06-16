<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestionCommercialWeb.Login" %>

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
    background: #a4b0be;
    padding: 30px 40px;
    display: inline-block;
    border-radius: 5px;
}

span {
    color: #fff;
    font-family: system-ui;
}

input {
    background: #ced6e0;
    border: 0;
    padding: 10px;
    border-radius: 3px;
    color: #57606f;
    margin-left: 10px;
}

form#form1 > div {
    margin: 10px;
    text-align: left;
}

input#Button1 {
    padding: 10px 25px;
    margin: 10px;
    margin-left: 0;
    font-weight: bold;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span>Login : </span>
             <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <div>
            <span>Mot de Passe : </span>
             <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
