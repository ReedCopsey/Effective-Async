using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemoStoreApp.Common
{
    class FooService : IDisposable
    {
        public async Task<string> BarAsync()
        {
            Random random = new Random();
            await Task.Delay(random.Next(500, 1200));
            return "FooBarBaz!";
        }

        public void Dispose()
        {
            
        }
    }
}
