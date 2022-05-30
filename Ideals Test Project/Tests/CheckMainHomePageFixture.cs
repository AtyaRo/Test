using Ideals_Test_Project.Pages;
using Ideals_Test_Project.TestSources;

namespace Ideals_Test_Project.Tests
{
    [TestFixture]
    [TestFixtureSource(typeof(DriverSource), nameof(DriverSource.Drivers))]
    public class CheckMainHomePageFixture : BaseTest
    {
        private HomePage _homePage;

        public CheckMainHomePageFixture(string driverSource) : base(driverSource)
        {
        }

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            BaseTearDown();
        }

        [Test]
        public void CheckHomePageMainElements()
        {
            OpenHomePage();
            CheckHeaderElements();
            CheckMainBlockElements();
            CheckFooterElements();
        }

        private void OpenHomePage()
        {
            _homePage.OpenHomePage();
        }

        private void CheckHeaderElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.ContactUs, "Contact us element is not present on the page");
                Assert.IsNotNull(_homePage.HeaderBanner, "HeaderBannerelement is not present on the page");
                Assert.IsNotNull(_homePage.SignIn, "SignIn element is not present on the page");
                Assert.IsNotNull(_homePage.CategoriesBlock, "CategoriesBlock us element is not present on the page");
                Assert.IsNotNull(_homePage.SearchField, "Search element is not present on the page");
                Assert.IsNotNull(_homePage.AddToCartBtn, "Add to cart element is not present on the page");
            });
        }

        private void CheckMainBlockElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.SaleBlock, "SAle block element is not present on the page");
                Assert.IsNotNull(_homePage.Popular, "Popular element is not present on the page");
                Assert.IsNotNull(_homePage.BestSellers, "BestSellers element is not present on the page");
                Assert.IsNotEmpty(_homePage.FeaturedHomePageItems, "Featured items are not present on the page");
            });
        }

        private void CheckFooterElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.SocialBlock, "Social block element is not present on the page");
                Assert.IsNotNull(_homePage.Newsletter, "Newsletter element is not present on the page");
                Assert.IsNotNull(_homePage.StoreInfo, "StoreInfo element is not present on the page");
            });
        }

    }
}
