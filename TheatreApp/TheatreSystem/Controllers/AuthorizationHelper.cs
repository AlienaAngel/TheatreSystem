using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    public static class AuthorizationHelper
    {
        public static string UserRole = "USER_ROLE";
        public static string ModeratorRole = "MODERATOR_ROLE";
        public static string AdminRole = "ADMIN_ROLE";

        public static bool IsAccess(String permitedRole, HttpSessionStateBase Session)
        {

            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                return user.HasRole(permitedRole);
            }
            else
            {
                return false;
            }
        }

        public static User CurrentUser(HttpSessionStateBase Session)
        {
            return (User)Session["user"];
        }

        public static void InitializationAuthVar(dynamic ViewBag, HttpSessionStateBase Session)
        {
            ViewBag.IsUser = false;
            ViewBag.IsModerator = false;
            ViewBag.IsAdmin = false;

            if (Session["user"] != null)
            {
                User user = (User)Session["user"];

                foreach (Role role in user.Roles)
                {
                    if (role.Name.Equals("USER_ROLE"))
                    {
                        ViewBag.IsUser = true;
                    }

                    if (role.Name.Equals("MODERATOR_ROLE"))
                    {
                        ViewBag.IsModerator = true;
                    }

                    if (role.Name.Equals("ADMIN_ROLE"))
                    {
                        ViewBag.IsAdmin = true;
                    }
                }


            }
        }
    }
}