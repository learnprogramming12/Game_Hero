using SFML;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class TextureManager
    {
        static string _imagePath = "../../resources/images/";

        static Texture _textureBackground;
        static Texture _texturePlayer;
        static Texture _textureMissle;
        static Texture _textureBullet;
        static Texture _textureBomb;
        static Texture _textureEnemy;
        static Texture _textureDoubleMissles;
        static Texture _textureFourMissles;
        static Texture _textureEnemyMissle2;
        static Texture _textureEnemyJet;
        static Texture _textureEnemyRotation;
        static Texture _textureEnemyHoneyBadger;
        static Texture _textureBoss;
        static Texture _textureMissleLauncher;
        static Texture _textureCannon;
        static Texture _textureBossMissile;
        static Texture _textureBossBomb;
        static DoubleLinkedList<Texture> _textureExplosionAnimation = new DoubleLinkedList<Texture>();

        public static Texture Background{ get { return _textureBackground; } }
        public static Texture Player{ get { return _texturePlayer; } }
        public static Texture Missile { get { return _textureMissle; } }
        public static Texture Bullet { get { return _textureBullet; } }
        public static Texture Bomb { get { return _textureBomb; } }
        public static Texture Enemy { get { return _textureEnemy; } }
        public static Texture DoubleMissles { get { return _textureDoubleMissles; } }
        public static Texture FourMissles { get { return _textureFourMissles; } }
        public static Texture EnemyMissle2 { get { return _textureEnemyMissle2; } }
        public static Texture EnemyJet{ get { return _textureEnemyJet; } }
        public static Texture EnemyRotation { get { return _textureEnemyRotation; } }
        public static Texture EnemyHoneyBadger { get { return _textureEnemyHoneyBadger; } }
        public static Texture Boss { get { return _textureBoss; } }
        public static Texture MissleLauncher { get { return _textureMissleLauncher; } }
        public static Texture Cannon { get { return _textureCannon; } }
        public static Texture BossMissle { get { return _textureBossMissile; } }
        public static Texture BossBomb { get { return _textureBossBomb; } }

        public static DoubleLinkedList<Texture> ExplosionAnimation { get { return _textureExplosionAnimation; } }
        public static void Load()
        {
            _textureBackground = new Texture(_imagePath + "sky.jpg");
            _texturePlayer = new Texture(_imagePath + "PlayerJet.png");
            _textureMissle = new Texture(_imagePath + "missile.png");
            _textureBullet = new Texture(_imagePath + "bullet.png");
            _textureBomb = new Texture(_imagePath + "bomb.png");
            _textureEnemy = new Texture(_imagePath + "enemy.png");
            _textureDoubleMissles = new Texture(_imagePath + "doublemissiles.png");
            _textureFourMissles = new Texture(_imagePath + "fourmissles.png");
            _textureEnemyMissle2 = new Texture(_imagePath + "enemyMissle2.png");
            _textureEnemyJet = new Texture(_imagePath + "enemyjet.png");
            _textureEnemyRotation = new Texture(_imagePath + "enemyRotation.png");
            _textureEnemyHoneyBadger = new Texture(_imagePath + "HoneyBadger.png");
            _textureBoss = new Texture(_imagePath + "Boss.png");
            _textureMissleLauncher = new Texture(_imagePath + "MissleLauncher.png");
            _textureCannon = new Texture(_imagePath + "Cannon.png");
            _textureBossMissile = new Texture(_imagePath + "BossMissile.png");
            _textureBossBomb = new Texture(_imagePath + "BossBomb.png");

            for (int i = 0; i < 8; i++)
            {
                _textureExplosionAnimation.AddLast(new Texture(_imagePath + "explosions/explosion-" + i.ToString() + ".png"));
            }

        }
    }
}
