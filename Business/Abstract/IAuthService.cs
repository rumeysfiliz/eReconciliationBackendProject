﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company);
        IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password, int companyId);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<User> GetByMailConfirmValue(string value);        
        IDataResult<User> GetById(int id);
        IResult UserExists(string email);
        IResult Update(User user);
        IResult CompanyExists(Company company);
        IResult SendConfirmEmail(User user);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
        IDataResult<UserCompany> GetCompany(int userId);
    }
}
