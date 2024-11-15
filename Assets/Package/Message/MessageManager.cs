using com.ootii.Messages;
	public static class MessageManager
	{
		public static void SendMessage(MessageType signal, object Data)
		{
			MessageDispatcher.SendMessageData(signal.ToString(), Data);
		}

		public static void SendMessage(MessageType signal)
		{
			MessageDispatcher.SendMessage(signal.ToString());
		}

		public static void AddListener(MessageType signal, MessageHandler Data)
		{
			MessageDispatcher.AddListener(signal.ToString(), Data, true);
		}

		public static void RemoveListener(MessageType signal, MessageHandler Data)
		{
			MessageDispatcher.RemoveListener(signal.ToString(), Data);
		}
	}
