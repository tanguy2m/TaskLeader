﻿using System;
using System.Windows.Forms;
using TaskLeader.GUI;

namespace TaskLeader.BO
{
    // Structure listant les différentes informations liées à une entité de la base
    public class DBentity
    {
        /// <summary>
        /// ID de l'entité dans la base
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nom de l'entité pour IHM
        /// </summary>
        public String nom { get; set; }

        /// <summary>
        /// Type de l'entité: List, Text, Date
        /// </summary>
        public String type { get; set; }

        /// <summary>
        /// ID de l'entité parente
        /// </summary>
        public int parentID { get; set; }

        /// <summary>
        /// Retourne le widget IHM correspondant au type
        /// </summary>
        /// <param name="dbName">Nom de la DB correspondante</param>
        /// <param name="value">EntityValue à sélectionner</param>
        /// <returns></returns>
        public UserControl getWidget(String dbName, EntityValue value)
        {
            switch (this.type)
            {
                case "List":
                    return new ListEntity(dbName, this.id, value);
                case "Text":
                    return new TextEntity(dbName, this.id, value);
                case "Date":
                    return new DateEntity(dbName, this.id, value);
                default:
                    return null;
            } 
        }    
    }

    public abstract class EntityValue
    {
        // Méthode privée pour fabriquer des string compatible sql
        protected String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }
        public abstract String sqlValue { get; }

        public abstract bool Equals(EntityValue b);

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            EntityValue comp = obj as EntityValue;
            if (comp == null)
                return false;
            else
                return Equals(comp);
        }

        public static bool operator ==(EntityValue a, EntityValue b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
                return true;

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
                return false;

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(EntityValue a, EntityValue b)
        {
            return !(a == b);
        }
    }

    /// <summary>
    /// Classe permettant de stocker des valeurs des entités de type List
    /// </summary>
    public class ListValue : EntityValue
    {
        /// <summary>
        /// Id de la valeur dans la  base
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// String représentant la valeur
        /// </summary>
        public String value { get; set; }
        public override String sqlValue { get { return this.sqlFactory(this.value); } }

        public override String ToString()
        {
            return value;
        }

        public override bool Equals(EntityValue b)
        {
            // Return true if the fields match:
            return (this.id == ((ListValue)b).id && (this.id > 0 || this.value == ((ListValue)b).value));
        }
    }

    public class DateValue : EntityValue
    {
        private DateTime _value;
        public DateTime value {
            get { return _value; }
            set { _value = value; }
        }
        public override String sqlValue { get { return "'" + this._value.ToString("yyyy-MM-dd") + "'"; } }

        public DateValue(String valeur)
        {
            DateTime.TryParse(valeur, out this._value); // Si le TryParse échoue, dateValue = DateTime.MinValue
        }

        public DateValue()
        {
            this._value = DateTime.MinValue;
        }

        public override bool Equals(EntityValue b)
        {
            // Return true if the fields match:
            return (this._value == ((DateValue)b).value);
        }
    }

    public class TextValue : EntityValue
    {
        public String value { get; set; }
        public override String sqlValue { get { return this.sqlFactory(this.value); } }

        public override bool Equals(EntityValue b)
        {
            return (this.value == ((TextValue)b).value);
        }

        public override string ToString()
        {
            return this.value;
        }
    }
}