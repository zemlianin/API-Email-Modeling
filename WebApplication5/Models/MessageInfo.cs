using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    /// <summary>
    /// Собщение!
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// Имеил отправителя!
        /// </summary>
        public string SenderEmail { get; set; }
        /// <summary>
        /// меил получателя!
        /// </summary>
        public string ReceiverEmail { get; set; }
        /// <summary>
        /// имеил получателя!
        /// </summary>
        public string Message { get; set; }

    }
}
