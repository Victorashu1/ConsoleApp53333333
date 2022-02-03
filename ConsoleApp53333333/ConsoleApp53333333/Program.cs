using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Diagnostics;

namespace http
{
    class Program
    {
        public static object MessageBox { get; private set; }

        public void sendGET ()
        {
            // Адрес ресурса, к которому выполняется запрос
            string url = "http://site.com/";

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
            }
        }
        private static async Task PostRequestAsync(string mesage,string Types)
        {
       
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/");
            request.Method = Types; 
            string data = ""+mesage;
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            response.Close();
            Console.WriteLine("Запрос выполнен...");
        }
       
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Http Server\n введите значение или show вывести все Get запросом");
            var server = new SimpleHttpServer(Log);
            server.Start();

            while (true)
            {
                
                var s = Console.ReadLine();
                if(s=="show") break;
                Task task1 = PostRequestAsync(s, "POST");
               
            }


            string url = "http://127.0.0.1:8000/";

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                Log(response);
            }

       




        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}