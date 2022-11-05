namespace Protected;

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
        string error;

        // get the customer
        Customer customer = this.database.GetById(customerId);
        if (customer == null)
        {
            error = $"Cannot find customer with id {customerId}";
            this.logger.Error(error);
            return error;
        }

        // update customer email
        Customer updatedCustomer;
        try
        {
            updatedCustomer = customer.UpdateEmail(email);
        }
        catch (Exception)
        {
            error = $"Cannot update customer with the new email {email}";
            this.logger.Error(error);
            return error;
        }

        // send confirmation email to check if the email is valid
        try
        {
            smtpClient.SendEmailConfirmation(updatedCustomer.Name, email);
        }
        catch (Exception)
        {
            error = $"Cannot send email confirmation to the customer {updatedCustomer.Name}";
            this.logger.Error(error);
            return error;
        }

        // store the new email into the db
        try
        {
            this.database.Save(updatedCustomer);
        }
        catch (Exception)
        {
            error = $"Cannot save customer {updatedCustomer.Name}";
            this.logger.Error(error);
            return error;
        }

        // log the successful result
        this.logger.Info("Email changed successfully");

        return "OK";
    }
}
