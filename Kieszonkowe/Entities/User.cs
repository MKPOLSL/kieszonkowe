using System;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
