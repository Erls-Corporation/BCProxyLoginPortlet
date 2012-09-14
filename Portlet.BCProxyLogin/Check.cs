using System;

namespace BCProxyLogin
{
    public class Check : Object
    {
        public bool Success { get; set; }
        public string Reason { get; set; }

        public Check(bool success, string reason)
        {
            Success = success;
            Reason = reason;
        }
        public Check(bool success)
        {
            Success = success;
        }
    }
}