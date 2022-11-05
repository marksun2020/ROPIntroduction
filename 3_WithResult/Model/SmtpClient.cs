namespace WithResult.Model
{
    public class SmtpClient
    {
        public Result SendEmailConfirmation(string customerName, string email)
        {
            try
            {
                // ...
            }
            catch (Exception)
            {
                return Result.Fail($"Cannot send email confirmation to the customer {customerName}");
            }

            return Result.Ok();
        }

        public void RollbackEmailChange() { }
    }
}