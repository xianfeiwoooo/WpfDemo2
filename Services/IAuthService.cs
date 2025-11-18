using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDemo2.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string phone, string password);
    }
}
