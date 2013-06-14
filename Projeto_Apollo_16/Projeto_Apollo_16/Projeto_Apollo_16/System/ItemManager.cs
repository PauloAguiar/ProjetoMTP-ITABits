using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_Apollo_16.ItemClasses
{
    class ItemManager
    {
        #region Fields Region

        Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
        Dictionary<string, Shield> shields = new Dictionary<string, Shield>();

        #endregion  

        #region Keys Property Region

        public Dictionary<string, Weapon>.KeyCollection weaponKeys
        {
            get { return weapons.Keys; }
        }

        public Dictionary<string, Shield>.KeyCollection ShieldKeys
        {
            get { return shields.Keys; }
        }

        #endregion

        #region Constructor Region

        public ItemManager() 
        {
        }

        #endregion

        #region Weapon Methods

        public void AddWeapon(Weapon weapon)
        {
            if (!weapons.ContainsKey(weapon.Name)) 
            {
                weapons.Add(weapon.Name, weapon);
            }
        }

        public Weapon GetWeapon(string name) 
        {
            if (weapons.ContainsKey(name)) 
            {
                return (Weapon)weapons[name].Clone();
            }

            return null;
        }

        public bool ContainsWeapon(string name) 
        {
            return weapons.ContainsKey(name);
        }

        #endregion

        #region Shield Methods

        public void AddShield(Shield shield) 
        {
            if (!shields.ContainsKey(shield.Name))
            {
                shields.Add(shield.Name, shield);
            }
        }

        public Shield GetShield(string name) 
        {
            if (shields.ContainsKey(name)) 
            {
                return (Shield)shields[name].Clone();
            }

            return null;
        }

        public bool ContainsShield(string name)
        {
            return shields.ContainsKey(name);
        }

        #endregion
    }
}
