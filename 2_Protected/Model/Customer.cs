namespace Protected.Model;

public class Customer
{
    public string Name { get; set; } = "Customer";
    public string Email { get; set; } = "test@tst.tst";

    public Customer UpdateEmail(string email)
    {
        Customer newCustomer = new Customer { Name = this.Name, Email = email };

        return newCustomer;
    }
}
