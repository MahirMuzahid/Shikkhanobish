using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using OpenTokSDK;

namespace Shikkhanobish.Model
{
    public class VideoCAllApiInfo
    {
        public Session Session { get; protected set; }
        public string Token { get; protected set; }
        public OpenTok OpenTok { get; protected set; }
        public VideoCAllApiInfo ( )
        {
            int apiKey = 0;
            string apiSecret = null;
            try
            {
                apiSecret = "c255c95670bc11eecaf5950baf375d7478f74665";
                apiKey = 46485492;
            }

            catch ( Exception ex )
            {
                if ( !( ex is ConfigurationErrorsException || ex is FormatException || ex is OverflowException ) )
                {
                    throw ex;
                }
            }

            finally
            {
                if ( apiKey == 0 || apiSecret == null )
                {
                    Console.WriteLine (
                        "The OpenTok API Key and API Secret were not set in the application configuration. " +
                        "Set the values in App.config and try again. (apiKey = {0}, apiSecret = {1})" , apiKey , apiSecret );
                    Console.ReadLine ();
                    Environment.Exit ( -1 );
                }
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.OpenTok = new OpenTok ( apiKey , apiSecret );

            this.Session = this.OpenTok.CreateSession ();
            this.Token = this.OpenTok.GenerateToken ( this.Session.Id);
        }
    
    }
}
