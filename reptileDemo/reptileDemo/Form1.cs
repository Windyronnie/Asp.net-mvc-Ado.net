using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using HtmlAgilityPack;

namespace reptileDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string htmlcode = GetHTML("http://www.111.com.cn/?tracker_u=20174044").Replace("\n", "").Replace("\r", "");
            DataTable dt_shop = UrlExtract(htmlcode);
            DataTable dt_good = UrlGood(htmlcode);
        }

        public static DataTable UrlGood(string url) 
        {
            url = url.Replace("&lt;", "<");
            url = url.Replace("&gt;", ">");
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(url);
            HtmlAgilityPack.HtmlNodeCollection html_shop = htmlDoc.DocumentNode.SelectNodes("//li[@class=\"stitle\" or @class=\"stitle last\"]");
            
            
            DataTable dt_good = new DataTable();

            dt_good.Columns.Add("Id", Type.GetType("System.String"));
            dt_good.Columns.Add("Name", Type.GetType("System.String"));
            dt_good.Columns.Add("SJId", Type.GetType("System.String"));
            dt_good.Columns.Add("Url", Type.GetType("System.String"));

            for (int i = 0; i < html_shop.Count; i++)
            {
                string ss = html_shop[i].ChildNodes["a"].Attributes["href"].Value;
                string[] sss = ss.Split('/');
                ss = sss[4].ToString();
                sss = ss.Split('-');
                ss = sss[0].ToString();

                string strs = html_shop[i].ChildNodes["textarea"].ChildNodes["div"].Id;
                HtmlAgilityPack.HtmlNodeCollection html_good = htmlDoc.DocumentNode.SelectNodes("//div[@id=\"" + strs + "\"]/div/dl");

                for (int j = 0; j < html_good.Count; j++)
                {
                    string str = "";
                    if (html_good[j].ChildNodes["dt"].ChildNodes[0].Attributes.Contains("href"))
                    {
                        string good_url = html_good[j].ChildNodes["dt"].ChildNodes["a"].Attributes["href"].Value;
                        str = html_good[j].ChildNodes["dt"].ChildNodes["a"].Attributes["href"].Value;
                        string[] strss = str.Split('/');
                        str = strss[4].ToString();
                        strss = str.Split('-');
                        str = strss[0].ToString();
                        dt_good.Rows.Add(str, html_good[j].ChildNodes["dt"].ChildNodes["a"].InnerText, ss, good_url);
                    }
                    else 
                    {
                        continue;
                    }
                    string small_good = "";

                    for (int y = 0; y < html_good[j].ChildNodes["dd"].ChildNodes.Count; y++)
                    {
                        if (html_good[j].ChildNodes["dd"].ChildNodes["em"].ChildNodes["a"].Attributes.Contains("href"))
                        {
                            if (html_good[j].ChildNodes["dd"].ChildNodes[y].Name == "em") 
                            {
                                string small_good_url = html_good[j].ChildNodes["dd"].ChildNodes[y].ChildNodes["a"].Attributes["href"].Value;
                                small_good = html_good[j].ChildNodes["dd"].ChildNodes[y].ChildNodes["a"].Attributes["href"].Value;
                                string[] small_goods = small_good.Split('/');
                                small_good = small_goods[4].ToString();
                                small_goods = small_good.Split('-');
                                small_good = small_goods[0].ToString();
                                dt_good.Rows.Add(small_good, html_good[j].ChildNodes["dd"].ChildNodes[y].ChildNodes["a"].InnerText, str, small_good_url);
                            }
                            
                        }
                        
                    }

                }
            }

            return dt_good;
        }

        /// <summary>
        /// 找到对应的商品分类
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DataTable UrlExtract(string url) 
        {
            url = url.Replace("&lt;", "<");
            url = url.Replace("&gt;", ">");
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(url);
            HtmlAgilityPack.HtmlNodeCollection htmlnode = htmlDoc.DocumentNode.SelectNodes("//li[@class=\"stitle\" or @class=\"stitle last\" ]/a");

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.String"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Url", Type.GetType("System.String"));

            for (int i = 0; i < htmlnode.Count; i++)
            {
                string urls = htmlnode[i].Attributes["href"].Value;
                string ss = htmlnode[i].Attributes["href"].Value;
                string[] sss = ss.Split('/');
                ss = sss[4].ToString();
                sss = ss.Split('-');
                ss = sss[0].ToString();
                dt.Rows.Add(ss, htmlnode[i].ChildNodes["h4"].InnerText, urls);
            }

            return dt;
        }

        /// <summary>
        ///  提取页面链接
        /// </summary>
        /// <param name="textToFormat"></param>
        /// <returns></returns>
        public static string[] GetA(String textToFormat)
        {
            const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //新建正则模式
            MatchCollection m = r.Matches(textToFormat); //获得匹配结果
            string[] links = new string[m.Count];
            for (int i = 0; i < m.Count; i++)
            {
                links[i] = m[i].ToString(); //提取出结果
            }
            return links;
        }

        /// <summary>
        /// HTML 编码
        /// </summary>
        public static String HtmlEncode(String textToFormat)
        {
            return String.IsNullOrEmpty(textToFormat) ? textToFormat : HttpUtility.HtmlEncode(textToFormat);
        }

        /// <summary>
        /// HTML 解码
        /// </summary>
        public static String HtmlDecode(String textToFormat)
        {
            return String.IsNullOrEmpty(textToFormat) ? textToFormat : HttpUtility.HtmlDecode(textToFormat);
        }

        /// <summary>
        /// 移除所有标记
        /// </summary>
        public static String StripAllTags(String strToStrip)
        {
            strToStrip = Regex.Replace(strToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = StripHtmlXmlTags(strToStrip);
            return strToStrip;
        }

        /// <summary>
        /// 移除其他 HTML 标记
        /// </summary>
        public static String StripForPreview(String content)
        {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br />", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = content.Replace("'", "&#39;");
            return StripHtmlXmlTags(content);
        }

        /// <summary>
        /// 移除 HTML、XML 标记
        /// </summary>
        public static String StripHtmlXmlTags(String content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 移除 Script 标记
        /// </summary>
        public static String StripScriptTags(String content)
        {
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "'javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(content, "\"javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public string GetHTML(string url)
        {
            WebClient web = new WebClient();
            byte[] buffer = web.DownloadData(url);
            string content = Encoding.GetEncoding("GBK").GetString(buffer);
            return content;
        }

    }
}
