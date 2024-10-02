/*A jQuery plugin which add loading indicators into buttons
* By Minoli Perera
* MIT Licensed.
*/
(function ($) {
    $('.has-spinner').attr("disabled", false);
    $.fn.buttonLoader = function (action) {
        var self = $(this);
        //console.log(self);
        if (action == 'start') {
            if ($(self).hasClass("disabled")) {
                return false;
            }
            $(self).addClass("disabled");
            $(self).attr('data-btn-text', $(self).html());
            var text = 'Procesando';
            //console.log($(self).attr('data-load-text'));
            if($(self).attr('data-load-text') != undefined && $(self).attr('data-load-text') != ""){
                var text = $(self).attr('data-load-text');
            }
            $(self).html('<i class="fa fa-spinner fa-spin" title="button-loader"></i> '+text);
            $(self).addClass('active');
        }
        if (action == 'stop') {
            $(self).html($(self).attr('data-btn-text'));
            $(self).removeClass('active');
            $(self).removeClass("disabled");
        }
    }
})(jQuery);
