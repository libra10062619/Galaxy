using System;
namespace Galaxy.Infrastructure.Domain
{
    /// <summary>
    /// Biz unique attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class BizUniqueAttribute : Attribute
    {
        public string GroupName { get; set; }

        public BizUniqueAttribute()
        {
        }
    }
}
