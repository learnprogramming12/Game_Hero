using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    enum DifficultyLevel
    {
        Level1 = 1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        NoMore,
    }
    internal class ManagerOfPassingLevels
    {
        //it can allow a combination of different or same enemy formation in order to enter a higher diffficulty level.
        private int _prefferedNumOfEnemyFormation = 1;
        public DoubleLinkedList<EnemyFormation> _listOfEnemyFormation;
      //  private Random _random;//used for creating different type of enemy formation randomly.
        private DifficultyLevel _difficultyLevel = DifficultyLevel.Level1;
        private const int _conditionOfIncreaseDifficulty = 10;//This means how many waves of attack that each difficulty level has.
        private int _countForWavesOfAttack = 0;
        private int _countForEachWave = 0;
        RenderWindow _renderWindow;
        bool _bossAppeared = false;
        public ManagerOfPassingLevels(RenderWindow window)
        {
            _renderWindow = window;
            _listOfEnemyFormation = new DoubleLinkedList<EnemyFormation>();
        }
        public void init()
        {
            _prefferedNumOfEnemyFormation = 1;
            _difficultyLevel = DifficultyLevel.Level1;
            _countForWavesOfAttack = 0;
            _countForEachWave = 0;
            _bossAppeared = false;
            _listOfEnemyFormation.Clear();
        }
        //This is not a reasonably method of passing levels. Based on current two formations, maybe it can be much more reasonable to
        //adust some parameters. Or develop other types of formation or fighter aircraft with different lifepoints
        //or weapons with different damage power.
        private void CreateEnemyFormation(Player player)
        {
            for(int i = 0; i < _listOfEnemyFormation.Count;)
            {
                if (_listOfEnemyFormation[i].AreAllAircraftsDestroyed() == true)
                    _listOfEnemyFormation.Remove(_listOfEnemyFormation[i]);
                else
                    ++i;
            }
            if (_listOfEnemyFormation.Count >= _prefferedNumOfEnemyFormation)
                return;

            //here can not use list.count in a for loop condition whereas the list is changed in for loop body, because this will change the for loop condition
            int currentFormationCount = _listOfEnemyFormation.Count;
            DifficultyLevel level = DifficultyLevel.Level1;
            for (int i = 0; i < _prefferedNumOfEnemyFormation - currentFormationCount; i++)
            {
                float rotationAngle = Game.Random.Next(1, 4) * 0.7f;
                int formationSelection = Game.Random.Next(0, 2);
                int aircraftSelection = Game.Random.Next(0, 3);
                switch (_difficultyLevel)
                {
                    case DifficultyLevel.Level1:
                        level = DifficultyLevel.Level2;
                        if(aircraftSelection == 0)
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(3, 1, 0.5f, 0, AircraftType.AircraftOfRotation));
                        else
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(2, 1, 0.5f, rotationAngle, AircraftType.AircraftOfGoldenEagle));

                        break;
                    case DifficultyLevel.Level2:
                        level = DifficultyLevel.Level3;
                        if (aircraftSelection == 0)
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(3, 2, 0.5f, rotationAngle, AircraftType.AircraftOfGoldenEagle));
                        else if(aircraftSelection == 1)
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(4, 0, 0.5f, 0, AircraftType.AircraftOfRotation));
                        else
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(2, 1, 0.5f, 0, AircraftType.AircraftOfHarpyEagle));
                        break;
                    case DifficultyLevel.Level3:
                        level = DifficultyLevel.Level4;
                        if(aircraftSelection == 0)
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(3, 2, 0.3f, 0, AircraftType.AircraftOfHarpyEagle));
                        }
                        else
                        {
                            //it's time to let player upgrade the weapon system.
                            PrizeType prizeChance1 = PrizeType.None;
                            if (player.getWeaponType() == WeaponSystem.FirepowerIntensity.SingleMissle)
                                prizeChance1 = PrizeType.DoubleProjectile;

                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Three, 3, 0.5f, AircraftType.AircraftOfGoldenEagle, prizeChance1));
                        }
                        break;
                    case DifficultyLevel.Level4:
                        level = DifficultyLevel.Level5;
                        if (aircraftSelection == 0)
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(3, 3, 0.5f, 0, AircraftType.AircraftOfHoneyBadger));
                        }
                        else
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Five, 3, 0.5f, AircraftType.AircraftOfHarpyEagle));
                        }
                        break;
                    case DifficultyLevel.Level5:
                        level = DifficultyLevel.Level6;
                        //_prefferedNumOfEnemyFormation = 3;
                        if (aircraftSelection == 0)
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(4, 3, 0.5f, 0, AircraftType.AircraftOfHoneyBadger));
                        }
                        else
                        {
                            //it's time to let player upgrade the weapon system.
                            PrizeType prizeChance2 = PrizeType.None;
                            if (player.getWeaponType() == WeaponSystem.FirepowerIntensity.SingleMissle || player.getWeaponType() == WeaponSystem.FirepowerIntensity.DoubleMissle)
                                prizeChance2 = PrizeType.FourProjectile;

                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Five, 4, 0.5f, AircraftType.AircraftOfHarpyEagle, prizeChance2));
                        }
                        break;
                    case DifficultyLevel.Level6:
                        level = DifficultyLevel.Level7;
                        //last chance for player to upgrade the weapon system.
                        PrizeType prize = PrizeType.None;
                        if (player.getWeaponType() == WeaponSystem.FirepowerIntensity.SingleMissle || player.getWeaponType() == WeaponSystem.FirepowerIntensity.DoubleMissle)
                            prize = PrizeType.FourProjectile;
                        _prefferedNumOfEnemyFormation = 3;
                        if (aircraftSelection == 0)
                        {
                            //size = FormationOfVShapeEnemy.FormationSize.Five;
                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Five, 4, 0.5f, AircraftType.AircraftOfHarpyEagle, prize));
                        }
                        else if(aircraftSelection == 1)
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(4, 4, 0.5f, 0, AircraftType.AircraftOfHoneyBadger));
                        }
                        else
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(4, 0, 0.5f, 0, AircraftType.AircraftOfRotation));
                        }
                        break;
                    case DifficultyLevel.Level7:
                        level = DifficultyLevel.Level8;
                        if (aircraftSelection == 0)
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(4, 4, 0.5f, 0, AircraftType.AircraftOfHoneyBadger));
                        }
                        else
                        {
                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Seven, 5, 0.5f, AircraftType.AircraftOfHarpyEagle));
                        }
                        break;
                    case DifficultyLevel.Level8:
                        level = DifficultyLevel.Level8;
                        if (_bossAppeared == false)
                        {
                            _bossAppeared = true;
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(1, 1, 0.5f, 0, AircraftType.AircraftOfBoss));
                        }
                        if (formationSelection == 0)
                            _listOfEnemyFormation.AddLast(new FormationOfHorizonShapeEnemy(5, 5, 0.5f, 0, AircraftType.AircraftOfHoneyBadger));
                        else
                            _listOfEnemyFormation.AddLast(new FormationOfVShapeEnemy(FormationOfVShapeEnemy.FormationSize.Seven, 5, 0.5f, AircraftType.AircraftOfHarpyEagle));
                        break;
/*                    case DifficultyLevel.NoMore:
                        Player.PlayerState = PlayerState.Win;
                        break;*/

                }
                _countForEachWave++;
                if(_countForEachWave >= _prefferedNumOfEnemyFormation)
                {
                    //it's time to be regarded as an attack wave
                    _countForWavesOfAttack++;
                    _countForEachWave = 0;
                }
                if (_difficultyLevel == DifficultyLevel.Level8)
                {
                    bool bBossAlive = false;
                    for (int j = 0; j < _listOfEnemyFormation.Count; j++)
                    {
                        if (_listOfEnemyFormation[j].AircraftType == AircraftType.AircraftOfBoss)
                        {
                            bBossAlive = true;
                            break;
                        }
                    }
                    if (bBossAlive == false)
                    {
                        Player.PlayerState = PlayerState.Win;
                        break;
                    }
                    else
                    {//As long as the boss is alive, the attack will continue
                        continue;
                    }
                }
                if (_countForWavesOfAttack >= _conditionOfIncreaseDifficulty)
                {
                    _difficultyLevel = level;
                    //it's time to enter the next difficulty level
                    _countForWavesOfAttack = 0;
                    break;
                }
            }
        }

        public void Update(Player player)
        {
            if (Player.PlayerState == PlayerState.InBattle)
                CreateEnemyFormation(player);
            else
                return;

            for(int i = 0; i < _listOfEnemyFormation.Count; i++)
            {
                _listOfEnemyFormation[i].Update(player);
            }
        }

        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < _listOfEnemyFormation.Count; i++)
            {
                _listOfEnemyFormation[i].Draw(window);
            }
        }
    }
}
