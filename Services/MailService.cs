using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Repositories.Models;

namespace Services
{
    public class MailService
    {
        private const int LIMIT_MINUS = 10;
        private const string FromEmail = "baviapartment88@gmail.com";
        private const string Password = "nong aqji krlu xvue"; // Không nên để mật khẩu trực tiếp trong code
        private const string verifyAccountURL = "http://localhost:6969/VerifyPRN/verify-account";
        private const string resetPasswordURL = "http://localhost:8080/VerifyPRN/reset-password";
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static DateTime ExpireDateTime()
        {
            return DateTime.Now.AddMinutes(LIMIT_MINUS);
        }

        public static bool IsExpired(DateTime time)
        {
            return DateTime.Now > time;
        }

        public static void SendEmailResetPass(string email, int cid)
        {
            string token = GenerateToken();

            TokenForgetPassword tokenForgetPassword = new TokenForgetPassword() { IsUsed = false, ExpiryTime = ExpireDateTime(), Cid = cid, Token = token };
            TokenService tokenService = new TokenService();
            tokenService.AddToken(tokenForgetPassword);
            string link = resetPasswordURL + "?id=" + cid + "&token=" + token;
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(FromEmail, Password);
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(FromEmail);
                    mail.To.Add(email);
                    mail.Subject = "Reset Password";
                    mail.Body = "<h2>Vào link sau để reset mật khẩu:  </h2>" + link;
                    mail.IsBodyHtml = true;

                    client.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }



        public static void SendEmailVerify(string email,int cid)
        {
            string token = GenerateToken();

            TokenForgetPassword tokenForgetPassword = new TokenForgetPassword() {IsUsed= false,ExpiryTime= ExpireDateTime(), Cid=cid,Token=token };
            TokenService tokenService = new TokenService(); 
            tokenService.AddToken(tokenForgetPassword);
            string link = verifyAccountURL+"?id="+cid+"&token=" + token;
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(FromEmail, Password);
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(FromEmail);
                    mail.To.Add(email);
                    mail.Subject = "Verify Account";
                    mail.Body = "<h2>Vào link sau để xác nhận tài khoản:  </h2>" + link;
                    mail.IsBodyHtml = true;

                    client.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
        public static void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(FromEmail, Password);
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(FromEmail);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    client.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
