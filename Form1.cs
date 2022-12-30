using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SkimoHandlingTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static void alertBox(string type, string message)
        {
            if (MessageBox.Show(message) == DialogResult.OK)
            {
                if (type == "error")
                    Application.Exit();
            }

        }
        private static void checkSkimoFiles()
        {
            var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Multi Theft Auto: San Andreas All\\Common");
            if (key == null)
            {
                alertBox("error", "Nemáš nainstalované MTA!");
            }

            var p = key?.GetValue("File Cache Path") as string;

            if (string.IsNullOrEmpty(p))
            {
                alertBox("error", "Během získávání cesty k MTA nastala chyba!");
            }

            string path = p + "/resources/";
            try
            {
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
                        alertBox("error", "Nejdřív se budeš muset alespoň jednou připojit na Skimo!");
                    }
                }
                else
                {
                    alertBox("error", "Tvá resources složka je prázdná! Zkus se připojit na Skimo, aby se ti znovu stáhly všechny potřebné soubory");
                }
            }
            catch
            {
                Exception e = new Exception();
                alertBox("error", e.ToString());
            }


        }
        private void checkHeditPath()
        {
            //prompt po prvnim spuštění ukládající cestu ke složce s heditem
            //následný check, pokud tam ty soubory jsou, pokud ne, tak vyhodit znovu prompt a pak to přepsat v configu
            //následně znovu spustit celou funkci znovu a v případě úspěchu vypsat soubory do selectboxu
        }
        private void listAllHeditFiles()
        {

        }
        private void heditToLuaTable(string path, string title, int vehlib, int owner)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkSkimoFiles();
            checkHeditPath();
        }
    }
}
