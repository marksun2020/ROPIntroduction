namespace WithResult.Model
{
    public class Customer
    {
        public string Name { get; set; } = "Customer";
        public string Email { get; set; } = "test@tst.tst";

        public Result<Customer> UpdateEmail(string email)
        {
            Customer newCustomer;
            try
            {
                // update email
                newCustomer = new Customer { Name = this.Name, Email = email };
            }
            catch (Exception)
            {
                return Result.Fail<Customer>($"Cannot update customer with the new email {email}");
            }
            return Result.Ok<Customer>(newCustomer);
        }
    }
}