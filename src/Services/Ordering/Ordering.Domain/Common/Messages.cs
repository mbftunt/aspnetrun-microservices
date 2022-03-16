using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public class Messages
    {
        readonly static Messages _messages;


        public Messages()
        {
        }
        public static Messages Instance()
        {
            return _messages ?? new Messages();
        }
        //Comment by TUNT19 Message for Validator

        public string ERROR_NOT_EMPTY = "{property} is not empty.";
        public string ERROR_IS_REQUIRED = "{property} is required.";
        public string ERROR_MAX_LENGTH = "{property} must not exceed {maxLength} characters.";
        public string ERROR_GREATER = "{property} must greater than {value}.";
        public string ERROR_VALUE_FORMAT_EMAIL = "value must be email address.";

        //End comment by TUNT19 Message for Validator

        //Comment by TUNT19 Message for Logger

        public string INFO_UPDATE_SUCCES = "{item} {id} is successfully updated.";
        public string INFO_DELETE_SUCCES = "{item} {id} is successfully delete.";
        public string ERROR_NOT_EXISTS_IN_DATABASE = "{item} not exists on database";

        //End comment by TUNT19 Message for Logger


    }
}
