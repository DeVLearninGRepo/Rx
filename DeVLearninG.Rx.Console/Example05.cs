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
    /// Esempio di creazione observable tramite il factory method Interval e creazione di un Observer
    /// </summary>
    public class Example05
    {
        public Example05()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");



            var obs = Observable.Interval(TimeSpan.FromMilliseconds(500));

            Observer4 observer4 = new Observer4();
            obs.Subscribe((x) =>
            {
                System.Console.WriteLine($"OnNext: {x}");
            });

            
            
            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }
}
