using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class Game
    {
        private View _view;
        private string _teamsFolder;
        public Player _player1;
        public Player _player2;
        
        public Game(View view, string teamsFolder)
        {
            _view = view;
            _teamsFolder = teamsFolder;
            _player1 = new Player("Player 1");
            _player2 = new Player("Player 2");
        }

        public void Play()
        {
           
            SetUpLogic logic = new SetUpLogic(_view, _teamsFolder);

            if (logic.LoadTeams(_player1, _player2))
            {
                Battle battle = new Battle(_player1, _player2, _view);
                battle.Start();
            }
            
        }

    }
}