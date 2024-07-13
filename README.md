# GopherToolboxRefresh

## Opis projektu
GopherToolboxRefresh jest aplikacją internetową stowrzoną od Gżdacza dla Gżdaczy.
Umożliwia użytkownikom przeglądanie dostępnych Questów z Gżdacz Roomu, rezerwowanie zadań, zarządzanie nimi oraz przeglądanie ich historii. 
Administrator ma możliwość dodawania, edytowania i usuwania Questów, a także zarządzania podjętymi zadaniammi przez użytkowników i rozpatrywania próśb o anulowanie ich.

Aplikacja stworzona jako multikonwentowe centrum zarządzania Questami przez i w trakcie eventu.

## Autorzy
Autor: Gr4b4r, AcuDuk

## Specyfikacja technologii
- **Backend**: C# .NET 6.0
- **Baza danych**: SQLite
- **IDE**: Visual Studio 2022

## Instrukcje uruchomienia
1. Klonowanie repozytorium:
    ```bash
    git clone https://github.com/Gr4b4r/GopherToolboxRefresh.git
    cd GopherToolboxRefresh
    ```

2. Przygotowanie środowiska:
    ```bash
    dotnet restore
    ```

3. Utworzenie bazy danych i zastosowanie migracji:
    ```bash
    dotnet ef database update
    ```

4. Uruchomienie aplikacji:
    ```bash
    dotnet run
    ```

## Struktura projektu
- **Controllers**: Obsługuje żądania HTTP. Zawiera kontrolery takie jak `AdminController`, `HomeController`, `QuestController`.
- **Models**: Zawiera modele danych. Modele to `User`, `Quest`, `Order`, `CancelRequest`.
- **Views**: Zawiera widoki Razor odpowiedzialne za interfejs użytkownika.
- **wwwroot**: Zawiera statyczne zasoby takie jak CSS, JS, obrazy.
- **appsettings.json**: Plik konfiguracyjny aplikacji zawierający ustawienia, w tym konfigurację bazy danych.

## Modele
### User
- **Opis**: Reprezentuje użytkownika aplikacji.
- **Pola**:
  - **Id**: string
  - **Name**: string, max 100 znaków
  - **Surname**: string, max 100 znaków
  - **Nickname**: string, max 50 znaków
  - **Birthdate**: DateTime
  - **Phone**: string
  - **Email**: string, wymagane

### Quest
- **Opis**: Reprezentuje zadanie (quest) dostępne w aplikacji.
- **Pola**:
  - **Id**: int
  - **Title**: string, max 200 znaków
  - **Description**: string
  - **CurrentOccupiedSlots**: int

### Order
- **Opis**: Reprezentuje zamówienie użytkownika na wykonanie questa.
- **Pola**:
  - **Id**: int
  - **QuestId**: int
  - **UserId**: string
  - **IsCanceled**: bool

### CancelRequest
- **Opis**: Reprezentuje prośbę użytkownika o anulowanie zamówienia.
- **Pola**:
  - **Id**: int
  - **OrderId**: int
  - **RequestDate**: DateTime

## Kontrolery
### AdminController
- **GET UserManagement**: Wyświetla listę użytkowników.
  - **Metoda HTTP**: GET
  - **Parametry**: Brak
  - **Opis**: Wyświetla listę użytkowników w systemie.
  - **Zwracane dane**: Widok `UserManagement`

- **GET EditUser/{id}**: Wyświetla formularz edycji użytkownika.
  - **Metoda HTTP**: GET
  - **Parametry**: `id` (string)
  - **Opis**: Wyświetla formularz edycji użytkownika na podstawie przekazanego `id`.
  - **Zwracane dane**: Widok `EditUser`

- **POST EditUser**: Aktualizuje dane użytkownika.
  - **Metoda HTTP**: POST
  - **Parametry**: `User` (model)
  - **Opis**: Aktualizuje dane użytkownika w bazie danych.
  - **Zwracane dane**: Przekierowanie do `UserManagement`

- **GET CancelRequests**: Wyświetla listę próśb o anulowanie zadań.
  - **Metoda HTTP**: GET
  - **Parametry**: Brak
  - **Opis**: Wyświetla listę wszystkich próśb o anulowanie zadań.
  - **Zwracane dane**: Widok `CancelRequests`

- **POST ApproveCancellation/{id}**: Zatwierdza prośbę o anulowanie zadania.
  - **Metoda HTTP**: POST
  - **Parametry**: `id` (int)
  - **Opis**: Zatwierdza prośbę o anulowanie zadania na podstawie `id`.
  - **Zwracane dane**: Przekierowanie do `CancelRequests`

- **POST DenyCancellation/{id}**: Odrzuca prośbę o anulowanie zadania.
  - **Metoda HTTP**: POST
  - **Parametry**: `id` (int)
  - **Opis**: Odrzuca prośbę o anulowanie zadania na podstawie `id`.
  - **Zwracane dane**: Przekierowanie do `CancelRequests`

- **GET Orders**: Wyświetla listę zamówień.
  - **Metoda HTTP**: GET
  - **Parametry**: Brak
  - **Opis**: Wyświetla listę wszystkich zamówień.
  - **Zwracane dane**: Widok `Orders`

- **GET Create**: Wyświetla formularz tworzenia nowego questa.
  - **Metoda HTTP**: GET
  - **Parametry**: Brak
  - **Opis**: Wyświetla formularz umożliwiający utworzenie nowego questa.
  - **Zwracane dane**: Widok `Create`

- **POST Create**: Tworzy nowego questa.
  - **Metoda HTTP**: POST
  - **Parametry**: `Quest` (model)
  - **Opis**: Tworzy nowego questa w bazie danych.
  - **Zwracane dane**: Przekierowanie do `Index`

- **GET Edit/{id}**: Wyświetla formularz edycji questa.
  - **Metoda HTTP**: GET
  - **Parametry**: `id` (int)
  - **Opis**: Wyświetla formularz edycji questa na podstawie `id`.
  - **Zwracane dane**: Widok `Edit`

- **POST Edit/{id}**: Aktualizuje questa.
  - **Metoda HTTP**: POST
  - **Parametry**: `id` (int), `Quest` (model)
  - **Opis**: Aktualizuje questa w bazie danych.
  - **Zwracane dane**: Przekierowanie do `Index`

- **GET Delete/{id}**: Wyświetla formularz usunięcia questa.
  - **Metoda HTTP**: GET
  - **Parametry**: `id` (int)
  - **Opis**: Wyświetla formularz usunięcia questa na podstawie `id`.
  - **Zwracane dane**: Widok `Delete`

- **POST DeleteConfirmed/{id}**: Usuwa questa.
  - **Metoda HTTP**: POST
  - **Parametry**: `id` (int)
  - **Opis**: Usuwa questa z bazy danych.
  - **Zwracane dane**: Przekierowanie do `Index`

- **GET Details/{id}**: Wyświetla szczegóły questa.
  - **Metoda HTTP**: GET
  - **Parametry**: `id` (int)
  - **Opis**: Wyświetla szczegóły questa na podstawie `id`.
  - **Zwracane dane**: Widok `Details`

## System użytkowników
- **Role**: Admin
- **Konto admina**: login: admin@admin.pl, hasło: Admin123!@#
- **Funkcje admina**: 
  - Zarządzanie użytkownikami i questami.
  - Przeglądanie i rozpatrywanie próśb o anulowanie zadań.
- **Możliwości użytkowników zalogowanych**:
  - Przeglądanie dostępnych questów.
  - Rezerwowanie i zarządzanie questami.
- **Informacje powiązane z użytkownikiem**: Dane osobowe takie jak imię, nazwisko, adres e-mail, numery telefonów oraz informacje o zarezerwowanych questach.

## Najciekawsze funkcjonalności
- **Zarządzanie questami**: Administratorzy mogą dodawać, edytować i usuwać questy.
- **System użytkowników**: Obsługuje rejestrację, logowanie, przydzielanie ról oraz zarządzanie użytkownikami.
- **Zarządzanie zamówieniami**: Obsługuje tworzenie, przeglądanie i zarządzanie zamówieniami na questy.

## Link do repozytorium
[https://github.com/Gr4b4r/GopherToolboxRefresh](https://github.com/Gr4b4r/GopherToolboxRefresh)
"""
