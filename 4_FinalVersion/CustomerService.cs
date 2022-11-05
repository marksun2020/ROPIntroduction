namespace FinalVersion
{
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
            return this.database.GetById(customerId)
            .OnSuccess(res => res.Value.UpdateEmail(email))
            .OnSuccess(res => smtpClient.SendEmailConfirmation(res.Value.Name, email)
                .OnSuccess(() => res))
            .OnSuccess(res => this.database.Save(res.Value))
            .OnFailure(() => this.smtpClient.RollbackEmailChange())
            .OnBoth(result => LogUpdateEmail(result))
            .OnBoth(result => result.IsSuccess ? "Ok" : result.Error);
        }

        private void LogUpdateEmail(Result result)
        {
            if (result.IsSuccess)
            {
                this.logger.Info("Email changed successfully");
            }
            else
            {
                this.logger.Info(result.Error);
            }
        }
    }
}