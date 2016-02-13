using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using MegaManClone;
using MegaManClone.Sprites;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone.Sprites.EnemySprites;
using MegaManClone.Entities;
using MegaManClone.Entities.MegamanStates;
using MegaManClone.Entities.BlockStates;

namespace MegaManTest
{
    [TestClass]
    public class BlockCollision
    {
        MegamanGame mmGame = new MegamanGame();

        [TestMethod]
        public void TestMethod1()
        {
            Megaman mm = new Megaman(mmGame.Content);
            Block block = new Block(new Vector2(0, 0), BlockState.Pyramid, mm, mmGame.Content);

            block.Collide(mm);

            BlockState state = BlockState.Colliding;
            Assert.AreEqual(state, block.CurrentState);
        }
    }
}
