(function($) {

	var classes = {
		addToSchedule: '.addToSchedule',
		dialogSignIn: '.schedule-signin',
		dialogError: '.schedule-error',
		closeDialog: '.schedule-signin a.close, .schedule-error a.close',
		sessionContainer: '.schedule-session',
		pending: 'pending',
		onUserSchedule: 'user-schedule',
		timeslotOnUserSchedule: 'user-timeslot',
		timeslot: '.schedule-timeslot'
	};

	$(document).on('click', classes.addToSchedule, function() {
		var $this = $(this)
		var form = $this.parents("form");

		form.parents(classes.sessionContainer).addClass(classes.pending);

		$.ajax({
			type: form.attr("method"),
			url: form.attr('action'),
			success: function (data, status, xhr) {
				scheduleSuccess(data, status, xhr, form);
			},
			error: function (xhr, status, error) {
				scheduleError(form, xhr);
			},
			complete: function(){
				$this.blur();
			},
			timeout: 10000
		});

		return false;
	});

	var scheduleSuccess = function (data, status, xhr, form) {
		var container = form.parents(classes.sessionContainer);
		container.removeClass(classes.pending);
		container.toggleClass(classes.onUserSchedule);

		if (xhr.status == 201) {
			// session was added
			form.attr('method', 'DELETE');
			container.parents(classes.timeslot).addClass(classes.timeslotOnUserSchedule);
		} else if (xhr.status == 204) {
			// session was removed
			form.attr('method', 'POST');
			if (container.siblings(classes.sessionContainer + '.' + classes.onUserSchedule).length == 0) {
				container.parents(classes.timeslot).removeClass(classes.timeslotOnUserSchedule);
			}
		} else {
			scheduleError(form, xhr);
		}
		
	};

	var scheduleError = function(form, result) {
		form.parents(classes.sessionContainer).removeClass(classes.pending);

		if (result.status == 401) {
			form.find(classes.dialogSignIn).show();
		}
		else {
			form.find(classes.dialogError).show();
		}
	};

	$(document).on('click', classes.closeDialog, function () {
		$(this).parent('div').hide();
		return false;
	});

})(jQuery);