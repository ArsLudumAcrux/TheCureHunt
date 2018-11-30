using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potion")]
public class PotionScriptableObj : ScriptableObject {

    public int id;
    public int healthGain;

    [Range(0,4)]
    [Tooltip("Função Poção:\n0 = nada\n1 = proteção\n2 = recuperação\n3 = tempo\n4 = sorte")]
    public int potionFunction;

    public string potionName;
    public string potionDescription;
    public Sprite artwork;

    public float Duration;
}
