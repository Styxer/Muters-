using Munters.Giphy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Interfaces
{
    public interface IHttpManager
    {
        Task<Result> GetData(Uri uri);
    }
}
