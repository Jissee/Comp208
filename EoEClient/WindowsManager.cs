
using EoE.Client.Login;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace EoE.Client
{
    public class WindowsManager
    {
        public static WindowsManager INSTANCE { get; private set; }

        private Dictionary<string,Window> WindowsDict = new Dictionary<string,Window>();
        static WindowsManager()
        {
            INSTANCE = new WindowsManager();
        }

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LoginWindow());
        }
        public void ShowWindows<T>() where T : Window, new()
        {
            Type t = typeof(T);
            string typeName = t.FullName;

            if (!WindowsDict.ContainsKey(typeName)) 
            {
                WindowsDict.Add(typeName, new T());
            }
            Window window = WindowsDict[typeName];
            if (!window.IsVisible)
            {
                window.Show();
            }
            
        }
    }
}
