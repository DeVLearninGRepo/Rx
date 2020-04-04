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
    /// Esempio di creazione observable tramite il factory method Create
    ///  - utilizzo dell'operatore SubscribeOn ed ObserveOn
    /// </summary>
    public class Example03
    {
        public Example03()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            System.Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId);

            var obs = Observable.Create<Example3Result>((x) =>
            {
                System.Console.WriteLine("Start Thread " + Thread.CurrentThread.ManagedThreadId);


                x.OnNext(new Example3Result { Name = "Obj1" });
                x.OnNext(new Example3Result { Name = "Obj2" });

                Thread.Sleep(1000);

                x.OnNext(new Example3Result { Name = "Obj3" });
                x.OnNext(new Example3Result { Name = "Obj4" });

                System.Console.WriteLine("Finish Thread " + Thread.CurrentThread.ManagedThreadId);
                return Disposable.Empty;
            });


            //obs.SubscribeOn(NewThreadScheduler.Default)
            //    .Subscribe((x) =>
            //    {
            //        System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
            //    });


            obs.ObserveOn(NewThreadScheduler.Default)
               .Subscribe((x) =>
               {
                   System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
               });



            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }

    public class Example3Result
    {
        public string Name { get; set; }
    }
}
