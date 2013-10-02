using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemoStoreApp.Common
{
    public class SuspensionManager
    {
        public static async Task<string> LoadStateInformationAsync()
        {
            Random random = new Random();
            await Task.Delay(random.Next(1000, 3000));
            return "Async data loaded.";
        }
    }
}
