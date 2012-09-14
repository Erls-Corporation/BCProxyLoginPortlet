using System;

namespace BCProxyLogin
{
    public class Check : Object
    {
        public bool success { get; set; }
        public string reason { get; set; }

        public Check(bool success, string reason)
        {
            this.success = success;
            this.reason = reason;
        }
        public Check(bool success)
        {
            this.success = success;
        }
    }
}