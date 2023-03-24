using MAUI_API.Exceptions;
using MAUI_API.Interfaces;
using MAUI_API.Repositories;
using MAUIAppCommon.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace MAUI_API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _IUser;
        private readonly EventLogRepository eventLogRepository;
        public UserController(IUser _IUser, EventLogRepository eventLogRepository)
        {
            this._IUser = _IUser;
            this.eventLogRepository = eventLogRepository;
        }

        [Route("~/User/Register")]
        [HttpPost]
        public async Task Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new();
                if (await _IUser.UserIsInDatabase(registerModel))
                {
                    ModelState.AddModelError("", "Такой пользователь уже существует!");
                }
                else
                {
                    byte[] salt = { 1, 2, 3 };

                    user.Login = registerModel.Login;
                    user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: registerModel.Password!,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));

                    await _IUser.AddNewUser(user);
                }
            }
        }

        [Route("~/User/Login")]
        [HttpPost]
        public async Task Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = await _IUser.GetUserByLoginModelAsync(loginModel);
                    if (user != null)
                    {
                        await eventLogRepository.AddLogger("Пользователь зашел в систему", user);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                        User _user = await _IUser.GetUserByLoginAsync(loginModel);
                        await eventLogRepository.AddLogger("Пользователь ввел некорректный логин и(или) пароль", user);
                    }
                }
                catch (NotFoundException)
                {
                    await eventLogRepository.AddLogger("Пользователь ввел некорректный логин и(или) пароль", null);
                }
            }
        }
    }
}
