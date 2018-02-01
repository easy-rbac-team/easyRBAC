using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace EasyRbac.Dto.Application
{
    public class ApplicationIdentity : IIdentity
    {
        private ApplicationInfoDto _app;

        public ApplicationIdentity(ApplicationInfoDto app)
        {
            this._app = app;
        }

        public string AuthenticationType => "Application";

        public bool IsAuthenticated => true;

        public string Name => this._app.AppCode;

        public ApplicationInfoDto App => this._app;
    }
}
