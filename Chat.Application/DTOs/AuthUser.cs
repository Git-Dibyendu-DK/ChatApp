using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs
{
    public class AuthUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
