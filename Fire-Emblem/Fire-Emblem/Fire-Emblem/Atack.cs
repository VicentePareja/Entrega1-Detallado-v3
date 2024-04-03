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

    public void RealizarAtaque(string ventaja)
    {
    
        double WTB = ventaja == "atacante" ? 1.2 : ventaja == "defensor" ? 0.8 : 1.0;
        
        int defensaRival = Atacante.Arma == "Magic" ? Defensora.Res : Defensora.Def;
        
        int danio = (int)((Atacante.Atk * WTB) - defensaRival);
        danio = Math.Max(danio, 0);

        _view.WriteLine($"{Atacante.Nombre} ataca a {Defensora.Nombre} con {danio} de daño");

        Defensora.HPactual -= danio;
        
    }
    
    public void RealizarContraAtaque(string ventaja)
    {
       
        double WTB = ventaja == "defensor" ? 1.2 : ventaja == "atacante" ? 0.8 : 1.0;
        
        int defensaRival = Defensora.Arma == "Magic" ? Atacante.Res : Atacante.Def;
        
        int danio = (int)((Defensora.Atk * WTB) - defensaRival);
        danio = Math.Max(danio, 0);

        _view.WriteLine($"{Defensora.Nombre} ataca a {Atacante.Nombre} con {danio} de daño");

        Atacante.HPactual -= danio;

    }
    

}

