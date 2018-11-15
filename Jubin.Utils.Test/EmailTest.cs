using Jubin.Utils.Email;
using Xunit;

namespace Jubin.Utils.Test
{
    public class EmailTest
    {
        [Fact]
        public void Send_An_Email_using_AWS_SES()
        {
            EmailClient.SendEmail("jubin@jubin.net", "Jubin Jose", "jubin.jose@gmail.com",
                "<aws-smtp-username>", "<aws-smtp-password>",
                "AWS Test", "<b>Hello</b>");
        }
    }
}
