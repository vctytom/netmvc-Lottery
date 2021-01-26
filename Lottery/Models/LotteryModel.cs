using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Models
{
    public class LotteryModel
    {
        public class RootObject
        {
            public string lottery_id { get; set; }
            public string emp_name { get; set; }
            public string emp_id { get; set; }
            public List<string> lottery_list { get; set; }
        }
    }
}