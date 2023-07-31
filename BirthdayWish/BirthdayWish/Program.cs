using System;
using System.Collections.Generic;

namespace BirthdayWish
{
    public class Program
    {
        public static void Main(string[] args)
        {


            string excelPath = @"C:\Users\EUGENE\Documents\BirthdayDetails.xlsx";
            string outputPath = @"C:\Users\EUGENE\Documents\BirthdayDetails.json";
            //string fromPassword = "rwiqdlcgyapwtfml";
            //string fromEmail = "aklikaheugene@gmail.com";
            Console.WriteLine("Hello World");

            functions.KillExcelInstances();

            bool success = functions.ReadExcelToJson(excelPath, outputPath);
            if (!success)
            {
                Console.WriteLine("Unable to read excel to json");
                return;
            }

            List<BirthdayDetails> birthdayInfo = functions.ReadDataIntoObject(outputPath);

            if (!birthdayInfo.Any())
            {
                Console.WriteLine("Unable to read json data into object");
            }

            List<BirthdayDetails> birthDate = functions.Date(birthdayInfo);

            /*foreach (var item in birthDate)
            {
                bool whatsappSent = functions.SendWhaatsapp(item, out string message);

                if (!whatsappSent)
                {
                    Console.WriteLine(message + " for " + item.Contacts);
                }
            }*/
            if (!birthDate.Any())
            {
                Console.WriteLine("Unable to read parse date");
            }

            try
            {
                foreach (BirthdayDetails d in birthDate)
                {
                    //send ind. to email function
                    functions.SendMail(d);

                    // SMS

                    //Whatsapp
                }
            }
            catch (Exception)
            {

               
            }
            /*foreach (BirthdayDetails d in birthDate)
            {
                //send ind. to email function
                functions.SendMail(d);

                // SMS

                //Whatsapp
            }*/


            //functions.SendMail(birthDate);

            //functions.SendSMS(birthDate);

            //functions.SendWhatsApp(birthDate);







        }
    }
}