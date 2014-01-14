using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AsyncService
{
    public class CompositeService : ICompositeService
    {
        // Original/sync version
        public List<string> GetQuotes(int count)
        {
            var results = new List<string>();
            using (var client = new QuestionService.QuestionServiceClient())
            {
                for (int i = 0; i < count; ++i)
                    results.Add(client.GetQuote());
            }

            return results;
        }

        // Alternative async version - runs all 5 requests at once:
        //public async Task<List<string>> GetQuotesAsync(int count)
        //{
        //    using (var client = new QuestionService.QuestionServiceClient())
        //    {
        //        var tasks = new List<Task<string>>();

        //        for (int i = 0; i < count; ++i)
        //            tasks.Add(client.GetQuoteAsync());

        //        await Task.WhenAll(tasks);

        //        return tasks.Select(t => t.Result).ToList();
        //    }
        //}
    }
}
