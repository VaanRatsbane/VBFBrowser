using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Browser.VBFTool;

namespace Browser
{
    static class Program
    {

        public static VirtuosBigFileReader reader;
        public static VFileSystem fileSystem;
        public static Queue<string> consoleLog;

        const int LOGSIZE = 50;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            consoleLog = new Queue<string>(LOGSIZE);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Log(string message)
        {
            if (consoleLog.Count == LOGSIZE)
                consoleLog.Dequeue();
            consoleLog.Enqueue(message);
        }

        public static bool LoadVBF(string path)
        {
            reader = new VirtuosBigFileReader();
            try
            {
                reader.LoadBigFileFile(path);
                fileSystem = new VFileSystem(path.Split('/').Last(), reader);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static bool ExtractFile(string virtualPath, string destPath)
        {
            try
            {
                return reader.ExtractFileContents(virtualPath, destPath);
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}
