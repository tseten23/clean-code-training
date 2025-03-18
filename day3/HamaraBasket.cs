namespace hamaraBasket
{
    public class HamaraBasket(IList<Item> items)
    {
        private readonly IList<Item> items = items;

        public void UpdateQuality()
        {
            foreach (Item item in items)
            {
                item.UpdateQuality();
            }
        }
    } 
}
