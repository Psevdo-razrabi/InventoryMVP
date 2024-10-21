using Inventory;
using UnityEngine;
using Zenject;

public class ProjectInject : MonoInstaller
{
    [SerializeField] private InventoryView _inventoryView;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InventoryView>().FromInstance(_inventoryView).AsSingle().NonLazy();
    }
}
