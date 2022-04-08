using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    /// <summary>
    /// Модель юзера!
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Имя юзера!
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Его имеил!
        /// </summary>
        public string Email { get; set; }
    }
}
