using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest1.Configuration
{
    public class ResponseCodeConfig
    {
        public ResponseCode Normal { get; set; }
        public ResponseCode NotFile { get; set; }
        public ResponseCode ServiceError { get; set; }
        public ResponseCode ParamError { get; set; }
        public ResponseCode SqlError { get; set; }
    }

    public class ResponseCode
    {
        public string Code { get; set; }
        public string Msg { get; set; }
    }
}
