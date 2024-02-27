
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Web;
using AqaratProject.Helpers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AqaratProject.Services
{

    public class SMSService : ISMSService
    {
        private readonly TwilioSettings _twilio;

        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }

        public MessageResource Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            var result = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: mobileNumber
                );

            return result;
        }
    }




}