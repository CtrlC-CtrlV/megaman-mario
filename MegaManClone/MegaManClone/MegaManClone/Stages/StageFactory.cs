using MegaManClone.Entities.BlockStates;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MegaManClone.Entities;
using MegaManClone.Sprites;
using MegaManClone.Sprites.BlockSprites;
using MegaManClone.Sprites.EnemySprites;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone.Sprites.MegamanSprites;
using MegaManClone.Controllers;
using MegaManClone.Commands;
using Microsoft.Xna.Framework.Input;
using MegaManClone.Sprites.MenuSprites;
using MegaManClone.Sprites.Background;


namespace MegaManClone.Stages
{
    class StageFactory
    {
        #region Properties

        string configFilePath = "\\Content\\config.xml";
        ContentManager content;
        EnemySpriteFactory enemySpriteFactory;
        MegamanGame game;
        ItemSpriteFactory itemSpriteFactory;
        Megaman megaman;

        #endregion

        #region Constructors

        public StageFactory(MegamanGame game)
        {
            this.content = game.Content;
            this.game = game;
            this.megaman = game.Megaman;

            enemySpriteFactory = new EnemySpriteFactory(content);
            itemSpriteFactory = new ItemSpriteFactory(content);
        }

        #endregion

        #region Scene Generation

        void addBoundary(Stage stage, Vector2 boundary)
        {
            for (int i = 0; i < boundary.X; i += 32)
            {
                Block topBlock = new Block(new Vector2(i, -31), BlockState.Pyramid, megaman, content);
                Block bottomBlock = new Block(new Vector2(i, boundary.Y), BlockState.Pyramid, megaman, content);

                stage.AddCollidable(topBlock);
                stage.AddCollidable(bottomBlock);
                stage.AddSprite(topBlock);
                stage.AddSprite(bottomBlock);
            }

            for (int i = 0; i < boundary.Y; i += 32)
            {
                Block leftBlock = new Block(new Vector2(-31, i), BlockState.Pyramid, megaman, content);
                Block rightBlock = new Block(new Vector2(boundary.X, i), BlockState.Pyramid, megaman, content);

                stage.AddCollidable(leftBlock);
                stage.AddCollidable(rightBlock);
                stage.AddSprite(leftBlock);
                stage.AddSprite(rightBlock);
            }
        }

        Block getBlockFromNode(XmlNode blockNode, Stage stage)
        {
            Vector2 location = parseAndScaleOrderedPairToVector2(blockNode.Attributes["location"].InnerText);
            Block block = new Block(location, BlockStateHelper.GetState(blockNode.Attributes["blockType"].InnerText), megaman, content);

            foreach (XmlNode itemNode in blockNode.ChildNodes)
            {
                try
                {
                    HiddenItem item = itemSpriteFactory.GetHiddenItem(itemNode.Attributes["itemType"].InnerText, block);
                    stage.AddSprite(item);
                    stage.AddCollidable(item);
                    block.AddItem(item);
                } catch (Exception)
                {
                    //Comment?
                }
            }

            return block;
        }

        Sprite getEnemyFromNode(XmlNode enemyNode)
        {
            Sprite enemy = enemySpriteFactory.GetEnemy(enemyNode.Attributes["enemyType"].InnerText,
                parseAndScaleOrderedPairToVector2(enemyNode.Attributes["location"].InnerText));

            return enemy;
        }

        Sprite getItemFromNode(XmlNode itemNode)
        {
            Vector2 position;

            try
            {
                position = parseAndScaleOrderedPairToVector2(itemNode.Attributes["location"].InnerText);

            } catch (NullReferenceException)
            {
                // Not a required attribute; exception is OK
                position = Vector2.Zero;
            }

            return itemSpriteFactory.GetItem(itemNode.Attributes["itemType"].InnerText, position);
        }

        MenuChoice getMenuChoiceFromNode(XmlNode menuChoiceNode)
        {
            MenuChoice menuChoice = new MenuChoice(menuChoiceNode.Attributes["id"].InnerText, content,
                menuChoiceNode.Attributes["destination"].InnerText, game,
                parseAndScaleOrderedPairToVector2(menuChoiceNode.Attributes["position"].InnerText),
                menuChoiceNode.Attributes["text"].InnerText);

            try
            {
                menuChoice.Down = menuChoiceNode.Attributes["down"].InnerText;

            } catch (NullReferenceException)
            {

            }

            try
            {
                menuChoice.Left = menuChoiceNode.Attributes["left"].InnerText;

            } catch (NullReferenceException)
            {

            }

            try
            {
                menuChoice.Right = menuChoiceNode.Attributes["right"].InnerText;

            } catch (NullReferenceException)
            {

            }

            try
            {
                menuChoice.Up = menuChoiceNode.Attributes["up"].InnerText;

            } catch (NullReferenceException)
            {

            }

            return menuChoice;
        }

        Menu getMenuFromNode(XmlNode menuNode)
        {
            Menu menu = new Menu(menuNode.Attributes["background"].InnerText, bool.Parse(menuNode.Attributes["canGoBack"].InnerText), game, menuNode.Attributes["initialChoice"].InnerText);
            ControllerFactory controllerFactory = new ControllerFactory(menu, game, megaman);

            foreach (XmlNode configItemNode in menuNode.ChildNodes)
            {
                if (configItemNode.Name == "choice")
                {
                    MenuChoice menuChoice = getMenuChoiceFromNode(configItemNode);

                    menu.AddSprite(menuChoice);
                    menu.AddChoice(menuChoice);

                } else if (configItemNode.Name == "text")
                {
                    menu.AddSprite(getMenuTextFromNode(configItemNode));
                }
            }

            menu.AddController(controllerFactory.GetKeyboardController());
            menu.AddController(controllerFactory.GetGamePadController());

            return menu;
        }

        MenuText getMenuTextFromNode(XmlNode menuTextNode)
        {
            String type = menuTextNode.Attributes["type"].InnerText;

            if (type == "lives")
            {
                return new MenuText(content, parseAndScaleOrderedPairToVector2(menuTextNode.Attributes["position"].InnerText), new LivesTextSource(megaman));

            } else if (type == "score")
            {
                return new MenuText(content, parseAndScaleOrderedPairToVector2(menuTextNode.Attributes["position"].InnerText), new ScoreTextSource(game.CurrentScene as Stage));

            } else if (type == "string")
            {
                return new MenuText(content, parseAndScaleOrderedPairToVector2(menuTextNode.Attributes["position"].InnerText), menuTextNode.Attributes["value"].InnerText);

            } else // type == "time"
            {
                return new MenuText(content, parseAndScaleOrderedPairToVector2(menuTextNode.Attributes["position"].InnerText), new TimeTextSource(game.CurrentScene as Stage));
            }
        }

        ParallaxSprite getParallaxSpriteFromNode(XmlNode parallaxSpriteNode, Stage stage)
        {
            ParallaxSpriteFactory parallaxSpriteFactory = new ParallaxSpriteFactory(stage.Camera, content);
            return parallaxSpriteFactory.GetParallaxSprite(parallaxSpriteNode.Attributes["type"].InnerText,
                float.Parse(parallaxSpriteNode.Attributes["layerDepth"].InnerText),
                parseAndScaleOrderedPairToVector2(parallaxSpriteNode.Attributes["position"].InnerText));
        }

        public Scene GetScene(String sceneId)
        {
            XmlDocument configurationDocument = new XmlDocument();

            configurationDocument.Load(Directory.GetCurrentDirectory() + configFilePath);

            foreach (XmlNode sceneNode in configurationDocument.DocumentElement.ChildNodes)
            {
                if (sceneNode.Attributes["id"].InnerText == sceneId)
                {
                    if (sceneNode.Name == "stage")
                    {
                        return getStageFromNode(sceneNode);

                    } else if (sceneNode.Name == "menu")
                    {
                        return getMenuFromNode(sceneNode);
                    }
                }
            }

            throw new ArgumentException(String.Format("No scene with id {0}", sceneId));
        }

        Stage getStageFromNode(XmlNode stageNode)
        {
            Vector2 boundary = parseAndScaleOrderedPairToVector2(stageNode.Attributes["boundary"].InnerText);
            Stage stage = new Stage(game, boundary,
                stageNode.Attributes["background"].InnerText,
                stageNode.Attributes["music"].InnerText,
                stageNode.Attributes["id"].InnerText);
            ControllerFactory controllerFactory = new ControllerFactory(stage, game, megaman);

            addBoundary(stage, boundary);

            foreach (XmlNode configItemNode in stageNode.ChildNodes)
            {
                if (configItemNode.Name == "block")
                {
                    Block block = getBlockFromNode(configItemNode, stage); // there's got to be a better way then passing the stage...
                    stage.AddSprite(block);
                    stage.AddCollidable(block);

                } else if (configItemNode.Name == "platform")
                {
                    Point platformDimensions = parseAndScaleOrderedPairToPoint(configItemNode.Attributes["size"].InnerText);
                    Vector2 platformLocation = parseAndScaleOrderedPairToVector2(configItemNode.Attributes["location"].InnerText);
                    BlockState platformType = BlockStateHelper.GetState(configItemNode.Attributes["blockType"].InnerText);

                    platformDimensions.X += (int)platformLocation.X;
                    platformDimensions.Y += (int)platformLocation.Y;

                    for (int i = (int)platformLocation.X; i < platformDimensions.X; i += 32)
                    {
                        for (int j = (int)platformLocation.Y; j < platformDimensions.Y; j += 32)
                        {
                            Block block = new Block(new Vector2(i, j), platformType, megaman, content);
                            stage.AddSprite(block);
                            stage.AddCollidable(block);
                        }
                    }

                } else if (configItemNode.Name == "item")
                {
                    Sprite item = getItemFromNode(configItemNode);
                    stage.AddSprite(item);
                    stage.AddCollidable(item);

                } else if (configItemNode.Name == "enemy")
                {
                    Sprite enemy = getEnemyFromNode(configItemNode);
                    stage.AddSprite(enemy);
                    stage.AddCollidable(enemy);

                } else if(configItemNode.Name == "megaman")
                {
                    stage.MegamanInitialPosition = parseAndScaleOrderedPairToVector2(configItemNode.Attributes["location"].InnerText);

                } else if (configItemNode.Name == "parallax")
                {
                    stage.AddSprite(getParallaxSpriteFromNode(configItemNode, stage));
                }
            }

            stage.Checkpoint = parseAndScaleOrderedPairToVector2(stageNode.Attributes["checkpoint"].InnerText);

            stage.AddSprite(new HUD(game.Content, game.Megaman, stage));
            stage.AddController(controllerFactory.GetGamePadController());
            stage.AddController(controllerFactory.GetKeyboardController());

            return stage;
        }

        static Point parseAndScaleOrderedPairToPoint(string str)
        {
            int commaPosition = str.IndexOf(',');
            Point orderedPair = new Point();

            orderedPair.X = 32 * Int32.Parse(str.Substring(0, commaPosition));
            orderedPair.Y = 32 * Int32.Parse(str.Substring(commaPosition + 1));

            return orderedPair;
        }

        static Vector2 parseAndScaleOrderedPairToVector2(string str)
        {
            int commaPosition = str.IndexOf(',');
            Vector2 orderedPair = new Vector2();

            orderedPair.X = 32 * float.Parse(str.Substring(0, commaPosition));
            orderedPair.Y = 32 * float.Parse(str.Substring(commaPosition + 1));

            return orderedPair;
        }

        #endregion
    }
}
