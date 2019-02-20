using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _ctxt = new ApplicationDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [Authorize]
        public ActionResult ProfilePage()
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                User = user,
                Experiences = _ctxt.Experiences.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Educations = _ctxt.Educations.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Skills = _ctxt.Skills.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return View(VM);
        }

        [Authorize]
        [HttpPost]
        //ApplicationUser user
        public ActionResult EditInformationAjax(LinkedInUserProfileViewModel e)
        {
            string UserId = User.Identity.GetUserId();
            
            ApplicationUser oldUser = _ctxt.Users.Where(u=>u.Id== UserId).FirstOrDefault();

            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();

            if (ModelState.IsValid && oldUser != null)
            {
                oldUser.FName = e.User.FName;
                oldUser.LName = e.User.LName;
                oldUser.Age = e.User.Age;
                oldUser.Position = e.User.Position;
                oldUser.Country = e.User.Country;
                oldUser.Bio = e.User.Bio;
                oldUser.School = e.User.School;
                oldUser.University = e.User.University;
                //oldUser.ImageURL = e.User.ImageURL;
                //oldUser.CoverURL = e.User.CoverURL;
                _ctxt.SaveChanges();
                VM.User = oldUser;
                return PartialView("_PartialUserBasicInformation", VM);
            }

            return View();
        }

        #region Experience Operations
        [Authorize]
        [HttpPost]
        public ActionResult AddExperienceAjax(Experience experience)
        {
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();


            if (ModelState.IsValid && experience != null)
            {
                experience.FK_LinkedInUserId = User.Identity.GetUserId();
                _ctxt.Experiences.Add(experience);
                _ctxt.SaveChanges();
                VM.Experiences = _ctxt.Experiences.ToList();
                return PartialView("_PartialUserExperience", VM);
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditExperienceAjax(Experience experience)
        {

            Experience oldExperience = _ctxt.Experiences.Where(e => e.Id == experience.Id).FirstOrDefault();

            string UserId = User.Identity.GetUserId();

            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();

            if (ModelState.IsValid && experience != null)
            {
                oldExperience.Title = experience.Title;
                oldExperience.Company = experience.Company;
                oldExperience.Location = experience.Location;
                oldExperience.StartDate = experience.StartDate;
                oldExperience.EndDate = experience.EndDate;
                _ctxt.SaveChanges();
                VM.Experiences = _ctxt.Experiences.Where(ex => ex.FK_LinkedInUserId == UserId).ToList();
                return PartialView("_PartialUserExperience", VM);
            }

            return View();
        }

        [Authorize]
        public ActionResult LoadExperience(int? id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Experience experience = null;
            if (id != null)
            {
                experience = _ctxt.Experiences.Where(e => e.Id == id).FirstOrDefault();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                experience = experience,
                User = user,
                Experiences = _ctxt.Experiences.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return PartialView("_PartialUserExperience", VM);
        }
        [Authorize]
        public ActionResult DeleteExperience(int id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Experience experience = _ctxt.Experiences.Find(id);
            if (experience != null)
            {
                _ctxt.Experiences.Remove(experience);
                _ctxt.SaveChanges();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                User = user,
                Experiences = _ctxt.Experiences.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return RedirectToAction("ProfilePage");
        }

        #endregion


        #region Education Operations
        [Authorize]
        [HttpPost]
        public ActionResult AddEducationAjax(Education education)
        {
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();

            if (ModelState.IsValid && education != null)
            {
                education.FK_LinkedInUserId = User.Identity.GetUserId();
                _ctxt.Educations.Add(education);
                _ctxt.SaveChanges();
                VM.Educations = _ctxt.Educations.ToList();
                return PartialView("_PartialUserEducation", VM);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditEducationAjax(Education education)
        {

            Education oldEducation = _ctxt.Educations.Where(e => e.Id == education.Id).FirstOrDefault();
            string UserId = User.Identity.GetUserId();
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();

            if (ModelState.IsValid && education != null)
            {
                oldEducation.School = education.School;
                oldEducation.Degree = education.Degree;
                oldEducation.StartDate = education.StartDate;
                oldEducation.EndDate = education.EndDate;
                _ctxt.SaveChanges();
                VM.Educations = _ctxt.Educations.Where(ex => ex.FK_LinkedInUserId == UserId).ToList();
                return PartialView("_PartialUserEducation", VM);
            }

            return View();
        }

        [Authorize]
        public ActionResult LoadEducation(int? id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Education education = null;
            if (id != null)
            {
                education = _ctxt.Educations.Where(e => e.Id == id).FirstOrDefault();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                education = education,
                User = user,
                Educations = _ctxt.Educations.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return PartialView("_PartialUserEducation", VM);
        }

        [Authorize]
        public ActionResult DeleteEducation(int id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Education education = _ctxt.Educations.Find(id);
            if (education != null)
            {
                _ctxt.Educations.Remove(education);
                _ctxt.SaveChanges();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                User = user,
                Educations = _ctxt.Educations.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return RedirectToAction("ProfilePage");
        }
        #endregion

        #region Skill Operation
        [Authorize]
        [HttpPost]
        public ActionResult AddSkillAjax(Skill skill)
        {
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();
            if (ModelState.IsValid && skill != null)
            {
                skill.FK_LinkedInUserId = User.Identity.GetUserId();
                _ctxt.Skills.Add(skill);
                _ctxt.SaveChanges();
                VM.Skills = _ctxt.Skills.ToList();
                return PartialView("_PartialUserSkill", VM);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditSkillAjax(Skill skill)
        {

            Skill oldSkill = _ctxt.Skills.Where(e => e.Id == skill.Id).FirstOrDefault();
            string UserId = User.Identity.GetUserId();
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();

            if (ModelState.IsValid && skill != null)
            {
                oldSkill.Name = skill.Name;
                _ctxt.SaveChanges();
                VM.Skills = _ctxt.Skills.Where(ex => ex.FK_LinkedInUserId == UserId).ToList();
                return PartialView("_PartialUserSkill", VM);
            }

            return View();
        }

        [Authorize]
        public ActionResult LoadSkill(int? id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Skill skill = null;
            if (id != null)
            {
                skill = _ctxt.Skills.Where(e => e.Id == id).FirstOrDefault();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                skill = skill,
                User = user,
                Skills = _ctxt.Skills.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return PartialView("_PartialUserSkill", VM);
        }

        [Authorize]
        public ActionResult DeleteSkill(int id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            Skill skill = _ctxt.Skills.Find(id);
            if (skill != null)
            {
                _ctxt.Skills.Remove(skill);
                _ctxt.SaveChanges();
            }
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                User = user,
                Skills = _ctxt.Skills.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return RedirectToAction("ProfilePage");
        }
        #endregion



        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CoverUpload(HttpPostedFileBase file)
        {
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();
            if (file != null)
            {
                var userId = User.Identity.GetUserId();
                var user = _ctxt.Users.Where(u => u.Id == userId).FirstOrDefault();
                
                string pic = user.FName.Replace(" ","") + "_cover_" + System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images"), pic);
                // file is uploaded
                file.SaveAs(path);
                
                user.CoverURL = pic;
                _ctxt.SaveChanges();
                VM.User = user;
                VM.Experiences = _ctxt.Experiences.ToList();
                VM.Educations = _ctxt.Educations.ToList();
                VM.Skills = _ctxt.Skills.ToList();
            }
            // after successfully uploading redirect the user
            return PartialView("_PartialUserBasicInformation", VM);
        }

        public ActionResult ProfileUpload(HttpPostedFileBase file)
        {
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel();
            if (file != null)
            {
                var userId = User.Identity.GetUserId();
                var user = _ctxt.Users.Where(u => u.Id == userId).FirstOrDefault();

                string pic = user.FName.Replace(" ", "") + "_profile_" + System.IO.Path.GetFileName(file.FileName) ;
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images"), pic);
                // file is uploaded
                file.SaveAs(path);
                /// Images / Default_profile.png
                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB

                user.ImageURL = pic;
                _ctxt.SaveChanges();
                VM.User = user;

            }
            // after successfully uploading redirect the user
            return PartialView("_PartialUserBasicInformation", VM);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {FName=model.Fname,
                    LName = model.Lname,Age=model.Age,
                    UserName = model.Email, Email = model.Email,
                    CoverURL= "Default_cover_2.jpg",
                    ImageURL= "Default_profile.png",
                    //"url(@Url.Content("~/ Images / Default_cover.jpg"))"
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Wall", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      