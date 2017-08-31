using EasyRbac.Domain.Enums;

namespace EasyRbac.Domain.Entity
{
    public class AppResourceEntity
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public long ApplicationId { get; set; }

        public bool Enable { get; set; }

        public string ResourceName { get; set; }

        public string ResourceCode { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType {get;set;}

        public string IconUrl { get; set; }

        public string Parameters { get; set; }

        public string Descrypt { get; set; }
    }
}