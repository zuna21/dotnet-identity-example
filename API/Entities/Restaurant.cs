namespace API;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public Owner Owner { get; set; }
    public ICollection<Waiter> Waiters { get; set; }
}
