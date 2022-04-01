using UnityEngine;

public class SniperAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string str)
    {
        print("as");
        _animator.SetTrigger(str);
    }
}
