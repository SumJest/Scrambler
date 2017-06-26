using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace Scrambler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length<2)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Scrambler());
            }
            else
            {
                switch (args[0])
                {
                    case "/encode":
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Encode(args[1]));
                        break;
                    case "/decode":
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Decode(args[1]));
                        break;
                    default:
                        MessageBox.Show("Неизвестные аргументы!\nИспользуйте: \"/decode\" или \"/encode\" filepath");
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Scrambler());
                        break;
                }

            }



        }
    }
}
