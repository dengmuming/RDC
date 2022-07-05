using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Util
{

	public class RedisClient
	{
		public static ConnectionMultiplexer Redis { get; set; }
		public static IDatabase Db { get; set; }
		public static int DbSelet { get; set; }//当前选择的db
		public static int RedisSelect { get; set; }//当前链接的redis
		const int DefaultSelect = -1;
		public static int[] DbList = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

		public static string Init(int redisIndex, int dbIndex = DefaultSelect)
		{
			Console.WriteLine("{0},{1}", redisIndex, RedisSelect);
            try {
				//Redis Client
				if (redisIndex != RedisSelect || Redis is null)
				{
					RedisConfig rc = ConfGet(redisIndex);
					string config = String.Format("{0}:{1},password={2}", rc.host, rc.port, rc.passWord);
					Console.WriteLine(config);
					Redis = ConnectionMultiplexer.Connect(config);
					RedisSelect = redisIndex;
					if (dbIndex < 0)
					{
						return string.Empty;
					}
				}
				else if (dbIndex < 0)
				{
					return string.Empty;
				}

				//db初始化
				string err = GetDatabase(dbIndex);
				if (err != string.Empty)
				{
					return err;
				}
				DbSelet = dbIndex;
			}
			catch (RedisServerException e)
			{
				RedisSelect = DefaultSelect;
				DbSelet = DefaultSelect;
				return e.Message;
			}
			catch (Exception e)
			{
				RedisSelect = DefaultSelect;
				DbSelet = DefaultSelect;
				return e.Message;
			}
			return string.Empty;
		}

		public static string GetDatabase(int num)
		{
			Console.WriteLine(num);
			try
			{
				if (Redis is null)
				{
					return "redis连接失败";
				}
				Db = Redis.GetDatabase(num);
				return string.Empty;
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		//String Get
		public string StringGet(string key)
		{
			RedisValue res = Db.StringGet(key);
			if (res != String.Empty)
			{
				return res;
			}
			return "";
		}

		//Keys count.DBSIZE.
		//https://stackexchange.github.io/StackExchange.Redis/KeysScan
		public static List<string> DbKeysList(RedisConfig rc = null, int db = DefaultSelect)
		{
			int dbNum = DefaultSelect;
			if (db != DefaultSelect)
            {
				dbNum = db;
			}
			List<string> listKeys = new List<string>();
			try
			{
				IServer iServer = Redis.GetServer(rc.host, int.Parse(rc.port));
				var keys = iServer.Keys(dbNum);
				listKeys.AddRange(keys.Select(key => (string)key).ToList());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			return listKeys;
		}

		public static RedisConfig ConfGet(int index)
		{
			int count = 0;
			List<RedisConfig> jsonList = ConfGet();
			foreach (RedisConfig key in jsonList)
			{
				if (count == index)
				{
					return key;
				}
				count += 1;
			}
			return null;
		}

		public static string ConfDelete(int index)
        {
			string json = File.GetJsonFile(File.path);
			List<RedisConfig> jsonList = JsonConvert.DeserializeObject<List<RedisConfig>>(json);
			if (jsonList is null)
			{
				return "读取配置文件出错！";
			}
			jsonList.RemoveAt(index);
			string str = JsonConvert.SerializeObject(jsonList);
			//清空文本
			System.IO.File.WriteAllText(File.path, string.Empty);
			//写入配置
			File.WriteJsonFile(File.path, str);
			return string.Empty;
		}

		public static List<RedisConfig> ConfGet()
		{
			//读取配置
			string json = File.GetJsonFile(File.path);
			List<RedisConfig> jsonList = JsonConvert.DeserializeObject<List<RedisConfig>>(json);
			//规避空实例
			if (jsonList is null)
			{
				jsonList = new List<RedisConfig>();
			}
			return jsonList;
		}
	}

	public class RedisConfig
	{
		//构造方法，new时候自动执行
		public RedisConfig(string host = "", string port = "", string passWord = "", string name = "", string user = "")
        {
			this.host = host;
			this.port = port;
			this.passWord = passWord;
			this.name = name;
			this.user = user;
		}

		public string host { get; set; }
		public string port { get; set; }
		public string passWord { get; set; }
		public string name { get; set; }
		public string user { get; set; }
	}

	public class RedisList
	{
		public List<RedisConfig> configRedis { get; set; }
	}
}
