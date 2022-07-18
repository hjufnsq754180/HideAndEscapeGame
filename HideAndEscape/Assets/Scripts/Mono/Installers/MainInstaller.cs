using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private GridMap _gridMap;

    public override void InstallBindings()
    {
        GridMap gridMap = Container.InstantiatePrefabForComponent<GridMap>(_gridMap);
        Container.Bind<GridMap>().FromInstance(gridMap).AsSingle().NonLazy();
    }
}