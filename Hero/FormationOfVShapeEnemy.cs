using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{    
    internal class FormationOfVShapeEnemy:EnemyFormation
    {
        public enum FormationSize
        {
            Three,
            Five,
            Seven,
        }
        
        //private float _flyRangeY;
        //private const int _rotationAngle = 180;
       // private DoubleLinkedList<bool> _isRotatedOfAllAircrafts = new DoubleLinkedList<bool>();
        private PrizeType _prizeType;
        public FormationOfVShapeEnemy(FormationSize formationSize, int numOfAircraftWillFire,
            float fireFrequency, AircraftType aircraftType = AircraftType.AircraftOfGoldenEagle, PrizeType prizeTypeAfterAllDestoyed = PrizeType.None) : base(ValidateParameterOfFormationSize(formationSize), numOfAircraftWillFire, fireFrequency)
        {
            _formationType = FormationType.VShape;
            //_flyRangeY = window.Size.Y / 5f * 2;
            _prizeType = prizeTypeAfterAllDestoyed;
           
            CreateFormation(aircraftType);
        }
        private static int ValidateParameterOfFormationSize(FormationSize formationSize)
        {
            int size = 3;
             switch (formationSize)
            {
                case FormationSize.Five:
                    size = 5;
                    break;
                case FormationSize.Seven:
                    size = 7; 
                    break;
            }

            return size;
        }
        protected override void CreateFormation(AircraftType aircraftType)
        {
            Vector2f prefferedSpriteSize;
            switch (aircraftType)
            {
                case AircraftType.AircraftOfHarpyEagle:
                    prefferedSpriteSize = AircraftOfHarpyEagle.PrefferedAircraftSize;
                    break;
                case AircraftType.AircraftOfGoldenEagle:
                    prefferedSpriteSize = AircraftOfGoldenEagle.PrefferedAircraftSize;
                    break;
                default:
                    prefferedSpriteSize = AircraftOfGoldenEagle.PrefferedAircraftSize;
                    break;
            }
            float randomMin = 2 * prefferedSpriteSize.X /*+ (_numOfAircraftsInFormation / 2) * prefferedSpriteSize.X*/;
            float randomMax = Game.WindowSize.X - 2 * prefferedSpriteSize.X - _numOfAircraftsInFormation * prefferedSpriteSize.X;
            if (randomMax <= 0)
            {
                //This will never happen except that the aircraft is too large
                throw new ArgumentOutOfRangeException("aircrafts are too many or too large.");
            }

            float xPosForLeftMost = Game.Random.Next(Convert.ToInt32(randomMin), Convert.ToInt32(randomMax));
            float yPos = 0 - ((_numOfAircraftsInFormation + 1) / 2 + 1)* prefferedSpriteSize.Y;
            float yStep = prefferedSpriteSize.Y;
            for (int i = 0; i < _numOfAircraftsInFormation; i++)
            {
                if (i == (_numOfAircraftsInFormation + 1) / 2)
                    yStep *= -1;
                yPos += yStep;

                CreateAircraft(aircraftType, new Vector2f(xPosForLeftMost + i * prefferedSpriteSize.X, yPos), prefferedSpriteSize);

                //_isRotatedOfAllAircrafts.AddLast(false);
            }
        }
        private void CreateAircraft(AircraftType aircraftType, Vector2f aircraftPos, Vector2f prefferedAircraftSize)
        {
            Vector2f speed = new Vector2f(0, 2f);
            AircraftOfEnemy aircraft;
            switch (aircraftType)
            {
                case AircraftType.AircraftOfHarpyEagle:
                    aircraft = new AircraftOfHarpyEagle(2, speed, true);
                    break;
                case AircraftType.AircraftOfGoldenEagle:
                    aircraft = new AircraftOfGoldenEagle(1, speed, true);
                    break;
                default:
                    aircraft = new AircraftOfGoldenEagle(1, speed, true);
                    break;
            }
            aircraft.Sprite.Position = aircraftPos;

            _aircraftList.AddLast(aircraft);
        }

        private bool CheckIfOutOfRenderWindow(AircraftOfEnemy aircraft, int index)
        {
            //if the aircraft has never rotated, then it should be valid even in out of the window
            /*            if (_isRotatedOfAllAircrafts[index] == false)
                            return false;
                        FloatRect rectWindow = new FloatRect(0, 0, _window.Size.X, _window.Size.Y);
                        if (rectWindow.Intersects(aircraft.Sprite.GetGlobalBounds()))
                            return false;*/

            if (aircraft.Sprite.GetGlobalBounds().Top < Game.WindowSize.Y)
                return false;

            return true;
        }
        public override void Update(Player player)
        {
            AircraftOfEnemy aircraft = null;
            for (int i = 0; i < _aircraftList.Count;)
            {
                _aircraftList[i].Update();

                bool shouldDestroy = false;
                if(CheckIfDestroyedByWeapon(_aircraftList[i]) || CheckIfCollidedByPlayer(_aircraftList[i], player))
                {
                    shouldDestroy = true;
                    ManagerOfAnimation.Add(new Animation(TextureManager.ExplosionAnimation, _aircraftList[i].Sprite.Position));
                }
                if (shouldDestroy || CheckIfOutOfRenderWindow(_aircraftList[i], i))
                {
                    aircraft = _aircraftList[i];
                    DestroyAircraft(i);
                    //Console.WriteLine($"count--VShape: {_aircraftList.Count}");

                    continue;
                }

                ++i;
            }
            if ((_prizeType != PrizeType.None) && (_numOfAircraftDestroyedByPlayer == _numOfAircraftsInFormation) && (aircraft != null))
            {

                GeneratePrize(aircraft.Sprite.Position);
            }

            FloatRect playerBounds = player.Sprite.GetGlobalBounds();
            Vector2f targetPos = new Vector2f(playerBounds.Left + playerBounds.Width / 2f, playerBounds.Top + playerBounds.Height / 2f);
            CoordinateFirepower(targetPos);
        }
        private void DestroyAircraft(int index)
        {
            _aircraftList.RemoveAt(index);
           // _isRotatedOfAllAircrafts.RemoveAt(index);
        }

        private void GeneratePrize(Vector2f prizePos)
        {
            PrizeType prizeType = PrizeType.None;
            switch (_prizeType)
            {
                case PrizeType.DoubleProjectile:
                    prizeType = PrizeType.DoubleProjectile;
                    break;
                case PrizeType.FourProjectile:
                    prizeType= PrizeType.FourProjectile;
                    break;
                default:
                    return;
            }
            Prize prize = new Prize(prizeType);
            prize.Position = prizePos;

            ManagerOfPrize.PrizeList.AddLast(prize);

            Console.WriteLine("prize count: " + ManagerOfPrize.PrizeList.Count.ToString());
        }
    }
}
