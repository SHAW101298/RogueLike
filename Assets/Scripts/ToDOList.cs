using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDOList : MonoBehaviour
{
    public List<string> doZrobienia;

    private void Start()
    {
        doZrobienia.Add("Gracze beda alertowac przeciwnikow w okolicy. Szybciej i latwiej w ten sposob");
        doZrobienia.Add("Terminal admina");
        doZrobienia.Add("Odporności na obrażenia");
        doZrobienia.Add("LifeSteal");
        doZrobienia.Add("Ai");
        doZrobienia.Add("Multiplayer");
        doZrobienia.Add("Pomieszczenia");
        doZrobienia.Add("ModelePrzeciwników");
        doZrobienia.Add("Tworzenie Broni Po Smierci Przeciwników");
        doZrobienia.Add("Wyswietlanie Zdrowia Przeciwnikow");
        doZrobienia.Add("Perki Postaci");
        doZrobienia.Add("Modele Broni");
        doZrobienia.Add("Prefaby Broni");
        doZrobienia.Add("Przywrocic lobby. Wybor ilosci pomieszczen, poziomu trudnosci, itp");
        doZrobienia.Add("Zdrowie gracza");
        //doZrobienia.Add("W scenie Main Menu powinien być już gracz stworzony. Podchodząc do portalu (warframe !) jest ustawiany Ready-Check");
        //doZrobienia.Add("Sprawdzenie czy gracz zalogowal sie do uslug - czyli czy faktycznie mial internet wlaczajac gre. W razie co sprobowac ponownie polaczyc");
        //doZrobienia.Add("Wyswietlanie Statystyk Broni po wskazaniu");
        //doZrobienia.Add("Wyodrebnic State Charging, z attack Ranged");
        //doZrobienia.Add("Zamiana pomiedzy posiadanymi broniami");
        //doZrobienia.Add("Przycisk w menu chowajacy okno lobby naprawić"); \/
        //doZrobienia.Add("WYlaczyc pokazywanie lobby podczas tworzenia go");
        doZrobienia.Add("Poprawić animacje trzymania pistoletu");
        //doZrobienia.Add("Ręce również powinny się obracać wraz z obrotem kamery");
        // https://www.youtube.com/watch?v=luBBz5oeR4Q
        //doZrobienia.Add("Loadign Screen");
        //doZrobienia.Add("Przechowanie broni na czas zmiany postaci");
        //doZrobienia.Add("Wysyłanie dla innych graczy mapy - string pokazujacy kolejnośc pomieszczeń ?");
        //doZrobienia.Add("Opcje w grze, typu zmiana rozdzielczosci, tryb okienkowy");
        //doZrobienia.Add("Pasek pokazujacy stan przeładowania");
        doZrobienia.Add("Animacja przeladowywania");
        doZrobienia.Add("Pobranie listy broni od graczy chwile po stworzeniu ich obiektu w swiecie");
        doZrobienia.Add("Poziom trudnosci powinien być aplikowany, gdy gracz dołącza do pokoju");
        doZrobienia.Add("Zapisac poziom trudnosci w lobby");
        doZrobienia.Add("Zapisywac ilosc pieniedzy kazdego gracza");
        doZrobienia.Add("Wypadanie rzeczy z przeciwnikow gdy umieraja");
        doZrobienia.Add("Dodac kazdemu graczowi wszystkie bronie, aby usprawnic proces zmiany broni. Bedzie to synchronizowane na bazie zmieniania Network Variable i aktywowania broni z odpowiednim tempalate ID");
        doZrobienia.Add("Przeciwnicy spawnuja sie wszyscy na raz, czy dopiero po wejsciu do pomieszczenia ?");
        doZrobienia.Add("Sufity w pomieszczeniach na wysokosci 5m+");
        doZrobienia.Add("Skrzynka tworzaca bronie. Do wykorzystaina na spawnie nawet");
        doZrobienia.Add("System EVentów dla gracza. Last Attacked ENemy, Last Received Damage, Last Enemy that attacked Player, Last Dealt Damage, Last Used Weapon etc");
        doZrobienia.Add("Przenieść statystyki do UnitData");
        doZrobienia.Add("Aim Correction przenieść na zewnatrz. Nie jest aplikowane podczas wybierania poziomu trudnosci");
        doZrobienia.Add("Gun Total Damage nie dziala prawdopodobnie ?");
        doZrobienia.Add("Przeciwnicy powinni isc do gracza dopoki nie maja na niego czystego pola strzalu, a nie dopoki nie dotarli na odpowiedni dystans");
        doZrobienia.Add("Late Enemy Trigger. Gdy gracz zaatakuje przeciwnika ktory nie jest pobudzony, system powinien to wychwycic i zaalarmowac cala grupe");
        doZrobienia.Add("Breakables nie są przesylane do innych graczy");
        doZrobienia.Add("Afflicitons never end. Add Timer");
    }

}

/* Poziom trudności zmienia :
- Życie przeciwników.
- Obrażenia przeciwników 
- Ilość pieniędzy
- Odporności przeciwników
 */



/* Czego brakuje do rozegrania gry
 Lobby w którym wybierze sie poziom trudności
 Host rozpoczyna rozgrywkę, i przenosi go do mapy gry, rozpoczyna się od razu również tworzenie mapy na danym poziomie
 Tworzą się podstawowe postacie
 Gracze wybierają postacie
 Gracze dostają 1 losowa broń
 Gracze podchodzą do portalu i zostają przenoszeni na mapę gry
 Gdzieś w miedzyczasie spawnują się przeciwnicy na mapie ( lub przygotowane jest wcześniej wywoływanie spawnowania triggerem ? )
 Gdy gracze zblizają się do przeciwników lub do nich zaczynają strzelać, rozpoczyna sie walka i cała gra
*/

/*
Bronie na start będą miały 1-2 sloty na bonusy. Zależnie od piętra ( poziomu broni), ilość bonusów będzie się zwiększać. 
Poprzez walute zdobytą w grze, będzie można ulepszać bronie poza grą. Na przykład wykupienie "Chaotycznego Slotu" który pozwala umieścić chaotyczny bonus do broni na stałe lub przed grą
Chaotyczne bonusy będą wypadały podczas pomieszczeń wyzwań
- Bonusy będą mogły mieć rózne poziomy ?
- Bonusy nie mogą się powtórzyć ?
- Limit poziomu na bonusie jakiś ?

Trzeba zdecydować gdzie będzie odbywało się edycja chaotycznych bonusów i ich aplikacja
Główne pomieszczenie będzie służyło jako HUB do wszystkich czynności

 */

/* Dostepne Typy Broni
Pistolety
Rewolwery
Karabiny
Shotguny
Kusze
Karabiny Snajperskie
Uzi-ki
Bazoomkas
 */



//Kazda postac posiadac bedzie 1-2 umiejetnosci uzywalne w czasie gry.
// Gracze mogący zwiekszyc poziom trudnosci pomieszczen na wlasne zyczenie, zwiekszajac zdobywane nagrody.
// Zmienic system broni. Kazda statystyka ma poziom 1-5, i zostaje ulepszona wraz z poziomem broni. 

/*      START LOOP
 1. Pojawienie się menu głównego
    I Single Player tworzy prywatny serwer 
    II MultiPlayer tworzy lobby, do którego można się podłączyć poprzez kod lub z listy
 2. Wybranie Trybu
 3. Multiplayer
    - Hostowanie
    Stworzenie Lobby, a host przechodzi na scene gry oraz startuje Relay ( Lobby jest tylko do ustawienia pierwszego połączenia )
    Klienci po dołączeniu do lobby są automatycznie wrzucani do sceny gry oraz podłączeni do Relay
    Tworzy się główna postać gracza która pozwala na interakcje ze światem, a gracze mogą wybrać postać którą chcą grać.
    Gracze wybierają swoje pierwsze bronie
    Gracze wchodzą do strefy startu a host rozpoczyna faktyczną grę przyciskiem w świecie
    Zmiana sceny, lub wytworzenie modelu sceny za pomocą random-gen
    - Wszystkie dane graczy są zapisywane u hosta, pozwoli to na ponowne dołączenie do gry w razie problemu
    - Dostępna będzie konsola która ma przyzwolenie hosta serwera ( do rozwiązywania problemów w rozgrywce )
 4. SinglePlayer
    Tworzy się prywatny serwer a wszystko wygląda bardzo podobnie do multiplayer, jednak bez ogłaszania się w internecie
*/

/* To DO
Prefab Gracza powinien umożliwić ruszanie się, obracanie, wybieranie postaci. Prefab musi być widoczny dla innych ( jakiś duszek albo coś takiego )
Dla przeciwników dopiero przy aktywacji pomieszczenia dodać wykrywanie graczy, dzieki temu pomieszczenia mogą być wieksze ale przeciwnicy tylko grupami będą przychodzić
Stworzyć terminal dla admina

*/

/* Bounty Hunter
Bounty Hunter
Pasywka - Na każdej mapie jest przeciwnik za którego dostanie dodatkową ilość pieniędzy
Give It A Shot - 

 Bounty Hunter Ulepszenia
Zwiekszenie szansy na krytyk
Ostatni pocisk zawsze krytuje
Obrazenia z pistoletow

 */

/* Room Creation idea
Tworzenie ulozenia pomieszczen
1. Wywolanie skryptu ktory bedzie wczytywal dane.
2. Wywolywanie funkcji ktora bedzie wysylac liczbe odpowiadajaca nastepnemu poziomowi
3. Koordynaty do teleportacji zostaja zapisane, a pomieszczenie ustawione w odpowiednie miejsce ( zaraz po nastepnym - Obliczyć bounding Box ?)
3. Po x razach, mapa zostala stworzona i jest gotowa do gry.
4. Nastepnie za kazdym razem jak host ( lub pierwsza osoba ), przechodzi przez portal wychodzi żądanie aby stworzyć przeciwników w pomieszczeniu.
Prefaby przeciwników zapisane są w Prefab List, więc wystarczy wygenerować u hosta odpowiednią kombinację.
 */

/* Rooms Idea 2
1. Po aktywowaniu portalu jest loading Screen
2. W czasie loading Screenu gracze teleportowani są na bezpieczny plane
3. Stworzenie pierwszego pomieszczenia bezpiecznego z jednym wyjściem
4. Każde pomieszczenie będzie zawierać dane ile zajmuje miejsca na gridzie
I na bazie tego gridu bedzie sprawdzane czy można umieścić dane pomieszczenie w tym miejscu
Jeżeli tak, wstawiany jest ten prefab, oznaczenie drzwi na gridzie i od tego miejsca ponownie szukanie odpowiedniego prefabu. 
W razie problemu tworzenie od zera lub ustawienie portalu wcześniej.
5. Po stworzeniu 4? pomieszczeń na końcu tworzony jest portal do innego etapu.
6. Nastepuje tworzenie Nawigacji dla AI
7. Każde pomieszczenie będzie miało 2 podłogi. 1 dla AI do zczytania danych a 2 dla estetyki.
 */

/* Old Lobby
 Lobby:
Przycisk pozwalajacy pokazac i schowac okno Lobby ( jezeli jest sie do jakiegos podlaczonym )
Ikony z postaciami podswietlaja sie na czerwono / zielono, zaleznie od przycisku przygotowania
Po schowaniu lobby, pokazane sa tylko ikony postaci wraz z statusem przygotowania
 */

/* Bonuses from Verminitide
 
Assisting an ally under attack restores 15 Temporary Health to both players. (Assisting allies refers to saving them from a Gutter Runner, Packmaster, or Lifeleech, or Staggering Enemies who are about to backstab them.)
Blocking an attack increases the damage the attacker takes by 20% for 5.0 seconds.
Increases push strength by 50% when used against an attacking enemy.
Timed blocks reduce stamina cost by 100%.
Melee critical strikes reduce the cooldown of your Career Skill by 5%. Effect can only trigger once every 4 seconds.
Critical Hits increase attack speed by 20% for 5.0 seconds. 

Consecutive attacks against the same targets boosts attack power by 5.0% for 5.0 seconds. (This effect stacks up to 5 times.)
Headshots replenish 1.0 ammo
Critical hits refund the overcharge cost of the attack. (Only appears on Sienna's staves, Kerillian's Deepwood staff and Bardin's Drakefire weapons)
Critical hits increase attack power by 25% against targets with the same armor class for a short time. (This effect lasts 10 seconds.)
Headshots restore stamina to nearby allies. (Restores 1 stamina, half of a shield icon.)
Ranged critical hits reduce the cooldown of your Career Skill by 5%. Effect can only trigger once every 4 seconds.
Critical hits restore 5% of maximum ammunition. Can trigger once per attack.
Weapon generates 20.0% less Overheat. (Only appears on Sienna's staves, Kerillian's Deepwood staff and Bardin's Drakefire weapons) 

Taking damage reduces the damage you take from subsequent sources by 40% for 2 seconds. This effect can only trigger every 2 seconds.
Increases effectiveness of healing on you by 30%.
Healing an ally with medical supplies also heals you for 50.0% of your missing health.
25.0% chance to not consume healing item on use.
Passively regenerates 1 health every 5 seconds. Healing from First Aid Kits and Healing Draughts are converted to temporary health and cure any wound. 
 */

/* Statystyki opisujące broń 
    obrazenia
    szansa na kryty
    mnoznik krytow
    Affliction Chance ( status )
    amunicja / max
    szybkosc strzelania
    stability
    poziom
 */

/* Bonusy losowane do broni
+    Zwiekszenie obrazen
+    Zwiekszenie obrazen na przeciwnikach pod wyplywem statusu
+    Zwiekszenie szansy na krytyk
+    Zwiekszenie crit multiplier
+    Zwiekszenie szybkosci strzelania
+    Zwiekszenie amunicji
+    Zwiekszenie affliction Chance
+    Zmniejszenie czasu przeładowania
 */

/* Crazy ideas
 Instead of portals, there are doors, which close permanently.
Players can block off sections, which forces those left out to find other way or die trying.
  Map made with a tower design. Main stairs are available from beggining, but rooms get generated all around the main center point

        R  R
     RRRSRRRR
     R  R 


 */

/* Pomysły na perki
 Zwiększenie podstawowych statystyk postaci
    zdrowie
    stamina
    tarcza
    szybkosc ruchu
    regeneracja zdrowia
    regeneracja staminy
    regeneracja tarczy
    odpornosci
 Wywolanie efektu podczas otrzymania obrazen
 Wywolanie efektu podczas zadawania obrazen
 Zwiekszenie obrazen gdy jest sie w Affliction
 Zwiekszenie otrzymywanego zlota
 Zwiekszenie zbieranej amunicji
 Leczenie ostatnio otrzymanych obrazen
 Leczenie o ilosc ostatnio zadanych obrazen
 Zwiekszenie czegoś po przeładowaniu
 Zwiekszenie czegoś gdy pełne/mało  zdrowia lub staminy

GRA SWORN
 */

/* IKONY
Crosshair - Crit Chance
headshot - critical damage
heat - heat
cold - cold
flash - electricity
toxin - toxin
ammo - magazine
reload - reload

status chance
projectile amount
fire rate
 */

/* SKLEPIKARZE
 * 
 */

/* ZNISZCZALNE OBIEKTY NA MAPIE
Proste modele obiektów które da się zniszczyć poprzez zadawanie im obrażeń umiejetnosciami / przedmiotami / broniami. Jest szansa na wypadniecie zlota lub przedmiotow z nich.
Do zrobienia
    - Modele zniszczalnych obiektów
    - Modele zniszczonych obiektów
    - Skrypt BreakAbleObject
        Przechowuje zdrowie oraz Loot Table
    - Tworzenie tych obiektow na mapie w losowy sposob
        Nie zawsze się pojawiają, są zapisane miejsca w których mogą się pojawić
 */

/* OLTARZE POSWIECEN
Interaktywny spot, w którym można poświęcić część złota / zdrowia / statystyk, aby coś zyskać wzamian.
Do zrobienia
    - Modele Ołtarzy Gotowych
    - Modele Ołtarzy Użytych
    - Skrypt do interakcji z ołtarzem
    - Skrypt zabierający statystykę a dający nagrodę
    - Stworzenie UI do ołtarza
        * Ikona poświecanej statystyki
        * Ikona zyskiwanej nagrody
        * Reakcja na przyciski w UI
 */

/* ZUZYWALNE PRZEDMIOTY

 */

/* BUFFY CZASOWE

 */

/* SPAWNERY PRZECIWNIKOW

 */

/* CHALLENGE ROOM

 */

/* GAMBLING

 */

/* UI elements
Gdy zadawane są obrażenia, pokazywanie obrażeń nad przeciwnikami
Gdy zbierane jest złoto, małe powiadomienie w rogu ekranu
Gdy zbierane jest życie / amunicja jakiś flash ekranu
Słup światła na leżących przedmiotach
 */

/* Player Triggers
OnEnemyWeaponHit
OnEnemyAbilityHit
OnEnemyBlessingHit

OnReloadEmpty
OnReloadHalfEmpty
OnReload

OnHealthPickUp
OnAmmoPickUp
OnGoldPickUp
OnBlessingPickUp
OnGunPickUp

OnTakeHealthDamage
OnTakeShieldDamage

OnEnemyKill
OnDeath
OnBossKill
OnChallengeRoomCompleted

OnDash
OnItemBought
OnAbilityUse
OnJump
OnShoot
OnWeaponSwap

Every X seconds
Every X meters moved
Every X jump
Every X shot

 */

/*Player Perks
    Fill magazine
    Increase Damage of first shot
    Increase Damage of some shots
    Restore Health
    Restore Shields
    Restore Stamina
    Grant Immunity
    Grant Resistance
*/
