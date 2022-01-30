using Munters.Giphy.Interfaces;
using Munters.Giphy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Manager
{
    public class HttpManager : IHttpManager
    {
        public async Task<Result> GetData(Uri uri)
        {           
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var result = new Result(isSuccess: false, json: string.Empty);
                    var response = await httpClient.GetAsync(uri);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return new Result(isSuccess: false, ex.Message);
                }
            }
        }
    }
}
