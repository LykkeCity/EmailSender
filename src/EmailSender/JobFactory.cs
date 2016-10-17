using Microsoft.Practices.Unity;

namespace EmailSender
{
    public class JobFactory
    {
        public static void RunJobs(IUnityContainer container)
        {
            container.Resolve<EmailsQueueReader>().Start();
        }
    }
}
