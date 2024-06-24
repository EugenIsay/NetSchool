using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSchool
{
    public static class UserManager
    {
        public static List<User> Users = new List<User>();
        public static User curentUser = new User();
        public static void AddUser(string Name, string Password, string Role)
        {
            Users.Add(new User() { Name = Name, Password = Password, Role = Role });
        }
        public static bool CheckUser (string Name, string Password)
        {
            if (Users.FirstOrDefault(u => u.Name == Name && u.Password == Password) != null)
            {
                curentUser = Users.FirstOrDefault(u => u.Name == Name && u.Password == Password);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
