using System;

namespace Common.Resources.NR_900EMS.Scripts
{
    public interface IControllerService
    {
        event Action OnMinusTap;
        event Action OnPlusTap;
        event Action OnLeftArrowTap;
        event Action OnRightArrowTap;
        event Action OnAsteriskTap;
        event Action OnPwrTap;
        event Action OnPowerTap;
        event Action OnTwentyKTap;
        event Action OnThreeDivTwoTap;
    }
}
