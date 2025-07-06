using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    public class BookShelf
    {
        private Book[] books = new Book[5];

        // Indexer
        public Book this[int index]
        {
            get
            {
                if (index >= 0 && index < books.Length)
                    return books[index];
                else
                    throw new IndexOutOfRangeException("Invalid index.");
            }
            set
            {
                if (index >= 0 && index < books.Length)
                    books[index] = value;
                else
                    throw new IndexOutOfRangeException("Invalid index.");
            }
        }

        public void DisplayAll()
        {
            foreach (var book in books)
            {
                if (book != null)
                    book.Display();
            }
        }
    }

}
