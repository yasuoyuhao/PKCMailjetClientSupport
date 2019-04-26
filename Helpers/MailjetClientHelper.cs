using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace MailjetClientSupport.Helpers
{
    public static class MailjetClientHelper
    {
        public static async Task GenerateClient(string publicKey, string privateKey)
        {
            Mailjet.Client.MailjetClient client = new Mailjet.Client.MailjetClient(publicKey, privateKey)
            {
                Version = ApiVersion.V3_1,
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "services@klearthink.com"},
                  {"Name", "Klearthink Services"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", "yasuoyuhao@gmail.com"},
                   {"Name", "yasuoyuhao"}
                   }
                  }},
                 {"Subject", "測試中英文 hi, test"},
                 {"TextPart", "測試中英文 hi, test"},
                 {"HTMLPart", "<h3>測試中英文 hi, test</h3><br />May the delivery force be with you!"}
                 }
                });

            MailjetResponse response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
