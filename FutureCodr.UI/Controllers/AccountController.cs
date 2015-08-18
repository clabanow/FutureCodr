using System.Web.Security;

namespace FutureCodr.UI.Controllers
{
    using FutureCodr.UI.Models.Authentication;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;

    [Authorize]
    public class AccountController : Controller
    {
        private const string XsrfKey = "XsrfId";

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            UserValidator<ApplicationUser> userValidator = UserManager.UserValidator as UserValidator<ApplicationUser>;
            if (userValidator != null)
            {
                userValidator.AllowOnlyAlphanumericUserNames = false;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string str in result.Errors)
            {
                base.ModelState.AddModelError("", str);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            int? message = null;
            IdentityResult asyncVariable0 = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (asyncVariable0.Succeeded)
            {
                message = 2;
            }
            else
            {
                message = 3;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (UserManager != null))
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, base.Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            ApplicationUser user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, false);
                return RedirectToLocal(returnUrl);
            }
            ((dynamic)ViewBag).ReturnUrl = returnUrl;
            ((dynamic)ViewBag).LoginProvider = loginInfo.Login.LoginProvider;
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel
            {
                UserName = loginInfo.DefaultUserName
            };
            return View("ExternalLoginConfirmation", model);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }
            if (ModelState.IsValid)
            {
                ExternalLoginInfo asyncVariable0 = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (asyncVariable0 == null)
                {
                    return View("ExternalLoginFailure");
                }
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, asyncVariable0.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }
            ((dynamic)ViewBag).ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return base.View();
        }

        private bool HasPassword()
        {
            ApplicationUser user = UserManager.FindById<ApplicationUser>(base.User.Identity.GetUserId());
            return ((user != null) && (user.PasswordHash != null));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult LinkLogin(string provider)
        {
            return new ChallengeResult(provider, base.Url.Action("LinkLoginCallback", "Account"), base.User.Identity.GetUserId());
        }

        public async Task<ActionResult> LinkLoginCallback()
        {
            ActionResult result;
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync("XsrfId", User.Identity.GetUserId());
            if (loginInfo == null)
            {
                result = RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            else
            {
                IdentityResult asyncVariable0 = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
                if (asyncVariable0.Succeeded)
                {
                    result = RedirectToAction("Manage");
                }
                else
                {
                    result = RedirectToAction("Manage", new { Message = ManageMessageId.Error });
                }
            }
            return result;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                    
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return base.RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ((dynamic)ViewBag).HasLocalPassword = hasPassword;
            ((dynamic)ViewBag).ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    AddErrors(result);
                }
            }
            else
            {
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                if (ModelState.IsValid)
                {
                    IdentityResult asyncVariable1 = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (asyncVariable1.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    AddErrors(asyncVariable1);
                }
            }
            return View(model);
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ManageMessageId? nullable4 = null;
            if (((((ManageMessageId)message) != ManageMessageId.ChangePasswordSuccess) && (((ManageMessageId)message) != ManageMessageId.SetPasswordSuccess)) && (((ManageMessageId)message) != ManageMessageId.RemoveLoginSuccess))
            {
                nullable4 = message;
            }
            ((dynamic)base.ViewBag).StatusMessage = ((((ManageMessageId)nullable4.GetValueOrDefault()) == ManageMessageId.Error) && nullable4.HasValue) ? "Your password has been changed." : "";
            ((dynamic)base.ViewBag).HasLocalPassword = HasPassword();
            ((dynamic)base.ViewBag).ReturnUrl = base.Url.Action("Manage");
            return base.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return base.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole<ApplicationUser>(user.Id, "user");
                    await SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            IList<UserLoginInfo> logins = UserManager.GetLogins<ApplicationUser>(base.User.Identity.GetUserId());
            ((dynamic)base.ViewBag).ShowRemoveButton = HasPassword() || (logins.Count > 1);
            return PartialView("_RemoveAccountPartial", logins);
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(new string[] { "ExternalCookie" });
            ClaimsIdentity asyncVariable0 = await UserManager.CreateIdentityAsync(user, "ApplicationCookie");
            AuthenticationProperties properties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };
            AuthenticationManager.SignIn(properties, new ClaimsIdentity[] { asyncVariable0 });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return base.HttpContext.GetOwinContext().Authentication;
            }
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}
