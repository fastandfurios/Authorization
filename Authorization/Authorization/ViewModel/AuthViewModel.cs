
using Authorization.Model;
using Authorization.Service;
using Authorization.View;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Authorization.ViewModel
{
    class AuthViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //private readonly Realm _realm = Realm.GetInstance();
        private string _login;
        private string _password;
        private bool _authNotSuccess;
        private AuthService _authService = new AuthService();

        public AuthViewModel()
        {
            Input = new Command(obj =>
                                        { 
                                            LoginCommandExecute(Login, Password); 
                                        },
                                        obj => Connectivity.NetworkAccess == NetworkAccess.Internet && Login != string.Empty && Password != string.Empty);

            var user = new User();
            user.Login = _login;
            user.Password = _password;

            AuthNotSuccess = false;
        }

        /// <summary>
        /// Отображает введеный логин пользователя
        /// </summary>
        public string Login 
        {
            set
            {
                if (_login != value)
                {
                    _login = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
                }
            }
            get => _login;
        }

        /// <summary>
        /// Отображает введеный пароль пользователя
        /// </summary>
        public string Password 
        {
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get => _password;
        }

        public bool AuthNotSuccess 
        {
            set => _authNotSuccess = value;
            get => _authNotSuccess;
        }

        /// <summary>
        /// Выполняет команду при нажатии кнопки
        /// </summary>
        public ICommand Input 
        { 
            get;
        }

        /// <summary>
        /// При нажатии кнопки запускает метод отправки данных на сервер. Выдает алерт
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        private async void LoginCommandExecute(string login, string password)
        {
            try
            {
                var app = Application.Current as App;
                if (app == null)
                {
                    return;
                }

                bool result = await _authService.LoginAsync(login, password);

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Уведомление", "Вход выполнен!", "ОК");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Уведомление", "Неверный логин или пароль!", "ОК");
                }
            }
            catch (AuthenticationException e)
            {
                AuthNotSuccess = true;
                Debug.WriteLine(e);
                return;
            }
            
                //Realm.Write(() =>
                //{
                //    Realm.Add(user,true);
                //});
        }
    }
}
