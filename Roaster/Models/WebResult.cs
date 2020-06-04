using Roaster.Enums;
using System;

namespace Roaster.Models
{
    public class WebResult<T>
    {
        public ResultStatus Status { get; set; }
        public T Result { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
