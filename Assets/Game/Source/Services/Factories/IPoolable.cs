using System;

namespace Game.Source.Services.Factories
{
    public interface IPoolable<T>
    {
        public Action<T> Release { get; set; }
    }
}