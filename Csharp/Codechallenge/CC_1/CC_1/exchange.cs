using System;


namespace CC_1
{
     class exchnage
    {
        public void swap_string()
        {
            string input1 = "abcd";
            string result1 = swapfirststandlast(input1);
            Console.WriteLine($"Output: {result1}");

            string input2 = "a";
            string result2 = swapfirststandlast(input2);
            Console.WriteLine($"Output: {result2}");

            string input3 = "xy";
            string result3 = swapfirststandlast(input3);
            Console.WriteLine($"Output: {result3}");
            Console.ReadLine();
        }

        public string swapfirststandlast(string str)
        {
            if (str.Length <= 1)
            {
                return str;
            }

            string firstword = str[0].ToString();
            string lastword = str[str.Length - 1].ToString();
            string c_word = str.Substring(1, str.Length - 2);
            return lastword + c_word + firstword;
        }
    }
}