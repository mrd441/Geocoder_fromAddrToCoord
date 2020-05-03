using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Http;
using System.Xml;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace OSM_Geocoding
{
    public partial class Form1 : Form
    {
        public struct ProxyListElement
        {
            public WebProxy proxy;
            public string address { get; set; }
            public long responseTime { get; set; }
            public int raiting { get; set; }
        }

        public List<ProxyListElement> proxyLixt;

        public struct AddressListElement
        {
            public int row { get; set; }
            public string longit { get; set; }
            public string latid { get; set; }
            public string city { get; set; }
            public string road { get; set; }
            public string house_number { get; set; }
            public bool isChecking { get; set; }
            public bool Checked { get; set; }
        }

        public List<AddressListElement> addressList;

        public struct resultInfoElement
        {
            public string fileName { get; set; }
            public string filePath { get; set; }
            public int addressCount { get; set; }
            public int addressCekhed { get; set; }
            public bool comlete { get; set; }

        }

        public List<resultInfoElement> resultList;

        public int checkedAddresses
        {
            get { return _checkedAddresses; }
            set
            {
                _checkedAddresses = value;
               
                int x = resultList.Count;
                if (x > 0)
                {
                    resultInfoElement elem = resultList.Last();
                    elem.addressCekhed = _checkedAddresses;
                    resultList[x-1] = elem;
                    resultInfoElementBindingSource.ResetItem(x-1);
                }
            }
        }

        private int _checkedAddresses;
        public int checkingAddresses;
        public int curentProxyIndex;
        public bool useProxy;
        public string usingService;
        string yandexCity;
        bool useYandexCity;
        int reqestDelay;
        bool fillCity;
        Dictionary<string, int> validCity;
        private static List<Task> workers = new List<Task>();

        public Form1()
        {
            InitializeComponent();
            proxyLixt = new List<ProxyListElement>();
            addressList = new List<AddressListElement>();
            resultList = new List<resultInfoElement>();
            resultInfoElementBindingSource.DataSource = resultList;
            proxyListElementBindingSource.DataSource = proxyLixt;
            addressListElementBindingSource.DataSource = addressList;
            ServicePointManager.DefaultConnectionLimit = 501;

            pictureBox1.Visible = false;
            useProxy = true;
            reqestDelay = 200;
            numericUpDown1.Value = reqestDelay;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            useYandexCity = useYandexCityCheckBox.Checked;

            //proxyLixt[0]
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Contains(".xls") | file.Contains(".xlsx"))
                {
                    if (!listBox1.Items.Contains(file))
                    {
                        listBox1.Items.Add(file);
                        loging(0, "Добавлен файл " + file);                        
                        checkedListBox1.Items.Add(file.Split('\\').Last(), false);
                    }                    
                }
                else if (file.Contains(".txt"))
                {
                    loadProxyList(file);
                    loging(0, "Список прокси загружен");
                }
                else {
                    loging(2, "Не корректный тип файла " + file);
                }                
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                loging(2, "Не добавлено ни одного файла");
                return;
            }
            if (proxyLixt.Count == 0)
            {
                loging(2, "не найден ни один прокси сервер, будет использовано прямое подключение");
                proxyLixt.Clear();

                proxyLixt.Add(new ProxyListElement{ address = "localhost" , raiting = 5});
                useProxy = false;
                proxyListElementBindingSource.ResetBindings(true);
                //return;
            }


            loging(1, "Начало");
            pictureBox1.Visible = true;
            resultList.Clear();
                        
            try
            {
                for (int i=0;i<listBox1.Items.Count; i++)
                    await MainAsync(i);
            }
            catch (Exception exss)
            {
                loging(2, exss.Message);
            }
            pictureBox1.Visible = false;
            this.AllowDrop = true;
            loging(1, "Завершено");
        }

        async Task MainAsync(int fileIndex)
        {
            try
            {
                validCity = new Dictionary<string, int>();
                yandexCity = "";
                loging(0, "Чтение входного файла");
                loadXML(listBox1.Items[fileIndex].ToString());
                loging(0, "Чтение файла заершено");

                fillCity = checkedListBox1.GetItemCheckState(0) == CheckState.Checked;
                //GenerateNewXML();

                checkedAddresses = 0;
                checkingAddresses = 0;

                //int i = curentProxyIndex;
                int j = 0;
                int notWorkingProxies = 0;
                if (addressList.Count>0)
                    await reverseGeocodinYandex(j, true);
                while (checkedAddresses < addressList.Count)
                {
                    if (proxyLixt[curentProxyIndex].raiting > 0)
                    {
                        while ((workers.Count + checkedAddresses) < addressList.Count & workers.Count < 500)
                        {
                            if (!(addressList[j].Checked | addressList[j].isChecking))
                            {
                                //Task aTask = Task.Run(() => reverseGeocoding(j, curentProxyIndex));
                                Task aTask;
                                aTask = usingService == "yandex"? Task.Run(() => reverseGeocodinYandex(j, false)) : Task.Run(() => reverseGeocoding(j, curentProxyIndex));
                                
                                workers.Add(aTask);
                                await Task.Delay(10);
                                j++;
                                curentProxyIndex++;
                                if (j >= addressList.Count) j = 0;
                                if (curentProxyIndex >= proxyLixt.Count)
                                {
                                    curentProxyIndex = 0;
                                    notWorkingProxies = 0;
                                    await Task.Delay(reqestDelay);
                                }
                            }
                            else
                            {
                                j++;
                                if (j >= addressList.Count)
                                {
                                    j = 0;
                                    
                                }
                            }
                            await Task.Delay(10);
                        }
                    }
                    else
                    {
                        await Task.Delay(10);
                        curentProxyIndex++;
                        notWorkingProxies = useProxy? notWorkingProxies+1 : 0;
                        if(notWorkingProxies == proxyLixt.Count)
                        {
                            throw new Exception("Процесс завершился ошибкой. Не получено ответа ни от одного прокси");

                        }
                        if (curentProxyIndex > proxyLixt.Count)
                        {
                            curentProxyIndex = 0;
                            notWorkingProxies = 0;
                            await Task.Delay(reqestDelay);
                        }
                    }
                    await Task.Delay(10);
                    workers.RemoveAll(x => x.IsCompleted);
                }
                loging(0, "Все адреса успешно установлены");

                loging(0, "Формирование выходного файла");
                loging(0, "Выходной файл сформирован: " + GenerateNewXML());
            }
            catch (Exception ssss)
            {
                throw new Exception("Процесс завершился ошибкой. " + ssss.Message);
            }
            
        }

        async Task reverseGeocodinYandex(int addressIndex, bool CheckCity)
        {
            AddressListElement aAddressListElement = addressList[addressIndex];
            aAddressListElement.isChecking = true;
            addressList[addressIndex] = aAddressListElement;
            addressListElementBindingSource.ResetItem(addressIndex);
            checkingAddresses++;
            try
            {
                if (aAddressListElement.latid != null & aAddressListElement.latid != "" & aAddressListElement.longit != null & aAddressListElement.longit != "")
                {
                    var httpClientHandler = new HttpClientHandler();
                    // Finally, create the HTTP client object

                    var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                    client.BaseAddress = new Uri("https://geocode-maps.yandex.ru");
                    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                    client.Timeout = TimeSpan.FromMilliseconds(3000);

                    //geocode-maps.yandex.ru/1.x/?apikey=d4a52bbe-5323-4938-ad75-d228bf49210b&geocode=47.595025,42.095995
                    var uri = "/1.x/?";
                    string apiKey = "d4a52bbe-5323-4938-ad75-d228bf49210b";
                    uri = uri + "apikey=" + apiKey + "&geocode=" + aAddressListElement.longit + "," + aAddressListElement.latid;

                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        XmlDocument doc = new XmlDocument();
                        string area = "";
                        string hamlet = "";
                        string road = "";
                        string house_number = "";
                        doc.LoadXml(responseString);
                        XmlNodeList nodeList = doc.GetElementsByTagName("Component");
                        foreach (XmlNode node in nodeList)
                        {
                            string kindString = checkXmlNode(node.FirstChild);

                            switch (kindString)
                            {
                                case "area":
                                    area = checkXmlNode(node.LastChild);
                                    break;
                                case "locality":
                                    hamlet = checkXmlNode(node.LastChild);
                                    break;
                                case "street":
                                    road = checkXmlNode(node.LastChild);
                                    break;
                                case "house":
                                    house_number = checkXmlNode(node.LastChild);
                                    break;
                            }
                        }

                        if (CheckCity)
                        {
                            if (hamlet!="")
                                yandexCity = hamlet;
                            else
                                yandexCity = area;
                            aAddressListElement.isChecking = false;
                            addressList[addressIndex] = aAddressListElement;
                            addressListElementBindingSource.ResetItem(addressIndex);
                            return;
                        }

                        if (hamlet != "")
                            aAddressListElement.city = hamlet;
                        else
                            aAddressListElement.city = area;

                        if (aAddressListElement.city != "")
                        {
                            if (validCity.ContainsKey(aAddressListElement.city))
                                validCity[aAddressListElement.city] = validCity[aAddressListElement.city] + 1;
                            else
                                validCity.Add(aAddressListElement.city, 1);
                        }

                        aAddressListElement.road = road;
                        aAddressListElement.house_number = house_number;
                        aAddressListElement.Checked = true;
                        aAddressListElement.isChecking = false;
                        addressList[addressIndex] = aAddressListElement;
                        addressListElementBindingSource.ResetItem(addressIndex);                        
                        checkedAddresses++;
                    }
                }
                else
                {
                    aAddressListElement.Checked = true;
                    checkedAddresses++;
                }
            }
            catch(Exception ex)
            {
                aAddressListElement.Checked = false;
                aAddressListElement.isChecking = false;
                addressList[addressIndex] = aAddressListElement;
                addressListElementBindingSource.ResetItem(addressIndex);
            }            
        }
        async Task reverseGeocoding(int addressIndex, int proxyIndex)
        {
            try
            {
                var stopWatch = Stopwatch.StartNew();

                //var proxy = new WebProxy("88.247.153.104", 8080);
                ProxyListElement aProxyListElement = proxyLixt[proxyIndex];
                var proxy = aProxyListElement.proxy;

                AddressListElement aAddressListElement = addressList[addressIndex];
                aAddressListElement.isChecking = true;
                addressList[addressIndex] = aAddressListElement;
                addressListElementBindingSource.ResetItem(addressIndex);
                checkingAddresses++;

                if (aAddressListElement.latid != null & aAddressListElement.latid != "" & aAddressListElement.longit != null & aAddressListElement.longit != "")
                {


                    // Now create a client handler which uses that proxy
                    var httpClientHandler = new HttpClientHandler();
                    httpClientHandler.Proxy = proxy;
                    httpClientHandler.UseProxy = useProxy;

                    // Finally, create the HTTP client object

                    var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                    client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                    client.Timeout = TimeSpan.FromMilliseconds(3000);

                    //nominatim.openstreetmap.org/reverse?lat=42.095995&lon=47.595025&accept-language=ru&zoom=18&email=441-05@mail.ru&addressdetails=1format=xml nominatim.openstreetmap.org/reverse?accept-language=ru&zoom=18&format=xml&lat=44.948674&lon=45.854249


                    var uri = "/reverse?accept-language=ru&zoom=18&format=xml";

                    uri = uri + "&lat=" + aAddressListElement.latid;
                    uri = uri + "&lon=" + aAddressListElement.longit;

                    try
                    {
                        var response = await client.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                doc.LoadXml(responseString);
                                var hamlet = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/addressparts/hamlet"));
                                var city = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/addressparts/city"));
                                var county = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/addressparts/county"));                                

                                var road = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/addressparts/road"));
                                var house_number = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/addressparts/house_number"));
                                var fullAddress = checkXmlNode(doc.DocumentElement.SelectSingleNode("/reversegeocode/result"));

                                if (hamlet != "")
                                    aAddressListElement.city = hamlet;
                                else if (city != "")
                                    aAddressListElement.city = city;
                                else
                                    aAddressListElement.city = county;

                                if (useYandexCity & yandexCity != "")
                                    aAddressListElement.city = yandexCity;

                                if (aAddressListElement.city != "")
                                {
                                    if (validCity.ContainsKey(aAddressListElement.city))
                                        validCity[aAddressListElement.city] = validCity[aAddressListElement.city] + 1;
                                    else
                                        validCity.Add(aAddressListElement.city, 1);
                                }

                                aAddressListElement.road = road;
                                aAddressListElement.house_number = house_number;
                                aAddressListElement.Checked = true;
                                checkedAddresses++;
                                Console.WriteLine(fullAddress);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(responseString + " " + ex.Message);
                            }
                        }
                        else
                            Console.WriteLine(response.StatusCode);
                    }
                    catch (Exception ex)
                    {
                        if (useProxy)
                            aProxyListElement.raiting--;
                        Console.WriteLine(ex.Message);
                    }

                    long responseTime = stopWatch.ElapsedMilliseconds;
                    aAddressListElement.isChecking = false;

                    aProxyListElement.responseTime = responseTime;
                    proxyLixt[proxyIndex] = aProxyListElement;
                    proxyListElementBindingSource.ResetItem(proxyIndex);

                }
                else
                {
                    aAddressListElement.Checked = true;
                    checkedAddresses++;
                }
                aAddressListElement.isChecking = false;
                addressList[addressIndex] = aAddressListElement;
                addressListElementBindingSource.ResetItem(addressIndex);

                checkingAddresses--;
                await Task.Delay(10);
            }
            catch(Exception ex)
            {
                AddressListElement aAddressListElement = addressList[addressIndex];
                aAddressListElement.isChecking = false;
                addressList[addressIndex] = aAddressListElement;
                addressListElementBindingSource.ResetItem(addressIndex);
            }
        }

        static string checkXmlNode(XmlNode axmlNode)
        {
            if (axmlNode != null)
                return axmlNode.InnerText;
            else
                return "";
        }

        private void loadProxyList(string file)
        {
            proxyLixt.Clear();
            curentProxyIndex = 0;
            string path = file;
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string fullAddress = sr.ReadLine();
                            string[] proxyAddress = fullAddress.Split(':');
                            var proxy = new WebProxy(proxyAddress[0], Convert.ToInt32(proxyAddress[1]));
                            ProxyListElement ProxyListElement = new ProxyListElement();
                            ProxyListElement.proxy = proxy;
                            ProxyListElement.responseTime = 0;
                            ProxyListElement.raiting = 5;
                            ProxyListElement.address = fullAddress;
                            proxyLixt.Add(ProxyListElement);

                        }
                    }
                }
                proxyListElementBindingSource.ResetBindings(true);
                useProxy = true;
                curentProxyIndex = 0;
            }
            catch (Exception ex)
            {
                loging(2, "Не удалось загрузить список прокси. " + ex.ToString());
            }
        }

        public void loging(int level, string text)
        {
            var aColor = Color.Black;
            if (level == 1)
                aColor = Color.Green;
            else if (level == 2)
                aColor = Color.Red;
            string curentTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");            
            logBox.AppendText(curentTime + ": " + text + Environment.NewLine, aColor);
        }

        private void loadXML(string file)
        {            
            try
            {
                addressList.Clear();

                Excel.Application xlApp = new Excel.Application(); 
                Excel.Workbook xlWB;               
                Excel.Worksheet xlSht; 
                xlWB = xlApp.Workbooks.Open(file);                                          
                xlSht = (Excel.Worksheet)xlWB.Worksheets[1]; 
                Excel.Range last = xlSht.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
               
                var arrData = (object[,])xlSht.get_Range("A1", last).Value;
                xlWB.Close(false); 
                xlApp.Quit(); 

                int rowCount = arrData.GetUpperBound(0);
                int colCount = arrData.GetUpperBound(1);

                for (int i = 2; i <= rowCount; i++)
                {
                    if (arrData[i, 17] != null && arrData[i, 18] != null)
                    {
                        AddressListElement aAddressListElement = new AddressListElement();
                        aAddressListElement.row = i;
                        aAddressListElement.latid = getStringFromXML(arrData[i, 17]).Replace(',', '.');
                        aAddressListElement.longit = getStringFromXML(arrData[i, 18]).Replace(',', '.');                        
                        addressList.Add(aAddressListElement);
                    }
                }

                resultInfoElement aResultInfoElement = new resultInfoElement();
                aResultInfoElement.fileName = file.Split('\\').Last();
                aResultInfoElement.filePath = file;
                aResultInfoElement.addressCount = addressList.Count;
                aResultInfoElement.comlete = false;
                resultList.Add(aResultInfoElement);
                resultInfoElementBindingSource.ResetBindings(true);

                addressListElementBindingSource.ResetBindings(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка загрузки Excel файла: " + ex.Message);
            }
        }

        private string getStringFromXML(object data)
        {
            string test = "";
            try
            {
                test = data.ToString();
            }
            catch (Exception)
            {
                test = "";
            }
            return test;
        }

        public string GenerateNewXML()
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Random rnd = new Random();
                int resulFileNumber = resultList.Count-1;
                string newFullfileName = resultList.Last().filePath;

                string goodCity = "";
                int cityCount = 0;
                foreach (string key in validCity.Keys)
                    if (validCity[key] > cityCount)
                    {
                        goodCity = key;
                        cityCount = validCity[key];
                    }

                int lastRowIndex = addressList.Last().row;

                //Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWB;
                Excel.Worksheet xlSht;
                xlWB = xlApp.Workbooks.Open(newFullfileName);
                xlSht = (Excel.Worksheet)xlWB.Worksheets[1];
                Excel.Range range = xlSht.get_Range("M2", "O" + lastRowIndex.ToString());

                object[,] arr = range.Value;

                List<string> streetList = new List<string>();
                int i = 0;
                foreach (AddressListElement aRow in addressList)
                {
                    int rowIndex = aRow.row-1;
                    //string city = goodCity == "" ? "б/а" : goodCity;
                    string city = aRow.road == "" ? "б/а" : aRow.city;
                    string road = aRow.road == "" ? "б/а" : aRow.road;
                    string houseNumber = aRow.house_number;

                    if (!(streetList.Contains(road + houseNumber) | houseNumber == ""))
                    {
                        houseNumber = aRow.house_number;
                        streetList.Add(road + houseNumber);
                    }
                    else
                        houseNumber = "б/а";

                    if (fillCity)
                        arr[rowIndex, 1] = city;
                    arr[rowIndex, 2] = road;
                    arr[rowIndex, 3] = houseNumber;
                    i++;
                }
                range.Value = arr;

                xlWB.Save();
                xlWB.Close(true);
                xlApp.Quit();

                return newFullfileName;                
            }
            catch (Exception ex)
            {
                foreach (Excel.Workbook xlBool in xlApp.Workbooks)
                    xlBool.Close(false);
                xlApp.Quit();
                throw new Exception("Ошибка создания выходного файла: " + ex.Message);
            }
        }

        private void OffProxyButton_Click(object sender, EventArgs e)
        {
            if (useProxy)
            {                
                useProxy = false;
                proxyLixt.Clear();
                proxyLixt.Add(new ProxyListElement { address = "localhost", raiting = 5 });
                useProxy = false;
                proxyListElementBindingSource.ResetBindings(true);                
            }
            loging(2, "Использование прокси успешно отключено");
        }

        private void proxyListElementBindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                
            }
        }

        private void addressListElementBindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {

            }
        }

        private void resultInfoElementBindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reqestDelay = Decimal.ToInt32(numericUpDown1.Value);
        }

        private void radioButtonYa_CheckedChanged(object sender, EventArgs e)
        {
            usingService = "yandex";
            OffProxyButton_Click(this, EventArgs.Empty);
        }

        private void radioButtonOSM_CheckedChanged(object sender, EventArgs e)
        {
            usingService = "osm";
        }

        private void useYandexCityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useYandexCity = useYandexCityCheckBox.Checked;
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
public static class RichTextBoxExtensions
{
    public static void AppendText(this RichTextBox box, string text, Color color)
    {
        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;

        box.SelectionColor = color;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;
        box.SelectionStart = box.Text.Length;
        box.ScrollToCaret();
    }
}
