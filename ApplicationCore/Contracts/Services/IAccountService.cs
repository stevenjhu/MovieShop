using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model);
        Task<int> RegisterUser(UserRegisterModel model);
    }
}
