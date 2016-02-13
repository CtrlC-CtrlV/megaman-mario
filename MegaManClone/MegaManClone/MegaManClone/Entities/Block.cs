using MegaManClone.Entities.BlockStates;
using MegaManClone.Sprites;
using MegaManClone.Sprites.BlockSprites;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities
{
    class Block : ISprite, ICollidable
    {
        #region Fields

        Sprite currentSprite;
        IBlockState currentState;
        BlockState currentType;
        Vector2 initialPosition;
        BlockState initialType;
        int itemIndex = 0;
        BlockSpriteFactory spriteFactory;
        BlockStateMachine stateMachine;

        #endregion

        #region Properties

        public Rectangle AABB
        {
            get
            {
                Rectangle currentAABB = currentSprite.AABB;

                currentAABB.X = (int)currentSprite.Position.X;
                currentAABB.Y = (int)currentSprite.Position.Y;

                return currentAABB;
            }
        }

        public bool Active
        {
            get { return true; }
        }

        public Sprite CurrentSprite
        {
            get { return currentSprite; }
        }

        public IBlockState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public BlockState CurrentType
        {
            get { return currentType; }
            set { currentType = value; }
        }

        public List<HiddenItem> Items = new List<HiddenItem>();

        public int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        public BlockSpriteFactory SpriteFactory
        {
            get { return spriteFactory; }
        }

        public BlockStateMachine StateMachine
        {
            get { return stateMachine; }
        }

        public Vector2 Velocity
        {
            get
            {
                return currentState.GetVelocity();
            }
        }

        #endregion

        #region Constructor

        public Block(Vector2 position, BlockState initialType, Megaman megaman, ContentManager content)
        {
            spriteFactory = new BlockSpriteFactory(content);
            stateMachine = new BlockStateMachine(this, megaman);
            currentState = stateMachine.GetState(BlockState.Normal);
            currentType = initialType;
            currentSprite = spriteFactory.GetSprite(BlockStateHelper.GetState(currentType, currentState));
            currentSprite.Position = position;

            initialPosition = position;
            this.initialType = initialType;
        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            currentSprite.Draw(spriteBatch, camera);
        }

        public void Reset()
        {
            currentState = stateMachine.GetState(BlockState.Normal);
            currentType = initialType;
            StateChanged();
            currentSprite.Position = initialPosition;
            itemIndex = 0;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(currentSprite, gameTime);
            currentSprite.Update(gameTime);
        }

        #endregion

        #region ICollidable Implementation

        public void BlockMovement(ICollidable otherObject)
        {

        }

        public void Collide(ICollidable otherObject)
        {
            if (currentType == BlockState.Hidden)
            {
                otherObject.TriggerFall();

            } else if (currentType == BlockState.Death && otherObject is Megaman)
            {
                (otherObject as Megaman).Die();

            } else
            {
                currentState.Collide(otherObject);
            }
        }

        public void TakeDamage(ICollidable otherObject, int Damage)
        {
            
        }

        public void TriggerFall()
        {

        }

        #endregion
        
        #region Miscellaneous Methods

        public void AddItem(HiddenItem item)
        {
            Items.Add(item);
        }

        public void StateChanged()
        {
            Sprite tempSprite = spriteFactory.GetSprite(BlockStateHelper.GetState(currentType, currentState));
            tempSprite.Position = currentSprite.Position;
            currentSprite = tempSprite;
        }

        #endregion
    }
}
