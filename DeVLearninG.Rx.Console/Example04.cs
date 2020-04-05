using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace DeVLearninG.Rx.Console
{
    /// <summary>
    /// Creazione observable tramite il factory method Create
    ///  - utilizzo dell'operatore ObserveOn
    /// </summary>
    public class Example04
    {
        public Example04()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            System.Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId);

            var obs = Observable.Create<Example4Result>((x) =>
            {
                System.Console.WriteLine("Start Thread " + Thread.CurrentThread.ManagedThreadId);

                x.OnNext(new Example4Result { Name = "Obj1" });
                x.OnNext(new Example4Result { Name = "Obj2" });
                x.OnNext(new Example4Result { Name = "Obj3" });
                x.OnNext(new Example4Result { Name = "Obj4" });

                System.Console.WriteLine("Finish Thread " + Thread.CurrentThread.ManagedThreadId);
                return Disposable.Empty;
            });

            obs
                .ObserveOn(NewThreadScheduler.Default)
                .Subscribe((x) =>
                {
                    System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
                });

            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }

    public class Example4Result
    {
        public string Name { get; set; }
    }
}
