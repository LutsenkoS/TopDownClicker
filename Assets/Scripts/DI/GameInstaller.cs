using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] EnemyBlackController enemyBlack;
    [SerializeField] EnemyBlueController enemyBlue;
    [SerializeField] EnemyRedController enemyRed;
    [SerializeField] MenuView menu;
    [SerializeField] GameOverView gameOver;


    public override void InstallBindings()
    {

        InstallGame();
        InstallViews();
        InstallEnemies();
    }

   

    private void InstallGame()
    {
        Container.Bind<GameController>().AsSingle();
    }

    private void InstallViews()
    {
        Container.Bind<IMenuView>().To<MenuView>().FromInstance(menu).AsSingle();
        Container.Bind<IGameOverView>().To<GameOverView>().FromInstance(gameOver).AsSingle();
    }
    private void InstallEnemies()
    {
        Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle();

        Container.BindFactory<EnemyBlackController, EnemyBlackController.Factory>()
                 .FromSubContainerResolve()
                 .ByNewPrefabMethod(enemyBlack, InstallEnemyBlack)
                 .UnderTransformGroup("EnemiesContainer");
        Container.BindFactory<EnemyBlueController, EnemyBlueController.Factory>()
                 .FromSubContainerResolve()
                 .ByNewPrefabMethod(enemyBlue, InstallEnemyBlue)
                 .UnderTransformGroup("EnemiesContainer")
                 .AsTransient();
        Container.BindFactory<EnemyRedController, EnemyRedController.Factory>()
                 .FromSubContainerResolve()
                 .ByNewPrefabMethod(enemyRed, InstallEnemyRed)
                 .UnderTransformGroup("EnemiesContainer")
                 ;

    }

    private void InstallEnemyRed(DiContainer subContainer)
    {
        subContainer.BindFactory<EnemyRedController, EnemyRedController.Factory>();
    }

    private void InstallEnemyBlue(DiContainer subContainer)
    {
        subContainer.BindFactory<EnemyBlueController, EnemyBlueController.Factory>();
    }

    private void InstallEnemyBlack(DiContainer subContainer)
    {
        subContainer.BindFactory<EnemyBlackController, EnemyBlackController.Factory>();
    }
}
