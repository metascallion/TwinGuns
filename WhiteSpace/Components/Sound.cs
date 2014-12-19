using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Codecs;
using CSCore;
using Microsoft.Xna.Framework;
using WhiteSpace.GameLoop;
using WhiteSpace;

namespace WhiteSpace.Components
{
    public class Sound<StateType> : Updateable<StateType> where StateType : struct
    {
        private bool loop = false;
        private IWaveSource source;
        private ISoundOut wasout;
        private float elapsedTime = 0;

        public float Volume
        {
            private get { return this.wasout.Volume; }
            set { this.wasout.Volume = value; }
        }

        public Sound(string fileName, bool loop, Updater<StateType> updaterToRegisterTo) : base(updaterToRegisterTo)
        {
            this.loop = loop;
            this.source = CodecFactory.Instance.GetCodec("Content/" + fileName + ".wav");
            wasout = new WasapiOut();
            wasout.Initialize(source);
            play();
        }

        public void play()
        {
            wasout.Play();
        }

        public void stop()
        {
            this.source.Position = 0;
            this.wasout.Stop();
        }

        public void pause()
        {
            this.wasout.Stop();
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (loop)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                float t = wasout.WaveSource.GetLength().Seconds;
                if (elapsedTime / 1000 >= t - 15)
                {
                    elapsedTime = 0;
                    this.source.Position = 0;
                }
            }
        }
    }
}
