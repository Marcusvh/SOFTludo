Feature: Spilhåndtering
  
Som spiller
Vil jeg kunne oprette, deltage i, starte og spille Ludo-spil
Så jeg kan nyde at spille Ludo med andre spillere

Background:
  Given at spiltjenesten er tilgængelig

Scenario: Hent alle spil
  When jeg anmoder om alle spil
  Then bør jeg modtage en liste over spil

Scenario: Hent et specifikt spil
  Given at et spil med id 1 eksisterer
  When jeg anmoder om spil med id 1
  Then bør jeg modtage spillets detaljer

Scenario: Hent et ikke-eksisterende spil
  Given at intet spil med id 999 eksisterer
  When jeg anmoder om spil med id 999
  Then bør jeg ikke modtage noget indhold

Scenario: Opret et nyt spil
  Given en spiller med id 1 eksisterer
  When jeg opretter et nyt spil med spiller id 1
  Then bør spillet oprettes succesfuldt
  And spiller 1 bør være i spillet

Scenario: Deltag i et eksisterende spil
  Given at et spil med id 1 eksisterer
  And en spiller med id 2 eksisterer
  When spiller 2 deltager i spil 1
  Then bør spil 1 inkludere spiller 2

Scenario: Deltag i et ikke-eksisterende spil
  Given at intet spil med id 999 eksisterer
  When spiller 2 forsøger at deltage i spil 999
  Then bør der returneres en fejl

Scenario: Start et spil
  Given at et spil med id 1 eksisterer
  And spillet har nok spillere
  And en spiller med id 1 er i spillet
  When spiller 1 starter spil 1
  Then bør spil 1 være i gang

Scenario: Kast terningen
  Given at et spil med id 1 er i gang
  And det er spiller 1 tur
  When spiller 1 kaster terningen i spil 1
  Then bør terningekastets resultat returneres
  And spillets tilstand bør opdateres

Scenario: Spil en tur
  Given at et spil med id 1 er i gang
  And det er spiller 1 tur
  And spiller 1 har kastet terningen
  When spiller 1 spiller en brik i spil 1
  Then bør brikken flyttes tilsvarende
  And spillets tilstand bør opdateres

Scenario: Forsøg på ugyldigt træk
  Given at et spil med id 1 er i gang
  And det er spiller 1 tur
  And spiller 1 har kastet terningen
  When spiller 1 forsøger et ugyldigt træk i spil 1
  Then bør der returneres en fejl
  And spillets tilstand bør forblive uændret