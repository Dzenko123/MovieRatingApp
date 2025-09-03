\# **MovieRatingApp**

MovieRatingApp je jednostavan sistem za ocjenjivanje filmova i TV serija, razvijen kao lični projekat. Aplikacija omogućava korisnicima da anonimno ocjenjuju filmove i serije od 1 do 5 zvjezdica, te pregledaju Top 10 stavki prema prosječnoj ocjeni. Cilj projekta je demonstrirati vještine u full-stack razvoju, rukovanju podacima, API-jevim interakcijama i osnovnom UI/UX dizajnu.

Svaka stavka (film ili TV serija) sadrži sljedeće informacije:

\- Slika (cover image)

\- Naslov

\- Godina izlaska

\- Kratki opis

\- Glumce (najmanje dva)

\- Prosječnu ocjenu

![image alt](https://github.com/Dzenko123/MovieRatingApp/blob/1563cda2602e82fc60079a3253e825f5661e0a74/assets/movies.png)

![image alt](https://github.com/Dzenko123/MovieRatingApp/blob/1563cda2602e82fc60079a3253e825f5661e0a74/assets/series.png)
---

\## **Funkcionalnosti**



\### *Frontend*

\- \*\*Početna stranica:\*\* Prikazuje Top 10 filmova ili serija prema prosječnoj ocjeni.

\- \*\*Toggle View:\*\* Omogućava prebacivanje između Top 10 filmova i Top 10 TV serija.

\- \*\*Pretraga:\*\*  

&nbsp; - Automatski aktivira pretragu nakon unosa najmanje 2 karaktera  

&nbsp; - Pretražuje po naslovu, opisu i glumcima 

&nbsp; - Podržava napredne upite poput: "5 stars", "at least 3 stars", "after 2015", "older than 5 years"  

&nbsp; - Rezultati su uvijek sortirani po prosječnoj ocjeni  

&nbsp; - Brisanjem polja za pretragu vraća se default Top 10 prikaz

\- \*\*Pagination / Load More:\*\* Dugme za učitavanje sljedećih 10 stavki.

\- \*\*Ocjenjivanje:\*\* Jednostavan UI za dodavanje zvjezdica (1-5), bez tekstualnih recenzija.

\- \*\*Prikaz prosječne ocjene:\*\* Svaki film ili serija prikazuje svoju prosječnu ocjenu.

![image alt](https://github.com/Dzenko123/MovieRatingApp/blob/1563cda2602e82fc60079a3253e825f5661e0a74/assets/5_stars.png)

![image alt](https://github.com/Dzenko123/MovieRatingApp/blob/1563cda2602e82fc60079a3253e825f5661e0a74/assets/older_than_20_years.png)

\### *Backend*

\- \*\*RESTful API\*\* za podršku frontend funkcionalnostima.

\- \*\*JWT zaštita API-ja\*\*, osiguravajući da samo autorizovani zahtjevi mogu mijenjati ili dohvatiti podatke.

\- Relacijska baza podataka sa tablicama:

&nbsp; - 'Actor' – pohrana podataka o glumcima  

&nbsp; - 'Movie' – pohrana podataka o filmovima  

&nbsp; - 'MovieActor' – veza između filmova i glumaca (many-to-many)  

&nbsp; - 'MovieRates' – ocjene filmova  

&nbsp; - 'TvShow' – pohrana podataka o TV serijama  

&nbsp; - 'TvShowActor' – veza između serija i glumaca (many-to-many)  

&nbsp; - 'TvShowRating' – ocjene TV serija

\- Struktura baze omogućava jednostavno dohvaćanje Top 10 stavki i računanje prosječne ocjene.

---

\## **Tehnologije i alati**

\- \*\*Backend:\*\* .NET (C#)

\- \*\*Frontend:\*\* Angular

\- \*\*Baza podataka:\*\* SQL Server

\- \*\*Autentifikacija:\*\* JWT tokeni (za API)

\- \*\*Docker:\*\* Konfiguracija za jednostavno pokretanje aplikacije

\- \*\*Folder 'assets':\*\* Prikaz aplikacije i cover slike

---

