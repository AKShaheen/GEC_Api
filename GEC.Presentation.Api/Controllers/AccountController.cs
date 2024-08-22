using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Interfaces;
using GEC.Business.Services.PasswordHash;
using GEC.Infrastructure.Models;
using GEC.Presentation.Api.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    public class AccountController(IValidator<RegisterRequest> _registerValidator, 
                                    IValidator<LoginRequest> _loginValidator,
                                    IUserService _userService) : BaseApiController
    { 
        // private readonly IValidator<RegisterRequest> _registerValidator;
        // private readonly IValidator<LoginRequest> _loginValidator;
        // private readonly IUserService _userService;

        // public AccountController(IValidator<RegisterRequest> registerValidator, 
        //                         IValidator<LoginRequest> loginValidator,
        //                         IUserService userService)
        // {
        //     _registerValidator = registerValidator;
        //     _loginValidator = loginValidator;
        //     _userService = userService;
        // }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request){
            var result = await _registerValidator.ValidateAsync(request);
            if(!result.IsValid){
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors.Select(x => x.ErrorMessage));
            }
            var response = await _userService.RegisterAsync(request.Adapt<UserDto>());
            return Ok(response.Adapt<UserViewModel>());
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginRequest request){
            var result = await _loginValidator.ValidateAsync(request);
            if(!result.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors.Select(x => x.ErrorMessage));
            
            var userDto = await _userService.LoginAsync(request.Email, request.Password);
            Console.WriteLine(userDto);
            if (userDto == null) return BadRequest("Wrong username or password");
            return Ok(userDto.Adapt<UserViewModel>());
        }
    }
}