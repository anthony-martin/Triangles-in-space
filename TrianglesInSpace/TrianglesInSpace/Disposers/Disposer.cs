using System;
using System.Collections.Generic;

namespace TrianglesInSpace.Disposers
{
    class Disposer : IDisposable
    {
        private readonly List<Action> m_Disposables;
        
        public Disposer()
        {
            m_Disposables = new List<Action>();
        }

        public void Add(Action dispose)
        {
            m_Disposables.Add(dispose);
        }

        public void Dispose()
        {
            foreach (var disposable in m_Disposables)
            {
                disposable();
            }
        }
    }
}
