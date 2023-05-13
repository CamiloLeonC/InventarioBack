using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace General_back.Models.Dto
{
    public class LoginInteractionResult
    {
        public LoginInteractionResult()
        {
        }

        public string Value { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }
    }
}
