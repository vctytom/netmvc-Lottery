using Lottery.Models;
using Lottery.Models.ViewModel;
using Lottery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Lottery.Models.LotteryModel;

namespace Lottery.Controllers
{
    public class HomeController : Controller
    {
        private LotteryEntities db = new LotteryEntities();

        LotteryServices service = new LotteryServices();
        //號碼最大上限
        int maxlottery = 10;

        public ActionResult Index()
        {
            var data =
                (from d in db.Lottery_step1
                 join c in db.EMPs on d.winner equals c.emp_id into c_sub
                 from c in c_sub.DefaultIfEmpty()

                 select new IndexViewModel
                 {
                     emp_id = d.winner,
                     lottery_num_1 = d.lottery_num_1,
                     lottery_num_2 = d.lottery_num_2,
                     lottery_num_3 = d.lottery_num_3,
                     lottery_num_4 = d.lottery_num_4,
                     lottery_num_5 = d.lottery_num_5
                 }).ToList();
            

            ViewBag.SumCount = data.Count();

            return View(data);
        }

        public ActionResult About()
        {
            //檢查是否有重複值
            List<Lottery_step1> Lottery_step1_list = service.GetLotteryData();

            var data = (from a in Lottery_step1_list
                        group new
                        {
                            a.lottery_num_1,
                            a.lottery_num_2,
                            a.lottery_num_3,
                            a.lottery_num_4,
                            a.lottery_num_5
                        }
                        by new
                        {
                            a.lottery_num_1,
                            a.lottery_num_2,
                            a.lottery_num_3,
                            a.lottery_num_4,
                            a.lottery_num_5
                        } into g
                        where g.Count() > 1
                        select g.Key).ToList();

            ListNumViewModel vmAbout = new ListNumViewModel();
            List<num_item> num_list = new List<num_item>();
            foreach (var i in data)
            {
                num_item n = new num_item();
                n.lottery_num_1 = i.lottery_num_1;
                n.lottery_num_2 = i.lottery_num_2;
                n.lottery_num_3 = i.lottery_num_3;
                n.lottery_num_4 = i.lottery_num_4;
                n.lottery_num_5 = i.lottery_num_5;
                num_list.Add(n);
            }
            vmAbout.lottery_list = num_list;
            ViewBag.Title = "檢查重複值";

            return View(vmAbout);
        }

        public ActionResult Contact()
        {
            //篩選
            return View();
        }

        public ActionResult GetNumsJSONResult()
        {
            //取得所有號碼，回傳json格式(有排序)
            var data = service.GetLotteryData();
                //.OrderBy(x => x.lottery_num_5)
                //.OrderBy(x => x.lottery_num_4)
                //.OrderBy(x => x.lottery_num_3)
                //.OrderBy(x => x.lottery_num_2)
                //.OrderBy(x => x.lottery_num_1);

            
            List<Dictionary<string, string[]>> ja = new List<Dictionary<string, string[]>>();
            foreach (var item in data)
            {
                RootObject root = new RootObject();
                List<string> lottery_list = new List<string>() {
                    item.lottery_num_1.ToString(),
                    item.lottery_num_2.ToString(),
                    item.lottery_num_3.ToString(),
                    item.lottery_num_4.ToString(),
                    item.lottery_num_5.ToString(),
                };

                //Dictionary<string, string> j_emp_id = new Dictionary<string, string>();
                //Dictionary<string, string[]> j_lottery_num = new Dictionary<string, string[]>();
                //Dictionary<string, List<Dictionary<string, string[]>>> ji = new Dictionary<string, List<Dictionary<string, string[]>>>();
                //j_emp_id.Add("emp_id", item.winner.ToString());

                j_lottery_num.Add("lottery_list", new string[] {
                    
                    item.lottery_num_1.ToString(),
                    item.lottery_num_2.ToString(),
                    item.lottery_num_3.ToString(),
                    item.lottery_num_4.ToString(),
                    item.lottery_num_5.ToString(),
                    
                });
                
                ja.Add(j_lottery_num);
                
            }
            return Json(new { ja }, JsonRequestBehavior.AllowGet);
        }
    }
}