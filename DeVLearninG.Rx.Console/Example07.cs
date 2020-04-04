using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeVLearninG.Rx.Console
{
    /// <summary>
    /// Esempio di creazione observable tramite ToObservable di un'enumerable
    ///  - utilizzo dell'operatore Buffer
    /// </summary>
    public class Example07
    {
        public Example07()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var myInbox = FakeEmailGeneration().ToObservable();

            var fakeEmailsObs = myInbox.Buffer(TimeSpan.FromSeconds(3));

            fakeEmailsObs.Subscribe(emails =>
            {
                System.Console.WriteLine($"Hai ricevuto {emails.Count} messaggi");
                foreach (var email in emails)
                {
                    System.Console.WriteLine(" - {0}", email);
                }
                System.Console.WriteLine();
            });


            Utils.PrintColoredMessage(GetType().Name + " End");
        }

        private IEnumerable<string> FakeEmailGeneration()
        {
            var random = new Random();
            var colours = new List<String> { "Here is an email!", "Another email!", "Yet another email!" };

            for (; ; )
            {
                yield return colours[random.Next(colours.Count)];
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}
