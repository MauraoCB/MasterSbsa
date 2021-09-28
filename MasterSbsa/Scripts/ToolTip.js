/*
* Js desenvolvido para exibir um tooltip personalizado.
* WVS - 08/08/2014
*/
$(function () {
    var Time = 2500;
    $(document).tooltip({
        show: {
            effect: "fade",
            delay: 50
        },
        open: function (event, ui) {
            setTimeout(function () {
                $(ui.tooltip).hide('fade');
            }, Time);
        },
        hide: {
            effect: "fade",
            delay: 150
        },
	    close: function(event, ui) {
	        ui.tooltip.closest('.ui-effects-wrapper').remove(),
	        ui.tooltip.closest('.ui-tooltip ui-widget ui-corner-all ui-widget-content').remove();
	    },
        position: {
            my: "center bottom-30",
            at: "center",
            using: function (position, feedback) {
                $(this).css(position);
                $("<div>")
    .addClass("arrow")
    .addClass(feedback.vertical)
    .addClass(feedback.horizontal)
    .appendTo(this);
            }
        }
    });
});