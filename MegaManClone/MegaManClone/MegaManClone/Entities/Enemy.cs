using MegaManClone.Sprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities
{
    class Enemy : ISprite, ICollidable
    {
        #region Fields

        bool active = true;

        #endregion

        #region Properties

        public Rectangle AABB
        {
            get
            {
                return new Rectangle();
            }
        }

        public bool Active
        {
            get { return active; }
        }

        public Vector2 Velocity
        {
            get
            {
                return new Vector2();
            }
        }

        #endregion

        #region Constructor

        public Enemy()
        {

        }

        #endregion

        #region Methods

        #endregion

        #region ICollidable Implementation

        public void BlockMovement(ICollidable otherObject)
        {

        }

        public void Collide(ICollidable otherObject)
        {

        }

        public void TakeDamage(ICollidable otherObject, int Damage)
        {

        }

        public void TriggerFall()
        {

        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {

        }

        public void Reset()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
