using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class AudioPlayer
    {
        private Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public void LoadContent(ContentManager content)
        {
            // Songs
            AddSongs(content.Load<Song>("Sound/Song/Song_Adventure"), "Song_Adventure");

            // Sound Effects
            AddSongs(content.Load<Song>("Sound/Song/SoundEffect/Enemy/SoundEffect_Goblin"), "SoundEffect_Goblin");

            AddSongs(content.Load<Song>("Sound/Song/SoundEffect/Player/SoundEffect_Hit"), "SoundEffect_Hit");
            AddSongs(content.Load<Song>("Sound/Song/SoundEffect/Player/SoundEffect_level_up"), "SoundEffect_level_up");
            AddSongs(content.Load<Song>("Sound/Song/SoundEffect/Player/SoundEffect_sword_swing"), "SoundEffect_sword_swing");
        }


        private void AddSoundEffects(SoundEffect soundEffect, string name)
        {
            soundEffects.Add(name, soundEffect);
        }

        private void AddSongs(Song song, string name)
        {
            songs.Add(name, song);
        }

        public void Song_Play(string name)
        {
            Song tmp = songs[name];
            MediaPlayer.Play(tmp);
        }

        public void SoundEffect_Play(string name)
        {
            SoundEffect tmp = soundEffects[name];
            tmp.Play();
        }
    }
}
