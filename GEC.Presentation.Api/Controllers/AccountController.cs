using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Contracts.Response;
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
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid Input",
                    Errors = string.Join("," , result.Errors.Select(x => x.ErrorMessage))
                };
                return BadRequest(badResponse);
                //return StatusCode(StatusCodes.Status400BadRequest, result.Errors.Select(x => x.ErrorMessage));
            }
            var responseData = await _userService.RegisterAsync(request.Adapt<UserDto>());
            var response = new BaseResponse<UserViewModel>{
                StatusCode = StatusCodes.Status200OK,
                Message =  "Customer Added Successfully",
                Data = request.Adapt<UserViewModel>()
            };
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request){
            var result = await _loginValidator.ValidateAsync(request);
            if(!result.IsValid){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid Input",
                    Errors = string.Join("," , result.Errors.Select(x => x.ErrorMessage))
                };
                return BadRequest(badResponse);
            }
            try {
                var userDto = await _userService.LoginAsync(request.Email, request.Password);
                var response = new BaseResponse<UserViewModel>{
                    StatusCode = StatusCodes.Status200OK,
                    Message =  "Customer Added Successfully",
                    Data = request.Adapt<UserViewModel>()
                };
                return Ok(response);
            }catch (KeyNotFoundException){
                    var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Email Doesn't Exist"
                };
                return NotFound(badResponse);
            }catch (InvalidOperationException){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Wrong Password"
                };
                return NotFound(badResponse);
            }
        }
    }
}