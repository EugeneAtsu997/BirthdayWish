using Newtonsoft.Json;

namespace BirthdayWish
{
    public class BirthdayDetails
    {
        public string Email? { get; set; };
        public string DateOfBirth? { get; set; };
        public string Subject? { get; set; };
        public string Message? { get; set; };
        public string Contacts? { get; set; };
    }


    /*public class WhatsappResponse
    {
        public string messaging_product { get; set; }
        public Contact[] contacts { get; set; }
        public Message[] messages { get; set; }
    }

    public class Contact
    {
        public string input { get; set; }
        public string wa_id { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
    }




    public class WhatsappError
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public Error_Data error_data { get; set; }
        public string fbtrace_id { get; set; }
    }

    public class Error_Data
    {
        public string messaging_product { get; set; }
        public string details { get; set; }
    }*/

}
