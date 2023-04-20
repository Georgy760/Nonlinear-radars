using Common.Resources.Orion_2._4.Scripts;
using Zenject;

namespace Common.Resources.Orion_2._4.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public ControllerManager ControllerManagerPrefab;
        public override void InstallBindings()
        {
            BindControllerService();
        }

        private void BindControllerService()
        {
            Container
                .Bind<IControllerService>()
                .FromComponentInNewPrefab(ControllerManagerPrefab)
                .UnderTransform(transform).AsSingle();
        }
    }
}