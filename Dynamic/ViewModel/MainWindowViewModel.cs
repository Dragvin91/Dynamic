using Dynamic.Infrastructure;
using Dynamic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;



namespace Dynamic.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<RM3_ПКЭ> Rm3_пке { get; set; } //коллекция хранит дерево всех папок в папке PKE
        public ObservableCollection<UID> uids { get; set; }
        public ObservableCollection<Result_Check_PKE> result { get; set; }        
        public UID select_UID { get; set; }         
        public MainWindowViewModel()
        {
            Rm3_пке = new List<RM3_ПКЭ>();
            uids = new ObservableCollection<UID>();
            result= new ObservableCollection<Result_Check_PKE>();
        }
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      OpenFolder();
                  }));
            }
        }
        private RelayCommand doubleClickCommand;
        public RelayCommand DoubleClickCommand
        {
            get
            {
                return doubleClickCommand ??
                  (doubleClickCommand = new RelayCommand(obj =>
                  {                      
                      ShowTable(select_UID);
                      chi = 0;
                  }));
            }
        }
        private RelayCommand converting;
        public RelayCommand Converting
        {
            get
            {
                return converting ??
                  (converting = new RelayCommand(obj =>
                  {
                      var convert = new CreatingXLSX();
                      convert.ConvertXLS(select_UID,result);
                      chi = 1;
                  }));
            }
        }
        byte chi = 0;
        private RelayCommand openExcel;
        public RelayCommand OpenExcel
        {
            get
            {
                return openExcel ??
                  (openExcel = new RelayCommand(obj =>
                  {
                      Process.Start("Result.xlsx");
                  },(obj)=>chi==1));
            }
        }
        public void OpenFolder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog(); // открытие диалогового окна

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                string[] directoriesNameObject = Directory.GetDirectories(folderBrowser.SelectedPath); //выбор папки

                Transformation(directoriesNameObject);
            }
            Preobrazovanie();
        }
        public void Transformation(string[] directories)
        {
            foreach (var directory in directories)
            {
                var nameObject = directory.Split('\\')[directory.Split('\\').Length - 1]; // разбиение на массив для получения названия объекта 
                var name = new RM3_ПКЭ() { NameObject = nameObject, Start_Check_PKE = new List<Start_Check_PKE>() };

                string[] directoriesStartCheck = Directory.GetDirectories(directory); // получаем папки с датами
                for (int j = 0; j < directoriesStartCheck.Length; j++)
                {
                    var startCheck = new Start_Check_PKE();
                    startCheck.UID = new List<UID>();
                    string[] StartCheck = directoriesStartCheck[j].Split('\\');
                    string[] files = Directory.GetFiles(directoriesStartCheck[j]); // получаем адреса файлов в папке
                    var uid = new UID();
                    foreach (var file in files)
                    {
                        var a = file.Split('\\')[file.Split('\\').Length - 1].Split('_')[0]; // берем UID из имени файла 
                        if (uid.uid == null)
                        {
                            uid.uid = a; // просвоение UID
                            uid.Param_Check_PKE = new List<Param_Check_PKE>() { XmlParserParam(file) };
                            uid.Result_Check_PKE = new List<Result_Check_PKE>() { XmlParserResult(file) };
                        }
                        else
                        {
                            if (uid.uid == a) //если UID прежний, то добавляем в коллекцию параметры и результаты
                            {
                                uid.Param_Check_PKE.Add(XmlParserParam(file));
                                uid.Result_Check_PKE.Add(XmlParserResult(file));
                            }
                            else
                            {
                                startCheck.UID.Add(uid);
                                uid = new UID();
                                uid.uid = a; //присваиваем новый UID
                                uid.Param_Check_PKE = new List<Param_Check_PKE>() { XmlParserParam(file) };
                                uid.Result_Check_PKE = new List<Result_Check_PKE>() { XmlParserResult(file) };
                            }
                        }
                    }
                    startCheck.UID.Add(uid);
                    startCheck.Start_Check = StartCheck[StartCheck.Length - 1]; // переменная для временного хранения названия папки начало проверки
                    name.Start_Check_PKE.Add(startCheck);
                }
                Rm3_пке.Add(name);
            }
        }
        public Param_Check_PKE XmlParserParam(string PathFile)
        {
            var d = new Param_Check_PKE();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(PathFile);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            d.TimeStart = DataTimeConverter((xRoot.FirstChild.Attributes.GetNamedItem("TimeStart")).Value);
            d.TimeStop = DataTimeConverter((xRoot.FirstChild.Attributes.GetNamedItem("TimeStop")).Value);
            d.nameObject = (xRoot.FirstChild.Attributes.GetNamedItem("nameObject")).Value;
            d.averaging_interval_time = (xRoot.FirstChild.Attributes.GetNamedItem("averaging_interval_time")).Value;
            d.averaging_interval = (xRoot.FirstChild.Attributes.GetNamedItem("averaging_interval")).Value;
            d.active_cxema = (xRoot.FirstChild.Attributes.GetNamedItem("active_cxema")).Value;

            return d;
        }
        public Result_Check_PKE XmlParserResult(string PathFile)
        {
            var q = new Result_Check_PKE();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(PathFile);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            q.pke_cxema = (xRoot.LastChild.Attributes.GetNamedItem("pke_cxema")).Value;
            if (q.pke_cxema == "1")
            {
                q.UA = (xRoot.LastChild.Attributes.GetNamedItem("UA")).Value;
                q.PA = (xRoot.LastChild.Attributes.GetNamedItem("PA")).Value;
                q.QA = (xRoot.LastChild.Attributes.GetNamedItem("QA")).Value;
                q.SA = (xRoot.LastChild.Attributes.GetNamedItem("SA")).Value;
            }
            if (q.pke_cxema == "2" || q.pke_cxema == "3")
            {
                if (q.pke_cxema == "2")
                {
                    q.IAB = (xRoot.LastChild.Attributes.GetNamedItem("IAB")).Value;
                    q.IBC = (xRoot.LastChild.Attributes.GetNamedItem("IBC")).Value;
                    q.ICA = (xRoot.LastChild.Attributes.GetNamedItem("ICA")).Value;
                    q.sigmaUyAB = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyAB")).Value;
                    q.sigmaUyBC = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyBC")).Value;
                    q.sigmaUyCA = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyCA")).Value;
                }
                else
                {
                    q.UH = (xRoot.LastChild.Attributes.GetNamedItem("UH")).Value;
                    q.PH = (xRoot.LastChild.Attributes.GetNamedItem("PH")).Value;
                    q.QH = (xRoot.LastChild.Attributes.GetNamedItem("QH")).Value;
                    q.SH = (xRoot.LastChild.Attributes.GetNamedItem("SH")).Value;
                    q.IH = (xRoot.LastChild.Attributes.GetNamedItem("IH")).Value;
                    q.KH = (xRoot.LastChild.Attributes.GetNamedItem("KH")).Value;
                    q.sigmaUyA = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyA")).Value;
                    q.sigmaUyB = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyB")).Value;
                    q.sigmaUyC = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUyC")).Value;
                }
                q.UAB = (xRoot.LastChild.Attributes.GetNamedItem("UAB")).Value;
                q.UBC = (xRoot.LastChild.Attributes.GetNamedItem("UBC")).Value;
                q.UCA = (xRoot.LastChild.Attributes.GetNamedItem("UCA")).Value;
                q.IB = (xRoot.LastChild.Attributes.GetNamedItem("IB")).Value;
                q.IC = (xRoot.LastChild.Attributes.GetNamedItem("IC")).Value;
                q.PO = (xRoot.LastChild.Attributes.GetNamedItem("PO")).Value;
                q.PP = (xRoot.LastChild.Attributes.GetNamedItem("PP")).Value;
                q.QO = (xRoot.LastChild.Attributes.GetNamedItem("QO")).Value;
                q.QP = (xRoot.LastChild.Attributes.GetNamedItem("QP")).Value;
                q.SO = (xRoot.LastChild.Attributes.GetNamedItem("SO")).Value;
                q.SP = (xRoot.LastChild.Attributes.GetNamedItem("SP")).Value;
                q.UO = (xRoot.LastChild.Attributes.GetNamedItem("UO")).Value;
                q.UP = (xRoot.LastChild.Attributes.GetNamedItem("UP")).Value;
                q.IO = (xRoot.LastChild.Attributes.GetNamedItem("IO")).Value;
                q.IP = (xRoot.LastChild.Attributes.GetNamedItem("IP")).Value;
                q.KO = (xRoot.LastChild.Attributes.GetNamedItem("KO")).Value;
            }
            q.TimeTek = DataTimeConverter((xRoot.LastChild.Attributes.GetNamedItem("TimeTek")).Value);
            q.Freq = (xRoot.LastChild.Attributes.GetNamedItem("Freq")).Value;
            q.sigmaUy = (xRoot.LastChild.Attributes.GetNamedItem("sigmaUy")).Value;
            q.IA = (xRoot.LastChild.Attributes.GetNamedItem("IA")).Value;

            return q;
        }
        public string DataTimeConverter(string Millisecond)
        {
            string data_time = "";
            DateTime data_start = new DateTime(1970, 1, 1, 00, 00, 00);
            data_time = data_start.AddMilliseconds(double.Parse(Millisecond)).ToString();
            return data_time;

        }
        public void Preobrazovanie()
        {
            foreach (var name in Rm3_пке)
            {
                foreach (var start in name.Start_Check_PKE)
                {
                    for (int i = 0; i < start.UID.Count; i++)
                    {
                        var temp_uid = new UID();
                        temp_uid.uid = start.UID[i].uid;
                        temp_uid.nameObject = start.UID[i].Param_Check_PKE[i].nameObject;
                        temp_uid.start_check_uid = start.UID[i].Param_Check_PKE[i].TimeStart;
                        temp_uid.stop_check_uid = start.UID[i].Param_Check_PKE[start.UID[i].Param_Check_PKE.Count - 1].TimeStop;
                        temp_uid.active_cxema = start.UID[i].Param_Check_PKE[i].active_cxema;
                        temp_uid.averaging_interval_time = start.UID[i].Param_Check_PKE[i].averaging_interval_time;
                        uids.Add(temp_uid);
                    }
                }
            }
        }
        public void ShowTable(UID select_uid)
        {
            result.Clear();
            var temp_result = new Result_Check_PKE();
            foreach (var name in Rm3_пке)
            {
                if (name.NameObject == select_uid.nameObject)
                {
                    foreach (var start in name.Start_Check_PKE)
                    {
                        foreach (var s in start.UID)
                        {
                            if (s.uid == select_uid.uid)
                            {
                                foreach (var r in s.Result_Check_PKE)
                                { result.Add(r); }
                            }
                        }
                    }
                }
            }          
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
