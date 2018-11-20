using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Galaxy.Infrastructure.Events
{
    public static class EventHandlerHelper
    {
        public static IEnumerable<MethodInfo> GetInlineEventHandlerMethods<TEvent>(Type type, TEvent @event) where TEvent : IDomainEvent
        {
            var query = from m in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                   let parameters = m.GetParameters()
                   where m.IsDefined(typeof(InlineEventHandlerAttribute)) &&
                   m.ReturnType == typeof(void) &&
                   parameters.Length == 1 &&
                   parameters[0].ParameterType == @event.GetType()
                   select m;
            return query.AsEnumerable();
        }

        public static MethodInfo GetAsyncHandlingMethod<TDomainEventHandler>(TDomainEventHandler handler, string eventName)
            where TDomainEventHandler: IDomainEventHandler
        {
            var query = from m in handler.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        let parameters = m.GetParameters()
                        where parameters.Length == 1 &&
                                         parameters[0].ParameterType.FullName == eventName
                        select m;
            return query.FirstOrDefault();

            //return handlerType is IHandler 
                //? handlerType.GetMethod("HandleAsync", BindingFlags.Public | BindingFlags.Instance) 
                                 //: null;
        }

        public static Type GetEventType<TDomainEventHandler>(TDomainEventHandler handler, string eventName)
            where TDomainEventHandler: IDomainEventHandler
        {
            var query = from m in handler.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        let parameters = m.GetParameters()
                        where parameters.Length == 1 &&
                                         parameters[0].ParameterType.FullName == eventName
                        select m;
            return query.FirstOrDefault().GetParameters()[0].ParameterType;

            //return handlerType is IHandler 
                //? handlerType.GetMethod("HandleAsync", BindingFlags.Public | BindingFlags.Instance) 
                                 //: null;
        }
    }
}
