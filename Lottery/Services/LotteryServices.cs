using Lottery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Services
{
    public class LotteryServices
    {
        private LotteryEntities db = new LotteryEntities();

        int minNum = 1;
        int maxNum = 20;
        int allCount = 180;

        public void AddLotteryData()
        {
            //List<Lottery_step1> myLists = new List<Lottery_step1>();

            for (int x = 1; x <= allCount; x++)
            {//電腦自動選號(多組、但是未確認有無重複)
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                List<int> randomList = Enumerable.Range(minNum, maxNum).OrderBy(q => rnd.Next()).Take(5).ToList();
                randomList.Sort();
                var lottery_item = new Lottery_step1()
                {
                    lottery_num_1 = randomList[0],
                    lottery_num_2 = randomList[1],
                    lottery_num_3 = randomList[2],
                    lottery_num_4 = randomList[3],
                    lottery_num_5 = randomList[4]
                };

                //myLists.Add(lottery_item);
                db.Lottery_step1.Add(lottery_item);
            }
            db.SaveChanges();
        }

        public void DeleteLotteryData(List<Lottery_step1> Lottery_step1_list)
        {
            db.Lottery_step1.RemoveRange(Lottery_step1_list);
            db.SaveChanges();
        }

        public List<Lottery_step1> GetLotteryData()
        {
            var x = db.Lottery_step1.ToList();

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
                winner = random_emp_id
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

        public Lottery_step1 GetLotteryDataSingle(string winner)
        {
            Lottery_step1 x = db.Lottery_step1.FirstOrDefault(s => s.winner == winner);

            return x;
        }

        public IQueryable<Lottery_step1> GetLotteryLlist(string winner)
        {
            //回傳List
            var myLists = db.Lottery_step1.Where(p => p.winner == winner);

            return myLists;
        }
    }
}