using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Resources.Orion_2._4.Scripts.Display
{
    [RequireComponent(typeof(Image))]
    public class InterfaceButtons : MonoBehaviour
    {
        [SerializeField] private Sprite unselectedFrame;
        [SerializeField] private Sprite selectedFrame;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color _unselectedColor = new Color(255, 178, 37);
        [SerializeField] private Image _image;
        [SerializeField] 

        public void Select()
        {
            text.color = Color.black;
            _image.sprite = selectedFrame;
        }

        public void Unselect()
        {
            text.color = _unselectedColor;
            _image.sprite = unselectedFrame;
        }
    }
}
