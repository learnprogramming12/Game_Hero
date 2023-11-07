using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class FormationOfSlashShapeEnemy:EnemyFormation
    {
        public FormationOfSlashShapeEnemy(int numOfAircraftsInFormation, int numOfAircraftWillFire,
            float fireFrequency, AircraftType aircraftType = AircraftType.AircraftOfGoldenEagle) : base(numOfAircraftsInFormation, numOfAircraftWillFire, fireFrequency)
        {
            _formationType = FormationType.SlashShape;

        }
        protected override void CreateFormation(AircraftType aircraftType)
        {

        }
        public override void Update(Player player)
        {
         //   Console.WriteLine("slash enemy update");
        }
    }
}
