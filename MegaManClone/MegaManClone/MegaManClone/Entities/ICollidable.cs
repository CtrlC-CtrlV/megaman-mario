using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities
{
    interface ICollidable
    {
        Rectangle AABB
        {
            get;
        }

        bool Active
        {
            get;
        }

        Vector2 Velocity
        {
            get;
        }

        void BlockMovement(ICollidable otherObject);
        
        void Collide(ICollidable otherObject);
        
        void TakeDamage(ICollidable otherObject, int Damage);
        
        void TriggerFall();
    }
}
