using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_Apollo_16.ItemClasses
{
    class Shield : BaseItem
    {
        #region Field Region
        
        int shieldValue;
        int shieldModifier;
        
        #endregion

        #region Property Region

        public int ShieldValue 
        {
            get { return shieldValue; }
            protected set { shieldValue = value; }
        
        }
        
        public int ShieldModifier
        {
            get { return shieldModifier; }
            protected set { shieldModifier = value; }
        
        }
        
        #endregion

        #region Constructor Region

        public Shield(
            string shieldName,
            string shieldType,
            int shieldValue,
            int shieldModifier,
            params Type[] allowableClasses)
            : base(shieldName, shieldType, allowableClasses)
        {
            //shieldValue = shieldValue;    //não faz nada
            //shieldModifier = shieldModifier;
        }
       
        #endregion
         
        #region Abstract Method Region
            public override object Clone()
            {
                Type[] allowedClasses = new Type[allowableClasses.Count];
                for (int i = 0; i < allowableClasses.Count; i++)
                    allowedClasses[i] = allowableClasses[i];
                
                 Shield shield = new Shield(
                     Name,
                     Type,
                     shieldValue,
                     shieldModifier,
                     allowedClasses);
                 return shield;
            }
            public override string ToString()
            {
                string shieldString = base.ToString() + ", ";
                shieldString += shieldValue.ToString() + ", ";
                shieldString += shieldModifier.ToString() + ", ";
                foreach (Type t in allowableClasses)
                    shieldString += ", " + t.Name;
                return shieldString;
             }
             #endregion

    }
}
