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
    public class Example09
    {
        public Example09()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var unshared = Observable.Range(1, 4);

            // Each subscription starts a new sequence
            unshared.Subscribe(i => System.Console.WriteLine("Unshared Subscription #1: " + i));
            unshared.Subscribe(i => System.Console.WriteLine("Unshared Subscription #2: " + i));

            System.Console.WriteLine();

            // By using publish the subscriptions are shared, but the sequence doesn't start until Connect() is called.
            var shared = unshared.Publish();
            shared.Subscribe(i =>
            {
                System.Console.WriteLine("Shared Subscription #1: " + i);
            });

            shared.Subscribe(i =>
            {
                System.Console.WriteLine("Shared Subscription #2: " + i);
            });

            shared.Connect();


            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }
}
