
namespace Study.MyBatis
{
    public class Item
    {
        private string name;
        private string enable;
        private string path;

        public Item()
        {

        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
    }
}
