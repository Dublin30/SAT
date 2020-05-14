using SAT.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SAT.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //when a class has validation attributes, that validation should be
            //checked BEFORE attempting to process any data.
            if (!ModelState.IsValid)
            //if(ModelState.IsValid == false)
            {
                //send them back to the form, by passing the object to the view,
                //the form returns with the original populated information.
                return View(cvm);
            }
            //Only executes if the form (object) passes model validation
            //build the Message - what we see when we receive the email
            string message = $"You have received an email from {cvm.Name} with a subject " +
            $"{cvm.Message}. Please respond to {cvm.EmailAdress} with your response to " +
            $"the following message: <br />{cvm.Message}";

            //MailMessage (what sends the email) - System.Net.Mail
            MailMessage mm = new MailMessage(
            //FROM
            "noreply@quintindublin.com",
            //TO - this assumes forwarding by the host
            //"you@yourdomain.ext",//could be you@gmail.com or whatever addy you wish
            "QDublin@outlook.com",//hard code until SmarterASP works around the forwarding issue
                                  //SUBJECT
            "From Website: " + cvm.Message,
            //BODY
            message);

            //MailMessage properties
            //Allow HTML formatting in the email (message has HTML in it)
            mm.IsBodyHtml = true;
            //if you want to mark these emails with high priority
            mm.Priority = MailPriority.High;//the default is Normal
                                            //respond to the sender's email instead our own SMTP Client (webmail)
            mm.ReplyToList.Add(cvm.EmailAdress);

            //SmtpClient - This is the information from the HOST (smarterAsp.net)
            //that allows the email to actually be sent.
            SmtpClient client = new SmtpClient("mail.quintindublin.com");

            //client credentials (smarterASP requires your user name and password)
            client.Credentials = new NetworkCredential("noreply@quintindublin.com",
            "P@ssw0rd");

            //It is possible that the mailserver is down or we may have configuration
            //issues, so we want to encapsulate our code in a try/catch
            try
            {
                //attempt to send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage =
                $"We're sorry your request could not be completed at this time." +
                $" Please try again later. Error Message: <br /> {ex.Message}";
                return View(cvm);
                //return the view WITH the entire message so that users can copy/paste
                //it for later.
            }
            //if all goes will return a view that displays a confirmation to the end user
            //that the email was sent.
            return View("EmailConfirmation", cvm);
        }
    }
}