using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.EnemySprites
{
    class EnemySpriteFactory
    {
        #region Properties

        ContentManager content;

        #endregion

        #region Constructor

        public EnemySpriteFactory(ContentManager content)
        {
            this.content = content;
        }

        #endregion

        public Sprite GetEnemy(String enemyType, Vector2 position)
        {
            if (enemyType == "met")
            {
                return new MetWalking(content, position);

            } 
            else if (enemyType == "wheelhog")
            {
                return new WheelhogRolling(content, position);
            }
            else //enemyType == "beeCopter"
            {
                return new BeeCopter(content, position);
            }
        }
    }
}
