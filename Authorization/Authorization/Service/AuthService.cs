using Authorization.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service
{
    class AuthService
    {
        /// <summary>
        /// Задает адрес авторизации
        /// </summary>
        private const string Adress = "http://battery.itmit-studio.ru//api/login";
        private const string Value = "Radik";

        public async Task<bool> LoginAsync(User user)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {
                        "login", user.Login
                    },
                    {
                        "password", user.Password
                    }
                });
                response = await client.PostAsync(new Uri(Adress), encodedContent);
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(jsonString);

            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
