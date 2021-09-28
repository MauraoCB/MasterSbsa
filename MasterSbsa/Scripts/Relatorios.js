$(document).ready(function () {
	$("#Centro").fadeIn(), $("#Rel").fadeOut(), $("#MainContent_CrViewer__UI_mb").fadeOut(), $("#MainContent_CrViewer__UI_mb").fadeOut(), $("#MainContent_CrViewer__UI_bc").fadeOut(), $("#MainContent_CrViewer__UI_bc").fadeOut()
});

function MostraRel() {
	$("#Centro").fadeOut(), $("#topo").fadeOut(), $("#Rel").fadeIn(1500)
};

function EscondeRel() {
	$("#Centro").fadeIn(1500),
    $("#topo").fadeIn(1500),
    $("#Rel").fadeOut(),
    $("#uppRel").fadeOut(),
    $("#MainContent_CrViewer__UI_mb").fadeOut(),
    $("#MainContent_crViewer__UI_mb").fadeOut(),
    $("#MainContent_CrViewer__UI_bc").fadeOut(),
    $("#MainContent_crViewer__UI_bc").fadeOut()
};