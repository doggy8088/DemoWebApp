public class MyService
{
    public Guid ID { get; set; }
    public MyService()
    {
        this.ID = Guid.NewGuid();
    }
    
    public string DoSomething()
    {
        return "Hello World " + this.ID;
    }
}