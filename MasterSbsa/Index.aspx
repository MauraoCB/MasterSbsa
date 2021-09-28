<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="MasterSbsa.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .RodapeIndex
        {
            background: #eaeced !important;
        }
        @media (max-width: 766px)
        {
            #h
            {
                padding-top: 15% !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="h">
                <div class="container">
                    <div class="row">
                        <div class="col-md-8 col-md-offset-2 welcome">
                            <h1 runat="server" id="h1BemVindo" visible="false"></h1>
                            <h1 runat="server" id="h1Sistema" visible="false"></h1>
                            <hr class="aligncenter">
                            <p><%=MensagemTelaInicial()%></p>
                        </div>
                    </div>
                </div>
            </div>
            <nav class="navbar navbar-default  navbar-fixed-bottom RodapeIndex" role="navigation">
				<div class="container text-center">
					<p class="navbar-text col-md-12 col-sm-12 col-xs-12">Santos Brasil</p>
				</div>
			</nav>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
