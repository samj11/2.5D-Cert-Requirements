using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _UICollectable;

    [SerializeField]
    private int _collectableCount;

    // Start is called before the first frame update
    void Start()
    {
        _UICollectable.text = "Collectable : " + _collectableCount;
    }

    public void UpdateCollectableCount()
    {
        ++_collectableCount;
        _UICollectable.text = "Collectable : " + _collectableCount;
    }
}