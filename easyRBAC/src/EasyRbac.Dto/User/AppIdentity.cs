using EasyRbac.Domain.Entity;
using EasyRbac.Dto.AppResource;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class AppIdentity : UserIdentity
    {
        private ApplicationEntity _applicationEntity;
        public AppIdentity(UserEntity userEntity, List<AppResourceDto> userResources,ApplicationEntity appEntity):
            base(userEntity,userResources)
        {
            this._applicationEntity = appEntity;
        }

        public long AppId => this._applicationEntity.Id;

        public string AppName => this._applicationEntity.AppName;

        public string AppCode => this._applicationEntity.AppCode;
    }
}
