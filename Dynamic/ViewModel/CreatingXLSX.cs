using Dynamic.Model;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Dynamic.ViewModel
{
   public class CreatingXLSX
    {
        public void ConvertXLS(UID select_UID, ObservableCollection<Result_Check_PKE> result)
        {            
            var coll = new List<List<string>>();
            coll= ReturnNotNullProperty(result);            
            XSSFWorkbook wb;
            //Лист в книге Excel
            XSSFSheet sh;
            //Создаем рабочую книгу
            wb = new XSSFWorkbook();
            //Создаём лист в книге
            sh = (XSSFSheet)wb.CreateSheet("Лист 1");
            //Количество заполняемых строк
            int countRow = 0;
            countRow = result.Count + 1;
            //Количество заполняемых столбцов
            int countColumn = 0;
            var kj = new List<string>();            
            switch (select_UID.active_cxema)
            {
                case "1":
                    {
                        string[] NameColumn = { "Схема проверки", "Дата/время", "IA", "Freq", "sigmaUy", "UA", "PA", "QA", "SA" };
                        countColumn = 9;                        
                        for (int p = 0; p < NameColumn.Length; p++) kj.Add(NameColumn[p]);
                        break;
                    }
                case "2": {
                        string[] NameColumn = { "Схема проверки", "Дата/время", "UAB", "UBC", "UCA", "IAB", "IBC", "ICA", "IA","IB","IC","PO",
                            "PP", "QO", "QP","SO", "SP", "UO" , "UP", "IO", "IP" , "KO", "Freq", "sigmaUy" , "sigmaUyAB", "sigmaUyBC", "sigmaUyCA" };
                        countColumn = 27;
                        for (int p = 0; p < NameColumn.Length; p++) kj.Add(NameColumn[p]);
                        break;
                    }
                case "3": {
                        string[] NameColumn = { "Схема проверки", "Дата/время", "UAB", "UBC", "UCA", "IA","IB","IC","PO",
                            "PP", "QO", "QP","SO", "SP", "UO" , "UP", "IO", "IP" , "KO", "Freq", "sigmaUy" , "QH","SH","UH","PH","IH","KH","sigmaUyA","sigmaUyB","sigmaUyC" };
                        countColumn = 30;
                        for (int p = 0; p < NameColumn.Length; p++) kj.Add(NameColumn[p]);
                        break;
                    }
                default:
                    {
                        string[] NameColumn = { "Схема проверки", "Дата/время", "IA", "Freq", "sigmaUy", "UA", "PA", "QA", "SA" };
                        countColumn = 9;
                        break;
                    }
            }            
                //создаем название столбцов
                var nameRow = sh.CreateRow(0);
                for (byte i = 0; i < kj.Count; i++)
                {
                    var nameCell = nameRow.CreateCell(i);
                    nameCell.SetCellValue(kj[i]);
                }                
                for (int i = 1; i < countRow; i++)
                {
                    nameRow= sh.CreateRow(i);
                    for (int j = 0; j < countColumn; j++)
                    {
                        var nameCell = nameRow.CreateCell(j);
                        nameCell.SetCellValue(coll[i-1][j]);
                        sh.AutoSizeColumn(j);
                    }
                }            
            // Удалим файл если он есть уже
            if (!File.Exists("Result.xlsx"))
            {
                File.Delete("Result.xlsx");
            }
            //запишем всё в файл
            try
            {
                using (var fs = new FileStream("Result.xlsx", FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
                //Откроем файл
                // Process.Start("Result.xlsx");
                MessageBox.Show("Файл создан в папке с программой, называется Result.xlsx");
            }
            catch (System.Exception)
            {
                MessageBox.Show("Закройте файл Excel, для перезаписи файла");               
            }                        
        }
        public List<List<string>> ReturnNotNullProperty(ObservableCollection<Result_Check_PKE> result)
        {               
            var collection = new List<List<string>>();                       
           foreach (var f in result)
            {
                var array = new List<string>();
                var a = f.GetType().GetProperties(); //получаем массив свойств класса Result_Check_PKE
                for (int k = 0; k < a.Length; k++)
                {
                    if (null != (a[k].GetValue(f))) // проверяем есть ли в свойстве значение
                    {                        
                        array.Add((a[k].GetValue(f)).ToString()); //записываем значение в коллекцию
                    }
                }
                collection.Add(array);
           }
            return collection;
        }
    }
}
