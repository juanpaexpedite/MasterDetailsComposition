(function ($) {
	var globalFunctions = ch9.functions,
		helpers = globalFunctions.helpers,
		resources = globalFunctions.Resources;

	var evalAPI = function(sessionPath) { return sessionPath + '/Evaluate'; };
	var evalFormClass = 'evalForm';
	var selectedClass = 'selected';
	var starSeriesClass = 'starSeries';
	var starClass = 'starSeriesStar';
	var feedbackClass = 'textFeedback';
	var autoPopTrigger = 'eval=True';
	var starSeriesNames = ['OverallScore','SpeakerScore'];

	var signInOutLink = function(){
		return $('.branding .signIn').html();
	};

	var getDataFromPage = function (dataName, param) {
		try {
			if (param) {
				return ch9.data[dataName][param];
			} else {
				return ch9.data[dataName];
			}
		} catch (e) {
			return null;
		}
	};

	var showEvaluateSession = function (evalUrl, title, sessionCanEvaluate, userCanEvaluate, isSignedIn, evaltype, attendeesonlyCanEvaluate) {
		var numOfQuestions = 3;

		var evalQuestions = getDataFromPage('evalQuestions', evaltype);
		if (!evalQuestions || (evalQuestions.length < numOfQuestions + 1)) {
			evalQuestions = ['item'];
		}
		
		var backgroundStyles = {
			'background-color': '#fff',
			'height': '100%',
			'left': '0px',
			'position': 'fixed',
			'opacity': '0.5',
			'top': '0px',
			'width': '100%',
			'z-index': '9996'
		};
		var dialogHolderStyles = {
			'background-color': '#018396',
			'border': '1px solid #000',
			'border-radius': '5px 5px 5px 5px',
			'left': '50%',
			'margin-left': '-250px',
			'padding': '15px',
			'position': 'fixed',
			'top': '5%',
			'width': '500px',
			'z-index': '9997'
		};
		var dialogStyles = {
			'background-color': '#fff',
			'border': '1px solid #333333',
			'border-radius': '5px',
			'padding': '15px'
		};
		var closeStyles = {
			'color': 'white',
			'cursor': 'pointer',
			'display': 'inline-block',
			'font-size': '13px',
			'margin-left': '5px',
			'padding': '4px 7px',
			'text-align': 'center'
		};
		var cancelStyles = {
			'float': 'right'
		};
		var buttonNormalStyles = {
			'background-color':'#712214'
		};
		var buttonOverStyles = {
			'background-color':'#96331D'
		};
		var titleStyles = {
			'color': '#fff',
			'font-weight': 'bold',
			'margin-bottom': '15px',
			'text-align': 'left'
		};
		var formStyles = {
			'display': 'none'
		};
		var starSeriesStyles = {
			'margin-bottom': '20px',
			'margin-top': '5px'
		};
		var starStyles = {
			'cursor': 'pointer',
			'display': 'inline-block',
			'height': '28px',
			'width': '40px'
		};
		var starNormalStyles = {
			'background': 'url("/styles/images/ratingStars.png") no-repeat scroll 0px 0px transparent'
		};
		var starOverStyles = {
			'background': 'url("/styles/images/ratingStars.png") no-repeat scroll 0px -28px transparent'
		};
		var starSelectedStyles = {
			'background': 'url("/styles/images/ratingStars.png") no-repeat scroll 0px -56px transparent'
		};
		var errorStyles = {
			'display': 'none',
			'line-height': 'normal',
			'margin-bottom': '0px',
			'padding-bottom': '10px'
		};
		var sorryHeadlineStyles = {
			'font-size': '120%',
			'margin-top': '0px'
		};
		var validaitonErrorStyles = {
			'display': 'none',
			'color': '#712514',
			'margin-top': '10px'
		};
		var textboxStyles = {
			'border': '1px solid #712514',
			'height': '60px',
			'margin-top': '5px',
			'width': '430px'
		};
		var submitButtonStyles = {
			'color': 'white',
			'cursor': 'pointer',
			'display': 'inline-block',
			'font-weight': 'bold',
			'margin-top': '5px',
			'padding': '12px',
			'text-align': 'center'
		};
		var initializingStyles = {
		};
		var evalDateStyles = {
			'color': '#9F9F9F',
			'font-size': '90%',
			'margin-bottom': '20px'
		};
		var uploadingStyles = {
			'display': 'none'
		};
		var finishedStyles = {
			'display': 'none'
		};

		/* event handlers */
		var closeDialog = function () {
			$background.remove();
			$dialogHolder.remove();
		};

		var starClick = function(e){
			$validationError.hide();
			$(this).prevAll().addBack().addClass(selectedClass);
			$(this).nextAll().removeClass(selectedClass);
		};

		var starEnter = function(){
			$(this).prevAll().addBack().css(starOverStyles);
			$(this).nextAll().css(starNormalStyles);
		};

		var starHolderLeave = function (target) {
			$(target).children().each(function (i, el) {
				var $this = $(this);
				$this.css($this.hasClass(selectedClass) ? starSelectedStyles : starNormalStyles);
			});
		};

		var buttonEnter = function(){
			$(this).css(buttonOverStyles);
		}

		var buttonLeave = function(){
			$(this).css(buttonNormalStyles);
		}

		var submitClicked = function () {
			var data = {};
			var $holder = $(this).parents('.' + evalFormClass);

			//get values from star series
			$holder.find('.' + starSeriesClass).each(function(){
				var $this = $(this);
				var fieldname = $this.data('fieldname');
				var value = 1;
				if(fieldname && fieldname.length > 0){
					$this.find('.' + starClass).each(function(){
						if( $(this).hasClass(selectedClass) ) {
							value = $(this).data('number');
							if(value > 0){
								data[fieldname] = value;
							}
						}
					});
				}
			});

			//get value from textarea
			var $feedbackEl = $holder.find('.' + feedbackClass);
			if($feedbackEl.length){
				var fieldname = $feedbackEl.data('fieldname');
				if(fieldname && fieldname.length){
					data[fieldname] = $feedbackEl.val();
				}
			}

			if(data && data[starSeriesNames[0]] && data[starSeriesNames[0]]>0 && data[starSeriesNames[1]] && data[starSeriesNames[1]]>0){
				showUploading();

				//submit the data
				$.ajax({
					type: 'POST',
					url: $holder.data('evalUrl'),
					data: data,
					success: function (json) {
						showFinished();
					},
					error: function () {
						showError();
					},
					dataType: 'text',
					timeout: 30000
				});
			}else{
				$validationError.show();
			}
		};


		/* util */
		var showSorry = function(){
			showState($sorry);
		};

		var showNotUser = function(){
			showState($notUser);
		};
		
		var showForm = function(){
			showState($form);
		};

		var showEveyoneMustSignIn = function () {
			showState($eveyoneMustSignIn);
		};

		var showMustSignIn = function(){
			showState($mustSignIn);
		};

		var showError = function(){
			showState($error);
		};

		var showUploading = function(){
			showState($uploading);
		};

		var showFinished = function(){
			showState($finished);
		};

		var showState = function($makeVisible){
			$initializing.hide();
			$form.hide();
			$error.hide();
			$uploading.hide();
			$finished.hide();
			$makeVisible.show();
		};

		var buildForm = function(json) {
			var makeStar = function(count, selected) {
				return $('<div></div>')
					.addClass(starClass)
					.data('number', count)
					.css(starStyles)
					.css(selected ? starSelectedStyles : starNormalStyles)
					.addClass(selected ? selectedClass : '')
					.click(starClick)
					.mouseenter(starEnter);
			};

			var makeStarSeries = function(fieldname, max, selected) {
				var $starSeries = $('<div></div>')
					.addClass(starSeriesClass)
					.css(starSeriesStyles)
					.data('fieldname', fieldname)
					.mouseleave(function (event) {
						if (event.relatedTarget != null) {
							starHolderLeave(event.currentTarget);
						}
					});

				for (var i = 1; i <= max; i++) {
					$starSeries.append(makeStar(i, (i <= selected)));
				}

				return $starSeries;
			};
			
			var makeLegend = function (text) {
				return $('<div></div>').html(text);
			};

			var $textbox = $('<textarea></textarea>')
				.addClass(feedbackClass)
				.css(textboxStyles)
				.text(json.Comment ? json.Comment : '')
				.data('fieldname', 'Comment');

			var $submitButton = $('<div></div>')
				.text(resources.getString("JsEventEvalSubmit"))
				.css(submitButtonStyles)
				.css(buttonNormalStyles)
				.click(submitClicked)
				.mouseenter(buttonEnter)
				.mouseleave(buttonLeave);

			var $evalDate = $('<div></div>')
				.css(evalDateStyles)
				.text(String.format(resources.getString("JsEventEvalDate"), evalQuestions[0], json.localizedModified));

			$form
				.append((json.localizedModified && json.localizedModified.length) ? $evalDate : '')
				.append(makeLegend(evalQuestions[1]))
				.append(makeStarSeries(starSeriesNames[0], 4, json.OverallScore))
				.append(makeLegend(evalQuestions[2]))
				.append(makeStarSeries(starSeriesNames[1], 5, json.SpeakerScore))
				.append(makeLegend(evalQuestions[3]))
				.append($textbox)
				.append($validationError)
				.append($submitButton);
		};


		/**** create dialog ****/

		var $close = $('<div></div>')
			.text(resources.getString("JsNavigationClose"))
			.css(closeStyles)
			.css(buttonNormalStyles)
			.click(function () {
				closeDialog();
				return false;
			})
			.mouseenter(buttonEnter)
			.mouseleave(buttonLeave);

		var $cancel = $('<div></div>')
			.text(resources.getString("JsNavigationCancel"))
			.css(closeStyles)
			.css(cancelStyles)
			.css(buttonNormalStyles)
			.click(function () {
				closeDialog();
				return false;
			})
			.mouseenter(buttonEnter)
			.mouseleave(buttonLeave);

		var $title = $('<div></div>')
			.css(titleStyles)
			.append( (title.length > 0) ? title : String.format(resources.getString("JsEventEvalEvaluateThis"), evalQuestions[0]));

		var $validationError = $('<div></div>')
			.css(validaitonErrorStyles)
			.text(resources.getString("JsEventEvalRequiredFields"));

		var $form = $('<div></div>')
			.addClass(evalFormClass)
			.css(formStyles)
			.data('evalUrl', evalUrl);

		var $initializing = $('<div></div>')
			.css(initializingStyles)
			.html(resources.getString("JsEventEvalInitializing"));

		var $error = $('<div></div>')
			.addClass('alert')
			.css(errorStyles)
			.text(resources.getString("JsEventEvalError"));

		var $sorry = $('<div></div>')
			.addClass('alert')
			.css(errorStyles)
			.text(String.format(resources.getString("JsEventEvalCannotEvaluateTime"), evalQuestions[0]));

		var $sorryHeadline = $('<h2></h2>')
			.css(sorryHeadlineStyles)
			.text(String.format(resources.getString("JsEventEvalCannotEvaluatePermission"), evalQuestions[0]));

		var $notUser = $('<div></div>')
			.addClass('alert')
			.css(errorStyles)
			.append($sorryHeadline.clone())
			.append(String.format(resources.getString("JsEventEvalSignInMicrosoftAccount"), signInOutLink(), evalQuestions[0]));

		var $eveyoneMustSignIn = $('<div></div>')
			.addClass('alert')
			.css(errorStyles)
			.append($sorryHeadline.clone())
			.append(String.format(resources.getString("JsEventEvalEveryoneSignIn"), signInOutLink()));

		var $mustSignIn = $('<div></div>')
			.addClass('alert')
			.css(errorStyles)
			.append($sorryHeadline.clone())
			.append(String.format(resources.getString("JsEventEvalSignIn"), signInOutLink()));

		var $uploading = $('<div></div>')
			.css(uploadingStyles)
			.html(resources.getString("JsEventEvalSaving"));

		var $finished = $('<div></div>')
			.css(finishedStyles)
			.addClass('finished')
			.text(String.format(resources.getString("JsEventEvalThankYou"), evalQuestions[0]))
			.append($close);

		var $dialog = $('<div></div>')
			.css(dialogStyles)
			.append($initializing)
			.append($form)
			.append($error)
			.append($uploading)
			.append($finished)
			.append($sorry)
			.append($notUser)
			.append($eveyoneMustSignIn)
			.append($mustSignIn);

		/* container that holds everything*/
		var $dialogHolder = $('<div></div>')
			.addClass('evalHolder')
			.append($cancel)
			.append($title)
			.append($dialog)
			.css(dialogHolderStyles)
			.appendTo('body');

		/* semi-transparent background */
		var $background = $('<div></div>')
			.css(backgroundStyles)
			.append()
			.click(function () {
				closeDialog();
			})
			.appendTo('body');
		
		/* init the eval */
		if(sessionCanEvaluate && userCanEvaluate && isSignedIn && (evalQuestions.length >= numOfQuestions + 1)){
		//really, if userCanEvaluate then sessionCanEvaluate and isSignedIn must be true
			$.ajax({
				url: evalUrl,
				dataType: 'json',
				cache: false,
				success: function(json){
					if(json){
						try {
							var modifiedDate = helpers.getDateFromJson(json.Modified);
							json.localizedModified = helpers.getLocalizedDate(modifiedDate);
						} catch (e) {
							json.localizedModified = null;
						}

						try{
							if(json.OverallScore){
								json.OverallScore = json.OverallScore * 1;
							}else{
								json.OverallScore = 0;
							}

							if(json.SpeakerScore){
								json.SpeakerScore = json.SpeakerScore * 1;
							}else{
								json.SpeakerScore = 0;
							}
						}catch(e){
							showError();
						}

						buildForm(json);
						showForm();

					}else{
						showError();
					}			
				},
				error: function () {
					showError();
				},
				timeout: 30000
			});
		}else if(sessionCanEvaluate && !attendeesonlyCanEvaluate && !isSignedIn) {
			showEveyoneMustSignIn();
		}else if(sessionCanEvaluate && !isSignedIn) {
			showMustSignIn();
		}else if(sessionCanEvaluate && !userCanEvaluate) {
			showNotUser();
		}else if(!sessionCanEvaluate) {
			showSorry();
		}else{		// will be triggered for evalQuestions.length of the wrong length
			showError();
		}
	};

	/* calling page events */
	$(document).on('click', 'a.evaluate', function (event) {
		var $this = $(this);
		var href = $(this).attr('href');
		var queryIndex =  href.indexOf('?');
		if(queryIndex){
			href = href.substring(0, href.indexOf('?'));
		}

		showEvaluateSession(evalAPI(href), $this.text(), true, $this.data('canevaluate'), $('body').hasClass('signedIn'), $this.data('evaltype'), $this.data('attendeesonly'));

		return false;
	});

	if(document.location.search && document.location.search.toLowerCase().indexOf(autoPopTrigger.toLowerCase()) > 0){
		//this should only be triggered by pages that can be evaluated by passing the pathname into evalAPI()
		$(function() {
			showEvaluateSession(evalAPI(document.location.pathname), '', $('body').data('evalsession'), $('body').data('evaluser'), $('body').hasClass('signedIn'), $('body').data('evaltype'), $this.data('attendeesonly'));
		});
	}

})(jQuery);