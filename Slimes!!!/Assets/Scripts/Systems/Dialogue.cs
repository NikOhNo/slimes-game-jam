using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    [SerializeField] 
    private string characterName;
    [SerializeField]
    private Sprite characterImage;
    [TextArea(3, 10)]
    [SerializeField] 
    private string[] sentences;

    public string CharacterName { get { return characterName; } }
    public Sprite CharacterImage { get { return characterImage; } }
    public string[] Sentences { get { return sentences; } }
}
