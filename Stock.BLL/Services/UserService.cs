using Stock.BLL.Exceptions;
using Stock.BLL.Interfaces;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;
using BCrypt.Net;

namespace TI_Devops_2024_DemoAspMvc.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public int Register(User u)
        {
            bool error = false;
            List<string> errorMessages = new List<string>();

            if(_userRepository.ExistByUsername(u.Username))
            {
                error = true;
                errorMessages.Add($"User with username : {u.Username} already exists");
            }
            if (_userRepository.ExistByEmail(u.Email))
            {
                error = true;
                errorMessages.Add($"User with email : {u.Email} already exists");
            }
            if(error)
            {
                throw new UserAlreadyExistsException(errorMessages);
            }

            u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);

            return _userRepository.Create(u);
        }

        public User Login(string login, string password)
        {
            User? u = _userRepository.GetUserByUsernameOrEmail(login);

            if(u is null)
            {
                throw new KeyNotFoundException("User doesn't exists");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, u.Password))
            {
                throw new Exception("Wrong password");
            }

            return u;
        }

        public string LoginToken(string login, string password)
        {
            User? u = _userRepository.GetUserByUsernameOrEmail(login);

            if (u is null)
            {
                throw new KeyNotFoundException("User doesn't exists");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, u.Password))
            {
                throw new Exception("Wrong password");
            }

            return _authService.Generate(u);
        }
    }
}
