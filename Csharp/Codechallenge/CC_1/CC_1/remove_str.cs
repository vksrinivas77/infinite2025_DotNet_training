using System;

namespace CC_1
{
    class remove
    {
        public void remove_str()
        {
            string word1 = "Python";
            int add1 = 1;
            string result1 = Remove_str(word1, add1);
            Console.WriteLine(result1);
            string word2 = "Python";
            int add2 = 0;
            string result2 = Remove_str(word2, add2);
            Console.WriteLine(result2);
            string word3 = "Python";
            int add3 = 4;
            string result3 = Remove_str(word3, add3);
            Console.WriteLine(result3);

            Console.ReadLine();
        }

        public string Remove_str(string str, int index)
        {
            if (index < 0 || index >= str.Length)
            {
                Console.WriteLine("Index is out of range.");
            }

            return str.Remove(index, 1);
        }
    }
}