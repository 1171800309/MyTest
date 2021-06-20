using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest1.ViewModel
{
    public class ResponseResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }

        public ResponseResult(string code, string msg, object data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
    }
}
