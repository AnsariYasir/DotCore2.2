using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class EmailHelper
    {

        //public EmailHelper()
        //{
        //    emailsending();
        //}




        public void emailsending(string emailaddress, string newpassword)
        {
            // string[] files=null;
            try
            {

                using (MailMessage msg = new MailMessage())
                {
                    msg.From = new MailAddress("c3pro7@gmail.com");
                    msg.To.Add(emailaddress);
                    msg.Subject = "Password Recovery";
                    msg.Body = "Please take down :-\n" + "Email address = " + emailaddress + "\n" + "Password = " + newpassword;


                    //    msg.Body = "Please take down you email address=" + emailaddress + "+ password=" + newpassword;


                    //if (files.Count() > 0)
                    //{
                    //    foreach (string a in files)
                    //    {
                    //        msg.Attachments.Add(new Attachment(a));
                    //        //  Thread.Sleep(1000);
                    //    }
                    //}


                    SmtpClient smt = new SmtpClient();
                    smt.Host = "smtp.gmail.com";
                    System.Net.NetworkCredential ntcd = new NetworkCredential();
                    ntcd.UserName = "c3pro7";
                    ntcd.Password = "octaware";
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
