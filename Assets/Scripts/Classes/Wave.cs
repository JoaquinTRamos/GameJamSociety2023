using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Pool;


[Serializable]
public class Wave
{
    public int fire, water, earth, lightning, wind;

    public int GetEnemyCount()
    {
        return fire + water + earth + lightning + wind;
    }

    public List<Element> GetValidElements()
    {
        List<Element> results = new List<Element>();

        if (fire > 0)
            results.Add(Element.Fire);

        if (water > 0)
            results.Add(Element.Water);

        if (earth > 0)
            results.Add(Element.Earth);

        if (lightning > 0)
            results.Add(Element.Lightning);

        if (wind > 0)
            results.Add(Element.Wind);

        return results;
    }

    public void SubtractFromElement(Element element)
    {

        if (fire > 0 && element == Element.Fire)
            fire -= 1;

        if (water > 0 && element == Element.Water)
            water -= 1;

        if (earth > 0 && element == Element.Earth)
            earth -= 1;

        if (lightning > 0 && element == Element.Lightning)
            lightning -= 1;

        if (wind > 0 && element == Element.Wind)
            wind -= 1;
    }

    public void AssignElementQuantity(Element element, int i)
    {
        if (element == Element.Fire)
            fire = i;

        if (element == Element.Water)
            water = i;

        if (element == Element.Earth)
            earth = i;

        if (element == Element.Lightning)
            lightning = i;

        if (element == Element.Wind)
            wind = i;
    }
}



