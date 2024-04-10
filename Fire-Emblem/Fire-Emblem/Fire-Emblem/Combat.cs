using Fire_Emblem_View;
namespace Fire_Emblem

{
    public class Combat
    {
        private readonly Character _atacante;
        private readonly Character _defensor;
        private readonly string _ventaja;
        private readonly View _view;

        public Combat(Character atacante, Character defensor, string ventaja, View view)
        {
            _atacante = atacante;
            _defensor = defensor;
            _ventaja = ventaja;
            _view = view;
        }

        public void Iniciar()
        {
            Atack ataque = new Atack(_atacante, _defensor, _view);
            ataque.RealizarAtaque(_ventaja);
            
            if (_defensor.HPactual > 0)
            {
                ataque.RealizarContraAtaque(_ventaja);
            }
            
            if (_atacante.HPactual > 0 && _defensor.HPactual > 0)
            {
                if (_atacante.Spd >= _defensor.Spd + 5)
                {
                    new Atack(_atacante, _defensor, _view).RealizarAtaque(_ventaja);
                }
                else if (_defensor.Spd >= _atacante.Spd + 5)
                {
                    new Atack(_atacante, _defensor, _view).RealizarContraAtaque(_ventaja);
                }
                else
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
            }
            
            _view.WriteLine($"{_atacante.Nombre} ({_atacante.HPactual}) : {_defensor.Nombre} ({_defensor.HPactual})");
        }
    }
}