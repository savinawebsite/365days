<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="365Days.aspx.cs" Inherits="_365Days._365Days" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <link rel="stylesheet" type="text/css" href="../Css/custom.css" />
	  <link rel="stylesheet" type="text/css" href="../Css/main.css" />
      <script  type="text/javascript" src="../Scripts/custom.js"></script>
      <link rel="stylesheet" type="text/css" href="../css/login.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<script src="https://webfonts.creativecloud.com/lekton:n4:all.js" type="text/javascript"></script>
<script src="https://use.typekit.net/ik/8p4FeSXY7YyOi2lp4fV-ZrFdbGi86i57K2w9JAueCBMfenMgfJAV9yJGwQwXFQbUwuvXjhZRF2IkFAJXjDwt526hF29hZRwDjDbkjDJ3jR9tjsGkHKoydcsGjAlyjW4yOcFzdPUydcsGjAlyjW4yOcFzdPUaiaS0jAoq-eBnjABkjPoRdhXKBe80ZYmDiY4oOA80jkuziWsC-Ao8Jy41dasG-Awldag8dKuD-eBqZAb7f6R-UbIbMg6IJMI7fbKvdsMgeMb6M9GIQWmDZZMgq_OnD39.js" type="text/javascript"></script>
	  
<script type="text/javascript">
   try {Typekit.load();} catch(e) {}
</script>

	  <script type="text/javascript">

	      function checkPassword(password) {
	          var pattern = /^[a-zA-Z0-9_-]{6,20}$/;
	          if (pattern.test(password)) {
	              return true;
	          } else {
	              return false;
	          }
	      }

	      function hasWhiteSpace(s) {
	          return /\s/g.test(s);
	      }
         

	      $(document).ready(function () {



	          $(window).scroll(function () {
	              if ($(this).scrollTop() > 50) {
	                  $('#back-to-top').fadeIn();
	              } else {
	                  $('#back-to-top').fadeOut();
	              }
	          });

	          // scroll body to 0px on click
	          $('#back-to-top').click(function () {
	              $('#back-to-top').tooltip('hide');
	              $('body,html').animate({
	                  scrollTop: 0
	              }, 2000);
	              return false;
	          });

	          $('#back-to-top').tooltip('show');


		    $('.zeroDayIframe').load(function(){
			var height = $('.zeroDayIframe').contents().height();
        		$('.postImages_mobile').height(height + 10);
                   });

            
              var loginStatus  = '<%= Session["CustomerId"] %>';
 
              $('#btnLoginForm').click(function () {
                  $('.updateprofile').hide();
                  $('.CustInfoUpdate').hide();
                  $('.passconfirm').hide();
                  $('.forgotPwd1').hide();
                  $('.email').show();
                  $('.email').val('');
                  $('.pass').val('');
                  $('.firstname').val('');
                  $('.lastname').val('');
                  $('.passconfirm').val('');
                  $('.pass').attr("placeholder", "Password");
                  $('.pass').show();
                  $('.forgotPwd').show();
                  $('#btnLogin').attr('value', 'Sign In');
                
                  if (loginStatus != null && loginStatus != '' && loginStatus != 'undefined') {
                      if (loginStatus == '0') {
                          $('#login-modal').modal();
                      } else {
                          var xmlhttp;
                          if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                              xmlhttp = new XMLHttpRequest();
                          }
                          else {// code for IE6, IE5
                              xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                          }
                          xmlhttp.onreadystatechange = function () {
                              if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                                  if (xmlhttp.responseText != "") {
                                      //logout success
                                      loginStatus = '0';
                                      $('#btnLoginForm').html('Sign In');
                                      location.reload();
                                   
                                  }
                              }
                          }
                          xmlhttp.open("GET", "../Ajax.aspx?action=Logout", true);
                          xmlhttp.send();
                      }
                  }
              });

              $('#aforgotPwd').click(function () {
                  $('.forgotPwd1').show(500);
                  $('.pass').hide();
                  $('.forgotPwd').hide();
                  $('#btnLogin').attr('value', 'Request Password Now');
              });

              $("#btnLogin").click(function (event) {
                  var action = $('#btnLogin').val();
                  if (action == 'Save') {
                      //update customer info
                      var email = $('.email').val()
                      var pass = $(".pass").val()
                      var passconfirm = $('.passconfirm').val();
                      var firstname = $('.firstname').val();
                      var lastname = $('.lastname').val();
                      if (email == "" || pass == "" || passconfirm == "" || firstname == "" || lastname == "") {
                          $(".modal-body").html("Please enter fill your infomation");
                          $("#alertDialog").modal();
                          return;
                      } else {
                          if (pass.length < 6) {
                              $(".modal-body").html("Password must be of minimum 6 characters");
                              $("#alertDialog").modal();
                              return;
                          } else {
                              if (pass.length > 20) {
                                  $(".modal-body").html("Password allow be of maximum 20 characters");
                                  $("#alertDialog").modal();
                                  return;
                              } else {

                                  if (hasWhiteSpace(pass)) {
                                      $(".modal-body").html("Password can not contain whitespace character");
                                      $("#alertDialog").modal();
                                      return;
                                  } else {
                                      if (pass !== passconfirm) {
                                          $(".modal-body").html("Password does not match the confirm password");
                                          $("#alertDialog").modal();
                                          return;
                                      } else {
                                          //process insert customer info
                                          var xmlhttp;
                                          if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                                              xmlhttp = new XMLHttpRequest();
                                          }
                                          else {// code for IE6, IE5
                                              xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                                          }
                                          xmlhttp.onreadystatechange = function () {
                                              if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                                                  if (xmlhttp.responseText != "") {
                                                      if (xmlhttp.responseText == "1") {
                                                          //update success
                                                          $('#btnLoginForm').html('Sign Out');
                                                          location.reload();
                                                      }
                                                  }
                                              }
                                          }
                                          var safeemail = escape(email);
                                          var safepass = escape(pass);
                                          var safefristname = escape(firstname);
                                          var safelastname = escape(lastname);
                                          xmlhttp.open("GET", "../Ajax.aspx?action=Update&email=" + safeemail + "&pass=" + safepass + "&firstname=" + safefristname + "&lastname=" + safelastname + "", true);
                                          xmlhttp.send();
                                      }
                                  }

                              }
                          }
                      }
                  } else {

                      if (action == 'Sign In') {
                          //process account and login

                          var email = $('.email').val()
                          var pass = $(".pass").val()
                          if (email == "" || pass == "") {
                              $(".modal-body").html("Please enter your email and password");
                              $("#alertDialog").modal();
                              return;
                          } else {
                              //process login
                              var xmlhttp;
                              if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                                  xmlhttp = new XMLHttpRequest();
                              }
                              else {// code for IE6, IE5
                                  xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                              }
                              xmlhttp.onreadystatechange = function () {
                                  if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                                      if (xmlhttp.responseText != "") {

                                          if (xmlhttp.responseText == "0") {
                                              $(".modal-body").html("Account is not exist");
                                              $("#alertDialog").modal();
                                              return;
                                          }
                                          if (xmlhttp.responseText == "1") {
                                              //login success
                                              $('#btnLoginForm').html('Sign Out');
                                              location.reload();
                                          }
                                          if (xmlhttp.responseText == "2") {
                                              $(".modal-body").html("Password incorrect");
                                              $("#alertDialog").modal();
                                              return;
                                          }
                                          if (xmlhttp.responseText == "3") {
                                              //update new password, info
                                              $('.updateprofile').show(500);
                                              $('.CustInfoUpdate').show(500);
                                              $('.passconfirm').show(500)
                                              $('.email').hide();
                                              $('.pass').val('');
                                              $('.forgotPwd').hide();
                                              $('.pass').attr("placeholder", "New password");
                                              $('#btnLogin').attr('value', 'Save');
                                          }
                                      }
                                  }
                              }
                              var safeemail = escape(email);
                              var safepass = escape(pass);
                              xmlhttp.open("GET", "../Ajax.aspx?action=Login&email=" + safeemail + "&pass=" + safepass + "", true);
                              xmlhttp.send();
                          }
                      } else {
                          //request password now
                          var email = $('.email').val()
                          if (email == "") {
                              $(".modal-body").html("Please enter your email");
                              $("#alertDialog").modal();
                              return;
                          } else {
                         
                                  //process insert customer info
                                  var xmlhttp;
                                  if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                                      xmlhttp = new XMLHttpRequest();
                                  }
                                  else {// code for IE6, IE5
                                      xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                                  }
                                  xmlhttp.onreadystatechange = function () {
                                      if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                                          if (xmlhttp.responseText != "") {
                                              if (xmlhttp.responseText == "1") {
                                                  $('#login-modal').modal('hide');
                                                  $(".modal-body").html("Please check your email to receive your account password");
                                                  $("#alertDialog").modal();
                                                  return;
                                              }
                                              if (xmlhttp.responseText == "0") {
                                                  $(".modal-body").html("Email is not exist");
                                                  $("#alertDialog").modal();
                                                  return;
                                              }
                                          }
                                      }
                                  }
                                  var safeemail = escape(email);
                                  xmlhttp.open("GET", "../Ajax.aspx?action=RequestPassword&email=" + safeemail + "", true);
                                  xmlhttp.send();
                              
                          }
                      }
                  }
              });

              if (loginStatus != null && loginStatus != '' && loginStatus != 'undefined') {
                  if (loginStatus == '0') {
                      $('#btnLoginForm').html('Sign In');
                      $('#MainContent_parentBuyProduct').css("display", "none");
                      $('#pricingLink').css('display', 'none')
                      $('#useGuide').css('display', 'none')
					  $('.dvInfoNoPackage').css('display', 'none')
					  $('.mobileNoLoginPrice').css('display', 'block')
                  } else {
                      $('#btnLoginForm').html('Sign Out');
                      $('#MainContent_parentBuyProduct').css("display", "none");
                      var lsPurchaseNumber = '<%= Session["Level"] %>';
                      if (lsPurchaseNumber == '0') {
                          $('#pricingLink').css('display', 'block')
							$('.currentDayAndOpenDay').css('display', 'none')
							$('.dvInfoNoPackage').css('display', 'block')
                          $('#useGuide').css('display', 'none')
						  $('.mobileNoLoginPrice').css('display', 'none')
                      } else {
                          var day
                          switch (lsPurchaseNumber) {
                              case '1':
                                  day = 51
                                  break
                              case '2':
                                  day = 107
                                  break
                              case '3':
                                  day = 137
                                  break
                              case '4':
                                  day = 168
                                  break
                              case '5':
                                  day = 201
                                  break
                              case '6':
                                  day = 230
                                  break
                              case '7':
                                  day = 364
                                  break
                              case '8':
                                  day = 292
                                  break
                              case '9':
                                  day = 317
                                  break
                              case '10':
                                  day = 365
                                  break
                              case '11':
                                  day = 7
                                  break
                          }

                          $('#pricingLink').css('display', 'none')
						  $('.dvInfoNoPackage').css('display', 'none')
						  $('.mobileNoLoginPrice').css('display', 'none')
                          $('#useGuide').css('display', 'block')
                          $('#useGuide').html('<p>You can access your training from DAY 1 to DAY ' + day + '.</p>')
						  $('.mobileNoLoginPrice').css('display', 'none')
                      }

                  }
                }
             
              for (var lessionId = 1; lessionId <= 365; lessionId++) {
                  if ($('#' + lessionId).val() == 3) {
                      $('#' + lessionId).css("background", "#FF7272");
                  }
              }
          });
    </script>

  <style>
body {
    background: #7F7F7F;
}
.postImages_mobile {
	display: none;
}

</style>  


  <!-- Login Modal -->
        <div class="modal fade" id="login-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
    	  <div class="modal-dialog">
				<div class="loginmodal-container">
					<h1>ANDHAKAARA</h1><br>
                    <h4 hidden class="updateprofile">Please update your profile and change a new password for the next login            <br /></h4>

                    <h4 hidden class="forgotPwd1"> To receive your account password please enter your email address below. A confirmation email will be sent to this email address with  your account password. <br /></h4>

                    <h4 hidden class="forgotPwd1"> If you do not receive an email please check your junk folder and/or contact us at office@andhakaara.com<br /></h4>
         
                    <div hidden class="CustInfoUpdate">
                        <input type="text" id="txtfirstname" name="firstname" placeholder="First name" class="firstname">
                        <input type="text" id="txtlastname" name="lastname" placeholder="Last name" class="lastname">
                    </div>
					<input type="text" id="txtemail" name="email" placeholder="Email" class="email">
					<input type="password" id="txtpwd" name="pass" placeholder="Password" class="pass">
                    <input hidden type="password" id="txtpwdconfirm" name="passconfirm" placeholder="Password confirm" class="passconfirm">

				    <div class="forgotPwd">
					    <a id="aforgotPwd">Forgotten your password?</a>
				    </div>
                    
					<input type="button" id="btnLogin" name="signin" class="login loginmodal-submit" value="Sign In">
     
                    <!--	
				  <div class="login-help">
					<a href="#">Register</a> - <a href="#">Forgot Password</a>
				  </div>
                    -->
				</div>
			</div>
		  </div>
          <!--End Login Modal -->

                       
    <div class="clearfix" id="page"><!-- group -->
   <div class="clearfix grpelem" id="ppu29098"><!-- column -->
	<button type="button" id="btnLoginForm" class="btSignIn"></button>
		<div class="clearfix colelem" id="pu29098"><!-- group -->		 
			 <div class="clip_frame grpelem" id="u29100"><!-- image -->
				<a href="https://andhakaara.com"><img class="block" id="u29100_img" src="images/logo%20kreis%20total%20eclipse.png?crc=486896189" data-hidpi-src="images/logo%20kreis%20total%20eclipse_2x.png?crc=4058258319" alt="" width="102" height="96"/>
				</a>
			 </div>	 
			 <div class="size_fixed grpelem" id="u29163"><!-- custom html -->
				<span class="skilltechtypetext" style"text-align:center;>
					<span class="typed-cursor" style"text-align:center;>|</span>
				</span>
			 </div>
			 <div class="logoAndhakaara grpelem" id="u29259"><!-- image -->
				<a href="https://andhakaara.com"><img class="block" id="u29259_img" src="images/andhakaara%20logo%20neu3.jpg?crc=4089473405" data-hidpi-src="images/andhakaara%20logo%20neu3_2x.jpg?crc=3983588285" alt="" width="441" height="91"/>
				</a>
			 </div>
		</div>

		<div class="clearfix colelem"><!-- group -->       
			<div class="clearfix colelem" id="myIdMichaelDoNotTouch">
				<!-- <iframe  id="andhakaaraMuseIframe" src="https://andhakaara.myshopify.com/" marginwidth="0" marginheight="0" frameborder="no" scrolling="no"  style="border: 0px;"></iframe> -->
				<div class="loggedInUser" id="loggedInUser" runat="server" style="display:none;">
					<div class="userWelcom clearfix">
						<p class="userWelcomUsername" id="userWelcomUsername" runat="server"></p>
						<p class="userWelcomLevel" id="userWelcomLevel" runat="server"></p>
                        <!-- <p class="pricingLink" id="pricingLink">You haven't yet purchased any package</br> <a href="pricing.html">See Pricing Plan Of 365 Days</a></p> -->
				<div class="pricingLink" id="pricingLink">								
						<div class="titlePriceLevel1"><b>LEVEL 1 </b>(DAY 1-DAY 51) for </div><div class="priceLevel1"><b>USD 308.00</b></div>
						<!-- button BUY LEVEL-1 -->
								<div class="btBuyLevel1">
									<div id='product-component-1513242147295'></div>
									<script type="text/javascript">
									/*<![CDATA[*/
									  (function () {
										var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
										if (window.ShopifyBuy) {
										  if (window.ShopifyBuy.UI) {
											ShopifyBuyInit();
										  } else {
											loadScript();
										  }
										} else {
										  loadScript();
										}

									  function loadScript() {
										var script = document.createElement('script');
										script.async = true;
										script.src = scriptURL;
										(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
										script.onload = ShopifyBuyInit;
									  }

									  function ShopifyBuyInit() {
										var client = ShopifyBuy.buildClient({
										  domain: 'andhakaara.myshopify.com',
										  apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
										  appId: '6',
										});

										ShopifyBuy.UI.onReady(client).then(function (ui) {
										  ui.createComponent('product', {
											id: [8690440329],
											node: document.getElementById('product-component-1513242147295'),
											moneyFormat: '${{amount}}',
											options: {"product":{"buttonDestination":"checkout","variantId":"all","width":"240px","contents":{"img":false,"imgWithCarousel":false,"title":false,"variantTitle":false,"price":false,"description":false,"buttonWithQuantity":false,"quantity":false},"text":{"button":"BUY"},"styles":{"product":{"text-align":"left","@media (min-width: 601px)":{"max-width":"100%","margin-left":"0","margin-bottom":"50px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Vollkorn, serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","color":"#daa12b","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-size":"12px","font-family":"Gill Sans, sans-serif","font-weight":"normal","color":"#daa12b"}},"googleFonts":["Karla","Vollkorn"]},"cart":{"contents":{"button":true},"styles":{"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"footer":{"background-color":"#ffffff"}},"googleFonts":["Karla"]},"modalProduct":{"contents":{"img":false,"imgWithCarousel":true,"variantTitle":false,"buttonWithQuantity":true,"button":false,"quantity":false},"styles":{"product":{"@media (min-width: 601px)":{"max-width":"100%","margin-left":"0px","margin-bottom":"0px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Vollkorn, serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"}},"googleFonts":["Karla","Vollkorn"]},"toggle":{"styles":{"toggle":{"font-family":"Karla, sans-serif","background-color":"#d3c27c",":hover":{"background-color":"#beaf70"},":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"count":{"font-size":"17px","color":"#000000",":hover":{"color":"#000000"}},"iconPath":{"fill":"#000000"}},"googleFonts":["Karla"]},"option":{"styles":{"label":{"font-family":"Gill Sans, sans-serif"},"select":{"font-family":"Gill Sans, sans-serif"}}},"productSet":{"styles":{"products":{"@media (min-width: 601px)":{"margin-left":"-20px"}}}}},
										  });
										});
									  }
									})();
									/*]]>*/
									</script>	
								</div>
						<!-- end button BUY LEVEL-1 -->
						
						<div class="titlePriceLevelFull"><b>or all 365 DAYS </b> for </div> <div class="priceLevelFull"><b>USD 1008.00</b></div>
						<!-- button BUY LEVEL-Full -->
								<div class="btBuyLevelFull">
									<div id='product-component-1513246124469'></div>
									<script type="text/javascript">
									/*<![CDATA[*/
									  (function () {
										var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
										if (window.ShopifyBuy) {
										  if (window.ShopifyBuy.UI) {
											ShopifyBuyInit();
										  } else {
											loadScript();
										  }
										} else {
										  loadScript();
										}

									  function loadScript() {
										var script = document.createElement('script');
										script.async = true;
										script.src = scriptURL;
										(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
										script.onload = ShopifyBuyInit;
									  }

									  function ShopifyBuyInit() {
										var client = ShopifyBuy.buildClient({
										  domain: 'andhakaara.myshopify.com',
										  apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
										  appId: '6',
										});

										ShopifyBuy.UI.onReady(client).then(function (ui) {
										  ui.createComponent('product', {
											id: [8690588233],
											node: document.getElementById('product-component-1513246124469'),
											moneyFormat: '${{amount}}',
											options: {"product":{"buttonDestination":"checkout","variantId":"all","width":"240px","contents":{"img":false,"imgWithCarousel":false,"title":false,"variantTitle":false,"price":false,"description":false,"buttonWithQuantity":false,"quantity":false},"text":{"button":"BUY"},"styles":{"product":{"text-align":"left","@media (min-width: 601px)":{"max-width":"100%","margin-left":"0","margin-bottom":"50px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Vollkorn, serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","color":"#daa12b","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-size":"12px","font-family":"Gill Sans, sans-serif","font-weight":"normal","color":"#daa12b"}},"googleFonts":["Karla","Vollkorn"]},"cart":{"contents":{"button":true},"styles":{"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"footer":{"background-color":"#ffffff"}},"googleFonts":["Karla"]},"modalProduct":{"contents":{"img":false,"imgWithCarousel":true,"variantTitle":false,"buttonWithQuantity":true,"button":false,"quantity":false},"styles":{"product":{"@media (min-width: 601px)":{"max-width":"100%","margin-left":"0px","margin-bottom":"0px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Vollkorn, serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"}},"googleFonts":["Karla","Vollkorn"]},"toggle":{"styles":{"toggle":{"font-family":"Karla, sans-serif","background-color":"#d3c27c",":hover":{"background-color":"#beaf70"},":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"count":{"font-size":"17px","color":"#000000",":hover":{"color":"#000000"}},"iconPath":{"fill":"#000000"}},"googleFonts":["Karla"]},"option":{"styles":{"label":{"font-family":"Gill Sans, sans-serif"},"select":{"font-family":"Gill Sans, sans-serif"}}},"productSet":{"styles":{"products":{"@media (min-width: 601px)":{"margin-left":"-20px"}}}}},
										  });
										});
									  }
									})();
									/*]]>*/
									</script>
								</div>
						<!-- end button BUY LEVEL-FULL -->					
				</div>		
						
						
						
					</div>
					<div class="currentDayAndOpenDay">
						<p class="dvYouarecurrently">You are currently on Day
							<span id="appent_count_number" class="appent_count number" runat="server"><b>0</b></span> of <b>365</b> DAYS
						</p>
						<p class="days_hide"></p>
						<p class="dvYourlessonprgress">
							After 24 hours you will be able to activate Day
							<span id="appent_count_number_progress" class="appent_count number" runat="server"><b>0</b></span>
						</p>
					</div>
					
					<div class="dvInfoNoPackage">
						<p>	You haven't yet purchased any package </br>
							Please select a package and click <b>BUY</b> button							
						</p>
					</div>
				</div>
                <div class="useGuide" id="useGuide"></div>
   
				<div id="parentBuyProduct" runat="server">
					<div class="level1 clearfix">
						<div class="product365dayParent">
								<div id="product-component-3f4fe7bc515"></div>
								<script type="text/javascript">
									/*<![CDATA[*/

									(function () {
										var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
										if (window.ShopifyBuy) {
											if (window.ShopifyBuy.UI) {
												ShopifyBuyInit();
											} else {
												loadScript();
											}
										} else {
											loadScript();
										}

										function loadScript() {
											var script = document.createElement('script');
											script.async = true;
											script.src = scriptURL;
											(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
											script.onload = ShopifyBuyInit;
										}

										function ShopifyBuyInit() {
											var client = ShopifyBuy.buildClient({
												domain: 'andhakaara.myshopify.com',
												apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
												appId: '6',
											});

											ShopifyBuy.UI.onReady(client).then(function (ui) {
												ui.createComponent('product', {
													id: [8690440329],
													node: document.getElementById('product-component-3f4fe7bc515'),
													moneyFormat: '%24%7B%7Bamount%7D%7D',
													options: {
														"product": {
															"buttonDestination": "checkout",
															"variantId": "all",
															"width": "240px",
															"margin-left": "27px",
															"margin-top": "-10px !important",
															"contents": {
																"img": false,
																"imgWithCarousel": false,
																"title": false,
																"variantTitle": false,
																"price": false,
																"description": false,
																"buttonWithQuantity": false,
																"quantity": false
															},
															"text": {
																"button": "BUY"
															},
															"styles": {
																"product": {
																	"text-align": "left",
																	"@media (min-width: 601px)": {
																		"max-width": "100%",
																		"margin-left": "0",
																		"margin-bottom": "50px"
																	}
																},
																"button": {
																	"background-color": "#f9e684",
																	"color": "#000000",
																	"font-size": "13px",
																	"padding-top": "14.5px",
																	"padding-bottom": "14.5px",
																	"padding-left": "17px",
																	"padding-right": "17px",
																	":hover": {
																		"background-color": "#e0cf77",
																		"color": "#000000"
																	},
																	"border-radius": "11px",
																	":focus": {
																		"background-color": "#e0cf77"
																	},
																	"font-weight": "normal"
																},
																"variantTitle": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																},
																"title": {
																	"font-family": "Droid Sans, sans-serif",
																	"font-weight": "normal",
																	"font-size": "26px"
																},
																"description": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																},
																"price": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-size": "18px",
																	"font-weight": "normal"
																},
																"quantityInput": {
																	"font-size": "13px",
																	"padding-top": "14.5px",
																	"padding-bottom": "14.5px"
																},
																"compareAt": {
																	"font-size": "15px",
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																}
															},
															"googleFonts": [
																"Droid Sans"
															]
														},
														"cart": {
															"contents": {
																"button": true
															},
															"styles": {
																"button": {
																	"background-color": "#f9e684",
																	"color": "#000000",
																	"font-size": "13px",
																	"padding-top": "14.5px",
																	"padding-bottom": "14.5px",
																	":hover": {
																		"background-color": "#e0cf77",
																		"color": "#000000"
																	},
																	"border-radius": "11px",
																	":focus": {
																		"background-color": "#e0cf77"
																	},
																	"font-weight": "normal"
																},
																"footer": {
																	"background-color": "#ffffff"
																}
															}
														},
														"modalProduct": {
															"contents": {
																"img": false,
																"imgWithCarousel": true,
																"variantTitle": false,
																"buttonWithQuantity": true,
																"button": false,
																"quantity": false
															},
															"styles": {
																"product": {
																	"@media (min-width: 601px)": {
																		"max-width": "100%",
																		"margin-left": "0px",
																		"margin-bottom": "0px"
																	}
																},
																"button": {
																	"background-color": "#f9e684",
																	"color": "#000000",
																	"font-size": "13px",
																	"padding-top": "14.5px",
																	"padding-bottom": "14.5px",
																	"padding-left": "17px",
																	"padding-right": "17px",
																	":hover": {
																		"background-color": "#e0cf77",
																		"color": "#000000"
																	},
																	"border-radius": "11px",
																	":focus": {
																		"background-color": "#e0cf77"
																	},
																	"font-weight": "normal"
																},
																"variantTitle": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																},
																"title": {
																	"font-family": "Droid Sans, sans-serif",
																	"font-weight": "normal"
																},
																"description": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																},
																"price": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																},
																"quantityInput": {
																	"font-size": "13px",
																	"padding-top": "14.5px",
																	"padding-bottom": "14.5px"
																},
																"compareAt": {
																	"font-family": "Gill Sans, sans-serif",
																	"font-weight": "normal"
																}
															},
															"googleFonts": [
																"Droid Sans"
															]
														},
														"toggle": {
															"styles": {
																"toggle": {
																	"background-color": "#f9e684",
																	":hover": {
																		"background-color": "#e0cf77"
																	},
																	":focus": {
																		"background-color": "#e0cf77"
																	},
																	"font-weight": "normal"
																},
																"count": {
																	"font-size": "13px",
																	"color": "#000000",
																	":hover": {
																		"color": "#000000"
																	}
																},
																"iconPath": {
																	"fill": "#000000"
																}
															}
														},
														"option": {
															"styles": {
																"label": {
																	"font-family": "Gill Sans, sans-serif"
																},
																"select": {
																	"font-family": "Gill Sans, sans-serif"
																}
															}
														},
														"productSet": {
															"styles": {
																"products": {
																	"@media (min-width: 601px)": {
																		"margin-left": "-20px"
																	}
																}
															}
														}
													}
												});
											});
										}
									})();
									/*]]>*/
								</script>
						</div>
						<p><b>LEVEL 1</b> (DAY 1-DAY 51) for <label><b>USD 308.00</b></label></p>
					</div>
					
					<div class="buy_all_product clearfix">
						<div class="buy_all_text"> <p><b>or all 365 DAYS </b>FOR  </p><label><b>USD 1008.00</b></label></div>
						<div id="product-component-bbf61d48ba3"></div>
						<script type="text/javascript">
							/*<![CDATA[*/

							(function () {
								var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
								if (window.ShopifyBuy) {
									if (window.ShopifyBuy.UI) {
										ShopifyBuyInit();
									} else {
										loadScript();
									}
								} else {
									loadScript();
								}

								function loadScript() {
									var script = document.createElement('script');
									script.async = true;
									script.src = scriptURL;
									(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
									script.onload = ShopifyBuyInit;
								}

								function ShopifyBuyInit() {
									var client = ShopifyBuy.buildClient({
										domain: 'andhakaara.myshopify.com',
										apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
										appId: '6',
									});

									ShopifyBuy.UI.onReady(client).then(function (ui) {
										ui.createComponent('product', {
											id: [8690588233],
											node: document.getElementById('product-component-bbf61d48ba3'),
											moneyFormat: '%24%7B%7Bamount%7D%7D',
											options: {
												"product": {
													"buttonDestination": "checkout",
													"variantId": "all",
													"width": "240px",
													"contents": {
														"img": false,
														"imgWithCarousel": false,
														"title": false,
														"variantTitle": false,
														"price": false,
														"description": false,
														"buttonWithQuantity": false,
														"quantity": false
													},
													"text": {
														"button": "BUY"
													},
													"styles": {
														"product": {
															"text-align": "left",
															"@media (min-width: 601px)": {
																"max-width": "100%",
																"margin-left": "0",
																"margin-bottom": "50px"
															}
														},
														"button": {
															"background-color": "#8cc63f",
															"color": "#000000",
															"font-size": "13px",
															"padding-top": "14.5px",
															"padding-bottom": "14.5px",
															"padding-left": "17px",
															"padding-right": "17px",
															":hover": {
																"background-color": "#7eb239",
																"color": "#000000"
															},
															"border-radius": "11px",
															":focus": {
																"background-color": "#7eb239"
															},
															"font-weight": "normal"
														},
														"variantTitle": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														},
														"title": {
															"font-family": "Droid Sans, sans-serif",
															"font-weight": "normal",
															"font-size": "26px"
														},
														"description": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														},
														"price": {
															"font-family": "Gill Sans, sans-serif",
															"font-size": "18px",
															"font-weight": "normal"
														},
														"quantityInput": {
															"font-size": "13px",
															"padding-top": "14.5px",
															"padding-bottom": "14.5px"
														},
														"compareAt": {
															"font-size": "15px",
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														}
													},
													"googleFonts": [
														"Droid Sans"
													]
												},
												"cart": {
													"contents": {
														"button": true
													},
													"styles": {
														"button": {
															"background-color": "#8cc63f",
															"color": "#000000",
															"font-size": "13px",
															"padding-top": "14.5px",
															"padding-bottom": "14.5px",
															":hover": {
																"background-color": "#7eb239",
																"color": "#000000"
															},
															"border-radius": "11px",
															":focus": {
																"background-color": "#7eb239"
															},
															"font-weight": "normal"
														},
														"footer": {
															"background-color": "#ffffff"
														}
													}
												},
												"modalProduct": {
													"contents": {
														"img": false,
														"imgWithCarousel": true,
														"variantTitle": false,
														"buttonWithQuantity": true,
														"button": false,
														"quantity": false
													},
													"styles": {
														"product": {
															"@media (min-width: 601px)": {
																"max-width": "100%",
																"margin-left": "0px",
																"margin-bottom": "0px"
															}
														},
														"button": {
															"background-color": "#8cc63f",
															"color": "#000000",
															"font-size": "13px",
															"padding-top": "14.5px",
															"padding-bottom": "14.5px",
															"padding-left": "17px",
															"padding-right": "17px",
															":hover": {
																"background-color": "#7eb239",
																"color": "#000000"
															},
															"border-radius": "11px",
															":focus": {
																"background-color": "#7eb239"
															},
															"font-weight": "normal"
														},
														"variantTitle": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														},
														"title": {
															"font-family": "Droid Sans, sans-serif",
															"font-weight": "normal"
														},
														"description": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														},
														"price": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														},
														"quantityInput": {
															"font-size": "13px",
															"padding-top": "14.5px",
															"padding-bottom": "14.5px"
														},
														"compareAt": {
															"font-family": "Gill Sans, sans-serif",
															"font-weight": "normal"
														}
													},
													"googleFonts": [
														"Droid Sans"
						                            ]
						                        },
						                        "toggle": {
						                            "styles": {
						                                "toggle": {
						                                    "background-color": "#8cc63f",
						                                    ":hover": {
						                                        "background-color": "#7eb239"
						                                    },
						                                    ":focus": {
						                                        "background-color": "#7eb239"
						                                    },
						                                    "font-weight": "normal"
						                                },
						                                "count": {
						                                    "font-size": "13px",
						                                    "color": "#000000",
						                                    ":hover": {
						                                        "color": "#000000"
						                                    }
						                                },
						                                "iconPath": {
						                                    "fill": "#000000"
						                                }
						                            }
						                        },
						                        "option": {
						                            "styles": {
						                                "label": {
						                                    "font-family": "Gill Sans, sans-serif"
						                                },
						                                "select": {
						                                    "font-family": "Gill Sans, sans-serif"
						                                }
						                            }
						                        },
						                        "productSet": {
						                            "styles": {
						                                "products": {
						                                    "@media (min-width: 601px)": {
						                                        "margin-left": "-20px"
						                                    }
						                                }
						                            }
						                        }
						                    }
						                });
						            });
						        }
						    })();
						    /*]]>*/
						</script>
					</div>										
				</div>
				
				<!-- Package proposed with no login on mobile -->
					<div class="mobileNoLoginPrice">								
						<div class="titlePriceLevel1"><b>LEVEL 1 </b>(DAY 1-DAY 51) for </div><div class="priceLevel1"><b>USD 308.00</b></div>
						<!-- button BUY LEVEL-1 -->
								<div class="btBuyLevel1">
									<div id='product-component-1513581970841'></div>
									<script type="text/javascript">
									/*<![CDATA[*/
									  (function () {
										var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
										if (window.ShopifyBuy) {
										  if (window.ShopifyBuy.UI) {
											ShopifyBuyInit();
										  } else {
											loadScript();
										  }
										} else {
										  loadScript();
										}

									  function loadScript() {
										var script = document.createElement('script');
										script.async = true;
										script.src = scriptURL;
										(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
										script.onload = ShopifyBuyInit;
									  }

									  function ShopifyBuyInit() {
										var client = ShopifyBuy.buildClient({
										  domain: 'andhakaara.myshopify.com',
										  apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
										  appId: '6',
										});

										ShopifyBuy.UI.onReady(client).then(function (ui) {
										  ui.createComponent('product', {
											id: [8690440329],
											node: document.getElementById('product-component-1513581970841'),
											moneyFormat: '${{amount}}',
											options: {"product":{"buttonDestination":"checkout","variantId":"all","width":"240px","contents":{"img":false,"imgWithCarousel":false,"title":false,"variantTitle":false,"price":false,"description":false,"buttonWithQuantity":false,"quantity":false},"text":{"button":"BUY"},"styles":{"product":{"text-align":"left","@media (min-width: 601px)":{"max-width":"100%","margin-left":"0","margin-bottom":"50px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Roboto, sans-serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","color":"#3d3d3d","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-size":"12px","font-family":"Gill Sans, sans-serif","font-weight":"normal","color":"#3d3d3d"}},"googleFonts":["Karla","Roboto"]},"cart":{"contents":{"button":true},"styles":{"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"footer":{"background-color":"#ffffff"}},"googleFonts":["Karla"]},"modalProduct":{"contents":{"img":false,"imgWithCarousel":true,"variantTitle":false,"buttonWithQuantity":true,"button":false,"quantity":false},"styles":{"product":{"@media (min-width: 601px)":{"max-width":"100%","margin-left":"0px","margin-bottom":"0px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Roboto, sans-serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"}},"googleFonts":["Karla","Roboto"]},"toggle":{"styles":{"toggle":{"font-family":"Karla, sans-serif","background-color":"#d3c27c",":hover":{"background-color":"#beaf70"},":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"count":{"font-size":"17px","color":"#000000",":hover":{"color":"#000000"}},"iconPath":{"fill":"#000000"}},"googleFonts":["Karla"]},"option":{"styles":{"label":{"font-family":"Gill Sans, sans-serif"},"select":{"font-family":"Gill Sans, sans-serif"}}},"productSet":{"styles":{"products":{"@media (min-width: 601px)":{"margin-left":"-20px"}}}}},
										  });
										});
									  }
									})();
									/*]]>*/
									</script>	
								</div>
						<!-- end button BUY LEVEL-1 -->
						
						<div class="titlePriceLevelFull"><b>or all 365 DAYS </b> for </div> <div class="priceLevelFull"><b>USD 1008.00</b></div>
						<!-- button BUY LEVEL-Full -->
								<div class="btBuyLevelFull">
								<div id='product-component-1513581853738'></div>
									<script type="text/javascript">
									/*<![CDATA[*/
									  (function () {
										var scriptURL = 'https://sdks.shopifycdn.com/buy-button/latest/buy-button-storefront.min.js';
										if (window.ShopifyBuy) {
										  if (window.ShopifyBuy.UI) {
											ShopifyBuyInit();
										  } else {
											loadScript();
										  }
										} else {
										  loadScript();
										}

									  function loadScript() {
										var script = document.createElement('script');
										script.async = true;
										script.src = scriptURL;
										(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
										script.onload = ShopifyBuyInit;
									  }

									  function ShopifyBuyInit() {
										var client = ShopifyBuy.buildClient({
										  domain: 'andhakaara.myshopify.com',
										  apiKey: 'b405fc4d6cd13e6f0c549420645e61d4',
										  appId: '6',
										});

										ShopifyBuy.UI.onReady(client).then(function (ui) {
										  ui.createComponent('product', {
											id: [8690588233],
											node: document.getElementById('product-component-1513581853738'),
											moneyFormat: '${{amount}}',
											options: {"product":{"buttonDestination":"checkout","variantId":"all","width":"240px","contents":{"img":false,"imgWithCarousel":false,"title":false,"variantTitle":false,"price":false,"description":false,"buttonWithQuantity":false,"quantity":false},"text":{"button":"BUY"},"styles":{"product":{"text-align":"left","@media (min-width: 601px)":{"max-width":"100%","margin-left":"0","margin-bottom":"50px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Roboto, sans-serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","color":"#3d3d3d","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-size":"12px","font-family":"Gill Sans, sans-serif","font-weight":"normal","color":"#3d3d3d"}},"googleFonts":["Karla","Roboto"]},"cart":{"contents":{"button":true},"styles":{"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"footer":{"background-color":"#ffffff"}},"googleFonts":["Karla"]},"modalProduct":{"contents":{"img":false,"imgWithCarousel":true,"variantTitle":false,"buttonWithQuantity":true,"button":false,"quantity":false},"styles":{"product":{"@media (min-width: 601px)":{"max-width":"100%","margin-left":"0px","margin-bottom":"0px"}},"button":{"background-color":"#d3c27c","color":"#000000","font-family":"Karla, sans-serif","font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px","padding-left":"17px","padding-right":"17px",":hover":{"background-color":"#beaf70","color":"#000000"},"border-radius":"11px",":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"variantTitle":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"title":{"font-family":"Roboto, sans-serif"},"description":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"price":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"},"quantityInput":{"font-size":"17px","padding-top":"16.5px","padding-bottom":"16.5px"},"compareAt":{"font-family":"Gill Sans, sans-serif","font-weight":"normal"}},"googleFonts":["Karla","Roboto"]},"toggle":{"styles":{"toggle":{"font-family":"Karla, sans-serif","background-color":"#d3c27c",":hover":{"background-color":"#beaf70"},":focus":{"background-color":"#beaf70"},"font-weight":"normal"},"count":{"font-size":"17px","color":"#000000",":hover":{"color":"#000000"}},"iconPath":{"fill":"#000000"}},"googleFonts":["Karla"]},"option":{"styles":{"label":{"font-family":"Gill Sans, sans-serif"},"select":{"font-family":"Gill Sans, sans-serif"}}},"productSet":{"styles":{"products":{"@media (min-width: 601px)":{"margin-left":"-20px"}}}}},
										  });
										});
									  }
									})();
									/*]]>*/
									</script>	
									
								</div>
						<!-- end button BUY LEVEL-FULL -->					
				</div>
					<!-- END Package proposed with no login on mobile -->
				
				<div class="andhakaaraX365">
					<div class="andhakaaraX365Name clearfix" id="anchorStartMatrix">
						<div>
							<h1>365</h1>
							<h1>DAYS</h1>
						</div>
						<div>
							<p>THE ANDHAKAARA PATH TO POWER</p>
						</div>
					</div>
					
					<div class="flex_content_center">    
						   <div id="dvLessions" class="dvLessions" runat="server">
						   </div>
					<!-- Load the introduction on the rigth site	   -->
						  <div class="postImages"> 
								<iframe class="zeroDayIframe" src="http://365-days.andhakaara.com/Lesson_content/DAY_0/index.html" frameborder="0" scrolling="no"></iframe>
								
						  </div>
					</div>
				</div>   <!--End div andhakaaraX365 -->
			</div>
	 </div>   <!--End div Main Matrix 365 body -->
		
		<div class="postImages_mobile" id="anchorDayContent"> 
			<iframe class="zeroDayIframe" src="http://365-days.andhakaara.com/Lesson_content/DAY_0/index.html" frameborder="0" scrolling="no"></iframe>
				<div class="btBackToTop">
					<a id="back-to-top">Back to top</a>
				</div>
		</div>

     
	
		<div class="verticalspacer" data-offset-top="1884" data-content-above-spacer="1884" data-content-below-spacer="199"></div>
	   <div class="clearfix grpelem" id="ppu29096"><!-- column -->
			<div class="clearfix colelem" id="pu29096"><!-- group -->
				 <a class="nonblock nontext clip_frame grpelem" id="u29096" href="https://inktale.com/andhakaara" target="_blank"><!-- image --><img class="block" id="u29096_img" src="images/inktale_orange_2x.jpg?crc=130134847" alt="" width="129" height="81"/></a>
				 <a class="nonblock nontext clip_frame grpelem" id="u29108" href="https://www.facebook.com/Andhakaara-1710645739160224/"><!-- image --><img class="block" id="u29108_img" src="images/images.png?crc=3965400639" data-hidpi-src="images/images_2x.png?crc=3944263049" alt="" width="40" height="40"/></a>
				 <a class="nonblock nontext clip_frame clearfix grpelem" id="u29161" href="https://www.instagram.com/andhakaara/"><!-- image --><img class="position_content" id="u29161_img" src="images/instagram-icon_1057-2227.jpg?crc=132121548" data-hidpi-src="images/instagram-icon_1057-2227_2x.jpg?crc=492954730" alt="" width="50" height="50"/></a>
				 <a class="nonblock nontext clip_frame grpelem" id="u29106" href="https://www.youtube.com/channel/UCGzjF-K8tvqavHE16nOFu2Q"><!-- image --><img class="block" id="u29106_img" src="images/igh0zt-ios7-style-metro-ui-metroui-youtube-alt-2.jpg?crc=214828878" data-hidpi-src="images/igh0zt-ios7-style-metro-ui-metroui-youtube-alt-2_2x.jpg?crc=3790459328" alt="" width="40" height="40"/></a>
			</div>
			<div class="clearfix colelem" id="u29160-8"><!-- content -->
				 <p>Andhakaara Limited - Seershin - Furbo - Galway - Ireland -</p>
				 <p>Ulster Bank - IBAN IE96ULSB98575310623362 - BIC ULSBIE2D</p>
				 <p>UID 3276047AH</p>
			</div>
	   </div>
  </div>
 </div>
		<script>

				!function(t){"use strict";var s=function(s,e){this.el=t(s),this.options=t.extend({},t.fn.typed.defaults,e),this.isInput=this.el.is("input"),this.attr=this.options.attr,this.showCursor=this.isInput?!1:this.options.showCursor,this.elContent=this.attr?this.el.attr(this.attr):this.el.text(),this.contentType=this.options.contentType,this.typeSpeed=this.options.typeSpeed,this.startDelay=this.options.startDelay,this.backSpeed=this.options.backSpeed,this.backDelay=this.options.backDelay,this.stringsElement=this.options.stringsElement,this.strings=this.options.strings,this.strPos=0,this.arrayPos=0,this.stopNum=0,this.loop=this.options.loop,this.loopCount=this.options.loopCount,this.curLoop=0,this.stop=!1,this.cursorChar=this.options.cursorChar,this.shuffle=this.options.shuffle,this.sequence=[],this.build()};s.prototype={constructor:s,init:function(){var t=this;t.timeout=setTimeout(function(){for(var s=0;s<t.strings.length;++s)t.sequence[s]=s;t.shuffle&&(t.sequence=t.shuffleArray(t.sequence)),t.typewrite(t.strings[t.sequence[t.arrayPos]],t.strPos)},t.startDelay)},build:function(){var s=this;if(this.showCursor===!0&&(this.cursor=t('<span class="typed-cursor">'+this.cursorChar+"</span>"),this.el.after(this.cursor)),this.stringsElement){s.strings=[],this.stringsElement.hide();var e=this.stringsElement.find("p");t.each(e,function(e,i){s.strings.push(t(i).html())})}this.init()},typewrite:function(t,s){if(this.stop!==!0){var e=Math.round(70*Math.random())+this.typeSpeed,i=this;i.timeout=setTimeout(function(){var e=0,r=t.substr(s);if("^"===r.charAt(0)){var o=1;/^\^\d+/.test(r)&&(r=/\d+/.exec(r)[0],o+=r.length,e=parseInt(r)),t=t.substring(0,s)+t.substring(s+o)}if("html"===i.contentType){var n=t.substr(s).charAt(0);if("<"===n||"&"===n){var a="",h="";for(h="<"===n?">":";";t.substr(s).charAt(0)!==h;)a+=t.substr(s).charAt(0),s++;s++,a+=h}}i.timeout=setTimeout(function(){if(s===t.length){if(i.options.onStringTyped(i.arrayPos),i.arrayPos===i.strings.length-1&&(i.options.callback(),i.curLoop++,i.loop===!1||i.curLoop===i.loopCount))return;i.timeout=setTimeout(function(){i.backspace(t,s)},i.backDelay)}else{0===s&&i.options.preStringTyped(i.arrayPos);var e=t.substr(0,s+1);i.attr?i.el.attr(i.attr,e):i.isInput?i.el.val(e):"html"===i.contentType?i.el.html(e):i.el.text(e),s++,i.typewrite(t,s)}},e)},e)}},backspace:function(t,s){if(this.stop!==!0){var e=Math.round(70*Math.random())+this.backSpeed,i=this;i.timeout=setTimeout(function(){if("html"===i.contentType&&">"===t.substr(s).charAt(0)){for(var e="";"<"!==t.substr(s).charAt(0);)e-=t.substr(s).charAt(0),s--;s--,e+="<"}var r=t.substr(0,s);i.attr?i.el.attr(i.attr,r):i.isInput?i.el.val(r):"html"===i.contentType?i.el.html(r):i.el.text(r),s>i.stopNum?(s--,i.backspace(t,s)):s<=i.stopNum&&(i.arrayPos++,i.arrayPos===i.strings.length?(i.arrayPos=0,i.shuffle&&(i.sequence=i.shuffleArray(i.sequence)),i.init()):i.typewrite(i.strings[i.sequence[i.arrayPos]],s))},e)}},shuffleArray:function(t){var s,e,i=t.length;if(i)for(;--i;)e=Math.floor(Math.random()*(i+1)),s=t[e],t[e]=t[i],t[i]=s;return t},reset:function(){var t=this;clearInterval(t.timeout);var s=this.el.attr("id");this.el.after('<span id="'+s+'"/>'),this.el.remove(),"undefined"!=typeof this.cursor&&this.cursor.remove(),t.options.resetCallback()}},t.fn.typed=function(e){return this.each(function(){var i=t(this),r=i.data("typed"),o="object"==typeof e&&e;r||i.data("typed",r=new s(this,o)),"string"==typeof e&&r[e]()})},t.fn.typed.defaults={strings:["These are the default values...","You know what you should do?","Use your own!","Have a great day!"],stringsElement:null,typeSpeed:0,startDelay:0,backSpeed:0,shuffle:!1,backDelay:500,loop:!1,loopCount:!1,showCursor:!0,cursorChar:"|",attr:null,contentType:"html",callback:function(){},preStringTyped:function(){},onStringTyped:function(){},resetCallback:function(){}}}(window.jQuery);

			</script>

			<script>

				$(".skilltechtypetext").typed({
					strings: ["TANTRIC GAMES AGAINST SOCIAL ALIENTATION^", "ART - SEXOLOGY - SPIRITUALITY  - PSYCHOLOGY ^", "ANCIENT WISDOM FOR MODERN TIMES^"],
					contentType: 'text', // 'html' or 'text'
					typeSpeed: 30,
					loop: true,
					backDelay: 1200,
					showCursor: true,
			        cursorChar: "|",
				});

			</script>
			
			<script>
				$(document).ready(function(){
				  // Add smooth scrolling to all links
				  $("a").on('click', function(event) {

					// Make sure this.hash has a value before overriding default behavior
					if (this.hash !== "") {
					  // Prevent default anchor click behavior
					  event.preventDefault();

					  // Store hash
					  var hash = this.hash;

					  $('html, body').animate({
						scrollTop: $(hash).offset().top
					  }, 1200, function(){
				   
						// Add hash (#) to URL when done scrolling (default click behavior)
						window.location.hash = hash;
					  });
					} // End if
				  });
				});
			</script>



 
     

</asp:Content>
