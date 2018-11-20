using System;

namespace Galaxy.Infrastructure.Messaging
{
	public class MessageEventArgs : EventArgs
	{
        public string EventName { get; set; }

        public object Message { get; set; }

        public MessageEventArgs(string eventName, object message)
		{
            this.EventName = eventName;
			this.Message = message;
		}
	}
}