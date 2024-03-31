using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        _view.WriteLine("Elige un archivo para cargar los equipos");
    }
}