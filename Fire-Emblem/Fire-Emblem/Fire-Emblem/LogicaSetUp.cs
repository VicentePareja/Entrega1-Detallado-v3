﻿using System;
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
        private List<Skill> skills;
        private Player _player1;
        private Player _player2;

        public LogicaSetUp(View view, string teamsFolder)
        {
            _view = view;
            _teamsFolder = teamsFolder;
            characters = new List<Character>();
            
        }

        public bool CargarEquipos(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
    
            MostrarArchivosDisponibles();
            string selectedFile = SeleccionarArchivo();
    
            if (selectedFile == null)
            {
                _view.WriteLine("Selección inválida.");
                return false;
            }
    
            ImportarCharacters();
            ImportarSkills();
    
            if (ValidTeams(selectedFile))
            {
                ChooseCharacters(selectedFile);
                return true;
            }
            else
            {
                _view.WriteLine("Archivo de equipos no válido");
                return false;
            }
        }

        private void MostrarArchivosDisponibles()
        {
            _view.WriteLine("Elige un archivo para cargar los equipos");
            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (files.Length == 0)
            {
                _view.WriteLine("No hay archivos disponibles.");
                throw new InvalidOperationException("No hay archivos disponibles.");
            }

            for (int i = 0; i < files.Length; i++)
            {
                _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
            }
        }

        private string SeleccionarArchivo()
        {
            string input = _view.ReadLine();
            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (int.TryParse(input, out int fileIndex) && fileIndex >= 0 && fileIndex < files.Length)
            {
                return files[fileIndex];
            }
            else
            {
                return null;
            }
        }


        public bool ValidTeams(string selectedFile)
        {
            var (lines, isPlayer1, team1, team2, team1Populated
                , team2Populated, currentTeamNames) = InitializeTeams(selectedFile);
            
            (isPlayer1, team1Populated, team2Populated) = ProcessTeamLines(lines, isPlayer1, team1, team2, currentTeamNames);
            
            return CheckFinalTeamsPopulated(currentTeamNames, isPlayer1, team1, team2, team1Populated, team2Populated);
        }
        
        private (bool isPlayer1, bool team1Populated, bool team2Populated) ProcessTeamLines(string[] lines
            , bool isPlayer1, Team team1, Team team2, List<string> currentTeamNames)
        {
            bool team1Populated = false;
            bool team2Populated = false;
        
            foreach (var line in lines)
            {
                if (line == "Player 1 Team")
                {
                    if (!isPlayer1 && currentTeamNames.Any())
                    {
                        team2Populated = true;
                        if (!FinalizeTeam(currentTeamNames, team2)) return (isPlayer1, team1Populated, team2Populated);
                    }
                    isPlayer1 = true;
                }
                else if (line == "Player 2 Team")
                {
                    if (isPlayer1 && currentTeamNames.Any())
                    {
                        team1Populated = true;
                        if (!FinalizeTeam(currentTeamNames, team1)) return (isPlayer1, team1Populated, team2Populated);
                    }
                    isPlayer1 = false;
                }
                else
                {
                    currentTeamNames.Add(line);
                }
            }
        
            return (isPlayer1, team1Populated, team2Populated);
        }


        private bool CheckFinalTeamsPopulated(List<string> currentTeamNames, bool isPlayer1
            , Team team1, Team team2, bool team1Populated, bool team2Populated)
        {
            if (currentTeamNames.Any())
            {
                if (isPlayer1)
                {
                    team1Populated = true;
                }
                else
                {
                    team2Populated = true;
                }
                if (!FinalizeTeam(currentTeamNames, isPlayer1 ? team1 : team2)) return false;
            }

            return team1Populated && team2Populated;
        }

        private (string[] lines, bool isPlayer1, Team team1, Team team2, bool team1Populated
            , bool team2Populated, List<string> currentTeamNames) InitializeTeams(string selectedFile)
        {
            var lines = File.ReadAllLines(selectedFile);
            bool isPlayer1 = true;
            Team team1 = new Team();
            Team team2 = new Team();
            bool team1Populated = false;
            bool team2Populated = false;
            List<string> currentTeamNames = new List<string>();
            return (lines, isPlayer1, team1, team2, team1Populated, team2Populated, currentTeamNames);
        }

        private bool FinalizeTeam(List<string> currentTeamNames, Team team)
        {
            bool valid = ValidateAndClearCurrentTeam(currentTeamNames, team);
            currentTeamNames.Clear();
            return valid;
        }
        
        private Character CreateCharacterFromLine(string characterLine)
        {
            var parts = characterLine.Split(" (", 2);
            var characterName = parts[0];
            var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

            var character = new Character { Nombre = characterName };

            if (!string.IsNullOrEmpty(skillsText))
            {
                var skillNames = skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var skillName in skillNames)
                {
                    var trimmedSkillName = skillName.Trim();
                    character.AddSkill(new Skill(trimmedSkillName, "Descripción no proporcionada")); 
                }
            }

            return character;
        }

        private bool ValidateTeam(Team team)
        {
            return team.EsEquipoValido();
        }

        private void ClearTeamCharacters(Team team)
        {
            team.Characters.Clear();
        }

        private bool ValidateAndClearCurrentTeam(List<string> characterNames, Team team)
        {
            foreach (var characterLine in characterNames)
            {
                var character = CreateCharacterFromLine(characterLine);
                team.Characters.Add(character);
            }

            bool isValid = ValidateTeam(team);
            ClearTeamCharacters(team);

            return isValid;
        }

        
        public void ChooseCharacters(string selectedFilePath)
        {
            var lines = File.ReadAllLines(selectedFilePath);
            bool isPlayer1 = true; 

            foreach (var line in lines)
            {
                if (line == "Player 1 Team")
                {
                    isPlayer1 = true;
                }
                else if (line == "Player 2 Team")
                {
                    isPlayer1 = false;
                }
                else
                {
                    AssignCharacterToTeam(line, isPlayer1 ? _player1.Team : _player2.Team);
                }
            }
        }


        private Character CloneCharacter(string characterName)
        {
            var originalCharacter = characters.FirstOrDefault(c => c.Nombre == characterName);
            if (originalCharacter != null)
            {
                return new Character
                {
                    Nombre = originalCharacter.Nombre,
                    Arma = originalCharacter.Arma,
                    Género = originalCharacter.Género,
                    HPmáximo = originalCharacter.HPmáximo,
                    HPactual = originalCharacter.HPmáximo,
                    Atk = originalCharacter.Atk,
                    Spd = originalCharacter.Spd,
                    Def = originalCharacter.Def,
                    Res = originalCharacter.Res,
                };
            }
            
            return null;
        }

        private Character CreateOrCloneCharacter(string characterLine)
        {
            var parts = characterLine.Split(" (", 2);
            var characterName = parts[0];
            var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

            var newCharacter = CloneCharacter(characterName);
            if (newCharacter != null)
            {
                AssignSkillsToCharacter(newCharacter, skillsText);
            }

            return newCharacter;
        }

        private void AssignSkillsToCharacter(Character character, string skillsText)
        {
            if (!string.IsNullOrEmpty(skillsText))
            {
                var skillNames = skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var skillName in skillNames)
                {
                    var trimmedSkillName = skillName.Trim();
                    character.AddSkill(new Skill(trimmedSkillName, "Descripción no proporcionada"));
                }
            }
        }


        private void AssignCharacterToTeam(string characterLine, Team team)
        {
            var newCharacter = CreateOrCloneCharacter(characterLine);
            if (newCharacter != null)
            {
                team.Characters.Add(newCharacter);
            }
            else
            {
                _view.WriteLine($"Personaje no encontrado: {characterLine.Split(" (", 2)[0]}");
            }
        }

        

        public void ImportarCharacters()
        {
            
            string jsonPath = Path.Combine(_teamsFolder, "../..", "characters.json"); 

            try
            {
                
                string jsonString = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new StringToIntConverter() }
                };
                
                characters = JsonSerializer.Deserialize<List<Character>>(jsonString, options);
            }
            catch (Exception ex)
            {
                _view.WriteLine($"Error al importar personajes: {ex.Message}");
            }
        }
        
        public void ImportarSkills()
        {
            string jsonPath = Path.Combine(_teamsFolder, "../..", "skills.json");

            try
            {
                
                string jsonString = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                skills = JsonSerializer.Deserialize<List<Skill>>(jsonString, options);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al importar habilidades: {ex.Message}");
            }
        }



    }
}