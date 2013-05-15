using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_Apollo_16.ItemClasses
{
    class Weapon : BaseItem
    {
        #region Field Region
        
        int attackValue;
        int attackModifier;
        int damageValue;
        int damageModifier;
        
        #endregion

        #region Property Region

        public int AttackValue 
        {
            get { return attackValue; }
            protected set { attackValue = value; }
        
        }
        
        public int AttackModifier
        {
            get { return attackModifier; }
            protected set { attackModifier = value; }
        
        }
        
        public int DamageValue
        {
            get { return damageValue; }
            protected set { damageValue = value; }
        }
        
        public int DamageModifier
        {
            get { return damageModifier; }
            protected set { damageModifier = value; }
        }
        #endregion

        #region Constructor Region

        public Weapon(
            string weaponName,
            string weaponType,
            int attackValue,
            int attackModifier,
            int damageValue,
            int damageModifier,
            params Type[] allowableClasses)
            : base(weaponName, weaponType, allowableClasses)
        {
            AttackValue = attackValue;
            AttackModifier = attackModifier;
            DamageValue = damageValue;
            DamageModifier = damageModifier;        
        }
       
        #endregion
         
        #region Abstract Method Region
            public override object Clone()
            {
                Type[] allowedClasses = new Type[allowableClasses.Count];
                for (int i = 0; i < allowableClasses.Count; i++)
                    allowedClasses[i] = allowableClasses[i];
                
                 Weapon weapon = new Weapon(
                     Name,
                     Type,
                     AttackModifier,
                     DamageValue,
                     DamageModifier,
                     allowedClasses);
                 return weapon;
                 }
            public override string ToString()
            {
                string weaponString = base.ToString() + ", ";
                weaponString += AttackValue.ToString() + ", ";
                weaponString += AttackModifier.ToString() + ", ";
                weaponString += DamageValue.ToString() + ", ";
                weaponString += DamageModifier.ToString();
                foreach (Type t in allowableClasses)
                    weaponString += ", " + t.Name;
                return base.ToString();
             }
             #endregion
 }

        
    }

