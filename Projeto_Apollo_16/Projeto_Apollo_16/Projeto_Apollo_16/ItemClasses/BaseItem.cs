using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_Apollo_16
{
    //public enum Wings { One, Two }
    //public enum WeaponLocation { Wings, Tail ,   }
    public abstract class BaseItem
    {
        #region Field Region
        
        protected List<Type> allowableClasses = new List<Type>();
        
        string name;
        string type;
        bool equipped;
         
        #endregion
         
        #region Property Region
        
        public List<Type> AllowableClasses
        {
            get { return allowableClasses; }
            protected set { allowableClasses = value; }
        }
        public string Type
        {
            get { return type; }
            protected set { type = value; }
        }
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
        public bool IsEquiped
        {
            get { return equipped; }
            protected set { equipped = value; }
        }
        #endregion

        #region Constructor Region
        
        public BaseItem(string name, string type, params Type[]
        allowableClasses)
        {
            foreach (Type t in allowableClasses)
                AllowableClasses.Add(t);
         Name = name;
         Type = type;
         IsEquiped = false;
        }
         
        #endregion
         
        #region Abstract Method Region
        
        public abstract object Clone();
        
        public virtual bool CanEquip(Type characterType)
        {
            return allowableClasses.Contains(characterType);
        }
        public override string ToString()
        {
            string itemString = "";
            itemString += Name + ", ";
            itemString += Type + ", ";
            return itemString;
        }
        #endregion
    }
}
