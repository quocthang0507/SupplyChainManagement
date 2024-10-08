﻿using ApplicationCore.Interfaces;
using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private SendGridOptions _sendGridOptions { get; }
        private IFunctional _functional { get; }
        private SmtpOptions _smtpOptions { get; }

        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
            IFunctional functional,
            IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _functional = functional;
            _smtpOptions = smtpOptions.Value;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            if (_sendGridOptions.IsDefault)
            {
                _functional.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey,
                                                    _sendGridOptions.FromEmail,
                                                    _sendGridOptions.FromFullName,
                                                    subject,
                                                    message,
                                                    email)
                                                    .Wait();
            }

            if (_smtpOptions.IsDefault)
            {
                _functional.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                                            _smtpOptions.fromFullName,
                                            subject,
                                            message,
                                            email,
                                            email,
                                            _smtpOptions.smtpUserName,
                                            _smtpOptions.smtpPassword,
                                            _smtpOptions.smtpHost,
                                            _smtpOptions.smtpPort,
                                            _smtpOptions.smtpSSL)
                                            .Wait();
            }


            return Task.CompletedTask;
        }
    }
}
