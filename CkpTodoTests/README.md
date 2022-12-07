# Testy automatyczne API

Niniejsze oprogramowanie przeprowadza testy automatyczne na wszystkich endpointach aplikacji głównej, w celu sprawdzenia czy działają tak, jak zostało to zaplanowane.

## Wymagane oprogramowanie

* NodeJS 18.12.1 LTS (lub nowszy)

## Przygotowanie środowiska

```
npm install
```

## Uruchomienie testów

Przed uruchomieniem testów upewnij się, że aplikacja API jest uruchomiona i działa na porcie TCP 5000. Jeżeli działa na innym porcie lub innym hoście niż lokalny, to edytuj plik konfiguracyjny ```.env```.

```
npm test
```