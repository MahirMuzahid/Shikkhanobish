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
        public async Task SendMsg (string number, string msg, string apiKey)
        {
            string url = "https://www.bdgosms.com/send/?req=out&apikey="+ apiKey+"&numb=" + number + "&sms=" + msg;
            HttpClient client = new HttpClient ();
            HttpResponseMessage response = await client.GetAsync ( url ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( false );
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
