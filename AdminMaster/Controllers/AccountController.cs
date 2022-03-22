using AdminMaster.Respository.Interface;
using AdminMaster.Utils.Enums;
using AdminMaster.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminMaster.Controllers
{
    public class AccountController : Controller
    {
        private IUsers UserService;
        public AccountController(IUsers users)
        {
            UserService = users;
        }
        public IActionResult Login()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Login(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = UserService.SignIn(model);
                if(result== SignInEnum.Success)
                {
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, model.Email),
                  
                     
                };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    });

                    return RedirectToAction("Index", "Home");
                }
                else if (result == SignInEnum.WrongCredentials)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login credentials !");
                }
                else if (result == SignInEnum.NotVerified)
                {
                    ModelState.AddModelError(string.Empty, "IUser not verified please verifiy first!");

                }
                else if (result == SignInEnum.InActive)
                {
                    ModelState.AddModelError(string.Empty, "Your account is inactive !");

                }
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult Register(SignUpModel model)
        {
            if(ModelState.IsValid)
            {
                var result = UserService.SignUp(model);
                if (result == SignUpEnum.Success)
                {
                    return RedirectToAction("VerifyAccount");
                }
                else if(result==SignUpEnum.EmailExist)
                {
                    ModelState.AddModelError(string.Empty, "Email already exist, Please try another !");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong !");
                }

            }
            return View(model);
        }

        public IActionResult VerifyAccount()
        {
            return View();
        } 
        
        [HttpPost]
        public IActionResult VerifyAccount(string Otp)
        {
            if (Otp != null)
            {
                if (UserService.VerifyAccount(Otp))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please enter correct OTP !");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter OTP !");
            }
            return View();
        }
    }
}
