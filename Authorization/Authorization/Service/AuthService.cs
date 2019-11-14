using Authorization.Model;
using Authorization.ViewModel;
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
        /// Задает адрес для авторизации
        /// </summary>
        private const string Adress = "http://battery.itmit-studio.ru//api/login";

       /// <summary>
       /// Отправляет введенные данные пользователем на сервер
       /// </summary>
       /// <param name="login">Логин пользователя</param>
       /// <param name="password">Пароль пользователя</param>
       /// <returns></returns>
        public async Task<bool> LoginAsync(string login, string password)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(login);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {
                        "login", login
                    },
                    {
                        "password", password
                    }
                });
                response = await client.PostAsync(new Uri(Adress), encodedContent);
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(jsonString);

            var jsonData = JsonConvert.DeserializeObject<JsonDataResponse<User>>(jsonString);
            
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
