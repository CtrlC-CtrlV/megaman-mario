using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Stages
{
    class CollisionHelper
    {
        readonly static double projectionOffset = 0.04;

        public static CollisionSide GetCollisionSide(ICollidable objectA, ICollidable objectB)
        {
            Rectangle AABBA = objectA.AABB,
                tempAABBA,
                AABBB = objectB.AABB,
                tempAABBB;
            Vector2 velocityA = objectA.Velocity,
                velocityB = objectB.Velocity;
            CollisionSide collisionSide = CollisionSide.None;
            double collisionTime, earliestCollisionTime = Double.PositiveInfinity;

            collisionTime = (double)(AABBA.Top - AABBB.Bottom) / (double)(velocityB.Y - velocityA.Y) + projectionOffset;
            tempAABBA = AABBA;
            tempAABBB = AABBB;
            tempAABBA.X += (int)(velocityA.X * collisionTime);
            tempAABBA.Y += (int)(velocityA.Y * collisionTime);
            tempAABBB.X += (int)(velocityB.X * collisionTime);
            tempAABBB.Y += (int)(velocityB.Y * collisionTime);
            if (tempAABBA.Intersects(tempAABBB) &&
                collisionTime < earliestCollisionTime)
            {
                if (velocityA.Y != 0 || velocityB.Y != 0)
                {
                    collisionSide = CollisionSide.Top;
                }
            }

            collisionTime = (double)(AABBA.Left - AABBB.Right) / (double)(velocityB.X - velocityA.X) + projectionOffset;
            tempAABBA = AABBA;
            tempAABBB = AABBB;
            tempAABBA.X += (int)(velocityA.X * collisionTime);
            tempAABBA.Y += (int)(velocityA.Y * collisionTime);
            tempAABBB.X += (int)(velocityB.X * collisionTime);
            tempAABBB.Y += (int)(velocityB.Y * collisionTime);
            if (tempAABBA.Intersects(tempAABBB) &&
                collisionTime < earliestCollisionTime)
            {
                if (velocityA.X != 0 || velocityB.X != 0)
                {
                    collisionSide = CollisionSide.Left;
                    earliestCollisionTime = collisionTime;
                }
            }

            collisionTime = (double)(AABBA.Right - AABBB.Left) / (double)(velocityB.X - velocityA.X) + projectionOffset;
            tempAABBA = AABBA;
            tempAABBB = AABBB;
            tempAABBA.X += (int)(velocityA.X * collisionTime);
            tempAABBA.Y += (int)(velocityA.Y * collisionTime);
            tempAABBB.X += (int)(velocityB.X * collisionTime);
            tempAABBB.Y += (int)(velocityB.Y * collisionTime);
            if (tempAABBA.Intersects(tempAABBB) &&
                collisionTime < earliestCollisionTime)
            {
                if (velocityA.X != 0 || velocityB.X != 0)
                {
                    collisionSide = CollisionSide.Right;
                    earliestCollisionTime = collisionTime;
                }
            }

            collisionTime = (double)(AABBA.Bottom - AABBB.Top) / (double)(velocityB.Y - velocityA.Y) + projectionOffset;
            tempAABBA = AABBA;
            tempAABBB = AABBB;
            tempAABBA.X += (int)(velocityA.X * collisionTime);
            tempAABBA.Y += (int)(velocityA.Y * collisionTime);
            tempAABBB.X += (int)(velocityB.X * collisionTime);
            tempAABBB.Y += (int)(velocityB.Y * collisionTime);
            if (tempAABBA.Intersects(tempAABBB) &&
                collisionTime < earliestCollisionTime)
            {
                if (velocityA.Y != 0 || velocityB.Y != 0)
                {
                    collisionSide = CollisionSide.Bottom;
                    earliestCollisionTime = collisionTime;
                }
            }

            return collisionSide;
        }

        public static bool sweptShapeTest(ICollidable objectA, ICollidable objectB, double projectionTime)
        {
            Rectangle originalAABBA = objectA.AABB,
                originalAABBB = objectA.AABB,
                projectedAABBA = originalAABBA,
                projectedAABBB = originalAABBB,
                sweptShapeA = new Rectangle(),
                sweptShapeB = new Rectangle();
            Vector2 velocityA = objectA.Velocity;
            Vector2 velocityB = objectB.Velocity;

            // Get projected positions
            projectedAABBA.X += (int)(velocityA.X * projectionTime);
            projectedAABBA.Y += (int)(velocityA.Y * projectionTime);
            projectedAABBB.X += (int)(velocityB.X * projectionTime);
            projectedAABBB.Y += (int)(velocityB.Y * projectionTime);

            // Assemble boxes based on max/min values
            sweptShapeA.X = Math.Min(originalAABBA.X, projectedAABBA.X);
            sweptShapeA.Y = Math.Min(originalAABBA.Y, projectedAABBA.Y);
            sweptShapeB.X = Math.Min(originalAABBB.X, projectedAABBB.X);
            sweptShapeB.Y = Math.Min(originalAABBB.Y, projectedAABBB.Y);
            sweptShapeA.Width = Math.Max(originalAABBA.Right, projectedAABBA.Right) - sweptShapeA.X;
            sweptShapeA.Height = Math.Max(originalAABBA.Bottom, projectedAABBA.Bottom) - sweptShapeA.Y;
            sweptShapeB.Width = Math.Max(originalAABBB.Right, projectedAABBB.Right) - sweptShapeB.X;
            sweptShapeB.Height = Math.Max(originalAABBB.Bottom, projectedAABBB.Bottom) - sweptShapeB.Y;

            return sweptShapeA.Intersects(sweptShapeB);
        }
    }
}
