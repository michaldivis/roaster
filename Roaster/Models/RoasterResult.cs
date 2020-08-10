using Roaster.Enums;
using System;

namespace Roaster.Models
{
    public class RoasterResult<T>
    {
        public ResultStatus Status { get; set; }
        public T Data { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
