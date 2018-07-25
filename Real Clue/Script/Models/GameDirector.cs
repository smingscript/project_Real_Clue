using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public delegate bool NewRoundBeginningHandler();

    public class GameDirector
    {
        #region NewGameBegun event things for C# 3.0
        public event EventHandler<NewGameBegunEventArgs> NewGameBegun;

        protected virtual void OnNewGameBegun(NewGameBegunEventArgs e)
        {
            if (NewGameBegun != null)
                NewGameBegun(this, e);
        }

        private NewGameBegunEventArgs OnNewGameBegun()
        {
            NewGameBegunEventArgs args = new NewGameBegunEventArgs();
            OnNewGameBegun(args);

            return args;
        }

        /*private NewGameBegunEventArgs OnNewGameBegunForOut()
        {
            NewGameBegunEventArgs args = new NewGameBegunEventArgs();
            OnNewGameBegun(args);

            return args;
        }*/

        public class NewGameBegunEventArgs : EventArgs
        {


            /*public NewGameBegunEventArgs()
            {
            }

            public NewGameBegunEventArgs()
            {

            }*/
        }
        #endregion

        private bool _runGame;

        public GameDirector() { }

        public GameDirector(PlayerMaker playerMaker) : this()
        {
            _players = playerMaker.Players;
            _turnNo = 1;
            _runGame = true;
        }

        public event NewRoundBeginningHandler NewRoundBeginning;

        private List<Player> _players;

        private int _turnNo;
        public int TurnNo { get; }

        private Deck _deck;

        private static GameDirector _instance;
        private int _playerTurn;

        public static GameDirector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameDirector();
                return _instance;
            }
        }

        public void Run()
        {
            while (_runGame)
            {
                bool beginnable = NewRoundBeginning();

                if (beginnable == false)

                    break;

                StartRound();
            }
        }

        private void StartRound()
        {
            OnNewGameBegun();
            Deck.Instance.Draw();
            //PlayerMaker.Instance.SetPlayers();
        }

        internal void EndTurn()
        {
            //게임의 턴을 증가시킨다
            _turnNo++;
            StartTurn();
            throw new NotImplementedException();
        }

        /// <summary>
        /// 새로운 턴이 시작되면 해당 턴의 플레이어를 정한다.
        /// </summary>
        private void StartTurn()
        {
            _playerTurn = (_turnNo % _players.Count) - 1;
            _players[_playerTurn].TurnNo = _turnNo;

        }
    }
}
