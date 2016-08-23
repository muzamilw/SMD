<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTester.aspx.cs" Inherits="SMD.MIS.Views.EmailTester" %>

<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<style type="text/css">
    @media only screen and (max-width:480px) {
        body, table, td, p, a, li, blockquote {
            -webkit-text-size-adjust: none !important;
        }

        body {
            width: 100% !important;
            min-width: 100% !important;
        }

        td[id=bodyCell] {
            padding: 10px !important;
        }

        table.kmMobileHide {
            display: none !important;
        }

        table[class=kmTextContentContainer] {
            width: 100% !important;
        }

        table[class=kmBoxedTextContentContainer] {
            width: 100% !important;
        }

        td[class=kmImageContent] {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        img[class=kmImage] {
            width: 100% !important;
        }

        table[class=kmSplitContentLeftContentContainer], table[class=kmSplitContentRightContentContainer], table[class=kmColumnContainer], td[class=kmVerticalButtonBarContentOuter] table[class=kmButtonBarContent], td[class=kmVerticalButtonCollectionContentOuter] table[class=kmButtonCollectionContent], table[class=kmVerticalButton], table[class=kmVerticalButtonContent] {
            width: 100% !important;
        }

        td[class=kmButtonCollectionInner] {
            padding-left: 9px !important;
            padding-right: 9px !important;
            padding-top: 9px !important;
            padding-bottom: 0 !important;
            background-color: transparent !important;
        }

        td[class=kmVerticalButtonIconContent], td[class=kmVerticalButtonTextContent], td[class=kmVerticalButtonContentOuter] {
            padding-left: 0 !important;
            padding-right: 0 !important;
            padding-bottom: 9px !important;
        }

        table[class=kmSplitContentLeftContentContainer] td[class=kmTextContent], table[class=kmSplitContentRightContentContainer] td[class=kmTextContent], table[class=kmColumnContainer] td[class=kmTextContent], table[class=kmSplitContentLeftContentContainer] td[class=kmImageContent], table[class=kmSplitContentRightContentContainer] td[class=kmImageContent] {
            padding-top: 9px !important;
        }

        td[class="rowContainer kmFloatLeft"], td[class="rowContainer kmFloatLeft firstColumn"], td[class="rowContainer kmFloatLeft lastColumn"] {
            float: left;
            clear: both;
            width: 100% !important;
        }

        table[id=templateContainer], table[class=templateRow] {
            max-width: 600px !important;
            width: 100% !important;
        }

        h1 {
            font-size: 24px !important;
            line-height: 130% !important;
        }

        h2 {
            font-size: 20px !important;
            line-height: 130% !important;
        }

        h3 {
            font-size: 18px !important;
            line-height: 130% !important;
        }

        h4 {
            font-size: 16px !important;
            line-height: 130% !important;
        }

        td[class=kmTextContent] {
            font-size: 14px !important;
            line-height: 130% !important;
        }

        td[class=kmTextBlockInner] td[class=kmTextContent] {
            padding-right: 18px !important;
            padding-left: 18px !important;
        }

        table[class="kmTableBlock kmTableMobile"] td[class=kmTableBlockInner] {
            padding-left: 9px !important;
            padding-right: 9px !important;
        }

            table[class="kmTableBlock kmTableMobile"] td[class=kmTableBlockInner] [class=kmTextContent] {
                font-size: 14px !important;
                line-height: 130% !important;
                padding-left: 4px !important;
                padding-right: 4px !important;
            }
    }
</style>
<center>
    <table align="center" border="0" cellpadding="0" cellspacing="0" id="bodyTable" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;padding:0;background-color:#E9E9E9;height:100%;margin:0;width:100%" width="100%">
        <tbody>
            <tr>
                <td align="center" id="bodyCell" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;padding-top:50px;padding-left:20px;padding-bottom:20px;padding-right:20px;border-top:0;height:100%;margin:0;width:100%" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" id="templateContainer" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;border:0 solid #aaa;background-color:#FFF;border-radius:10px" width="600">
                        <tbody>
                            <tr>
                                <td id="templateContainerInner" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;padding:0">
                                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" class="templateRow" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td class="rowContainer kmFloatLeft" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" valign="top">
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="kmTextBlock" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                        <tbody class="kmTextBlockOuter">
                                                                            <tr>
                                                                                <td class="kmTextBlockInner" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;" valign="top">&nbsp;</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>

                                                                    <table border="0" cellpadding="0" cellspacing="0" class="kmTextBlock" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                        <tbody class="kmTextBlockOuter">
                                                                            <tr>
                                                                                <td class="kmTextBlockInner" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;" valign="top">
                                                                                    <table align="left" border="0" cellpadding="0" cellspacing="0" class="kmTextContentContainer" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="kmTextContent" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;color:#505050;font-family:Helvetica, Arial;font-size:14px;line-height:150%;text-align:left;padding-top:9px;padding-bottom:9px;padding-left:18px;padding-right:18px;" valign="top">
                                                                                                    <p style="margin:0;padding-bottom:0;text-align: center;"><img 150="" src="width=" /></p>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" class="templateRow" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td class="rowContainer kmFloatLeft" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" valign="top">
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="kmTextBlock" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                        <tbody class="kmTextBlockOuter">
                                                                            <tr>
                                                                                <td class="kmTextBlockInner" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;" valign="top">
                                                                                    <table align="left" border="0" cellpadding="0" cellspacing="0" class="kmTextContentContainer" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="kmTextContent" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;color:#505050;font-family:Helvetica, Arial;font-size:14px;line-height:150%;text-align:left;font-size:30px;color:#FFFFFF;padding-bottom:9px;text-align:center;padding-right:18px;padding-left:18px;padding-top:9px;line-height:200%;background-color:#0B5280;font-family:Arial;" valign="top">Advertiser invitation</td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>

                                                                    <table border="0" cellpadding="0" cellspacing="0" class="kmTextBlock" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                        <tbody class="kmTextBlockOuter">
                                                                            <tr>
                                                                                <td class="kmTextBlockInner" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;" valign="top">
                                                                                    <table align="left" border="0" cellpadding="0" cellspacing="0" class="kmTextContentContainer" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="kmTextContent" style="border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;color:#505050;font-family:Helvetica, Arial;font-size:14px;line-height:150%;text-align:left;padding-top:9px;padding-bottom:9px;padding-left:18px;padding-right:18px;" valign="top">
                                                                                                    <p style="margin:0;padding-bottom:1em">&nbsp;</p>

                                                                                                    <p style="margin:0;padding-bottom:1em;margin: 0px; font-size: 12px; line-height: normal; font-family: Helvetica;"><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif">Hey ++username++,</span></span></p>

                                                                                                    <p style="margin:0;padding-bottom:1em;margin: 0px; font-size: 12px; line-height: normal; font-family: Helvetica;"><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif">
                                                                                                        ++email++ has invited you to Join Crowdcents to&nbsp; Advertise for free<br />

                                                                                                        Click  <a href="++inviteurl++"> here </a> to register and start advertising
                                                                                                        </span></span></p>



                                                                                                    <p style="margin:0;padding-bottom:1em"><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif"><span style="line-height:1.6em">Regards,</span></span></span></p>



                                                                                                    <p style="margin:0;padding-bottom:1em"><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif">Team Cash4Ads</span></span></p>


                                                                                                    <div>&nbsp;</div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</center>


