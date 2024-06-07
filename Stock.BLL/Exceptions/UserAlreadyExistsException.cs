using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BLL.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {

        public List<string> ErrorMessages { get; set; }

        public UserAlreadyExistsException()
        {
            ErrorMessages = new List<string>()
            {
                "User already exists"
            };
        }

        public UserAlreadyExistsException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string error in ErrorMessages)
            {
                sb.AppendLine(error);
            }
            return sb.ToString();
        }
    }
}