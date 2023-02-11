namespace thirdProject {

    class Book {
        public string title;
        public string author;
        private int pages;

        public Book(string aTitle, string aAuthor, int aPages) {
            title = aTitle;
            author = aAuthor;
            Pages = aPages;
            Console.WriteLine("title " + title + " author " + author + " pages " + pages);
        }

        public int Pages {
            get { return pages; }
            set {
                if(value >= 0) {
                    pages = value;
                }
                else {
                    pages = -1;
                }
            }
        }




    }

}
