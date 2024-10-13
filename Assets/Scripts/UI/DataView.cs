using R3;

namespace Inventory
{
    public class DataView
    {
        public readonly int Capacity;
        public readonly ReactiveProperty<int> Coins;

        public DataView(ReactiveProperty<int> coins, int capacity)
        {
            Coins = coins;
            Capacity = capacity;
        }
    }
}