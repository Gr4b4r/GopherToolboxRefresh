   ﻿# GopherToolboxRefresh

## Opis
GopherToolboxRefresh jest aplikacją internetową stowrzoną od Gżdacza dla Gżdaczy.
Umożliwia użytkownikom przeglądanie dostępnych Questów z Gżdacz Roomu, rezerwowanie zadań, zarządzanie nimi oraz przeglądanie ich historii. 
Administrator ma możliwość dodawania, edytowania i usuwania Questów, a także zarządzania podjętymi zadaniammi przez użytkowników i rozpatrywania próśb o anulowanie ich.

Aplikacja stworzona jako multikonwentowe centrum zarządzania Questami przez i w trakcie eventu.


##Struktura projektu

- Controllers - Zawiera kontrolery obsługujące żądania HTTPS
- Models - Zawiera modele danych
- Views - Zawiera widoki (pliki Razor)
- wwwroot - Zawiera statyczne zasoby, takie jak pliki CSS, JS, obrazy
- appsettings.json - Plik konfiguracyjny aplikacji

**Użytkowanie**

-Rejestracja-
Przejdź do /Account/Register
Wypełnij formularz rejestracyjny i wyślij

-Logowanie- 
Przejdź do /Account/Login
Wypełnij formularz logowania i wyślij

## Konto Administracyjne
- login: admin@tgies.pl
- hasło: H4$l0d0K0nt4A4m1n4
**Ważne**
- Aplikacja będzie dostępna pod adresem 'http://localhost:7064'
## Wymagania systemowe
- .NET 6.0 SDK lub nowszy
- Entity Framework Core
- Baza danych SQLite
- Visual Studio 2022 lub nowszy (opcjonalnie)

## Instalacja

1. **Klonowanie repozytorium**
   ```bash
   git clone https://github.com/Gr4b4r/GopherToolbox.git
   cd projekt
   
2. **Przygotowanie środowiska**
   ```bash
   dotnet restore
          
3. **Utwórz bazę danych i zastosuj migracje**
   ```bash
   dotnet ef database update
  
 4. **Uruchomienie aplikacji**
   ```bash
   dotnet run
