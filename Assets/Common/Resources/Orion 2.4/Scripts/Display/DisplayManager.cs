using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

namespace Common.Resources.Orion_2._4.Scripts.Display
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField] private List<InterfaceButtons> _interfaceButtonsRow1;
        [SerializeField] private List<InterfaceButtons> _interfaceButtonsRow2;
        [SerializeField] private int _selectedButton = 0;
        [SerializeField] private int _selectedRow = 0;

        public void OnEnable()
        {
            _interfaceButtonsRow1[_selectedButton].Select();
        }
      


        public void MoveDown()
        {
            if (_selectedRow == 0)
            {
                _selectedRow = 1;
                _interfaceButtonsRow1[_selectedButton].Unselect();
                if (_interfaceButtonsRow1.Count > _interfaceButtonsRow2.Count)
                {
                    if(_selectedButton == _interfaceButtonsRow1.Count - 1)
                    {
                        _selectedButton = _interfaceButtonsRow2.Count-1;
                        _interfaceButtonsRow2[_selectedButton].Select();
                    } else _interfaceButtonsRow2[_selectedButton].Select();
                }
                else
                {
                    _interfaceButtonsRow2[_selectedButton].Select();
                }
            }
        }

        public void MoveUp()
        {
            if (_selectedRow == 1)
            {
                _selectedRow = 0;
                _interfaceButtonsRow2[_selectedButton].Unselect();
                if (_interfaceButtonsRow1.Count < _interfaceButtonsRow2.Count)
                {
                    if(_selectedButton == _interfaceButtonsRow2.Count - 1)
                    {
                        _selectedButton = _interfaceButtonsRow1.Count-1;
                        _interfaceButtonsRow1[_selectedButton].Select();
                    }
                    else _interfaceButtonsRow1[_selectedButton].Select();
                }
                else
                {
                    _interfaceButtonsRow1[_selectedButton].Select();
                }
            }
        }
        public void MoveLeft()
        {
            if (_selectedButton > 0)
            {
                switch (_selectedRow)
                {
                    case 0:
                        _interfaceButtonsRow1[_selectedButton].Unselect();
                        _selectedButton--;
                        _interfaceButtonsRow1[_selectedButton].Select();
                        break;
                    case 1:
                        _interfaceButtonsRow2[_selectedButton].Unselect();
                        _selectedButton--;
                        _interfaceButtonsRow2[_selectedButton].Select();
                        break;
                }
            }
        }
        public void MoveRight()
        {
            switch (_selectedRow)
            {
                case 0:
                    if (_selectedButton < _interfaceButtonsRow1.Count - 1)
                    {
                        _interfaceButtonsRow1[_selectedButton].Unselect();
                        _selectedButton++;
                        _interfaceButtonsRow1[_selectedButton].Select();
                    }
                    break;
                case 1:
                    if (_selectedButton < _interfaceButtonsRow2.Count - 1)
                    {
                        _interfaceButtonsRow2[_selectedButton].Unselect();
                        _selectedButton++;
                        _interfaceButtonsRow2[_selectedButton].Select();
                    }
                    break;
            }
        }
    }
}
