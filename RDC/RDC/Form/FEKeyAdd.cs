using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace RDC
{
    public partial class FEKeyAdd : UIEditForm
    {
        public delegate void AddEvent(string name);
        public event AddEvent Add;
        public static int notEdit = -1;
        public static int RedisIndex { set; get; }
        public static int DbIndex { set; get; }
        public FEKeyAdd(int redisIndex, int dbIndex)
        {
            InitializeComponent();
            RedisIndex = redisIndex;
            DbIndex = dbIndex;
            //初始化下拉框
            ArrayList mylist = new ArrayList();
            mylist.Add(new DictionaryEntry(0, "String"));
            mylist.Add(new DictionaryEntry(1, "List"));
            mylist.Add(new DictionaryEntry(2, "Set"));
            mylist.Add(new DictionaryEntry(3, "Zset"));
            mylist.Add(new DictionaryEntry(4, "Hash"));
            mylist.Add(new DictionaryEntry(5, "Stream"));
            uiComboBox1.DataSource = mylist;
            uiComboBox1.DisplayMember = "Value";
            uiComboBox1.ValueMember = "Key";
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            //Redis Client
            RedisClient.Init(RedisIndex);
            RedisClient.GetDatabase(DbIndex);
            string key = uiTextBox1.Text.ToString();
            string value = uiTextBox2.Text.ToString();
            string textBox3 = uiTextBox3.Text.ToString();
            string type = uiComboBox1.Text.ToString();
            bool res;
            switch (type)
            {
                case "String":
                    res = RedisClient.Db.StringSet(key, value);
                    if (res == false)
                    {
                        UIMessageDialog.ShowErrorDialog(this, UILocalize.ErrorTitle, Style);
                        return;
                    }
                    UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, Style);
                    break;
                case "List":
                    _ = RedisClient.Db.ListLeftPush(key, value);
                    UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, Style);
                    break;
                case "Set":
                    res = RedisClient.Db.SetAdd(key, value);
                    if (res == false)
                    {
                        UIMessageDialog.ShowErrorDialog(this, UILocalize.ErrorTitle, Style);
                        return;
                    }
                    UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, Style);
                    break;
                case "Zset":
                    res = RedisClient.Db.SortedSetAdd(key, value, Convert.ToDouble(textBox3));
                    if (res == false)
                    {
                        UIMessageDialog.ShowErrorDialog(this, UILocalize.ErrorTitle, Style);
                        return;
                    }
                    UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, Style);
                    break;
                case "Hash":
                    res = RedisClient.Db.HashSet(key, textBox3, value);
                    if (res == false)
                    {
                        UIMessageDialog.ShowErrorDialog(this, UILocalize.ErrorTitle, Style);
                        return;
                    }
                    UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, Style);
                    break;
                default:
                    return;
            }
            //event事件
            if (this.Add != null)
            {
                this.Add(key);
            }
            this.Close();
        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(uiComboBox1.Text)
            {
                case "Zset":
                    uiLabel4.Text = "分数";
                    uiPanel2.Show();
                    break;
                case "Hash":
                    uiLabel4.Text = "Hash Key";
                    uiPanel2.Show();
                    break;
                case "Stream":
                    uiLabel4.Text = "ID";
                    uiPanel2.Show();
                    break;
                default:
                    uiPanel2.Hide();
                    break;
            }
        }
    }
}
