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
    /// Esempio di creazione observable tramite il factory method Create e creazione di un Observer
    /// </summary>
    public class Example04
    {
        public Example04()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var obs = Observable.Create<Example4Result>((x) =>
            {
                x.OnNext(new Example4Result { Name = "Obj1" });
                x.OnNext(new Example4Result { Name = "Obj2" });
                x.OnNext(new Example4Result { Name = "Obj3" });
                x.OnNext(new Example4Result { Name = "Obj4" });

                x.OnCompleted();

                return Disposable.Empty;
            });

            Observer4 observer4 = new Observer4();
            obs.Subscribe(observer4);


            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }

    public class Observer4 : IObserver<Example4Result>
    {
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            System.Console.WriteLine("Error " + error.Message);
        }

        public void OnNext(Example4Result value)
        {
            System.Console.WriteLine("Obs OnNext: " + value.Name + " on Thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class Example4Result
    {
        public string Name { get; set; }
    }
}
