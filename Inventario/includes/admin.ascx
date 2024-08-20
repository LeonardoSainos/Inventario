<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin.ascx.cs" Inherits="Inventario.includes.admin" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web"%>

<!DOCTYPE html>
<html>
<head>
    <uc:Links runat="server"/>
    <title>Administración</title>
    <link rel="icon" href="favicon.png">
</head>
<body>
    <div id="wrapper">
        <uc:Navbar runat="server"/> 
        <uc:NavBar2 runat="server" />
        <div id="page-wrapper">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-header">
                            <h1 class="animated lightSpeedIn">Panel Administrativo<small></small></h1>
                            <span class="label label-warning">Transporte de logística S.A de C.V</span>
                            <p class="pull-right text-primary">
                                <strong>
                                    <uc:TimeZone runat="server"/>
                                </strong>
                            </p>
                        </div>
                    </div>
                </div>                          
            </div>
            <div class="container">
                <asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
    <uc:Footer runat="server" />
    <uc:Script2 runat="server" />
</body>
</html>
