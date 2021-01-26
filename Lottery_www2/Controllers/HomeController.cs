using Lottery_www2.Models;
using Lottery_www2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery_www2.Controllers
{
    public class HomeController : Controller
    {
        //號碼最大上限
        int maxlottery = 10;
        LotteryServices service = new LotteryServices();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult AddSingleDataRandom(string random_emp_id)
        {
            //電腦自動選號(僅自動新增1筆)
            //判斷有無員工編號
            if (random_emp_id != null)
            {
                //1.判斷此人員是否已經有號碼
                if (service.GetLotteryDataSingle(random_emp_id) != null)
                {
                    var lotteryNum = service.GetLotteryLlist(random_emp_id).Count();
                    if (lotteryNum >= maxlottery)
                    {
                        //回傳錯誤，已達號碼上限
                        return Json(new { status = "error", responseText = String.Format("此人員已有{0}組號碼產生!", maxlottery) });
                    }
                    else
                    {
                        //有號碼，新增第2筆以上
                        //電腦自動選號
                        Lottery_step1 lottery_item = new Lottery_step1();
                        lottery_item = service.AddLotteryDataSingleRandom(random_emp_id);
                        return Json(new { status = "success", responseText = lottery_item }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {   //沒號碼，新增一組號碼
                    //電腦自動選號
                    Lottery_step1 lottery_item = service.AddLotteryDataSingleRandom(random_emp_id);

                    return Json(new { status = "success", responseText = lottery_item }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = "error", responseText = "無此人員" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddSingleData(List<int> lottery_num, string emp_id)
        {
            if (lottery_num != null)
            {
                if (lottery_num.Count == 5)
                {
                    Lottery_step1 item = new Lottery_step1();
                    item.winner = emp_id;
                    item.lottery_num_1 = lottery_num[0];
                    item.lottery_num_2 = lottery_num[1];
                    item.lottery_num_3 = lottery_num[2];
                    item.lottery_num_4 = lottery_num[3];
                    item.lottery_num_5 = lottery_num[4];
                    item.create_date = DateTime.Now;

                    //1.判斷此人員是否已經有號碼
                    if (service.GetLotteryDataSingle(item.winner) != null)
                    {

                        var lotteryNum = service.GetLotteryLlist(item.winner).Count();
                        if (lotteryNum >= maxlottery)
                        {
                            //回傳錯誤，已達號碼上限
                            return Json(new { status = "error", responseText = String.Format("此人員已有{0}組號碼產生!", maxlottery) });
                        }
                        else
                        {
                            //有號碼，新增第2筆以上
                            //人工選號
                            service.AddLotteryDataSingle(item);
                            return Json(new { status = "success", responseText = String.Format("新增成功，第{0}筆", lotteryNum + 1) });
                        }
                    }
                    else
                    {
                        //沒號碼，新增一組號碼
                        //人工選號
                        service.AddLotteryDataSingle(item);
                        return Json(new { status = "success", responseText = "新增成功" });
                    }
                }
            }
            return Json(new { status = "error", responseText = "數字錯誤!" });

        }
    }
}