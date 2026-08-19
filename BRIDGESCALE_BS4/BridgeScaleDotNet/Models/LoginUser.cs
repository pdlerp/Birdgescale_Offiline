using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeScaleDotNet.Models
{
    public static class LocalStorage
    {
        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static int IsFirstWeight { get; set; } = 1;
        public static int GrossWeight { get; set; } = 0;
        public static int TareWeight { get; set; } = 0;
        public static string CounterID { get; set; }
        public static int OperatorID { get; set; }
    }
}
