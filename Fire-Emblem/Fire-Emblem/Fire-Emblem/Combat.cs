using Fire_Emblem_View;
namespace Fire_Emblem

{
    public class Combat
    {
        public Player Jugador1 { get; set; }
        public Player Jugador2 { get; set; }
        private View _view;

        public Combat(Player jugador1, Player jugador2, View view)
        {
            Jugador1 = jugador1;
            Jugador2 = jugador2;
            _view = view;
        }

        public void Iniciar()
        {
            
            int contador = 0;
            while (contador < 3)
            {
                contador++;
                // Jugador 1 ataca y Jugador 2 se defiende
                RealizarTurno(Jugador1, Jugador2);

                // Verificar si el juego debe terminar
                if (JuegoTerminado()) break;

                // Jugador 2 ataca y Jugador 1 se defiende
                RealizarTurno(Jugador2, Jugador1);

                // Verificar si el juego debe terminar
                if (JuegoTerminado()) break;
            }

            // Determinar y anunciar el ganador
            AnunciarGanador();
        }

        private void RealizarTurno(Player atacante, Player defensor)
        {
            Character unidadAtacante = EscogerUnidad(atacante);
            
            Character unidadDefensora = EscogerUnidad(defensor);
            
            Atack ataque = new Atack(unidadAtacante, unidadDefensora, _view);
            ataque.RealizarAtaque();
            
            if (unidadDefensora.HPactual <= 0)
            {
                defensor.Team.Characters.Remove(unidadDefensora);
                _view.WriteLine($"{unidadDefensora.Nombre} ha sido eliminado del equipo de {defensor.GetType().Name}.");
            }
            else
            {
                
                Atack contraataque = new Atack(unidadDefensora, unidadAtacante, _view);
                contraataque.RealizarAtaque();
                if (unidadAtacante.HPactual <= 0)
                {
                    atacante.Team.Characters.Remove(unidadAtacante);
                    _view.WriteLine($"{unidadAtacante.Nombre} ha sido eliminado del equipo de {atacante.GetType().Name}.");
                }
            }
        }

        private void ImprimirOpcionesDePersonajes(Player jugador)
        {
            // Asumiendo que "Name" es una propiedad pública de Player para obtener su nombre
            _view.WriteLine($"{jugador.Name} selecciona una opción");
            for (int i = 0; i < jugador.Team.Characters.Count; i++)
            {
                _view.WriteLine($"{i}: {jugador.Team.Characters[i].Nombre}");
            }
        }

        private Character EscogerUnidad(Player jugador)
        {
            ImprimirOpcionesDePersonajes(jugador); // Usa el nuevo método para imprimir opciones
            int eleccion = -1; // Inicializa elección como inválida
            do
            {
                string input = _view.ReadLine();
                if (int.TryParse(input, out eleccion) && eleccion >= 0 && eleccion < jugador.Team.Characters.Count)
                {
                    // Si la elección es válida, sale del bucle
                    break;
                }
                else
                {
                    // Elección inválida, pide al usuario que elija de nuevo
                    _view.WriteLine("Elección inválida. Por favor, elige de nuevo.");
                }
            } while (true); // Continúa hasta que se reciba una entrada válida

            return jugador.Team.Characters[eleccion];
        }


        private bool JuegoTerminado()
        {
            // Verificar si alguno de los jugadores ha perdido todas sus unidades
            return Jugador1.Team.Characters.Count == 0 || Jugador2.Team.Characters.Count == 0;
        }

        private void AnunciarGanador()
        {
            if (Jugador1.Team.Characters.Count == 0)
            {
                _view.WriteLine("Jugador 2 es el ganador!");
            }
            else if (Jugador2.Team.Characters.Count == 0)
            {
                _view.WriteLine("Jugador 1 es el ganador!");
            }
            else
            {
                _view.WriteLine("Empate!");
            }
        }
    }
}