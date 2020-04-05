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
    /// Creazione observable tramite ToObservable di un'enumerable
    ///  - utilizzo dell'operatore Throttle
    /// </summary>
    public class Example07
    {
        public Example07()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var obs = GenerateEvents().ToObservable();

            obs.Throttle(TimeSpan.FromMilliseconds(750))
                .Subscribe((x) =>
                {
                    System.Console.WriteLine($"OnNext: {x}");
                });

            Utils.PrintColoredMessage(GetType().Name + " End");
        }

        /// <summary>
        /// Genera eventi in modo tale che 5 siano ad intervalli di 500ms e 5 con intervallo di 1000ms
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GenerateEvents()
        {
            int i = 0;

            while (true)
            {
                if (i > 1000)
                {
                    yield break;
                }

                yield return i;

                Thread.Sleep(i++ % 10 < 5 ? 500 : 1000);
            }
        }
    }
}
