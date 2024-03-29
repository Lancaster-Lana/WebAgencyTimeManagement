﻿using System;
using System.Diagnostics;
using System.Timers;
using System.Configuration;
using System.ServiceProcess;using System.Net.Mail;
using Agency.PaidTimeOffBLL.Framework;

namespace EmailService
{
    public partial class EmailService : ServiceBase
    {
        private const string APP_CONFIG_TIMER_INTERVAL = "TimerInterval";
        private const string APP_CONFIG_SMTP_SERVER = "SMTPServer";
        private const string APP_CONFIG_ENT_USER_ACCOUNT_ID = "ENTUserAccountId";

        private Timer _emailTimer;        
        private int _entUserAccountId;
        private bool _processing;

        public EmailService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {                        
            //Get the user account id that should be used to update the record.
            string entUserAccountId = ConfigurationManager.AppSettings[APP_CONFIG_ENT_USER_ACCOUNT_ID];

            if ((entUserAccountId == "") || (int.TryParse(entUserAccountId, out _entUserAccountId) == false))
            {
                //Log an event to the event log
                var ev = new EventLog();
                ev.Source = "AgencyEmailService";
                ev.WriteEntry("The ENTUserAccountId must be configured in the application configuration file before starting this service.  " +
                              "This value should be set to the valid ENTUserAccountId in the ENTUserAccount table which will be used to update the email record after it has been sent.", EventLogEntryType.Error);
             
                //Stop the service
                this.Stop();
            }
                        
            //Instantiate a timer.
            _emailTimer = new Timer();

            //Check if the timer interval has been set.
            string timerInterval = ConfigurationManager.AppSettings[APP_CONFIG_TIMER_INTERVAL];
                        
            if (timerInterval != "")
            {
                _emailTimer.Interval = Convert.ToDouble(timerInterval);
            }
            else
            {
                //Default to 60 seconds
                _emailTimer.Interval = 60000;
            }

            //Hook the Elapsed event to the event handler
            _emailTimer.Elapsed += _emailTimer_Elapsed;

            //Start the timer.
            _emailTimer.Enabled = true;            
        }

        void _emailTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_processing)
            {
                _processing = true;
                try
                {
                    //Check if there are any emails that need to be sent
                    var emails = new ENTEmailEOList();
                    emails.LoadUnsent();
            
                    if (emails.Count != 0)
                    {
                        var validationErrors = new ENTValidationErrors();

                        //if there are then send one at a time
                        var client = new SmtpClient();                    
                        foreach (ENTEmailEO email in emails)
                        {
                            var message = new MailMessage();
                            message.From = new MailAddress(email.FromEmailAddress);
                            AddAddresses(email.ToEmailAddress, message.To);
                            AddAddresses(email.CCEmailAddress, message.CC);
                            AddAddresses(email.BCCEmailAddress, message.Bcc);
                            
                            message.Subject = email.Subject;
                            message.Body = email.Body;
                            message.IsBodyHtml = true;

                            client.Send(message);

                            //Update record after the email is sent
                            email.EmailStatusFlag = ENTEmailEO.EmailStatusFlagEnum.Sent;
                            if (!email.Save(ref validationErrors, _entUserAccountId))
                            {
                                foreach (ENTValidationError ve in validationErrors)
                                {
                                    var ev = new EventLog();
                                    ev.Source = "EmailService";
                                    ev.WriteEntry(ve.ErrorMessage, EventLogEntryType.Error);
                                }
                            }
                        }
                    }
                    _processing = false;
                }
                catch (Exception exception)
                {
                    _processing = false;
                    var ev = new EventLog();
                    ev.Source = "EmailService";
                    ev.WriteEntry(exception.Message, EventLogEntryType.Error);
                }                
            }            
        }

        private void AddAddresses(string emailAddresses, MailAddressCollection mailAddressCollection)
        {
            if (emailAddresses != null)
            {
                string[] addresses = emailAddresses.Split(new[] { ';' });

                foreach (string address in addresses)
                {
                    mailAddressCollection.Add(address);
                }
            }
        }

        protected override void OnStop()
        {
            _emailTimer.Stop();            
        }
    }
}
