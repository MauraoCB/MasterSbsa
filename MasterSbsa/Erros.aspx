<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
	CodeBehind="Erros.aspx.cs" Inherits="MasterSbsa.Erros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		.divCentro
		{
			width: 80% !important;
		}
		.table-fixed thead
		{
			width: 97%;
		}
		.table-fixed tbody
		{
			height: 230px;
			overflow-y: auto;
			width: 100%;
		}
		.table-fixed thead, .table-fixed tbody, .table-fixed tr, .table-fixed td, .table-fixed th
		{
			display: block;
		}
		.table-fixed tbody td, .table-fixed thead > tr > th
		{
			float: left;
			border-bottom-width: 0;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="divCentro">
			<h3 class="h3">Mensagens</h3>
			<div class="row">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4>
							Gestão de Jornada
						</h4>
					</div>
					<table class="table table-fixed">
						<thead>
							<tr>
								<th class="col-xs-1">
									Cod.
								</th>
								<th class="col-xs-5">
									Descrição
								</th>
								<th class="col-xs-5">
									Ação
								</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td class="col-xs-1">
									1
								</td>
								<td class="col-xs-5">
									Você não é autorizado(a) a visualizar as informações deste funcionário(a).
								</td>
								<td class="col-xs-5">
									Solicitar ao RH a delegação do centro de custo ao seu usuário.
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
