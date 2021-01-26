using Lottery_www2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery_www2.Services
{
    public class LotteryServices
    {
        int minNum = 1;
        int maxNum = 20;
        private LotteryEntities db = new LotteryEntities();

        public Lottery_step1 GetLotteryDataSingle(string winner)
        {
            Lottery_step1 x = db.Lottery_step1.FirstOrDefault(s => s.winner == winner);

            return x;
        }
        public Lottery_step1 AddLotteryDataSingleRandom(string random_emp_id)
        {
            //隨機選號
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<int> randomList = Enumerable.Range(minNum, maxNum).OrderBy(q => rnd.Next()).Take(5).ToList();
            randomList.Sort();
            var lottery_item = new Lottery_step1()
            {
                lottery_num_1 = randomList[0],
                lottery_num_2 = randomList[1],
                lottery_num_3 = randomList[2],
                lottery_num_4 = randomList[3],
                lottery_num_5 = randomList[4],
                winner = random_emp_id,
                create_date=DateTime.Now
            };
            db.Lottery_step1.Add(lottery_item);
            db.SaveChanges();
            return lottery_item;
        }
        public void AddLotteryDataSingle(Lottery_step1 item)
        {
            //人工選號
            if (
                item.lottery_num_1 != null &&
                item.lottery_num_2 != null &&
                item.lottery_num_3 != null &&
                item.lottery_num_4 != null &&
                item.lottery_num_5 != null &&
                item.winner != null
                )
            {
                db.Lottery_step1.Add(item);
                db.SaveChanges();
            }
        }


        public IQueryable<Lottery_step1> GetLotteryLlist(string winner)
        {
            //回傳List
            var myLists = db.Lottery_step1.Where(p => p.winner == winner);

            return myLists;
        }
    }
}