using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int[] arr;
    public int s;
    public int result;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Button]
    int func()
    {
        result = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                int mean = (int) GetMean(i, j);
                if (mean == s)
                    result++;
                else if(mean > s)
                    break;
            }
        }

        if (result > 1000000000)
            result = 1000000000;

        return result;
    }

    float GetMean(int start, int end)
    {
        float result = 0;
        
        for (int i = start; i <= end; i++)
        {
            result += arr[i];
        }

        return result / (end-start+1);
    }

}
