using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout_Kata;
using System.Collections.Generic;

namespace UnitTestKata
{
    [TestClass]
    public class UnitTestKata
    {
        private CheckoutKata checkoutKata;
        private List<Item> items;
        private List<ShoppedItem> shoppedTtems;
        private List<Rule> rules;

        [TestInitialize]
        public void Setup()
        {
            items = new List<Item>()
                        {
                            new Item { Name="A", Price= 50} ,
                            new Item { Name = "B", Price = 30 },
                            new Item { Name = "C", Price = 60 },
                            new Item { Name = "D", Price = 15 }
                        };

            rules = new List<Rule>()
                        {
                            new Rule { ItemName = "A", Count = 3, Price = 120 },
                            new Rule { ItemName = "B", Count = 2, Price = 45 }
                        };

            shoppedTtems = new List<ShoppedItem>()
                        {
                            new ShoppedItem { Name="A", Count= 1} ,
                            new ShoppedItem { Name="B", Count= 1} ,
                            new ShoppedItem { Name="C", Count= 1} ,
                            new ShoppedItem { Name="D", Count= 1}
                        };


            checkoutKata = new CheckoutKata(items,rules, shoppedTtems);
        }

        [TestMethod]
        public void TestCheckoutKata()
        {
            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(155, result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenItemIsNull()
        {
            try
            {
                items = null;
                rules = new List<Rule>();
                shoppedTtems = new List<ShoppedItem>();
                checkoutKata.CalculateTotalPrice();
                Assert.Fail("There is No Item in the market!");
            }
            catch
            { }
        }

        [TestMethod]
        public void TestCheckoutKataWhenShoppedItemIsNull()
        {
            try
            {
                items = new List<Item>();
                rules = new List<Rule>();
                shoppedTtems = null;
                checkoutKata.CalculateTotalPrice();
                Assert.Fail("There is No Item in the shopping list!");
            }
            catch { }
        }

    }

    [TestClass]
    public class UnitTestKataWhenAddItems
    {
        private CheckoutKata checkoutKata;
        private List<Item> items = new List<Item>();
        private List<Rule> rules = new List<Rule>();
        private List<ShoppedItem> shoppedTtems = new List<ShoppedItem>();

        private Item item;
        private ShoppedItem shoppedItem;

        [TestInitialize]
        public void Setup()
        {
            rules = new List<Rule>()
                        {
                            new Rule { ItemName = "A", Count = 3, Price = 120 },
                            new Rule { ItemName = "B", Count = 2, Price = 45 }
                        };
            checkoutKata = new CheckoutKata(items, rules, shoppedTtems);
        }

        [TestMethod]
        public void AddItemWhenItemIsNull()
        {
            var result = checkoutKata.AddItems(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddItemWhenNameItemIsNull()
        {
            item = new Item() { Name = null, Price = 55 };
            var result = checkoutKata.AddItems(item);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddItemWhenNameItemIsNotNull()
        {
            item = new Item() { Name = "A", Price = 55 };
            var result = checkoutKata.AddItems(item);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenAddNewItemWithoutOffer()
        {
            item = new Item() { Name = "E", Price = 45 };
            checkoutKata.AddItems(item);

            shoppedItem = new ShoppedItem() { Name = "E", Count = 1 };
            checkoutKata.AddShoppedItem(shoppedItem);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(45, result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenMatchNoOffer()
        {
            item = new Item() { Name = "A", Price = 50 };
            checkoutKata.AddItems(item);

            shoppedItem = new ShoppedItem() { Name = "A", Count = 2 };
            checkoutKata.AddShoppedItem(shoppedItem);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenMatchOffer()
        {
            item = new Item() { Name = "B", Price = 30 };
            checkoutKata.AddItems(item);

            shoppedItem = new ShoppedItem() { Name = "B", Count = 2 };
            checkoutKata.AddShoppedItem(shoppedItem);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(45, result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenHaveNormalPriceAndOffer()
        {
            item = new Item() { Name = "B", Price = 30 };
            checkoutKata.AddItems(item);

            shoppedItem = new ShoppedItem() { Name = "B", Count = 3 };
            checkoutKata.AddShoppedItem(shoppedItem);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(75, result);
        }

    }

    [TestClass]
    public class UnitTestKataWhenAddRules
    {
        private CheckoutKata checkoutKata;
        private List<Item> items = new List<Item>();
        private List<Rule> rules = new List<Rule>();
        private List<ShoppedItem> shoppedTtems = new List<ShoppedItem>();

        private Rule rule;

        [TestInitialize]
        public void Setup()
        {
            items = new List<Item>()
                        {
                            new Item { Name="A", Price= 50} ,
                            new Item { Name = "B", Price = 30 },
                            new Item { Name = "C", Price = 60 },
                            new Item { Name = "D", Price = 15 }
                        };
            shoppedTtems = new List<ShoppedItem>()
            {
                new ShoppedItem {Name="A" ,Count= 1 },
                new ShoppedItem {Name="B" ,Count= 1 },
                new ShoppedItem {Name="C" ,Count= 1 },
                new ShoppedItem {Name="D" ,Count= 1 }

            };
            checkoutKata = new CheckoutKata(items,rules, shoppedTtems);
        }

        [TestMethod]
        public void AddItemWhenRuleIsNull()
        {
            var result = checkoutKata.AddRule(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddRuleWhenItemNameIsNull()
        {
            rule = new Rule() { ItemName = null, Count = 2, Price = 100 };
            var result = checkoutKata.AddRule(rule);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddRuleWhenItemNameIsNotNull()
        {
            rule = new Rule() { ItemName = "A", Count = 3, Price = 120 };
            var result = checkoutKata.AddRule(rule);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenAddNewRulewithoutItemIncluded()
        {
            rule = new Rule() { ItemName = "A", Count = 3, Price = 120 };
            checkoutKata.AddRule(rule);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(155, result);
        }

        [TestMethod]
        public void TestCheckoutKataWhenAddNewRuleMatchItemIncluded()
        {
            rule = new Rule() { ItemName = "A", Count = 1, Price = 35 };
            checkoutKata.AddRule(rule);

            var result = checkoutKata.CalculateTotalPrice();
            Assert.AreEqual(140, result);
        }

    }
}
