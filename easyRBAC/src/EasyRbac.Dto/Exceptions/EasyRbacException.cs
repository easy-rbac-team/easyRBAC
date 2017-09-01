using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EasyRbac.Dto.Exceptions
{
    public class EasyRbacException : Exception
    {
        public EasyRbacException()
        {
        }

        public EasyRbacException(string message) : base(message)
        {
        }

        public EasyRbacException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EasyRbacException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
