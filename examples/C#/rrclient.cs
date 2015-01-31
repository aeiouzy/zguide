﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZeroMQ;

namespace ZeroMQ.Test
{
	static partial class Program
	{
		public static void RRClient(IDictionary<string, string> dict, string[] args)
		{
			//
			// Hello World client
			// Connects REQ socket to tcp://localhost:5559
			// Sends "Hello" to server, expects "World" back
			//
			// Author: metadings
			//

			// Socket to talk to server
			using (var context = ZContext.Create())
			using (var requester = ZSocket.Create(context, ZSocketType.REQ))
			{
				requester.Connect("tcp://127.0.0.1:5559");

				for (int n = 0; n < 10; ++n)
				{
					using (var request = new ZFrame("Hello"))
					{
						requester.Send(request);
					}

					using (ZFrame reply = requester.ReceiveFrame())
					{
						Console.WriteLine("Hello {0}!", reply.ReadString());
					}
				}
			}
		}
	}
}