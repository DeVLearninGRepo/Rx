DeVLearninG - Angular - RxJs - esempi
==================

Esempi relativi alla libreria Rx per integrare i video presenti sul nostro canale YouTube https://www.youtube.com/c/DeVLearninG
i video di riferimento sono:
 - https://youtu.be/X6_xWpQihFw - parte teorica
 - https://youtu.be/___________ - parte pratica in Angular RxJs

Ricordatevi di iscrivervi al nostro canale YouTube per essere sempre aggiornati sui nuovi video.

Seguiteci anche su Facebook dove è possibile condividere ognuno le proprie conoscenze:
 - Gruppo  https://www.facebook.com/groups/215017629842040 
 - Pagina  https://www.facebook.com/devlearning2020

## Sommario
* [Progetto DeVLearninG-Rx-Angular-App](#Progetto-DeVLearninG--Rx--Angular--App)
    * [Esempio interval](#Esempio-interval)
    * [Esempio timer](#Esempio-timer)
    * [Esempio of](#Esempio-of)
    * [Esempio pipe](#Esempio-pipe)
    * [Esempio map](#Esempio-map)
    * [Esempio tap](#Esempio-tap)
    * [Esempio filter](#Esempio-filter)
    * [Esempio retry e catchError](#Esempio-retry-e-catchError)
    * [Esempio retryWhen](#Esempio-retryWhen)
    * [Esempio BehaviorSubject](#Esempio-BehaviorSubject)


## Progetto DeVLearninG-Rx-Angular-App

Descrizione dei vari esempi realizzati tramite Angular e RxJs

Il progetto DeVLearninG-Rx-Angular-App contiene 

### Esempio interval

Creazione di un observable tramite l'operatore interval. Generazione di eventi ogni x millisecondi
 - Print su console ogni 100 millisecondi
 - Unsubscribe dopo 2 secondi

```Js
import { interval } from 'rxjs';
...
var interval$ = interval(100);

var intervalSubscription = interval$.subscribe((x) => {
    console.debug(x);
});

setTimeout(() => {
   intervalSubscription.unsubscribe();
}, 2000);
```

### Esempio timer

Creazione di un observable tramite l'operatore timer. Generazione di eventi ogni x millisecondi a partire da un tempo n
 - Dopo 3 secondi inizio degli eventi
 - Print su console ogni secondo
 - Unsubscribe dopo 8 secondi

```Js
import { timer } from 'rxjs';
...
var timer$ = timer(3000, 1000);

var timerSubscription = timer$.subscribe((x) => {
    console.debug(x);
});

setTimeout(() => {
   timerSubscription.unsubscribe();
}, 8000);
```

### Esempio of 

Creazione di un observable tramite l'operatore of

```Js
import { of } from 'rxjs';
...
var obs$ = of(1, 2, 3, 4, 5);

obs$.subscribe((x) => {
    console.debug(x);
});
```

### Esempio pipe 

Utilizzo dell'operatore pipe per concatenare gli operatori

```Js
import { of } from 'rxjs';
...
var obs$ = of(1, 2, 3, 4, 5);

obs$.pipe(
        operatore1(),
        operatore2(),
        operatore3(),
        operatore4(),
    )
    .subscribe((x) => {
        console.debug(x);
    });
```

### Esempio map 

Utilizzo dell'operatore map per trasformare gli elementi
 - somma 1 ad ogni elemento

```Js
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
...
var obs$ = of(1, 2, 3, 4, 5);

obs$.pipe(
        map(x => x + 1)
    )
    .subscribe((x) => {
        console.debug(x);
    });
```

### Esempio tap 

Utilizzo dell'operatore tap, viene invocato ad ogni elemento dell'observale e restituisce la sequenza inalterata utile per debug

```Js
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
...
var obs$ = of(1, 2, 3, 4, 5);

obs$.pipe(
        tap(x => console.debug(x)),
        map(x => x + 1)
    )
    .subscribe((x) => {
        console.debug(x);
    });
```

### Esempio filter 

Utilizzo dell'operatore filter, utile per filtrare la sequenza. Ogni elemento deve rispettare la condizione

```Js
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
...
var obs$ = of(1, 2, 3, 4, 5);

obs$.pipe(
        filter(x => x % 2 === 0),
        tap(x => console.debug(x)),
        map(x => x + 1)
    )
    .subscribe((x) => {
        console.debug(x);
    });
```


### Esempio retry e catchError

Utilizzo degli operatori:
 - retry per rieseguire la chiamata in caso di errore per il numero di volte spcificato
 - catchError per gestire l'errore

Nota: non usare mai nel caso di validazioni di credenziali, rischio blocco dell'utenza ;)

```Js
import { of } from 'rxjs';
import { tap, catchError, retry, map } from 'rxjs/operators';
...
this.videoObs$ = this.http.get<Video[]>(`${environment.apiBaseUrl}videos?channelId=1`)
    .pipe(
        retry(3),
        tap(x => console.debug(`VideoCollection length ${x.length}`)),
        map(res => {
            if (!res) {
                throw new Error('Value expected!');
            }
            return res;
        }),
        catchError(e => { console.error(e); return of([])
    })
);
```


### Esempio retryWhen

Utilizzo dell operatore:
 - retryWhen per rieseguire la chiamata in caso di errore e poter attendere un certo periodo di tempo prima del tentativo successivo

in questo caso viene rieseguita la chiamata in caso di errore dopo 2 secondi per un massimo di 3 tentativi

Nota: non usare mai nel caso di validazioni di credenziali, rischio blocco dell'utenza ;)

```Js
import { tap, catchError, retryWhen, delay, take }  from 'rxjs/operators';
this.videoObs$ = this.http.get<Video[]>(`${environment.apiBaseUrl}videos?channelId=1`).pipe(
    tap(x => console.debug(`VideoCollection length ${x.length}`)),
    retryWhen(errors => errors.pipe(delay(2000), take(3))),
)
```


### Esempio BehaviorSubject

Utilizzo del BehaviorSubject

Service
```Js
...
private channelSubject: BehaviorSubject<Channel>;

constructor() {
    this.channelSubject = new BehaviorSubject<Channel>(null);
}

public setCurrentChannel(channel: Channel) {
    ...
    this.channelSubject.next(channel);
}
...
```

Component1 - genera un'evento e quindi una chiamata al metodo next del BehaviorSubject presente nel Service
```Js
...
public selectChannelCommand(channel: Channel) {    
    this.channelService.setCurrentChannel(channel);
}
...
```

Component2 - sottoscrizione al BehaviorSubject per la cattura degli eventi, trasformazione del BehaviorSubject in observable per applicare operatori e reperire informazioni da un'api
```Js
...
constructor(private channelService: ChannelService) { }
...
this.channelService.ChannelSubject.subscribe(x => this.currentChannel = x);

var obs = this.channelService.ChannelSubject.asObservable();

obs
    .pipe(
        filter(x => x != null),
        switchMap(x => this.apiService.getVideos(x.id)),
    ).subscribe(x => this.videos = x);
...
```

