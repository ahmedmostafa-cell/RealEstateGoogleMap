

using System;
using Twilio.Rest.Api.V2010.Account;

namespace AqaratProject.Services
{
    public interface ISMSService
    {
        MessageResource Send(string mobileNumber, string body);
    }
}