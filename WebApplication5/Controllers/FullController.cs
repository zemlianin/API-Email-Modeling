using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за все объекты
    /// </summary>
    public class FullController : Controller
    {
        /// <summary>
        /// Создание рандомных Юзеров и сообщений
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-random")]
        public IActionResult CreateUserAndMessageRandom()
        {
            var messageKontrol = new MessageController();
            var userKontrol = new UserInfoController();
            var randomNumber = new Random();
            for (int i = 0; i < randomNumber.Next(2, 6); i++)
            {
                userKontrol.CreateUser(new Models.CreateUserRequest()
                {
                    Email = RandomWord(),
                    UserName = RandomWord()
                }
                );

            }
            for (int i = 0; i < randomNumber.Next(2, 6); i++)
            {
                messageKontrol.SendMessage(new Models.SendMessageRequest()
                {
                    ReceiverEmail = UserInfoController._users[randomNumber.Next(0, UserInfoController._users.Count)].Email,
                    SenderEmail = UserInfoController._users[randomNumber.Next(0, UserInfoController._users.Count)].Email,
                    Message = RandomWord()
                });
            }
            object[] result = new object[] { UserInfoController._users, MessageController._messages };
            return Ok(result);
        }

        /// <summary>
        /// Создание рандомного слова
        /// </summary>
        /// <returns></returns>
        private string RandomWord()
        {
            var random = new Random();
            string s = "";
            for (int i = 0; i < random.Next(2, 5); i++)
            {
                s += (char)random.Next('a', 'z' + 1);
            }
            return s;
        }


    }
}
