using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Domain.Enums;
using SQLinq;

namespace EasyRbac.Dto.AppResource
{
    [SQLinqTable("app_resouce")]
    public class AppResourceDto
    {
        public long Id { get; set; }

        public long ApplicationId { get; set; }
        
        public bool IsPublic { get; set; }

        public string ResouceName { get; set; }

        public string ResouceCode { get; set; }

        public string Url { get; set; }

        public ResourceType ResouceType { get; set; }

        public string IconUrl { get; set; }

        public string Parameters { get; set; }

        public string Describe { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<AppResourceDto> Children { get; set; }
    }
}

