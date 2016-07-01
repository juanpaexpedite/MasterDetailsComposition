(function ($) {
	var globalFunctions = ch9.functions,
		helpers = globalFunctions.helpers,
		resources = globalFunctions.Resources;

	var $holder = $('.player-text .countdown');

	var drawTime = function (countToDateTime) {
		var now = new Date().getTime();

		if (countToDateTime > now) {
			var timeLeft = countToDateTime - now;

			var days = (timeLeft > 86400000) ? Math.floor(timeLeft / 86400000) : 0;
			timeLeft = timeLeft - days * 86400000;

			var hours = (timeLeft > 3600000) ? Math.floor(timeLeft / 3600000) : 0;
			timeLeft = timeLeft - hours * 3600000;

			var minutes = (timeLeft > 60000) ? Math.floor(timeLeft / 60000) : 0;
			timeLeft = timeLeft - minutes * 60000;

			var seconds = (timeLeft > 1000) ? Math.floor(timeLeft / 1000) : 0;

			$holder.html(
				(days > 0 ? (helpers.plural(days, days.toString(), resources.getString("JsDateTimeDay"), resources.getString("JsDateTimeDays")) + ', ') : '')
				+ helpers.plural(hours, hours.toString(), resources.getString("JsDateTimeHour"), resources.getString("JsDateTimeHours")) + ', '
				+ helpers.plural(minutes, minutes.toString(), resources.getString("JsDateTimeMinute"), resources.getString("JsDateTimeMinutes")) + ', '
				+ helpers.plural(seconds, seconds.toString(), resources.getString("JsDateTimeSecond"), resources.getString("JsDateTimeSeconds"))
			);
		}
	};

	$(function () {
		var countTo = $(".player-text").data("countto") || 0;
		if (!$holder.length) {
			$holder = $('.player-text .countdown');
		}
		if (countTo && countTo > 0) {
			var now = new Date().getTime();
			var countToDateTime = now + countTo;
			var countdownTimer = setInterval(function () {
				drawTime(countToDateTime);
			}, 1000);
		}
	});

})(jQuery);