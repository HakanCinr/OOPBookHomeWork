using System.Net;

namespace OOPBookHomeWork
{
    public partial class Form1 : Form
    {
        List<Book> books = new List<Book>();
        bool orderStatus = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {

            WebClient wc = new WebClient();
            string imdbData = wc.DownloadString("https://www.goodreads.com/list/show/18834.BBC_Top_200_Books");


            int contentStartIndex = imdbData.IndexOf("js-dataTooltip\">");

            int contentEndIndex = imdbData.IndexOf("class=\"pagination");

            string content = imdbData.Substring(contentStartIndex, contentEndIndex - contentStartIndex);

            List<string> list = new List<string>();

            while (content.Contains("<tr"))
            {
                int trStartIndex = content.IndexOf("<tr");
                int trEndIndex = content.IndexOf("</tr>");

                string filmContent = content.Substring(trStartIndex, trEndIndex - trStartIndex);

                list.Add(filmContent);

                content = content.Substring(trEndIndex + 2);

            }
            foreach (string str in list)
            {
                Book book = new Book();

                int nameIndex = str.IndexOf("<a title");
                int nameEndIndex = str.IndexOf("href");

                book.Name = str.Substring(nameIndex + 10, nameEndIndex - nameIndex - 12);
                books.Add(book);

            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books;
            lblcount.Text = books.Count.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (orderStatus == false)
            {
                books = books.OrderByDescending(x => x.Name).ToList();
                orderStatus = true;
            }
            else
            {
                books = books.OrderBy(x => x.Name).ToList();
                orderStatus = false;
            }


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = txtsearch.Text.ToLower().Trim();
            var book2 = books.Where(x => x.Name.ToLower().Contains(searchText)).ToList();


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = book2;
            lblcount.Text = book2.Count.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
