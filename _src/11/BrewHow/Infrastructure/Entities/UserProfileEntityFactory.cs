using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrewHow.Domain.Entities;
using WebMatrix.WebData;

namespace BrewHow.Infrastructure.Entities
{
    public class UserProfileEntityFactory : IUserProfileEntityFactory
    {
        public UserProfileEntity Create()
        {
            var context = HttpContext.Current;

            if (context == null)
            {
                throw new InvalidOperationException(
                    "The request is not occurring within a valid HTTP context.");
            }

            if (!context.Request.IsAuthenticated)
            {
                return null;
            }

            return new UserProfileEntity
            {
                UserId = WebSecurity.CurrentUserId,
                UserName = WebSecurity.CurrentUserName
            };
        }
    }
}