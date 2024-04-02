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
        // Determinar W T B basado en la ventaja o desventaja del triángulo de armas
        double WTB = ventaja == "atacante" ? 1.2 : ventaja == "defensor" ? 0.8 : 1.0;

        // Determinar si usar Def o Res
        int defensaRival = Atacante.Arma == "Magic" ? Defensora.Res : Defensora.Def;

        // Calcular daño
        int danio = (int)((Atacante.Atk * WTB) - defensaRival);
        danio = Math.Max(danio, 0);

        _view.WriteLine($"{Atacante.Nombre} ataca a {Defensora.Nombre} con {danio} de daño");

        Defensora.HPactual -= danio;
        
    }
    
    public void RealizarContraAtaque(string ventaja)
    {
        // Invertir la lógica de ventaja para el contraataque
        double WTB = ventaja == "defensor" ? 1.2 : ventaja == "atacante" ? 0.8 : 1.0;

        // En el contraataque, el defensor original ahora es el atacante, y viceversa
        // Por lo tanto, debemos ajustar qué defensa usar basado en el arma del defensor original, que ahora ataca
        int defensaRival = Defensora.Arma == "Magic" ? Atacante.Res : Atacante.Def;

        // Calcular daño para el contraataque
        int danio = (int)((Defensora.Atk * WTB) - defensaRival);
        danio = Math.Max(danio, 0);

        _view.WriteLine($"{Defensora.Nombre} ataca a {Atacante.Nombre} con {danio} de daño");

        Atacante.HPactual -= danio;

    }

    


}

