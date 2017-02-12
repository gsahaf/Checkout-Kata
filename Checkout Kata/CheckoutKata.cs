using System.Collections.Generic;
using System.Linq;

namespace Checkout_Kata
{
    public class CheckoutKata
    {
        private decimal TotalPrice;
        private List<Item> _items;
        private List<Rule> _rules;
        private List<ShoppedItem> _shoppedItems;

        public CheckoutKata(List<Item> items, List<Rule> rules, List<ShoppedItem> ShoppedItems)
        {
            _items = items;
            _rules = rules;
            _shoppedItems = ShoppedItems;
        }
        public decimal CalculateTotalPrice()
        {
            if (_items == null)
                throw new System.ArgumentException("There is No Item in the market!");

            if (_shoppedItems== null)
                throw new System.ArgumentException("There is No Item in the shopping list!");

            if (_rules == null)
                _rules = new List<Rule>();

            foreach (var item in _shoppedItems)
            {
                var itemPrice = _items.Find(i => i.Name == item.Name).Price;
                var ruleItem = _rules.Find(i => i.ItemName == item.Name);

                if (ruleItem != null)
                {
                    TotalPrice += (item.Count / ruleItem.Count) * ruleItem.Price +
                        (item.Count % ruleItem.Count) * itemPrice;
                }
                else
                {
                    TotalPrice += item.Count * itemPrice;
                }

            }
            return TotalPrice;

        }

        public bool AddItems(Item item)
        {
            if (item == null)
                return false;
            if (item.Name == null)
                return false;

            _items.Add(item);
            return true;
        }

        public bool AddRule(Rule rule)
        {
            if (rule == null)
                return false;

            if (rule.ItemName == null)
                return false;

            _rules.Add(rule);
            return true;
        }

        public bool AddShoppedItem(ShoppedItem shoppedItem)
        {
            if (shoppedItem == null)
                return false;
            _shoppedItems.Add(shoppedItem);
            return true;
        }

    }
}
