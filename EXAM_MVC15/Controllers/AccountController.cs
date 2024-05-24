using Core.Models;
using EXAM_MVC15.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXAM_MVC15.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid) return View();
            User user= new User()
            {
                UserName = registerDto.UserName,
                Name=registerDto.Name,
                Surname=registerDto.SurName,
                Email=registerDto.Email,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if(user == null){
                ModelState.AddModelError("", "Istifadeci ve ya pasword yanlisdir");
                return View();
            }
           
            await _signInManager.PasswordSignInAsync(user,loginDto.Password,loginDto.Confirmed,true);
           
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role = new IdentityRole("SuperAdmin");
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");
           await _roleManager.CreateAsync(role);
           await _roleManager.CreateAsync(role1);
          await  _roleManager.CreateAsync(role2);
            return Ok("Rolll yarandiiii");
        }

        public async Task<IActionResult> AddRole()
        {
            var user = await _userManager.FindByNameAsync("ramazann.75");

           await _userManager.AddToRoleAsync(user ,"SuperAdmin");

            return Ok("Rol verildi");
        }
    }
}
