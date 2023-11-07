using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class ManagerOfMusic
    {
        /*        static string _audioPath = "../../resources/audio/";
        */
        static string _audioPath = "../../resources/audio/";

  //      static Sound _background;
        static Sound _missle;
        static Sound _explosion;
        static Sound _prize;


      //  public static Sound Background { get { return _background; } }
        public static Sound Missle { get { return _missle; } }
        public static Sound Explosion { get { return _explosion; } }
        public static Sound Prize { get { return _prize; } }
        static Music music;
        public static void Load()
        {
            SoundBuffer soundBuffer1 = new SoundBuffer(_audioPath + "prize.wav");
            _prize = new Sound(soundBuffer1);

            SoundBuffer soundBuffer2 = new SoundBuffer(_audioPath + "missle.wav");
            _missle = new Sound(soundBuffer2);

            SoundBuffer soundBuffer3 = new SoundBuffer(_audioPath + "explosion.wav");
            _explosion = new Sound(soundBuffer3);


        }
    }
}
