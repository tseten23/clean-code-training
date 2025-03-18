namespace hamaraBasket
{
    public abstract class Item(string name, int sellIn, int quality)
    {
        public string Name { get; protected set; } = name;
        public int SellIn { get; protected set; } = sellIn;
        public int Quality { get; protected set; } = quality;

        public abstract void UpdateQuality();

        protected void IncreaseQuality(int amount)
        {
            Quality = Math.Min(50, Quality + amount);
        }

        protected void DecreaseQuality(int amount)
        {
            Quality = Math.Max(0, Quality - amount);
        }

        protected void DecreaseSellIn()
        {
            SellIn--;
        }

        public override string ToString()
        {
            return $"{Name}, {SellIn}, {Quality}";
        }
    }

    public class RegularItem(string name, int sellIn, int quality) : Item(name, sellIn, quality)
    {
        public override void UpdateQuality()
        {
            DecreaseSellIn();
            DecreaseQuality(SellIn < 0 ? 2 : 1);
        }
    }

    public class IndianWine(string name, int sellIn, int quality) : Item(name, sellIn, quality)
    {
        public override void UpdateQuality()
        {
            DecreaseSellIn();
            IncreaseQuality(1);
        }
    }

    public class MovieTickets(string name, int sellIn, int quality) : Item(name, sellIn, quality)
    {
        public override void UpdateQuality()
        {
            if (SellIn <= 0)
            {
                Quality = 0;
            }
            else if (SellIn <= 5)
            {
                IncreaseQuality(3);
            }
            else if (SellIn <= 10)
            {
                IncreaseQuality(2);
            }
            else
            {
                IncreaseQuality(1);
            }

            DecreaseSellIn();
        }
    }

    public class ForestHoney(string name, int sellIn, int quality) : Item(name, sellIn, quality)
    {
        public override void UpdateQuality()
        {
            // No changes
        }
    }
}
