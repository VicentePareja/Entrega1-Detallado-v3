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
            while (contador < 3) // Limita a 3 rondas de combate
            {
                contador++;
                if (contador % 2 == 1) // Turnos impares: Jugador 1 ataca, Jugador 2 se defiende
                {
                    // Jugador 1 ataca y Jugador 2 se defiende
                    RealizarTurno(Jugador1, Jugador2, contador);
                }
                else // Turnos pares: Jugador 2 ataca, Jugador 1 se defiende
                {
                    // Jugador 2 ataca y Jugador 1 se defiende
                    RealizarTurno(Jugador2, Jugador1, contador);
                }

                // Verificar si el juego debe terminar
                if (JuegoTerminado()) break;
            }

            // Determinar y anunciar el ganador
            AnunciarGanador();
        }


        private void RealizarTurno(Player atacante, Player defensor, int turno)
        {
            Character unidadAtacante = EscogerUnidad(atacante);
            
            Character unidadDefensora = EscogerUnidad(defensor);

            _view.WriteLine($"Round 1: {unidadAtacante.Nombre} ({atacante.Name}) comienza");

            string ventaja = CalcularVentaja(unidadAtacante, unidadDefensora);
            ImprimirVentaja(unidadAtacante, unidadDefensora, ventaja);
            
            Atack ataque = new Atack(unidadAtacante, unidadDefensora, _view);
            ataque.RealizarAtaque(ventaja);
            
            if (unidadDefensora.HPactual <= 0)
            {
                defensor.Team.Characters.Remove(unidadDefensora);
                _view.WriteLine($"{unidadDefensora.Nombre} ha sido eliminado del equipo de {defensor.GetType().Name}.");
            }
            else
            {
                
                ataque.RealizarContraAtaque(ventaja);
                if (unidadAtacante.HPactual <= 0)
                {
                    atacante.Team.Characters.Remove(unidadAtacante);
                    _view.WriteLine($"{unidadAtacante.Nombre} ha sido eliminado del equipo de {atacante.GetType().Name}.");
                }
            }
        }

        public string CalcularVentaja(Character atacante, Character defensor)
        {
            // Definir el triángulo de armas
            var ventajas = new Dictionary<string, string>
            {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };

            // Comprobar si el atacante tiene ventaja
            if (ventajas.ContainsKey(atacante.Arma) && ventajas[atacante.Arma] == defensor.Arma)
            {
                return "atacante";
            }
            // Comprobar si el defensor tiene ventaja
            else if (ventajas.ContainsKey(defensor.Arma) && ventajas[defensor.Arma] == atacante.Arma)
            {
                return "defensor";
            }
            // Si ninguno tiene ventaja
            return "ninguno";
        }

        public void ImprimirVentaja(Character atacante, Character defensor, string ventaja)
        {
            switch (ventaja)
            {
                case "atacante":
                    _view.WriteLine($"{atacante.Nombre} ({atacante.Arma}) tiene ventaja con respecto a {defensor.Nombre} ({defensor.Arma})");
                    break;
                case "defensor":
                    _view.WriteLine($"{defensor.Nombre} ({defensor.Arma}) tiene ventaja con respecto a {atacante.Nombre} ({atacante.Arma})");
                    break;
                case "ninguno":
                    _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                    break;
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