using System.Collections;

namespace Study.MyBatis
{
    public class ItemList
    {
        private ArrayList items = new ArrayList();
        public ArrayList Items
        {
            get { return items; }
        }
        public void AddItem(Item item)
        {
            items.Add(item);
        }
    }
}
