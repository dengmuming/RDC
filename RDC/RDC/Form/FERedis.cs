using Newtonsoft.Json;
using Util;
using Sunny.UI;
using System;
using System.Collections.Generic;

namespace RDC
{
    public partial class FERedis : UIEditForm
    {
        public delegate void AddEvent(string name);
        public event AddEvent Add;
        public static int notEdit = -1;
        public static int TreeIndex { set; get; }
        public FERedis(int index)
        {
            InitializeComponent();
            TreeIndex = index;
            //初始化表单
            if (TreeIndex >= 0)
            {
                RedisConfig conf = RedisClient.ConfGet(index);
                string passWord = conf.passWord != string.Empty ? conf.passWord : "";
                textName.AppendText(conf.name);
                textHost.AppendText(conf.host);
                textPort.AppendText(conf.port);
                textPassWord.AppendText(passWord);
                textUser.AppendText(conf.user);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //读取配置
            List<RedisConfig> jsonList = RedisClient.ConfGet();
            //Redis配置实例
            RedisConfig cr = new RedisConfig(textHost.Text, textPort.Text, textPassWord.Text, textName.Text, textUser.Text);
            //编辑
            if (TreeIndex >= 0)
            {
                jsonList[TreeIndex] = cr;
            }
            else//新增
            {
                jsonList.Add(cr);
            }
            string str = JsonConvert.SerializeObject(jsonList);
            //清空文本
            System.IO.File.WriteAllText(File.path, string.Empty);
            //写入配置
            File.WriteJsonFile(File.path, str);
            //event事件
            if (Add != null)
            {
                this.Add(textName.Text.ToString());
            }
            //关闭窗体
            //this.Close();
        }
    }
}
