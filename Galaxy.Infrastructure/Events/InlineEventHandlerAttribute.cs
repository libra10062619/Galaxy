using System;
namespace Galaxy.Infrastructure.Events
{
    /// <summary>
    /// Inline event handler attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class InlineEventHandlerAttribute : Attribute
    {
    }
}
