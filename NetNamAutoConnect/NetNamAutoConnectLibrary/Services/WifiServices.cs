﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetNamAutoConnectLibrary.Services
{
    public static class WifiServices
    {
        private static readonly Uri LOGIN_URI = new Uri("http://logoutwifi.netnam.vn/login");

        private static readonly Uri LOGOUT_URI = new Uri("http://logoutwifi.netnam.vn/logout");

        public static async Task Login(string username, string password)
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("username",username),
                        new KeyValuePair<string, string>("password",password),
                    });
                    await client.PostAsync(LOGIN_URI, content);
                }
            }
            catch (Exception)
            {
            }
        }

        public static async Task Logout()
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    await client.GetAsync(LOGOUT_URI);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}