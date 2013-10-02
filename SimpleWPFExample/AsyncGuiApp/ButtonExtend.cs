using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsyncGuiApp
{
    public static class ButtonExtend
    {
        public static async Task WhenClickedAsync(this Button button)
        {
            var tcs = new TaskCompletionSource<bool>();
            RoutedEventHandler handler = (s, e) => tcs.TrySetResult(true);

            try
            {
                button.Click += handler;
                await tcs.Task;
            }
            finally
            {
                button.Click -= handler;
            }
        }
    }
}
