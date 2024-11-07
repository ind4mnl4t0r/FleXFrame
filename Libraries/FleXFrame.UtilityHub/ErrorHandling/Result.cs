using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.UtilityHub.ErrorHandling
{
    // Result class definition
    public class Result<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess => Error == null;

        public static Result<T> Success(T data) => new Result<T> { Data = data };
        public static Result<T> Failure(string error) => new Result<T> { Error = error };
    }
}
