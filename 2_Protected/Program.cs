namespace Protected;

public class Program
{

    public static void Main(string[] args)
    {
        var service = new CustomerService();
        var result = service.UpdateEmail(1, "new email");

        Console.WriteLine($"Result: {result}");
    }


}