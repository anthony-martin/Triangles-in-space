
using Ninject.Modules;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Time;

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
        }


    }
}
