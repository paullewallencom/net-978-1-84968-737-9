using System;
using System.Linq;
using System.Linq.Expressions;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class UserProfileRepository : RepositoryBase, IUserProfileRepository
    {
        public UserProfileRepository(IBrewHowContext context)
            : base(context)
        {
        }

        public UserProfileEntity GetUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(
                    "username",
                    "Cannot pass null to UserProfileRepository::GetUser");
            }

            username = username.ToLower();

            return this
                .UserProfileEntities
                .FirstOrDefault(u => u.UserName == username);
        }

        public void Save(UserProfileEntity userProfileEntity)
        {
            if (userProfileEntity == null)
            {
                throw new ArgumentNullException(
                    "userProfileEntity",
                    "Can't save null to the user profiles.");
            }
            // Make sure it doesn't exist.  If it does then 
            // you're doing it wrong.
            var existingUserProfile = GetUser(userProfileEntity.UserName);

            if (existingUserProfile != null)
            {
                // Nothing to do.
                return;
            }

            this
                .Context
                .UserProfiles
                .Add(AssignEntityToModel(userProfileEntity));

            this.Context.SaveChanges();
        }

        private IQueryable<UserProfileEntity> UserProfileEntities
        {
            get
            {
                return this
                    .Context
                    .UserProfiles
                    .Select(EntityMappingExpressions.AsUserProfileEntity); 
            }
        }

        private UserProfile AssignEntityToModel(UserProfileEntity userProfileEntity)
        {
            return new UserProfile
            {
                UserId = userProfileEntity.UserId,
                UserName = userProfileEntity.UserName
            };
        }
    }
}