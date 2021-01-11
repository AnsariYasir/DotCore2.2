using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class Helper
    {
        public static T HandleDBNull<T>(object value)
        {
            try
            {
                T result;
                result = value == DBNull.Value ? default(T) : (T)value;
                return result;
            }
            catch (InvalidCastException cex)
            {
                // logger.ErrorFormat("Invalid cast while mapping db value '{0}' to type {1}. Error: {2}", value, typeof(T).Name, cex);
                throw new InvalidCastException(string.Format("Invalid cast while mapping db value '{0}' to type {1}. Error: {2}", value, typeof(T).Name, cex.Message));
            }
        }

        public static string GetAppsettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static void SendEmail(string emailaddress, string newpassword)
        {

            try
            {

                using (MailMessage msg = new MailMessage())
                {
                    msg.From = new MailAddress(GetAppsettingValue("FromEmailAddress"));
                    msg.To.Add(emailaddress);
                    msg.Subject = "Password Recovery";
                    msg.Body = "Please take down :-\n" + "Email address = " + emailaddress + "\n" + "Password = " + newpassword;



                    SmtpClient smt = new SmtpClient();
                    smt.Host = GetAppsettingValue("SMTPHost");
                    System.Net.NetworkCredential ntcd = new NetworkCredential();
                    ntcd.UserName = GetAppsettingValue("EmailUserName");
                    ntcd.Password = GetAppsettingValue("EmailPassword");
                    smt.Credentials = ntcd;
                    smt.EnableSsl = true;
                    smt.Port = 587;
                    smt.Send(msg);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
