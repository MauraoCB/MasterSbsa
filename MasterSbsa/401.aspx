<%@ Page Title="" Language="C#" MasterPageFile="~/Detalhe.Master" AutoEventWireup="true" CodeBehind="401.aspx.cs" Inherits="MasterSbsa._401" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .error-template {
            padding: 40px 15px;
            text-align: center;
        }

        .error-actions {
            margin-top: 15px;
            margin-bottom: 15px;
        }

            .error-actions .btn {
                margin-right: 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="error-template">
                    <h1>Oops!
                    </h1>
                    <h2>Página não disponível
                    </h2>
                    <h3>Desculpe, mas parece que você precisa fazer uma nova autenticação =/
                    </h3>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
