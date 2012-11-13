using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCProxyLogin
{
    public class Ret
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Ret()
        {
            Success = true;
        }
    }

    public class LoginRet : Ret
    {
        public string Url { get; set; }   
    }

}