using ImperialBreath.Website.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImperialBreath.Website.Services
{
    public class UserService
    {
        public const string SESSION_USER_ID = "UserId";

        private readonly List<User> users;
        private JsonFileService jsonFileService;
        public UserService(JsonFileService jsonFileService)
        {
            this.jsonFileService = jsonFileService;
            users = jsonFileService.GetUsers().ToList();
        }

        public bool Authorize(string id, string pw)
        {
            User user = users.Find(x => x.Id == id);
            if (user == null)
                return false;

            string salt = user.Pw.Split('@')[0];
            return user.Pw == HashPassword(pw, salt);
        }

        public void AddUser(string id, string pw)
        {
            if (users.FindAll(user => user.Id == id).Count() != 0)
                throw new UserException("아이디가 존재합니다.");

            string salt = Guid.NewGuid().ToString();
            User newUser = new User()
            {
                Id = id,
                Pw = HashPassword(pw, salt)
            };
            users.Add(newUser);

            jsonFileService.SaveUsers(users);
        }

        public static string HashPassword(string data, string salt)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data + salt));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
                stringBuilder.AppendFormat("{0:x2}", b);
            return salt + "@" + stringBuilder.ToString();
        }

        public void ToggleTask(string userId, int taskId)
        {
            User user = users.Find(x => x.Id == userId);

            if (user.DoneTaskId == null)
                user.DoneTaskId = new int[] { taskId };
            else if (user.DoneTaskId.Contains(taskId))
                user.DoneTaskId = user.DoneTaskId.Where(val => val != taskId).ToArray();
            else
                user.DoneTaskId = user.DoneTaskId.Append(taskId).ToArray();

            jsonFileService.SaveUsers(users);
        }
    }

    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
            
        }
    }
}
