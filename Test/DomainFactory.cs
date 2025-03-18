namespace hamaraBasket.Tests
{
    internal class DomainFactory
    {
        internal Item SingleItemProvider(string name, int sellIn, int quality)
        {
            return CreateItemFactory(name, sellIn, quality);
        }

        internal void UpdateQuality(Item item)
        {
            HamaraBasket app = new([item]);
            app.UpdateQuality();
        }

        public static Item CreateItemFactory(string name, int sellIn, int quality)
        {
            return name switch
            {
                "Indian Wine" => new IndianWine(name, sellIn, quality),
                "Movie Tickets" => new MovieTickets(name, sellIn, quality),
                "Forest Honey" => new ForestHoney(name, sellIn, quality),
                _ => new RegularItem(name, sellIn, quality),
            };
        }
    }
}
