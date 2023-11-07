using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class FormationOfTest:EnemyFormation
    {
        private float _flyRangeY;
        public FormationOfTest(int numOfAircraftsInFormation, int numOfAircraftWillFire,
    float fireFrequency, float rotationAngleEachTime = 2f, AircraftType aircraftType = AircraftType.AircraftOfHarpyEagle) : base(numOfAircraftsInFormation, numOfAircraftWillFire, fireFrequency)
        {
            _formationType = FormationType.HorizonShape;
            _flyRangeY = Game.WindowSize.Y / 3f;
          //  _rotationAngleEachTime = rotationAngleEachTime;

            CreateFormation(aircraftType);
        }
        protected override void CreateFormation(AircraftType aircraftType)
        {
            Vector2f prefferedSpriteSize = new Vector2f(50, 60);
            float perSegmentWidth = (Game.WindowSize.X - 2 * prefferedSpriteSize.X) / _numOfAircraftsInFormation;
            float randomMax = perSegmentWidth - prefferedSpriteSize.X;
            if (randomMax <= 0)
            {
                throw new ArgumentOutOfRangeException("number of aircraft is too many.");
            }
            float xPos;
            for (int i = 0; i < _numOfAircraftsInFormation; i++)
            {
                float span = Game.Random.Next(0, Convert.ToInt32(randomMax));
                xPos = i * perSegmentWidth + span + prefferedSpriteSize.X;
                // Console.WriteLine("randomMax: " + randomMax.ToString() + "  Xpos:" + xPos.ToString());
                Vector2f aircraftPos = new Vector2f(xPos, -prefferedSpriteSize.Y / 2f + 10);//The origin is center of the aircraft, so set it negative value
                Vector2f speed = new Vector2f(0, 2f);
                AircraftOfEnemy aircraft = new AircraftOfEnemyTest(100, speed, true);
                aircraft.Sprite.Position = aircraftPos;
/*                aircraft.Sprite.Texture = TextureManager.EnemyJet;
                aircraft.Sprite.Scale = new Vector2f(prefferedSpriteSize.X / TextureManager.Enemy.Size.X, prefferedSpriteSize.Y / TextureManager.Enemy.Size.Y);
                aircraft.Sprite.Origin = new Vector2f(aircraft.Sprite.GetLocalBounds().Width / 2f, aircraft.Sprite.GetLocalBounds().Height / 2f);*/

                _aircraftList.AddLast(aircraft);

/*                if (aircraftPos.X <= _window.Size.X / 2f)
                {
                    _rotationOfAllAircrafts.AddLast(-_rotationAngleEachTime);
                }
                else
                {
                    _rotationOfAllAircrafts.AddLast(_rotationAngleEachTime);
                }*/
            }
        }

        public override void Update(Player player)
        {
            for (int i = 0; i < _aircraftList.Count;)
            {
                if (_aircraftList[i].Sprite.Position.Y >= _flyRangeY)
                {
                    Redirect(_aircraftList[i], i);
                    //    Console.WriteLine($"PositionY: {_aircraftList[i].Position.Y}  flyRangeY: {_flyRangeY}   bounds: {_aircraftList[i].Sprite.GetGlobalBounds().ToString()}");
                }

                _aircraftList[i].Update();

                if (CheckIfDestroyedByWeapon(_aircraftList[i]) || CheckIfOutOfRenderWindow(_aircraftList[i]) || CheckIfCollidedByPlayer(_aircraftList[i], player))
                {
                    DestroyAircraft(i);
                    //Console.WriteLine($"count of horizon aircraft: {_aircraftList.Count}");

                    continue;
                }

                ++i;
            }

            FloatRect playerBounds = player.Sprite.GetGlobalBounds();
            Vector2f targetPos = new Vector2f(playerBounds.Left + playerBounds.Width / 2f, playerBounds.Top + playerBounds.Height / 2f);
            CoordinateFirepower(targetPos);
        }
        private void Redirect(AircraftOfEnemy aircraft, int index)
        {
            aircraft.Sprite.Position = new Vector2f(aircraft.Sprite.Position.X, aircraft.Sprite.Position.Y);
            aircraft.Speed = new Vector2f(0, 0);
        }
        private bool CheckIfOutOfRenderWindow(AircraftOfEnemy aircraft)
        {
            return false;
        }

        private void DestroyAircraft(int index)
        {
            _aircraftList.RemoveAt(index);
        }

    }
}
