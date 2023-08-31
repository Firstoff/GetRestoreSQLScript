using System;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace lesson9_methods
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        void GenScript(string targetbase)
        {
            metroTextBox2.Text = $"" +
                $"USE master\r\n" +
                $"ALTER DATABASE [{targetbase}]\r\n" +
                $"SET SINGLE_USER WITH ROLLBACK IMMEDIATE\r\n" +
                $"\r\n" +
                $"-- RESTORE SET - этот скрипт в буфере обмена\r\n" +
                $"\r\n" +
                $"ALTER DATABASE [{targetbase}] SET MULTI_USER\r\n" +
                $"GO\r\n" +
                $"\r\n" +
                $"-- shrink log\r\n" +
                $"\r\n" +
                $"USE [{targetbase}]\r\n" +
                $"DBCC SHRINKFILE(2 , 0, TRUNCATEONLY)\r\n" +
                $"\r\n" +
                $"--gmsa user\r\n" +
                $"\r\n" +
                $"CREATE USER [IE\\gmsa-test1c$]\r\n" +
                $"ALTER ROLE[db_owner] ADD MEMBER [IE\\gmsa-test1c$]\r\n" +
                $"GO\r\n";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            GenScript(metroTextBox1.Text);
            Clipboard.SetText(metroTextBox2.Text);
            metroLabel1.Text = "Текст в буфере обмена";
        }

    }
}
