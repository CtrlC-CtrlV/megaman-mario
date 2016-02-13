using MegaManClone.Entities;
using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MenuSprites
{
    class ScoreTextSource : ITextSource
    {
        #region Fields

        Stage stage;

        #endregion

        #region Constructor

        public ScoreTextSource(Stage stage)
        {
            this.stage = stage;
        }

        #endregion

        #region ITextSource Implementation

        public String GetText()
        {
            List<Tuple<int, string>> scores = stage.GetHighScores(5);
            string str = "High Scores:\n";
            foreach (Tuple<int, string> score in scores)
            {
                str += String.Format("{0} - {1}\n", score.Item1, score.Item2);
            }

            return str;
        }

        #endregion
    }
}
