using DataAccess.Abstract;
using Entities.Dtos;
using FluentEmail.Core;

namespace DataAccess.Concrete.EntityFramework;

public class EfMailDal : IMailDal
{
    private readonly IFluentEmail _fluentEmail;

    public EfMailDal(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public void SendMail(SendMailDto sendMailDto)  //otomatik mail göndermek içi  bu işlemi yapıyoruz.
    {
        _fluentEmail.To(sendMailDto.email).Subject(sendMailDto.subject).Body(sendMailDto.body).Send();
        //using (MailMessage mail = new MailMessage())
        //{
        //    mail.From = new MailAddress(sendMailDto.mailParameter.Email);
        //    mail.To.Add(sendMailDto.email);
        //    mail.Subject = sendMailDto.subject;
        //    mail.Body = sendMailDto.body;
        //    mail.IsBodyHtml = true;
        //    //mail.Attachments.Add();

        //    using (SmtpClient smtp = new SmtpClient(sendMailDto.mailParameter.SMTP))
        //    {
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new NetworkCredential(sendMailDto.mailParameter.Email, sendMailDto.mailParameter.Password);
        //        smtp.EnableSsl = sendMailDto.mailParameter.SSL;
        //        //smtp.Port = sendMailDto.mailParameter.Port;


        //        smtp.Send(mail);
        //    }
        //}
    }
}
