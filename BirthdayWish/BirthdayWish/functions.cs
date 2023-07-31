using Aspose.Cells;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WhatsappBusiness.CloudApi;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.Messages.Requests;

namespace BirthdayWish
{
    public static class functions
    {
        private static WhatsAppBusinessCloudApiConfig whatsAppConfig;

        //kill all excel instances
        public static void KillExcelInstances()
        {
            try
            {
                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        p.Kill();
                    }
                }
            }

            catch (Exception)
            {

            }
        }

        // Read Data into object

        public static bool ReadExcelToJson(string excelPath, string outputPath)
        {
            bool success = false;

            try
            {
                var workbook = new Workbook(excelPath);
                workbook.Save(outputPath);
                success = true;

            }
            catch (Exception)
            {

            }
            return success;

        }

        public static List<BirthdayDetails> ReadDataIntoObject(string jsonFilepath)
        {
            List<BirthdayDetails> birthdayDetail = new List<BirthdayDetails>();
            try
            {
                string jsonString = File.ReadAllText(jsonFilepath);
                birthdayDetail = JsonConvert.DeserializeObject<List<BirthdayDetails>>(jsonString);
            }
            catch (Exception)
            {

            }
            return birthdayDetail;


        }

        //get birth date

        public static List<BirthdayDetails> Date(List<BirthdayDetails> birthdayDetails)
        {
            List<BirthdayDetails> Dates = new List<BirthdayDetails>();
            try
            {

                foreach (var date in birthdayDetails)
                {
                    DateTime parsedDate = DateTime.ParseExact(date.DateOfBirth, "dd/MM/yyyy", null);

                    if (parsedDate == DateTime.Today)
                    {
                        Dates.Add(date);
                    }

                }
            }

            catch (Exception)
            {
            }
            return Dates;


        }


        //Send mail
        public static void SendMail(List<BirthdayDetails> Dates)
        {

            string receiverMail = "";
            string messageSubject = "";
            string messageBody = "";

            try
            {


                foreach (var item in Dates)
                {
                    receiverMail = item.Email;
                    messageBody = item.Message;
                    messageSubject = item.Subject;
                    //dateTime = item.DateOfBirth;


                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("aklikaheugene@gmail.com");
                    message.Body = messageBody;
                    message.Subject = messageSubject;
                    message.To.Add(receiverMail);

                    //Configure SMTP client

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("aklikaheugene@gmail.com", "rwiqdlcgyapwtfml"),
                        EnableSsl = true
                    };

                    smtpClient.Send(message);
                }

            }

            catch (Exception)
            {

            }
        }

        //SEND SMS
        public static void SendSMS(List<BirthdayDetails> Dates)
        {

            try
            {
                foreach (var items in Dates)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://deywuro.com/api/sms");
                    var content = new StringContent("{\r\n    \"username\": \"ECLGLOBAL\",\r\n    \"password\": \"123654\",\r\n    \"destination\": \"" + items.Contacts + "\",\r\n    \"source\": \"ECLGLOBAL\",\r\n    \"message\": \"" + items.Message + "\"\r\n}", null, "application/json");
                    request.Content = content;
                    var response = client.SendAsync(request).Result;
                    //response.EnsureSuccessStatusCode();
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());

                }

            }
            catch (Exception ex)
            {

            }

        }

        //SEND WHATSAPP MESSAGE
        public static void SendWhatsApp(List<BirthdayDetails> Dates)
        {
            string accountSid = "AC3f00d69445579d293b62c7cf55c2d1d2";
            string authToken = "578eb870bcb9b4668305f82937a21058";

            TwilioClient.Init(accountSid, authToken);
            try
            {
                foreach(var items in Dates)
                {
                    

                    var message = MessageResource.Create(
                        from: new Twilio.Types.PhoneNumber("whatsapp:+16187498219"),
                        body: items.Message,
                        to: new Twilio.Types.PhoneNumber("whatsapp: +233245822226")
                    );
                }
            }

            catch (Exception) { }
        }


        // Send SMS
        /*public static void SendSMS(List<BirthdayDetails> Dates)
        {
            // string accountSid = Environment.GetEnvironmentVariable("ACc33e0bf171387b9325f7aa6f3101f6b3");
            //string authToken = Environment.GetEnvironmentVariable("99b89f7d98f4a010091befe5aa64085f");

            string accountSid = "ACc33e0bf171387b9325f7aa6f3101f6b3";
            string authToken = "99b89f7d98f4a010091befe5aa64085f";


            TwilioClient.Init(accountSid, authToken);
            try
            {
                foreach (var items in Dates)
                {
                    items.Contacts = "233542361520";
                    var message = MessageResource.Create(
                        body: items.Message,
                        from: new Twilio.Types.PhoneNumber("+18145262744"),
                        to: new Twilio.Types.PhoneNumber("+" + items.Contacts)
                        );
                }

            }
            catch (Exception ex)
            {

            }

        }*/



        //SEND WHATSAPP MESSAGE
        /*public static bool SendWhaatsapp(BirthdayDetails birthdayDetails, out string message)
        {
            // birthdayDetails.
            bool worked = false;
            message = "Request Successful";
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v17.0/116267054857923/messages");
                request.Headers.Add("Authorization", "Bearer EAAsRFxs0HqcBAFMxQVJ3tR5UU85pK1aZCCOZBtlX8k4y8oZARPLZBZC8J8EuLn8Pr4nO9z7MuhL6cNMB6XD2HzeABlng6ZBKqfpLWbRlH21X3UByuArZApUv5yFVjSbzRrCZCeAWSgcYGQOTrSKN8bnqvZCxRuzVMHoBExw75QcqcYvXF4CTKMkfnN4NFAyRJr5aq9mWzUzzvpyvjVSHJOkXQ");
                var content = new StringContent("{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \""+ birthdayDetails.Contacts+" \",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \" "+ birthdayDetails.Message +"\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}", null, "application/json");
                request.Content = content;
                var response = client.SendAsync(request).Result;
                //response.EnsureSuccessStatusCode();

                var s = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;

                    WhatsappResponse whatsappResponse = JsonConvert.DeserializeObject<WhatsappResponse>(data);
                }
                else
                {
                    var data = response.Content.ReadAsStringAsync().Result;

                    WhatsappError whatsappResponse = JsonConvert.DeserializeObject<WhatsappError>(data);

                    message = whatsappResponse.error.error_data.details;
                }
            }
            catch (Exception)
            {


            }
            return worked;
        }*/



    }


}
