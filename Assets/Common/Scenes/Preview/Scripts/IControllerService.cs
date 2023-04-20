using System;

namespace Common.Scenes.Preview.Scripts
{
    public interface IControllerService
    {
        event Action OnPrevModel;
        event Action OnNextModel;
        event Action OnSelectModel;
    }
}
