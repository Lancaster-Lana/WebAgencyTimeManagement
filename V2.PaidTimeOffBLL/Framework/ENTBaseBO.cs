﻿using System;
using System.Data.Linq;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffBLL.Framework
{
    /// <summary>
    /// The BaseBO class is the Base for any business object class that will retrieve data from the database.
    /// </summary>    
    [Serializable]
    public abstract class ENTBaseBO
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ENTBaseBO() { }

        #endregion Constructor

        #region Properties

        public int ID { get; set; }
        public DateTime InsertDate { get; private set; }
        public int InsertENTUserAccountId { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public int UpdateENTUserAccountId { get; private set; }
        public Binary Version { get; set; }

        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// The property is used when binding to a control.
        /// </summary>
        public string DisplayText
        {
            get { return GetDisplayText(); }
        }

        #endregion Properties

        #region Abstract Methods

        /// <summary>
        /// Get the record from the database and load the object's properties
        /// </summary>
        /// <returns>Returns true if the record is found.</returns>
        public abstract bool Load(int id);

        /// <summary>
        /// This method will map the fields in the data reader to the member variables in the object.
        /// </summary>
        protected abstract void MapEntityToCustomProperties(IENTBaseEntity entity);

        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// </summary>
        protected abstract string GetDisplayText();

        #endregion Abstratct Methods

        #region Public Methods

        /// <summary>
        /// This method will load all the properties of the object from the entity.
        /// </summary>        
        public void MapEntityToProperties(IENTBaseEntity entity)
        {            
            if (entity != null)
            {
                InsertDate = entity.InsertDate;
                InsertENTUserAccountId = entity.InsertENTUserAccountId;
                UpdateDate = entity.UpdateDate;
                UpdateENTUserAccountId = entity.UpdateENTUserAccountId;
                Version = entity.Version;
                this.MapEntityToCustomProperties(entity);
            }
        }

        #endregion Public Methods
    }     
}
