using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shikkhanobish.Model
{
    class Massage
    {
        public bool isSent { get; set; }
        public async Task SendMsg (string number, string msg)
        {
          
            string url = "https://api.shikkhanobish.com/api/Master/GetKeys";
            HttpClient client = new HttpClient ();
            HttpResponseMessage response = await client.GetAsync ( url ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var keys = JsonConvert.DeserializeObject<ApiKey> ( result );
            string apiKey = keys.msgapi;
            url = "https://www.bdgosms.com/send/?req=out&apikey="+ apiKey+"&numb=" + number + "&sms=" + msg;
            client = new HttpClient ();
            response = await client.GetAsync ( url ).ConfigureAwait ( false );
            result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( false );
            if ( result [ 0] == '{' )
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
