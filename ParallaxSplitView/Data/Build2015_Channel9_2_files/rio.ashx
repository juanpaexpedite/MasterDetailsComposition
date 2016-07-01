
function RioTrackingManager2(){this.guestCellCode='200695139';this.crcc='200695139';this.crid='-1';this.eac='-1';this.uniqueActionTagCode='null';this.uniqueActionTagId='-1';this.action='null';this.wid=-1;this.vn=-1;this.fn=-1;this.fbid="null";this.isExtendedDataTag=true;this.isImpressionAction=false;this.isClickAction=false;this.testMode=false;this.fireCrsImpressionTags=true;this.isChat=false;this.isProactiveChat=false;this.isUatid=false;this.isWtCookieSet=false;this.cellCodeCookieKey="RioTracking.CellCode.ClientSide";this.endActionCodeCookieKey="RioTracking.EndActionCode.ClientSide";this.permIdCookieKey="RioTracking.PermId.ClientSide";this.finalTagUatCookieKey="RioTracking.CompletionUatc";this.wtCookieKey="R";this.referringFbidCookieKey="RioTracking.ReferringFbid";this.meteorRedirectCookieKey="RioTracking.MeteorRedirect";this.baseAtlasUrlNotExtended=document.location.protocol+"//view.atdmt.com/jaction/{0}";this.baseAtlasUrl=document.location.protocol+"//view.atdmt.com/jaction/{0}/v3/ato.-1/atc1.{1}/atc2.{2}/atc3.{3}";this.baseOnvAtlasUrl=document.location.protocol+"//view.atdmt.com/jaction/mrtity_RIOOnNetworkVideo_1/v3/ato.-1/atc1.{0}/atc2.{1}/atc3.{2}/atc4.{3}";this.handlerRoot=document.location.protocol+"//www.microsoft.com/click/services/Tracking/";this.baseCrsImpressionUrl=this.handlerRoot+"Impression.ashx";this.baseCrsClickUrl=this.handlerRoot+"Click.ashx";this.baseOnvEventUrl=this.handlerRoot+"RichMedia.ashx?pid=1&cc={0}&eid={1}&vid=3&eac={2}&uid={3}";this.baseMeteorReferralUrl=this.handlerRoot+"Referral.ashx?cc={0}&rfbid={1}&rappid={2}";this.baseUniversalActionTagUrl=document.location.protocol+"//ad.atdmt.com/m/a.js;m={0};cache={1}?permid={2}&cellcode={3}&endactioncode={4}";this.baseUniversalActionTagUrlWithAction=document.location.protocol+"//ad.atdmt.com/m/a.js;m={0};cache={1}?event={2}&permid={3}&cellcode={4}&endactioncode={5}";}
RioTrackingManager2.prototype.init=function(){this.fbid=this.queryStringParamSafeValue("fbid",true,"null")
this.setCellCode();this.setUserId();this.wid=escape(this.queryStringParamSafeValue("wid",true,this.wid));this.vn=escape(this.queryStringParamSafeValue("vn",true,this.vn));this.fn=escape(this.queryStringParamSafeValue("fn",true,this.fn));this.fireImpressionTags();}
RioTrackingManager2.prototype.setCellCode=function(){if(this.crcc==this.guestCellCode){var currentCc=this.getCookie(this.cellCodeCookieKey);if((currentCc!=null)&&(currentCc.toString().length>0)&&(currentCc.split('.')[1]=='1')){this.crcc=parseInt(currentCc.split('.')[0]);}}
var referringFbid=this.getCookie(this.referringFbidCookieKey);if((referringFbid!=null&&referringFbid.toString().length!=0)&&this.fbid!=referringFbid.toString()&&this.guestCellCode!=-1){this.crcc=this.guestCellCode;}
document.cookie=this.cellCodeCookieKey+"="+this.crcc+"."+((this.crcc==this.guestCellCode)?'0':'1');}
RioTrackingManager2.prototype.setUserId=function(){if(this.crid==-1){var currentId=this.getCookie(this.permIdCookieKey);if((currentId!=null)&&(currentId.toString().length>0)){this.crid=currentId;}}
document.cookie=this.permIdCookieKey+"="+this.crid;}
RioTrackingManager2.prototype.fireImpressionTags=function(){if(this.uniqueActionTagCode!="null"){this.fireAtlasImpressionTag();}
if(this.uniqueActionTagId!="-1"&&this.action!="null"){this.isImpressionAction=true;this.fireAtlasUniversalImpressionTag();}
else if(this.uniqueActionTagId!="-1"&&this.action=="null"){this.fireAtlasUniversalImpressionTag();}
if((this.crcc!="-1")&&(this.fireCrsImpressionTags)){this.fireCrsImpressionTag();}}
RioTrackingManager2.prototype.click=function(endActionCode,uatcOrUatid,action){uatcOrUatid=(uatcOrUatid==null)?null:uatcOrUatid.replace(/^\s+|\s+$/gm,'');if(uatcOrUatid!=null&&uatcOrUatid.length!=0){var isUatc=isNaN(uatcOrUatid);if(isUatc==true){this.fireAtlasClickTag(uatcOrUatid,endActionCode);}
if(isUatc==false&&action==undefined){this.isUatid=true;this.fireAtlasUniversalClickTag(endActionCode,uatcOrUatid,action);}
else if(isUatc==false&&action!="null"){this.isClickAction=true;this.isUatid=true;this.fireAtlasUniversalClickTag(endActionCode,uatcOrUatid,action);}}
this.fireCrsClickTag(endActionCode,uatcOrUatid);var date=new Date();var curDate=null
do{curDate=new Date();}
while(curDate-date<500);return true;}
RioTrackingManager2.prototype.clickChatDirect=function(endActionCode,uatcOrUatid,action){this.isChat=true;this.click(endActionCode,uatcOrUatid,action);return true;}
RioTrackingManager2.prototype.clickChatProactive=function(endActionCode,uatcOrUatid,action){this.isChat=true;this.isProactiveChat=true;this.click(endActionCode,uatcOrUatid,action);return true;}
RioTrackingManager2.prototype.atlasTrack=function(atlasTagCode){atlasUrl=this.baseAtlasUrlNotExtended.format(atlasTagCode);this.fireTag(atlasUrl,true);var date=new Date();var curDate=null
do{curDate=new Date();}
while(curDate-date<500);return true;}
RioTrackingManager2.prototype.clickNoWait=function(endActionCode,atlasTagCode){if(atlasTagCode!=null){this.fireAtlasClickTag(atlasTagCode,endActionCode);}
this.isUatid=false;this.fireCrsClickTag(endActionCode);}
RioTrackingManager2.prototype.trackVideoEvent=function(endActionCode,eventId){var atlasUrl=this.baseOnvAtlasUrl.format(this.crid,this.crcc,endActionCode,eventId);var crsUrl=this.baseOnvEventUrl.format(this.crcc,eventId.toString(),endActionCode.toString(),this.crid);var timestamp=new Date();crsUrl+="&rnd="+Math.ceil(Math.random()*99999999)+""+timestamp.getUTCFullYear()+timestamp.getUTCMonth()+timestamp.getUTCDate()+timestamp.getUTCHours()+timestamp.getUTCMinutes()+timestamp.getUTCSeconds()+timestamp.getUTCMilliseconds();crsUrl+="&r="+escape(document.referrer);this.fireTag(atlasUrl,false);this.fireTag(crsUrl,true);}
RioTrackingManager2.prototype.trackMeteorReferral=function(appId,referringFbid){this.crcc=escape(this.queryStringParamSafeValue("CR_CC",true,this.guestCellCode));var meteorReferralUrl=this.baseMeteorReferralUrl.format(this.crcc,referringFbid,appId);this.fireTag(meteorReferralUrl,false);}
RioTrackingManager2.prototype.fireAtlasImpressionTag=function(){var atlasUrl;if(this.isExtendedDataTag){atlasUrl=this.baseAtlasUrl.format(this.uniqueActionTagCode,this.crid,this.crcc,this.eac);}
else{atlasUrl=this.baseAtlasUrlNotExtended.format(this.uniqueActionTagCode);}
this.fireTag(atlasUrl,false);}
RioTrackingManager2.prototype.fireAtlasUniversalImpressionTag=function(){var atlasUrl;if(this.isImpressionAction){atlasUrl=this.baseUniversalActionTagUrlWithAction.format(this.uniqueActionTagId,Math.ceil(Math.random()*99999999),this.action,this.crid,this.crcc,this.eac);}
else{atlasUrl=this.baseUniversalActionTagUrl.format(this.uniqueActionTagId,Math.ceil(Math.random()*99999999),this.crid,this.crcc,this.eac);}
this.fireTag(atlasUrl,false);}
RioTrackingManager2.prototype.fireCrsImpressionTag=function(){var crsUrl=this.baseCrsImpressionUrl+"?CR_CC="+this.crcc;if((this.vn!=-1)&&(this.fn!=-1)){crsUrl+="&vn="+this.vn.toString()+"&fn="+this.fn.toString();}
else if(this.wid!=-1){crsUrl+="&wid="+this.wid.toString();}
else if(!this.isWtCookieSet){this.setWtCookie();}
if(this.crid!=-1){crsUrl+="&CR_ID="+this.crid.toString();}
if(this.eac!=-1){crsUrl+="&CR_EAC="+this.eac.toString();}
if(this.testMode){crsUrl+="&test=1";}
if(this.crcc==this.guestCellCode){crsUrl+="&o=1";}
else{crsUrl+="&o=0";}
if((this.fbid!="null")&&(typeof(meteorAppId)!='undefined'&&meteorAppId.length!=0)){var referringFbid=this.getCookie(this.referringFbidCookieKey);if((referringFbid!=null&&referringFbid.toString().length!=0)&&(this.fbid!=referringFbid.toString())){crsUrl+="&s=1";crsUrl+="&rfbid="+referringFbid.toString();crsUrl+="&rappid="+meteorAppId.toString();var referringCellCode=escape(this.queryStringParamSafeValue("CR_CC",true,this.guestCellCode));crsUrl+="&rcc="+referringCellCode;}
if(this.fbid!=referringFbid.toString()){crsUrl+="&xa="+escape("<EUA><A1>"+meteorAppId.toString()+"</A1><A2>"+this.fbid.toString()+"</A2><T>1</T></EUA>");}}
crsUrl+="&r="+escape(document.referrer);this.fireTag(crsUrl,false);}
RioTrackingManager2.prototype.fireAtlasClickTag=function(atlasUatc,endActionCode){var atlasUrl=this.baseAtlasUrl.format(atlasUatc,this.crid,this.crcc,endActionCode);this.fireTag(atlasUrl,true);}
RioTrackingManager2.prototype.fireAtlasUniversalClickTag=function(endActionCode,atalsUatId,action){if(this.isClickAction){atlasUrl=this.baseUniversalActionTagUrlWithAction.format(atalsUatId,Math.ceil(Math.random()*99999999),action,this.crid,this.crcc,endActionCode);}
else{atlasUrl=this.baseUniversalActionTagUrl.format(atalsUatId,Math.ceil(Math.random()*99999999),this.crid,this.crcc,endActionCode);}
this.fireTag(atlasUrl,false);}
RioTrackingManager2.prototype.fireCrsClickTag=function(endActionCode,uatid){document.cookie=this.endActionCodeCookieKey+"="+endActionCode.toString();var crsUrl=this.baseCrsClickUrl;crsUrl+="?CR_EAC="+endActionCode.toString();if(this.testMode){crsUrl+="&test=1";}
if(this.isChat){crsUrl+="&c=1";}
if(this.isProactiveChat){crsUrl+="&cp=1";}
if((this.isUatid)&&(uatid!=null)){crsUrl+="&uatid="+uatid;}
crsUrl+="&r="+escape(document.referrer);var timestamp=new Date();crsUrl+="&rnd="+Math.ceil(Math.random()*99999999)+""+timestamp.getUTCFullYear()+timestamp.getUTCMonth()+timestamp.getUTCDate()+timestamp.getUTCHours()+timestamp.getUTCMinutes()+timestamp.getUTCSeconds()+timestamp.getUTCMilliseconds();this.fireTag(crsUrl,true);}
function wt_GetCurrentCellCode(){var cookie=RioTracking.getCookie(RioTracking.wtCookieKey);var currentCellCode=escape(RioTracking.queryStringParamSafeValue("CR_CC",true,RioTracking.guestCellCode));if((this.uniqueActionTagCode=="null")||(currentCellCode==-1)||(this.wid!=-1)||((this.vn!=-1)&&(this.fn!=-1))){return cookie;}
return RioTracking.setWtCookie();}
RioTrackingManager2.prototype.setWtCookie=function(){var cookieVal="";try{var rCookie=this.getCookie(this.wtCookieKey);var cookies;if(rCookie!=''){cookies=this.getCookie(this.wtCookieKey).split("|");}
else{cookies=new Array();}
var currentCellCode=escape(RioTracking.queryStringParamSafeValue("CR_CC",true,RioTracking.guestCellCode));var indexOfCellCode=this.ArrayIndexOf(cookies,currentCellCode);if(indexOfCellCode>-1){cookies.splice(indexOfCellCode,1);}
var newCellCodeVal=currentCellCode+"-"+RioTracking.getCurrentDateTime();cookies=this.ArrayAdd(cookies,0,newCellCodeVal);var newCookie=((cookies.length>1?this.ArrayJoin(cookies,3,"|"):cookies));var exdate=new Date();exdate.setFullYear(exdate.getFullYear()+1);cookieVal=this.wtCookieKey+"="+newCookie+";expires="+exdate.toGMTString()+";domain=microsoft.com;path=/";document.cookie=cookieVal;this.isWtCookieSet=true;}
catch(e){cookieVal="";}
return cookieVal;}
RioTrackingManager2.prototype.AddHandler=function(object,eventName,handler){if(object.addEventListener){object.addEventListener(eventName,handler,false);}
else{object.attachEvent("on"+eventName,handler);}}
RioTrackingManager2.prototype.CreateDelegate=function(object,method){return(function(){return method.apply(object,arguments);})}
RioTrackingManager2.prototype.fireTag=function(tag,appendRandom){var scriptObj=document.createElement("script");scriptObj.type="text/javascript";scriptObj.src=tag;document.getElementsByTagName("head")[0].appendChild(scriptObj);}
RioTrackingManager2.prototype.queryStringParamSafeValue=function(name,caseInsensitive,defaultValueIfNull){name=name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");var r="[?&]"+name+"=([^&#]*)";var rmod=""
if(caseInsensitive)rmod+="i";var s=new RegExp(r,rmod);var res=s.exec(window.location.href.toString());if(!res&&defaultValueIfNull)return defaultValueIfNull;else if(!res)return null;else return res[1];}
RioTrackingManager2.prototype.getCookie=function(check_name){var a_all_cookies=document.cookie.split(';');var a_temp_cookie='';var cookie_name='';var cookie_value='';var b_cookie_found=false;for(i=0;i<a_all_cookies.length;i++){a_temp_cookie=a_all_cookies[i].split('=');cookie_name=a_temp_cookie[0].replace(/^\s+|\s+$/g,'');if(cookie_name==check_name){b_cookie_found=true;if(a_temp_cookie.length>1){cookie_value=unescape(a_temp_cookie[1].replace(/^\s+|\s+$/g,''));}
return cookie_value;break;}
a_temp_cookie=null;cookie_name='';}
if(!b_cookie_found){return'';}}
String.prototype.format=function(){var pattern=/\{\d+\}/g;var args=arguments;return this.replace(pattern,function(capture){return args[capture.match(/\d+/)];});}
RioTrackingManager2.prototype.ArrayIndexOf=function(array,obj){var i=array.length;while(i--){if(array[i].match(obj+"-")){return i;}}
return-1;}
RioTrackingManager2.prototype.ArrayJoin=function(array,count,delimiter){var _result="";var _maxCellCodes=(count>array.length?array.length:count);for(_index=0;_index<_maxCellCodes;_index++){_result+=array[_index]+(_index<_maxCellCodes-1?delimiter:"");}
return _result;}
RioTrackingManager2.prototype.ArrayAdd=function(array,addIndex,obj){for(_index=array.length;_index>addIndex;_index--){array[_index]=array[_index-1];}
array[addIndex]=obj;return array;}
RioTrackingManager2.prototype.getCurrentDateTime=function(){var currentTime=new Date();var month=currentTime.getMonth()+1;var day=currentTime.getDate();var year=currentTime.getFullYear();var hours=currentTime.getHours();var minutes=currentTime.getMinutes();var seconds=currentTime.getSeconds();if(minutes<10){minutes="0"+minutes;}
if(seconds<10){seconds="0"+seconds;}
return month+"/"+day+"/"+year+" "+hours+":"+minutes+":"+seconds;}
if(typeof(meteorAppId)!='undefined'&&meteorAppId.length!=0){var meteorRedirectCookie=RioTracking.getCookie(RioTracking.meteorRedirectCookieKey);var fbid=RioTracking.queryStringParamSafeValue("fbid",true,-1).toString();if((meteorRedirectCookie==null)||(meteorRedirectCookie.toString().length==0)){if(fbid!=-1){document.cookie=RioTracking.referringFbidCookieKey+"="+RioTracking.queryStringParamSafeValue("fbid",true,-1).toString();document.cookie=RioTracking.meteorRedirectCookieKey+"=1";}
else{document.cookie=RioTracking.meteorRedirectCookieKey+"=0";}}
var meteorScriptObj=document.createElement("script");meteorScriptObj.type="text/javascript";meteorScriptObj.src="http://static.meteorsolutions.com/metsol.js";meteorScriptObj.onreadystatechange=function(){if((this.readyState=='complete')||(this.readyState=='loaded')){var meteorScriptObj2=document.createElement("script");meteorScriptObj2.type="text/javascript";meteorScriptObj2.text="meteor.tracking.track(meteorAppId, {'query_string_tag_key': 'CR_CC','remove_tag_key':false});";document.getElementsByTagName("head")[0].appendChild(meteorScriptObj2);}}
meteorScriptObj.onload=function(){var meteorScriptObj2=document.createElement("script");meteorScriptObj2.type="text/javascript";meteorScriptObj2.text="meteor.tracking.track(meteorAppId, {'query_string_tag_key': 'CR_CC','remove_tag_key':false});";document.getElementsByTagName("head")[0].appendChild(meteorScriptObj2);}
document.getElementsByTagName("head")[0].appendChild(meteorScriptObj);}
var RioTracking=new RioTrackingManager2();RioTracking.init();