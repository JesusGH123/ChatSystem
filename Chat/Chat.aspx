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
        <input class="btn btn-primary" type="submit" value="&#8592">
        <h1 style="text-align:center;color:green;"> Friend </h1> 
        <div class="container border w-100 m-2"> 
            <div class="container"> 
                <div class="row"> 
                    <div class="card-body border m-3">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <h6 class="card-subtitle mb-2 text-muted">2021-03-01 3:45</h6>
                        <input class="btn btn-primary" type="submit" value="Edit">
                        <input class="btn btn-primary" type="submit" value="Delete">
                    </div>
                    <div class="card-body border m-3">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <h6 class="card-subtitle mb-2 text-muted">2021-03-01 3:45</h6>
                        <input class="btn btn-primary" type="submit" value="Edit">
                        <input class="btn btn-primary" type="submit" value="Delete">
                    </div>
                    <div class="card-body border m-3">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <h6 class="card-subtitle mb-2 text-muted">2021-03-01 3:45</h6>
                        <input class="btn btn-primary" type="submit" value="Edit">
                        <input class="btn btn-primary" type="submit" value="Delete">
                    </div>
                    <div class="card-body border m-3">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <h6 class="card-subtitle mb-2 text-muted">2021-03-01 3:45</h6>
                        <input class="btn btn-primary" type="submit" value="Edit">
                        <input class="btn btn-primary" type="submit" value="Delete">
                    </div>
                </div> 
            </div> 
        </div>

        <div class="input-group">
            <textarea class="form-control" aria-label="With textarea"></textarea>
            <input class="btn btn-primary" type="submit" value="Send">
        </div>
    </form>
</body>
</html>
