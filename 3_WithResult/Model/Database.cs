namespace WithResult.Model
{
    public class Database
    {
        public Result<Customer> GetById(int id)
        {
            try
            {
                // ...
                return Result.Ok(new Customer());
            }
            catch (Exception)
            {
                return Result.Fail<Customer>($"Cannot find customer with id {id}");
            }
        }

        public Result Save(Customer customer)
        {
            try
            {
                // ...
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail($"Cannot save customer {customer.Name}");
            }
        }
    }
}