using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfDemo2.Core.Command;
using WpfDemo2.Services;
using WpfDemo2.ViewModels;

namespace WpfDemo2.Core.Models
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _auth;
        private string _phone;
        private string _password;
        private string _captchaInput;
        private string _captchaCode;
        private bool _rememberMe;
        private bool _showPassword;

        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string CaptchaInput { get => _captchaInput; set => SetProperty(ref _captchaInput, value); }
        public string CaptchaCode { get => _captchaCode; set => SetProperty(ref _captchaCode, value); }
        public bool RememberMe { get => _rememberMe; set => SetProperty(ref _rememberMe, value); }
        public bool ShowPassword { get => _showPassword; set => SetProperty(ref _showPassword, value); }

        public ICommand LoginCommand { get; }
        public ICommand RefreshCaptchaCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand TogglePasswordVisibilityCommand { get; }

        public event Action LoginSucceeded;
        public event Action<string> LoginFailed;

        public LoginViewModel(IAuthService auth)
        {
            _auth = auth;
            CaptchaCode = GenerateCaptcha();
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            RefreshCaptchaCommand = new RelayCommand(() => CaptchaCode = GenerateCaptcha());
            ForgotPasswordCommand = new RelayCommand(() => LoginFailed?.Invoke("请联系管理员重置密码"));
            TogglePasswordVisibilityCommand = new RelayCommand(() => ShowPassword = !ShowPassword);
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Phone)) { LoginFailed?.Invoke("请输入手机号"); return; }
            if (string.IsNullOrWhiteSpace(Password)) { LoginFailed?.Invoke("请输入密码"); return; }
            if (string.IsNullOrWhiteSpace(CaptchaInput)) { LoginFailed?.Invoke("请输入验证码"); return; }
            if (!string.Equals(CaptchaInput, CaptchaCode, StringComparison.OrdinalIgnoreCase)) { LoginFailed?.Invoke("验证码不正确"); return; }
            var ok = await _auth.LoginAsync(Phone, Password);
            if (ok) LoginSucceeded?.Invoke(); else LoginFailed?.Invoke("账号或密码错误");
        }

        private string GenerateCaptcha()
        {
            var r = new Random();
            return r.Next(1000, 9999).ToString();
        }
    }
}
