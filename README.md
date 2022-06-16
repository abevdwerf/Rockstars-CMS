# Rockstars-FHICT
Project rockstarsIT

Om het project lokaal te draaien zijn er een aantal punten om rekening mee te houden, namelijk:
- AppSettings.json aanmaken + database connectie invoeren
- Migrations draaien
- Spotify credentials aanvragen bij spotify en in SpotifyCredentials class zetten
- Active Directory koppelen aan applicatie, dit moet in de AppSettings.json. Hier moet een key ingevoerd worden waardoor de authorisatie via de ingevoerde
active directory loopt


## Talen Toevoegen

Bron: https://www.ezzylearning.net/tutorial/building-multilingual-applications-in-asp-net-core

Om een nieuwe website taal toe te voegen aan de applicatie moeten de volgende stappen worden gezet:
  - In de Startup.cs onder ConfigureServices is de code "services.Configure<RequestLocalizationOptions>(options => ...)" te vinden.
  - Tussen de options staan op het moment van de oplevering twee regels, "new CultureInfo(*taal*)". 
  - Om een nieuwe taal toe te voegen, moet hier nog zo een regel bijkomen. Vervang *taal* dan door een afkorting, zoals "nl" voor Nederlands of "de" voor Duits.
    - Varianten van talen zijn hier ook mogelijk. Bijvoorbeeld "en-uk" voor Brits Engels, en "en-us" voor Amerikaans Engels.
  - Maak nu in de map "Controllers" een nieuw item aan van het Resource type. 
    - Geef deze een naam in het volgende formaat: *controllernaam*.*taalafkorting*.resx. Bijvoorbeeld "HomeController.nl.resx"
  - In deze file zijn de volgende kolommen aanwezig: Name, Value en Comment.
    - Onder "Name" typ je een syntax waarmee de tekst kan worden aangeroepen. Deze naam moet hetzelfde zijn in alle resx files.
    - Onder "Value" typ je de tekst in de gewenste taal.
    - Onder "Comment" kan je een beschrijving/toelichting toevoegen op de ingevulde velden. Dit is optioneel.
    - Bijv. voor een pagina kop van de Index pagina, zou je als syntax "index.pagetitle" kunnen maken. Onder value "Welkom" voor Nederlands, en "Wilkommen" voor Duits.
  - In de controller moet er een veld van type "IStringLocalizer<*controllernaam*>" (Bijv. IStringLocalizer<HomeController>) komen. Deze moet ook in de constructor.
    - Hiervoor moet ook een reference komen. Deze zijn al aanwezig in de bestaande controllers.
  - In de view waarin je de vertaling wilt gebruiken, moet er een reference naar Microsoft.Extensions.Localization komen, en een injection van de IStringLocalizer.
    - Bijv. @using Microsoft.Extensions.Localization, gevolgd door @inject IStringLocalizer<Rockstars.Controllers.HomeController> Localizer.
  - Nu kan je de vertalingen overal aanroepen in de view, met @Localizer["*Name/syntax*"].
    -Bijv. <p>@Localizer["index.pagetitle"]</p>
  - Wisselen tussen talen moet met de select rechtsboven in de header van de pagina. Deze is te vinden in de _Layout.cshtml.
    - Nieuwe talen worden hier automatisch toegevoegd.
  - Dit wisselen wordt geregeld in de HomeController.cs onder de method "ChangeLanguage".
Voor meer details, zie de meegegeven bron.
