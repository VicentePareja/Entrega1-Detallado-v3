﻿using Fire_Emblem_View;
namespace Fire_Emblem
{
    public class Combat
    {
        private readonly Character _attacker;
        private readonly Character _defender;
        private readonly string _advantage;
        private readonly View _view;

        public Combat(Character attacker, Character defender, string advantage, View view)
        {
            _attacker = attacker;
            _defender = defender;
            _advantage = advantage;
            _view = view;
        }

        public void Start()
        {
            ApplySkills();
            PerformInitialAttack();
            PerformCounterAttack();
            PerformFollowUp();
            PrintFinalState();
        }

        private void ApplySkills()
        {
            foreach (var skill in _attacker.Skills) {
                skill.ApplyEffect(_attacker);
            }
            foreach (var skill in _defender.Skills) {
                skill.ApplyEffect(_defender);
            }
        }

        private void PerformInitialAttack()
        {
            Attack attack = new Attack(_attacker, _defender, _view);
            attack.PerformAttack(_advantage);
        }

        private void PerformCounterAttack()
        {
            if (_defender.CurrentHP > 0)
            {
                Attack counterAttack = new Attack(_attacker, _defender, _view);
                counterAttack.PerformCounterAttack(_advantage);
            }
        }

        private void PerformFollowUp()
        {
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                Attack followUpAttack = new Attack(_attacker, _defender, _view);
                if (_attacker.Spd >= _defender.Spd + 5)
                {
                    followUpAttack.PerformAttack(_advantage);
                }
                else if (_defender.Spd >= _attacker.Spd + 5)
                {
                    followUpAttack.PerformCounterAttack(_advantage);
                }
                else
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
            }
        }

        private void PrintFinalState()
        {
            _view.WriteLine($"{_attacker.Name} ({_attacker.CurrentHP}) : {_defender.Name} ({_defender.CurrentHP})");
        }
    }
}
