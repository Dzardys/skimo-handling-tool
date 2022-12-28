using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace KundovinaCZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static void errorBox(string error)
        {
            if(MessageBox.Show(error) == DialogResult.OK)
            {
                Application.Exit();
            }
           
        }
        private static void checkSkimoFiles()
        {
            var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Multi Theft Auto: San Andreas All\\Common");
            if (key == null)
            {
                errorBox("Nemáš nainstalované MTA!");
            }

            var p = key?.GetValue("File Cache Path") as string;

            if (string.IsNullOrEmpty(p))
            {
                errorBox("Během získávání cesty k MTA nastala chyba!");
            }

            string path = p + "/resources/";
            string[] subdirs = Directory.GetDirectories(path).Select(Path.GetFileName).ToArray();

            if (subdirs.Length > 0)
            {
                bool found = false;

                foreach (string folder in subdirs)
                {
                    if (folder == "skimo_updater") { found = true; break; }
                }

                if (!found)
                {
                    errorBox("Nejdřív se budeš muset alespoň jednou připojit na Skimo!");
                }
            }
            else
            {
                errorBox("Tvá resources složka je prázdná! Zkus se připojit na Skimo, aby se ti znovu stáhly všechny potřebné soubory");
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkSkimoFiles();
        }
    }
}
