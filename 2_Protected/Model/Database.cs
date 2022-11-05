namespace Protected.Model;

public class Database
{
    public Customer GetById(int id)
    {
        Customer customer = new Customer();
        
        // ...

        return customer;
    }

    public void Save(Customer customer) { }
}
