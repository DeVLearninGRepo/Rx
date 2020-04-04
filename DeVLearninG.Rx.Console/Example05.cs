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
    public class Example05
    {
        public Example05()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");



            var obs = Observable.Create<Example5Result>((x) =>
            {
                x.OnNext(new Example5Result { Name = "Obj1" });
                x.OnNext(new Example5Result { Name = "Obj2" });
                x.OnNext(new Example5Result { Name = "Obj3" });
                x.OnNext(new Example5Result { Name = "Obj4" });

                x.OnCompleted();

                return Disposable.Empty;
            });

            Observer5 observer = new Observer5();
            obs.Subscribe(observer);



            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }

    public class Observer5 : IObserver<Example5Result>
    {
        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            System.Console.WriteLine("Error " + error.Message);
        }

        public void OnNext(Example5Result value)
        {
            System.Console.WriteLine("Obs OnNext: " + value.Name + " on Thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class Example5Result
    {
        public string Name { get; set; }
    }
}
