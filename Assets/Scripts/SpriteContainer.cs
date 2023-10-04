using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteContainer 
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    public List<Sprite> Sprites { get { return _sprites; } }
}
