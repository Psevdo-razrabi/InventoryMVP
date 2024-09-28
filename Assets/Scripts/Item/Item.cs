using CustomObserverable;

public class Item
{
    public GuidItem Id { get; private set; }
    public ItemDescription ItemDescription { get; private set; }
    public int Quantity { get; private set; }

    public Item(ItemDescription itemDescription, int quantity = 1)
    {
        Id = GuidItem.NewGuid();
        ItemDescription = itemDescription;
        Quantity = quantity;
    }
}