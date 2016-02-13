using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MegaManClone.Sprites;
using MegaManClone.Entities;
using MegaManClone.Commands;
using MegaManClone.Controllers;
using MegaManClone.Stages;
using MegaManClone.Entities.MegamanStates;
using MegaManClone.Entities.BlockStates;
using MegaManClone.Sprites.EnemySprites;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone;


namespace MegaManTests
{
    [TestClass]
    public class TestCollisionResonse_MegaMan
    {
        MegaManGame megamanGame;
        GameTime gameTime;
        Megaman megaMan;
        Sprite metWalking;
        Block block;

        [TestInitialize()]
        public void TestInitialize(Megaman megaMan, Block block)
        {

            this.megaMan = megaMan;
            this.block = block;

        }

        [TestMethod]
        public void TestCollisionResponse_Megaman_Block_FromLeft(Megaman megaMan, Block block)
        {
            megaMan = new Megaman(new Vector2(50, 240), megamanGame.Content);
            block = new Block(new Vector2(100, 240), BlockState.Question, megaMan, megamanGame.Content);

            megaMan.RightCommand();
            megaMan.ActionState.UpdatePosition(new Vector2(105, 240), gameTime);

        }
        //        [TestMethod]
        //        public void TestCollisionResponse_Megaman_Block_FromAbove()
        //        {
        //        }
        [TestMethod]
        public void TestCollisionResponse_Megaman_Met_FromLeft()
        {
            megaMan = new Megaman(new Vector2(300, 354), megamanGame.Content);
            metWalking = new MetWalking(megamanGame.Content);
            metWalking.Position = new Vector2(400, 354);

            megaMan.ActionState.UpdatePosition(new Vector2(401, 354), gameTime);

            IMegamanPowerUpState actualState = megaMan.PowerUpState;
            IMegamanPowerUpState expectedState = megaMan.PowerUpStateMachine.getState(MegamanState.Dead);

            Assert.AreEqual(expectedState, actualState);
        }
        //       [TestMethod]
        //       public void TestCollisionResponse_Megaman_Met_FromAbove()
        //       {
        //       }
    }
}
