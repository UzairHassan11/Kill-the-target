using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public int[] arr;
    public int result;
    public List<int> destinations = new List<int>();
    public List<int> tripsSpans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Button]
    int din()
    {
        destinations.Clear();
        tripsSpans.Clear();
        for (int i = 0; i < arr.Length; i++)
        {
            print("0");
            if (!destinations.Contains(arr[i]))
            {
                destinations.Add(arr[i]);
            }
        }

        // destinations.Sort();

        tripsSpans = new List<int>(arr.Length);
        for (int i = 0; i < arr.Length; i++)
        {
            int n = AllDestinationTripsDays(i, destinations);
            if(n !=-1)
                tripsSpans.Add(n);
        }

        int desiredDay = tripsSpans[tripsSpans.Count - 1];
        for (int i = 0; i < tripsSpans.Count; i++)
        {
            if (tripsSpans[i] < desiredDay)
                desiredDay = tripsSpans[i];
        }

        print(desiredDay);
        return desiredDay;
    }

    int AllDestinationTripsDays(int start, List<int>destinations)
    {
        int result = 0;
        List<int> tempDestinations = destinations.ToList();
        for (int i = start; i < arr.Length; i++)
        {
            if (tempDestinations.Contains(arr[i]))
                tempDestinations.Remove(arr[i]);

            result++;
            
            if(tempDestinations.Count == 0)
                break;
        }

        if (tempDestinations.Count == 0)
            return result;
        else
            return -1;
    }

    [Button]
    void PopulateList()
    {
        for (int i = 0; i < arr.Length; i++)
        {
            destinations.Add(arr[i]);
        }
    }
}
