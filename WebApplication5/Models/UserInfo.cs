using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    /// <summary>
    /// Модель юзера!
    /// </summary>
    public class UserInfo : IComparable<UserInfo>
    {
        /// <summary>
        /// имеил!
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Имя!
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">собщение для сравнения</param>
        /// <returns></returns>
        public int CompareTo(UserInfo message)
        {
            return this.Email.CompareTo(message.Email);
        }
    }
}
