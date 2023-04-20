using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutputPower : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    public Power Power;
    public void SetPower(Power val)
    {
        Power = val;
        _textMeshPro.text = val.ToString();
    }
}

public enum Power
{
    POOF,
    PMIN,
    PMAX
}