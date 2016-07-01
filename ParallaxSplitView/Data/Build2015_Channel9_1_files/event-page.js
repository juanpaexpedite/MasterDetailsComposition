(function($) {

$(document).on('click', '#event-schedule .timeslot-toggle a', function(event) {
    $(this).parents('.timeslot-collapsed, .timeslot-expanded').toggleClass('timeslot-collapsed timeslot-expanded');

    var holder = $(this).parents('.schedule-timeslot').find('.timeslot-holder');
    var url = holder.data('href');
    if (holder && url && !$.trim(holder.html())) {
        holder.html("<img src='/styles/images/ui-anim_basic_16x16.gif' alt='loading...' class='loading' />");

        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            success: function (data, status, xhr) {
				if (status == "success") {
					holder.html(data);
					if(!$('body').hasClass('noRating')){
						setTimeout(function(){ 
							ch9.functions.initRatings(); 
						},10);
					}
                } else {
                    timeslotError(holder, data);
                }
            },
            error: function (xhr, status, error) {
                timeslotError(holder, xhr);
            },
            complete: function () {
                $(this).blur();
            },
            timeout: 10000
        });
    }
});

var timeslotError = function (holder, data) {
    holder.html(data);
}

$(document).on('click', '#event-schedule .session-toggle a', function(event) {
	$(this).parents('.session-collapsed, .session-expanded').toggleClass('session-collapsed session-expanded');
});

$(document).on('click', '#toggleMySessions>span.toggle', function () {
	$(this).toggleClass('toggled');
	$('#event-schedule').toggleClass('my-sessions-only');
	return false;
});

$(document).on('click', '#toggleSessionsOnly>span.toggle', function (event) {
	$(this).toggleClass('toggled');
	$('#event-schedule').toggleClass('sessions-only');
	return false;
});

$(document).on('click', '#toggleSessionsOnly>a#all', function (event) {
	$(this).siblings('.toggle').removeClass('toggled');
	$('#event-schedule').removeClass('sessions-only');
	return false;
});

$(document).on('click', '#toggleSessionsOnly>a#sessionsOnly', function (event) {
	$(this).siblings('.toggle').addClass('toggled');
	$('#event-schedule').addClass('sessions-only');
	return false;
});

})(jQuery);