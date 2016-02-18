﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CHUYAChuya.Web.Controllers.Base;
using CHUYAChuya.Web.Models;

namespace CHUYAChuya.Web.Controllers.Base
{
	public class LogInController : BaseController
	{
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Logear(string UserName, string Password)
        {
            //validamos usuario
            bool validar = Membership.Provider.ValidateUser(UserName, Password);

            if (validar)
            {
                //registramos usuario
                FormsService.SignIn(UserName, true);
                return Json(1);
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return Json(2);
        }

        public ActionResult CerrarSession()
        {
            FormsService.SignOut();
            Roles.DeleteCookie();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

	}
}