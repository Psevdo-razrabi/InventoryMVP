﻿using UniRx;

namespace CustomObserverable
{
    public interface IObservableArray<T>
    {
        public Subject<T[]> ValueChangeInArray { get; }
        public int Count { get; }
        T this[int index] { get; }
        void Swap(int indexOne, int indexTwo);
        void Clear();
        bool TryAdd(T element);
        bool TryRemove(T element);
    }
}