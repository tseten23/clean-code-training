using hamaraBasket.Tests;
using Moq;

namespace hamaraBasket
{
    [TestFixture]
    public class HamaraBasketTest
    {
        private DomainFactory domainFactory;

        [SetUp]
        public void Setup()
        {
            domainFactory = new();
        }

        [Test]
        public void ShouldReduceSellInByValueByOne()
        {
            //Arrange
            string name = "Regular Item";
            int sellIn = 10;
            int quality = 10;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
        }

        [Test]
        public void ShouldReduceQualityByValueByOne()
        {
            //Arrange
            string name = "Regular Item";
            int sellIn = 10;
            int quality = 10;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.That(item.Quality, Is.EqualTo(quality - 1));
        }

        [Test]
        public void ShouldDecreaseQualityTwiceAsFastAfterSellIn()
        {
            //Arrange
            string name = "Regular Item";
            int sellIn = 0;
            int quality = 10;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(quality - 2));
            });
        }

        [Test]
        public void QualityShouldNeverGoesBelowZero()
        {
            //Arrange
            string name = "Regular Item";
            int sellIn = 5;
            int quality = 0;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(0));
            });
        }

        [Test]
        public void ShouldIncreaseIndianWineQualityWhenSellInValueGetsDecrease()
        {
            //Arrange
            string name = "Indian Wine";
            int sellIn = 10;
            int quality = 20;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(quality + 1));
            });
        }

        [Test]
        public void ShouldNeverIncreaseOrDecreaseIndianWineQualityWhenExceeds50()
        {
            //Arrange
            string name = "Indian Wine";
            int sellIn = 10;
            int quality = 50;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(50));
            });
        }

        [Test]
        public void ShouldNotChangeForestHoneyQualityOrSellIn()
        {
            //Arrange
            string name = "Forest Honey";
            int sellIn = 10;
            int quality = 30;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn));
                Assert.That(item.Quality, Is.EqualTo(quality));
            });
        }

        [Test]
        public void ShouldIncreaseInMovieTicketsQualityBy2_When10DaysOrLess()
        {
            //Arrange
            string name = "Movie Tickets";
            int sellIn = 10;
            int quality = 20;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(quality + 2));
            });
        }

        [Test]
        public void ShouldIncreaseInMovieTicketsQualityBy3_When5DaysOrLess()
        {
            //Arrange
            string name = "Movie Tickets";
            int sellIn = 5;
            int quality = 20;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(quality + 3));
            });
        }

        [Test]
        public void ShouldDropMovieTicketsQualityToZeroAfterConcert()
        {
            //Arrange
            string name = "Movie Tickets";
            int sellIn = 0;
            int quality = 20;
            Item item = SingleItemProvider(name, sellIn, quality);

            //Act
            UpdateQuality(item);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.SellIn, Is.EqualTo(sellIn - 1));
                Assert.That(item.Quality, Is.EqualTo(0));
            });
        }

        [Test]
        public void UpdateQuality_CallsUpdateQualityOnEachItem()
        {
            // Arrange
            string name = "Movie Tickets";
            int sellIn = 0;
            int quality = 20;

            Mock<Item> mockItem1 = new(name, sellIn, quality);
            Mock<Item> mockItem2 = new(name, sellIn, quality);

            List<Item> items = [mockItem1.Object, mockItem2.Object];

            var hamaraBasket = new HamaraBasket(items);

            // Act
            hamaraBasket.UpdateQuality();

            // Assert
            mockItem1.Verify(i => i.UpdateQuality(), Times.Once);
            mockItem2.Verify(i => i.UpdateQuality(), Times.Once);
        }

        private Item SingleItemProvider(string name, int sellIn, int quality)
        {
            return domainFactory.SingleItemProvider(name, sellIn, quality);
        }

        private void UpdateQuality(Item item)
        {
            domainFactory.UpdateQuality(item);
        }
    }
}
