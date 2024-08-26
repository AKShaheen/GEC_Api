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
            try {
                var userDto = await _userService.LoginAsync(request.Email, request.Password);
                return Ok(userDto.Adapt<UserViewModel>());
            }catch (KeyNotFoundException){
                return BadRequest("Email Not Found");
            }catch (InvalidOperationException){
                return BadRequest("Wrong Email or Password");
            }
            
        }
    }
}