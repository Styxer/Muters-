using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Model
{
    public class Result
    {
        public Result(bool isSuccess = false, string json = "")
        {
            IsSuccess = isSuccess;
            ResultJson = json;
        }

        public Result()
        {

        }

        public bool IsSuccess { get; set; }
        public string ResultJson { get; set; }

        public string Error { get; set; } = string.Empty;
    }
}
