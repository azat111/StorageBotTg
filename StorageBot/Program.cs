using Microsoft.EntityFrameworkCore;
using StorageBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Update = Telegram.Bot.Types.Update;

namespace StorageBot
{
	static class StorageBot
	{
		static Dictionary<long, Users> authorizedUsers = new Dictionary<long, Users>();

		static void Main(string[] args)
		{
			var client = new TelegramBotClient("5905944280:AAHXbVp579CtSDieTUGIRjA1iOFqTw9J6go");
			client.StartReceiving(Update, Error);
			Console.ReadLine();

		}

		async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken arg3)
		{
			var message = update.Message;

			if (message == null)
			{
				return;
			}
			if (message.Text == "/login")
			{
				await Login((TelegramBotClient)botClient, update);
				return;
			}
			else if (message.Text == "/logout")
			{
				await Logout((TelegramBotClient)botClient, update);
				return;
			}
			return;
		}

		async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
		{

		}	
		async static Task Login(TelegramBotClient botClient, Update update)
		{
			var message = update.Message;

			if (authorizedUsers.ContainsKey(message.Chat.Id))
			{
				await botClient.SendTextMessageAsync(message.Chat.Id, "Вы уже авторизованы. Для выхода из учетной записи используйте команду /logout");
				return;
			}

			if (message.Text == "/login")
			{
				await botClient.SendTextMessageAsync(message.Chat.Id, "Введите логин");
			}
			else
			{
				await botClient.SendTextMessageAsync(message.Chat.Id, "Для авторизации используйте команду /login.");
				return;
			}

			string username = await GetNextMessageFromUser(botClient, message.Chat.Id);

			while (username == "/login")
			{
				username = await GetNextMessageFromUser(botClient, message.Chat.Id);
			}

			if (username != "/login")
			{
				await botClient.SendTextMessageAsync(message.Chat.Id, "Введите пароль");
				string password = await GetNextMessageFromUser(botClient, message.Chat.Id);

				using (StorageManagerContext db = new StorageManagerContext())
				{
					var user = db.Users.FirstOrDefault(u => u.Login == username && u.Password == password);

					if (user == null)
					{
						await botClient.SendTextMessageAsync(message.Chat.Id, "Неверный логин или пароль.");
						return;
					}
					authorizedUsers.Add(message.Chat.Id, user);
					await CheckPosition(botClient, message.Chat.Id, user,update);	
				}
			}
		}
		async static Task Logout(TelegramBotClient botClient, Update update)
		{
			var message = update.Message;
			if (!authorizedUsers.ContainsKey(message.Chat.Id))
			{
				await botClient.SendTextMessageAsync(message.Chat.Id, "Вы не авторизованы. Для входа в учетную запись используйте команду /login");
				return;
			}
			authorizedUsers.Remove(message.Chat.Id);
			await botClient.SendTextMessageAsync(message.Chat.Id, "Вы вышли из учетной записи.");
		}
		static int updateId = 0;
		async static Task<string> GetNextMessageFromUser(TelegramBotClient botClient, long chatId)
		{
			
			string result = null;

			while (result == null)
			{
				var updates = await botClient.GetUpdatesAsync(offset: updateId);

				foreach (var update in updates)
				{
					if (update.Message != null && update.Message.Chat.Id == chatId)
					{
						result = update.Message.Text;
						updateId = update.Id + 1;
						break;
					}
				}

				await Task.Delay(1000);
			}

			return result;
		}
		async static Task CheckPosition(TelegramBotClient botClient, long chatId, Users user, Update update)
		{
			switch (user.Position)
			{
				case "администратор":
					await HandleAdmin(botClient, chatId, user, update);
					break;
				case "водитель":
					await HandleDriver(botClient, chatId, user,update);
					break;
				default:
					await botClient.SendTextMessageAsync(chatId, "У вас нет прав для доступа к функциям бота.");
					break;
			}
		}

		async static Task HandleDriver(TelegramBotClient botClient, long chatId, Users user, Update update)
		{
			var message = await botClient.SendTextMessageAsync(chatId, $"Добро пожаловать, {user.Login}! Вы вошли как водитель.");
			while (true)
			{
				var text = await GetNextMessageFromUser(botClient, chatId);
				if (text == "Заказы на кейтеринг")
				{
					await checkZak(botClient, message);
				}
				else if (text == "/logout")
				{
					await Logout(botClient, update);
					break;
				}
				else
				{
					await botClient.SendTextMessageAsync(chatId, $"Вы написали: {text}. Я не знаю, что на это ответить.");
				}
			}
			botClient.SendTextMessageAsync(chatId, $"Добро пожаловать, {user.Login}! Вы вошли как водитель.");


		}

		async static Task HandleAdmin(TelegramBotClient botClient, long chatId, Users user, Update update)
		{

			var message = await botClient.SendTextMessageAsync(chatId, $"Добро пожаловать, {user.Login}! Вы вошли как администратор." );

			while (true)
			{
				var text = await GetNextMessageFromUser(botClient, chatId);

				if (text == "Заказы на кейтеринг")
				{
					await checkZak(botClient, message);
				}
				else if (text == "/logout")
				{
					await Logout(botClient, update);
					break;
				}
				else if (text.Contains("Продукты из заказа"))
				{
					string a = text;
					await chekprod(botClient, message,a);
				}
				else
				{
					await botClient.SendTextMessageAsync(chatId, $"Вы написали: {text}. Я не знаю, что на это ответить.");
				}
			}

		}

		async static Task checkZak(TelegramBotClient botClient, Message message)
		{
			using (StorageManagerContext storageManagerContext = new StorageManagerContext())
				{
					List<Catering> catering = storageManagerContext.Catering.Include(x => x.Customer).Include(a => a.IdStatusNavigation).ToList();
					foreach (var item in catering)
					{
					if (item.IdStatus!=2 && item.IdStatus != 3)
					{
						await botClient.SendTextMessageAsync(message.Chat.Id, "Номер заказа: " + item.CateringId + " \n " + "Дата заказа: " + item.OrderDate + " \n " + "Дата мероприятия: " + item.EventDate + " \n " + "Общая стоимость: " + item.TotalCost + " \n " + "Клиент: " + item.Customer.CustomerName + " \n " + "Статус: " + item.IdStatusNavigation.StatusName + " \n " + "Адресс: " + item.EventAddress + " \n " + "Ссылка для яндекс карт: " + item.Link + " \n ");
					}
				}
			}

				return;
		}
		async static Task chekprod(TelegramBotClient botClient, Message message,string a)
		{
			string q = a;
			q = q.Trim(new char[] { 'П', 'р', 'о', 'д', 'у', 'к', 'т', 'ы', ' ', 'и', 'з', 'а' });
			int num = Convert.ToInt32(q);
			using (StorageManagerContext storageManagerContext = new StorageManagerContext())
			{
				foreach (var item in storageManagerContext.CateringDetails.Include(x => x.Product))
				{
					if (item.CateringId == num)
					{
						await botClient.SendTextMessageAsync(message.Chat.Id, "Название продукта: " + item.Product.ProductName + " \n " + "Количество: " + item.Quantity + " \n " + "Стоимость: " + item.Cost);

					}
				}
			}

			return;
		}


	}
}
