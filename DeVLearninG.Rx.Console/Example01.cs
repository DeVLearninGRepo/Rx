using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace DeVLearninG.Rx.Console
{
    /// <summary>
    /// Esempio di creazione observable tramite il factory method Return
    /// </summary>
    public class Example01
    {

        public Example01()
        {

        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var obs1 = Observable.Return<int>(1);
            var obs2 = Observable.Return<int>(2);
            var obs3 = Observable.Return<int>(3);
            var obs4 = Observable.Return<int>(4);
            var obs5 = Observable.Return<int>(5);



            var obsAll = obs1
                .Merge(obs2)
                .Merge(obs2)
                .Merge(obs3)
                .Merge(obs4)
                .Merge(obs5);

            System.Console.WriteLine("ObsAll Subscribe");
            obsAll.Subscribe((x) =>
            {
                System.Console.WriteLine("ObsAll OnNext: " + x + " on Thread " + Thread.CurrentThread.ManagedThreadId);
            }, () =>
            {
                System.Console.WriteLine("ObsAll OnCompleted" + " on Thread " + Thread.CurrentThread.ManagedThreadId);
            });


            var obsDistinct = obsAll.Distinct();

            System.Console.WriteLine("ObsDistinct Subscribe");
            obsDistinct.Subscribe((x) =>
            {
                System.Console.WriteLine("ObsDistinct OnNext: " + x + " on Thread " + Thread.CurrentThread.ManagedThreadId);
            }, () =>
            {
                System.Console.WriteLine("ObsDistinct OnCompleted" + " on Thread " + Thread.CurrentThread.ManagedThreadId);
            });



            Utils.PrintColoredMessage(GetType().Name + " End");
        }
    }
}
