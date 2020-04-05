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
    /// Esempio di creazione Observable tramite factory method Range
    ///  - utilizzo dell'operatore Publish
    /// </summary>
    public class Example09
    {
        public Example09()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var obs = Observable.Range(1, 4);

            obs.Subscribe(i => System.Console.WriteLine("Sottoscrizione non condivisa #1: " + i));
            obs.Subscribe(i => System.Console.WriteLine("Sottoscrizione non condivisa #2: " + i));

            System.Console.WriteLine();

            var shared = obs.Publish();
            
            shared.Subscribe(i =>
            {
                System.Console.WriteLine("Sottoscrizione condivisa #1: " + i);
            });

            shared.Subscribe(i =>
            {
                System.Console.WriteLine("Sottoscrizione condivisa #2: " + i);
            });

            shared.Connect();

            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }
}
