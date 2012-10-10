using System;

namespace TrianglesInSpace.Disposers
{
    public class Disposer : IDisposable
    {
        private Action m_Disposables;
        
        public void Add(Action dispose)
        {
            m_Disposables += dispose;
        }

        public void Dispose()
        {
            m_Disposables();
        }
    }

    public static class DisposerExtensions
    {
        public static void AddTo(this Action action, Disposer disposer)
        {
            disposer.Add(action);
        }
    }

}
