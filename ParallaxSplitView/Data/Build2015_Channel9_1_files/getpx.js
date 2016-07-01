var k_src='', k_med='', k_term='', k_ad='',k_name='';

function kVoid() { return; }
					
function kenshoo_nconv(params,subdomain) {
	   var hostProtocol = (("https:" == document.location.protocol) ? "https" : "http");
	   var url = hostProtocol+'://'+subdomain+'.xg4ken.com/media/redir.php?track=1';

	   if (params != null){
		   for (var x=0; x<params.length; x++){
				url = url + '&' + params[x];
		   }
	   }

	   url = url + '&ref=' + document.referrer;
       	   var a = new Image(1,1);
           a.src = url;

	   a.onload = function() { kVoid(); }
}

function getRandomNumber(range){
	return Math.floor(Math.random() * range);
}

function getRandomChar(){
	var chars = '0123456789abcdefghijklmnopqurstuvwxyz';
	return chars.substr( getRandomNumber(37), 1 );
}

function randomID(size){
	var str = '';
	for(var i = 0; i < size; i++){
		str += getRandomChar();
	}
	return str;
}

function gup( name, lf ){
 name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
 var regexS = "[\\?&]"+name+"=([^&#]*)";
 var regex = new RegExp( regexS );
 var results = regex.exec( lf );
 if( results == null )    return "";
 else    return results[1];
}



function getcookie(c_name){
  var cnamelookup = c_name+'=';
  var c_start=document.cookie.indexOf(cnamelookup);
	var val='';
  if (c_start!=-1){
	   c_start=c_start + c_name.length+1;
     c_end=document.cookie.indexOf(';',c_start-1);
     if (c_end==-1) c_end=document.cookie.length;
		 val = unescape(document.cookie.substring(c_start,c_end));
		 if((c_start-1)==(c_end)) val='';//just ;
	}
	return val;
}


function setcookie(name, val, expires){
	document.cookie = name + '=' + escape(val) + '; expires=' + expires.toUTCString() + '; path=/;';
}


function track(cid){
var parmed = gup('kmed',window.location.href);
if(parmed != null && parmed.length > 0){
	var date1 = new Date();
	date1.setTime(date1.getTime()+(365*24*60*60*1000));
	k_med=parmed;
  if(parmed == 'ppc'){
			setcookie('kmed','ppc', date1);
	}else if(parmed=='display'){
	  setcookie('kmed','display', date1);
  } //end parmed==display
}else if( getcookie('kmed')=='ppc' ){
		k_med='ppc';
}//end cookie ppc check
}

function k_trackevent(params,subdomain){
	 kenshoo_nconv(params,subdomain);
}
function k_trackeventencode(params,subdomain) {
         if (params != null){
                   for (var i=0; i<params.length; i++){
                                var param = params[i];
                                if (param.indexOf("product") != -1)
                                        params[i] = encode_param(param,"product");
                                if (param.indexOf("kw") != -1)
                                        params[i]=encode_param(param,"kw");
                   }
        }
		kenshoo_nconv(params,subdomain);
}

function encode_param(unencoded_param,param_name) {
        var param = param_name + "=";
        var val_part = unencoded_param.substring(param_name.length+1,unencoded_param.length);
        param += encodeURIComponent(val_part);
        return param;
}

function k_fp_click(subdomain) {

	var k_3rdparty_cookie = getcookie("k_3rdparty_cookie");

	if (k_3rdparty_cookie == null || k_3rdparty_cookie == "") {
		// Create a new one
		k_3rdparty_cookie = guidGenerator();
	}

	// Update expiration
	var d = new Date();
	var expiresAfter = 3600 * 1000 * 24 * 365; //365 days
	d = new Date(d.getTime() + expiresAfter);

	setcookie("k_3rdparty_cookie", k_3rdparty_cookie, d);

	var prof = getParameterByName("prof");
	var camp = getParameterByName("camp");
	var affcode = getParameterByName("affcode");
 	if (isValidParams(prof,camp,affcode)){
		var params = new Array();
   		params[0]="prof=" + prof;
   		params[1]="camp=" + camp;
   		params[2]="affcode=" + affcode;
   		params[3]='k_3rdparty_cookie=' + k_3rdparty_cookie;
	
		kenshoo_nconv(params,subdomain);
	}
}

function isValidParams(prof,camp,affcode){
	var blnValid = 
		((prof  != null && prof  != 'undefined' && prof  !=0) &&
			(camp != null && camp !='undefined') &&
			(affcode != null && affcode !='undefined'));
	return blnValid;
			
}

function k_fp_conv(params,subDomain) {
	var k_3rdparty_cookie = getcookie("k_3rdparty_cookie");
	params[params.length-1]='k_3rdparty_cookie=' + k_3rdparty_cookie;
	k_trackevent(params, subDomain);
				
}


function getParameterByName(name) {

	name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");

	var regexS = "[\\?&]" + name + "=([^&#]*)";

	var regex = new RegExp(regexS);

	var results = regex.exec(window.location.search);

	if(results == null)
		return null;
	else
		return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function guidGenerator() {
    var S4 = function() {
       return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
    };
    return (S4()+S4()+"-"+S4()+"-"+S4()+"-"+S4()+"-"+S4()+S4()+S4());
}