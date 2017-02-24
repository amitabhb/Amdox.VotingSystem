//-----------------------------------------------------------------------
// <copyright file="Common.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amdox.Common
{
    public static class Common
    {
        public static class ConfigurationSettings
        {

        }

        public static class EmailText
        {
            #region email text
            
            #region Partner Vote Open
            public static string OpenVoteSubject = "Voting System:{0}";
            public static string OpenVoteEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>"+
                "</td></tr>"+
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" +
                "<p>Please<a href='{0}'> Click here</a>" +
            " to view resolution(s) and documents in relation to the above.</p>" +
            "<p>Your vote must be submitted before close on {1}</p>" +
           "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk</p>" +
            "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region Partner Vote Publish
            public static string PublishVoteSubject = "Voting System:{0}";
            public static string PublishVoteEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" +
                "<p>Please<a href='{0}'> Click here</a>" +
            " to view resolution(s) and documents in relation to the above.</p>" +
            "<p>Your vote must be submitted before close on {1}</p>" +
           "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk.</p>" +
             "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region Notify Partner & Admin: Proxy Assigned
            public static string NotifyPartnerSubject = "Voting System: You have assigned a Proxy";
            public static string NotifyPartnerEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" +
                "<p>This email is to confirm you have appointed {0} as proxy.</p>" +
           "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk</p>" +
             "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region Notify proxy:proxy Assigned
            public static string NotifyProxySubject = "Voting System: You have been assigned as a Proxy - {0}";
            public static string NotifyProxyEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" +
                "<p>Pursuant to Schedule 2 of the Members Agreement,you have been appointed as proxy to speak and vote on behalf of {0}.</P>" +
           "<p>You will find details of how to vote on behalf of proxies in the 'Vote as Proxy' section of the <a href='{1}'>Partner Voting site</a></p>" +
           "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk</p>" +
             "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region Notify proxy: Proxy removed
            public static string ProxyRemovedNotifySubject = "Voting System: You have been removed as Proxy by {0}";
            public static string ProxyRemovedNotifyEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" +
                "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk</p>" +
            "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region Proxy has Voted
            public static string ProxyVotedSubject = "Voting System: Your Proxy has voted for {0}";
            public static string ProxyVotedEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>" + 
                "<p>As requested, this email is to confirm {0} has voted on your behalf </p>"+
           "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #region remind partner
            public static string PartnerReminderSubject = "Voting System:Reminder - {0}";
            public static string PartnerReminderBody ="<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>"
                + "<p>Please <a href='{0}'>click here</a> to view resolution(s) and documents in relation to the above.</p>" +
               "<p>Your vote must be submitted before close on {1}.</p>" +
            "<p>If you choose to appoint a proxy, they will receive a notification to vote on your behalf.</p>" +

         "<p>If you have any questions, please contact amitabh.barua@amdox.com.uk</p>" +
             "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion


            #region Partner Vote outcome published
            public static string PublishVoteOutcomeSubject = "Voting System:Partner Voting now Closed - {0}";
            public static string PublishVoteOutcomeEmailBody = "<table style='width:100%; margin-left:23%;'><tbody><tr>" +
                "<td style='width:50%; background-color:#00949e; color:#ffffff;font-size:14px;padding:15px;' align='left'>" +
                "<img scr='~/Images/CRS_horizontal.jpg' width:80px height='40px'/>" +
                "</td></tr>" +
                "<td style='width:50%; background-color:#D8D8D8;color:#000;font-size:17px;padding:15px;'>" +
                "<p><h4>Hi,</h4></p>"+
                "<p>Please note vote in relation to the above is closed.</p>" +
           "<p>To view details of previous votes, please visit 'View Results' pages of <a href='{0}'>Partner Voting site </a></p>" +
             "<p><h4>Many Thanks</h4> </p>" +
           "</td>" +
           "</tr>" +
           "<tr style='width:100%;'>" +
           "<td></td>" +
           "</tr>" +
            "</tbody></table>";
            #endregion

            #endregion

        }

        public static class FieldName
        {
            #region FieldNameConstants
            public static string OrgCookieName = "AdxOrgId";
            #endregion
        }
    }
}
