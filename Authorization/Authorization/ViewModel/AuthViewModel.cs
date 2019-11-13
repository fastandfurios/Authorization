
using Authorization.Model;
using Authorization.Service;
using Authorization.View;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Authorization.ViewModel
{
    class AuthViewModel
    {
        private string _login;
        private string _password;
        private bool _authNotSuccess;
        //private readonly Realm _realm = Realm.GetInstance();
        private AuthService _authService = new AuthService();
        public AuthViewModel()
        {
            var user = new User();

            _login = user.Login;
            _password = user.Password;

            Input = new CommutatorCommand(obj =>
                                          {
                                              //_realm.Write(() =>
                                              //{
                                              //    user.Login = Login;
                                              //    user.Password = Password;
                                              //});
                                              LoginCommandExecute(user);
                                          },
                                          obj => Connectivity.NetworkAccess == NetworkAccess.Internet && Login != string.Empty && Password != string.Empty);
        }

        public string Login 
        {
            get => _login;
            set => _login = value;
        }

        public string Password 
        {
            get => _password;
            set => _password = value; 
        }

        public ICommand Input 
        { 
            get; 
            set; 
        }

        private async void LoginCommandExecute(User user)
        {
            
            bool result = await _authService.LoginAsync(user);

            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Уведомление","Вход выполнен","ОК");
            }

            var app = Application.Current as App;
            if(app == null)
            {
                return;
            }

                //Realm.Write(() =>
                //{
                //    Realm.Add(user,true);
                //});
        }
    }
}
