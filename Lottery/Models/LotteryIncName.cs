using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Models
{
    public class LotteryIncName
    {
        public int lottery_id { get; set; }
        public Nullable<int> lottery_num_1 { get; set; }
        public Nullable<int> lottery_num_2 { get; set; }
        public Nullable<int> lottery_num_3 { get; set; }
        public Nullable<int> lottery_num_4 { get; set; }
        public Nullable<int> lottery_num_5 { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
    }
}