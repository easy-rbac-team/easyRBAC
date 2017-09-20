using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using EasyRbac.Domain.Enums;
using SQLinq;

namespace EasyRbac.Dto.AppResource
{
    [DebuggerDisplay("Id = {Id}")]
    [SQLinqTable("app_resouce")]
    public class AppResourceDto
    {
        public string Id { get; set; }

        public long ApplicationId { get; set; }

        public string ResourceName { get; set; }

        public string ResourceCode { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public string IconUrl { get; set; }

        public string Parameters { get; set; }

        public string Describe { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<AppResourceDto> Children { get; set; } = new List<AppResourceDto>();
    }
}

