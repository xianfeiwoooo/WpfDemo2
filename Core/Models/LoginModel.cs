using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfDemo2.Core.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        private string _phone;
        private string _password;
        private string _captchaInput;
        private string _captchaCode;
        private bool _rememberMe;

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string CaptchaInput
        {
            get => _captchaInput;
            set => SetProperty(ref _captchaInput, value);
        }

        public string CaptchaCode
        {
            get => _captchaCode;
            set => SetProperty(ref _captchaCode, value);
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
