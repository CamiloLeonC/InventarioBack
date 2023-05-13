using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Models.InteractionResult
{
    public class InteractionResult
    {
        public InteractionResult()
        {
        }

        public string Value { get; set; }
        public bool Success { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }
    }
}
