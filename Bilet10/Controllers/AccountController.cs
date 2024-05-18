using Bilet10.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bilet10.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user=new User()
            {
                Name= registerDto.Name,
                Surname=registerDto.Surname,
                Email=registerDto.Email,
                UserName=registerDto.UserName,
            };
            
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
       
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, "Admin");


            return RedirectToAction("Index", "Home");
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.EmailOrUserName);
            if(user == null)
            {
                user= await _userManager.FindByEmailAsync(login.EmailOrUserName);
                if( user == null)
                {
                    ModelState.AddModelError("", "Pasword ve ya username duzgun daxil edilmeyib!");
                    return View();
                }
            }
            var result= await _signInManager.CheckPasswordSignInAsync(user,login.Password,true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin!");
            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Pasword ve ya username duzgun daxil edilmeyib!");
                return View();
            }
            await _signInManager.SignInAsync(user, login.IsRemember);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role = new IdentityRole("Admin");
            

            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Member");
           await  _roleManager.CreateAsync(role);
           await  _roleManager.CreateAsync(role1);
           await  _roleManager.CreateAsync(role2);
            return Ok();




        }
    }
}
