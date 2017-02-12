namespace Checkout_Kata
{
    /// <summary>
    /// This class is for base Item info
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// This is the item which is in the shopping list
    /// </summary>
    public class ShoppedItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}