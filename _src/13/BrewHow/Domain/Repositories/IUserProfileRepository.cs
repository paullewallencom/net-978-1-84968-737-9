using System;
using BrewHow.Domain.Entities;

namespace BrewHow.Domain.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfileEntity GetUser(string username);
        void Save(UserProfileEntity userProfileEntity);
    }
}
