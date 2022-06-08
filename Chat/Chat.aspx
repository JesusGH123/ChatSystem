<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Chat.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <style>
      
       .vertical-scrollable> .row {
          position: absolute;
          top: 120px;
          bottom: 100px;
          left: 180px;
          width: 50%;
          overflow-y: scroll; 
        }
        .col-sm-8 { 
            color: white; 
            font-size: 24px; 
            padding-bottom: 20px; 
            padding-top: 18px; 
        } 
          
        .col-sm-8:nth-child(2n+1) { 
            background: green; 
        } 
          
        .col-sm-8:nth-child(2n+2) { 
            background: black; 
        } 


    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="←" OnClick="btnBack_Click"/>
        &nbsp;
        <asp:Label ID="FriendName" style="text-align:center;color:green;" runat="server" Text="Friend"></asp:Label>
        <div class="container w-100 m-2"> 
            <asp:GridView ID="MessageGrid" BorderWidth="0px" runat="server">
                 <Columns>
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="message" runat="server" Text='<%#Eval("content") %>' style="display: none;"> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="ButtonSend" class="btn btn-danger" runat="server" Text="X"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="datetime" runat="server" Text='<%#Eval("date_time").ToString() %>' style="color: gray"> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
              </Columns>
            </asp:GridView>
        </div>

        <div class="input-group">
            <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonSend" class="btn btn-primary" runat="server" Text="Send" OnClick="ButtonSend_Click"/>

        </div>
    </form>
</body>
</html>
