using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.Dto.User
{
    public class UserIdentity : IIdentity
    {
        private UserEntity _userEntity;

        public UserIdentity(UserEntity userEntity, List<AppResourceDto> userResources)
        {
            this._userEntity = userEntity;
            this.UserResources = userResources;
        }

        /// <summary>Gets the type of authentication used.</summary>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType => "EasyRBAC Token";

        /// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated => true;

        /// <summary>Gets the name of the current user.</summary>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name => this._userEntity.UserName;

        public long UserId => this._userEntity.Id;

        public string RealName => this._userEntity.RealName;

        public string MobilePhone => this._userEntity.MobilePhone;

        public DateTime CreateTime => this._userEntity.CreateTime;

        public List<AppResourceDto> UserResources { get; }
    }
}
