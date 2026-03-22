using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using portfolio.UI;

namespace portfolio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var menuManager = new MenuManager();
                menuManager.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
