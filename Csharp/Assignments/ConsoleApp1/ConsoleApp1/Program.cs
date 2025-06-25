using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Box box1 = new Box(5);
            Box box2 = new Box(10);
            Box box3 = box1 + box2;

            player p1 = new player(10,20);
            player.check_players();
            Console.WriteLine(box3.Length);
           

            int per_item_cost = 200;
            int item1 = 200;
            int item2 = 200;
            int item3 = 200;
            int item4 = 200;
            int item5 = 200;
            int total_cost;


            total_cost = item1 + item2 + item3 + item4 + item5;
            float discount = 0.10f;
            float final_cost = (per_item_cost * 5) * (1 - discount);
            Console.WriteLine($"the final price :{final_cost}");
            Console.ReadLine();

        }
    }
    public class Box
    {
        public int Length { get; set; }

        public Box(int length)
        {
            Length = length;
        }

       
        public static Box operator +(Box b1, Box b2)
        {
            return new Box(b1.Length + b2.Length);
        }
   
    }
    class player
    {
        public int score { get; set; }
        

        public player(int p1,int  p2)
        {
            score = p1;
            score1 = p2;
        }

        public static player check_players()
        {
            if()
            return 0;
        }
    }
}
