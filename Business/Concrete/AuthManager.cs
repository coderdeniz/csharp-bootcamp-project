using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Jwt.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Access Token Oluşturuldu");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Şifre yanlış");
            }
            return new SuccessDataResult<User>(userToCheck, "Giriş Başarılı");
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var checkUserExist = UserExist(userForRegisterDto.Email);
            if (!checkUserExist.Success)
            {
                return new ErrorDataResult<User>(checkUserExist.Message);
            }

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password,out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kullanıcı başarıyla kayıt oldu.");
        }

        public IResult UserExist(string email)
        {
            var result = _userService.GetByMail(email);
            if (result != null)
            {
                return new ErrorResult("Kullanıcı Mevcut");
            }
            return new SuccessResult();
        }
    }
}
