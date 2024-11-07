using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.UtilityHub.ErrorHandling
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public string[]? Errors { get; set; } // Optional: To hold multiple error messages if needed
    }
}
