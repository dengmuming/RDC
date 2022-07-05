using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Sunny.UI;
using Util;
using System.Linq;
using System.Drawing;
using StackExchange.Redis;

namespace RDC
{
    public partial class FormMain : UIForm
    {
        public FEKey FEKeyC; //创建用户控件一变量
        public FormMain()
        {
            InitializeComponent();
            //鼠标右键菜单
            //二级菜单
            ToolStripMenuItem blue = new ToolStripMenuItem("Blue");
            ToolStripMenuItem green = new ToolStripMenuItem("Green");
            ToolStripMenuItem orange = new ToolStripMenuItem("Orange");
            ToolStripMenuItem red = new ToolStripMenuItem("Red");
            ToolStripMenuItem gray = new ToolStripMenuItem("Gray");
            ToolStripMenuItem purple = new ToolStripMenuItem("Purple");
            ToolStripMenuItem layuiGreen = new ToolStripMenuItem("LayuiGreen");
            ToolStripMenuItem layuiRed = new ToolStripMenuItem("LayuiRed");
            ToolStripMenuItem layuiOrange = new ToolStripMenuItem("LayuiOrange");
            ToolStripMenuItem darkBlue = new ToolStripMenuItem("DarkBlue");
            ToolStripMenuItem black = new ToolStripMenuItem("Black");
            ToolStripMenuItem colorful = new ToolStripMenuItem("Colorful");

            //一级菜单
            ToolStripMenuItem mnuprint = new ToolStripMenuItem("主题");
            mnuprint.DropDownItems.Add(blue);
            mnuprint.DropDownItems.Add(green);
            mnuprint.DropDownItems.Add(orange);
            mnuprint.DropDownItems.Add(red);
            mnuprint.DropDownItems.Add(gray);
            mnuprint.DropDownItems.Add(purple);
            mnuprint.DropDownItems.Add(layuiGreen);
            mnuprint.DropDownItems.Add(layuiRed);
            mnuprint.DropDownItems.Add(layuiOrange);
            mnuprint.DropDownItems.Add(darkBlue);
            mnuprint.DropDownItems.Add(black);
            mnuprint.DropDownItems.Add(colorful);
            uiContextMenuStrip1.Items.Add(mnuprint);

            //二级菜单事件
            mnuprint.DropDownItemClicked += delegate (object sender, ToolStripItemClickedEventArgs e)
            {
                _ThemeClick(sender, e);
            };

            //菜单追加到右上角倒三角按钮
            this.ExtendMenu = uiContextMenuStrip1;
            treeRedis.AutoScroll = true;
            //配置树一级节点初始化
            List<RedisConfig> jsonList = RedisClient.ConfGet();
            if (jsonList is null)
            {
                return;
            }
            foreach (RedisConfig conf in jsonList)
            {
                TreeNode treeNode = new TreeNode(conf.name);
                treeRedis.Nodes.Add(treeNode);
            }

            //实例化用户控件
            FEKeyC = new FEKey();
        }
        
        //倒三角二级菜单事件
        private void _ThemeClick(object sender, ToolStripItemClickedEventArgs e)
        {
            //根据enum的键获得enum类型
            UIStyle typeValue = (UIStyle)Enum.Parse(typeof(UIStyle), e.ClickedItem.Text, true);
            uiStyleManager1.Style = typeValue;
            //跟随主题的提示框
            UIMessageTip.ShowOk(UILocalize.SuccessTitle);
        }

        private void uiContextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                //MessageBox.Show("ok");
            }
        }

        //Redis链接新增按钮
        private void uiButton1_Click(object sender, EventArgs e)
        {
            FERedis fer = new FERedis(FERedis.notEdit);
            fer.Add += redisAdd;
            fer.Render();
            fer.ShowDialog();
            //if (fer.IsOK)
            //{
            //    this.ShowSuccessDialog(fer.tree);
            //}
            //fer.Dispose();
        }

        private void redisAdd(string name)
        {
            TreeNodeCollection nodes = treeRedis.Nodes;
            if (name == string.Empty)
            {
                return;
            }
            //新增
            if (FERedis.TreeIndex < 0)
            {
                TreeNode treeNode = new TreeNode(name);
                treeRedis.Nodes.Add(treeNode);
                UIMessageDialog.ShowMessageDialog("修改Redis连接" + UILocalize.SuccessTitle, UILocalize.InfoTitle, false, Style);
                return;
            }
            //编辑
            nodes[FERedis.TreeIndex].Text = name;
            UIMessageDialog.ShowMessageDialog("添加Redis连接" + UILocalize.SuccessTitle, UILocalize.InfoTitle, false, Style);
        }

        private void _TreeRedisInit(TreeNode node, int index)
        {
            string res = RedisClient.Init(index);
            if (res != string.Empty)
            {
                UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res,Style);
                return;
            }
            //添加二级节点
            foreach (var dbNum in RedisClient.DbList)
            {
                res = RedisClient.GetDatabase(dbNum);
                if (res != string.Empty)
                {
                    UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res, Style);
                    return;
                }
                RedisConfig conf = RedisClient.ConfGet(index);
                List<string> keys = RedisClient.DbKeysList(conf, dbNum);
                TreeNode treeNode1 = new TreeNode(dbNum.ToString() + "(" + keys.Count().ToString() + ")");
                node.Nodes.Add(treeNode1);
            }
        }

        private void _TreeDbInit(TreeNode node, int dbIndex, int treeCount = 0)
        {
            Console.WriteLine(string.Format("一级节点是：{0}，一级节点Index是：{1}", node.Parent.Text, node.Parent.Index));

            //redis连接
            string res = RedisClient.Init(node.Parent.Index, dbIndex);
            if (res != string.Empty)
            {
                UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res, Style);
                return;
            }
            RedisConfig conf = RedisClient.ConfGet(node.Parent.Index);
            List<string> keys = RedisClient.DbKeysList(conf, dbIndex);
            //已有内容不更新
            if (treeCount > 0)
            {
                return;
            }
            foreach (var key in keys)
            {
                TreeNode treeNode = new TreeNode(key);
                node.Nodes.Add(treeNode);
            }
        }
        
        private void _TreeKeyInit(TreeNode node)
        {
            //Reids Key 类型
            string key = node.Text.ToString();
            RedisType keyType = RedisClient.Db.KeyType(key);
            int redisIndex = node.Parent.Parent.Index;
            int dbIndex = node.Parent.Index;
            RedisValue[] members;

            //Console.WriteLine(string.Format("parentIndex是{0}, treeIndex是{1}", parentIndex, treeIndex));
            switch (keyType)
            {
                case RedisType.None://None.The specified key does not exist.
                case RedisType.String:
                    RedisValue value = RedisClient.Db.StringGet(node.Text.ToString());
                    FEKeyC.Init(key, value, "String", redisIndex, dbIndex);
                    break;
                case RedisType.List:
                    members = RedisClient.Db.ListRange(node.Text.ToString(), 0, 100);
                    FEKeyC.Init(key, members, "List", redisIndex, dbIndex);
                    break;
                case RedisType.Set:
                    members = RedisClient.Db.SetMembers(node.Text.ToString());
                    FEKeyC.Init(key, members, "Set", redisIndex, dbIndex);
                    break;
                case RedisType.SortedSet:
                    SortedSetEntry[] sortedSetEntry = RedisClient.Db.SortedSetRangeByRankWithScores(node.Text.ToString());
                    FEKeyC.Init(key, sortedSetEntry, "Zset", redisIndex, dbIndex);
                    //f2.init_List(key, sortedSetEntry, "Zset", parentIndex, treeIndex);
                    break;
                case RedisType.Hash:
                    HashEntry[] hashEntry = RedisClient.Db.HashGetAll(node.Text.ToString());
                    FEKeyC.Init(key, hashEntry, "Hash", redisIndex, dbIndex);
                    break;
                case RedisType.Stream://The data-type was not recognised by the client library.
                case RedisType.Unknown:
                    break;
            }
            FEKeyC.Show();                   //将窗体一进行显示
            FEKeyC.Dock = DockStyle.Fill;    //控件填充到Panel中
            uiPanel5.Controls.Clear();       //清空原容器上的控件
            uiPanel5.Controls.Add(FEKeyC);   //将窗体一加入容器panel2
        }

        private void TreeRedis_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Console.WriteLine(string.Format("点击节点是：{0}，节点级别是：{1}", e.Node.Text, e.Node.Level));
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                int level = e.Node.Level;
                int treeIndex = e.Node.Index;
                int treeCount = e.Node.Nodes.Count;
                TreeNode node = e.Node;
                //一级redis节点
                if (level == 0)
                {
                    if (treeCount > 0)
                    {
                        return;
                    }
                    _TreeRedisInit(node, treeIndex);
                    return;
                }

                //二级db节点
                if (level == 1)
                {
                    _TreeDbInit(node, treeIndex, treeCount);
                }

                //三级节点
                if (level == 2)
                {
                    //redis连接
                    string res = RedisClient.Init(e.Node.Parent.Parent.Index, e.Node.Parent.Index);
                    if (res != string.Empty)
                    {
                        UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res, Style);
                        return;
                    }
                    _TreeKeyInit(node);
                }
            }


            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeRedis.GetNodeAt(ClickPoint);
                if (CurrentNode == null)//判断你点的是不是一个节点
                {
                    return;
                }

                ContextMenuStrip ms = new ContextMenuStrip();
                ms.Items.Add("刷新");
                switch (e.Node.Level)
                {
                    case 0://一级节点
                        ms.Items.Add("展开/折叠");
                        ms.Items.Add("编辑");
                        ms.Items.Add("删除");
                        break;
                    case 1://二级节点
                        ms.Items.Add("展开/折叠");
                        ms.Items.Add("新增");
                        break;
                    case 2://三节节点
                        ms.Items.Add("删除");
                        break;
                    default:
                        return;
                }
                ms.ItemClicked += new ToolStripItemClickedEventHandler(_TreeRightClick);
                CurrentNode.ContextMenuStrip = ms;
                treeRedis.SelectedNode = CurrentNode;//选中这个节点
            }
        }

        private void _TreeRightClick(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine(e.ClickedItem.Text);
            string clickText = e.ClickedItem.Text;
            TreeNode node = treeRedis.SelectedNode;
            int level = node.Level;
            int redisIndex;
            int dbIndex;
            int dbCount;
            int keyIndex;
            int keyCount;
            string res;
            bool flag;
            TreeNode redisNode;
            TreeNode dbNode;
            TreeNode keyNode;
            switch (level)
            {
                case 0://一级节点
                    redisNode = node;
                    redisIndex = redisNode.Index;
                    dbCount = redisNode.Nodes.Count;
                    if (clickText == "刷新")
                    {
                        node.Nodes.Clear();
                        _TreeRedisInit(redisNode, redisIndex);
                        node.Expand();
                    }
                    else if (clickText == "展开/折叠")
                    {
                        if (node.IsExpanded == true)//展开状态
                        {
                            node.Collapse();
                            break;
                        }
                        if (dbCount <= 0)
                        {
                            node.Nodes.Clear();
                            _TreeDbInit(redisNode, redisIndex);
                        }
                        node.Expand();
                    }
                    else if (clickText == "编辑")
                    {
                        //子窗体
                        FERedis fERedis = new FERedis(redisIndex);
                        fERedis.Add += redisAdd;
                        //展示子窗体
                        fERedis.ShowDialog();
                    }
                    else if (clickText == "删除")
                    {
                        res = RedisClient.ConfDelete(redisIndex);
                        if (res != string.Empty)
                        {
                            UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res, Style);
                            return;
                        }
                        //tree清理
                        treeRedis.Nodes.Remove(redisNode);
                        UIMessageDialog.ShowSuccessDialog(this, UILocalize.SuccessTitle, UILocalize.OK, Style);
                    }
                    break;
                case 1://二级节点
                    dbNode = node;
                    dbIndex = dbNode.Index;
                    keyCount = dbNode.Nodes.Count;
                    redisIndex = dbNode.Parent.Index;
                    if (clickText == "刷新")
                    {
                        dbNode.Nodes.Clear();
                        _TreeDbInit(dbNode, dbIndex);
                        dbNode.Expand();
                    }
                    else if (e.ClickedItem.Text == "展开/折叠")
                    {
                        if (dbNode.IsExpanded == true)//展开状态
                        {
                            dbNode.Collapse();
                            break;
                        }
                        if (keyCount <= 0)
                        {
                            dbNode.Nodes.Clear();
                            _TreeDbInit(dbNode, dbIndex);
                        }
                        dbNode.Expand();
                    }
                    else if (e.ClickedItem.Text == "新增")
                    {
                        //子窗体
                        FEKeyAdd fEKeyAdd = new FEKeyAdd(redisIndex, dbIndex);
                        fEKeyAdd.Add += KeyAdd;
                        //展示子窗体
                        fEKeyAdd.ShowDialog();
                    }
                    break;
                 case 2://三级节点node.Parent.Parent
                    keyNode = node;
                    keyIndex = keyNode.Index;
                    dbNode = node.Parent;
                    dbIndex = dbNode.Index;
                    redisNode = dbNode.Parent;
                    redisIndex = redisNode.Index;
                    res = RedisClient.Init(redisIndex, dbIndex);
                    TreeNode parentNode = node.Parent;
                    if (res != string.Empty)
                    {
                        UIMessageDialog.ShowWarningDialog(this, UILocalize.ErrorTitle, res, Style);
                        return;
                    }

                    if (e.ClickedItem.Text == "删除")
                    {
                        flag = RedisClient.Db.KeyDelete(node.Text);
                        if (flag == false)
                        {
                            MessageBox.Show("删除失败，请刷新后重试");
                            return;
                        }
                        node.Parent.Nodes.Clear();
                        _TreeDbInit(dbNode, redisIndex);
                        dbNode.Expand();
                        MessageBox.Show("删除成功！");
                    }
                    else if (e.ClickedItem.Text == "刷新")
                    {
                        _TreeKeyInit(node);
                    }
                    break;
                default:
                    return;
            }
        }

        public void KeyAdd(string name)
        {
            TreeNode node = treeRedis.Nodes[FEKeyAdd.RedisIndex].Nodes[FEKeyAdd.DbIndex];
            node.Nodes.Clear();
            _TreeDbInit(node, FEKeyAdd.DbIndex);
            //Todo 自动点击新增的key
        }
    }
}
