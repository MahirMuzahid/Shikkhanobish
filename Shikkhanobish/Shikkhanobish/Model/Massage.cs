using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Shikkhanobish.Model
{
    class Massage
    {
        public bool isSent { get; set; }
        public async void SendMsg (string number, string msg, string apiKey)
        {
            string url = "https://www.bdgosms.com/send/?req=out&apikey="+ apiKey+"&numb=" + number + "&sms=" + msg;
            HttpClient client = new HttpClient ();
            HttpResponseMessage response = await client.GetAsync ( url );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
            if ( r == "Sms sent successfully" )
            {
                isSent = true;
            }
            else
            {
                isSent = false;
            }
        }
    }
}
