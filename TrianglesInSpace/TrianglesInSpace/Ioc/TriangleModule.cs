
using Ninject.Modules;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Time;
using TrianglesInSpace.Rendering;
using TrianglesInSpace.Wpf;
using TrianglesInSpace.Objects;
using TrianglesInSpace.World;
using TrianglesInSpace.Vessels;

namespace TrianglesInSpace.Ioc
{
    class TriangleModule : NinjectModule
    {
        public override void Load()
        {
            BindBus();
            //Bind<>().To<>();
        }

        private void BindBus()
        {
            Bind<IBus>().To<MessageBus>().InSingletonScope();
            Bind<IMessageSender>().To<MessageSender>();
            Bind<IMessageReceiver>().To<MessageReceiver>();
            Bind<IMessageRegistrationList>().To<MessageRegistrationList>();
            Bind<IMessageContext>().To<MessageContext>().InSingletonScope();
            Bind<IClock>().To<SynchronizedClock>().InSingletonScope();
            Bind<IRenderer>().To<Renderer>().InSingletonScope();
            Bind<IMainFormModel>().To<MainFormModel>().InTransientScope();

            Bind<IPlayerId>().To<PlayerId>().InSingletonScope();
            Bind<ISelectableObjectRepository>().To<SelectableObjectRepository>().InSingletonScope();
            Bind<IFieldDisplay>().To<FieldDisplay>().InSingletonScope();
            Bind<ITargetedVesselRenderer>().To<TargetedVesselRenderer>().InSingletonScope();

            Bind<IVesselRepository>().To<VesselRepository>().InSingletonScope();
        }


    }
}
