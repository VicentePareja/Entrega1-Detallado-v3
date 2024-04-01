using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class LogicaSetUp
    {
        private View _view;
        private string _teamsFolder;
        private List<Character> characters;
        private Player _player1;
        private Player _player2;

        public LogicaSetUp(View view, string teamsFolder)
        {
            _view = view;
            _teamsFolder = teamsFolder;
            characters = new List<Character>();
            
        }

        public void CargarEquipos(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
    
            _view.WriteLine("Elige un archivo para cargar los equipos");

            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (files.Length == 0)
            {
                _view.WriteLine("No hay archivos disponibles.");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
            }

            string input = _view.ReadLine();
            if (int.TryParse(input, out int fileIndex) && fileIndex >= 0 && fileIndex < files.Length)
            {
                string selectedFile = files[fileIndex];
                string content = File.ReadAllText(selectedFile);
        
                // Llama a ImportarCharacters para cargar los personajes desde el JSON
                ImportarCharacters();
        
                // Ahora llama a ChooseCharacters pasándole la ruta del archivo seleccionado
                if (ValidTeams(selectedFile))
                {
                    ChooseCharacters(selectedFile);
                }
                else
                {
                    _view.WriteLine("Archivo de equipos no válido");
                }
                
            }
            else
            {
                _view.WriteLine("Selección inválida.");
            }
        }

        public bool ValidTeams(string selectedFile)
        {
            var lines = File.ReadAllLines(selectedFile);
            bool isPlayer1 = true; // Controla a qué jugador se están asignando personajes

            // Crea equipos temporales para validar
            Team team1 = new Team();
            Team team2 = new Team();

            List<string> currentTeamNames = new List<string>();

            foreach (var line in lines)
            {
                if (line == "Player 1 Team")
                {
                    // Verifica y limpia el equipo anterior antes de cambiar
                    if (currentTeamNames.Any())
                    {
                        bool valid = ValidateAndClearCurrentTeam(currentTeamNames, isPlayer1 ? team1 : team2);
                        if (!valid) return false; // Si el equipo actual no es válido, retorna falso
                        currentTeamNames.Clear();
                    }
                    isPlayer1 = true;
                }
                else if (line == "Player 2 Team")
                {
                    // Verifica y limpia el equipo anterior antes de cambiar
                    if (currentTeamNames.Any())
                    {
                        bool valid = ValidateAndClearCurrentTeam(currentTeamNames, isPlayer1 ? team1 : team2);
                        if (!valid) return false; // Si el equipo actual no es válido, retorna falso
                        currentTeamNames.Clear();
                    }
                    isPlayer1 = false;
                }
                else
                {
                    currentTeamNames.Add(line);
                }
            }

            // Verifica el último equipo después de salir del bucle
            if (currentTeamNames.Any())
            {
                bool valid = ValidateAndClearCurrentTeam(currentTeamNames, isPlayer1 ? team1 : team2);
                if (!valid) return false; // Si el equipo final no es válido, retorna falso
            }

            return true; // Todos los equipos son válidos
        }
        
        private bool ValidateAndClearCurrentTeam(List<string> characterNames, Team team)
        {
            foreach (var name in characterNames)
            {
                // Aquí simplemente añadimos los personajes por nombre; no necesitamos instancias completas
                // Esto es solo para la validación de duplicados
                team.Characters.Add(new Character { Nombre = name });
            }

            bool isValid = team.EsEquipoValido();
            team.Characters.Clear(); // Limpia el equipo después de la validación para reutilizarlo
            return isValid;
        }


        public void ChooseCharacters(string selectedFilePath)
        {
            var lines = File.ReadAllLines(selectedFilePath);
            bool isPlayer1 = true; // Comienza asignando personajes al equipo del jugador 1

            List<string> currentTeamNames = new List<string>();

            foreach (var line in lines)
            {
                if (line == "Player 1 Team")
                {
                    isPlayer1 = true;
                    if (currentTeamNames.Any())
                    {
                        AssignCharactersToTeam(currentTeamNames, isPlayer1 ? _player1.Team : _player2.Team);
                        currentTeamNames.Clear();
                    }
                    _view.WriteLine("Player 1 selecciona una opción");
                }
                else if (line == "Player 2 Team")
                {
                    if (currentTeamNames.Any())
                    {
                        AssignCharactersToTeam(currentTeamNames, isPlayer1 ? _player1.Team : _player2.Team);
                        currentTeamNames.Clear();
                    }
                    _view.WriteLine("Player 2 selecciona una opción");
                    isPlayer1 = false;
                }
                else
                {
                    currentTeamNames.Add(line);
                }
            }

            // Asigna los personajes restantes al último equipo
            if (currentTeamNames.Any())
            {
                AssignCharactersToTeam(currentTeamNames, isPlayer1 ? _player1.Team : _player2.Team);
            }
        }
        private void AssignCharactersToTeam(List<string> characterNames, Team team)
        {
            for (int i = 0; i < characterNames.Count; i++)
            {
                var characterName = characterNames[i];
                _view.WriteLine($"{i}: {characterName}");
            }

            string input = _view.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 0 && choice < characterNames.Count)
            {
                var selectedCharacterName = characterNames[choice];
                var originalCharacter = characters.FirstOrDefault(c => c.Nombre == selectedCharacterName);
                if (originalCharacter != null)
                {
                    // Crea una nueva instancia del personaje para evitar duplicados
                    var newCharacter = new Character
                    {
                        Nombre = originalCharacter.Nombre,
                        Arma = originalCharacter.Arma,
                        Género = originalCharacter.Género,
                        HPmáximo = originalCharacter.HPmáximo,
                        HPactual = originalCharacter.HPactual,
                        Atk = originalCharacter.Atk,
                        Spd = originalCharacter.Spd,
                        Def = originalCharacter.Def,
                        Res = originalCharacter.Res,
                    };

                    // Copia las habilidades del personaje original al nuevo personaje
                    
                }
                else
                {
                    _view.WriteLine($"Personaje no encontrado: {selectedCharacterName}");
                }
            }
            else
            {
                _view.WriteLine("Selección inválida.");
            }
        }
        
        
        



        public void ImportarCharacters()
        {
            // Ruta corregida al archivo characters.json
            string jsonPath = Path.Combine(_teamsFolder, "../..", "characters.json"); // Sube dos niveles en la jerarquía de directorios

            try
            {
                // Lee el archivo JSON
                string jsonString = File.ReadAllText(jsonPath);

                // Configura JsonSerializerOptions para usar el convertidor personalizado
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new StringToIntConverter() }
                };

                // Deserializa el contenido usando las opciones configuradas
                characters = JsonSerializer.Deserialize<List<Character>>(jsonString, options);
            }
            catch (Exception ex)
            {
                _view.WriteLine($"Error al importar personajes: {ex.Message}");
            }
        }


    }
}