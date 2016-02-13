using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Sprites;
using MegaManClone.Entities.MegamanStates;
using MegaManClone.Sprites.MegamanSprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework.Audio;

namespace MegaManClone.Entities
{
    class Megaman : ISprite, ICollidable
    {
        #region Fields

        MegamanActionStateMachine  actionStateMachine;
        int                        armor;
        Buster                     buster;
        int                        coins;
        ContentManager             content;
        MegamanActionState         currentActionState;
        IMegamanPowerUpState       currentPowerUpState;
        Sprite                     currentSprite;
        Stage                      currentStage;
        MegamanState               direction;
        Vector2                    initialPosition;
        MegamanPowerUpStateMachine powerUpStateMachine;
        MegamanActionState         previousActionState;
        IMegamanPowerUpState       previousPowerUpState;
        MegamanSpriteFactory       spriteFactory;
        
        public int Health;
        public int Lives;
        public int MaxHealth;
        public int Points;

        #endregion

        #region Properties

        public Rectangle AABB
        {
            get
            {
                Rectangle AABB = ((ICollidable)currentSprite).AABB;

                AABB.X = (int)currentSprite.Position.X;
                AABB.Y = (int)currentSprite.Position.Y;

                return AABB;
            }
        }

        internal MegamanActionStateMachine ActionStateMachine
        {
            get { return actionStateMachine; }
        }

        public bool Active
        {
            get { return true; }
        }

        public int Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
                while (coins >= 100)
                {
                    coins -= 100;
                    Lives++;
                }
            }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        internal MegamanActionState CurrentActionState
        {
            get { return currentActionState; }
            set { currentActionState = value; }
        }

        internal IMegamanPowerUpState CurrentPowerUpState
        {
            get { return currentPowerUpState; }
            set
            {
                previousPowerUpState = currentPowerUpState;
                currentPowerUpState = value;
            }
        }

        public Sprite CurrentSprite
        {
            get { return currentSprite; }
        }

        public Stage CurrentStage
        {
            get { return currentStage; }
            set
            {
                currentStage = value;
                buster.CurrentStage = value;
            }
        }

        public MegamanState Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int MMHealth
        {
            get { return Health; }
            set { Health = value; }
        }

        public Vector2 InitialPosition
        {
            get { return initialPosition; }
            set { initialPosition = value; }
        }

        internal MegamanPowerUpStateMachine PowerUpStateMachine
        {
            get { return powerUpStateMachine; }
        }

        internal MegamanActionState PreviousActionState
        {
            get { return previousActionState; }
            set { previousActionState = value; }
        }


        internal IMegamanPowerUpState PreviousPowerUpState
        {
            get { return previousPowerUpState; }
            set { previousPowerUpState = value; }
        }

        public Vector2 Velocity
        {
            get
            {
                return currentSprite.Velocity;
            }
        }

        #endregion

        #region Constructor

        public Megaman(ContentManager content)
        {
            buster = new Buster(this);
            actionStateMachine = new MegamanActionStateMachine(this);
            powerUpStateMachine = new MegamanPowerUpStateMachine(this);
            spriteFactory = new MegamanSpriteFactory(content);
            currentActionState = actionStateMachine.GetActionState(ActionState.Idle);
            currentPowerUpState = powerUpStateMachine.getState(MegamanState.Small);
            currentSprite = spriteFactory.GetSprite(MegamanStateHelper.GetState(currentActionState, currentPowerUpState));
            currentSprite.Position = new Vector2();
            initialPosition = new Vector2();
            direction = MegamanState.Right;
            MaxHealth = 100;
            Health = MaxHealth;
            armor = 0;
            this.content = content;
        }

        #endregion

        #region Miscellanious Methods

        public void StateChanged()
        {
            Sprite newSprite = spriteFactory.GetSprite(MegamanStateHelper.GetState(currentActionState, currentPowerUpState));

            Vector2 newPosition = currentSprite.Position;
            newPosition.Y -= newSprite.AABB.Height - currentSprite.AABB.Height;
            newSprite.Position = newPosition;
            newSprite.Effects = direction == MegamanState.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            newSprite.Velocity = currentSprite.Velocity;
            newSprite.Acceleration = currentSprite.Acceleration;

            currentSprite = newSprite;

            MaxHealth = currentPowerUpState.GetMaxHealth();
            armor = currentPowerUpState.GetArmor();
        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            currentSprite.Draw(spriteBatch, camera);
        }

        public void Reset()
        {
            currentActionState = actionStateMachine.GetActionState(ActionState.Idle);
            currentPowerUpState = powerUpStateMachine.getState(MegamanState.Small);
            StateChanged();
            currentSprite = spriteFactory.GetSprite(MegamanStateHelper.GetState(currentActionState, currentPowerUpState));
            currentSprite.Position = initialPosition;
            direction = MegamanState.Right;

            Health = MaxHealth;
        }

        public void Update(GameTime gameTime)
        {
            currentActionState.Update(gameTime);
            currentPowerUpState.Update(gameTime);
            currentSprite.Update(gameTime);
            buster.Update(gameTime);
        }

        #endregion

        #region ICollidable Implementation

        public void BlockMovement(ICollidable otherObject)
        {
            currentActionState.BlockMovement(otherObject);
        }

        public void Collide(ICollidable otherObject)
        {
            if (CollisionHelper.GetCollisionSide(this, otherObject) == CollisionSide.Bottom ||
                CurrentPowerUpState is MegamanFalconState)
            {
                otherObject.TakeDamage(this, 0);
            }
        }

        public void TakeDamage(ICollidable otherObject, int Damage)
        {
            Damage -= armor;
            if (Damage < 0)
            {
                Damage = 0;
            }
            Health -= Damage;

            Health = (int)MathHelper.Clamp(Health, 0, MaxHealth);

            CurrentPowerUpState.TakeDamage();
            otherObject.BlockMovement(this);
        }

        public virtual void TriggerFall()
        {
            currentActionState.TriggerFall();
        }

        #endregion

        #region Command Receiver Methods

        public void Die()
        {
            currentPowerUpState = powerUpStateMachine.getState(MegamanState.Dead);
            StateChanged();
        }

        public void DownCommand()
        {
            if (!(currentPowerUpState is MegamanDeadState))
            {
                currentActionState.DownCommand();
            }
        }

        public void FalconTransition()
        {
            currentPowerUpState.FalconTransition();
        }

        public void LargeTransition()
        {
            currentPowerUpState.LargeTransition();
        }

        public void LeftCommand()
        {
            if (!(currentPowerUpState is MegamanDeadState))
            {
                currentActionState.LeftCommand();
            }
        }

        public void RightCommand()
        {
            if (!(currentPowerUpState is MegamanDeadState))
            {
                currentActionState.RightCommand();
            }
        }

        public void UpCommand()
        {
            if (!(currentPowerUpState is MegamanDeadState))
            {
                currentActionState.UpCommand();
            }
        }

        public void ZeroTransition()
        {
            currentPowerUpState.ZeroTransition();
        }

        public void ChargeBuster()
        {
            buster.Charge();
        }

        #endregion
    }
}
