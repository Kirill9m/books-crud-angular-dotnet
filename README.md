# Books & Quotes CRUD Apps
Responsive CRUD application built with Angular 20 and .NET 9 Web API.
Includes JWT authentication, Books management and "My Quotes" feature.

## Tech Stack
Frontend: Angular 20, Bootstrap, Font Awesome  
Backend: .NET 9 Web API  
Auth: JWT
DB ?

## Features

### Authentication
- Register
- Login
- JWT token validation

### Books CRUD
- View books list
- Add new book
- Edit book
- Delete book

### My Quotes CRUD
- View personal quotes
- Add quote
- Edit quote
- Delete quote

## Responsive Design
- Mobile friendly layout
- Responsive navigation menu
- Works on desktop / tablet / mobile

## Run Locally

### Backend
cd backend
dotnet run

### Frontend
cd frontend/client
npm install
npm start

## Assignment Requirements
Responsiv CRUD-applikation med tokenhantering och byggt i Angular 20 och .NET 9 C# API

Du har två veckor på dig att slutföra denna uppgift gör. Målet är att skapa en responsiv CRUD-webbapplikation med Angular 20 för front-end och .NET 9 C# för back-end API. Dessutom måste du implementera tokenhantering för användarverifiering, använda Bootstrap och Font Awesome-ikoner för styling och lägga till en "Mina citat"-sida där användare kan lägga till och se sina favoritcitat.

Krav:

Implementera en webbapplikation med en sida som visar en lista över alla böcker.
Skapa en startsida med en knapp för att lägga till en ny bok.
Om du klickar på knappen "Lägg till ny bok" bör användaren omdirigeras till ett formulär där de kan ange information om en ny bok (t.ex. titel, författare, publiceringsdatum).
Efter att ha skickat in formuläret ska användaren omdirigeras tillbaka till startsidan, där de kan se den nya boken som lagts till i listan.
Varje bok i listan bör ha en "Redigera"-knapp som tar användaren till ett formulär där de kan redigera detaljerna i boken.
Efter att ha skickat in formuläret ska användaren omdirigeras tillbaka till startsidan, där de kan se de uppdaterade bokdetaljerna i listan.
Varje bok i listan bör ha en "Radera"-knapp som låter användaren ta bort boken.
Efter att ha tagit bort en bok bör användaren se boken borttagen från listan.

Tokenhantering:

Implementera användarautentisering med JWT (JSON Web Tokens).
Skapa en enkel inloggningssida där användare kan ange sina referenser (t.ex. användarnamn och lösenord).
Man ska kunna registrera en ny användare och sen använda den för att loga in. 
Efter lyckad inloggning bör back-end generera en token och skicka tillbaka den till front-end.
Front-end bör lagra token säkert (t.ex. i lokal lagring eller en cookie) och använda den för efterföljande API-förfrågningar till back-end.
Implementera token-validering på back-end för att säkerställa att endast autentiserade användare kan komma åt CRUD-operationerna.

Mina citatsida:

Skapa en separat vy som heter "Mina citat".
Visa listan på 5 citat du gillar.
man ska kunna lägga till, ta bort och editera citaten 
Det ska finna en meny så att man kan gå mellan bok vyn och citat vyn

Responsiv designtestning:

Se till att applikationens layout och komponenter smidigt anpassar sig till olika skärmstorlekar, inklusive stationära datorer, surfplattor och mobila enheter.
Testa applikationens genom att ändra storlek på webbläsarfönstret och verifiera att alla element justeras på rätt sätt.
Kontrollera att navigeringsmenyer kollapsar till en responsiv mobilmeny på mindre skärmar.
Verifiera att formulärfält, knappar och andra UI-element bibehåller korrekt avstånd och justering över olika visningsportar.
Testa applikationen på olika enheter (t.ex. smartphones, surfplattor) och webbläsare för att säkerställa konsekvent beteende.

Bootstrap och Font Awesome:

Använd Bootstrap för att skapa en lyhörd och visuellt tilltalande layout för applikationen.
Använd Bootstrap-klasser för att utforma knappar, formulär och andra UI-komponenter.
Inkludera Font Awesome-ikoner för att förbättra programmets visuella element.
Verifiera att Font Awesome-ikonerna visas korrekt och används korrekt i hela programmet.

Ytterligare utmaning:

Implementera en knapp som gör att användaren kan växla mellan ljusa och mörka UX-design för applikationen.

Inlämning
Lägg upp applikationen på en gratis hostingtjänst (t.ex. Netlify, Vercel eller Azure Static Web Apps).
