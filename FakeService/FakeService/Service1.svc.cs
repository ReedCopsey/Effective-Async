using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FakeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class QuestionService : IQuestionService
    {
        private static Random random = new Random();

        public int AskDeepThought()
        {
            System.Threading.Thread.Sleep(random.Next(700));
            return 42;
        }

        public string GetQuote()
        {
            var quotes = new [] 
                {
                    "I love deadlines.  I like the wooshing sound they make as they fly by.",
                    "A common mistake people make when trying to design something completely foolproof is to underestimate the ingenuity of complete fools.",
                    "In the beginning the Universe was created.  This has made a lot of people very angry and been widely regarded as a bad move.",
                    "For a moment, nothing happened.  Then, after a second or so, nothing continued to happen",
                    "The knack of flying is learning how to throw yourself at the ground and miss.",
                    "We have normality.  I repeat, we have normality.  Anything you still can't cope with is therefore your own problem.",
                    "Life is wasted on the living.",
                    "I don't believe it.  Prove it to me and I still won't believe it.",
                    "The major difference between a thing that might go wrong and a thing that cannot possibly go wrong is that when a thing that cannot possibly go wrong goes wrong it usually turns out to be impossible to get at and repair.",
                    "Human beings, who are almost unique in having the ability to learn from the experience of others, are also remarkable for their apparent disinclination to do so."
                };

            System.Threading.Thread.Sleep(random.Next(700));

            return quotes[random.Next(quotes.Length)];
        }
    }
}
