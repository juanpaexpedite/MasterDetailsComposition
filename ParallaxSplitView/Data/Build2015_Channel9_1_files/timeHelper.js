(function () {
	var globalFunctions = ch9.functions,
		resources = globalFunctions.Resources;

	$('html').addClass('hasTimeHelper');
	var isEmbeded = $('html').hasClass('embeded');

	var removeDialog = function(){
		$('.timeHelperDialog').add('.timeHelperBG').remove();
	}

	ch9.functions.enableTimeHelper = function(){
		$('time.timeHelper')
		.on('selectstart', function(e){
			e.preventDefault();
		})
		.on('click', function(e){
			var $this = $(this);
			
			if($this.hasClass('minClick')){
				var thisOffset = $this.offset();
				if( !((Math.floor(thisOffset.left) + $this.width() < e.pageX) && (Math.floor(thisOffset.top) + $this.height() - 15 < e.pageY)) ){
					return;
				}
			}

			e.preventDefault();
			e.stopPropagation();

			var thisMessage = $this.data('message');
			var displayDialog = function(message) {
				$('body').append(
					$('<div></div>')
						.addClass('timeHelperBG')
						.click(function(){
							removeDialog();
						})
				)
				.append(
					$('<div></div>')
						.addClass('timeHelperDialog')
						.append(message)
						.append('<div><button>' + resources.getString("JsNavigationClose") + '</button></div>')
						.click(function(){
							removeDialog();
						}).ifThen(isEmbeded, function () {
							$(this).position({
								my: 'top',
								at: 'top',
								of: e
							})
							.css({
								'position':'absolute',
								'left': '2%',
								'right': '2%'
							});
						})
				);			
			};
			
			var displaySorry = function(){
				displayDialog('<div>' + resources.getString("JsTimeHelperNoInfo") + '</div>');
			};

			if (thisMessage) {
				if (thisMessage.length) {
					thisMessage = "<div>" + thisMessage.replace(/\n|\n\r/g, "<br>") + "</div>";
				}
				displayDialog(thisMessage);
			}else{
				displaySorry();
			}
		});
	};

	$(function () {
		ch9.functions.enableTimeHelper();
	});

})(jQuery);
