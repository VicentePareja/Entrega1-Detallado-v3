using Fire_Emblem_View;
namespace Fire_Emblem;

public class Atack
{
    public Character Atacante { get; private set; }
    public Character Defensora { get; private set; }
    private View _view;

    public Atack(Character atacante, Character defensora, View view)
    {
        Atacante = atacante;
        Defensora = defensora;
        _view = view;
    }

    public void RealizarAtaque()
    {

        int danio = Atacante.Atk - Defensora.Def;


        danio = Math.Max(danio, 0);

        _view.WriteLine($"{Atacante.Nombre} ataca a {Defensora.Nombre} causando {danio} puntos de daño.");


        Defensora.HPactual -= danio;


        if (Defensora.HPactual <= 0)
        {
            _view.WriteLine($"{Defensora.Nombre} ha sido derrotado!");
      
        }
    }


}

