﻿using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace DeVLearninG.Rx.Console
{
    /// <summary>
    /// Creazione observable a partire da un IEnumerable tramite il method ToObservable
    ///   - utilizzo dell'operatore Where
    ///   - utilizzo dell'operatore Select
    /// </summary>
    public class Example02
    {
        private Random _random;

        public Example02()
        {
            _random = new Random();
        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            var obs1 = Generate().ToObservable();

            var obs = obs1
                .Where(x => x.Id % 2 == 0)
                .Select(x => x.Name);

            obs
                .Subscribe((x) =>
                {
                    System.Console.WriteLine("ObsAll OnNext: " + x + " on Thread " + Thread.CurrentThread.ManagedThreadId);
                }, (c) =>
                {
                    System.Console.WriteLine("ObsAll OnCompleted" + " on Thread " + Thread.CurrentThread.ManagedThreadId);
                });

            Utils.PrintColoredMessage(GetType().Name + " End");
        }

        public IEnumerable<Example02Event> Generate()
        {            
            int numberOfEvents = _random.Next(10, 21);
            for (int i = 0; i <= numberOfEvents; i++)
            {
                yield return new Example02Event(_random);
            }
        }
    }

    public class Example02Event
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Example02Event(Random random)
        {
            Id = random.Next(0, 1000);
            Name = $"Event {Id}";
        }
    }
}
