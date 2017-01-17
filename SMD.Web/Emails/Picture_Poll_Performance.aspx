
<html>
<head>
    <meta charset="utf-8" />
    <meta content="IE=edge" http-equiv="X-UA-Compatible" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>Picture Poll Performance Update
    </title>
    <style type="text/css">
        /*////// RESET STYLES //////*/
        p {
            margin: 10px 0;
            padding: 0;
        }

        table {
            border-collapse: collapse;
        }

        h1, h2, h3, h4, h5, h6 {
            display: block;
            margin: 0;
            padding: 0;
        }

        img, a img {
            border: 0;
            height: auto;
            outline: none;
            text-decoration: none;
        }

        body, #bodyTable, #bodyCell {
            height: 100%;
            margin: 0;
            padding: 0;
            width: 100%;
        }

        /*////// CLIENT-SPECIFIC STYLES //////*/
        #outlook a {
            padding: 0;
        }
        /* Force Outlook 2007 and up to provide a "view in browser" message. */
        @-ms-viewport {
            width: device-width;
        }
        /* Force IE "snap mode" to render widths normally. */
        img {
            -ms-interpolation-mode: bicubic;
        }
        /* Force IE to smoothly render resized images. */
        table {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }
        /* Remove spacing between tables in Outlook Desktop. */
        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }
        /* Force Outlook.com to display emails at full width. */
        p, a, li, td, blockquote {
            mso-line-height-rule: exactly;
        }
            /* Force Outlook Desktop to render line heights as they're originally set. */
            a[href^="tel"], a[href^="sms"] {
                color: inherit;
                cursor: default;
                text-decoration: none;
            }
        /* Force mobile devices to inherit declared link styles. */
        p, a, li, td, body, table, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }
        /* Prevent Windows- and Webkit-based platforms from changing declared text sizes. */
        .ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font {
            line-height: 100%;
        }
        /* Force Outlook.com to display line heights normally. */
        a[x-apple-data-detectors] {
            color: inherit !important;
            text-decoration: none !important;
            font-size: inherit !important;
            font-family: inherit !important;
            font-weight: inherit !important;
            line-height: inherit !important;
        }
        /* Force iOS devices to heed link styles set in CSS. */


        #footerContent a {
            color: #B7B7B7 !important;
        }

        /*////// MOBILE STYLES //////*/
        @media only screen and (max-width:480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            h1 {
                font-size: 24px !important;
            }

            #templateHeader {
                padding-right: 20px !important;
                padding-left: 20px !important;
            }

            #headerContainer {
                padding-right: 0 !important;
                padding-left: 0 !important;
            }

            #headerTable {
                border-top-left-radius: 0 !important;
                border-top-right-radius: 0 !important;
            }

                #headerTable td {
                    padding-top: 30px !important;
                }

            #bodyContainer {
                padding-right: 20px !important;
                padding-left: 20px !important;
            }

            #bodyContent {
                padding-right: 0 !important;
            }

            #footerContent p {
                border-bottom: 1px solid #E5E5E5;
                font-size: 14px !important;
                padding-bottom: 40px !important;
            }

            .utilityLink {
                border-bottom: 1px solid #E5E5E5;
                display: block;
                font-size: 13px !important;
                padding-top: 20px;
                padding-bottom: 20px;
                text-decoration: none !important;
            }

            .mobileHide {
                display: none;
                visibility: hidden;
            }
        }

        @media screen {
            @font-face { /* latin */
                font-family: 'Open Sans';
                font-style: normal;
                font-weight: 400;
                src: local('Open Sans'), local('OpenSans'), url(http://fonts.gstatic.com/s/opensans/v10/cJZKeOuBrn4kERxqtaUH3ZBw1xU1rKptJj_0jans920.woff2) format('woff2');
                unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2212, U+2215, U+E0FF, U+EFFD, U+F000;
            }

            @font-face { /* latin */
                font-family: 'Open Sans';
                font-style: normal;
                font-weight: 700;
                src: local('Open Sans Bold'), local('OpenSans-Bold'), url(http://fonts.gstatic.com/s/opensans/v10/k3k702ZOKiLJc3WVjuplzBampu5_7CjHW5spxoeN3Vs.woff2) format('woff2');
                unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2212, U+2215, U+E0FF, U+EFFD, U+F000;
            }

            @font-face { /* latin-ext */
                font-family: 'Open Sans';
                font-style: normal;
                font-weight: 400;
                src: local('Open Sans'), local('OpenSans'), url(http://fonts.gstatic.com/s/opensans/v10/u-WUoqrET9fUeobQW7jkRZBw1xU1rKptJj_0jans920.woff2) format('woff2');
                unicode-range: U+0100-024F, U+1E00-1EFF, U+20A0-20AB, U+20AD-20CF, U+2C60-2C7F, U+A720-A7FF;
            }

            @font-face { /* latin-ext */
                font-family: 'Open Sans';
                font-style: normal;
                font-weight: 700;
                src: local('Open Sans Bold'), local('OpenSans-Bold'), url(http://fonts.gstatic.com/s/opensans/v10/k3k702ZOKiLJc3WVjuplzCYtBUPDK3WL7KRKS_3q7OE.woff2) format('woff2');
                unicode-range: U+0100-024F, U+1E00-1EFF, U+20A0-20AB, U+20AD-20CF, U+2C60-2C7F, U+A720-A7FF;
            }

            *, td {
                font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif !important;
            }
        }
    </style>
</head>
<body>
    <center>      
      <table align="center" border="0" cellpadding="0" cellspacing="0" height="100%" id="bodyTable" width="100%">        
        <tr>          
          <td align="center" id="bodyCell" valign="top">            
            <span style="color:#FFFFFF; display:none; font-size:0px; height:0px; visibility:hidden; width:0px;">Picture Poll Performance Update ++campaignname++</span>
<!-- BEGIN TEMPLATE // -->
            <table border="0" cellpadding="0" cellspacing="0" width="100%">              
              <tr>                
                <td align="center" bgcolor="#0f68a1" id="templateHeader" style="background-color:#0f68a1; padding-right:30px; padding-left:30px;" valign="top">                  
<!--[if gte mso 9]>
                                    <table align="center" border="0" cellspacing="0" cellpadding="0" width="400">
                                    <tr>
                                    <td align="center" valign="top" width="400">
                                    <![endif]-->
                  <table align="center" border="0" cellpadding="0" cellspacing="0" class="emailContainer" style="max-width:700px;" width="100%">                    
                    <tr>                      
                      <td align="center" id="logoContainer" style="padding-top:40px; padding-bottom:20px;" valign="top">                        
                        <img alt="MailChimp" height="63" src="http://manage.cash4ads.com/emails/images/Cash4Ads_Email_Logo.png" style="color:#FFFFFF; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:12px; font-weight:400; letter-spacing:-1px; padding:0; margin:0; text-align:center;" width="60"/>
                      </td>
                    </tr>
                    <tr>                      
                      <td align="center" style="padding-bottom:10px;" valign="top">                        
                        <h1 style="color:#FFFFFF; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:30px; font-style:normal; font-weight:600; line-height:42px; letter-spacing:normal; margin:0; padding:0; text-align:center;">
                        Picture Poll Performance Update
                        </h1>
                      </td>
                    </tr>
                       <tr>                      
                      <td align="center" style="padding-bottom:10px;" valign="top">                        
                        <p style="color:#FFFFFF; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:400; line-height:24px; padding:0; margin:0; text-align:center;">
                         ++campaignname++
                        </p>
                      </td>
                    </tr>
                   
                  </table>
<!--[if gte mso 9]>
                                    </td>
                                    </tr>
                                    </table>
                                    <![endif]-->
                </td>
              </tr>
              <tr>                
                <td align="center" bgcolor="#0f68a1" id="headerContainer" style="background-color:#0f68a1; padding-right:30px; padding-left:30px;" valign="top">                  
                  <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">                    
                    <tr>                      
                      <td align="center" valign="top">                        
<!--[if gte mso 9]>
                                                <table align="center" border="0" cellspacing="0" cellpadding="0" width="640">
                                                <tr>
                                                <td align="center" valign="top" width="640">
                                                <![endif]-->
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="emailContainer" style="max-width:640px;" width="100%">                          
                          <tr>                            
                            <td align="center" valign="top">                              
                              <table align="center" bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" id="headerTable" style="background-color:#FFFFFF; border-collapse:separate; border-top-left-radius:4px; border-top-right-radius:4px;" width="100%">                                
                                <tr>                                  
                                  <td align="center" style="padding-top:40px; padding-bottom:0;" valign="top" width="100%">
                                    &nbsp;
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
<!--[if gte mso 9]>
                                                </td>
                                                </tr>
                                                </table>
                                                <![endif]-->
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>                
                <td align="center" id="templateBody" valign="top">                  
                  <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">                    
                    <tr>                      
                      <td align="center" valign="top">                        
<!--[if gte mso 9]>
                                                <table align="center" border="0" cellspacing="0" cellpadding="0" width="700">
                                                <tr>
                                                <td align="center" valign="top" width="700">
                                                <![endif]-->
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="emailContainer" style="max-width:700px;" width="100%">                          
                          <tr>                            
                            <td align="right" class="mobileHide" valign="top" width="30">                              
                              <img src="http://cdn-images.mailchimp.com/template_images/tr_email/arrow.jpg" style="display:block;" width="30"/>
                            </td>
                            <td id="bodyContainer" style="padding-right:70px; padding-left:40px;" valign="top" width="100%">                              
                              <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">                                
                                <tr>                                  
                                  <td align="left" id="bodyContent" valign="top">                                    
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">                                      
                                      <tr>                                        
                                        <td align="left" style="padding-right:20px; padding-bottom:60px;" valign="top">                                          
                                          <img alt="Lists" src="http://manage.cash4ads.com/emails/images/Polls_Email_Blue.png" style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:14px; font-weight:600; height:auto; letter-spacing:-1px; padding:0; margin:0; text-align:center;" width="48"/>
                                        </td>
                                        <td align="left" style="padding-bottom:40px;" valign="top">                                          
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                           Last 7 Days Performance.
                                          </p>
                                          
                                               <br />


                                          
                                            <table  cellpadding="0" cellspacing="0" class="ReadMsgBody">
                                                                                                <thead align="right" style="border-bottom:1px solid #e8e8e8">
                                                <tr>
                                                <th></th>
                                                <th style="padding-bottom:12px; text-transform:uppercase"></th>
                                                </tr>
                                                </thead>
                                                <tr style="border-bottom:1px solid #e8e8e8">
                                                    <td style="padding:19px 0 15px"><p style="color:#0da32c; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:normal; line-height:24px; padding-top:0; margin-top:0; text-align:left;">Picture ++winnerpoll++ Winner</p></td>
                                                    <td style="padding:19px 0 15px;text-align:right"><p style="color:#0da32c; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:normal; line-height:24px; padding-top:0; margin-top:0; text-align:right;">++winnerpollperc++</p></td>
                                                  </tr>
                                                <tr style="border-bottom:1px solid #e8e8e8">
                                                    <td style="padding:19px 0 15px"><p style="color:#0f68a1; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:normal; line-height:24px; padding-top:0; margin-top:0; text-align:left;">Polls Answered</p></td>
                                                    <td style="padding:19px 0 15px;text-align:right"><p style="color:#0f68a1; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:normal; line-height:24px; padding-top:0; margin-top:0; text-align:right;">++pollanswercount++</p></td>
                                                    </tr>
                                                
                                            </table>


                                          
                                        </td>
                                      </tr>

                                        <tr>
                                             <td align="left" style="padding-right:20px; padding-bottom:60px;" valign="top">

                                             </td>
                                        <td align="center" style="padding-right:20px; padding-bottom:60px;" valign="top" >
                                            <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:400; line-height:24px; padding:0; margin:0; text-align:left;">
Open Picture Poll Campaign from your dashboard  to view Charts for full analytics.

                                          </p>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" style="padding-right:20px; padding-bottom:10px;" valign="top" colspan="2">
                                                <table align="center" bgcolor="#F2F2F2" border="0" cellpadding="0" cellspacing="0" style='width:100%;'>
                      
                            <tr><td align="center" style="padding-top:20px"><img style='text-align:center;padding-top:20px;max-width:560px' src='++surveyleftimage++'/></td>
                                <td align="center" style="padding-top:20px"><img style='text-align:center;padding-top:20px;max-width:560px' src='++surveyrightimage++'/></td>
                            </tr>
                            <tr><td colspan='2' align="center" style='text-align:center;padding-bottom:2px;'><p style="style=color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;"><b>++campaignname++</b></p></td></tr>
                            
</table>
                        



                                            </td>
                                        </tr>
                                         <tr>                                        
                                        <td align="center" style="padding-right:20px; padding-bottom:60px;" valign="top" colspan="2">

                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">                                      
                                      <tr>                                        
                                        <td align="center" valign="middle">                                          
<!--[if gte mso 9]>
                                                                                    <table align="center" border="0" cellspacing="0" cellpadding="0" style="width:200px" width="200">
                                                                                    <tr>
                                                                                    <td align="center" bgcolor="#6DC6DD" style="padding:20px;" valign="top">
                                                                                    <![endif]-->
                                          <a href="http://manage.cash4ads.com/" style="background-color:#6DC6DD; border-collapse:separate; border-top:20px solid #6DC6DD; border-right:40px solid #6DC6DD; border-bottom:20px solid #6DC6DD; border-left:40px solid #6DC6DD; border-radius:3px; color:#FFFFFF; display:inline-block; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:600; letter-spacing:.3px; text-decoration:none;" target="_blank">Manage Your Campaigns</a>
<!--[if gte mso 9]>
                                                                                    </td>
                                                                                    </tr>
                                                                                    </table>
                                                                                    <![endif]-->
                                        </td>
                                      </tr>
                                    </table>
                                            </td>
                                            </tr>
                                      <tr>                                        
                                        <td align="left" style="padding-right:20px; padding-bottom:60px;" valign="top">                                          
                                          <img alt="Email" src="http://manage.cash4ads.com/emails/images/tip.png" style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:14px; font-weight:600; height:auto; letter-spacing:-1px; padding:0; margin:0; text-align:center;" width="48"/>
                                        </td>
                                        <td align="left" style="padding-bottom:40px;" valign="top">                                          
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                           Upload 2 Clear Contrasting Pictures
                                          </p>
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:400; line-height:24px; padding:0; margin:0; text-align:left;">
                                         Uplaod 2 images that will help you identify new customers


                                          </p>
                                        </td>
                                      </tr>
                                      <tr>                                        
                                        <td align="left" style="padding-right:20px; padding-bottom:60px;" valign="top">                                          
                                          <img alt="Test" src="http://manage.cash4ads.com/emails/images/tip.png" style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:14px; font-weight:600; height:auto; letter-spacing:-1px; padding:0; margin:0; text-align:center;" width="48"/>
                                        </td>
                                        <td align="left" style="padding-bottom:40px;" valign="top">                                          
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                          Use Your Poll Results To Re-Target Audiences
                                          </p>
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:400; line-height:24px; padding:0; margin:0; text-align:left;">
                                          Send different video ads to people who preferred one image over the other



                                          </p>
                                        </td>
                                      </tr>
                                        <tr>
                                            <td align="left" style="" valign="top" colspan="2">
                                                <p style="color:#0f68a1; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                                    You may also want to... 
                                                </p>

                                            </td>
                                        </tr>
                                          <tr>                                        
                                      
                                        <td align="left" style="padding-bottom:40px;" valign="top" colspan="2">                                          
                                            <img src="http://manage.cash4ads.com/emails/images/Email_Deal_Ad.png" />


                                            





                                        </td>
                                      </tr>
                                 
                                    </table>
                                  </td>
                                </tr>
                                <tr>                                  
                                  <td align="center" style="padding-bottom:10px;" valign="top">                                    
                                   &nbsp;
                                  </td>
                                </tr>
                                <tr>                                  
                                  <td align="center" style="padding-bottom:60px;" valign="top">                                    
                                    <table align="center" bgcolor="#F2F2F2" border="0" cellpadding="0" cellspacing="0" style="background-color:#F2F2F2; border-collapse:separate; border-radius:4px;" width="100%">                                      
                                      <tr>                                        
                                        <td align="center" style="padding:20px;" valign="middle">                                          
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                            Need Additional Support?
                                          </p>
                                          <p style="color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:400; line-height:24px; padding-top:0; margin-top:0; text-align:left;">
                                            Free accounts come with 30 days of email support, and all paid accounts have access to email and live chat support.
                                          </p>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
<!--[if gte mso 9]>
                                                </td>
                                                </tr>
                                                </table>
                                                <![endif]-->
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>                
                <td align="center" id="templateFooter" style="padding-right:30px; padding-left:30px;" valign="top">                  
<!--[if gte mso 9]>
                                    <table align="center" border="0" cellspacing="0" cellpadding="0" width="640">
                                    <tr>
                                    <td align="center" valign="top" width="640">
                                    <![endif]-->
                  <table align="center" border="0" cellpadding="0" cellspacing="0" class="emailContainer" style="max-width:640px;" width="100%">                    
                    <tr>                      
                      <td align="center" valign="top">                        
                        <table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">                          
                          <tr>                            
                            <td align="center" style="padding-bottom:20px;" valign="top">                              
<!--[if mso]>
                                                            <table align="center" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                            <td align="center" valign="top">
                                                            <![endif]-->
                              <table align="left" border="0" cellpadding="0" cellspacing="0" style="display:inline; border-collapse:collapse;">                                
                                <tr>                                  
                                  <td align="center" style="padding-right:20px; padding-bottom:10px;" valign="top">                                    
                                    <a href="https://www.facebook.com/Cash4ads-567555380070390/" target="_blank"> 
                                      <img alt="Facebook" src="http://manage.cash4ads.com/emails/images/color-facebook-96.png" style="border:0; display:block; height:auto; max-width:48px; outline:none; text-decoration:none; width:48px; -ms-interpolation-mode: bicubic;" width="48"/></a>
                                  </td>
                                </tr>
                              </table>
<!--[if mso]>
                                                            </td>
                                                            <td align="center" valign="top">
                                                            <![endif]-->
                              <table align="left" border="0" cellpadding="0" cellspacing="0" style="display:inline; border-collapse:collapse;">                                
                                <tr>                                  
                                  <td align="center" style="padding-right:20px; padding-bottom:10px;" valign="top">                                    
                                    <a href="https://twitter.com/cash4adsnow" target="_blank"> 
                                      <img alt="Twitter" src="http://manage.cash4ads.com/emails/images/color-twitter-96.png" style="border:0; display:block; height:auto; max-width:48px; outline:none; text-decoration:none; width:48px; -ms-interpolation-mode: bicubic;" width="48"/></a>
                                  </td>
                                </tr>
                              </table>
<!--[if mso]>
                                                            </td>
                                                            <td align="center" valign="top">
                                                            <![endif]-->
                              <table align="left" border="0" cellpadding="0" cellspacing="0" style="display:inline; border-collapse:collapse;">                                
                                <tr>                                  
                                  <td align="center" style="padding-right:20px; padding-bottom:10px;" valign="top">                                    
                                    <a href="https://www.instagram.com/Cash4AdsNow/" target="_blank"> 
                                      <img alt="Instagram" class="mcnFollowBlockIcon" src="http://manage.cash4ads.com/emails/images/color-instagram-96.png" style="border:0; display:block; height:auto; max-width:48px; outline:none; text-decoration:none; width:48px; -ms-interpolation-mode: bicubic;" width="48"/></a>
                                  </td>
                                </tr>
                              </table>
<!--[if mso]>
                                                            </td>
                                                            <td align="center" valign="top">
                                                            <![endif]-->
                              <table align="left" border="0" cellpadding="0" cellspacing="0" style="display:inline; border-collapse:collapse;">                                
                                <tr>                                  
                                  <td align="center" style="padding-right:0; padding-bottom:10px;" valign="top">                                    
                                    <a href="https://www.linkedin.com/company/cash-4-ads" target="_blank"> 
                                      <img alt="LinkedIn" src="http://manage.cash4ads.com/emails/images/color-linkedin-96.png" style="border:0; display:block; height:auto; max-width:48px; outline:none; text-decoration:none; width:48px; -ms-interpolation-mode: bicubic;" width="48"/></a>
                                  </td>
                                </tr>
                              </table>
<!--[if mso]>
                                                            </td>
                                                            </tr>
                                                            </table>
                                                            <![endif]-->
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>                      
                      <td id="footerContent" style="color:#B7B7B7; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:12px; font-weight:400; line-height:24px; padding-bottom:20px; text-align:center;" valign="top">                        
                        <p style="color:#B7B7B7; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:12px; font-weight:400; line-height:24px; padding:0; margin:0; text-align:center;">
                          &copy; 2017 Clydy Ltd.
                          <sup>
                            &reg;
                          </sup>, All Rights Reserved.<br/>67 Hurworth Avenue  &bull; Slough, Berkshire &bull;  United Kingdom SL3 7FF
                        </p>
                        <a class="utilityLink" href="http://cash4ads.com" style="color:#B7B7B7; text-decoration:underline;" target="_blank">Contact Us</a><span class="mobileHide"> &nbsp; &bull; &nbsp; </span><a class="utilityLink" href="http://cash4ads.com/terms-of-use/" style="color:#B7B7B7; text-decoration:underline;" target="_blank">Terms of Use</a><span class="mobileHide"> &nbsp; &bull; &nbsp; </span><a class="utilityLink" href="http://cash4ads.com/privacy-policy/" style="color:#B7B7B7; text-decoration:underline;" target="_blank">Privacy Policy</a>
                      </td>
                    </tr>
                  </table>
<!--[if gte mso 9]>
                                    </td>
                                    </tr>
                                    </table>
                                    <![endif]-->
                </td>
              </tr>
            </table>
<!-- // END TEMPLATE -->
          </td>
        </tr>
      </table>
    </center>
    <img height="1" src="" width="1" />
</body>
</html>
