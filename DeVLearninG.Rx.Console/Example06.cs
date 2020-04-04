﻿using System;
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
    public class Example06
    {
        public Example06()
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
        /// Genera eventi con intervallo che si alterna tra 500ms 1000ms ogni 5 eventi
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
