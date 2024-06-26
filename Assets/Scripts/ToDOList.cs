using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDOList : MonoBehaviour
{
    public List<string> doZrobienia;

    private void Start()
    {
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
Tworzenie ulozenia pomieszczen
1. Wywolanie skryptu ktory bedzie wczytywal dane.
2. Wywolywanie funkcji ktora bedzie wysylac liczbe odpowiadajaca nastepnemu poziomowi
3. Koordynaty do teleportacji zostaja zapisane, a pomieszczenie ustawione w odpowiednie miejsce ( zaraz po nastepnym )
3. Po x razach, mapa zostala stworzona i jest gotowa do gry.
4. Nastepnie za kazdym razem jak host ( lub pierwsza osoba ), przechodzi przez portal wychodzi żądanie aby stworzyć przeciwników w pomieszczeniu.
Prefaby przeciwników zapisane są w Prefab List, więc wystarczy wygenerować u hosta odpowiednią kombinację.
Aasb
 */

