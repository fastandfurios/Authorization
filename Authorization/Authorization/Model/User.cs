using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Model
{
    public class User
    {

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает роль пользователя
        /// </summary>
        public string Role 
        { 
            get; 
            set; 
        }
    }
}
