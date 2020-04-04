DeVLearninG - Rx esempi
==================

Esempi relativi alla libreria Rx per integrare i video presenti sul nostro canale YouTube https://www.youtube.com/c/DeVLearninG
il video di riferimento è https://youtu.be/X6_xWpQihFw dove è possibile seguire la parte teorica.

## Sommario
* [Rx.NET](#Rx.NET)
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


## Rx.NET

Descrizione dei vari esempi realizzati tramite la libreria Rx.NET

### Progetto DeVLearninG.Rx.Console

Il progetto DeVLearninG.Rx.Console contiene 10 esempi di utilizzo della libreria


### Esempio01

Creazione observable tramite il factory method Return

```C#
...
Observable.Return<int>(1)
...
```


### Esempio02

Creazione observable tramite il factory method Return

```C#
...
 obsAll.SubscribeOn(NewThreadScheduler.Default)
.Subscribe((x) =>
{
    System.Console.WriteLine("ObsAll OnNext: " + x + " on Thread " + Thread.CurrentThread.ManagedThreadId);
}, (c) =>
{
    System.Console.WriteLine("ObsAll OnCompleted" + " on Thread " + Thread.CurrentThread.ManagedThreadId);
});

obsAll.ObserveOn(NewThreadScheduler.Default)
.Subscribe((x) =>
{
    System.Console.WriteLine("ObsAll OnNext: " + x + " on Thread " + Thread.CurrentThread.ManagedThreadId);
}, (c) =>
{
    System.Console.WriteLine("ObsAll OnCompleted" + " on Thread " + Thread.CurrentThread.ManagedThreadId);
});
...
```


### Esempio03

Esempio di creazione observable tramite il factory method Create
 - utilizzo dell'operatore SubscribeOn
 - utilizzo dell'operatore ObserveOn

```C#
...
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


obs
    .SubscribeOn(NewThreadScheduler.Default)
    .Subscribe((x) =>
    {
        System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
    });


obs
    .ObserveOn(NewThreadScheduler.Default)
    .Subscribe((x) =>
    {
        System.Console.WriteLine("Obs OnNext: " + x.Name.ToString() + " on Thread " + Thread.CurrentThread.ManagedThreadId);
    });
...
```


### Esempio04

Esempio di creazione observable tramite il factory method Create e creazione di un Observer

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


### Esempio05

Esempio di creazione observable tramite il factory method Interval e creazione di un Observer

```C#
...
var obs = Observable.Interval(TimeSpan.FromMilliseconds(500));

obs.Subscribe((x) =>
{
    System.Console.WriteLine($"OnNext: {x}");
});
...
```


### Esempio06

Esempio di creazione observable tramite il factory method Interval e creazione di un Observer

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


### Esempio07

Esempio di creazione observable tramite ToObservable di un'enumerable
 - utilizzo dell'operatore Buffer

```C#
...

...
```


### Esempio08

Esempio di creazione observable tramite ToObservable di un'enumerable
 - utilizzo dell'operatore Buffer

```C#
...
var myInbox = FakeEmailGeneration().ToObservable();

var fakeEmailsObs = myInbox.Buffer(TimeSpan.FromSeconds(3));

fakeEmailsObs.Subscribe(emails =>
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

Esempio di creazione Observable tramite factory method Range
 - utilizzo dell'operatore Publish

```C#
...
var shared = unshared.Publish();

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

var obs = obsCreated
    .Merge(obsChanged)
    .Merge(obsDeleted)
    .TimeInterval()
    .Scan((state, item) => state == null || item.Interval - state.Interval > TimeSpan.FromMilliseconds(1) || (state.Value.FullPath != item.Value.FullPath || state.Value.ChangeType != state.Value.ChangeType) ? item : state)
    .DistinctUntilChanged()
    .Select(x => x.Value)
    .Select(x => new FileWatcherRaisedEvent { Filename = x.FullPath, ChangeType = x.ChangeType });
...
```