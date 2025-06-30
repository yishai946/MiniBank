using System.Globalization;
using System.Threading;

namespace MiniBank
{
    public class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            new MiniBankApplication().Run();
        }
    }
}
