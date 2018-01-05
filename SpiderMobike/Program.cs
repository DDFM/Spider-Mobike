using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpiderMobike
{
    class Program
    {
        private static string _url = "https://mwx.mobike.com/mobike-api/rent/nearbyBikesInfo.do";
        private static string _cityCode = "010";
        static void Main(string[] args)
        {
            // var mobike4s = Comm.FormatterComm.Deserialize<List<MobikeInfo>>(System.IO.File.ReadAllText("四环.txt"));

            Dictionary<string, string> heasers = new Dictionary<string, string>
            {
                { "time", DateTime.Now.Ticks.ToString() },
                { "citycode", _cityCode },
                { "host", "mwx.mobike.com" },
                { "content-type", "application/x-www-form-urlencoded" },
                { "opensrc", "list" },
                { "mobileno", "" },
                { "wxcode", "fake wxcode" },
                { "platform", "3" },
                { "accept-language", "zh-cn" },
                { "subsource", "" },
                { "lang", "zh" },
                { "user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0_3 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Mobile/15A432 MicroMessenger/6.5.19 NetType/WIFI Language/zh_CN" },
                { "referer", "https://servicewechat.com/fake/137/page-frame.html" }
            };

            //二环
            //116.364106,39.952578
            //116.442582,39.910972
            //三环
            //116.313801,39.973814
            //116.466441,39.862695
            //四环
            //116.279306,39.98841
            //116.49145,39.842754
            decimal top = 39.98841m,//大
                bottom = 39.842754m,//小
                left = 116.279306m,//小
                right = 116.49145m,//大
                offset = 0.002m;
            ConcurrentQueue<MobikeInfo> mobikeInfoQueue = new ConcurrentQueue<MobikeInfo>();
            List<MobikeInfo> mobikeInfoList = new List<MobikeInfo>();
            List<Task> tasks = new List<Task>();

            for (int verticalblock = 0; verticalblock < (top - bottom) / offset; verticalblock++)
            {
                for (int horizontalblock = 0; horizontalblock < (right - left) / offset; horizontalblock++)
                {
                    var longitude = 116.440085m + offset * verticalblock;
                    var latitude = 39.975266m + offset * horizontalblock;
                    try
                    {
                        Dictionary<string, object> para = new Dictionary<string, object>
                        {
                            { "verticalAccuracy", "10" },
                            { "speed", "-1" },
                            { "horizontalAccuracy", "65" },
                            { "accuracy", "2000" },
                            { "errMsg", "getLocation:ok" },
                            { "citycode", _cityCode },
                            { "wxcode", "fake wxcode" },
                            { "altitude", "46.802894592285156" }
                        };
                        para["longitude"] = longitude;
                        para["latitude"] = latitude;
                        var http = new Comm.HttpComm
                        {
                            KeepAlive = false,
                            Method = "post",
                            Headers = heasers,
                            Params = GetParam(para)
                        };
                        http.Request(_url);

                        var mobikes = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(Encoding.UTF8.GetString(http.ResponseBts))["object"];

                        foreach (var mobike in mobikes)
                        {
                            mobikeInfoList.Add(mobike.ToObject<MobikeInfo>());
                        }
                        Console.WriteLine(mobikeInfoList.Count + "------" + "------" + mobikes.Count);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("发生错误：" + ex.Message);
                    }
                }
            }
            //mobikeInfoList = mobikeInfoList.Distinct().ToList();
            //var bts = Encoding.UTF8.GetBytes(Comm.FormatterComm.Serialize(mobikeInfoList));
            //using (var file = System.IO.File.OpenWrite("三环.txt"))
            //{
            //    file.Write(bts, 0, bts.Length);
            //}
            Console.Write("成功");
            Console.Read();

        }

        private static string GetParam(Dictionary<string, object> para)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var item in para)
            {
                if (i > 0)
                {
                    stringBuilder.Append("&");
                }
                stringBuilder.Append(string.Format("{0}={1}", item.Key, item.Value));
                i++;
            }
            return stringBuilder.ToString();
        }
    }
}
