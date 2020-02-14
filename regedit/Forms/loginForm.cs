using IndigoSoftware.Properties;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using regedit;
using regeditChange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndigoSoftware.Forms
{
    public partial class loginForm : MaterialForm
    {
        //
        private readonly MaterialSkinManager materialSkinManager; // переменная для скина
        mainForm fav = new mainForm(); // fav. использование функций со следующей формы
        //

        public loginForm()
        {
            // загрузка
            InitializeComponent();
            //
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo600, Primary.Indigo600, Primary.Grey50, Accent.Indigo400, TextShade.WHITE);
            //
        }

        void loginForm_Load(object sender, EventArgs e) // выполнятся при загрузке формы
        {
            versionLbl.Text = Application.ProductVersion.ToString();
            //
            mainForm nextForm = new mainForm();
            // 
            nextForm.regeditPO_Load(sender, e);
        }

        void materialFlatButton1_Click(object sender, EventArgs e) // кнопка Продолжить
        {
            if (oznRadBut.Checked)
            {
                mainForm dal = new mainForm();
                this.Hide();
                dal.Show();
            }
            else
            {
                MaterialMessageBox.Show("Вам необходимо ознакомится с правилами использования, прежде чем использовать нашу программу!", fav.namepro, MessageBoxButtons.OK);
            }
        }

        void loginForm_FormClosed(object sender, FormClosedEventArgs e) // кнопка закрыть - закрыват программу
        {
            Application.Exit();
        }

        void pictureBox2_MouseMove(object sender, MouseEventArgs e) // меняет курсор на руку
        {
            Cursor.Current = Cursors.Hand;
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e) // меняет курсор на руку
        {
            Cursor.Current = Cursors.Hand;
        }

        void pictureBox1_Click(object sender, EventArgs e) // картинка-кнопка Вконтакте
        {
            if (MaterialMessageBox.Show("Вы уверены что хотите открыть браузер и перейти в нашу группу Вконтакте?", fav.namepro, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("https://vk.com/indigo.laptop");
            }
        }

        void pictureBox2_Click(object sender, EventArgs e) // картинка-кнопка Сайт
        {
            if (MaterialMessageBox.Show("Вы уверены что хотите открыть браузер и перейти на наш сайт?", fav.namepro, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("https://indigo-software.at.ua/");
            }
        }
    }
}
