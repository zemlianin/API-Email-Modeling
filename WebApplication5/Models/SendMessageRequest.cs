using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public class SendMessageRequest
    {
        /// <summary>
        /// Имеил отправителя
        /// </summary>
        public string SenderEmail { get; set; }
        /// <summary>
        /// Имеил получателя
        /// </summary>
        public string ReceiverEmail { get; set; }
        /// <summary>
        /// Содержание
        /// </summary>
        public string Message { get; set; }


    }
}
