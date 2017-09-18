using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.AppResource
{
    public class AppAndResourceDto
    {
        public long AppId { get; set; }

        public string AppName { get; set; }

        public string AppCode { get; set; }

        public List<AppResourceDto> AppResouces { get; set; }
    }
}
