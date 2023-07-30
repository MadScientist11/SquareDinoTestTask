using System;

namespace Game.Source.Services
{
    public interface IPoolable<T>
    {
        public Action<T> Release { get; set; }
    }
}