using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Test
{
    class User
    {
        public String UserId { get; set; }
        public string UserName { get; set; }
    }
    class TestClass
    {
        public void Test1()
        {
            var users = new List<User>()
            {
                new User(),new User()
            };

            var result = users.Where(x => x.UserId == "123456").ToList();
            var t = from x in users
                    where x.UserId == "123456"                                        
                    select users;
        }
    }
}
