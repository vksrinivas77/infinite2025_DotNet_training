using System;


namespace CC_1
{
     class largest
    {
        public void check_large()
        {
            int row1 = Large_Number(1, 2, 3);
            int row2 = Large_Number(1, 3, 2);
            int row4 = Large_Number(1, 2, 2);
            int row3 = Large_Number(1, 1, 1);
            Console.WriteLine($"Output: {row1}");
            Console.WriteLine($"Output: {row2}");
            Console.WriteLine($"Output: {row3}");
            Console.WriteLine($"Output: {row4}");
            Console.ReadLine();
        }

        public int Large_Number(int n1, int n2, int n3)
        {
            int largest_num = (n1 >= n2 && n1 >= n3) ? n1 :
                          (n2 >= n1 && n2 >= n3) ? n2 :
                          n3;

            return largest_num;
        }
    }
}