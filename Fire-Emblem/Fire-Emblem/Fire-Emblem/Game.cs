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
            _player1 = new Player();
            _player2 = new Player();
        }

        public void Play()
        {
            // Instanciar LogicaSetUp y delegar la lógica
            LogicaSetUp logica = new LogicaSetUp(_view, _teamsFolder);
            logica.CargarEquipos(_player1, _player2);
        }
    }
}