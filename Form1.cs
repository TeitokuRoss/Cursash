using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Text.Json.Serialization;
//using Windows.Web.Http;
using System.IO.Compression;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cursash2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static async Task<string> func(string spec, string year, int un,int qua,int bas)
        {
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient(handler))
            {
                string site;
                if (year != "2021")
                    site = year + ".edbo.gov.ua/offers/?qualification=1&education-base=40&speciality=121&education-form=1&course=1&university-name=" + un;
                else
                    site = year + ".edbo.gov.ua/offers-universities/";
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://vstup" + site))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:97.0) Gecko/20100101 Firefox/97.0");
                    request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                    request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                    request.Headers.TryAddWithoutValidation("Origin", "https://vstup" + year + ".edbo.gov.ua");
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Referer", "https://vstup" + year + ".edbo.gov.ua/offers/?qualification=1&education-base=40&speciality=121&education-form=1&course=1&university-name=" + un);
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                    request.Headers.TryAddWithoutValidation("Cookie", "_ga=GA1.1.2041819962.1638457784; _ga_W6WT1K3VXZ=GS1.1.1644952579.3.1.1644952591.0; _ga_YC32TV7WL7=GS1.1.1644952590.4.1.1644952753.0; _ga_96Q7K30V0N=GS1.1.1643115886.4.0.1643115888.0; PHPSESSID=sof0jmp661095i2grv7rhbha8a");
                    if (year != "2021")
                        request.Content = new StringContent("action=universities&university-id=&qualification="+qua+"&education-base="+bas+"&speciality=" + spec + "&program=&education-form=1&course=1&region=&university-name=" + un);
                    else
                        request.Content = new StringContent("qualification="+qua+"&education_base="+bas+"&speciality=" + spec + "&region=80&university=" + un + "&study_program=&education_form=&course=");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded; charset=UTF-8");


                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    response.Content.ReadAsStringAsync().Wait();
                    var responseBody = response.Content.ReadAsStreamAsync().Result;
                    Stream decompressed = new GZipStream(responseBody, CompressionMode.Decompress);
                    StreamReader objReader = new StreamReader(decompressed, Encoding.UTF8);
                    //var response = JsonConvert.DeserializeObject<T>(responseBody);
                    var resp = objReader.ReadToEnd();
                    return resp;
                }
            }
        }
        static async Task<string> func2(string codes, string year, int un)
        {
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient(handler))
            {
                string site;
                if (year != "2021")
                    site = year + ".edbo.gov.ua/";
                else
                    site = year + ".edbo.gov.ua/offers-list/";
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://vstup" + site))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:97.0) Gecko/20100101 Firefox/97.0");
                    request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                    request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                    request.Headers.TryAddWithoutValidation("Origin", "https://vstup" + year + ".edbo.gov.ua");
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Referer", "https://vstup" + year + ".edbo.gov.ua/offers/?qualification=1&education-base=40&speciality=121&university-name=" + un);
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                    request.Headers.TryAddWithoutValidation("Cookie", "_ga=GA1.1.2041819962.1638457784; _ga_W6WT1K3VXZ=GS1.1.1644952579.3.1.1644952591.0; _ga_YC32TV7WL7=GS1.1.1644952590.4.1.1644952753.0; _ga_96Q7K30V0N=GS1.1.1643115886.4.0.1643115888.0; PHPSESSID=sof0jmp661095i2grv7rhbha8a");

                    if (year != "2021")
                        request.Content = new StringContent("action=offers&usids=" + codes);
                    else
                        request.Content = new StringContent("ids=" + codes);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded; charset=UTF-8");



                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    response.Content.ReadAsStringAsync().Wait();
                    var responseBody = response.Content.ReadAsStreamAsync().Result;
                    Stream decompressed = new GZipStream(responseBody, CompressionMode.Decompress);
                    StreamReader objReader = new StreamReader(decompressed, Encoding.UTF8);
                    //var response = JsonConvert.DeserializeObject<T>(responseBody);
                    var resp = objReader.ReadToEnd();
                    return resp;
                }
            }
        }
        private async void writeout(string year, string spec, int un,int qua,int bas, int first, int second, ListBox l)
        {
            var result = await func(spec, year, un,qua,bas);
            //MessageBox.Show(result);
            if (comboBox2.SelectedItem.ToString() == "2021")
            {
                var results = result.Split('"');
                if (results.Length < second)
                {
                    l.Items.Add("Конкурсні пропозиції не знайдені");
                    return;
                }
                result = results[second];
                //MessageBox.Show(result, second.ToString());
            }
            else
            {
                var results = result.Split('"');
                if (results.Length < first)
                {
                    l.Items.Add("Конкурсні пропозиції не знайдені");
                    return;
                }
                result = results[first];
                //MessageBox.Show(result, second.ToString());
            }
            string[] codes = result.Split(',');
            result = await func2(result, year, un);
            //MessageBox.Show(result);
            var resp = JsonConvert.DeserializeObject<JToken>(result);
            //MessageBox.Show(resp["offers_requests_info"].ToString());
            if (year != "2021") 
            foreach (string c in codes)
            {
                var numbers = resp["offers_requests_info"][c];
                    //MessageBox.Show(resp["offers_requests_info"].ToString());
                    //MessageBox.Show(resp["offers_requests_info"][c].ToString());
                l.Items.Add("Сер:" + numbers[2].ToString() + " Мин:" + numbers[3].ToString() + " Макс:" + numbers[4].ToString());
            }
            else 
            {
                var respi = resp["offers"];
                //MessageBox.Show(respi.ToString());
                for(int i=0;i<codes.Length;i++)
                {
                    //MessageBox.Show(respi[i]["st"].ToString());
                    if (respi[i]["st"]!=null)
                    {
                        var numbers = respi[i]["st"]["c"];
                        l.Items.Add("Сер:" + numbers["rm"].ToString() + " Мин:" + numbers["obm"].ToString() + " Макс:" + numbers["ocm"].ToString());
                    }
                    
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string year = comboBox2.SelectedItem.ToString();
            string spec = comboBox1.SelectedItem.ToString();
            int qua = comboBox3.SelectedIndex;
            int qualification=0;
            int[] bases ={ 40,320,520};
            switch (qua)
            {
                case (0): qualification = 1;bases =new int[] {40,320,520};  break;
                case (1): qualification = 2; bases = new int[] { 40, 320, 520,640 }; break;
                case (2): qualification = 6; bases = new int[] { 40, 320}; break;
                case (3): qualification = 9; bases = new int[] { 30, 40, 510,520 }; break;
            }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach(int bas in bases)
            {
                //znu

                writeout(year, spec, 73, qualification, bas, 5, 11, listBox1);

                //zntu

                writeout(year, spec, 91, qualification, bas, 7, 13, listBox2);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }


}
