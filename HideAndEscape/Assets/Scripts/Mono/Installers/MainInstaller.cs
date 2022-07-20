using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private GridMap _gridMap;
    [SerializeField]
    private GameManager _gameManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        GridMap gridMap = Container.InstantiatePrefabForComponent<GridMap>(_gridMap);
        Container.Bind<GridMap>().FromInstance(gridMap).AsSingle().NonLazy();
    }
}