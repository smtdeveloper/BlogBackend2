using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogBackend2.Utilities.Results
{
    public class DataResult<T> : Result
    {
        public T Data { get; set; }

        public DataResult(bool success) : base(success)
        {
        }

        public DataResult(bool success, string message) : base(success, message)
        {
        }

        public DataResult(bool success, string message, T data) : base(success, message)
        {
            this.Data = data;
        }
    }
}
