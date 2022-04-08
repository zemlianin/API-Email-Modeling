using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    /// <summary>
    /// контроллер юзеров
    /// </summary>
    [Route("/api/[controller]")]

    public class UserInfoController : Controller
    {
        /// <summary>
        /// Лист юсеров
        /// </summary>
        internal static List<UserInfo> _users = new List<UserInfo>();

        /// <summary>
        /// Метод создания юзера
        /// </summary>
        /// <param name="req">данные о юзере</param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] CreateUserRequest req)
        {
            var user = new UserInfo()
            {
                Email = req.Email,
                UserName = req.UserName
            };

            ReadJson();

            _users.Add(user);
            using (var fsWrite = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                _users.Sort();
                JsonSerializer.SerializeAsync<List<UserInfo>>(fsWrite, _users);
                Console.WriteLine("Data has been saved to file");
            }

            return Ok(user);
        }
        /// <summary>
        /// Поиск бзера по имейлу
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpGet("get-user-by-Email")]
        public IActionResult GetUserByEmail([FromQuery] string Email)
        {
            ReadJson();
            var result = _users.Where(x => x.Email == Email).ToList();
            if (result.Count == 0)
            {
                return NotFound(new { Message = $"Пользователь с Email = {Email} не найден" });
            }
            return Ok(result.First());
        }
        /// <summary>
        /// Вывод всех юзеров
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            ReadJson();
            return Ok(_users);
        }
        /// <summary>
        /// Метод дессериалимзации
        /// </summary>
        private void ReadJson()
        {
            using (var fsRead = new FileStream("user.json", FileMode.Open, FileAccess.Read))
            {
                var formatter = new DataContractJsonSerializer(typeof(UserInfo[]));

                if (fsRead.Length > 0)
                    _users = ((UserInfo[])formatter.ReadObject(fsRead)).ToList<UserInfo>();

            }
        }
    }
}
