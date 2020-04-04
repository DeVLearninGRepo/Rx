using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeVLearninG.Rx.Console
{
    /// <summary>
    /// Esempio di un'observable per la gestione degli eventi generati dal FileSystemWatcher
    ///  - creazione Observable tramite event pattern
    ///  - concatenzione degli eventi Created, Changed, Deleted
    ///  - filtro per evitare duplicazioni di eventi
    ///  - proiezione del risultato
    /// </summary>
    public class Example10
    {
        private const string FSW_DIRECTORY = ".\\FSW";
        private FileSystemWatcher _fsw;

        public Example10()
        {
            _fsw = new FileSystemWatcher();

            FileSystemWatcherConfigure();
        }

        public void Start()
        {
            Utils.PrintColoredMessage(GetType().Name + " Start");

            

            var obsCreated = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => _fsw.Created += x,
                x => _fsw.Created -= x)
                .Select(x => x.EventArgs);

            var obsChanged = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => _fsw.Changed += x,
                x => _fsw.Changed -= x)
                .Select(x => x.EventArgs);

            var obsDeleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
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

            obs
               .SubscribeOn(NewThreadScheduler.Default)
               .Subscribe(x =>
           {
               System.Console.WriteLine("  " + x.ChangeType.ToString() + " " + x.Filename);
           });

            
            
            Utils.PrintColoredMessage(GetType().Name + " End");
        }

        private void FileSystemWatcherConfigure()
        {
            Directory.CreateDirectory(FSW_DIRECTORY);

            _fsw.Path = FSW_DIRECTORY;
            _fsw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _fsw.IncludeSubdirectories = false;
            _fsw.EnableRaisingEvents = true;
        }
    }

    public class FileWatcherRaisedEvent
    {
        public string Filename { get; set; }
        public WatcherChangeTypes ChangeType { get; set; }
    }

    public class FileWatcherRaisedEventEqualityComparer : IEqualityComparer<FileWatcherRaisedEvent>
    {
        public bool Equals(FileWatcherRaisedEvent x, FileWatcherRaisedEvent y)
        {
            return x.Filename == y.Filename && x.ChangeType == y.ChangeType;
        }

        public int GetHashCode(FileWatcherRaisedEvent obj)
        {
            return obj.Filename.GetHashCode() ^ obj.ChangeType.GetHashCode();
        }
    }
}
