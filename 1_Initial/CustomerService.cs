namespace Initial;

using Model;

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
        Customer customer = this.database.GetById(customerId);
        // update customer email
        Customer updatedCustomer = customer.UpdateEmail(email);
        // send confirmation email to check if the email is valid
        smtpClient.SendEmailConfirmation(updatedCustomer.Name, email);
        // store the new email into the db
        this.database.Save(updatedCustomer);
        // log the successful result
        this.logger.Info("Email changed successfully");

        return "OK";
    }
}
