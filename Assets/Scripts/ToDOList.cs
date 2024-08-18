using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDOList : MonoBehaviour
{
    public List<string> doZrobienia;

    private void Start()
    {
        doZrobienia.Add("Sprawdzenie czy gracz zalogowal sie do uslug - czyli czy faktycznie mial internet wlaczajac gre. W razie co sprobowac ponownie polaczyc");
        //doZrobienia.Add("W scenie Main Menu powinien być już gracz stworzony. Podchodząc do portalu (warframe !) jest ustawiany Ready-Check");
        doZrobienia.Add("Odporności na obrażenia");
        doZrobienia.Add("LifeSteal");
        doZrobienia.Add("Ai");
        doZrobienia.Add("Multiplayer");
        doZrobienia.Add("Pomieszczenia");
        doZrobienia.Add("ModelePrzeciwników");
        doZrobienia.Add("Tworzenie Broni Po Smierci Przeciwników");
        doZrobienia.Add("Wyswietlanie Statystyk Broni po wskazaniu");
        doZrobienia.Add("Wyswietlanie Zdrowia Przeciwnikow");
        doZrobienia.Add("Perki Postaci");
        doZrobienia.Add("Modele Broni");
        doZrobienia.Add("Prefaby Broni");
    }

}
// 


/*
 Kazda postac posiadac bedzie 1-2 umiejetnosci uzywalne w czasie gry.
Bounty Hunter
Pasywka - Na każdej mapie jest przeciwnik za którego dostanie dodatkową ilość pieniędzy
Give It A Shot - 
 */
/*
Tworzenie ulozenia pomieszczen
1. Wywolanie skryptu ktory bedzie wczytywal dane.
2. Wywolywanie funkcji ktora bedzie wysylac liczbe odpowiadajaca nastepnemu poziomowi
3. Koordynaty do teleportacji zostaja zapisane, a pomieszczenie ustawione w odpowiednie miejsce ( zaraz po nastepnym )
3. Po x razach, mapa zostala stworzona i jest gotowa do gry.
4. Nastepnie za kazdym razem jak host ( lub pierwsza osoba ), przechodzi przez portal wychodzi żądanie aby stworzyć przeciwników w pomieszczeniu.
Prefaby przeciwników zapisane są w Prefab List, więc wystarczy wygenerować u hosta odpowiednią kombinację.
Aasb
 */


/*
 Lobby:
Przycisk pozwalajacy pokazac i schowac okno Lobby ( jezeli jest sie do jakiegos podlaczonym )
Ikony z postaciami podswietlaja sie na czerwono / zielono, zaleznie od przycisku przygotowania
Po schowaniu lobby, pokazane sa tylko ikony postaci wraz z statusem przygotowania
 */


/*
 Cherrif Ulepszenia
Zwiekszenie szansy na krytyk
Ostatni pocisk zawsze krytuje
Obrazenia z pistoletow

 */

/*
 
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


/*
Statystyki opisujące broń 
    obrazenia
    szansa na kryty
    mnoznik krytow
    Affliction Chance ( status )
    amunicja / max
    szybkosc strzelania
    stability
    poziom
 */
/*
Bonusy losowane do broni
+    Zwiekszenie obrazen
+    Zwiekszenie obrazen na przeciwnikach pod wyplywem statusu
+    Zwiekszenie szansy na krytyk
+    Zwiekszenie crit multiplier
    Zwiekszenie szybkosci strzelania
    Zwiekszenie amunicji
+    Zwiekszenie affliction Chance
    Zmniejszenie czasu przeładowania
 */
