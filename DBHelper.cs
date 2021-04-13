using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RedisMessenger
{
    public static class DBHelper
    {

        private const string PREF_CHATS = "Chats", PREF_MESSAGES = "Messages", ONLINE_USERS = "OnlineUsers", USERS_LIST = "Users";

        private static IDatabase Database;
        private static RedisChannel MyChannel;

        public delegate void ActionDel(ActionTypes actionType, int arg);
        public static event ActionDel OnAnyAction;

        static DBHelper()
        {
            var options = new ConfigurationOptions();
            options.EndPoints.Add("localhost", 6379);
            var connection = ConnectionMultiplexer.Connect(options);
            Database = connection.GetDatabase();
            MyChannel = new RedisChannel("Channel1", RedisChannel.PatternMode.Auto);
            ISubscriber subscriber = connection.GetSubscriber();
            subscriber.Subscribe(MyChannel, (channel, message) => MessageAction(message));
        }

        #region Users

        public static User[] GetUsers()
        {
            User[] res;
            ConvertData(out res, Database.ListRange(USERS_LIST, 0, -1));
            return res;
        }

        public static bool IsUserExists(string login)
        {
            foreach (User u in GetUsers())
                if (u.Login.ToUpper() == login.ToUpper())
                    return true;
            return false;
        }

        public static User GetUserByLoginPassword(string login, string password)
        {
            foreach (User u in GetUsers())
                if (u.Login.ToUpper() == login.ToUpper() && u.Password == password)
                    return u;
            return null;
        }

        public static bool IsEmailExists(string email)
        {
            foreach (User u in GetUsers())
                if (u.Email == email)
                    return true;
            return false;
        }

        public static User GetUserById(int id)
        {
            try
            {
                return JsonSerializer.Deserialize<User>(Database.ListGetByIndex(USERS_LIST, id));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает Id добавленного пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            User u = GetUserById(-1);
            if(u != null)
                user.Id = u.Id+1;
            Database.ListRightPushAsync(USERS_LIST, JsonSerializer.Serialize(user));
            return GetUserById(-1).Id;
        }

        public static void EditUser(User user)
        {
            Database.ListSetByIndex(USERS_LIST, user.Id, JsonSerializer.Serialize(user));
        }

        #endregion

        #region Messages

        public static Message[] GetMessages(int chatId)
        {
            Message[] res;
            ConvertData(out res, Database.ListRange($"{PREF_MESSAGES}:{chatId}", 0, -1));
            return res;
        }

        public static void AddMessage(Message m)
        {
            Database.ListRightPushAsync($"{PREF_MESSAGES}:{m.ChatId}", JsonSerializer.Serialize(m));
            Publish(ActionTypes.NewMessage, m.ChatId);
        }

        #endregion

        #region Chats

        public static Chat[] GetChats()
        {
            Chat[] res;
            ConvertData(out res, Database.ListRange(PREF_CHATS, 0, -1));
            return res;
        }

        public static Chat GetChatById(int id)
        {
            try
            {
                return JsonSerializer.Deserialize<Chat>(Database.ListGetByIndex(PREF_CHATS, id));
            }
            catch
            {
                return null;
            }
        }

        public static int AddChat(Chat c)
        {
            Chat chat = GetChatById(-1);
            if (chat != null)
                c.Id = chat.Id+1;
            Database.ListRightPushAsync($"{PREF_CHATS}", JsonSerializer.Serialize(c));
            Publish(ActionTypes.ChatAdded, c.Id);
            return GetChatById(-1).Id;
        }

        #endregion

        #region OnlineUsers

        public static User[] GetOnlineUsers()
        {
            int[] Ids;
            ConvertData(out Ids, Database.ListRange($"{ONLINE_USERS}", 0, -1));
            User[] res = new User[Ids.Length];
            for (int i = 0; i < Ids.Length; i++)
                res[i] = GetUserById(Ids[i]);
            return res;
        }

        public static void AddOnlineUser(int userId)
        {
            if (IsUserOnline(userId))
                return;
            Database.ListRightPushAsync($"{ONLINE_USERS}", userId);
            Publish(ActionTypes.UserOnline, userId);
        }

        public static void DeleteOnlineUser(int userId)
        {
            if (!IsUserOnline(userId))
                return;
            Database.ListRemove($"{ONLINE_USERS}", userId);
            Publish(ActionTypes.UserOnline, userId);
        }

        private static bool IsUserOnline(int userId)
        {
            foreach (User u in GetOnlineUsers())
                if (u.Id == userId)
                    return true;
            return false;
        }

        #endregion

        private static void ConvertData<T>(out T[] output, RedisValue[] values)
        {
            output = new T[values.Length];
            for (int i = 0; i < values.Length; i++)
                output[i] = JsonSerializer.Deserialize<T>(values[i]);
        }

        private static void MessageAction(RedisValue message)
        {
            System.Diagnostics.Trace.WriteLine("MESSAGE FROM REDIS: " + message);
            string[] arr = message.ToString().Split(' ');
            ActionTypes actionType = ActionTypes.UserOnline;
            switch (Convert.ToInt32(arr[0]))
            {
                case 1: actionType = ActionTypes.NewMessage; break;
                case 2: actionType = ActionTypes.ChatAdded; break;
            }
            OnAnyAction?.Invoke(actionType, Convert.ToInt32(arr[1]));
        }

        private static void Publish(ActionTypes actionType, int arg)
        {
            Database.Publish(MyChannel, $"{(int)actionType} {arg}");
        }

    }

    public enum ActionTypes
    {
        UserOnline, NewMessage, ChatAdded
    }


    public class Message
    {

        public int Id { get; set; }
        public string Date { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }

        public Message(int id, string date, int chatId, int userId, string text)
        {
            Id = id;
            Date = date;
            ChatId = chatId;
            UserId = userId;
            Text = text;
        }

    }

    public class User
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }

        public User(int id, string login, string password, string nick, string email, byte[] avatar)
        {
            Id = id;
            Login = login.ToUpper();
            Password = password;
            Nick = nick;
            Email = email;
            Avatar = avatar;
        }

    }

    public class Chat
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int UsersInChat { get { return Id; } }
        public byte[] Avatar { get; set; }

        public Chat(int id, string name, byte[] avatar)
        {
            Id = id;
            Name = name;
            Avatar = avatar;
        }

    }

}
