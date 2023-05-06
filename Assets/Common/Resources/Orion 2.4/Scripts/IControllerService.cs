using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Resources.Orion_2._4.Scripts
{
    public interface IControllerService
    {
        /// <summary>
        ///ПИТАНИЕ – Нажать для включения. Нажать и удерживать для 
        ///выключения. Короткое нажатие при включенном приборе вызывает 
        ///переключение между автоматическим и ручным выбором мощности 
        ///передатчика.
        /// </summary>
        event Action OnPowerTap;

        void PowerTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///ЗВУК – Нажатие с удержанием включает либо выключает звук. 
        ///Короткое нажатие вызывает меню Звук (Audio Mode) и позволяет выбрать 
        ///тип звукового оповещения: Tones, Listen (2ndAM, 2ndFM, 3rdAM, 3rdFM)
        /// </summary>
        event Action OnVolumeTap;

        void VolumeTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///УВЕЛИЧЕНИЕ МОЩНОСТИ / КНОПКА ВВЕРХ – В зависимости от выбора 
        ///режима управления мощностью: в режиме Auto – увеличивает границу 
        ///мощности; в режиме Manual – увеличивает мощность. В режиме Меню
        ///(Menu) используется для навигации.
        /// </summary>
        event Action OnPowerUp;

        void PowerUpTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///УМЕНЬШЕНИЕ МОЩНОСТИ / КНОПКА ВНИЗ – В зависимости от 
        ///выбора режима управления мощностью: в режиме Auto – уменьшает 
        ///границу мощности; в режиме Manual – уменьшает мощность. В режиме
        ///Меню (Menu) используется для навигации.
        /// </summary>
        event Action OnPowerDown;

        void PowerDownTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///УМЕНЬШЕНИЕ ГРОМКОСТИ / КНОПКА ВЛЕВО – Уменьшает громкость 
        ///звука. В режиме Меню (Menu) используется для навигации.
        /// </summary>
        event Action OnVolumeDown;

        void VolumeDownTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///УВЕЛИЧЕНИЕ ГРОМКОСТИ / КНОПКА ВПРАВО – Увеличивает громкость 
        ///звука. В режиме Меню (Menu) используется для навигации.
        /// </summary>
        event Action OnVolumeUp;

        void VolumeUpTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///УВЕЛИЧЕНИЕ ГРОМКОСТИ / КНОПКА ВПРАВО – Увеличивает громкость 
        ///звука. В режиме Меню (Menu) используется для навигации.
        /// </summary>
        event Action OnInput;

        void InputTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///МЕНЮ – Вызов Меню настроек / выход из Меню настроек
        /// </summary>
        event Action OnMenu;

        void MenuTap(InputAction.CallbackContext callbackContext);
        /// <summary>
        ///ЛКМ кликнута
        /// </summary>
        event Action OnMouseClick;
        /// <summary>
        ///Позиция мыши
        /// </summary>
        event Action<Vector2> MousePos;

        event Action OnEsc;

        event Action<float> MoveOrion;

        event Action OnHelp;
        
    }
}
