using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMD.ServiceCaller
{
    class Program
    {

        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            if ( args.Length == 0 )
            {
                RunAsync("").Wait();
            }
            else
            {
                RunAsync(args[0]).Wait();
            }
           
        }

        static async Task RunAsync(string type)
        {
            client.BaseAddress = new Uri("http://manage.cash4ads.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = null;
            if (type == "")
                response = await client.GetAsync("test");
            else
                response = await client.GetAsync("test?mode=" + type);
            if (response.IsSuccessStatusCode)
            {
                Console.Write("success");
            }

        }
    }
}
