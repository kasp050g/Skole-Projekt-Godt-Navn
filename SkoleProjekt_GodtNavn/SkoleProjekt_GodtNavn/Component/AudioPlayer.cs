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
            AddSoundEffects(content.Load<SoundEffect>("Sound/SoundEffect/Enemy/SoundEffect_Goblin"), "SoundEffect_Goblin");
            AddSoundEffects(content.Load<SoundEffect>("Sound/SoundEffect/Enemy/golem_death"), "golem_death");

            AddSoundEffects(content.Load<SoundEffect>("Sound/SoundEffect/Player/SoundEffect_Hit"), "SoundEffect_Hit");
            AddSoundEffects(content.Load<SoundEffect>("Sound/SoundEffect/Player/SoundEffect_level_up"), "SoundEffect_level_up");
            AddSoundEffects(content.Load<SoundEffect>("Sound/SoundEffect/Player/SoundEffect_sword_swing"), "SoundEffect_sword_swing");
        }

        private void AddSoundEffects(SoundEffect soundEffect, string name)
        {
            soundEffects.Add(name, soundEffect);
        }

        private void AddSongs(Song song, string name)
        {
            songs.Add(name, song);
        }

        public void Song_Play(string name,float volume)
        {
            Song tmp = songs[name];
            
            MediaPlayer.Play(tmp);
            MediaPlayer.Volume = volume;
            MediaPlayer.IsRepeating = true;
        }

        public void SoundEffect_Play(string name,float timeDelay)
        {
            AudioContainer tmp = new AudioContainer();
            tmp.name = name;
            tmp.timeDelay = timeDelay;

            audioContainers.Add(tmp);
        }
        private void SoundEffect(string name)
        {
            SoundEffect tmp = soundEffects[name];
            tmp.Play();
        }

        List<AudioContainer> audioContainers = new List<AudioContainer>();
        List<AudioContainer> removeList = new List<AudioContainer>();


        public void allSound(GameTime gameTime)
        {
            foreach (AudioContainer x in audioContainers)
            {
                x.timeDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (x.timeDelay <= 0)
                {
                    SoundEffect(x.name);
                    removeList.Add(x);
                }
            }

            foreach (AudioContainer x in removeList)
            {
                audioContainers.Remove(x);
            }
        }
    }
}
