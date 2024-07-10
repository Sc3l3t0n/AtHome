namespace AtHome.WebApi.Models;

public class Item
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public Place Place { get; set; }
    public DateTime StoreDate { get; set; }
    public DateTime? ExpireDate { get; set; }
}