using System.Collections.Generic;
using StorageBot.Model;
namespace StorageBot
{
	public static class Session
	{
		public static Dictionary<int, Users> Users { get; set; } = new Dictionary<int, Users>();
	}
}
