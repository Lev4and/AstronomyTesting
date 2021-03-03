using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class User
    {
        public User()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
