<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Chat.Login" ValidateRequest="false"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html, body {
            margin: 0;
            height: 100%;
        }
    </style>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
</head>
<body class="d-flex align-items-center">
    <form id="form1" class="w-100" runat="server">
        <div class="card w-25 p-3 mx-auto">
          <div class="card-body">
            <img class="rounded mx-auto d-block p-3" width="100px" src="Content/chatlogo.jpeg" alt="logo" />
            <div class="input-group mb-3">
                <span class="input-group-text" id="basic-addon1">Id</span>
                <asp:TextBox ID="id_textbox" runat="server" type="text" class="form-control" placeholder="Id" aria-label="Id" aria-describedby="basic-addon1"></asp:TextBox>
            </div>
            <div class="input-group mb-3">
                <span class="input-group-text" id="basic-addon1">Password</span>
                <asp:TextBox ID="password_textbox" runat="server" type="password" class="form-control" placeholder="Password" aria-label="Password" aria-describedby="basic-addon1"></asp:TextBox>
            </div>
            <a href="Register.aspx">Register</a>
            <asp:Button runat="server" id="btnLogin" Text="Log In" class="btn btn-primary" OnClick="btnLogin_Click"/>
          </div>
            <asp:Label ID="msg_label" runat="server" Text=""></asp:Label>
        </div>
    </form>
        
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</body>
</html>
