//using Business.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using SoftwareBlog.Identity;
//using SoftwareBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wepProgProje.Models;
namespace wepProgProje.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        IBlogPostService _blogPostService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IBlogPostService blogPostService, RoleManager<IdentityRole> roleManager)
        {
            _ = this.User;
            _userManager = userManager;
            _signInManager = signInManager;
            _blogPostService = blogPostService;
            _roleManager = roleManager;

        }
        public IActionResult EditProfile()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDetailsModel userDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(userDetails);
            }
            var user = await _userManager.GetUserAsync(User);




            return RedirectToAction("AboutMe", "Account");
        }
        public IActionResult AboutMe()
        {



            return View();
        }
        [HttpPost]
        public IActionResult AboutMe(UserDetailsModel userDetails)
        {


            return RedirectToAction("AboutMe", "Account");
        }
        public async Task<IActionResult> MyPage()
        {
            var user = await _userManager.GetUserAsync(User);
            string id = user.Id;
            var posts = await _blogPostService.GetAll();



            //  id = _signInManager.IsSignedIn();

            return View(new MyPageListViewModel()

            {
                MyPosts = posts.Where(x => x.UserId == id).ToList(),




            }

                );
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //public IActionResult Login(string ReturnUrl = null)
        //{
        //    return RedirectToAction("Index", "Home");

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            //  var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce hesap oluşturulmamış");
                return View(model);
            }

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //{
            //    ModelState.AddModelError("", "Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayınız.");
            //    return View(model);
            //}

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            var roles = await _userManager.GetRolesAsync(user);
            if (result.Succeeded)
            {
                foreach (var role in roles)
                {

                    if (role == "admin")
                    {


                        return RedirectToAction("UserList", "Admin");
                    }
                }

                return RedirectToAction("MyPage", "Account");
            }

            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
            return View(model);
        }
        //     [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("~/");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);
            }
            bool x = await _roleManager.RoleExistsAsync("user");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "user";
                await _roleManager.CreateAsync(role);
            }
            var user = new User()
            {

                UserName = model.UserName,
                Email = model.Email,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                // generate token
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var url = Url.Action("ConfirmEmail", "Account", new
                //{
                //    userId = user.Id,
                //    token = code
                //});

                // email
                // await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }
    }
}
}
