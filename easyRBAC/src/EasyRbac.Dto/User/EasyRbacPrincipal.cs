using EasyRbac.Dto.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class EasyRbacPrincipal : ClaimsPrincipal
    {
        private IIdentity _identity;

        public EasyRbacPrincipal(UserIdentity identity):base(identity)
        {
            this._identity = identity;
        }

        /// <summary>Gets a collection that contains all of the claims from all of the claims identities associated with this claims principal.</summary>
        /// <returns>The claims associated with this principal.</returns>
        public override IEnumerable<Claim> Claims
        {
            get
            {
                if (this._identity is UserIdentity)
                {
                    var identity = this._identity as UserIdentity;
                    return identity.UserResources.Select(x => new Claim(x.ResourceType.ToString(), x.ResourceCode));
                }
                else
                {
                    return Enumerable.Empty<Claim>();
                }
            }
        }

        /// <summary>Gets the primary claims identity associated with this claims principal.</summary>
        /// <returns>The primary claims identity associated with this claims principal.</returns>
        public override IIdentity Identity => this._identity;
    }
}
