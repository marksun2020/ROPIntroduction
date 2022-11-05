namespace FinalVersion.Model
{
    public class Database
    {
        public Result<Customer> GetById(int id)
        {
            try
            {
                Customer customer = new Customer();
                // ...
                return Result.Ok(customer);
            }
            catch (Exception)
            {
                return Result.Fail<Customer>($"Cannot find customer with id {id}");
            }
        }

        public Result<String> Save(Customer customer)
        {
            try
            {
                // ...
                return Result.Ok<string>("");
            }
            catch (Exception)
            {
                return Result.Fail<String>($"Cannot save customer {customer.Name}");
            }
        }
    }
}