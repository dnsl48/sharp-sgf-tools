using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#RE
    /// </summary>
    [Label("RE")]
    [Description("Result of the game")]
    public class GameResult : AProperty
    {
        /// <summary>
        /// Defines all the possible types of a game result
        /// </summary>
        public enum WinType : byte
        {
            /// <summary>Draw</summary>
            Draw = 0,

            /// <summary>Resign</summary>
            Resign = 1,

            /// <summary>Time</summary>
            Time = 2,

            /// <summary>Forfeit</summary>
            Forfeit = 3,

            /// <summary>Score</summary>
            Score = 4,

            /// <summary>Void</summary>
            Void = 5,

            /// <summary>Unknown</summary>
            Unknown = 6
        }

        private WinType _type;

        private Colour _winner = 0;

        private decimal _score = 0m;

        /// <value>Colour of a color of the winner (if defined by the WinType, otherwise may be a random colour)</value>
        public Colour who { get { return _winner; } }

        /// <value>Returns the game result</value>
        public WinType how { get { return _type; } }

        /// <value>Returns the result score (if defined, otherwise a 0m)</value>
        public decimal score { get { return _score; } }

        /// <summary>Initialize with a win type that does not imply a colour (e.g. Draw)</summary>
        /// <param name="type">Type of the game result</param>
        public GameResult(WinType type) => _type = type;

        /// <summary>Initialize with a win type and a colour of the winner</summary>
        /// <param name="type">Type of the game result (e.g. Resign or Time or Forfeit)</param>
        /// <param name="winner">Colour of the winner</param>
        public GameResult(WinType type, Colour winner) =>
            (_type, _winner) = (type, winner);

        /// <summary>Initialize with the winner colour and the game score. Implies win type to be <see cref="WinType.Score"/></summary>
        /// <param name="winner">The colour of the winner</param>
        /// <param name="score">The game score</param>
        public GameResult(Colour winner, decimal score) =>
            (_type, _winner, _score) = (WinType.Score, winner, score);

        /// <inheritdoc />
        public override string StringValue()
        {
            string result;

            switch (_type)
            {
                case WinType.Draw:
                    result = "Draw";
                    break;

                case WinType.Time:
                    result = $"{_winner.GetLabel()}+T";
                    break;

                case WinType.Forfeit:
                    result = $"{_winner.GetLabel()}+F";
                    break;

                case WinType.Resign:
                    result = $"{_winner.GetLabel()}+R";
                    break;

                case WinType.Score:
                    result = $"{_winner.GetLabel()}+{_score.ToString()}";
                    break;

                case WinType.Void:
                    result = "Void";
                    break;

                default:
                    result = "?";
                    break;
            }

            return $"{GetLabel()}[{result}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count != 1)
            {
                return null;
            }

            var value = values[0].Trim();

            if (value == "0" || value.ToLower() == "draw")
                return new GameResult(WinType.Draw);

            if (value == "?")
                return new GameResult(WinType.Unknown);

            if (value.ToLower() == "void")
                return new GameResult(WinType.Void);

            var result = value.Split(new[] { '+' }, 2);

            if (result.Length != 2)
                return null;

            var player = result[0].ToLower();

            Colour winner;
            if (player == "w")
                winner = Colour.White;
            else if (player == "b")
                winner = Colour.Black;
            else
                return null;

            var how = result[1].ToLower();

            if (how == "r" || how == "resign")
                return new GameResult(WinType.Resign, winner);
            else if (how == "t" || how == "time")
                return new GameResult(WinType.Time, winner);
            else if (how == "f" || how == "forfeit")
                return new GameResult(WinType.Forfeit, winner);

            try {
                var score = decimal.Parse(how);
                return new GameResult(winner, score);
            } catch (System.Exception) {
                return null;
            }
        }
    }
}
