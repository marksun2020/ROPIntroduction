namespace WithResult
{
    using WithResult.Model;

    public class CustomerService
    {
        SmtpClient smtpClient;
        Database database;
        Logger logger;

        public CustomerService()
        {
            this.smtpClient = new SmtpClient();
            this.database = new Database();
            this.logger = new Logger();
        }

        public string UpdateEmail(int customerId, string email)
        {
            // get the customer
            Result<Customer> getCustomerResult = this.database.GetById(customerId);
            if (getCustomerResult.IsFailure)
            {
                this.logger.Error(getCustomerResult.Error);
                return getCustomerResult.Error;
            }

            Customer customer = getCustomerResult.Value;

            // update customer email
            Result<Customer> updateMailResult = customer.UpdateEmail(email);
            if (updateMailResult.IsFailure)
            {
                this.logger.Error(updateMailResult.Error);
                return updateMailResult.Error;
            }

            Customer updatedCustomer = updateMailResult.Value;

            // send confirmation email to check if the email is valid
            Result sendConfirmationResult = smtpClient.SendEmailConfirmation(customer.Name, email);
            if (sendConfirmationResult.IsFailure)
            {
                this.logger.Error(sendConfirmationResult.Error);
                return sendConfirmationResult.Error;
            }
            // What about errors?

            // store the new email into the db
            Result saveCustomerResult = this.database.Save(customer);
            if (saveCustomerResult.IsFailure)
            {
                this.smtpClient.RollbackEmailChange();
                this.logger.Error(saveCustomerResult.Error);
                return saveCustomerResult.Error;
            }

            // log the successful result
            this.logger.Info("Email changed successfully");

            return "OK";
        }
    }
}