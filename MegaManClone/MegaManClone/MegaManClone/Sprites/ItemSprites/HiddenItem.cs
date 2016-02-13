using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.ItemSprites
{
    abstract class HiddenItem : Sprite
    {
        #region Fields
        protected Megaman megaman;
        Block block;
        SoundEffect sound;

        #endregion

        #region Constructor

        public HiddenItem(Rectangle AABB, Block block, Rectangle frame, int millisecondsPerFrame, Texture2D texture, SoundEffect sound)
            : base (AABB, frame, millisecondsPerFrame, Vector2.Zero, texture)
        {
            this.block = block;
            this.sound = sound;

            active = false;

            maxVelocity = new Vector2(100, 150);
            minVelocity = new Vector2(-100, -150);
        }

        #endregion

        #region Methods

        protected abstract void collect(Megaman megaman);

        public virtual void Reveal(Megaman megaman)
        {
            this.megaman = megaman;
            active = true;
            position = new Vector2(block.CurrentSprite.Position.X, block.CurrentSprite.Position.Y - 32);
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (active && otherObject is Megaman)
            {
                sound.Play();
                collect(otherObject as Megaman);
                Reset();
            }
        }

        #endregion

        #region ISprite Override

        public override void Reset()
        {
            active = false;
            position = Vector2.Zero;
        }

        #endregion
    }
}
