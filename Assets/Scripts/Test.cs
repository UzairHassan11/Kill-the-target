using UnityEngine;

public class Test : MonoBehaviour
{
    public bool RectsOverlap(Rect rect1, Rect rect2)
    {
        if ((rect1.xMax > rect2.xMin && rect1.xMin < rect2.xMax)
            &&
            (rect1.yMax > rect2.yMin && rect1.yMin < rect2.yMax))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}