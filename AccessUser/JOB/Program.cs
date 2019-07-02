using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using System.Reflection;
using System.IO;

namespace JOB
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Scheduler();
        }

        private static void Scheduler()
        {
        // For Interval in Seconds
        // This Scheduler will start at 11:10 and call after every 15 Seconds
        // IntervalInSeconds(start_hour, start_minute, seconds)
            MyScheduler.IntervalInSeconds(07, 45, 00,
            () =>
            {
                Console.WriteLine("//here write the code that you want to schedule 07:45:00");
                RunAsync().Wait();

            });

            //// For Interval in Minutes 
            // This Scheduler will start at 22:00 and call after every 30 Minutes
            // IntervalInSeconds(start_hour, start_minute, minutes)
            MyScheduler.IntervalInMinutes(22, 00, 30,
            () =>
            {

                //Console.WriteLine("//here write the code that you want to schedule");
            });

            // For Interval in Hours 
            // This Scheduler will start at 9:44 and call after every 1 Hour
            // IntervalInSeconds(start_hour, start_minute, hours)
            MyScheduler.IntervalInHours(9, 44, 1,
            () =>
            {

                //Console.WriteLine("//here write the code that you want to schedule");
            });

            // For Interval in Seconds 
            // This Scheduler will start at 17:22 and call after every 3 Days
            // IntervalInSeconds(start_hour, start_minute, days)
            MyScheduler.IntervalInDays(17, 22, 3,
            () =>
            {

                // Console.WriteLine("//here write the code that you want to schedule");
            });

            Console.ReadLine();
        }

        private static void CreateHeader<T>(List<T> list, StreamWriter sw)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Write(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sw.Write(lastProp + sw.NewLine);
        }
        private static void CreateRows<T>(List<T> list, StreamWriter sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Write(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Write(lastProp.GetValue(item) + sw.NewLine);
            }
        }

        public static void CreateCSV<T>(List<T> list, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                CreateHeader(list, sw);
                CreateRows(list, sw);
            }
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                //#GET ALL Access;
                client.BaseAddress = new Uri("https://localhost:44360/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/accesses");

                if (response.IsSuccessStatusCode)
                {
                    List<Access> access = await response.Content.ReadAsAsync<List<Access>>();
                    string path ="exports/";
                    DateTime dta = DateTime.Now;
                    string file = dta.ToString("ddMMyyyyHHmmss");
                    string ext = ".txt";
                    string completPath = path + file + ext;
                    CreateCSV(access, completPath);
                    Console.WriteLine(access.ToString());
                }
            }
        }
    }
}
