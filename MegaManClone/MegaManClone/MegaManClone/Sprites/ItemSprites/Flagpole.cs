using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaManClone.Stages;

namespace MegaManClone.Sprites.ItemSprites
{
    class Flagpole : Sprite

    {
        #region Properties

        Megaman megaman;
        Stage stage;
        int revealSpeed = -150;
        SoundEffect sound;
        static int flagpoleHeight = 288; // change according to sprite specifications
        int pointMultiplier = 2000; 

        #endregion

        #region Constructor

        public Flagpole(ContentManager content, Vector2 position)
            : base(new Rectangle(0, 0, 32, flagpoleHeight), new Rectangle(0, 0, 32, flagpoleHeight), 0, position, content.Load<Texture2D>("Items/teleporter"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/Teleport1");
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (otherObject is Megaman && active)
            {

                sound.Play();
                megaman = otherObject as Megaman;
                stage = megaman.CurrentStage;

                // todo - call stage end, trigger cutscene
                megaman.BlockMovement(this);
                int megamanHeight = otherObject.AABB.Top;
                megaman.Points += (int) (pointMultiplier * megamanHeight / flagpoleHeight);

                stage.Complete();
                
                active = false;
                

            }
        }

        #endregion
    }
}
