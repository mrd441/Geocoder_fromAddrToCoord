using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using System.Net;
using System.Net.Http;
using System.Xml;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Globalization;

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
            public string fullAddress { get; set; }
            public string origAddress { get; set; }
            public bool valid { get; set; }
            public string corp { get; set; }
            public string curSystem { get; set; }
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
        public long lastOsmTimestamp;
        int reqestDelay;
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
            reqestDelay = 1000;
            numericUpDown1.Value = reqestDelay;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

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
                loging(0, "Чтение входного файла");
                await Task.Run(() => loadXML(listBox1.Items[fileIndex].ToString()));
                loging(0, "Чтение файла заершено");

                
                //GenerateNewXML();

                checkedAddresses = 0;
                checkingAddresses = 0;

                //int i = curentProxyIndex;
                int j = 0;
                int notWorkingProxies = 0;
                //if (addressList.Count>0)
                    //await reverseGeocodinYandex(j, true);
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
                                aTask = (usingService == "yandex" || usingService == "dual") ? Task.Run(() => reverseGeocodinYandex(j, false, curentProxyIndex)) : Task.Run(() => reverseGeocoding(j, curentProxyIndex));
                                
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

        async Task reverseGeocodinYandex(int addressIndex, bool CheckCity, int proxyIndex)
        {
            AddressListElement aAddressListElement = addressList[addressIndex];
            aAddressListElement.isChecking = true;
            aAddressListElement.curSystem = "Ya";
            addressList[addressIndex] = aAddressListElement;
            addressListElementBindingSource.ResetItem(addressIndex);
            checkingAddresses++;
            bool valid = false;
            try
            {
                if (aAddressListElement.city != null & aAddressListElement.city != "" & aAddressListElement.road != null & aAddressListElement.road != "")
                {
                    var httpClientHandler = new HttpClientHandler();

                    var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                    client.BaseAddress = new Uri("https://geocode-maps.yandex.ru");
                    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                    client.Timeout = TimeSpan.FromMilliseconds(3000);

                    //geocode-maps.yandex.ru/1.x/?apikey=57896752-ae5f-4443-9614-7a4b17678d35&results=5&geocode=гМахачкала+авиационная(керимова)+5+к2
                    //c0d403ab-e5be-4049-908c-8122a58acf23
                    //57896752-ae5f-4443-9614-7a4b17678d35
                    var uri = "/1.x/?";
                    string apiKey = (yaAPIkey.Text=="")? "d4a52bbe-5323-4938-ad75-d228bf49210b" : yaAPIkey.Text;

                    uri = uri + "apikey=" + apiKey + "&results=5" + "&geocode=" + aAddressListElement.city + "+" + aAddressListElement.road + "+" + aAddressListElement.house_number + "+" + aAddressListElement.corp;
                    uri = uri.Replace(" ", "");
                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        XmlDocument doc = new XmlDocument();
                        string area = "";
                        string hamlet = "";
                        string road = "";
                        string house_number = "";
                        string pos = "";
                        string lat = "";
                        string lon = "";
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
                                case "pos":
                                    pos = checkXmlNode(node);
                                    break;
                                    
                            }
                        }

                        var posNode = doc.GetElementsByTagName("pos");
                        if (posNode!=null && posNode.Count>0)
                            pos = posNode[0].FirstChild.Value;

                        string[] posSplit = pos.Split(' ');
                        if (posSplit.Count()==2)
                        {
                            lon = pos.Split(' ')[0];
                            lat = pos.Split(' ')[1];
                        }
                        
                        if (hamlet != "")
                            area = hamlet;

                        house_number = house_number.Replace(" ", "").ToLower();
                        string curHauseNum = (aAddressListElement.road + aAddressListElement.house_number + aAddressListElement.corp).Replace(" ", "");
                        curHauseNum = curHauseNum.Substring(curHauseNum.Length - house_number.Length);

                        valid = (curHauseNum == house_number && house_number!="");
                        aAddressListElement.fullAddress = hamlet + ", " + road + ", " + house_number;
                        
                        aAddressListElement.valid = valid;
                        addressList[addressIndex] = aAddressListElement;
                        addressListElementBindingSource.ResetItem(addressIndex);
                        if (!valid && usingService == "dual")
                        {
                            await Task.Run(() => reverseGeocoding(addressIndex, proxyIndex));
                        }
                        else
                        {
                            aAddressListElement.latid = lat;
                            aAddressListElement.longit = lon;
                            aAddressListElement.Checked = true;
                            checkedAddresses++;
                            aAddressListElement.isChecking = false;
                            addressList[addressIndex] = aAddressListElement;
                            addressListElementBindingSource.ResetItem(addressIndex);
                        }
                        
                    }
                    else
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(responseString);
                        XmlNodeList nodeList = doc.GetElementsByTagName("message");
                        string message = "";
                        if (nodeList != null && nodeList.Count > 0)
                            message = nodeList[0].InnerText;
                        if (message == "Invalid key")
                            loging(2, "Ошибка запроса " + aAddressListElement.row + ". Недействиетльный ключ Yandex API");
                        else if (message != "")
                            loging(2, "Ошибка запроса " + aAddressListElement.row + ". " + message);
                        else
                            loging(2, "Ошибка запроса " + aAddressListElement.row + ". " + responseString);

                        if (usingService == "dual")
                            await Task.Run(() => reverseGeocoding(addressIndex, proxyIndex));
                        else
                        {
                            aAddressListElement.Checked = true;
                            checkedAddresses++;
                            aAddressListElement.isChecking = false;
                            addressList[addressIndex] = aAddressListElement;
                            addressListElementBindingSource.ResetItem(addressIndex);
                        }
                    }
                }
                else
                {
                    if (usingService == "dual")
                        await Task.Run(() => reverseGeocoding(addressIndex, proxyIndex));
                    else
                    {
                        aAddressListElement.Checked = true;
                        aAddressListElement.isChecking = false;
                        checkedAddresses++;
                        addressList[addressIndex] = aAddressListElement;
                        addressListElementBindingSource.ResetItem(addressIndex);
                    }
                }
            }
            catch(Exception ex)
            {
                if (usingService == "dual")
                    await Task.Run(() => reverseGeocoding(addressIndex, proxyIndex));
                else
                {
                    aAddressListElement.Checked = false;
                    aAddressListElement.isChecking = false;
                    addressList[addressIndex] = aAddressListElement;
                    addressListElementBindingSource.ResetItem(addressIndex);
                }
            }            
        }

        private string checkXmlAttribut(XmlDocument doc, string node, string attr)
        {
            try
            {
                var nameNode = doc.DocumentElement.SelectSingleNode(node).Attributes.GetNamedItem(attr);
                string dname = (nameNode == null) ? "" : nameNode.Value;
                return dname;
            }
            catch(Exception ew)
            {
                return "";
            }
        }

        async Task reverseGeocoding(int addressIndex, int proxyIndex)
        {
            try
            {
                long curStopWatch = 0;
                //var proxy = new WebProxy("88.247.153.104", 8080);
                ProxyListElement aProxyListElement = proxyLixt[proxyIndex];
                var proxy = aProxyListElement.proxy;

                AddressListElement aAddressListElement = addressList[addressIndex];
                aAddressListElement.isChecking = true;
                aAddressListElement.curSystem = "OSM";
                addressList[addressIndex] = aAddressListElement;
                addressListElementBindingSource.ResetItem(addressIndex);
                checkingAddresses++;

                if (aAddressListElement.city != null & aAddressListElement.city != "" & aAddressListElement.road != null & aAddressListElement.road != "")
                {


                    // Now create a client handler which uses that proxy
                    var httpClientHandler = new HttpClientHandler();
                    httpClientHandler.Proxy = proxy;
                    httpClientHandler.UseProxy = useProxy;

                    // Finally, create the HTTP client object
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                    client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                    client.Timeout = TimeSpan.FromMilliseconds(3000);

                    //nominatim.openstreetmap.org/reverse?lat=42.095995&lon=47.595025&accept-language=ru&zoom=18&email=441-05@mail.ru&addressdetails=1format=xml nominatim.openstreetmap.org/reverse?accept-language=ru&zoom=18&format=xml&lat=44.948674&lon=45.854249
                    //nominatim.openstreetmap.org/reverse?accept-language=ru&zoom=18&format=xml&lat=41.882855&lon=48.072434"
                    //nominatim.openstreetmap.org/ui/search.html?city=махачкала&state=дагестан&country=россия

                    var uri = "/search?format=xml&country=россия&state=дагестан";
                    string cityM = aAddressListElement.city;
                    string streetM = aAddressListElement.road;
                    string houseM = aAddressListElement.house_number;
                    string corpM = aAddressListElement.corp;
                    uri = uri + "&city=" + cityM;
                    uri = uri + "&street=" + streetM + "+" + houseM + "+" + corpM ;
                    uri = uri.Replace(" ", "+");
                    try
                    {
                        if (!useProxy)
                        {
                            int maxOsmTimeOut = 1001;
                            long curOsmTimestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            long timeDiff = curOsmTimestamp - lastOsmTimestamp;
                            if (timeDiff < maxOsmTimeOut)
                            {
                                lastOsmTimestamp = curOsmTimestamp + (maxOsmTimeOut - timeDiff);
                                Thread.Sleep(maxOsmTimeOut - Convert.ToInt32(timeDiff));
                            }
                            else
                                lastOsmTimestamp = curOsmTimestamp;

                        }

                        curStopWatch = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                        var response = await client.GetAsync(uri);

                        curStopWatch = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - curStopWatch;

                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                string dname = "0";
                                string lat = "0";
                                string lon = "0";
                                string address_rank = "0";//house
                                string type = "0";//house
                                string classT = "0" ;//class="building"

                                doc.LoadXml(responseString);
                                XmlNodeList nameNodes = doc.DocumentElement.SelectNodes("/searchresults/place");
                                foreach (XmlNode nameNode in nameNodes)
                                {
                                    type = nameNode.Attributes.GetNamedItem("type").Value;//house
                                    classT = nameNode.Attributes.GetNamedItem("class").Value;//class="building"
                                    if (classT == "building")
                                    {
                                        dname = nameNode.Attributes.GetNamedItem("display_name").Value;
                                        lat = nameNode.Attributes.GetNamedItem("lat").Value;
                                        lon = nameNode.Attributes.GetNamedItem("lon").Value;
                                        address_rank = nameNode.Attributes.GetNamedItem("address_rank").Value;//house
                                        break;
                                    }                                    
                                }

                                aAddressListElement.latid = lat;
                                aAddressListElement.longit = lon;
                                aAddressListElement.fullAddress = dname;

                                string houseNum = "*";
                                int address_rankI = 0;
                                try { address_rankI = Convert.ToInt32(address_rank); }
                                catch { }

                                if (address_rankI > 27)
                                {
                                    string[] dnameSplit = dname.Split(',');
                                    if (classT == "building" && dnameSplit.Count() > 0)
                                        houseNum = dnameSplit[0];
                                    else if(classT != "building" && dnameSplit.Count() > 1)
                                        houseNum = dnameSplit[1];
                                }

                                houseNum = houseNum.Replace(" ", "").ToLower();
                                string curHauseNum = (aAddressListElement.road + aAddressListElement.house_number + aAddressListElement.corp).Replace(" ", "");
                                curHauseNum = curHauseNum.Substring(curHauseNum.Length - houseNum.Length);

                                aAddressListElement.valid = curHauseNum == houseNum;

                                aAddressListElement.Checked = true;
                                checkedAddresses++;
                                Console.WriteLine(dname);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(responseString + " " + ex.Message);
                            }
                        }
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                aAddressListElement.Checked = true;
                                checkedAddresses++;
                            }
                            loging(2, "Ошибка запроса " + aAddressListElement.row + ". " + client.BaseAddress + uri);
                            Console.WriteLine(response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (useProxy)
                            aProxyListElement.raiting--;
                        Console.WriteLine(ex.Message);
                    }

                    aAddressListElement.isChecking = false;

                    aProxyListElement.responseTime = curStopWatch;
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

        public string convertGrad(string grad)
        {
            string[] alast = grad.Split('\'');
            string[] afirst = alast.First().Split('°');
            float seconds = float.Parse(alast.Last(), CultureInfo.InvariantCulture.NumberFormat);
            float minuts = int.Parse(afirst.Last(), CultureInfo.InvariantCulture.NumberFormat);
            int grads = int.Parse(afirst.First());
            return (grads + minuts/60 + seconds/3600).ToString().Replace(",",".");
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
            if (InvokeRequired)
            {
                this.Invoke(new Action<int,string>(loging), new object[] { level, text });
                return;
            }
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

                for (int i = 4; i <= rowCount; i++)
                {
                    if (arrData[i, 6] != null && arrData[i, 7] != null)
                    {
                        string city = getStringFromXML(arrData[i, 6]);
                        string origAddr = getStringFromXML(arrData[i, 7]);
                        string[] addr = getAddr(origAddr);

                        AddressListElement aAddressListElement = new AddressListElement();
                        aAddressListElement.origAddress = origAddr;
                        aAddressListElement.city = city;
                        aAddressListElement.road = addr[0];
                        aAddressListElement.house_number = addr[1];
                        aAddressListElement.corp = addr[2];
                        aAddressListElement.row = i;
                        //aAddressListElement.latid = getStringFromXML(arrData[i, 17]).Replace(',', '.');
                        //aAddressListElement.longit = getStringFromXML(arrData[i, 18]).Replace(',', '.');                        
                        addressList.Add(aAddressListElement);
                    }
                }

                resultInfoElement aResultInfoElement = new resultInfoElement();
                aResultInfoElement.fileName = file.Split('\\').Last();
                aResultInfoElement.filePath = file;
                aResultInfoElement.addressCount = addressList.Count;
                aResultInfoElement.comlete = false;
                resultList.Add(aResultInfoElement);

                SetBindingSourceDataSource(resultInfoElementBindingSource);
                SetBindingSourceDataSource(addressListElementBindingSource);
                //resultInfoElementBindingSource.ResetBindings(true);

                //addressListElementBindingSource.ResetBindings(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка загрузки Excel файла: " + ex.Message);
            }
        }

        public void SetBindingSourceDataSource(BindingSource bs)
        {
            if (InvokeRequired)
                Invoke(new Action<BindingSource>(SetBindingSourceDataSource),
                       new object[] { bs });
            else
                bs.ResetBindings(true);
        }

        private string[] getAddr(string fullAddr)
        {
            string[] splitaddr = fullAddr.ToLower().Split(',');
            string street = "";
            string corp = "";
            string house = "";
            if (splitaddr.Length > 0)
                street = splitaddr[0];

            foreach (string b in splitaddr)
            {
                string a = b;
                if (a.Contains("корпус"))
                {
                    int pos = a.IndexOf("корпус");
                    corp = "к" + a.Substring(pos + 6, a.Length - pos - 6).Trim();
                    street = a.Substring(0, pos);
                }
                else if(a.Contains("корп"))
                {
                    int pos = a.IndexOf("корп");
                    corp = "к" + a.Substring(pos + 4, a.Length - pos - 4).Trim();
                    street = a.Substring(0, pos);
                }

                if (a.Contains("д. "))
                {
                    house = a.Replace("д. ", "").ToLower().Replace(" ", "");
                    if (house.Contains("позиция"))
                        house = house.Substring(0, house.IndexOf("позиция")).Replace(" ","");
                }

                if (a.Contains("(р-он "))
                    street = a.Substring(0, a.IndexOf("(р-он ")).Trim();
                else if (a.Contains("(р-н "))
                    street = a.Substring(0, a.IndexOf("(р-н ")).Trim();
                else if (a.Contains("р-н "))
                    street = a.Substring(0, a.IndexOf("р-н ")).Trim();
                else if (a.Contains("р-он "))
                    street = a.Substring(0, a.IndexOf("р-он ")).Trim();
                else if (a.Contains("район "))
                    street = a.Substring(0, a.IndexOf("район ")).Trim();

                if (a.Contains("С/О ") && a.Contains("("))
                {
                    int pos = a.IndexOf("(");
                    street = a.Substring(pos + 1, a.Length - pos - 2).Trim();
                }
                
                if (a.Contains("туп "))
                {
                    int pos = a.IndexOf("туп ");
                    street = a.Substring(pos + 4, a.Length - pos - 4).Trim();
                }
                else if (a.Contains("туп."))
                {
                    int pos = a.IndexOf("туп.");
                    street = a.Substring(0, pos).Trim();
                }
                else if (a.Contains("ул "))
                {
                    int pos = a.IndexOf("ул ");
                    street = a.Substring(pos + 3, a.Length - pos - 3).Trim();
                }
                else if (a.Contains("ул."))
                {
                    int pos = a.IndexOf("ул.");
                    street = a.Substring(pos + 3, a.Length - pos - 3).Trim();
                }
                else if (a.Contains("пр-кт "))
                {
                    int pos = a.IndexOf("пр-кт ");
                    street = a.Substring(pos + 6, a.Length - pos - 6).Trim();
                }
            }
            if (street.Contains("нет данных") || street.Contains("д.") || street.Contains("кв."))
                street = "";
            return new string[] {street, house, corp };
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
                Excel.Range range = xlSht.get_Range("L2", "O" + lastRowIndex.ToString());

                object[,] arr = range.Value;

                List<string> streetList = new List<string>();
                int i = 0;
                foreach (AddressListElement aRow in addressList)
                {
                    int rowIndex = aRow.row-1;

                    arr[rowIndex, 1] = (aRow.valid)? "" : "ошибка";
                    arr[rowIndex, 2] = aRow.latid;
                    arr[rowIndex, 3] = aRow.longit;
                    arr[rowIndex, 4] = aRow.fullAddress;
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
            numericUpDown1.Value = 300;
            reqestDelay = Decimal.ToInt32(numericUpDown1.Value);
            OffProxyButton_Click(this, EventArgs.Empty);
        }

        private void radioButtonOSM_CheckedChanged(object sender, EventArgs e)
        {
            usingService = "osm";
            numericUpDown1.Value = 1000;
            reqestDelay = Decimal.ToInt32(numericUpDown1.Value);
        }

        private void radioButtonDual_CheckedChanged(object sender, EventArgs e)
        {
            usingService = "dual";
            numericUpDown1.Value = 300;
            reqestDelay = Decimal.ToInt32(numericUpDown1.Value);
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

        private void deleteXML_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
                listBox1.Items.Remove(listBox1.SelectedItem);
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
