using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDBAPI.Models
{
    public class UserBL
    {
        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "Mukul",
                Password = "123@qwe"
            });

            userList.Add(new User()
            {
                ID = 101,
                UserName = "Anubhav",
                Password = "456@qwe"
            });

            return userList;

        }
    }
}