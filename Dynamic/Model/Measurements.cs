using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dynamic.Model
{
    public class RM3_ПКЭ
    {
        public string NameObject { get; set; } //имя папки объекта 

        public List<Start_Check_PKE> Start_Check_PKE { get; set; }
        
        public RM3_ПКЭ()
        { }

        //public RM3_ПКЭ(string uID, Param_Check_PKE param_Check_PKE, Result_Check_PKE result_Check_PKE)
        //{
        //    UID = uID;
        //    Param_Check_PKE = param_Check_PKE;
        //    Result_Check_PKE = result_Check_PKE;
        //}
    }
    public class Start_Check_PKE
    {
        public List<UID> UID { get; set; }
        public string Start_Check { get; set; }
        
        public Start_Check_PKE() { }
    }
    public class UID
    {
        public string uid { get; set; }
        public string start_check_uid { get; set; }
        public string stop_check_uid { get; set; }
        public string active_cxema { get; set; }
        public string nameObject { get; set; }
        public string averaging_interval_time { get; set; }
        public List<Param_Check_PKE> Param_Check_PKE { get; set; }
        public List<Result_Check_PKE> Result_Check_PKE { get; set; }
        public UID() { }
    }
    public class Param_Check_PKE
    {
        public string TimeStart { get; set; }
        public string TimeStop { get; set; }
        public string nameObject { get; set; }
        public string averaging_interval_time { get; set; }
        public string averaging_interval { get; set; }
        public string active_cxema { get; set; }
        public Param_Check_PKE() { }
    }
    public class Result_Check_PKE 
    {
        public string pke_cxema { get; set; }
        public string TimeTek { get; set; }
        public string UAB { get; set; }
        public string UBC { get; set; }
        public string UCA { get; set; }
        public string IAB { get; set; }
        public string IBC { get; set; }
        public string ICA { get; set; }
        public string IA { get; set; }
        public string IB { get; set; }
        public string IC { get; set; }
        public string PO { get; set; }
        public string PP { get; set; }
        public string QO { get; set; }
        public string QP { get; set; }
        public string SO { get; set; }
        public string SP { get; set; }
        public string UO { get; set; }
        public string UP { get; set; }
        public string IO { get; set; }
        public string IP { get; set; }
        public string KO { get; set; }
        public string Freq { get; set; }
        public string sigmaUy { get; set; }
        public string sigmaUyAB { get; set; }
        public string sigmaUyBC { get; set; }
        public string sigmaUyCA { get; set; }
        public string UA { get; set; }
        public string PA { get; set; }
        public string QA { get; set; }
        public string SA { get; set; }
        public string QH { get; set; }
        public string SH { get; set; }
        public string UH { get; set; }
        public string PH { get; set; }
        public string IH { get; set; }
        public string KH { get; set; }
        public string sigmaUyA { get; set; }
        public string sigmaUyB { get; set; }
        public string sigmaUyC { get; set; }
        public Result_Check_PKE() { }
        
    }
}
