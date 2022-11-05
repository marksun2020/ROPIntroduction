namespace Initial.Model;

public class Customer
{
    public string Name { get; set; } = "Customer";
    public string Email { get; set; } = "";

    public Customer UpdateEmail(string email)
    {
        Customer newCustomer = new Customer { Name = this.Name, Email = email };

        return newCustomer;
    }
}
