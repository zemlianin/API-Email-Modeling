using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за работу с сообщениями
    /// </summary>
    [Route("/api/[controller]")]
    public class MessageController : Controller
    {
        /// <summary>
        /// Лист сообщений
        /// </summary>
        internal static List<MessageInfo> _messages = new List<MessageInfo>();

        /// <summary>
        /// Генерация сообщений
        /// </summary>
        /// <param name="req">модельь сообщения</param>
        /// <returns></returns>
        [HttpPost("send-message")]
        public IActionResult SendMessage([FromBody] SendMessageRequest req)
        {

            var message = new MessageInfo()
            {
                SenderEmail = req.SenderEmail,
                ReceiverEmail = req.ReceiverEmail,
                Message = req.Message,
            };
            bool flagSender = false;
            bool flagReceiver = false;
            //проверка на существование имэйлов среди пользователей
            UserInfoController._users.ForEach(a =>
            {
                if (message.SenderEmail == a.Email)
                    flagSender = true;
            });
            UserInfoController._users.ForEach(a =>
            {
                if (message.ReceiverEmail == a.Email)
                    flagSender = true;
            });

            if (!flagReceiver || !flagSender)
                return NotFound(new { Message = $"Пользователи не найдены" });

            ReadJson();
            _messages.Add(message);
            using (var fsWrite = new FileStream("message.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync<List<MessageInfo>>(fsWrite, _messages);
                Console.WriteLine("Data has been saved to file");
            }
            return Ok(message);

        }
        /// <summary>
        /// Получение сообщения по отправителю и получателю
        /// </summary>
        /// <param name="senderEmail">имеил отправителя</param>
        /// <param name="receiverEmail">имеил получателя</param>
        /// <returns></returns>
        [HttpGet("get-messages-bySenderAndReceiver")]
        public IActionResult GetMessagesBySenderAndReceiver(string senderEmail, string receiverEmail)
        {
            var thisMessages = new List<MessageInfo>();
            ReadJson();
            thisMessages = _messages.Where(x => (x.SenderEmail == senderEmail && x.ReceiverEmail == receiverEmail)).ToList();
            // thisMessages.Sort();
            return Ok(thisMessages);
        }
        /// <summary>
        /// Поиск сообщения по отправителю
        /// </summary>
        /// <param name="senderEmail">имеил отправителя</param>
        /// <returns></returns>
        [HttpGet("get-messages-bySender")]
        public IActionResult GetMessagesBySender(string senderEmail)
        {
            var thisMessages = new List<MessageInfo>();
            ReadJson();
            thisMessages = _messages.Where(x => (x.SenderEmail == senderEmail)).ToList();
            // thisMessages.Sort();
            return Ok(thisMessages);
        }
        /// <summary>
        /// Поиск сообщения по получателю
        /// </summary>
        /// <param name="receiverEmail">имеил получателя</param>
        /// <returns></returns>
        [HttpGet("get-messages-byReceiver")]
        public IActionResult GetMessagesByReceiver(string receiverEmail)
        {
            var thisMessages = new List<MessageInfo>();
            ReadJson();
            thisMessages = _messages.Where(x => (x.ReceiverEmail == receiverEmail)).ToList();
            // thisMessages.Sort();
            return Ok(thisMessages);
        }
        /// <summary>
        /// Метод дессериализации
        /// </summary>
        private void ReadJson()
        {
            try
            {
                using (var fsRead = new FileStream("message.json", FileMode.Open, FileAccess.Read))
                {
                    var formatter = new DataContractJsonSerializer(typeof(MessageInfo[]));

                    if (fsRead.Length > 0)
                        _messages = ((MessageInfo[])formatter.ReadObject(fsRead)).ToList<MessageInfo>();

                }
            }
            catch
            {
                Console.WriteLine("Error Reading");
            }
        }

    }
}
