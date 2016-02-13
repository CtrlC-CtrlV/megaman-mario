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
    public class ItemCollision
    {
        MegamanGame mmGame = new MegamanGame();

        [TestMethod]
        public void ETankCollideSmallMegaman()
        {
            ETank tank = new ETank(mmGame.Content, new Vector2(0,0));
            Megaman mm = new Megaman(mmGame.Content);
            mm.CurrentPowerUpState = mm.PowerUpStateMachine.getState(MegamanState.Small);
            mm.StateChanged();

            tank.Collide(mm);

            IMegamanPowerUpState expectedState = new MegamanLargeState(mm);
            Assert.AreEqual(expectedState, mm.CurrentPowerUpState);
        }

        [TestMethod]
        public void ETankCollideLargeMegaman()
        {
            ETank tank = new ETank(mmGame.Content, new Vector2(0, 0));
            Megaman mm = new Megaman(mmGame.Content);
            mm.CurrentPowerUpState = mm.PowerUpStateMachine.getState(MegamanState.Large);
            mm.StateChanged();

            tank.Collide(mm);

            IMegamanPowerUpState expectedState = mm.PowerUpStateMachine.getState(MegamanState.Large);
            Assert.AreEqual(expectedState, mm.CurrentPowerUpState);
        }

        [TestMethod]
        public void ETankCollideZeroMegaman()
        {
            ETank tank = new ETank(mmGame.Content, new Vector2(0, 0));
            Megaman mm = new Megaman(mmGame.Content);
            mm.CurrentPowerUpState = mm.PowerUpStateMachine.getState(MegamanState.Zero);
            mm.StateChanged();

            tank.Collide(mm);

            IMegamanPowerUpState expectedState = mm.PowerUpStateMachine.getState(MegamanState.Zero);
            Assert.AreEqual(expectedState, mm.CurrentPowerUpState);
        }

        [TestMethod]
        public void ETankCollideFalconMegaman()
        {
            ETank tank = new ETank(mmGame.Content, new Vector2(0, 0));
            Megaman mm = new Megaman(mmGame.Content);
            mm.CurrentPowerUpState = mm.PowerUpStateMachine.getState(MegamanState.Falcon);
            mm.StateChanged();

            tank.Collide(mm);

            IMegamanPowerUpState expectedState = mm.PowerUpStateMachine.getState(MegamanState.Falcon);
            Assert.AreEqual(expectedState, mm.CurrentPowerUpState);
        }
    }
}
