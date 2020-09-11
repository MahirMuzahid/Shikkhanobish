using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Net.Http;
using Android.Content.Res;
using Shikkhanobish.ContentPages;

namespace Shikkhanobish.Model
{
    class ConnectToRealTimeApiServer
    {
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";

        public async Task ConnectToServer ( )
        {
            _connection = new HubConnectionBuilder ()
                .WithUrl ( url )
                .Build ();

            await _connection.StartAsync ();
            isConnected = true;
            connectionStatus = "Connected";
            
            _connection.Closed += async ( s ) =>
            {
                isConnected = false;
                connectionStatus = "Disconnected";
                await _connection.StartAsync ();
                isConnected = true;

            };

            _connection.On<int , int , bool> ( "SendStudentThatCallRecivedOrIgnored" , async ( studentID , teacherID , recivedOrNot ) =>
            {
                int studentid = studentID;
                int teaqcherid = teacherID;
                bool recivedornot = recivedOrNot;
            } );

            _connection.On<int , string , string , int , int , string , string> ( "CallInfo" , async ( ApiKey , SessionId , UserToken , studentID , teacherID , Class , subject ) =>
            {
                string r = "Got this msg: " + ApiKey + " " + SessionId + " " + UserToken + " " + studentID + " " + teacherID + " " + Class + " " + subject;
                
            } );
        }

        public async Task DisConnectToServer ( )
        {
            _connection = new HubConnectionBuilder ()
                .WithUrl ( url )
                .Build ();


            await _connection.StopAsync ();
        }


        public async Task ConnectWithTeacher (  string SessionId , string UserToken , int studentID , int teacherID , string Cls , string subject, double cost, string studentName )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;          
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            //string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            //var r = JsonConvert.DeserializeObject<string> ( result );
        }

        public async Task ConnectWithStudent ( int studentID , int teacherID , bool recivedOrNot )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/SendStudentThatCallRecivedOrIgnored?ApiKey=" + studentID + "&SessionId=" + teacherID + "&UserToken=" + recivedOrNot;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
        }
    }
}
