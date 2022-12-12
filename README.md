# Czysty kod - programowanie - projekt zaliczeniowy

## Informatyka, niestacjonarne, semestr 3, rok akademicki 2022 / 2023

Aplikacja jako projekt zaliczeniowy do przedmiotu "Czysty kod - programowanie".

## Opis aplikacji

Program przeznaczony do zapisywania krókich zadań oraz informacji o zaplanowanych wydarzeniach. Posiada kontrolę kont użytkowników oraz system logowania do aplikacji.

## Skład grupy projektowej

* Artur Leśnik
* Weronika Ładak
* Paweł Piątek
* Tomasz Janus
* Karol Pilot

## Struktura projektu

* CkpTodoApp - kod źródłowy modułu backend aplikacji
* CkpTodoFrontend - kod źródłowy modułu frontend aplikacji
* CkpTodoTests - automatyczne testy endpointów api

## Uruchomienie lokalne frontendu

Wymagane oprogramowanie do uruchomienia:

  * NodeJS 18.12.1 LTS (lub nowszy)

Kompilacja ze źródła:

```
git submodule update --remote --recursive
cd CkpTodoFrontend
npm install
npm run build
npm install serve
npx serve -s build
```

Adres lokalny do frontendu:

```
http://localhost:3000
```

Domyślnie aplikacja komunikuje się z API na porcie 5039, jeżeli chcesz to zmienić, to przed kompilacją edytuj plik ```CkpTodoFrontend\src\api\consts\baseUrl.ts```.