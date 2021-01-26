using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Models.ViewModel
{
    public class ListNumViewModel
    {
        public string title { get; set; }
        public List<num_item> lottery_list { get; set; }
    }
    public class num_item
    {
        public Nullable<int> lottery_num_1 { get; set; }
        public Nullable<int> lottery_num_2 { get; set; }
        public Nullable<int> lottery_num_3 { get; set; }
        public Nullable<int> lottery_num_4 { get; set; }
        public Nullable<int> lottery_num_5 { get; set; }
    }
}