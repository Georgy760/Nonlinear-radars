using Common.Resources.NR_900EMS.Scripts;
using Zenject;

namespace Common.Resources.NR_900EMS.Infrastructure
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