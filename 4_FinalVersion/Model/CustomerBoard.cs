namespace FinalVersion.Model
{
    public class SmtpClient
    {
        public Result SendEmailConfirmation(string customerName, string email)
        {
            return Result.Ok();
        }

        public Result RollbackEmailChange()
        {

            return Result.Ok();
        }
    }
}