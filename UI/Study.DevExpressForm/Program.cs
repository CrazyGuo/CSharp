using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

using MySql.Data.MySqlClient;
namespace Study.DevExpressForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MySqlConnection con = new MySqlConnection();
            //Type t = con.GetType();

            //Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
            //Type t1 = conn.GetType();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.UserSkins.BonusSkins.Register();

            Application.Run(new JobSearchForm());
        }
    }
}