DeVLearninG - Rx.NET - esempi
==================

Esempi relativi alla libreria Rx per integrare i video presenti sul nostro canale YouTube https://www.youtube.com/c/DeVLearninG
i video di riferimento sono:
 - https://youtu.be/X6_xWpQihFw - parte teorica
 - https://youtu.be/nY5ZyD15x4g - parte pratica in C# .NET

Ricordatevi di iscrivervi al nostro canale YouTube per essere sempre aggiornati sui nuovi video.

Seguiteci anche su Facebook dove è possibile condividere ognuno le proprie conoscenze:
 - Gruppo  https://www.facebook.com/groups/215017629842040 
 - Pagina  https://www.facebook.com/devlearning2020

## Sommario
* [Progetto DeVLearninG.Rx.Console](#Progetto-DeVLearninG.Rx.Console)
    * [Esempio01](#Esempio01)
    * [Esempio02](#Esempio02)
    * [Esempio03](#Esempio03)
    * [Esempio04](#Esempio04)
    * [Esempio05](#Esempio05)
    * [Esempio06](#Esempio06)
    * [Esempio07](#Esempio07)
    * [Esempio08](#Esempio08)
    * [Esempio09](#Esempio09)
    * [Esempio10](#Esempio10)


## Progetto DeVLearninG.Rx.Console

Descrizione dei vari esempi realizzati tramite la libreria Rx.NET

Il progetto DeVLearninG.Rx.Console contiene 10 esempi di utilizzo della libreria ReactiveX, è possibile eseguirli decommentando l'esempio prescelto dalla classe Program.

### Esempio01

Creazione observable tramite il factory method Return
 - utilizzo dell'operatore Merge
 - utilizzo dell'operatore Distinct

```C#
...
var obs1 = Observable.Return<int>(1);
var obs2 = Observable.Return<int>(2);
var obs3 = Observable.Return<int>(3);
var obs4 = Observable.Return<int>(4);
var obs5 = Observable.Return<int>(5);

var obsAll = obs1
    .Merge(obs2)
    .Merge(obs2)
    .Merge(obs3)
    .Merge(obs3)
    .Merge(obs3)
    .Merge(obs4)
    .Merge(obs5);

var obsDistinct = obsAll.Distinct();
...
```


### Esempio02

Creazione observable a partire da un IEnumerable tramite il method ToObservable
 - utilizzo dell'operatore Select
 - utilizzo dell'operatore Where

```C#
...
var obs1 = Generate().ToObservable();

var obs = obs1
    .Where(x => x.Id % 2 == 0)
    .Select(x => x.Name);
...
```


### Esempio03

Creazione observable tramite il factory method Create
 - utilizzo dell'operatore SubscribeOn

```C#
...
obs
    .SubscribeOn(NewThreadScheduler.Default)
    .Subscribe((x) =>
    {
        System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
    });
...
```


### Esempio04

Creazione observable tramite il factory method Create
 - utilizzo dell'operatore ObserveOn

```C#
...
obs
    .ObserveOn(NewThreadScheduler.Default)
    .Subscribe((x) =>
    {
        System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
    });
...
```


### Esempio05

Creazione observable tramite il factory method Create e creazione di un Observer

```C#
...
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
...
```


### Esempio06

Creazione observable tramite il factory method Interval

```C#
...
var obs = Observable.Interval(TimeSpan.FromMilliseconds(500));

obs.Subscribe((x) =>
{
    System.Console.WriteLine($"OnNext: {x}");
});
...
```


### Esempio07

Creazione observable a partire da un IEnumerable tramite il method ToObservable
 - utilizzo dell'operatore Throttle

```C#
...
var obs = GenerateEvents().ToObservable();

obs
    .Throttle(TimeSpan.FromMilliseconds(750))
    .Subscribe((x) =>
    {
        System.Console.WriteLine($"OnNext: {x}");
    });
...
```


### Esempio08

Creazione observable a partire da un IEnumerable tramite il method ToObservable
 - utilizzo dell'operatore Buffer

```C#
...
var myInbox = FakeEmailGeneration().ToObservable();

myInbox
    .Buffer(TimeSpan.FromSeconds(3))
    .Subscribe(emails =>
    {
        System.Console.WriteLine($"Hai ricevuto {emails.Count} messaggi");
        foreach (var email in emails)
        {
            System.Console.WriteLine(" - {0}", email);
        }
        System.Console.WriteLine();
    });
...
```


### Esempio09

Creazione Observable tramite factory method Range
 - utilizzo dell'operatore Publish

```C#
...
var obs = Observable.Range(1, 4);

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
...
```


### Esempio10

Esempio di un'observable per la gestione degli eventi generati dal FileSystemWatcher
 - creazione Observable tramite event pattern
 - concatenzione degli eventi Created, Changed, Deleted
 - filtro per evitare duplicazioni di eventi
 - proiezione del risultato

```C#
...
var obsCreated = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
        x => _fsw.Created += x,
        x => _fsw.Created -= x)
    .Select(x => x.EventArgs);

var obsChanged = Observable
    .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
        x => _fsw.Changed += x,
        x => _fsw.Changed -= x)
    .Select(x => x.EventArgs);

var obsDeleted = Observable
    .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
        x => _fsw.Deleted += x,
        x => _fsw.Deleted -= x)
    .Select(x => x.EventArgs);

var obs = obsCreate
    .Merge(obsChanged)
    .Merge(obsDeleted)
    .TimeInterval()
    .Scan((state, item) => state == null
            || item.Interval - state.Interval > TimeSpan.FromMilliseconds(1)
            || state.Value.FullPath != item.Value.FullPath
            || !(state.Value.ChangeType == WatcherChangeTypes.Created && item.Value.ChangeType == WatcherChangeTypes.Changed) ? item : state)
    .Select(x => x.Value)
    .Select(x => new FileWatcherRaisedEvent { Filename = x.FullPath, ChangeType = x.ChangeType })
    .DistinctUntilChanged(new FileWatcherRaisedEventEqualityComparer());
...
```
