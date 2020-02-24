using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.Exceptions
{
    public class ErroResponse
    {
        public string Message { get; set; }

        public string ToJson()
        {
            return "{\"message\":\"" + this.Message + "\"}";
        }
    }
}
