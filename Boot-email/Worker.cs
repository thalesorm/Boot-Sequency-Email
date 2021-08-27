using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Boot_email
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                    var emails = new StreamReader("C:\\emauiks\\eamil.txt");
                  //  var emails = "medtechcell@gmail.com, tawistore@gmail.com";
                    mail.From = new MailAddress("seuemail@provedor.com");
                    foreach(var email in emails.ReadLine().Split(','))
                    {
                        mail.To.Add(email);
                    }
                    mail.Subject = "ASSUNTO DO EMAIL";
                    mail.Body = "CORPO DO EMAIL";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("seuemail@provedor.com", "senha");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch(Exception e)
                {
                    _logger.LogInformation("Erro: {Erro} {time}", e, DateTimeOffset.Now);
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
