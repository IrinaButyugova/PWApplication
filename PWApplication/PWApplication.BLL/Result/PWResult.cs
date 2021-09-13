using PWApplication.BLL.Errors;
using System.Collections.Generic;

namespace PWApplication.BLL.Result
{
    public class PWResult
    {
        public bool Succeeded { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
