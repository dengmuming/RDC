using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDC
{
    public partial class FEKey : UserControl
    {
        private int RedisIndex { set; get; }
        private int DbIndex { set; get; }
        private string Key { set; get; }
        private string Type { set; get; }

        public FEKey()
        {
            InitializeComponent();
        }

        private void InitCommom(string key, string type, int redisIndex, int dbIndex)
        {
            //初始化布局
            uiPanel2.Hide();
            uiPanel4.Show();
            uiPanel4.Dock = DockStyle.Fill;
            //初始化变量
            this.RedisIndex = redisIndex;
            this.DbIndex = dbIndex;
            this.Key = key;
            this.Type = type;
            uiTextBox2.Text = key;
            //插入行
            uiSymbolButton1.Visible = type != "String";
        }

        public void Init(string key, string value, string type, int redisIndex, int dbIndex)
        {
            if (value == null)
            {
                return;
            }
            InitCommom(key, type, redisIndex, dbIndex);
            //初始化文本
            uiTextBox1.Clear();
            uiTextBox1.AppendText(value.ToString());
        }

        public void Init(string key, RedisValue[] list, string type, int redisIndex, int dbIndex)
        {
            if (list == null)
            {
                return;
            }
            InitCommom(key, type, redisIndex, dbIndex);
            //初始化列表
            uiDataGridView1.RowHeadersVisible = false;     //首列不显示
            uiDataGridView1.AllowUserToAddRows = false;    //空行不显示
            uiDataGridView1.Rows.Clear();                  //行清空
            uiDataGridView1.Columns.Clear();               //列清空
            uiDataGridView1.Columns.Add("Row", "Row");     //列添加
            uiDataGridView1.Columns.Add("Value", "Value");
            uiDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; //宽度自适应
            int row = 1;
            foreach (var val in list)
            {
                int index = uiDataGridView1.Rows.Add();
                uiDataGridView1.Rows[index].Cells[0].Value = row;
                uiDataGridView1.Rows[index].Cells[1].Value = val;
                row += 1;
            }
        }

        public void Init(string key, SortedSetEntry[] sortedSetEntry, string type, int redisIndex, int dbIndex)
        {
            if (sortedSetEntry == null)
            {
                return;
            }
            InitCommom(key, type, redisIndex, dbIndex);
            //初始化列表
            uiDataGridView1.RowHeadersVisible = false;     //首列不显示
            uiDataGridView1.AllowUserToAddRows = false;    //空行不显示
            uiDataGridView1.Rows.Clear();                  //行清空
            uiDataGridView1.Columns.Clear();               //列清空
            uiDataGridView1.Columns.Add("Row", "Row");     //列添加
            uiDataGridView1.Columns.Add("Value", "Value");
            uiDataGridView1.Columns.Add("Score", "Score");
            uiDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; //宽度自适应
            int row = 1;
            foreach (var val in sortedSetEntry)
            {
                int index = uiDataGridView1.Rows.Add();
                uiDataGridView1.Rows[index].Cells[0].Value = row;
                uiDataGridView1.Rows[index].Cells[1].Value = val.Element;
                uiDataGridView1.Rows[index].Cells[2].Value = val.Score;
                row += 1;
            }
        }

        public void Init(string key, HashEntry[] hashEntry, string type, int redisIndex, int dbIndex)
        {
            if (hashEntry == null)
            {
                return;
            }
            InitCommom(key, type, redisIndex, dbIndex);
            //初始化列表
            uiDataGridView1.RowHeadersVisible = false;     //首列不显示
            uiDataGridView1.AllowUserToAddRows = false;    //空行不显示
            uiDataGridView1.Rows.Clear();                  //行清空
            uiDataGridView1.Columns.Clear();               //列清空
            uiDataGridView1.Columns.Add("Row", "Row");     //列添加
            uiDataGridView1.Columns.Add("Key", "Key");
            uiDataGridView1.Columns.Add("Value", "Value");
            uiDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;                 //宽度自适应
            uiDataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //单元格居中
            int row = 1;
            foreach (var val in hashEntry)
            {
                int index = uiDataGridView1.Rows.Add();
                uiDataGridView1.Rows[index].Cells[0].Value = row;
                uiDataGridView1.Rows[index].Cells[1].Value = val.Name;
                uiDataGridView1.Rows[index].Cells[2].Value = val.Value;
                row += 1;
            }
        }

        private void uiLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
