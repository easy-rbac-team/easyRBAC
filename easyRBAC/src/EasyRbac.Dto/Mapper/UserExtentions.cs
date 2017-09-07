using EasyRbac.Domain.Entity;
using EasyRbac.Dto.User;

namespace EasyRbac.Dto.Mapper
{
    public static class UserExtentions
    {
        public static UserInfoDto ToUserInfoDto(this UserEntity user)
        {
            return new UserInfoDto()
            {
                UserName = user.UserName,
                MobilePhone = user.MobilePhone,
                RealName = user.RealName,
                Enable = user.Enable
            };
        }
    }
}
