using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class CruiseMissile: Projectile       
    {
        AircraftOfEnemy _attackTarget;
        public CruiseMissile(AircraftOfEnemy attackTarget, Vector2f position, int damagePower, Vector2f speed):base(position, damagePower, speed)
        {
            _attackTarget = attackTarget;
        }
        public override void Update()
        {
            /*            if (_valid == false)
                            return;
                        if(_attackTarget != null && _attackTarget.LifePoints <= 0)
                        { 
                            //the target has been destroyed
                            _attackTarget = null;
                        }
                        //calculate and chase the enemy aircraft
                        if(_attackTarget != null)
                        {
                            _speed = Tools.GetVectorSpeed(_speed, _sprite.Position, _attackTarget.Position);
                            _sprite.Rotation = Tools.GetDegree(_sprite.Position, _attackTarget.Position) + 90;
                        }

                        base.Update(window);*/
            if (_valid == false)
                return;
            //calculate and chase the enemy aircraft
            if (_attackTarget != null)
            {
                _speed = Tools.GetVectorSpeed(_speed, _sprite.Position, _attackTarget.Sprite.Position);
                _sprite.Rotation = Tools.GetDegree(_sprite.Position, _attackTarget.Sprite.Position) + 90;
            }

            base.Update();
        }
    }
}
