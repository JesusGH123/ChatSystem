<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Chat.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <img class="m-2" width="30px" src="Content/chatlogo.jpg" alt="logo"/>
      <a class="navbar-brand" href="#">Chutbook	&#174;</a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <form class="form-inline my-2 my-lg-0">
          <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
        </form>
      </div>
      <div>
          <i>User</i>
          <a href="Login.aspx">Logout</a>
      </div>
    </nav>

    <section class="d-flex">
        <div class="w-75 p-3 ">
            <ul class="list-group">
                <li class="list-group-item d-flex justify-content-between align-items-center">
                <h5>Friend1</h5>
                Cras justo odio
                <span class="badge badge-primary badge-pill">14</span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                <h5>Friend2</h5>
                Dapibus ac facilisis in
                <span class="badge badge-primary badge-pill">2</span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                <h5>Friend3</h5>
                Morbi leo risus
                <span class="badge badge-primary badge-pill">1</span>
                </li>
            </ul>
       </div>
        <div class="w-25 p-3 list-group">
          <h2>Requests</h2>
          <a href="#" class="list-group-item list-group-item-action flex-column">
            <div class="d-flex w-100 justify-content-around">
              <h5 class="mb-1">User1</h5>
              <button type="button" class="btn btn-light">Accept</button>
              <button type="button" class="btn btn-danger">Block</button>
              <small>3 days ago</small>
            </div>
          </a>
          <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
            <div class="d-flex w-100 justify-content-around">
              <h5 class="mb-1">User2</h5>
              <button type="button" class="btn btn-light">Accept</button>
              <button type="button" class="btn btn-danger">Block</button>
              <small class="text-muted">3 days ago</small>
            </div>
          </a>
          <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
            <div class="d-flex w-100 justify-content-around">
              <h5 class="mb-1">User3</h5>
              <button type="button" class="btn btn-light">Accept</button>
              <button type="button" class="btn btn-danger">Block</button> 
              <small class="text-muted">3 days ago</small>
            </div>
          </a>
        </div>
    </section>

    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
