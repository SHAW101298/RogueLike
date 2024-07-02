using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameRandomizer : MonoBehaviour
{
    #region
    public static NameRandomizer Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
#endregion
    public List<string> possibleNames;
    // Start is called before the first frame update
    void Start()
    {
        LoadRandomNames();
    }

    void LoadRandomNames()
    {
        possibleNames.Add("Wigglebrain");
        possibleNames.Add("Fartzy");
        possibleNames.Add("Tootseed");
        possibleNames.Add("Spottyseed");
        possibleNames.Add("Shoospitz");
        possibleNames.Add("Swampworthy");
        possibleNames.Add("Roachworth");
        possibleNames.Add("Weeby");
        possibleNames.Add("Pimplerider");
        possibleNames.Add("Wigglerider");
        possibleNames.Add("Beerider");
        possibleNames.Add("Trashaloo");
        possibleNames.Add("Sockhair");
        possibleNames.Add("Slugbuns");
        possibleNames.Add("Roachbag");
        possibleNames.Add("Stinkbag");
        possibleNames.Add("Pukeshine");
        possibleNames.Add("Blubberface");
        possibleNames.Add("Bad News Swackhamer");
        possibleNames.Add("Stinky Porkins");
        possibleNames.Add("Dicman Peterson");
        possibleNames.Add("Foncy Woolysocks");
        possibleNames.Add("Buttermilk Sackrider");
        possibleNames.Add("Cheesy McFilthy");
        possibleNames.Add("Silky Stains");
        possibleNames.Add("Bobby Stainsworth");
        possibleNames.Add("Bambi Slurpington");
        possibleNames.Add("Rusty Shaftspinner");
        possibleNames.Add("Sasha Spankster");
        possibleNames.Add("Filthy Von Grime");
        possibleNames.Add("Floppy Wetwipe");
        possibleNames.Add("Misty Moist");
        possibleNames.Add("Bambi Bottoms");
        possibleNames.Add("Lance Thruster");
        possibleNames.Add("Rusty Johnson");
        possibleNames.Add("Chuckletron 3000");
        possibleNames.Add("Giggle Galore");
        possibleNames.Add("Chuckleberry Finn");
        possibleNames.Add("Chucklehead Charlie");
        possibleNames.Add("Laughing Lulu");
        possibleNames.Add("Snickerdoodle Dynamite");
        possibleNames.Add("Wacky Whizbang");
        possibleNames.Add("Jester Jumping Jack");
        possibleNames.Add("DuckDeath");
        possibleNames.Add("WarBad");
        possibleNames.Add("DemonicCocoa");
        possibleNames.Add("EaglePickle");
        possibleNames.Add("MoonPickle");
        possibleNames.Add("DirtyMoon");
        possibleNames.Add("PandoraPrimus");
        possibleNames.Add("Analinator");
    }
    public string GetRandomName()
    {
        int r = Random.Range(0, possibleNames.Count);
        int n = Random.Range(0, 100);
        return possibleNames[r] + n;
        
    }
}
