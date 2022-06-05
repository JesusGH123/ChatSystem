<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Chat.Index"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <style>
        Gridview {
            border: 0px;
        }
    </style>
</head>
<body>
   
    <form id="form1" runat="server">
     <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <img style="width:30px; margin: 10px;" src="Content/chatlogo.jpeg"/>
        <a class="navbar-brand" href="#">Chutbook</a>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="#">Home</a>
            </li>
           
          </ul>
          <form class="d-flex">
            <asp:TextBox class="form-control me-2" ID="userSearchtxt" style="width: 300px" runat="server"></asp:TextBox>
            <asp:Button class="btn btn-sucess" ID="buttonSearch" runat="server" Text="Search" OnClick="buttonSearch_Click" />
          </form>
        </div>
        <asp:Button class="btn btn-warning align-items-end" ID="block_button" runat="server" Text="Logout" OnClick="btnLogout_Click" />
      </div>
    </nav>

    <section class="d-flex">
        <div class="w-75 p-3 ">

            &nbsp;<asp:GridView ID="MessageGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="MessageGrid_SelectedIndexChanged">
               <Columns>
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="id" runat="server" Text='<%#Eval("id") %>' style="display: none;"> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="username" runat="server" Text='<%#Eval("username") %>' > </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="last_message_preview" runat="server" Text='<%#Eval("last_message_preview")+"..." %>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="delete_button" runat="server" Text="Delete" OnClick="delete_click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="block_button" runat="server" Text="Block" OnClick="block_click" />
                        </ItemTemplate>       
                    </asp:TemplateField>
              </Columns>
            </asp:GridView>

            <asp:Label ID="MessageLabel" runat="server"></asp:Label>
       </div>

       <div class="w-25 p-3">
          <h2>Requests</h2>
           &nbsp;<asp:GridView ID="RequestGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="MessageGrid_SelectedIndexChanged">
               <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="username" runat="server" Text='<%#Eval("username")%>' style="width: 50px;"> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="accept_invite_button" class="button button-success" runat="server" Text="Accept" OnClick="block_click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField>
                        <ItemTemplate>
                           <asp:Button ID="deny_invite_button" class="button button-danger" runat="server" Text="Block" OnClick="block_click" />
                        </ItemTemplate>
                    </asp:TemplateField>
              </Columns>
            </asp:GridView>
           <asp:Label ID="RequestLabel" runat="server" Text=""></asp:Label>

       </div> 
    </section>
    </form>

    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html> 