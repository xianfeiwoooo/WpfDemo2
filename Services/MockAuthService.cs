using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDemo2.Services
{
    public class MockAuthService : IAuthService
    {
        public Task<bool> LoginAsync(string phone, string password)
        {
            var ok = !string.IsNullOrWhiteSpace(phone) && !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
            return Task.FromResult(ok);
        }
    }
}
