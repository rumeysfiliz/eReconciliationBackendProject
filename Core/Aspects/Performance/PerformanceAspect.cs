﻿using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            //_fluentEmail = fluentEmail;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();

        }
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}";
                SendConfirmEmail(body);
            }
            _stopwatch.Reset();
        }
        void SendConfirmEmail(string body)
        {
            string subject = "Performans Maili";

            //_fluentEmail.To("s.ior@hotmail.com").Subject(subject).Body(body).Send();

            //SendMailDto sendMailDto = new SendMailDto()
            //{
            //    Email = "s.ior@hotmail.com",
            //    Password = "Changeme123",
            //    Port = 587,
            //    SMTP = "smtp-mail.outlook.com",
            //    SSL = true,
            //    email = "s.ior@hotmail.com",
            //    subject = subject,
            //    body = body

            //};



            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(sendMailDto.Email);
            //    mail.To.Add(sendMailDto.email);
            //    mail.Subject = sendMailDto.subject;
            //    mail.Body = sendMailDto.body;
            //    mail.IsBodyHtml = true;
            //    //mail.Attachments.Add();

            //    using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
            //    {
            //        smtp.UseDefaultCredentials = false;
            //        smtp.Credentials = new NetworkCredential(sendMailDto.Email, sendMailDto.Password);
            //        smtp.EnableSsl = sendMailDto.SSL;
            //        //smtp.Port = sendMailDto.mailParameter.Port;

            //        smtp.Send(mail); //burda da hata dönüyor genelde 
            //    }
            //}

        }

    }
}
