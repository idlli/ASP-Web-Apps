<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionLivraison.aspx.cs" Inherits="GestionCommercialWeb.GestionLivraison" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
    background: #dfe4ea;
    text-align: center;
    margin: 50px auto;
}

form#form1 {
    /* border-radius: 5px; */
    /* background: #a4b0be; */
    display: inline-block;
    /* padding: 50px; */
}

.container {
    margin-bottom: 20px;
    border-radius: 5px;
    background: #a4b0be;
    padding: 50px 80px;
}

input#Button4 {
    display: block;
    float: left;
    vertical-align: bottom;
    color: #f1f2f6;
    background: #57606f;
}

span {
    color: #fff;
    text-align: center;
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

p {
    color: #fff;
    font-family: system-ui;
}

.containerr > div {
    display: inline-block;
    margin: 10px 10px 25px 10px;
}

table#GridView1 {
    background: #dfe4ea;
    border-color: #a4b0be;
    color: #57606f;
    font-family: system-ui;
    border-radius: 3px;
    overflow: hidden;
    display: flex;
    justify-content: center;
    border: 0;
}

th {
    padding: 10px 20px;
}

td {
    padding: 8px 16px;
}

input[type="submit"] {
    padding: 10px 25px;
    cursor: pointer;
    margin-top: 15px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <span>N Commande : </span>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div class="containerr">
            <div>
                <p>N Livraison : </p>
                <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            </div>
            <div>
                <p>Date Livraison : </p>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div>
                <p>Livreur : </p>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Enregistrer la Livraison" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Valider la Livraison" OnClick="Button2_Click" />
        </div>
        <asp:Button ID="Button3" runat="server" Text="Bon de Livraison" OnClick="Button3_Click" />
        </div>
        <asp:Button ID="Button4" runat="server" Text="Retour" OnClick="Button4_Click" />
    </form>
</body>
</html>
